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
			// Call the data version with null data
			Log(level, sFormat, null, args);
		}

		public void Log(LogLevels level, Dictionary<string, object> data, string sFormat, params object[] args)
		{
			LogRecord record = new LogRecord(level, data, sFormat, args);
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
			private static TimeSpan _OldSpan = new TimeSpan(0, 5, 0);
			private DateTime _DateTime = DateTime.Now;
			private LogLevels _Level;
			private string _sMsg;
			private Object[] _args;
			private Dictionary<string, object> _data;

			/// <summary>
			/// Constructs a LogRecord object.
			/// </summary>
			/// <param name="level">Log level of the message.</param>
			/// <param name="sMsg">Message to be logged.</param>
			public LogRecord(LogLevels level, Dictionary<string, object> data, string sMsg, Object[] args)
			{
				_Level = level;
				_data = data;
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
				log.Log(_Level, _data, _sMsg, _args);
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
