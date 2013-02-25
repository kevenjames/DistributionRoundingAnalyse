using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace CommonTool
{
    public class CommonFunction
    {
        public static bool IsSvrExist(string strName)
        {
            ServiceController[] scs;
            scs = ServiceController.GetServices();

            foreach (ServiceController sct in scs)
            {
                if (sct.ServiceName == strName)
                    return true;
            }

            return false;
        }
        public static bool SvrOperate(string strName, bool bStart)
        {
            ServiceController sc = new ServiceController(strName);
            if (CommonFunction.IsSvrExist(strName) && sc.Status == ServiceControllerStatus.Stopped)
            {
                sc.Start();
                return true;
            }
            else
                return false;                
        }
    }
}
