using System;
using System.Collections.Generic;

namespace Eleven41.Logging
{
	/// <summary>
	/// Logs messages to multiple targets
	/// </summary>
	public class MultiLog : ILog
	{
		/// <summary>
		/// List of logs to output to.
		/// </summary>
		private List<ILog> _logs;

		/// <summary>
		/// Constucts a MultiLog object.
		/// </summary>
		public MultiLog(params ILog[] args)
		{
			_logs = new List<ILog>();

			foreach (var arg in args)
				_logs.Add(arg);
		}

		/// <summary>
		/// Add an ILog object to the list of
		/// log destinations.
		/// </summary>
		/// <param name="log">Object to add.</param>
		public void Add(ILog log)
		{
			_logs.Add(log);
		}

		#region ILog Members

		/// <summary>
		/// Log the message to each of the logs in the list.
		/// </summary>
		/// <param name="level">Level of the message.</param>
		/// <param name="sFormat">Message to send.</param>
		public virtual void Log(LogLevels level, string sFormat, params Object[] args)
		{
			// Send the message to each log
			foreach(ILog log in _logs)
			{
				log.Log(level, sFormat, args);
			}
		}

		public void Log(DateTime date, LogLevels level, string sFormat, params object[] args)
		{
			// Send the message to each log
			foreach (ILog log in _logs)
			{
				log.Log(date, level, sFormat, args);
			}
		}

		public void Log(LogLevels level, Dictionary<string, object> data, string sFormat, params object[] args)
		{
			// Send the message to each log
			foreach (ILog log in _logs)
			{
				log.Log(level, data, sFormat, args);
			}
		}

		public void Log(DateTime date, LogLevels level, Dictionary<string, object> data, string sFormat, params object[] args)
		{
			// Send the message to each log
			foreach (ILog log in _logs)
			{
				log.Log(date, level, data, sFormat, args);
			}
		}

		#endregion

	}
}
