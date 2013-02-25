using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTool;

namespace TaskManager
{
    class ParseDataContorller : IController
    {
        private static ParseDataContorller m_Instance;
        private static object _lock = new object();

        RequestParameter m_reqParam;
        GShare.JobStatus m_enJobStatus;
        private ParseDataContorller(RequestParameter param)
        {
            Init(param);
        }
        private void Init(RequestParameter param)
        {
            m_reqParam = param;
            m_enJobStatus = GShare.JobStatus.eUndefined;
        }
        public static ParseDataContorller GetInistance(RequestParameter param)
        {
            if (m_Instance == null)
            {
                lock (_lock)
                {
                    if (m_Instance == null)
                    { 
                        m_Instance = new ParseDataContorller(param);
                    }
                }
            }

            return m_Instance;
        }
        public void Run()
        {
            int nPort = 0;
            if (m_reqParam.m_enJobStatus == GShare.JobStatus.eUndefined)
            {
                nPort = GShare.cnPortForParseData;
                m_reqParam.m_enJobStatus = GShare.JobStatus.eBegin;
            }
            else
            {
                nPort = GShare.cnPortForUI;
            }
            
            WebSvr ws = new WebSvr(GShare.cstIPAddress, nPort);
            ws.SendMessage(m_reqParam.GetXml());
            return;
        }
    }
}
