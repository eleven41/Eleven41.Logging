using System;

namespace Eleven41.Logging
{
	/// <summary>
	/// Base class for many log types.
	/// </summary>
	public abstract class LogBase : ILog
	{
		private bool _bLogInfos = false;
		private bool _bLogDiags = false;
		private bool _bLogWarnings = false;
		private bool _bLogErrors = false;

		/// <summary>
		/// Constructs a LogBase object.
		/// </summary>
		protected LogBase()
		{
			// Turn everything on
			_bLogInfos = true;
			_bLogDiags = true;
			_bLogWarnings = true;
			_bLogErrors = true;
		}

		/// <summary>
		/// Constructs a LogBase object.
		/// </summary>
		/// <param name="args">Arguments for enabling/disabling logging.</param>
		protected LogBase(string[] args)
		{
			// Default on
			_bLogInfos = true;
			_bLogDiags = false;
			_bLogWarnings = true;
			_bLogErrors = true;

			for (int i = 0; i < args.Length; i++)
			{
				string arg = args[i];
				if (arg.StartsWith("-L"))
				{
					// Turn off, so that the next loop will turn things on
					_bLogInfos = false;
					_bLogDiags = false;
					_bLogWarnings = false;
					_bLogErrors = false;

					for (int j = 2; j < arg.Length; j++)
					{
						if (arg[j] == 'i')
							_bLogInfos = true;
						else if (arg[j] == 'd')
							_bLogDiags = true;
						else if (arg[j] == 'w')
							_bLogWarnings = true;
						else if (arg[j] == 'e')
							_bLogErrors = true;
					}
				}
			}
		}

		/// <summary>
		/// Constructs a LogBase object.
		/// </summary>
		/// <param name="bLogInfos">Enable info logging.</param>
		/// <param name="bLogDiags">Flag indicating whether to log diagnostic messages.</param>
		/// <param name="bLogWarnings">Enable warning logging.</param>
		/// <param name="bLogErrors">Enable error logging.</param>
		protected LogBase(bool bLogInfos, bool bLogDiags, bool bLogWarnings, bool bLogErrors)
		{
			_bLogInfos = bLogInfos;
			_bLogDiags = bLogDiags;
			_bLogWarnings = bLogWarnings;
			_bLogErrors = bLogErrors;
		}

		/// <summary>
		/// Flag indicating to log info messages.
		/// </summary>
		public bool LogInfos
		{
			get { return _bLogInfos; }
			set { _bLogInfos = value; }
		}

		/// <summary>
		/// Flag indicating to log diagnostic messages.
		/// </summary>
		public bool LogDiagnostics
		{
			get { return _bLogDiags; }
			set { _bLogDiags = value; }
		}

		/// <summary>
		/// Flag indicating to log warning messages.
		/// </summary>
		public bool LogWarnings
		{
			get { return _bLogWarnings; }
			set { _bLogWarnings = value; }
		}

		/// <summary>
		/// Flag indicating to log error messages.
		/// </summary>
		public bool LogErrors
		{
			get { return _bLogErrors; }
			set { _bLogErrors = value; }
		}

		#region ILog Members

		/// <summary>
		/// Log a message.
		/// </summary>
		/// <param name="level">Level of the message.</param>
		/// <param name="sMsg">Message to log.</param>
		public abstract void Log(LogLevels level, string sFormat, params Object[] args);

		public virtual void Log(LogLevels level, System.Collections.Generic.Dictionary<string, object> data, string sFormat, params object[] args)
		{
			// These log types do not support extra data,
			// so simply call the basic log function
			Log(level, sFormat, args);
		}
		
		#endregion

	}
}
