



using System;

namespace DFISYS.Core.DAL
{
    /// <summary>
    /// The base class for <see cref="VoteRow"/> that 
    /// represents a record in the <c>Vote</c> table.
    /// </summary>
    /// <remarks>
    /// Do not change this source code manually. Update the <see cref="VoteRow"/>
    /// class if you need to add or change some functionality.
    /// </remarks>
    public abstract class VoteRow_Base
    {
        private int _vote_ID;
        private string _userID;
        private string _vote_Title;
        private DateTime _vote_StartDate;
        private bool _vote_StartDateNull = true;
        private DateTime _vote_EndDate;
        private bool _vote_EndDateNull = true;
        private int _vote_Parent;
        private bool _vote_ParentNull = true;
        private string _vote_Parent_Image;
        private string _vote_InitContent;
        private int _cat_ID = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="VoteRow_Base"/> class.
        /// </summary>
        public VoteRow_Base()
        {
            // EMPTY
        }

        /// <summary>
        /// Gets or sets the <c>Vote_ID</c> column value.
        /// </summary>
        /// <value>The <c>Vote_ID</c> column value.</value>
        public int Vote_ID
        {
            get { return _vote_ID; }
            set { _vote_ID = value; }
        }

        /// <summary>
        /// Gets or sets the <c>UserID</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>UserID</c> column value.</value>
        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Vote_Title</c> column value.
        /// </summary>
        /// <value>The <c>Vote_Title</c> column value.</value>
        public string Vote_Title
        {
            get { return _vote_Title; }
            set { _vote_Title = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Vote_StartDate</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Vote_StartDate</c> column value.</value>
        public DateTime Vote_StartDate
        {
            get
            {
                if (IsVote_StartDateNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _vote_StartDate;
            }
            set
            {
                _vote_StartDateNull = false;
                _vote_StartDate = value;
            }
        }

        /// <summary>
        /// Indicates whether the <see cref="Vote_StartDate"/>
        /// property value is null.
        /// </summary>
        /// <value>true if the property value is null, otherwise false.</value>
        public bool IsVote_StartDateNull
        {
            get { return _vote_StartDateNull; }
            set { _vote_StartDateNull = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Vote_EndDate</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Vote_EndDate</c> column value.</value>
        public DateTime Vote_EndDate
        {
            get
            {
                if (IsVote_EndDateNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _vote_EndDate;
            }
            set
            {
                _vote_EndDateNull = false;
                _vote_EndDate = value;
            }
        }

        /// <summary>
        /// Indicates whether the <see cref="Vote_EndDate"/>
        /// property value is null.
        /// </summary>
        /// <value>true if the property value is null, otherwise false.</value>
        public bool IsVote_EndDateNull
        {
            get { return _vote_EndDateNull; }
            set { _vote_EndDateNull = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Vote_Parent</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Vote_Parent</c> column value.</value>
        public int Vote_Parent
        {
            get
            {
                if (IsVote_ParentNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _vote_Parent;
            }
            set
            {
                _vote_ParentNull = false;
                _vote_Parent = value;
            }
        }

        /// <summary>
        /// Indicates whether the <see cref="Vote_Parent"/>
        /// property value is null.
        /// </summary>
        /// <value>true if the property value is null, otherwise false.</value>
        public bool IsVote_ParentNull
        {
            get { return _vote_ParentNull; }
            set { _vote_ParentNull = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Vote_Parent_Image</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Vote_Parent_Image</c> column value.</value>
        public string Vote_Parent_Image
        {
            get { return _vote_Parent_Image; }
            set { _vote_Parent_Image = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Vote_InitContent</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Vote_InitContent</c> column value.</value>
        public string Vote_InitContent
        {
            get { return _vote_InitContent; }
            set { _vote_InitContent = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Cat_ID</c> column value.
        /// </summary>
        /// <value>The <c>Cat_ID</c> column value.</value>
        public int Cat_ID
        {
            get { return _cat_ID; }
            set { _cat_ID = value; }
        }

        /// <summary>
        /// Returns the string representation of this instance.
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
            dynStr.Append(':');
            dynStr.Append("  Vote_ID=");
            dynStr.Append(Vote_ID);
            dynStr.Append("  UserID=");
            dynStr.Append(UserID);
            dynStr.Append("  Vote_Title=");
            dynStr.Append(Vote_Title);
            dynStr.Append("  Vote_StartDate=");
            dynStr.Append(IsVote_StartDateNull ? (object)"<NULL>" : Vote_StartDate);
            dynStr.Append("  Vote_EndDate=");
            dynStr.Append(IsVote_EndDateNull ? (object)"<NULL>" : Vote_EndDate);
            dynStr.Append("  Vote_Parent=");
            dynStr.Append(IsVote_ParentNull ? (object)"<NULL>" : Vote_Parent);
            dynStr.Append("  Vote_Parent_Image=");
            dynStr.Append(Vote_Parent_Image);
            dynStr.Append("  Vote_InitContent=");
            dynStr.Append(Vote_InitContent);
            dynStr.Append("  Cat_ID=");
            dynStr.Append(Cat_ID);
            return dynStr.ToString();
        }
    } // End of VoteRow_Base class
} // End of namespace


