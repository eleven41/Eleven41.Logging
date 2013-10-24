using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Eleven41.Logging.TextWriterFormatters
{
	public class DefaultTextWriterFormatter : ITextWriterFormatter
	{
		public void WriteText(System.IO.TextWriter writer, LogLevels level, DateTime dateTime, string text)
		{
			string sLevel;
			switch (level)
			{
				case LogLevels.Info:
				default:
					sLevel = "I";
					break;

				case LogLevels.Diagnostic:
					sLevel = "D";
					break;

				case LogLevels.Warning:
					sLevel = "W";
					break;

				case LogLevels.Error:
					sLevel = "E";
					break;
			}

			writer.WriteLine("{0}:{1} {2} [{4}]: {3}",
				sLevel, dateTime.ToShortDateString(), dateTime.ToLongTimeString(),
				text, Thread.CurrentThread.GetHashCode());
		}
	}
}
