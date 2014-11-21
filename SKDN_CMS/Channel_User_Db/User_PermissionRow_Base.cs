
using System;
namespace DFISYS.User.Db
{
	public abstract class User_PermissionRow_Base
	{
		private int _up_id;
		private int _cur_id;
		private bool _cur_idNull = true;
		private int _permission_ID;
		private bool _permission_IDNull = true;

		public User_PermissionRow_Base()
		{
			// EMPTY
		}

		public int UP_ID
		{
			get { return _up_id; }
			set { _up_id = value; }
		}

		public int CUR_ID
		{
			get
			{
				if(IsCUR_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _cur_id;
			}
			set
			{
				_cur_idNull = false;
				_cur_id = value;
			}
		}

		public bool IsCUR_IDNull
		{
			get { return _cur_idNull; }
			set { _cur_idNull = value; }
		}

		public int Permission_ID
		{
			get
			{
				if(IsPermission_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _permission_ID;
			}
			set
			{
				_permission_IDNull = false;
				_permission_ID = value;
			}
		}

		public bool IsPermission_IDNull
		{
			get { return _permission_IDNull; }
			set { _permission_IDNull = value; }
		}

		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  UP_ID=");
			dynStr.Append(UP_ID);
			dynStr.Append("  CUR_ID=");
			dynStr.Append(IsCUR_IDNull ? (object)"<NULL>" : CUR_ID);
			dynStr.Append("  Permission_ID=");
			dynStr.Append(IsPermission_IDNull ? (object)"<NULL>" : Permission_ID);
			return dynStr.ToString();
		}
	

		/// <returns>A <see cref="Channel_User_RoleRow"/> object.</returns>


		/// <returns>A <see cref="PermissionRow"/> object.</returns>


	}
}
