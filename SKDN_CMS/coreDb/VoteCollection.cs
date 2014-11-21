namespace DFISYS.Core.DAL
{
	/// <summary>
	/// Represents the <c>Vote</c> table.
	/// </summary>
	public class VoteCollection : VoteCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="VoteCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal VoteCollection(MainDB db)
				: base(db)
		{
			// EMPTY
		}
	} // End of VoteCollection class
} // End of namespace




