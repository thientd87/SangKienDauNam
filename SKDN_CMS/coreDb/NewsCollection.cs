



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// Represents the <c>News</c> table.
	/// </summary>
	public class NewsCollection : NewsCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NewsCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal NewsCollection(MainDB db)
				: base(db)
		{
			// EMPTY
		}
	} // End of NewsCollection class
} // End of namespace




