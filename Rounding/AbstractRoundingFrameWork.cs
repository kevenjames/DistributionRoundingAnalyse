﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTool;

namespace Rounding
{
    abstract class AbstractRoundingFrameWork
	{
		public AbstractRoundingFrameWork()
		{
			
		}

		public abstract GShare.ErrorCode InitialInstance();
		public abstract void Run();
		public abstract void Destroy();
	}
}
