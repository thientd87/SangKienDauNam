
using System;
namespace DFISYS.User.Db
{
	public abstract class Channel_UserRow_Base
	{
		private int _cu_id;
		private string _user_ID;
		private int _channel_ID;
		private bool _channel_IDNull = true;

		public Channel_UserRow_Base()
		{
			// EMPTY
		}

		public int CU_ID
		{
			get { return _cu_id; }
			set { _cu_id = value; }
		}

		public string User_ID
		{
			get { return _user_ID; }
			set { _user_ID = value; }
		}

		public int Channel_ID
		{
			get
			{
				if(IsChannel_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _channel_ID;
			}
			set
			{
				_channel_IDNull = false;
				_channel_ID = value;
			}
		}

		public bool IsChannel_IDNull
		{
			get { return _channel_IDNull; }
			set { _channel_IDNull = value; }
		}

		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  CU_ID=");
			dynStr.Append(CU_ID);
			dynStr.Append("  User_ID=");
			dynStr.Append(User_ID);
			dynStr.Append("  Channel_ID=");
			dynStr.Append(IsChannel_IDNull ? (object)"<NULL>" : Channel_ID);
			return dynStr.ToString();
		}
	

		/// <returns>A <see cref="ChannelRow"/> object.</returns>


		/// <returns>A <see cref="UserRow"/> object.</returns>
		public UserRow UserRow
		{
			get 
			{ 				
				using(MainDB db = new MainDB())
				{
					UserRow record = db.UserCollection.GetByPrimaryKey(_user_ID);
					return record;
				}
			}
		}


	}
}
