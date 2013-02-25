using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CommonTool
{
    public static class Log
    {
        private static System.Diagnostics.EventLog m_log;

        private static void Init()
        {
            m_log = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("ParseDataLogSource"))
                System.Diagnostics.EventLog.CreateEventSource("ParseDataLogSource", "ParseDataLog");

            m_log.Source = "ParseDataLogSource";
            m_log.Log = "ParseDataLog";
        }

        public static void WriteLog(string msg)
        {
            Init();
            m_log.WriteEntry(msg);

            return;
        }
    }
}
