using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eleven41.Logging
{
	public class NullLog : ILog
	{
		public void Log(LogLevels level, string sMsg, params object[] args)
		{
		}
	}
}
