



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="NewsThreadRow"/> that 
	/// represents a record in the <c>NewsThread</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="NewsThreadRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class NewsThreadRow_Base
	{
		private int _thread_ID ;
		private string _title ;
		private bool _thread_isForcus ;
		private bool _thread_isForcusNull = true;
		private string _thread_Logo ;
	    private string _thread_RT;
        private int _thread_RC;
        private int _status;

		/// <summary>
		/// Initializes a new instance of the <see cref="NewsThreadRow_Base"/> class.
		/// </summary>
		public NewsThreadRow_Base()
		{
			// EMPTY
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
		/// Gets or sets the <c>Title</c> column value.
		/// </summary>
		/// <value>The <c>Title</c> column value.</value>
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Thread_isForcus</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Thread_isForcus</c> column value.</value>
		public bool Thread_isForcus
		{
			get
			{
				if(IsThread_isForcusNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _thread_isForcus;
			}
			set
			{
				_thread_isForcusNull = false;
				_thread_isForcus = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Thread_isForcus"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsThread_isForcusNull
		{
			get { return _thread_isForcusNull; }
			set { _thread_isForcusNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Thread_Logo</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Thread_Logo</c> column value.</value>
		public string Thread_Logo
		{
			get { return _thread_Logo; }
			set { _thread_Logo = value; }
		}

	    public string Thread_RT
	    {
	        get { return _thread_RT; }
	        set { _thread_RT = value; }
	    }

        /// <summary>
        /// Gets or sets the <c>Status</c> column value
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Status</c> column value</value>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Thread_RC</c> column value.
        /// </summary>
        /// <value>The <c>Thread_RC</c> column value.</value>
        public int Thread_RC
        {
            get { return _thread_RC; }
            set { _thread_RC = value; }
        }

	    /// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Thread_ID=");
			dynStr.Append(Thread_ID);
			dynStr.Append("  Title=");
			dynStr.Append(Title);
			dynStr.Append("  Thread_isForcus=");
			dynStr.Append(IsThread_isForcusNull ? (object)"<NULL>" : Thread_isForcus);
			dynStr.Append("  Thread_Logo=");
			dynStr.Append(Thread_Logo);
            dynStr.Append("  Thread_RC=");
            dynStr.Append(Thread_RC);
			return dynStr.ToString();
		}
	} // End of NewsThreadRow_Base class
} // End of namespace


