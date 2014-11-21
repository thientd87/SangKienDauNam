using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace Portal
{
	/// <summary>
	/// Summary description for AuthenticationHelper.
	/// </summary>
	public class AuthenticationHelper
	{
		public AuthenticationHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// Check to see if user has an identity or not
		/// </summary>
		/// <param name="ConfigConnectionString">String represent connection string to the database</param>
		/// <param name="UserName">ID of User</param>
		/// <param name="Password">User's password</param>
		/// <returns>Boolean value determine authentication success or fail</returns>
		public static bool UserLogin(string ConfigConnectionString, string  UserName, string Password) 
		{
			int intCount;
			SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings 
				[ConfigConnectionString]);
			SqlCommand myCommand = new SqlCommand();

			// Build Query to get Info of User
			string strSQL = "Select Count(*) ";
			strSQL += "From [User] Where [User].[UserName] = @UserName and [User].[Password] = @Password And [User].[Lock] <> 1";

			myCommand.Connection = myConnection;
			myCommand.CommandText = strSQL;

			// Create UserName and Password parameters
			SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100); 
			parameterUserName.Value = UserName; 
			myCommand.Parameters.Add(parameterUserName); 

			SqlParameter parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 40); 
			parameterPassword.Value = Password; 
			myCommand.Parameters.Add(parameterPassword);

			myConnection.Open();
			intCount = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
			myConnection.Close();
			return  Convert.ToBoolean(intCount);
		}

		/// <summary>
		/// Get all roles assigned to this user
		/// </summary>
		/// <param name="ConfigConnectionString">String represent connection string to the database</param>
		/// <param name="UserName">ID of user</param>
		/// <returns>A HashTable containts user's roles</returns>
//        public static Hashtable GetRoles(/*string ConfigConnectionString,*/ string UserName)
//        {
////			int intUserLevel;
////			SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings 
////				[ConfigConnectionString]); 
////			SqlCommand myCommand = new SqlCommand();
////			// Get UserLevel from User Table
////			string strSQL = "Select [User].[UserLevelID] From [User] where [User].UserName = @UserName";
////			myCommand.Connection = myConnection;
////			myCommand.CommandText = strSQL;
////
////			SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100); 
////			parameterUserName.Value = UserName; 
////			myCommand.Parameters.Add(parameterUserName);
////			myConnection.Open();
////			intUserLevel = (int)myCommand.ExecuteScalar();
////			
////			strSQL = "Select r.[RoleName], r.[RoleDesc] From [Role] r ";
////			strSQL += "Inner Join Granted ur on (r.RoleID = ur.RoleID) ";
////			strSQL += "Where ur.UserName = @Username Order By ur.RoleID";
////
////			// Clear the parameter and reassign the commandtext of myCommand
////			myCommand.CommandText = strSQL;
////			myCommand.Parameters.Clear();
////			SqlParameter parameterUserName2 = new SqlParameter("@UserName", SqlDbType.NVarChar, 100); 
////			parameterUserName2.Value = UserName;
////			myCommand.Parameters.Add(parameterUserName2);
////			
////			SqlDataReader roles = myCommand.ExecuteReader();
////			Hashtable ht = new Hashtable();
////			// Using SQlDataReader to get Roles list of given username
////			while(roles.Read())
////			{
////				ht.Add(roles["RoleName"].ToString(),roles["RoleDesc"].ToString());
////			} 
////			roles.NextResult();
////			// Add UserLevel into roles HashTable
////			ht.Add("UserLevel", intUserLevel);
////			roles.Close();
////			myConnection.Close();
//            //Hashtable ht = new Hashtable();
//            //string [] roles=Portal.UserManagement.GetRoles(UserName);
//            //for (int i=0; i<roles.Length;i++)
//            //{
//            //    ht.Add(roles[i],roles[i]);
//            //}
//            //return  ht;     
//        }
	}
}
