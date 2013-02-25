using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace CommonTool
{
    public class UDPListener : ThreadContainer
    {
        private static UDPListener m_Instance = null;
        private static object _lock = new object();

        private string m_strReceived;
        private int m_nPort;
        public delegate void MessageReceivedHandler(object sender, DREventArgs e);
        public event MessageReceivedHandler MsgRecved; 

        private UDPListener( int nPort )
        {
            m_nPort = nPort;
        }
        public static UDPListener GetInstance(int nPort)
        {
            if (m_Instance == null)
            {
                lock (_lock)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = new UDPListener(nPort);
                    }
                }
            }

            return m_Instance;
        }
        public void InitialListener()
        { 
            m_Instance.MessageSend += new MsgEventHandler(UDPResponse);
            m_Instance.Start();
            m_strReceived = "";
        }
        private void UDPResponse(object sender, DREventArgs e)
        {
            m_strReceived = e.msg;
        }
        public override void StartListener()
        {
            bool done = false;

            UdpClient listener = new UdpClient(m_nPort);
            IPEndPoint grpEP = new IPEndPoint(IPAddress.Any, m_nPort);

            try
            {
                while (!done)
                {
                    byte[] bytes = listener.Receive(ref grpEP);
                    string strPort = grpEP.ToString();
                    string strReceive = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                    MsgRecved(this, new DREventArgs(strReceive));
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(GShare.LogMark.eError, e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }
        public bool MsgReceived(string strMsg)
        {
            strMsg = m_strReceived;
            return (m_strReceived.Length > 0);
        }
    }
}
