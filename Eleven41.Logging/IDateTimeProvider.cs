using System;
using System.Collections.Generic;
using System.Text;

namespace Eleven41.Logging
{
	public interface IDateTimeProvider
	{
		DateTime GetCurrentDateTime();
	}
}
