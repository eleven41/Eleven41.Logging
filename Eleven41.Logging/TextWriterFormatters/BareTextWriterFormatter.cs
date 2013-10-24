using System;
using System.Collections.Generic;
using System.Text;

namespace Eleven41.Logging.TextWriterFormatters
{
	public class BareTextWriterFormatter : ITextWriterFormatter
	{
		public void WriteText(System.IO.TextWriter writer, LogLevels level, DateTime dateTime, string text)
		{
			writer.WriteLine("{0}", text);
		}
	}
}
