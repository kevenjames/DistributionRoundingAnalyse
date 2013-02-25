using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommonTool
{
    public class RequestParameter
    {
        public GShare.JobStatus m_enJobStatus;
        public GShare.Request m_enRequestId;
        public GShare.Request m_enReqSubId;
        public GShare.KLineDataType m_enKLineDataType;
        public GShare.ParseMode m_enParseMode;
        public string m_strMsg;
        public string m_strParamPath;

        public RequestParameter()
        {
            m_enJobStatus = GShare.JobStatus.eUndefined;
            m_enRequestId = GShare.Request.eInvalidRequest;
            m_enKLineDataType = GShare.KLineDataType.eUndefined;
            m_enParseMode = GShare.ParseMode.eUndefined;
        }
        public RequestParameter(string strReq)
        {
            m_enJobStatus = GShare.JobStatus.eUndefined;
            Initialize(strReq);
        }
        public GShare.ErrorCode Initialize(string strReq)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strReq);

            XmlNode node = xmlDoc.SelectSingleNode("//dra/req");
            XmlNamedNodeMap nnMap = node.Attributes;
            if(nnMap != null)
            {
                for (int i = 0; i < nnMap.Count; i++ )
                {
                    string attrName = nnMap.Item(i).Name;
                    string attrValue = nnMap.Item(i).Value;
                    if (attrName == "id")
                        m_enRequestId = (GShare.Request)int.Parse(attrValue);
                    else if (attrName == "subid")
                        m_enReqSubId = (GShare.Request)int.Parse(attrValue);
                    else if (attrName == "type")
                        m_enKLineDataType = (GShare.KLineDataType)int.Parse(attrValue);
                    else if (attrName == "mode")
                        m_enParseMode = (GShare.ParseMode)int.Parse(attrValue);
                    else if (attrName == "status")
                        m_enJobStatus = (GShare.JobStatus)int.Parse(attrValue);
                    else if (attrName == "msg")
                        m_strMsg = attrValue;
                    else if (attrName == "parapath")
                        m_strParamPath = attrValue;
                }
            }
            
            return GShare.ErrorCode.eSuccess;
        }
        public string GetXml()
        {
            string strXml = string.Format("<dra><req id='{0:d}' type='{1:d}' mode='{2:d}' msg='{3}' status='{4:d}' parapath='{5}'/></dra>",
                m_enRequestId, m_enKLineDataType, m_enParseMode, m_strMsg, m_enJobStatus, m_strParamPath);

            return strXml;
        }

    }
}
