



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="News_MediaRow"/> that 
	/// represents a record in the <c>News_Media</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="News_MediaRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class News_MediaRow_Base
	{
		private long _nm_id ;
		private long _news_ID ;
		private bool _news_IDNull = true;
		private int _object_ID ;
		private bool _object_IDNull = true;
		private string _useAvatar ;
		private string _use_Note ;
		private string _film_ID ;
		private bool _film_IDNull = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="News_MediaRow_Base"/> class.
		/// </summary>
		public News_MediaRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>NM_ID</c> column value.
		/// </summary>
		/// <value>The <c>NM_ID</c> column value.</value>
		public long NM_ID
		{
			get { return _nm_id; }
			set { _nm_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_ID</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_ID</c> column value.</value>
		public long News_ID
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
		/// Gets or sets the <c>Object_ID</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Object_ID</c> column value.</value>
		public int Object_ID
		{
			get
			{
				if(IsObject_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _object_ID;
			}
			set
			{
				_object_IDNull = false;
				_object_ID = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Object_ID"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsObject_IDNull
		{
			get { return _object_IDNull; }
			set { _object_IDNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>UseAvatar</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>UseAvatar</c> column value.</value>
		public string UseAvatar
		{
			get { return _useAvatar; }
			set { _useAvatar = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Use_Note</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Use_Note</c> column value.</value>
		public string Use_Note
		{
			get { return _use_Note; }
			set { _use_Note = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Film_ID</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Film_ID</c> column value.</value>
		public string Film_ID
		{
			get
			{
				if(IsFilm_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _film_ID;
			}
			set
			{
				_film_IDNull = false;
				_film_ID = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Film_ID"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsFilm_IDNull
		{
			get { return _film_IDNull; }
			set { _film_IDNull = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  NM_ID=");
			dynStr.Append(NM_ID);
			dynStr.Append("  News_ID=");
			dynStr.Append(IsNews_IDNull ? (object)"<NULL>" : News_ID);
			dynStr.Append("  Object_ID=");
			dynStr.Append(IsObject_IDNull ? (object)"<NULL>" : Object_ID);
			dynStr.Append("  UseAvatar=");
			dynStr.Append(UseAvatar);
			dynStr.Append("  Use_Note=");
			dynStr.Append(Use_Note);
			dynStr.Append("  Film_ID=");
			dynStr.Append(IsFilm_IDNull ? (object)"<NULL>" : Film_ID);
			return dynStr.ToString();
		}
	} // End of News_MediaRow_Base class
} // End of namespace


