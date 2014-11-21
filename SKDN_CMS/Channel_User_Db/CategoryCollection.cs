



using System;

namespace DFISYS.User.Db
{
	/// <summary>
	/// Represents the <c>Category</c> table.
	/// </summary>
	public class CategoryCollection : CategoryCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CategoryCollection(MainDB db)
				: base(db)
		{
			// EMPTY
		}
	} // End of CategoryCollection class
} // End of namespace




