



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// Represents the <c>CustomerFeedback</c> table.
	/// </summary>
	public class CustomerFeedbackCollection : CustomerFeedbackCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerFeedbackCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CustomerFeedbackCollection(MainDB db)
				: base(db)
		{
			// EMPTY
		}
	} // End of CustomerFeedbackCollection class
} // End of namespace




