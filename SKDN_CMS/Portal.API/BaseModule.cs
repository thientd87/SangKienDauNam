using System.Collections;

namespace DFISYS.API
{
	/// <summary>
	/// Summary description for BasePage.
	/// </summary>
	public class BaseModule : System.Web.UI.UserControl
	{

		#region Private Variables

		private ModuleCommunicators _ModuleCommunicators = new ModuleCommunicators();
		private ModuleListeners _ModuleListeners = new ModuleListeners();

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the collections of ModuleCommunicators
		/// </summary>
		public ModuleCommunicators ModuleCommunicators
		{
			get { return _ModuleCommunicators; }
		}

		/// <summary>
		/// Gets the collections of ModuleListeners
		/// </summary>
		public ModuleListeners ModuleListeners
		{
			get { return _ModuleListeners; }
		}

		#endregion

		#region Public Constructors

		/// <summary>
		/// Creates a base Module class
		/// </summary>
		public BaseModule()
		{
		}

		#endregion

		#region Public Methods

		public System.Web.UI.Control LoadModule(string virtualPath)
		{
			System.Web.UI.Control returnData = base.LoadControl(virtualPath);

			// Check and see if the module implements IModuleCommunicator
			IModuleCommunicator moduleCommunicator = returnData as IModuleCommunicator;
			if(moduleCommunicator!=null)
			{
				// Add the module because it implements IModuleCommunicator
				this.Add(moduleCommunicator);
			}

			// Check and see if the module implements IModuleCommunicator
			IModuleListener moduleListener = returnData as IModuleListener;
			if(moduleListener!=null)
			{
				// Add the module because it implements IModuleListener
				this.Add(moduleListener);
			}

			return returnData;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Adds the IModuleCommuicator class and wires-up any ModuleListeners to the new IModuleCommunicator
		/// </summary>
		/// <param name="item">The IModuleCommunicator object to add</param>
		/// <returns>The position into which the new element was inserted.</returns>
		private int Add(IModuleCommunicator item)
		{
			int returnData = _ModuleCommunicators.Add(item);

			for(int i=0;i<_ModuleListeners.Count;i++)
			{
				item.StartCommunicator += new ModuleCommunicationEventHandler(_ModuleListeners[i].OnModuleCommunication);
			}


			return returnData;

		}

		/// <summary>
		/// Adds the IModuleListener and wires-up any ModuleCommunicators to the new IModuleListener
		/// </summary>
		/// <param name="item">The IModuleListener object to add</param>
		/// <returns>The position into which the new element was inserted.</returns>
		private int Add(IModuleListener item)
		{
			int returnData = _ModuleListeners.Add(item);

			for(int i=0; i<_ModuleCommunicators.Count;i++)
			{
				_ModuleCommunicators[i].StartCommunicator += new ModuleCommunicationEventHandler(item.OnModuleCommunication);
			}

			return returnData;
		}


		#endregion

	}
}
