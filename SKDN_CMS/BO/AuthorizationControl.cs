using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

namespace Portal
{
	// Define a delegate whose signature containts AuthenticateRolesEventArgs
	public delegate void AuthenticateRolesEventHandler(object sender, AuthenticateRolesEventArgs e);
	/// <summary>
	/// Summary description for AuthorizationControl.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:AuthorizationControl runat=server></{0}:AuthorizationControl>")]
	public class AuthorizationControl : System.Web.UI.WebControls.PlaceHolder
	{
		#region Private Member
		private string text;
		private string _configconnectionstring;
		private string _allowroles;
		private string _denyroles;
		private int _allowlevel = 1000;
		private AuthenticationType _authenticationtype;
		private Hashtable _roles;
		private bool _authenticated;
		#endregion

		// Define public AuthenticationFailed event
		// Raised when the Authentication return false
		public event AuthenticateRolesEventHandler AuthenticationFailed;
	
		#region Get/Set Property
		[Bindable(true), 
			Category("Appearance"), 
			DefaultValue("")] 
		public string Text 
		{
			get{return text;}
			set{text = value;}
		}

		/// <summary>
		/// String represent Connection String to the database
		/// </summary>
		public string ConfigConnectionString
		{
			get{return _configconnectionstring;}
			set{_configconnectionstring = value;}
		}
		/// <summary>
		/// Custom allow roles list
		/// </summary>
		public string AllowRoles
		{
			get{return _allowroles;}
			set{_allowroles = value;}
		}

		/// <summary>
		/// Custom Deny roles list
		/// </summary>
		public string DenyRoles
		{
			get{return _denyroles;}
			set{_denyroles = value;}
		}

		/// <summary>
		/// Lowest User's Level can be accepted
		/// </summary>
		public int AllowLevel
		{
			get{return _allowlevel;}
			set{
				_allowlevel = value;
				if(_allowlevel < 0)
				{
					_allowlevel = 0;
				}
			}
		}
		#endregion

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output) 
		{
			// Check the value of _authenticated
			// If it is false, set our control's visible property to false.
			// If not, set the visible property to true
			if(_authenticated) 
			{ 
				base.Visible = true; 
				base.Render(output); 
			} 
			else 
			{ 
				base.Visible = false;
			} 
		}

		/// <summary>
		/// Fires right before Render
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) 
		{
            //base.OnPreRender(e);

			// Check to see if _authenticate = false then raise AuthenticationFailed event
			if(!_authenticated) 
			{ 
				OnAuthenticationFailed(); 
			} 
		}

		protected override void OnLoad(EventArgs e) 
		{
			// Check to see if our _configconnectionstring property is not set
			// throw an exception to inform that the _configconnectionstring is missing
			/*if(_configconnectionstring == null) 
			{ 
				throw new Exception("The ConfigConnectionString property was not set"); 
			}*/

			// Check to see if the current user has been authenticated
			// If it returns false, set _authenticated = false
			// and _authenticationtype = AuthenticationType.NotAuthenticated.
            //if(HttpContext.Current.Request.IsAuthenticated) 
            //{
            //    // Eetrieve a Hashtable of the current users roles
            //    //_roles = GetRoles();

            //    // Checks to see if our _roles Hashtable is null
            //    if(_roles == null)
            //    {
            //        // User does not have any roles defined in database
            //        _authenticated = false; 
            //        _authenticationtype = AuthenticationType.Unknown;
            //    } 
            //    else 
            //    { 
            //        _authenticated = ValidateRoles(); 
            //    } 
            //} 
            //else 
            //{
            //    // User has not been authenticated
            //    _authenticated = false; 
            //    _authenticationtype = AuthenticationType.NotAuthenicated; 
            //}
		}

        //private Hashtable GetRoles() 
        //{
        //    HttpContext context = HttpContext.Current; 
        //    Hashtable ht;

        //    // Check the current HttpContext to see if our Hashtable already exists
        //    ht = (Hashtable) context.Items["Roles"];
			
        //    // If it does already exist, return it from the context
        //    if(ht != null) 
        //    {
        //        return ht;
        //    } 
        //    else 
        //    {
        //        //// Retrieve the Hashtable
        //        //ht = AuthenticationHelper.GetRoles(/*ConfigConnectionString,*/context.User.Identity.Name);
        //        //// Add it to the current HttpContext
        //        //context.Items.Add("Roles",ht);
        //        //return ht;
        //    } 
        //}

		/// <summary>
		/// Validate and return if that user is allowed or denied
		/// </summary>
		/// <returns></returns>
		private bool ValidateRoles() 
		{
			// Check if the current user's level >= AllowLevel
			if(CheckLevel()) 
			{ 
				_authenticationtype = AuthenticationType.Level; 
				return true; 
			}
			/*// Check if any role in denyroles list has been found in our HashTable
			// So return false;
			if(CheckRoles(_denyroles)) 
			{ 
				_authenticationtype = AuthenticationType.Deny; 
				return false; 
			}*/
			// If not found then check one again with allow roles list
			_authenticationtype = AuthenticationType.Allow; 
			return CheckRoles(_allowroles);
		} 

		/// <summary>
		/// Retrieves the UserLevel value from roles Hashtable
		/// </summary>
		/// <returns></returns>
		private bool CheckLevel() 
		{ 
			int i = Convert.ToInt32(_roles["UserLevel"]); 
			return (i >= _allowlevel); 
		}

		/// <summary>
		/// Check if user has a role (from HashTable) in this roles string.
		/// </summary>
		/// <param name="roles">Roles string which we want to check</param>
		/// <returns>True if found any, otherwise false</returns>
		private bool CheckRoles(string roles) 
		{
			// Check to see role string is not null
			if(roles != null && roles.Trim().Length > 0) 
			{
				foreach (string role in roles.Split( new char[] {'|'} ))  
				{ 
					if(IsInRole(role)) 
					{ 
						return true; 
					} 
				} 
			} 
			return false; 
		} 

		/// <summary>
		/// Look for role in _roles HashTable
		/// </summary>
		/// <param name="role"></param>
		/// <returns>True if roles containts role</returns>
		private bool IsInRole(string role) 
		{         
			return _roles.ContainsKey(role); 
		}

		/// <summary>
		/// actually raise OnAuthenticationFailed event
		/// </summary>
		protected void OnAuthenticationFailed() 
		{ 
			if(AuthenticationFailed !=null)
			{
				AuthenticationFailed(this, new  
					AuthenticateRolesEventArgs(_authenticationtype,_roles));
			}
		}
	}

	/// <summary>
	/// Data of OnAuthenticationFailed event
	/// </summary>
	public class AuthenticateRolesEventArgs:System.EventArgs 
	{ 
		private AuthenticationType _authenticationtype;
		private Hashtable _roles;

		public AuthenticateRolesEventArgs(AuthenticationType authenticationtype, Hashtable roles) 
		{
			this._authenticationtype = authenticationtype; 
			this._roles = roles;
		}

		#region Get/Set Properties
		/// <summary>
		/// Return a string that user will be redirected back to the original page
		/// </summary>
		public string ReturnUrl
		{ 
			get 
			{ 
				return "ReturnUrl=" + 
					HttpContext.Current.Server.UrlEncode(
					HttpContext.Current.Request.RawUrl);
			} 
		}

		public Hashtable UserRoles
		{
			get
			{
				return _roles;
			}
		}
	

		public AuthenticationType AuthenticationType
		{
			get
			{
				return _authenticationtype;
			}
		}
		#endregion
	}

	/// <summary>
	/// Hold a value of what caused the authentication failure
	/// </summary>
	public enum AuthenticationType
	{
		Level = 0,			// Not Enough Level
		Allow = 1,			// Allow user
		Deny = 2,			// Denied access because user's particular roles were denied access
		NotAuthenicated = 3,// User has not logged in
		Unknown = 4			// Does not found roles or some other reasons
	}
}
