



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// Represents the <c>ThreadDetail</c> table.
	/// </summary>
	public class ThreadDetailCollection : ThreadDetailCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ThreadDetailCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal ThreadDetailCollection(MainDB db)
				: base(db)
		{
			// EMPTY
		}
	} // End of ThreadDetailCollection class
} // End of namespace




