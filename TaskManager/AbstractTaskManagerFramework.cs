using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTool;

namespace TaskManager
{
	abstract class AbstractTaskManagerFramework
	{
		public AbstractTaskManagerFramework()
		{
		}
		public abstract GShare.ErrorCode InitialInstance();
		public abstract void Run();
		public abstract void Destroy();
	}
}
