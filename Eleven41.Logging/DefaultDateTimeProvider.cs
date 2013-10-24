using System;
using System.Collections.Generic;
using System.Text;

namespace Eleven41.Logging
{
	public class DefaultDateTimeProvider : IDateTimeProvider
	{
		public DateTime GetCurrentDateTime()
		{
			return DateTime.Now;
		}
	}
}
