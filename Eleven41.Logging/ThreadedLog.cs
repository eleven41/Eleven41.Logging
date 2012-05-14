using System;
using System.Collections.Generic;
using System.Threading;

namespace Eleven41.Logging
{
	/// <summary>
	/// Logs message in a secondary thread.
	/// </summary>
	public class ThreadedLog : ILog
	{
		private ILog _log = null;
		private Queue<LogRecord> _records = new Queue<LogRecord>();

		/// <summary>
		/// Constructs a ThreadedLog object.
		/// </summary>
		/// <param name="log"></param>
		public ThreadedLog(ILog log)
		{
			_log = log;

			// Start the thread
			Thread t = new Thread(new ThreadStart(Run));
			t.Start();
		}

		#region ILog Members

		/// <summary>
		/// Logs a message.
		/// </summary>
		/// <param name="level">Log level of the message.</param>
		/// <param name="sMsg">Message to log.</param>
		public void Log(LogLevels level, string sMsg, params Object[] args)
		{
			LogRecord record = new LogRecord(level, sMsg, args);
			lock(this)
			{
				_records.Enqueue(record);
			}
		}

		#endregion

		/// <summary>
		/// Record of each message to be logged.
		/// </summary>
		private class LogRecord
		{
			private static TimeSpan _OldSpan = new TimeSpan(0, 5, 0);
			private DateTime _DateTime = DateTime.Now;
			private LogLevels _Level;
			private string _sMsg;
			private Object[] _args;

			/// <summary>
			/// Constructs a LogRecord object.
			/// </summary>
			/// <param name="level">Log level of the message.</param>
			/// <param name="sMsg">Message to be logged.</param>
			public LogRecord(LogLevels level, string sMsg, Object[] args)
			{
				_Level = level;
				_sMsg = sMsg;
				_args = args;
			}

			/// <summary>
			/// Log the message to the supplied event log.
			/// </summary>
			/// <param name="log"></param>
			public void Log(ILog log)
			{
				// If the item is too old, then forget about
				// trying to log it.
				if (DateTime.Now.Subtract(_DateTime) > _OldSpan)
					return;

				// Log using our information
				log.Log(_Level, _sMsg, _args);
			}
		}

		private ManualResetEvent _evStop = new ManualResetEvent(false);

		/// <summary>
		/// Stops the thread.
		/// </summary>
		public void Stop()
		{
			_evStop.Set();
		}

		private void Run()
		{
			while (true)
			{
				if (_evStop.WaitOne(5, false))
					break;

				// Process the logs
				bool bContinue = true;
				while (bContinue)
				{
					bContinue = ProcessLogs();
				}
			}
		}

		private bool ProcessLogs()
		{
			LogRecord record = null;

			// Get a record from the queue of log entries
			lock(this)
			{
				if (_records.Count == 0)
					return false;
				record = (LogRecord)_records.Dequeue();
			}

			// Log the record
			record.Log(_log);
			return true;
		}
	}
}
