using System;
using System.IO;

namespace Eleven41.Logging
{
	/// <summary>
	/// Logs messages to a file.
	/// </summary>
	public class FileLog : TextWriterLog
	{
		/// <summary>
		/// Constructs a FileLog object.
		/// </summary>
		/// <param name="sFile">File to log to.</param>
		public FileLog(string sFile)
			: base(null)
		{
			Init(sFile);
		}

		/// <summary>
		/// Constructs a FileLog object.
		/// </summary>
		/// <param name="sFile">File to log to.</param>
		/// <param name="args">Command line arguments.</param>
		public FileLog(string sFile, string[] args)
			: base (null, args)
		{
			Init(sFile);
		}

		/// <summary>
		/// Constructs a FileLog object.
		/// </summary>
		/// <param name="sFile">File to log to.</param>
		/// <param name="bLogInfos">Flag indicating whether to log info messages.</param>
		/// <param name="bLogDiags">Flag indicating whether to log diagnostic messages.</param>
		/// <param name="bLogWarnings">Flag indicating whether to log warning messages.</param>
		/// <param name="bLogErrors">Flag indicating whether to log error messages.</param>
		public FileLog(string sFile, bool bLogInfos, bool bLogDiags, bool bLogWarnings, bool bLogErrors)
			: base(null, bLogInfos, bLogDiags, bLogWarnings, bLogErrors)
		{
			Init(sFile);
		}

		/// <summary>
		/// Initializes the text writer.
		/// </summary>
		/// <param name="sFile">File to log to.</param>
		private void Init(string sFile)
		{
			FileStream stream = new FileStream(sFile, FileMode.Create, FileAccess.Write);
			Writer = new StreamWriter(stream);
		}
	}
}
