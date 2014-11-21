



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="ThreadDetailRow"/> that 
	/// represents a record in the <c>ThreadDetail</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="ThreadDetailRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class ThreadDetailRow_Base
	{
		private int _threaddetails_ID ;
		private string _news_ID ;
		private int _thread_ID ;

		/// <summary>
		/// Initializes a new instance of the <see cref="ThreadDetailRow_Base"/> class.
		/// </summary>
		public ThreadDetailRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>Threaddetails_ID</c> column value.
		/// </summary>
		/// <value>The <c>Threaddetails_ID</c> column value.</value>
		public int Threaddetails_ID
		{
			get { return _threaddetails_ID; }
			set { _threaddetails_ID = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_ID</c> column value.
		/// </summary>
		/// <value>The <c>News_ID</c> column value.</value>
		public string News_ID
		{
			get { return _news_ID; }
			set { _news_ID = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Thread_ID</c> column value.
		/// </summary>
		/// <value>The <c>Thread_ID</c> column value.</value>
		public int Thread_ID
		{
			get { return _thread_ID; }
			set { _thread_ID = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Threaddetails_ID=");
			dynStr.Append(Threaddetails_ID);
			dynStr.Append("  News_ID=");
			dynStr.Append(News_ID);
			dynStr.Append("  Thread_ID=");
			dynStr.Append(Thread_ID);
			return dynStr.ToString();
		}
	} // End of ThreadDetailRow_Base class
} // End of namespace


