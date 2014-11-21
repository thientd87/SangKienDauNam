



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// Represents the <c>News_Media</c> table.
	/// </summary>
	public class News_MediaCollection : News_MediaCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="News_MediaCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal News_MediaCollection(MainDB db)
				: base(db)
		{
			// EMPTY
		}
	} // End of News_MediaCollection class
} // End of namespace




