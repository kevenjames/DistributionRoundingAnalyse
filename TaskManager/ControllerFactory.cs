using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTool;

namespace TaskManager
{
    public class ControllerFactory
    {
        public static IController Factory(string strReq)
        { 
            RequestParameter rp = new RequestParameter(strReq);
            if (rp.m_enRequestId == GShare.Request.eParseKLineData)
            {
                return ParseDataContorller.GetInistance(rp);
            }
            else
                throw new Exception("Controller is null");
        }
    }
}
