



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="ThreadDetailsRow"/> that 
	/// represents a record in the <c>ThreadDetails</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="ThreadDetailsRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class ThreadDetailsRow_Base
	{
		private long _news_ID;
		private string _news_Title;
		private int _threaddetails_ID;
		private string _title;
		private int _thread_ID;

		/// <summary>
		/// Initializes a new instance of the <see cref="ThreadDetailsRow_Base"/> class.
		/// </summary>
		public ThreadDetailsRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>News_ID</c> column value.
		/// </summary>
		/// <value>The <c>News_ID</c> column value.</value>
		public long News_ID
		{
			get { return _news_ID; }
			set { _news_ID = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Title</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Title</c> column value.</value>
		public string News_Title
		{
			get { return _news_Title; }
			set { _news_Title = value; }
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
		/// Gets or sets the <c>Title</c> column value.
		/// </summary>
		/// <value>The <c>Title</c> column value.</value>
		public string Title
		{
			get { return _title; }
			set { _title = value; }
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
			dynStr.Append("  News_ID=");
			dynStr.Append(News_ID);
			dynStr.Append("  News_Title=");
			dynStr.Append(News_Title);
			dynStr.Append("  Threaddetails_ID=");
			dynStr.Append(Threaddetails_ID);
			dynStr.Append("  Title=");
			dynStr.Append(Title);
			dynStr.Append("  Thread_ID=");
			dynStr.Append(Thread_ID);
			return dynStr.ToString();
		}
	} // End of ThreadDetailsRow_Base class
} // End of namespace


