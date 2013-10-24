using System;
using System.Collections.Generic;
using System.Text;

namespace Eleven41.Logging.DateTimeProviders
{
	public class UtcDateTimeFormatter : IDateTimeProvider
	{
		public DateTime GetCurrentDateTime()
		{
			return DateTime.UtcNow;
		}
	}
}
