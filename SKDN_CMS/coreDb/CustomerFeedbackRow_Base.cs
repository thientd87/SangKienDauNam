



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="CustomerFeedbackRow"/> that 
	/// represents a record in the <c>CustomerFeedback</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CustomerFeedbackRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CustomerFeedbackRow_Base
	{
		private int _idea_ID ;
		private int _news_ID  = 0;
		private bool _news_IDNull = false;
		private string _reader_Name ;
		private string _reader_Addr ;
		private string _reader_Mail ;
		private string _idea_Title ;
		private string _idea_Content ;
		private DateTime _idea_Date ;
		private bool _idea_DateNull = true;
		private bool _approved ;
		private bool _approvedNull = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerFeedbackRow_Base"/> class.
		/// </summary>
		public CustomerFeedbackRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>Idea_ID</c> column value.
		/// </summary>
		/// <value>The <c>Idea_ID</c> column value.</value>
		public int Idea_ID
		{
			get { return _idea_ID; }
			set { _idea_ID = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_ID</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_ID</c> column value.</value>
		public int News_ID
		{
			get
			{
				if(IsNews_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _news_ID;
			}
			set
			{
				_news_IDNull = false;
				_news_ID = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="News_ID"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsNews_IDNull
		{
			get { return _news_IDNull; }
			set { _news_IDNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Reader_Name</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Reader_Name</c> column value.</value>
		public string Reader_Name
		{
			get { return _reader_Name; }
			set { _reader_Name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Reader_Addr</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Reader_Addr</c> column value.</value>
		public string Reader_Addr
		{
			get { return _reader_Addr; }
			set { _reader_Addr = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Reader_Mail</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Reader_Mail</c> column value.</value>
		public string Reader_Mail
		{
			get { return _reader_Mail; }
			set { _reader_Mail = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Idea_Title</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Idea_Title</c> column value.</value>
		public string Idea_Title
		{
			get { return _idea_Title; }
			set { _idea_Title = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Idea_Content</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Idea_Content</c> column value.</value>
		public string Idea_Content
		{
			get { return _idea_Content; }
			set { _idea_Content = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Idea_Date</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Idea_Date</c> column value.</value>
		public DateTime Idea_Date
		{
			get
			{
				if(IsIdea_DateNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _idea_Date;
			}
			set
			{
				_idea_DateNull = false;
				_idea_Date = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Idea_Date"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsIdea_DateNull
		{
			get { return _idea_DateNull; }
			set { _idea_DateNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Approved</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Approved</c> column value.</value>
		public bool Approved
		{
			get
			{
				if(IsApprovedNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _approved;
			}
			set
			{
				_approvedNull = false;
				_approved = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Approved"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsApprovedNull
		{
			get { return _approvedNull; }
			set { _approvedNull = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Idea_ID=");
			dynStr.Append(Idea_ID);
			dynStr.Append("  News_ID=");
			dynStr.Append(IsNews_IDNull ? (object)"<NULL>" : News_ID);
			dynStr.Append("  Reader_Name=");
			dynStr.Append(Reader_Name);
			dynStr.Append("  Reader_Addr=");
			dynStr.Append(Reader_Addr);
			dynStr.Append("  Reader_Mail=");
			dynStr.Append(Reader_Mail);
			dynStr.Append("  Idea_Title=");
			dynStr.Append(Idea_Title);
			dynStr.Append("  Idea_Content=");
			dynStr.Append(Idea_Content);
			dynStr.Append("  Idea_Date=");
			dynStr.Append(IsIdea_DateNull ? (object)"<NULL>" : Idea_Date);
			dynStr.Append("  Approved=");
			dynStr.Append(IsApprovedNull ? (object)"<NULL>" : Approved);
			return dynStr.ToString();
		}
	} // End of CustomerFeedbackRow_Base class
} // End of namespace


