using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Security.AccessControl;
using CommonTool;


namespace CommonTool
{
	public sealed class PipeServer
	{
		private static PipeServer m_Instance = null;
		private static object _lock = new object();
		private const int BufferSize = 1048576;

		private NamedPipeServerStream m_PipeServer;
        private string m_strPipeName;

		private PipeServer(){}
		private PipeServer( string strPipeName )
		{
			//Create pipe security object
			PipeSecurity ps = null;
			ps = CreatePipeSecurity();

			//Create a named pipe
			m_PipeServer = new NamedPipeServerStream(
								strPipeName,
								PipeDirection.InOut,
								NamedPipeServerStream.MaxAllowedServerInstances,
								PipeTransmissionMode.Message,
								PipeOptions.None,
								GShare.cnBufferSize,
								GShare.cnBufferSize,
								ps,
								HandleInheritability.None
								);

            m_strPipeName = strPipeName;
			Log.Log_Debug("The pipe server " + m_strPipeName + " is created." );
		}
    	public static PipeServer GetInstance(string strPipeName)
		{
			if( m_Instance == null )
			{
				lock(_lock)
				{
					if(m_Instance == null)
						m_Instance = new PipeServer(strPipeName);
				}
			}

			return m_Instance;
		}
        public void WaitForConnect()
		{
            if (m_PipeServer == null)
            {
                Log.Log_Error("pipe server " + m_strPipeName + " is null");
                return;
            }

            if (!m_PipeServer.IsConnected)
            {
                m_PipeServer.WaitForConnection();
                Log.Log_Debug("pipe server " + m_strPipeName + " connected.");
            }
			

            return;
		}
        public void Disconnect()
		{
			m_PipeServer.WaitForPipeDrain();
			m_PipeServer.Disconnect();
            Log.Log_Debug("pipe server" + m_strPipeName + " disconnected");
		}
        public bool IsConnected()
        {
            return m_PipeServer.IsConnected;
        }
        public void Close()
		{
			if( m_PipeServer != null )
			{
				m_PipeServer.Close();
				m_PipeServer = null;
                Log.Log_Info("Pipe server " + m_strPipeName + " closed");
			}
		}
        public GShare.ErrorCode WriteToPipe(string msg)
		{
			string message = msg;

			if (m_PipeServer == null)
				return GShare.ErrorCode.ePipeServerIsNull;
			if (!m_PipeServer.IsConnected)
				return GShare.ErrorCode.ePipeServerDisconnected;

			byte[] bResponse = Encoding.Unicode.GetBytes(message);
			int nResponse = bResponse.Length;

			m_PipeServer.Write( bResponse, 0, nResponse );

            Log.Log_Debug(m_strPipeName+ " write " +message);

			return GShare.ErrorCode.eSuccess;
		}
        public GShare.ErrorCode ReadFromPipe(string msg)
		{
			string message = "";

			if ( m_PipeServer == null )
				return GShare.ErrorCode.ePipeServerIsNull;
			if (!m_PipeServer.IsConnected)
				return GShare.ErrorCode.ePipeServerDisconnected;

			do
			{
				byte[] bResp = new byte[GShare.cnBufferSize];
				int cbResp = bResp.Length;
				int cbRead;

				cbRead = m_PipeServer.Read(bResp, 0, cbResp);
                message = Encoding.Unicode.GetString(bResp).TrimEnd('\0');
			}
			while (!m_PipeServer.IsMessageComplete);

			msg = message;

            Log.Log_Debug(m_strPipeName + " read " + message);

			return GShare.ErrorCode.eSuccess;

		}
        PipeSecurity CreatePipeSecurity()
		{
			PipeSecurity ps = new PipeSecurity();
			
			ps.SetAccessRule( new PipeAccessRule( "Authenticated Users", PipeAccessRights.ReadWrite, AccessControlType.Allow ) );
			ps.SetAccessRule( new PipeAccessRule( "Administrators", PipeAccessRights.FullControl, AccessControlType.Allow ) );

			return ps;
		}
	}
}
