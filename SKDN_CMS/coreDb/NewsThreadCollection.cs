



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// Represents the <c>NewsThread</c> table.
	/// </summary>
	public class NewsThreadCollection : NewsThreadCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NewsThreadCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal NewsThreadCollection(MainDB db)
				: base(db)
		{
			// EMPTY
		}
	} // End of NewsThreadCollection class
} // End of namespace




