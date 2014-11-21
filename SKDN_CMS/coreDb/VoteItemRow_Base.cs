using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="VoteItemRow"/> that 
	/// represents a record in the <c>VoteItem</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="VoteItemRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class VoteItemRow_Base
	{
		private int _voteIt_ID ;
		private string _voteIt_Content ;
		private int _vote_ID ;
		private bool _vote_IDNull = true;
		private decimal _voteIt_Rate  = 0;
		private bool _voteIt_RateNull = false;
		private bool _isShow ;
		private bool _isShowNull = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="VoteItemRow_Base"/> class.
		/// </summary>
		public VoteItemRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>VoteIt_ID</c> column value.
		/// </summary>
		/// <value>The <c>VoteIt_ID</c> column value.</value>
		public int VoteIt_ID
		{
			get { return _voteIt_ID; }
			set { _voteIt_ID = value; }
		}

		/// <summary>
		/// Gets or sets the <c>VoteIt_Content</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>VoteIt_Content</c> column value.</value>
		public string VoteIt_Content
		{
			get { return _voteIt_Content; }
			set { _voteIt_Content = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Vote_ID</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Vote_ID</c> column value.</value>
		public int Vote_ID
		{
			get
			{
				if(IsVote_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _vote_ID;
			}
			set
			{
				_vote_IDNull = false;
				_vote_ID = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Vote_ID"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsVote_IDNull
		{
			get { return _vote_IDNull; }
			set { _vote_IDNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>VoteIt_Rate</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>VoteIt_Rate</c> column value.</value>
		public decimal VoteIt_Rate
		{
			get
			{
				if(IsVoteIt_RateNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _voteIt_Rate;
			}
			set
			{
				_voteIt_RateNull = false;
				_voteIt_Rate = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="VoteIt_Rate"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsVoteIt_RateNull
		{
			get { return _voteIt_RateNull; }
			set { _voteIt_RateNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>isShow</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>isShow</c> column value.</value>
		public bool isShow
		{
			get
			{
				if(IsisShowNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _isShow;
			}
			set
			{
				_isShowNull = false;
				_isShow = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="isShow"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsisShowNull
		{
			get { return _isShowNull; }
			set { _isShowNull = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  VoteIt_ID=");
			dynStr.Append(VoteIt_ID);
			dynStr.Append("  VoteIt_Content=");
			dynStr.Append(VoteIt_Content);
			dynStr.Append("  Vote_ID=");
			dynStr.Append(IsVote_IDNull ? (object)"<NULL>" : Vote_ID);
			dynStr.Append("  VoteIt_Rate=");
			dynStr.Append(IsVoteIt_RateNull ? (object)"<NULL>" : VoteIt_Rate);
			dynStr.Append("  isShow=");
			dynStr.Append(IsisShowNull ? (object)"<NULL>" : isShow);
			return dynStr.ToString();
		}
	} // End of VoteItemRow_Base class
} // End of namespace


