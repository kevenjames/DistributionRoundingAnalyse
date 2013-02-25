using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTool;

namespace ParseData
{
    public interface IDataParser
    {
        void Run();
        void PreHandle();
        void AfterParse();
        void ParseData();
    }
}
