
using System;
namespace Portal.User.Db
{
	public abstract class sysdiagramsRow_Base
	{
		private string _name;
		private int _principal_id;
		private int _diagram_id;
		private int _version;
		private bool _versionNull = true;
		private byte[] _definition;

		public sysdiagramsRow_Base()
		{
			// EMPTY
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public int Principal_id
		{
			get { return _principal_id; }
			set { _principal_id = value; }
		}

		public int Diagram_id
		{
			get { return _diagram_id; }
			set { _diagram_id = value; }
		}

		public int Version
		{
			get
			{
				if(IsVersionNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _version;
			}
			set
			{
				_versionNull = false;
				_version = value;
			}
		}

		public bool IsVersionNull
		{
			get { return _versionNull; }
			set { _versionNull = value; }
		}

		public byte[] Definition
		{
			get { return _definition; }
			set { _definition = value; }
		}

		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Name=");
			dynStr.Append(Name);
			dynStr.Append("  Principal_id=");
			dynStr.Append(Principal_id);
			dynStr.Append("  Diagram_id=");
			dynStr.Append(Diagram_id);
			dynStr.Append("  Version=");
			dynStr.Append(IsVersionNull ? (object)"<NULL>" : Version);
			return dynStr.ToString();
		}
	

	}
}
