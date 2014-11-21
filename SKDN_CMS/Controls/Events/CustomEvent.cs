using System;

namespace Intelliworks.Modules.Events
{
	// Delegate declaration
	public delegate void EditEventHandler(object sender, EditEventArgs e);

	/// <summary>
	/// Lớp lưu trữ dữ liệu của sự kiện Edit 1 Item trong Grid
	/// </summary>
	public class EditEventArgs : EventArgs
	{
		private readonly int intKeyValue;

		// Constructor
		public EditEventArgs(int _intKeyValue)
		{
			intKeyValue = _intKeyValue;
		}

		/// <summary>
		/// Mã Item cần sửa
		/// </summary>
		public int KeyValue
		{
			get{return intKeyValue;}
		}
	}

	// Delegate declaration
	public delegate void SubmitCompleteEventHandler(object sender, EventArgs e);
}
