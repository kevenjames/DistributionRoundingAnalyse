using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTool;
using System.Collections;

namespace ParseData
{
    public class KLineDayParser : IDataParser
    {
        RequestParameter m_reqParam;
        ArrayList m_arrFilePath;
        List<KLineDay> m_lstKLine;
        Log m_log;
        ParseDataParameter m_ParseParam;

        class KLineDay
        {
            public string code;
            public GShare.KLineDataType type;
            public DateTime date;
            public Decimal open;
            public Decimal high;
            public Decimal low;
            public Decimal close;
            public long amount;
            public int vol;

            public string ToSQL()
            {
                return "";
            }
        }

        public KLineDayParser(RequestParameter reqParam)
        {
            m_log = Log.GetInstance(GShare.cstServiceParseData);
            m_reqParam = reqParam;
            if (m_ParseParam == null)
            {
                m_ParseParam = new ParseDataParameter();
                m_ParseParam.Initial(reqParam.m_strParamPath);
            }
                
        }
        public void Run()
        {
            PreHandle();
            ParseData();
            AfterParse();
        }
        public void PreHandle()
        {
            m_arrFilePath = new ArrayList();
            string strSZKLineFullPath = m_ParseParam.GetHomePath() + m_ParseParam.GetSZDayPath();
            string strSHKLineFullPath = m_ParseParam.GetHomePath() + m_ParseParam.GetSHDayPath();
            
            DirectoryInfo dirSH = new DirectoryInfo(strSHKLineFullPath);
            FileInfo[] files = dirSH.GetFiles();
            foreach( FileInfo file in files )
            {
                m_arrFilePath.Add(file.FullName);
            }

            DirectoryInfo dirSZ = new DirectoryInfo(strSZKLineFullPath);
            files = dirSZ.GetFiles();
            foreach (FileInfo file in files)
            {
                m_arrFilePath.Add(file.FullName);
            }

            return;
        }
        public void AfterParse()
        {
            return;
        }
        public void ParseData()
        {
            int nCount = 0;
            int nTotalFiles = m_arrFilePath.Count;
            
            m_lstKLine = new List<KLineDay>();
            foreach (string strFilePath in m_arrFilePath)
            {
                nCount++;
                SendStatusReport(nCount, nTotalFiles, strFilePath);

                string code = strFilePath.Substring(strFilePath.Length - 10, 6);
                FileStream fs = File.OpenRead(strFilePath);
                BinaryReader br = new BinaryReader(fs);
                int days = (int)fs.Length / 32;
                for (int i = 0; i < days; i++)
                {
                    KLineDay inf = new KLineDay();
                    int date = br.ReadInt32();
                    int year = date / 10000;
                    int month = int.Parse(date.ToString().Substring(4, 2));
                    int day = int.Parse(date.ToString().Substring(6, 2));
                    int open = br.ReadInt32();
                    int high = br.ReadInt32();
                    int low = br.ReadInt32();
                    int close = br.ReadInt32();
                    Single amount = br.ReadSingle();
                    decimal am = Convert.ToDecimal(amount);
                    long amstr = Convert.ToInt64(amount);
                    int vol = br.ReadInt32();
                    int preclose = br.ReadInt32();

                    inf.code = code;
                    inf.type = GShare.KLineDataType.eDay;
                    inf.date = new DateTime(year, month, day);
                    inf.open = Convert.ToDecimal(open / 100m);
                    inf.high = Convert.ToDecimal(high / 100m);
                    inf.low = Convert.ToDecimal(low / 100m);
                    inf.close = Convert.ToDecimal(close / 100m);
                    inf.amount = amstr;
                    inf.vol = vol;

                    m_lstKLine.Add(inf);
                }
                
                string strLog = "File " + strFilePath + " parsed.";
                Log.Log_Debug(strLog);

                br.Close();
                fs.Close();
            }
            return;
        }
        void SendStatusReport( int nCount, int nTotalCount, string strCurrentFile )
        {
            StatusReportParameter status = new StatusReportParameter(nCount, nTotalCount, strCurrentFile);

            WebSvr ws = new WebSvr(GShare.cstIPAddress, GShare.cnPortForTaskManager);
            ws.SendMessage(status.GetXml());
        }
    }
}
