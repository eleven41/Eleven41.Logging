using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eleven41.Logging;

namespace UtcSample
{
	class Program
	{
		static void Main(string[] args)
		{
			MyProcess(new ConsoleLog()
				{
					DateTimeProvider = new Eleven41.Logging.DateTimeProviders.UtcDateTimeFormatter()
				});
		}

		static void MyProcess(ILog log)
		{
			log.Log(LogLevels.Diagnostic, "This is a diagnostic message");
			log.Log(LogLevels.Info, "This is an information message");
			log.Log(LogLevels.Warning, "This is a warning message");
			log.Log(LogLevels.Error, "This is an error message: {0}", 1234);
		}
	}
}
