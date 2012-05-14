using System;

namespace Eleven41.Logging
{
	/// <summary>
	/// Logs messages to the console.
	/// </summary>
	public class ConsoleLog : TextWriterLog
	{
		/// <summary>
		/// Constructs a ConsoleLog object.
		/// </summary>
		public ConsoleLog()
			: base(Console.Out)
		{
		}

		/// <summary>
		/// Constructs a ConsoleLog object.
		/// </summary>
		public ConsoleLog(string[] args)
			: base(Console.Out, args)
		{
		}

		/// <summary>
		/// Constructs a ConsoleLog object.
		/// </summary>
		public ConsoleLog(bool bLogInfos, bool bLogDiags, bool bLogWarnings, bool bLogErrors)
			: base(Console.Out, bLogInfos, bLogDiags, bLogWarnings, bLogErrors)
		{
		}
	}
}
