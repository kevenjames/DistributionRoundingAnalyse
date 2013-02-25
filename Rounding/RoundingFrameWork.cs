using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonTool;
using System.ServiceProcess;

namespace Rounding
{
    class RoundingFrameWork : AbstractRoundingFrameWork
	{
        private UDPListener m_udpListener;
        private delegate void MessageHandle(DREventArgs e);
        public delegate void MsgEventHandler(object sender, DREventArgs e);

        private Log m_log;
        private string m_strRec;

        public override GShare.ErrorCode InitialInstance()
        {
            //Initial Log
            m_log = Log.GetInstance(GShare.cstServiceRoundingAnalyse);

            //Initial UDP Listener
            m_udpListener = UDPListener.GetInstance(GShare.cnPortForRoundingAnalyse);
            m_udpListener.MsgRecved += new UDPListener.MessageReceivedHandler(UdpMessageReceived);
            m_udpListener.InitialListener();

            return GShare.ErrorCode.eSuccess;
        }

        public override void Run()
        {
            try
            {
                string strMsg = "";
                if (!m_udpListener.MsgReceived(strMsg))
                {
                    Thread.Sleep(1000);
                    return;
                }

                Log.WriteLog(GShare.LogMark.eMessage, "Received msg: " + strMsg);
                WebSvr ws = new WebSvr(GShare.cstIPAddress, GShare.cnPortForTaskManager);

                string strSend = "TaskManger, this is ParseData speaking." + DateTime.Now.ToString("HH:mm:ss");
                ws.SendMessage(strSend);

                Thread.Sleep(1000);

            }
            catch (SystemException e)
            {
                Log.WriteLog(GShare.LogMark.eError, "Exception:" + e.Message);
            }
        }

        public override void Destroy()
        {
            Log.WriteLog(GShare.LogMark.eMessage, "ParseData Destroy");

            //Stop UDP Listener
            m_udpListener.Stop();
        }

        public void UdpMessageReceived(object sender, DREventArgs e)
        {
            m_strRec = e.msg;
        }
	}
}
