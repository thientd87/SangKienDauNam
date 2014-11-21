



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="ActionRow"/> that 
	/// represents a record in the <c>Action</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="ActionRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class ActionRow_Base
	{
		private int _comment_ID ;
		private long _news_ID ;
		private string _sender_ID ;
		private string _comment_Title ;
		private DateTime _createDate ;
		private bool _createDateNull = true;
		private string _content ;
		private int _actionType ;
		private bool _actionTypeNull = true;
		private string _reciver_ID ;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionRow_Base"/> class.
		/// </summary>
		public ActionRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>Comment_ID</c> column value.
		/// </summary>
		/// <value>The <c>Comment_ID</c> column value.</value>
		public int Comment_ID
		{
			get { return _comment_ID; }
			set { _comment_ID = value; }
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
		/// Gets or sets the <c>Sender_ID</c> column value.
		/// </summary>
		/// <value>The <c>Sender_ID</c> column value.</value>
		public string Sender_ID
		{
			get { return _sender_ID; }
			set { _sender_ID = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Comment_Title</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Comment_Title</c> column value.</value>
		public string Comment_Title
		{
			get { return _comment_Title; }
			set { _comment_Title = value; }
		}

		/// <summary>
		/// Gets or sets the <c>CreateDate</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>CreateDate</c> column value.</value>
		public DateTime CreateDate
		{
			get
			{
				if(IsCreateDateNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _createDate;
			}
			set
			{
				_createDateNull = false;
				_createDate = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="CreateDate"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsCreateDateNull
		{
			get { return _createDateNull; }
			set { _createDateNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Content</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Content</c> column value.</value>
		public string Content
		{
			get { return _content; }
			set { _content = value; }
		}

		/// <summary>
		/// Gets or sets the <c>ActionType</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>ActionType</c> column value.</value>
		public int ActionType
		{
			get
			{
				if(IsActionTypeNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _actionType;
			}
			set
			{
				_actionTypeNull = false;
				_actionType = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="ActionType"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsActionTypeNull
		{
			get { return _actionTypeNull; }
			set { _actionTypeNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Reciver_ID</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Reciver_ID</c> column value.</value>
		public string Reciver_ID
		{
			get { return _reciver_ID; }
			set { _reciver_ID = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Comment_ID=");
			dynStr.Append(Comment_ID);
			dynStr.Append("  News_ID=");
			dynStr.Append(News_ID);
			dynStr.Append("  Sender_ID=");
			dynStr.Append(Sender_ID);
			dynStr.Append("  Comment_Title=");
			dynStr.Append(Comment_Title);
			dynStr.Append("  CreateDate=");
			dynStr.Append(IsCreateDateNull ? (object)"<NULL>" : CreateDate);
			dynStr.Append("  Content=");
			dynStr.Append(Content);
			dynStr.Append("  ActionType=");
			dynStr.Append(IsActionTypeNull ? (object)"<NULL>" : ActionType);
			dynStr.Append("  Reciver_ID=");
			dynStr.Append(Reciver_ID);
			return dynStr.ToString();
		}
	} // End of ActionRow_Base class
} // End of namespace


