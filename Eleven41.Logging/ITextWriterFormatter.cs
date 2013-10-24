using System;
using System.Collections.Generic;
using System.Text;

namespace Eleven41.Logging
{
	public interface ITextWriterFormatter
	{
		void WriteText(System.IO.TextWriter writer, string level, DateTime dateTime, string text);
	}
}
