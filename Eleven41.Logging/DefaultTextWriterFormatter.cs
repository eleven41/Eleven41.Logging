using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Eleven41.Logging
{
	public class DefaultTextWriterFormatter : ITextWriterFormatter
	{
		public void WriteText(System.IO.TextWriter writer, string level, DateTime dateTime, string text)
		{
			writer.WriteLine("{0}:{1} {2} [{4}]: {3}",
				level, dateTime.ToShortDateString(), dateTime.ToLongTimeString(),
				text, Thread.CurrentThread.GetHashCode());
		}
	}
}
