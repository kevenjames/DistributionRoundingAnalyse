using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using CommonTool;


namespace CommonTool
{
	public sealed class PipeClient
	{
        private Dictionary<string, PipeClient> dicClient;

		private static PipeClient m_Instance = null;
		private static object _lock = new object();
		private const int BufferSize = 1048576;

		private NamedPipeClientStream m_PipeClient;
        private string m_strPipeName;

		private PipeClient() {}
		private PipeClient( string strServerName, string strPipeName )
		{
			m_PipeClient = new NamedPipeClientStream(
				strServerName,
				strPipeName,
				PipeDirection.InOut,
				PipeOptions.None
				);

            m_strPipeName = strPipeName;
            dicClient.Add(m_strPipeName, m_Instance);
		}
        public static PipeClient GetInstance( string strServerName, string strPipeName )
        {
             if (m_Instance == null)
             {
                 lock (_lock)
                 {
                     if (m_Instance == null)
                     {
                         m_Instance = new PipeClient(
                             strServerName,
                             strPipeName);
                     }
                 }
             }
            return m_Instance;
        }
        public bool Connect()
        {
            if (m_PipeClient == null)
            {
                Log.Log_Error("Pipe Client is null");
            }

            m_PipeClient.Connect(GShare.cnConnectMillis);

            return m_PipeClient.IsConnected;
        }
        public void Close()
        {
            if (m_PipeClient != null)
            {
                m_PipeClient.Close();
                m_PipeClient = null;
            }
            
        }
        public GShare.ErrorCode ReadFromPipe(string msg)
        {
            string message = "";

            if (!Connect())
            {
                Log.Log_Error("pipe client connect failed");
                return GShare.ErrorCode.ePipeClientDisconnected;
            }
                
            m_PipeClient.ReadMode = PipeTransmissionMode.Message;
            do
            {
                byte[] bResp = new byte[GShare.cnBufferSize];
                int cbResp = bResp.Length;
                int cbRead;

                cbRead = m_PipeClient.Read(bResp, 0, cbResp);
                message = Encoding.Unicode.GetString(bResp).TrimEnd('\0');
            }
            while (!m_PipeClient.IsMessageComplete);

            msg = message;

            m_PipeClient.Close();

            Log.Log_Debug(m_strPipeName + " read " + message);

            return GShare.ErrorCode.eSuccess;
        }
        public GShare.ErrorCode WriteToPipe(string msg)
        {
            string message = msg;

            if (!Connect())
            {
                Log.Log_Error("pipe client connect failed");
                return GShare.ErrorCode.ePipeClientDisconnected;
            }

            byte[] bResponse = Encoding.Unicode.GetBytes(message);
            int nResponse = bResponse.Length;

            m_PipeClient.Write(bResponse, 0, nResponse);

            m_PipeClient.Close();

            Log.Log_Debug(m_strPipeName + " write " + message);

            return GShare.ErrorCode.eSuccess;
        }

	}
}
