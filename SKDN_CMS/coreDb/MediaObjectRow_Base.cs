



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="MediaObjectRow"/> that 
	/// represents a record in the <c>MediaObject</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="MediaObjectRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class MediaObjectRow_Base
	{
		private int _object_ID ;
		private int _object_Type ;
		private bool _object_TypeNull = true;
		private string _object_Value ;
		private string _object_Url ;
		private string _object_Note ;
		private string _userID ;

		/// <summary>
		/// Initializes a new instance of the <see cref="MediaObjectRow_Base"/> class.
		/// </summary>
		public MediaObjectRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>Object_ID</c> column value.
		/// </summary>
		/// <value>The <c>Object_ID</c> column value.</value>
		public int Object_ID
		{
			get { return _object_ID; }
			set { _object_ID = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Object_Type</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Object_Type</c> column value.</value>
		public int Object_Type
		{
			get
			{
				if(IsObject_TypeNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _object_Type;
			}
			set
			{
				_object_TypeNull = false;
				_object_Type = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Object_Type"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsObject_TypeNull
		{
			get { return _object_TypeNull; }
			set { _object_TypeNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Object_Value</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Object_Value</c> column value.</value>
		public string Object_Value
		{
			get { return _object_Value; }
			set { _object_Value = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Object_Url</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Object_Url</c> column value.</value>
		public string Object_Url
		{
			get { return _object_Url; }
			set { _object_Url = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Object_Note</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Object_Note</c> column value.</value>
		public string Object_Note
		{
			get { return _object_Note; }
			set { _object_Note = value; }
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
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Object_ID=");
			dynStr.Append(Object_ID);
			dynStr.Append("  Object_Type=");
			dynStr.Append(IsObject_TypeNull ? (object)"<NULL>" : Object_Type);
			dynStr.Append("  Object_Value=");
			dynStr.Append(Object_Value);
			dynStr.Append("  Object_Url=");
			dynStr.Append(Object_Url);
			dynStr.Append("  Object_Note=");
			dynStr.Append(Object_Note);
			dynStr.Append("  UserID=");
			dynStr.Append(UserID);
			return dynStr.ToString();
		}
	} // End of MediaObjectRow_Base class
} // End of namespace


