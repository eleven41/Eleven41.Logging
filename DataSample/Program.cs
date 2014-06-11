using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eleven41.Logging;

namespace DataSample
{
	class Program
	{
		static void Main(string[] args)
		{
			ILog consoleLog = new DataConsoleLog();
			ILog dataLog = new DataLog(consoleLog, new Dictionary<string, object>()
				{
					{ "param1", "value1" },
					{ "param2", "value2" }
				});
			MyProcess(dataLog);

			Console.WriteLine("Press Enter to continue...");
			Console.ReadLine();
		}

		static void MyProcess(ILog log)
		{
			log.Log(LogLevels.Diagnostic, "This is a diagnostic message");
			log.Log(LogLevels.Info, "This is an information message");
			log.Log(LogLevels.Warning, new Dictionary<string, object>()
				{
					{ "param2", "override2" }
				}, "This is a warning message");
			log.Log(LogLevels.Error, "This is an error message: {0}", 1234);
		}
	}

	// Create a custom version of the ConsoleLog to demonstrate sending
	// data with the message.
	// This implementation is for illustration purposes and should not be used in production.
	class DataConsoleLog : ConsoleLog
	{
		public override void Log(LogLevels level, Dictionary<string, object> data, string sFormat, params object[] args)
		{
			if (data != null)
			{
				List<string> keyValues = new List<string>();
				foreach (var kvp in data)
					keyValues.Add(String.Format("{0}={1}", kvp.Key, kvp.Value));

				sFormat = sFormat + " (" + String.Join(",", keyValues) + ")";
			}

			Log(level, sFormat, args);
		}
	}
}
