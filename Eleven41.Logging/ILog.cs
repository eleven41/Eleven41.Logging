using System;

namespace Eleven41.Logging
{
	/// <summary>
	/// Interface defining classes used for logging.
	/// </summary>
	public interface ILog
	{
		/// <summary>
		/// Sends a message to the log.
		/// </summary>
		/// <param name="level">Level of the message.</param>
		/// <param name="sFormat">Message format to send.</param>
		/// <param name="args">Message arguments.</param>
		void Log(LogLevels level, string sFormat, params Object[] args);
	}
}
