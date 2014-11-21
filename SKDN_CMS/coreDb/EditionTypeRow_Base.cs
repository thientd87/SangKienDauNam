



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="EditionTypeRow"/> that 
	/// represents a record in the <c>EditionType</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="EditionTypeRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class EditionTypeRow_Base
	{
		private int _editionType_ID ;
		private string _editionName ;
		private string _editionDescription ;
		private string _editionDisplayURL ;

		/// <summary>
		/// Initializes a new instance of the <see cref="EditionTypeRow_Base"/> class.
		/// </summary>
		public EditionTypeRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>EditionType_ID</c> column value.
		/// </summary>
		/// <value>The <c>EditionType_ID</c> column value.</value>
		public int EditionType_ID
		{
			get { return _editionType_ID; }
			set { _editionType_ID = value; }
		}

		/// <summary>
		/// Gets or sets the <c>EditionName</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>EditionName</c> column value.</value>
		public string EditionName
		{
			get { return _editionName; }
			set { _editionName = value; }
		}

		/// <summary>
		/// Gets or sets the <c>EditionDescription</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>EditionDescription</c> column value.</value>
		public string EditionDescription
		{
			get { return _editionDescription; }
			set { _editionDescription = value; }
		}

		/// <summary>
		/// Gets or sets the <c>EditionDisplayURL</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>EditionDisplayURL</c> column value.</value>
		public string EditionDisplayURL
		{
			get { return _editionDisplayURL; }
			set { _editionDisplayURL = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  EditionType_ID=");
			dynStr.Append(EditionType_ID);
			dynStr.Append("  EditionName=");
			dynStr.Append(EditionName);
			dynStr.Append("  EditionDescription=");
			dynStr.Append(EditionDescription);
			dynStr.Append("  EditionDisplayURL=");
			dynStr.Append(EditionDisplayURL);
			return dynStr.ToString();
		}
	} // End of EditionTypeRow_Base class
} // End of namespace


