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
			this.DateTimeProvider = new DateTimeProviders.DefaultDateTimeProvider();
			this.TextWriterFormatter = new TextWriterFormatters.DefaultTextWriterFormatter();
		}

		/// <summary>
		/// Constructs a TextWriterLog object.
		/// </summary>
		public TextWriterLog(TextWriter writer)
		{
			_writer = writer;
			this.DateTimeProvider = new DateTimeProviders.DefaultDateTimeProvider();
			this.TextWriterFormatter = new TextWriterFormatters.DefaultTextWriterFormatter();
		}

		/// <summary>
		/// Constructs a TextWriterLog object.
		/// </summary>
		public TextWriterLog(TextWriter writer, string[] args)
			: base(args)
		{
			_writer = writer;
			this.DateTimeProvider = new DateTimeProviders.DefaultDateTimeProvider();
			this.TextWriterFormatter = new TextWriterFormatters.DefaultTextWriterFormatter();
		}

		/// <summary>
		/// Constructs a TextWriterLog object.
		/// </summary>
		public TextWriterLog(TextWriter writer, bool bLogInfos, bool bLogDiags, bool bLogWarnings, bool bLogErrors)
			: base(bLogInfos, bLogDiags, bLogWarnings, bLogErrors)
		{
			_writer = writer;
			this.DateTimeProvider = new DateTimeProviders.DefaultDateTimeProvider();
			this.TextWriterFormatter = new TextWriterFormatters.DefaultTextWriterFormatter();
		}

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
		/// <param name="sFormat">Message to log.</param>
		public override void Log(LogLevels level, string sFormat, params Object[] args)
		{
			Log(this.DateTimeProvider.GetCurrentDateTime(), level, String.Format(sFormat, args));
		}

		#endregion

		IDateTimeProvider _dateTimeProvider;

		public IDateTimeProvider DateTimeProvider 
		{
			get
			{
				return _dateTimeProvider;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException();
				_dateTimeProvider = value;
			}
		}

		private ITextWriterFormatter _textWriterFormatter;

		public ITextWriterFormatter TextWriterFormatter
		{
			get
			{
				return _textWriterFormatter;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException();
				_textWriterFormatter = value;
			}
		}
		
		
		/// <summary>
		/// Logs a message to the TextWriter.
		/// </summary>
		/// <param name="dt">Date/time of the event.</param>
		/// <param name="level">Level of the message.</param>
		/// <param name="sMsg">Message to log.</param>
		public void Log(DateTime dt, LogLevels level, string sMsg)
		{
			switch (level)
			{
				case LogLevels.Info:
				default:
					if (!LogInfos)
						return;
					break;

				case LogLevels.Diagnostic:
					if (!LogDiagnostics)
						return;
					break;

				case LogLevels.Warning:
					if (!LogWarnings)
						return;
					break;

				case LogLevels.Error:
					if (!LogErrors)
						return;
					break;
			}

			// Split the message into lines
			string[] lines = System.Text.RegularExpressions.Regex.Split(sMsg, "\r\n|\r|\n");
			foreach (var line in lines)
			{
				this.TextWriterFormatter.WriteText(_writer, level, dt, line);
			}
			_writer.Flush();
		}
	}
}
