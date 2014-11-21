namespace DFISYS.API
{

	#region IModuleCommunicator

	/// <summary>
	/// Any module that wants to pass text information to another module
	/// needs to implement this interface.
	/// </summary>
	public interface IModuleCommunicator
	{

		#region Public Events
		/// <summary>
		/// Raised when the wizard is finished
		/// </summary>
		event ModuleCommunicationEventHandler StartCommunicator;

		#endregion

	}

	#endregion

	#region IModuleListener

	public interface IModuleListener
	{
		void OnModuleCommunication(object s, ModuleCommunicationEventArgs e);
	}

	#endregion

	#region ModuleCommunicationEventHandler

	/// <summary>
	/// ModuleCommunication Event Handler Delegate.  
	/// </summary>
	public delegate void ModuleCommunicationEventHandler(object sender, ModuleCommunicationEventArgs e);

	#endregion

	#region ModuleCommunicationEventArgs
	/// <summary>
	/// The ModuleCommunicationEventArgs class contains all of the information regarding any 
	/// ModuleCommunication raised events. 
	/// </summary>
	public class ModuleCommunicationEventArgs : System.EventArgs
	{

		#region Private variables

		/// <summary>
		/// The private variable holder for the Text property
		/// </summary>
		private object _objEventData = null;

		private EventType _enuEventType;

		#endregion

		#region Public Properties


		/// <summary>
		/// Dữ liệu của Module nguồn cần chuyển cho Module đích
		/// </summary>
		public object Data
		{
			get { return _objEventData; }
			set { _objEventData = value; }
		}

		/// <summary>
		/// Kiểu sự kiện, được dùng để định danh sự kiện
		/// Mỗi Module có thể phát sinh các sự kiện khác nhau 
		/// do đó thuộc tính này dùng để giúp Module đích nhận dạng Module phát sinh sự kiện
		/// </summary>
		public EventType Type
		{
			get { return _enuEventType; }
			set { _enuEventType = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// The empty contructor for ModuleCommunicationEventArgs will require that the ModuleCommunicationDirection property
		/// be set before this event argument is useful.
		/// </summary>
		public ModuleCommunicationEventArgs()
		{
		}

		/// <summary>
		/// Hàm khởi tạo của lớp chứa dữ liệu về sự kiện được phát sinh bởi Module nguồn
		/// </summary>
		/// <param name="__objData">Đối tượng chứa dữ liệu đi kèm (các Module đích sẽ phải tự chuyển đổi kiểu cho phù hợp</param>
		/// <param name="__enuType">Kiểu sự kiện, dùng để nhận dạng Module nguồn, tránh cho Module đích phải bắt sự kiện không đúng mục đích</param>
		public ModuleCommunicationEventArgs(object __objData, EventType __enuType)
		{
			_objEventData = __objData;
			_enuEventType = __enuType;
		}


		#endregion
	}

	public enum EventType
	{
		//tam thoi dinh nghia 3 loai event 1- su kien phat sinh khi chuyen chuyen muc. 2- su kien khi xen thong tin chi tiet. 3- su kien khi loc tin
		CategoryChanged,
		ViewNewsDetail,
		DisplayFilterResult
	}

	#endregion

	#region ModuleCommunicators

	/// <summary>
	/// Provides for handling a collection of IModuleCommunicator
	/// </summary>
	public class ModuleCommunicators : System.Collections.CollectionBase
	{

		#region Public Properties

		/// <summary>
		/// Gets or sets the entry at the specified index of the ModuleCommunicators collection.
		/// </summary>
		public IModuleCommunicator this[int index]
		{
			get { return (IModuleCommunicator) this.List[index]; }
			set { this.List[index] = value; }
		}

		#endregion

		#region Public Constructors

		/// <summary>
		/// Creates an empty ModuleCommunicators collection.
		/// </summary>
		public ModuleCommunicators()
		{
		}

		#endregion

		#region Public Methods


		/// <summary>
		/// Add entries to the current ModuleCommunicators collection.
		/// </summary>
		/// <param name="item">The IModuleCommunicator object to add.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		public int Add(IModuleCommunicator item)
		{
			return this.List.Add(item);
		}

		#endregion


	}
	#endregion

	#region ModuleListeners

	/// <summary>
	/// Provides for handling a collection of IModuleCommunicator
	/// </summary>
	public class ModuleListeners : System.Collections.CollectionBase
	{

		#region Public Properties

		/// <summary>
		/// Gets or sets the entry at the specified index of the ModuleListeners collection.
		/// </summary>
		public IModuleListener this[int index]
		{
			get { return (IModuleListener) this.List[index]; }
			set { this.List[index] = value; }
		}

		#endregion

		#region Public Constructors

		/// <summary>
		/// Creates an empty ModuleListeners collection.
		/// </summary>
		public ModuleListeners()
		{
		}

		#endregion

		#region Public Methods


		/// <summary>
		/// Add entries to the current ModuleListeners collection.
		/// </summary>
		/// <param name="item">The IModuleListener object to add.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		public int Add(IModuleListener item)
		{
			return this.List.Add(item);
		}

		#endregion


	}
	#endregion

}
