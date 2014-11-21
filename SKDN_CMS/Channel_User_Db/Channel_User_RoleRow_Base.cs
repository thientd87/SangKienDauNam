
using System;
namespace DFISYS.User.Db
{
	public abstract class Channel_User_RoleRow_Base
	{
		private int _cur_id;
		private int _cu_id;
		private bool _cu_idNull = true;
		private int _role_ID;
		private bool _role_IDNull = true;

		public Channel_User_RoleRow_Base()
		{
			// EMPTY
		}

		public int CUR_ID
		{
			get { return _cur_id; }
			set { _cur_id = value; }
		}

		public int CU_ID
		{
			get
			{
				if(IsCU_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _cu_id;
			}
			set
			{
				_cu_idNull = false;
				_cu_id = value;
			}
		}

		public bool IsCU_IDNull
		{
			get { return _cu_idNull; }
			set { _cu_idNull = value; }
		}

		public int Role_ID
		{
			get
			{
				if(IsRole_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _role_ID;
			}
			set
			{
				_role_IDNull = false;
				_role_ID = value;
			}
		}

		public bool IsRole_IDNull
		{
			get { return _role_IDNull; }
			set { _role_IDNull = value; }
		}

		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  CUR_ID=");
			dynStr.Append(CUR_ID);
			dynStr.Append("  CU_ID=");
			dynStr.Append(IsCU_IDNull ? (object)"<NULL>" : CU_ID);
			dynStr.Append("  Role_ID=");
			dynStr.Append(IsRole_IDNull ? (object)"<NULL>" : Role_ID);
			return dynStr.ToString();
		}
	

		/// <returns>A <see cref="Channel_UserRow"/> object.</returns>


		/// <returns>A <see cref="RoleRow"/> object.</returns>


	}
}
