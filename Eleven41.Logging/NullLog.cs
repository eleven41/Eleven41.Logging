using System;
using System.Collections.Generic;
using System.Text;

namespace Eleven41.Logging
{
	public class NullLog : ILog
	{
		public void Log(LogLevels level, string sFormat, params object[] args)
		{
		}

		public void Log(LogLevels level, Dictionary<string, object> data, string sFormat, params object[] args)
		{
		}
	}
}
