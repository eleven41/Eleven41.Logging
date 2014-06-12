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
		private Thread _thread;

		/// <summary>
		/// Constructs a ThreadedLog object.
		/// </summary>
		/// <param name="log"></param>
		public ThreadedLog(ILog log)
		{
			_log = log;

			// Start the thread
			_thread = new Thread(new ThreadStart(Run));
			_thread.Start();
		}

		#region ILog Members

		/// <summary>
		/// Logs a message.
		/// </summary>
		/// <param name="level">Log level of the message.</param>
		/// <param name="sFormat">Format of the message to log.</param>
		/// <param name="args">Optional format arguments.</param>
		public void Log(LogLevels level, string sFormat, params Object[] args)
		{
			LogRecord record = new LogRecord(null, level, null, sFormat, args);
			lock (this)
			{
				_records.Enqueue(record);
			}
		}

		public void Log(LogLevels level, Dictionary<string, object> data, string sFormat, params object[] args)
		{
			LogRecord record = new LogRecord(null, level, data, sFormat, args);
			lock (this)
			{
				_records.Enqueue(record);
			}
		}

		public void Log(DateTime date, LogLevels level, string sFormat, params object[] args)
		{
			LogRecord record = new LogRecord(date, level, null, sFormat, args);
			lock (this)
			{
				_records.Enqueue(record);
			}
		}

		public void Log(DateTime date, LogLevels level, Dictionary<string, object> data, string sFormat, params object[] args)
		{
			LogRecord record = new LogRecord(date, level, data, sFormat, args);
			lock (this)
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
			private DateTime? _date;
			private LogLevels _level;
			private string _messageFormat;
			private Object[] _args;
			private Dictionary<string, object> _data;

			/// <summary>
			/// Constructs a LogRecord object.
			/// </summary>
			/// <param name="level">Log level of the message.</param>
			/// <param name="messageFormat">Message to be logged.</param>
			public LogRecord(DateTime? date, LogLevels level, Dictionary<string, object> data, string messageFormat, Object[] args)
			{
				_date = date;
				_level = level;
				_data = data;
				_messageFormat = messageFormat;
				_args = args;
			}

			/// <summary>
			/// Log the message to the supplied event log.
			/// </summary>
			/// <param name="log"></param>
			public void Log(ILog log)
			{
				// Log using our information
				if (_date.HasValue)
					log.Log(_date.Value, _level, _data, _messageFormat, _args);
				else
					log.Log(_level, _data, _messageFormat, _args);
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

		/// <summary>
		/// Stops the thread and waits for it to complete.
		/// </summary>
		public void StopAndWait()
		{
			_evStop.Set();
			_thread.Join();
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
