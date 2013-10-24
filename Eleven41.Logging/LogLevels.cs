using System;
using System.Collections.Generic;
using System.Text;

namespace Eleven41.Logging
{
	/// <summary>
	/// Defines the level of a log message.
	/// </summary>
	public enum LogLevels
	{
		/// <summary>
		/// The message is informational.
		/// </summary>
		Info,

		/// <summary>
		/// The message is diagnostic.
		/// </summary>
		Diagnostic,

		/// <summary>
		/// The message is a warning.
		/// </summary>
		Warning,

		/// <summary>
		/// The message is an error.
		/// </summary>
		Error
	};
}
