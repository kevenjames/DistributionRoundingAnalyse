using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using CommonTool;

namespace ParseData
{
    public class ParseDataParameter
    {
        GShare.JobStatus m_enStatus;

        string m_strXmlPath;

        //Paths
        string m_strTDXHome;
        string m_strSHDayPath;
        string m_strSZDayPath;
        string m_strSH5MPath;
        string m_strSZ5MPath;

        //Externs
        string m_strDayLineExtern;
        string m_str5MLineExtern;

        public void PaseDataParameter()
        {
            m_enStatus = GShare.JobStatus.eUndefined;
        }
        public void Initial(string strPath)
        {
            if (strPath == null)
                return;

            m_strXmlPath = strPath;
            StreamReader sr = new StreamReader(m_strXmlPath);
            string strXml = sr.ReadToEnd();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXml);

            XmlNode node = xmlDoc.SelectSingleNode("/configuration/paths/tdxhome");
            XmlNamedNodeMap nnMap = node.Attributes;
            for (int i = 0; i < nnMap.Count; i++ )
            {
                string attrName = nnMap.Item(i).Name;
                string attrValue = nnMap.Item(i).Value;
                if (attrName == "path")
                {
                    m_strTDXHome = attrValue;
                }
            }

            XmlNodeList ndList = xmlDoc.SelectNodes("/configuration/paths/kline/n");
            for (int i = 0; i < ndList.Count; i++)
            {
                string strType = "";
                string strExtern = "";
                string strSHPath = "";
                string strSZPath = "";
                node = ndList.Item(i);
                nnMap = node.Attributes;
                for (int j = 0; j < nnMap.Count; j++)
                {
                    string attrName = nnMap.Item(j).Name;
                    string attrValue = nnMap.Item(j).Value;
                    if (attrName == "type")
                    {
                        strType = attrValue;
                    }
                    else if (attrName == "ext")
                    {
                        strExtern = attrValue;
                    }
                    else if (attrName == "sh")
                    {
                        strSHPath = attrValue;
                    }
                    else if (attrName == "sz")
                    {
                        strSZPath = attrValue;
                    }
                }

                if ((GShare.KLineDataType)int.Parse(strType) == GShare.KLineDataType.eDay)
                {
                    m_strDayLineExtern = strExtern;
                    m_strSHDayPath = strSHPath;
                    m_strSZDayPath = strSZPath;
                }
                else if ((GShare.KLineDataType)int.Parse(strType) == GShare.KLineDataType.e5Minutes)
                {
                    m_str5MLineExtern = strExtern;
                    m_strSH5MPath = strSHPath;
                    m_strSZ5MPath = strSZPath;
                }
            }
        }
        public string GetHomePath()
        {
            return m_strTDXHome;
        }
        public string GetDayKLineFileExtern()
        {
            return m_strDayLineExtern;
        }
        public string Get5MKLineFileExtern()
        {
            return m_str5MLineExtern;
        }
        public string GetSHDayPath()
        {
            return m_strSHDayPath;
        }
        public string GetSZDayPath()
        {
            return m_strSZDayPath;
        }
        public string GetSH5MPath()
        {
            return m_strSH5MPath;
        }
        public string GetSZ5MPath()
        {
            return m_strSZ5MPath;
        }
    }
}
