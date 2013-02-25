using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace CommonTool
{
    public class WebSvr
    {
        private Socket m_Socket;
        private IPEndPoint m_EP;
        private IPAddress m_BroadCast;

        public WebSvr(string Address, int nPort)
        {
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            m_BroadCast = IPAddress.Parse(Address);
            m_EP = new IPEndPoint(m_BroadCast, nPort);
        }

        public GShare.ErrorCode SendMessage(string msg)
        {
            try
            {
                if (msg.Length == 0)
                    return GShare.ErrorCode.eUDPClientMsgNull;

                byte[] sendbuf = Encoding.ASCII.GetBytes(msg);
                m_Socket.SendTo(sendbuf, m_EP);
            }
            catch(Exception e)
            {
                Log.WriteLog(GShare.LogMark.eError, e.ToString());
                return GShare.ErrorCode.eCatchUdpException;
            }

            return GShare.ErrorCode.eSuccess;
        }

    }
}
