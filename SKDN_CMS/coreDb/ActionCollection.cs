



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// Represents the <c>Action</c> table.
	/// </summary>
	public class ActionCollection : ActionCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal ActionCollection(MainDB db)
				: base(db)
		{
			// EMPTY
		}
	} // End of ActionCollection class
} // End of namespace




