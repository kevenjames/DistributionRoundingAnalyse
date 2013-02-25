using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CommonTool;

namespace Rounding
{
    public partial class RoundingAnalyse : ServiceBase
    {
        public RoundingAnalyse()
        {
            InitializeComponent();
            m_bStopping = false;
            m_eStoppedEvent = new ManualResetEvent(false);
        }

        protected override void OnStart(string[] args)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ServiceWorkerThread));
        }

        protected override void OnStop()
        {
            m_bStopping = true;
            m_eStoppedEvent.WaitOne();
        }

        private void ServiceWorkerThread(object state)
        {
            RoundingFrameWork mainfrm = new RoundingFrameWork();
            if (mainfrm.InitialInstance() != GShare.ErrorCode.eSuccess)
            {
                mainfrm.Destroy();
                return;
            }

            while (!m_bStopping)
            {
                mainfrm.Run();
            }

            mainfrm.Destroy();

            m_eStoppedEvent.Set();
        }

        private bool m_bStopping;
        private ManualResetEvent m_eStoppedEvent;
    }
}
