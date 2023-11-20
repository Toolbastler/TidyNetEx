using System;
using System.Collections;
using System.Collections.Generic;

namespace TidyNet
{
	/// <summary>
	/// Collection of TidyMessages
	/// </summary>
	[Serializable]
	public class TidyMessageCollection : List<TidyMessage>//CollectionBase
	{
		/// <summary>
		/// Public default constructor
		/// </summary>
		public TidyMessageCollection()
		{
		}

		/// <summary>
		/// Adds a message.
		/// </summary>
		/// <param name="message">The message to add.</param>
		public new void Add(TidyMessage message)
		{
			if (message.Level == MessageLevel.Error)
			{
				_errors++;
			}
			else if (message.Level == MessageLevel.Warning)
			{
				_warnings++;
			}

			base.Add(message);
		}

		/// <summary> Errors - the number of errors that occurred in the most
		/// recent parse operation
		/// </summary>
		public virtual int Errors => _errors;

        /// <summary> Warnings - the number of warnings that occurred in the most
		/// recent parse operation
		/// </summary>
		public virtual int Warnings => _warnings;

        int _errors = 0;
		int _warnings = 0;
	}
}
