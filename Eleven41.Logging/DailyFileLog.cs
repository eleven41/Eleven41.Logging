using System;
using System.IO;

namespace Eleven41.Logging
{
	/// <summary>
	/// 
	/// </summary>
	public class DailyFileLog : TextWriterLog
	{
		int _lastDayOfYear;
		string _sFileSpec = null;
		Object _writerLock = new Object();

		/// <summary>
		/// Constructs a DailyFileLog object.
		/// </summary>
		/// <param name="sFileSpec">File-spec to log to.</param>
		public DailyFileLog(string sFileSpec)
			: base(null)
		{
			_sFileSpec = sFileSpec;
			CreateFile();
		}

		/// <summary>
		/// Constructs a DailyFileLog object.
		/// </summary>
		/// <param name="sFileSpec">File-spec to log to.</param>
		/// <param name="args">Command line arguments.</param>
		public DailyFileLog(string sFileSpec, string[] args)
			: base (null, args)
		{
			_sFileSpec = sFileSpec;
			CreateFile();
		}

		/// <summary>
		/// Constructs a DailyFileLog object.
		/// </summary>
		/// <param name="sFileSpec">File-spec to log to.</param>
		/// <param name="bLogInfos">Flag indicating whether to log info messages.</param>
		/// <param name="bLogDiags">Flag indicating whether to log diagnostic messages.</param>
		/// <param name="bLogWarnings">Flag indicating whether to log warning messages.</param>
		/// <param name="bLogErrors">Flag indicating whether to log error messages.</param>
		public DailyFileLog(string sFileSpec, bool bLogInfos, bool bLogDiags, bool bLogWarnings, bool bLogErrors)
			: base(null, bLogInfos, bLogDiags, bLogWarnings, bLogErrors)
		{
			_sFileSpec = sFileSpec;
			CreateFile();
		}

		/// <summary>
		/// Creates a new file.
		/// </summary>
		private void CreateFile()
		{
			DateTime now = DateTime.Now;
			
			string sDate = now.ToString("yyyy-MM-dd");
			string sFile = _sFileSpec.Replace("${DATE}", sDate);
			Writer = new StreamWriter(sFile, true);
			_lastDayOfYear = now.DayOfYear;
		}

		#region ILog Members

		/// <summary>
		/// Logs a message.
		/// </summary>
		/// <param name="level">Level of the message.</param>
		/// <param name="sFormat">Message to log.</param>
		public override void Log(LogLevels level, string sFormat, params Object[] args)
		{
			lock (_writerLock)
			{
				// If it's a different day from the last one, then
				// create a new file.
				if (DateTime.Now.DayOfYear != _lastDayOfYear)
				{
					// Close the old file
					if (Writer != null)
					{
						Writer.Flush();
						Writer.Close();
					}

					// Re-create the file
					CreateFile();
				}
			}

			// Write to the log
			base.Log(level, sFormat, args);
		}

		#endregion
	}
}
