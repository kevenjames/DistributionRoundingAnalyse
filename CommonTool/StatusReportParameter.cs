using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTool
{
    public class StatusReportParameter
    {
        GShare.Request m_eRequest;
        GShare.Request m_eSubRequest = GShare.Request.eStatusReport;
        int m_nCurrentCount;
        int m_nTotalCount;
        string m_strFilePath;

        public StatusReportParameter(int nCount, int nTotalCount, string strFilePath)
        {
            m_nCurrentCount = nCount;
            m_nTotalCount = nTotalCount;
            m_strFilePath = strFilePath;
        }

        public string GetXml()
        {
            string strFileName = m_strFilePath.Substring(m_strFilePath.Length - 12, 12);
            double dPercent = (m_nCurrentCount / m_nTotalCount) * 100;
            GShare.JobStatus eStatus = dPercent==100 ? GShare.JobStatus.eOnGoing : GShare.JobStatus.eEnd;
            string strReport = string.Format("解析{0}中，完成{1:00.00}。", strFileName, dPercent);
            string msg = string.Format("<dra><req id='{0:d}' subid='{1:d}' status='{2:d}' msg='{3}'>", 
                m_eRequest, m_eSubRequest, eStatus, strReport );
            return msg;
        }
    }
}
