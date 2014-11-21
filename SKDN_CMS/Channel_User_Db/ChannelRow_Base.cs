
using System;
namespace DFISYS.User.Db
{
	public abstract class ChannelRow_Base
	{
		private int _channel_ID;
		private string _channel_Name;
		private string _channel_Description;

		public ChannelRow_Base()
		{
			// EMPTY
		}

		public int Channel_ID
		{
			get { return _channel_ID; }
			set { _channel_ID = value; }
		}

		public string Channel_Name
		{
			get { return _channel_Name; }
			set { _channel_Name = value; }
		}

		public string Channel_Description
		{
			get { return _channel_Description; }
			set { _channel_Description = value; }
		}

		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Channel_ID=");
			dynStr.Append(Channel_ID);
			dynStr.Append("  Channel_Name=");
			dynStr.Append(Channel_Name);
			dynStr.Append("  Channel_Description=");
			dynStr.Append(Channel_Description);
			return dynStr.ToString();
		}
	

	}
}
