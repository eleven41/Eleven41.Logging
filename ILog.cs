using System;

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

	/// <summary>
	/// Interface defining classes used for logging.
	/// </summary>
	public interface ILog
	{
		/// <summary>
		/// Sends a message to the log.
		/// </summary>
		/// <param name="level">Level of the message.</param>
		/// <param name="sMsg">Message to send.</param>
		void Log(LogLevels level, string sMsg, params Object[] args);
	}
}
