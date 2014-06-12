using System;
using System.Collections.Generic;

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

		/// <summary>
		/// Sends a message to the log.
		/// </summary>
		/// <param name="date">Date to timestamp the message.</param>
		/// <param name="level">Level of the message.</param>
		/// <param name="sFormat">Message format to send.</param>
		/// <param name="args">Message arguments.</param>
		void Log(DateTime date, LogLevels level, string sFormat, params Object[] args);

		/// <summary>
		/// Sends a message to the log, including extra data (if supported).
		/// </summary>
		/// <param name="level">Level of the message.</param>
		/// <param name="data">Data to include with the message.</param>
		/// <param name="sFormat">Message format to send.</param>
		/// <param name="args">Message arguments.</param>
		void Log(LogLevels level, Dictionary<string, object> data, string sFormat, params Object[] args);

		/// <summary>
		/// Sends a message to the log, including extra data (if supported).
		/// </summary>
		/// <param name="date">Date to timestamp the message.</param>
		/// <param name="level">Level of the message.</param>
		/// <param name="data">Data to include with the message.</param>
		/// <param name="sFormat">Message format to send.</param>
		/// <param name="args">Message arguments.</param>
		void Log(DateTime date, LogLevels level, Dictionary<string, object> data, string sFormat, params Object[] args);
	}
}
