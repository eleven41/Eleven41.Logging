using System;
using System.IO;
using System.Threading;

namespace Eleven41.Logging
{
	/// <summary>
	/// Logs messages to a TextWriter object.
	/// </summary>
	public class TextWriterLog : LogBase
	{
		private TextWriter _writer = null;

		/// <summary>
		/// Constructs a TextWriterLog object.
		/// </summary>
		public TextWriterLog()
		{
			_writer = null;
		}

		/// <summary>
		/// Constructs a TextWriterLog object.
		/// </summary>
		public TextWriterLog(TextWriter writer)
		{
			_writer = writer;
		}

		/// <summary>
		/// Constructs a TextWriterLog object.
		/// </summary>
		public TextWriterLog(TextWriter writer, string[] args)
			: base(args)
		{
			_writer = writer;
		}

		/// <summary>
		/// Constructs a TextWriterLog object.
		/// </summary>
		public TextWriterLog(TextWriter writer, bool bLogInfos, bool bLogDiags, bool bLogWarnings, bool bLogErrors)
			: base(bLogInfos, bLogDiags, bLogWarnings, bLogErrors)
		{
			_writer = writer;
		}

		public bool IsBare { get; set; }

		/// <summary>
		/// The writer for the object.
		/// </summary>
		protected TextWriter Writer
		{
			get
			{
				return _writer;
			}

			set
			{
				_writer = value;
			}
		}

		#region ILog Members

		/// <summary>
		/// Logs a message to the TextWriter.
		/// </summary>
		/// <param name="level">Level of the message.</param>
		/// <param name="sMsg">Message to log.</param>
		public override void Log(LogLevels level, string sMsg, params Object[] args)
		{
			Log(GetDateTime(), level, String.Format(sMsg, args));
		}

		#endregion

		protected virtual DateTime GetDateTime()
		{
			return DateTime.Now;
		}

		/// <summary>
		/// Logs a message to the TextWriter.
		/// </summary>
		/// <param name="dt">Date/time of the event.</param>
		/// <param name="level">Level of the message.</param>
		/// <param name="sMsg">Message to log.</param>
		public void Log(DateTime dt, LogLevels level, string sMsg)
		{
			string sLevel;
			switch (level)
			{
				case LogLevels.Info:
				default:
					if (!LogInfos)
						return;
					sLevel = "I";
					break;

				case LogLevels.Diagnostic:
					if (!LogDiagnostics)
						return;
					sLevel = "D";
					break;

				case LogLevels.Warning:
					if (!LogWarnings)
						return;
					sLevel = "W";
					break;

				case LogLevels.Error:
					if (!LogErrors)
						return;
					sLevel = "E";
					break;
			}

			// Split the message into lines
			string[] lines = System.Text.RegularExpressions.Regex.Split(sMsg, "\r\n|\r|\n");
			foreach (var line in lines)
			{
				if (!IsBare)
				{
					_writer.WriteLine("{0}:{1} {2} [{4}]: {3}", sLevel,
						dt.ToShortDateString(), dt.ToLongTimeString(),
						line, Thread.CurrentThread.GetHashCode());
				}
				else
				{
					_writer.WriteLine("{0}", line);
				}
			}
			_writer.Flush();
		}
	}
}
