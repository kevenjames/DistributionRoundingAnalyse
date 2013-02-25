using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTool;

namespace ParseData
{
    public class DataParserFactory
    {
        public static IDataParser FactoryMethod(RequestParameter reqParam)
        {
            if (reqParam.m_enKLineDataType == GShare.KLineDataType.eDay)
                return new KLineDayParser(reqParam);
            else
                throw new Exception("Parse data null");
        }
    }
}
