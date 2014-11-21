
using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// Represents the <c>FeedBackNews</c> table.
	/// </summary>
	public class FeedBackNewsCollection : FeedBackNewsCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FeedBackNewsCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal FeedBackNewsCollection(MainDB db)
				: base(db)
		{
			// EMPTY
		}
	} // End of FeedBackNewsCollection class
} // End of namespace




