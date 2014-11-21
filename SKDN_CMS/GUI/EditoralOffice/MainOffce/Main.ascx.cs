using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using DFISYS.User.Security;
using System.Security;
using DFISYS.BO.Editoral.Menu;
namespace DFISYS.GUI.EditoralOffice.MainOffce
{
	public partial class Main : DFISYS.API.Module
	{
		protected void Page_Load(object sender, EventArgs e)
		{
              if (Request.QueryString["cpmode"] != null)
                {
                    string strMode = Request.QueryString["cpmode"];
                    if (strMode.IndexOf(",") > 0) strMode = strMode.Substring(0, strMode.IndexOf(","));

                    Control ctrNewslist = null;

                    // user cpMode to get path of usercontrol from xml config
                    XmlDocument doc = MenuCommon.getXML();
                    XmlNode node = doc.SelectSingleNode("//row[Cpmode='" + strMode + "']");
                    if (node != null)
                    {
                        XmlNode path = node.SelectSingleNode("path");
                        bool ischeckpermission = bool.Parse(node.SelectSingleNode("../../@checkpermission").InnerText);

                        if (path != null)
                        {
                            if (ischeckpermission)
                            {
                                XmlNode permissionId = node.SelectSingleNode("Permission_ID");
                                // check permission in database
                                MainSecurity mainSecurity = new MainSecurity();
                                DataTable tblPermissions = mainSecurity.GetPermissionAsTable(HttpContext.Current.User.Identity.Name);
                                if (tblPermissions.Select("Permission_ID=" + permissionId.InnerText).Length == 1)
                                {
                                    try
                                    {
                                        ctrNewslist = LoadControl(path.InnerText);
                                        plcMain.Controls.Add(ctrNewslist);
                                    }
                                    catch (SecurityException sEx)
                                    {
                                        plcMain.Controls.Clear();
                                        ctrNewslist = LoadControl("Statistic/MsgPermission.ascx");
                                        plcMain.Controls.Add(ctrNewslist);
                                    }

                                }
                                else
                                {
                                    ctrNewslist = LoadControl("Statistic/MsgPermission.ascx");
                                    plcMain.Controls.Add(ctrNewslist);
                                }
                            }
                            else
                            {
                                try
                                {
                                    ctrNewslist = LoadControl(path.InnerText);
                                    plcMain.Controls.Add(ctrNewslist);
                                }
                                catch { }
                            }
                        }
                    }
                    if (ctrNewslist == null)
                    {
                        ctrNewslist = LoadControl("OnLoad/onload.ascx");
                        plcMain.Controls.Add(ctrNewslist);
                    }
                }
            
			
		}
	}
}