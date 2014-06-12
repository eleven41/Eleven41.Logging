using System;
using System.Diagnostics;

namespace Eleven41.Logging
{
	/// <summary>
	/// Logs messages to the system's event log.
	/// </summary>
	public class EventLogLog : LogBase
	{
		/// <summary>
		/// Event log to write to.
		/// </summary>
		private EventLog _eventLog;

		/// <summary>
		/// Constructs an EventLogLog object.
		/// </summary>
		/// <param name="sSource">Source name for working with the event log.</param>
		public EventLogLog(string sSource)
		{
			Init(sSource);
		}

		/// <summary>
		/// Constructs an EventLogLog object.
		/// </summary>
		/// <param name="sSource">Source name for working with the event log.</param>
		/// <param name="args">Arguments for the log.</param>
		public EventLogLog(string sSource, string[] args)
			: base(args)
		{
			Init(sSource);
		}

		/// <summary>
		/// Initialize the object.
		/// </summary>
		/// <param name="sSource"></param>
		private void Init(string sSource)
		{
			// Make sure the source exists
			if (!EventLog.SourceExists(sSource))
			{
				EventLog.CreateEventSource(sSource, "Eleven41");
			}

			_eventLog = new EventLog();
			_eventLog.Source = sSource;
		}

		#region ILog Members

		/// <summary>
		/// Logs a message to the event log.
		/// </summary>
		/// <param name="level">Level of the message</param>
		/// <param name="sFormat">Message to log.</param>
		public override void Log(LogLevels level, string sFormat, params Object[] args)
		{
			string sFinalMsg = String.Format(sFormat, args);
			switch (level)
			{
				case LogLevels.Info:
				default:
					if (LogInfos)
						_eventLog.WriteEntry(sFinalMsg, EventLogEntryType.Information);
					break;
				case LogLevels.Diagnostic:
					if (LogDiagnostics)
						_eventLog.WriteEntry(sFinalMsg, EventLogEntryType.Information);
					break;
				case LogLevels.Warning:
					if (LogWarnings)
						_eventLog.WriteEntry(sFinalMsg, EventLogEntryType.Warning);
					break;
				case LogLevels.Error:
					if (LogErrors)
						_eventLog.WriteEntry(sFinalMsg, EventLogEntryType.Error);
					break;
			}
		}

		public override void Log(LogLevels level, System.Collections.Generic.Dictionary<string, object> data, string sFormat, params object[] args)
		{
			// We don't support data, so call the non-data version
			Log(level, sFormat, args);
		}

		public override void Log(DateTime date, LogLevels level, string sFormat, params object[] args)
		{
			// We don't support preset dates, so call the non-date version
			Log(level, sFormat, args);
		}

		public override void Log(DateTime date, LogLevels level, System.Collections.Generic.Dictionary<string, object> data, string sFormat, params object[] args)
		{
			// We don't support preset dates or data, so call the non-date, non-data version
			Log(level, sFormat, args);
		}

		#endregion
	}
}
