
using System;
namespace DFISYS.User.Db
{
	public abstract class Role_PermissionRow_Base
	{
		private int _role_ID;
		private int _permission_ID;

		public Role_PermissionRow_Base()
		{
			// EMPTY
		}

		public int Role_ID
		{
			get { return _role_ID; }
			set { _role_ID = value; }
		}

		public int Permission_ID
		{
			get { return _permission_ID; }
			set { _permission_ID = value; }
		}

		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Role_ID=");
			dynStr.Append(Role_ID);
			dynStr.Append("  Permission_ID=");
			dynStr.Append(Permission_ID);
			return dynStr.ToString();
		}
	

		/// <returns>A <see cref="PermissionRow"/> object.</returns>
		public PermissionRow PermissionRow
		{
			get 
			{ 				
				using(MainDB db = new MainDB())
				{
					PermissionRow record = db.PermissionCollection.GetByPrimaryKey(_permission_ID);
					return record;
				}
			}
		}


		/// <returns>A <see cref="RoleRow"/> object.</returns>
		public RoleRow RoleRow
		{
			get 
			{ 				
				using(MainDB db = new MainDB())
				{
					RoleRow record = db.RoleCollection.GetByPrimaryKey(_role_ID);
					return record;
				}
			}
		}


	}
}
