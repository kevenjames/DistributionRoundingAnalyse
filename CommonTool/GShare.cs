using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace CommonTool
{
    public class GShare
    {
        private static GShare m_Instance;
        private static object _lock = new object();

        private Dictionary<string, string> m_dicString;
        private Dictionary<string, int> m_dicInt;

        //const integer
        public const int cnConnectMillis                    = 5000;

        public const int cnPackageSize                      = 100;

        //Service name
        public const string cstServiceParseData             = "ParseData";
        public const string cstServiceTaskManager           = "TaskManager";
        public const string cstServiceRoundingAnalyse       = "RoundingAnalyse";
        public const string cstDBOperate                    = "DBOperation";

        //Server name
        public const string cstServerName                   = ".";
		//Pipe name
		public const string cstPipeToParseData		        = cstServerName + "/pipe/PipeParseData";
        public const string cstPipeToTaskManager            = cstServerName + "/pipe/PipeTaskManager";
        public const string cstPipeToRoundingAnalyse        = cstServerName + "/pipe/PipeRoundingAnalyse";

        //Server IP
        public const string cstIPAddress                    = "127.0.0.1";
        //Port
        public const int cnPortForTaskManager               = 11000;
        public const int cnPortForParseData                 = 11001;
        public const int cnPortForRoundingAnalyse           = 11002;
        public const int cnPortForUI                        = 11003;
        
		public const int cnBufferSize                       = 1048576;

        public const int cnKLineDayLength                   = 32;

        public enum LogMark
        { 
            eUndefined  = -1,
            eMessage    = 0,
            eError      = 1,
        }
        public enum ErrorCode
		{
			eUndefined	= -1,
			eSuccess	= 0,

			//1 - 10000 Common tool
			ePipeServerIsNull = 1,
            ePipeClientIsNull,
			ePipeServerDisconnected,
            ePipeClientDisconnected,
            eCatchUdpException,
            eServiceStartFailed,
            eUDPClientMsgNull,
            eDBConnectionCreateFailed,
            eDBOperateFailed,

			eFail		= 9999999,
		}
        public enum KLineDataType
        { 
            eUndefined = -1,
            eOneByOne,
            e5Minutes,
            e30Minutes,
            eDay,
            eWeek,
            eMonth,
        }
        public enum Request
        { 
            eInvalidRequest = -1,
            eNULL,
            eParseKLineData,
            eAnalyseAfterMarketClose,
            eTrackMarket,
            ePlaceAnOrder,
            eAnalyseTrading,
            eStatusReport,
        }
        public enum ParseMode
        { 
            eUndefined  = -1,
            eAppend,
            eReplace,
        }
        public enum JobStatus
        { 
            eUndefined = -1,
            eBegin,
            eOnGoing,
            eEnd,
        }

        private GShare() 
        {
            m_dicInt = new Dictionary<string,int>();
            m_dicString = new Dictionary<string,string>();
        }
        public static GShare GetInstance()
        {
            if (m_Instance == null)
            {
                lock (_lock)
                {
                    if (m_Instance == null)
                        m_Instance = new GShare();
                }
            }

            return m_Instance;
        }

        public void SetValue( string name, string val )
        {
            m_dicString.Add(name, val);

            return;
        }
        public void SetValue(string name, int val)
        {
            try
            {
                m_dicInt.Add(name, val);
            }
            catch (Exception ex)
            {
                Log.WriteLog(LogMark.eError, ex.Message);
            }
            

            return;
        }
        public string GetValue(string name, string val)
        {
            string msg = "";
            try
            {
                msg = m_dicString[name];
            }
            catch (KeyNotFoundException ex)
            {
                Log.WriteLog(LogMark.eError, ex.Message);
            }

            val = msg;
            return msg;
        }
        public int GetValue(string name, int val)
        {
            int Result = 0;
            try
            {
                Result = m_dicInt[name];
            }
            catch (KeyNotFoundException ex)
            {
                Log.WriteLog(LogMark.eError, ex.Message);
            }

            val = Result;
            return Result;
        }


    }
}
