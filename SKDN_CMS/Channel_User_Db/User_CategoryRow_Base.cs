
using System;
namespace DFISYS.User.Db
{
	public abstract class User_CategoryRow_Base
	{
		private int _uc_id;
		private int _up_id;
		private bool _up_idNull = true;
		private int _cat_ID;
		private bool _cat_IDNull = true;

		public User_CategoryRow_Base()
		{
			// EMPTY
		}

		public int UC_ID
		{
			get { return _uc_id; }
			set { _uc_id = value; }
		}

		public int UP_ID
		{
			get
			{
				if(IsUP_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _up_id;
			}
			set
			{
				_up_idNull = false;
				_up_id = value;
			}
		}

		public bool IsUP_IDNull
		{
			get { return _up_idNull; }
			set { _up_idNull = value; }
		}

		public int Cat_ID
		{
			get
			{
				if(IsCat_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _cat_ID;
			}
			set
			{
				_cat_IDNull = false;
				_cat_ID = value;
			}
		}

		public bool IsCat_IDNull
		{
			get { return _cat_IDNull; }
			set { _cat_IDNull = value; }
		}

		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  UC_ID=");
			dynStr.Append(UC_ID);
			dynStr.Append("  UP_ID=");
			dynStr.Append(IsUP_IDNull ? (object)"<NULL>" : UP_ID);
			dynStr.Append("  Cat_ID=");
			dynStr.Append(IsCat_IDNull ? (object)"<NULL>" : Cat_ID);
			return dynStr.ToString();
		}
	

		/// <returns>A <see cref="User_PermissionRow"/> object.</returns>


	}
}
