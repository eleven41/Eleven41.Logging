using System;
using System.Collections.Generic;
using System.Text;

namespace Eleven41.Logging
{
	public class BareTextWriterFormatter : ITextWriterFormatter
	{
		public void WriteText(System.IO.TextWriter writer, string level, DateTime dateTime, string text)
		{
			writer.WriteLine("{0}", text);
		}
	}
}
