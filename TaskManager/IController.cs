using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTool;

namespace TaskManager
{
    public interface IController
    {
        //Must be worked in singleton Mode
        
        //Operate
        void Run();
    }
}
