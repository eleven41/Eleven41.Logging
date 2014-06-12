using System;
using System.Collections.Generic;
using System.Text;

namespace Eleven41.Logging
{
	/// <summary>
	/// Wraps another ILog object and passes the supplied data
	/// with every message.
	/// </summary>
	public class DataLog : ILog
	{
		ILog _log;
		
		public DataLog(ILog log, Dictionary<string, object> data)
		{
			_log = log;
			
			if (data != null)
				_data = new Dictionary<string, object>(data);
			else
				_data = new Dictionary<string, object>();
		}

		private Dictionary<string, object> _data;

		public Dictionary<string, object> Data
		{
			get { return _data; }
			set 
			{
				if (value == null)
					throw new ArgumentNullException("Data", "Data must not be null");
				_data = value; 
			}
		}

		public void Log(LogLevels level, string sFormat, params object[] args)
		{
			// Pass the message on including our extra data
			_log.Log(level, _data, sFormat, args);
		}

		public void Log(DateTime date, LogLevels level, string sFormat, params object[] args)
		{
			// Pass the message on including our extra data
			_log.Log(date, level, _data, sFormat, args);
		}

		public void Log(LogLevels level, Dictionary<string, object> messageData, string sFormat, params object[] args)
		{
			// Start with our seed data
			Dictionary<string, object> data = new Dictionary<string, object>(_data);

			// Add the message data
			if (messageData != null)
			{
				foreach (var kvp in messageData)
					data[kvp.Key] = kvp.Value;
			}

			_log.Log(level, data, sFormat, args);
		}

		public void Log(DateTime date, LogLevels level, Dictionary<string, object> messageData, string sFormat, params object[] args)
		{
			// Start with our seed data
			Dictionary<string, object> data = new Dictionary<string, object>(_data);

			// Add the message data
			if (messageData != null)
			{
				foreach (var kvp in messageData)
					data[kvp.Key] = kvp.Value;
			}

			_log.Log(date, level, data, sFormat, args);
		}
	}
}
