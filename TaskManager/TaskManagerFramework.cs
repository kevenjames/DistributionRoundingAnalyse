using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonTool;
using System.ServiceProcess;

namespace TaskManager
{
	class TaskManagerFramework : AbstractTaskManagerFramework
	{
		private Log m_log;

        private UDPListener m_udpListener;
        private delegate void MessageHandle(DREventArgs e);
        public delegate void MsgEventHandler(object sender, DREventArgs e);
        private string m_strRec;


		public override GShare.ErrorCode InitialInstance()
		{
			//Read config
			//for hard code right now

            //Initial Log
			m_log = Log.GetInstance(GShare.cstServiceTaskManager);

            //Pull up Service ParseData and RoundingAnalyse
            if (!CommonFunction.SvrOperate(GShare.cstServiceParseData, true) || !CommonFunction.SvrOperate(GShare.cstServiceRoundingAnalyse, true))
                return GShare.ErrorCode.eServiceStartFailed;

            //Initial UDP listener
            m_udpListener = UDPListener.GetInstance(GShare.cnPortForTaskManager);
            m_udpListener.MsgRecved += new UDPListener.MessageReceivedHandler(UdpMessageReceived);
            m_udpListener.InitialListener();

            return GShare.ErrorCode.eSuccess;
		}
        public override void Run()
		{
            try
            {
                if (m_strRec==null || m_strRec.Length == 0)
                {
                    Thread.Sleep(1000);
                    return;
                }

                IController ctrl = ControllerFactory.Factory(m_strRec);
                ctrl.Run();

                m_strRec = "";
                
                Thread.Sleep(1000);

            }
            catch (SystemException e)
            {
                Log.WriteLog(GShare.LogMark.eError, e.Message);
            }
		}
		public override void Destroy()
		{
			Log.WriteLog(GShare.LogMark.eMessage, "TaskManager Destroy");

            //Stop Serivce ParseData
            CommonFunction.SvrOperate(GShare.cstServiceParseData, false);
            CommonFunction.SvrOperate(GShare.cstServiceRoundingAnalyse, false);

            m_udpListener.Stop();

            //Distroy
		}
        public void UdpMessageReceived(object sender, DREventArgs e)
        {
            m_strRec = e.msg;
        }
	}
}
