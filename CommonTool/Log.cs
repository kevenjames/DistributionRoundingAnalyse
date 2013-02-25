using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CommonTool
{
    public class Log
    {
        private string m_strLogPathName;
		private static Log m_Instance;
		private static object _lock = new object();
        const bool bLog = true;

		private Log(string strModual)
		{
			Init( strModual );
		}

        private void Init(string strModual)
        {
			string strPath = SetLogFilePathName(strModual);

			Environment.SetEnvironmentVariable( strModual, strPath );
        }

		public static Log GetInstance( string strModual )
		{
			if (m_Instance == null)
			{
				lock ( _lock )
				{
					if ( m_Instance == null )
						m_Instance = new Log(strModual);
				}
			}

			return m_Instance;
		}

		private string SetLogFilePathName( string strModual )
		{
			if (strModual != null)
			{
				m_strLogPathName = "C:\\DRLog\\" + strModual + ".log";
			}

			return m_strLogPathName;
		}

        public static void WriteLog(GShare.LogMark logMark, string msg)
        {
            if (!bLog)
                return;

            string mark;
            switch (logMark)
            { 
                case GShare.LogMark.eMessage:
                    mark = "Message:" + Environment.NewLine;
                    break;
                case GShare.LogMark.eError:
                    mark = "Error:" + Environment.NewLine;
                    break;
                default:
                    mark = "Not assigned" + Environment.NewLine;
                    break;

            }

            StreamWriter sw = File.AppendText( m_Instance.m_strLogPathName);
            sw.WriteLine(mark + DateTime.Now.ToString() + Environment.NewLine + msg + Environment.NewLine);
			sw.Close();

            return;
        }
    }
}
