



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// Represents the <c>ThreadDetails</c> table.
	/// </summary>
	public class ThreadDetailsCollection : ThreadDetailsCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ThreadDetailsCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal ThreadDetailsCollection(MainDB db)
				: base(db)
		{
			// EMPTY
		}
	} // End of ThreadDetailsCollection class
} // End of namespace




