using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonTool;
using System.ServiceProcess;

namespace ParseData
{
	class ParseDataFrameWork : AbstractParseDataFrameWork
	{
        private UDPListener m_udpListener;
        private delegate void MessageHandle(DREventArgs e);
        public delegate void MsgEventHandler(object sender, DREventArgs e);

        public Log m_log;
        private string m_strRec;
        ParseDataParameter m_param;

        public override GShare.ErrorCode InitialInstance()
        {
            //Initial Log
            m_log = Log.GetInstance(GShare.cstServiceParseData);

            //Initial UDP Listener
            m_udpListener = UDPListener.GetInstance(GShare.cnPortForParseData);
            m_udpListener.MsgRecved += new UDPListener.MessageReceivedHandler(UdpMessageReceived);
            m_udpListener.InitialListener();
            
            return GShare.ErrorCode.eSuccess;

        }

        public override void Run()
        {
            try
            {
                if (m_strRec == null || m_strRec.Length == 0)
                {
                    Thread.Sleep(1000);
                    return;
                }

                RequestParameter rp = new RequestParameter(m_strRec);
                IDataParser dp = DataParserFactory.FactoryMethod(rp);
                dp.Run();
                
                m_strRec = "";
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

            m_udpListener.Stop();
        }
        public void UdpMessageReceived(object sender, DREventArgs e)
        {
            m_strRec = e.msg;
        }
	}
}
