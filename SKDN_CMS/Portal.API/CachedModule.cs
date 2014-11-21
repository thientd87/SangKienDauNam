using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;

namespace DFISYS.API
{
	/// <summary>
	/// The CachedPortalModuleControl class is a custom server control that
	/// the Portal framework uses to optionally enable output caching of 
	/// individual portal module's content.<br />
	/// If a CacheTime value greater than 0 seconds is specified within the 
	/// Portal.Config configuration file, then the CachePortalModuleControl
	/// will automatically capture the output of the Portal Module User Control
	/// it wraps. It will then store this captured output within the ASP.NET
	/// Cache API. On subsequent requests (either by the same browser -- or
	/// by other browsers visiting the same portal page), the CachedPortalModuleControl
	/// will attempt to resolve the cached output out of the cache.
	/// </summary>
	/// <remarks>
	/// In the event that previously cached output can't be found in the
	/// ASP.NET Cache, the CachedPortalModuleControl will automatically instatiate
	/// the appropriate portal module user control and place it within the
	/// portal page.
	/// </remarks>
	public class CachedModule : Control 
	{
		#region Variables & Properties
		// Private field variables
		private string	_moduleReference = string.Empty;
		private string	_moduleType = string.Empty;
		private string	_moduleSrc = string.Empty;
		private string	_moduleVirtualPath = string.Empty;
		private int		_moduleCacheTime = 0;
		private string	_tabReference = string.Empty;
		private string  _cachedOutput = string.Empty;
		private bool	_blnHasEditRights = false;

		/// <summary>
		/// Module Reference
		/// </summary>
		public string ModuleReference
		{
			get 
			{
				return _moduleReference;
			}
			set
			{
				_moduleReference = value;
			}
		}

		/// <summary>
		/// Tab Reference
		/// </summary>
		public string TabReference
		{
			get 
			{
				return _tabReference;
			}
			set
			{
				_tabReference = value;
			}
		}

		/// <summary>
		/// CacheTime
		/// </summary>
		public int ModuleCacheTime
		{
			get
			{
				return _moduleCacheTime;
			}
			set
			{
				_moduleCacheTime = value;
			}
		}

		/// <summary>
		/// Module Type
		/// </summary>
		public string ModuleType
		{
			get
			{
				return _moduleType;
			}
			set
			{
				_moduleType = value;
			}
		}

		/// <summary>
		/// Module Source File
		/// </summary>
		public string ModuleSrc
		{
			get
			{
				return _moduleSrc;
			}
			set
			{
				_moduleSrc = value;
			}
		}

		public string ModuleVirtualPath
		{
			get
			{
				return _moduleVirtualPath;
			}
			set
			{
				_moduleVirtualPath = value;
			}
		}

		/// <summary>
		/// Is User Has edit rights
		/// </summary>
		public bool HasEditRights
		{
			get 
			{
				return _blnHasEditRights;
			}
			set
			{
				_blnHasEditRights = value;
			}
		}

		/// <summary>
		/// The CacheKey property is used to calculate a "unique" cache key
		/// entry to be used to store/retrieve the portal module's content
		/// from the ASP.NET Cache.
		/// </summary>
		public string CacheKey 
		{
			get 
			{
				StringBuilder sb = new StringBuilder();

				sb.Append(Config.GetPortalUniqueCacheKey());
				sb.Append("_ModuleRef_");
				sb.Append(_moduleReference.ToString());

				return sb.ToString();
			}
		}
		#endregion

		/// <summary>
		/// Initializes the Control. Called by the Protal Framework
		/// </summary>
		/// <param name="tabRef">Tab Reference</param>
		/// <param name="moduleRef">Module Reference</param>
		/// <param name="type">Module Type</param>
		/// <param name="virtualPath">Module Virtual Path</param>
		/// <param name="hasEditRights">User accessible</param>
		public void InitModule(string tabRef, string moduleRef, string type, string virtualPath, bool hasEditRights, string moduleSrc)
		{			
			_tabReference = tabRef;
			_blnHasEditRights = hasEditRights;
			_moduleReference = moduleRef;
			_moduleVirtualPath = virtualPath;
			_moduleSrc = moduleSrc;
			_moduleType = type;
		}

		/// <summary>
		/// The CreateChildControls method is called when the ASP.NET Page Framework
		/// determines that it is time to instantiate a server control.<br/>
		/// The CachedPortalModuleControl control overrides this method and attempts
		/// to resolve any previously cached output of the portal module from the ASP.NET cache.  
		/// If it doesn't find cached output from a previous request, then the
		/// CachedPortalModuleControl will instantiate and add the portal module's
		/// User Control instance into the page tree.
		/// </summary>
		protected override void CreateChildControls() 
		{
			// Attempt to resolve previously cached content from the ASP.NET Cache
			if (_moduleCacheTime > 0) 
			{
				_cachedOutput = (string) Context.Cache[CacheKey];
			}

			// If no cached content is found, then instantiate and add the portal
			// module user control into the portal's page server control tree
			if (_cachedOutput == null)
			{
				base.CreateChildControls();

				DFISYS.API.Module module = (DFISYS.API.Module) Page.LoadControl(_moduleSrc);

				module.InitModule(_tabReference, _moduleReference, _moduleType, _moduleVirtualPath, _blnHasEditRights);

				this.Controls.Add(module);
			}
		}

		/// <summary>
		/// The Render method is called when the ASP.NET Page Framework
		/// determines that it is time to render content into the page output stream.
		/// The CachedPortalModuleControl control overrides this method and captures
		/// the output generated by the portal module user control. It then 
		/// adds this content into the ASP.NET Cache for future requests.
		/// </summary>
		/// <param name="output"></param>
		protected override void Render(HtmlTextWriter output) 
		{
			// If no caching is specified, render the child tree and return 
			//if (_moduleConfiguration.CacheTime == 0) // Jes1111
			if (_moduleCacheTime <= 0) 
			{
				base.Render(output);
				return;
			}

			// If no cached output was found from a previous request, render
			// child controls into a TextWriter, and then cache the results
			// in the ASP.NET Cache for future requests.
			if (_cachedOutput == null)
			{
				using (TextWriter tempWriter = new StringWriter())
				{
					base.Render(new HtmlTextWriter(tempWriter));
					_cachedOutput = tempWriter.ToString();
				}

				/*// change 28/Feb/2003 - Jeremy Esland - Cache
				// added file dependencies for cache insert
				if (this._moduleConfiguration.CacheDependency != null)
				{
					string[] dependencyList = new string[this._moduleConfiguration.CacheDependency.Count];
					int i = 0;
					foreach(string thisfile in this._moduleConfiguration.CacheDependency)
					{
						dependencyList[i] = thisfile;
						i++;
					}
					using (CacheDependency _cacheDependency = new CacheDependency(dependencyList))
					{
						Context.Cache.Insert(CacheKey, _cachedOutput, _cacheDependency, DateTime.Now.AddSeconds(_moduleConfiguration.CacheTime), TimeSpan.Zero);
					}
					Debug.WriteLine("************** Insert Render1" + CacheKey);
				}
				else
				{
					Context.Cache.Insert(CacheKey, _cachedOutput, null, DateTime.Now.AddSeconds(_moduleConfiguration.CacheTime), TimeSpan.Zero);
					Debug.WriteLine("************** Insert Render2" + CacheKey);
				}*/

				using (CacheDependency _cacheDependency = new CacheDependency(Context.Server.MapPath(_moduleSrc)))
				{
					// Add Cache Dependency
					Context.Cache.Insert(CacheKey, _cachedOutput, _cacheDependency, DateTime.Now.AddSeconds(_moduleCacheTime), TimeSpan.Zero);
					Context.Response.Cache.SetCacheability(HttpCacheability.Server);
				}

			}

			// Output the user control's content
			output.Write(_cachedOutput);
		}

		/// <summary>
		/// The Module can control its visibility. The Login Module does so
		/// </summary>
		/// <returns>true if the Module should be visible</returns>
		public virtual bool IsVisible()
		{
			return true;
		}
	}
}
