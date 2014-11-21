
using System;
namespace DFISYS.User.Db
{
	public abstract class RoleRow_Base
	{
		private int _role_ID;
		private string _role_Name;
		private string _role_Description;

		public RoleRow_Base()
		{
			// EMPTY
		}

		public int Role_ID
		{
			get { return _role_ID; }
			set { _role_ID = value; }
		}

		public string Role_Name
		{
			get { return _role_Name; }
			set { _role_Name = value; }
		}

		public string Role_Description
		{
			get { return _role_Description; }
			set { _role_Description = value; }
		}

		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Role_ID=");
			dynStr.Append(Role_ID);
			dynStr.Append("  Role_Name=");
			dynStr.Append(Role_Name);
			dynStr.Append("  Role_Description=");
			dynStr.Append(Role_Description);
			return dynStr.ToString();
		}
					
			public PermissionRow[] PermissionRow 
      {
        get
        {							
          using(MainDB db = new MainDB())
          {
            Role_PermissionRow[] records = db.Role_PermissionCollection.GetByRole_ID(_role_ID);
		    					
            System.Collections.ArrayList tempModels = new System.Collections.ArrayList();
            foreach(Role_PermissionRow temp in records)
            {
              tempModels.Add(temp.PermissionRow);	
            }
		   					
            return (PermissionRow[])(tempModels.ToArray(typeof(PermissionRow)));
          }
        }
      }
				


	}
}
