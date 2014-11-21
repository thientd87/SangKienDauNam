using System;
using System.Data;
using System.Web;
using System.Web.Security;
using DFISYS.User.Security;
using DFISYS.BO.Editoral.Menu;
using System.Xml;
using System.Text;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Menu {
    public partial class menu : System.Web.UI.UserControl {

        public string CurrentMenuItem = string.Empty;
        XmlDocument doc = null;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                getCurrentMenuItem();
                string userName = HttpContext.Current.User.Identity.Name;
                MainSecurity mainSecurity = new MainSecurity();
                DataTable tblPermissions = mainSecurity.GetPermissionAsTable(userName);
                bool isClear = Request.RawUrl == "/office.aspx" ? true : false;
                if (tblPermissions != null) {

                    doc = MenuCommon.getXML();
                    XmlNodeList boxes = doc.SelectNodes("boxes/box[@isvisible='true']");

                    bool isCheckPermisson = false, isCount = false;
                    XmlNodeList rows = null;
                    string a = string.Empty;
                    foreach (XmlNode box in boxes) {
                        StringBuilder html = new StringBuilder();
                        bool isPermision = false;
                        StringBuilder htmlHeader = new StringBuilder();
                        htmlHeader.AppendLine("<li class=\"\"><a href=\"javascript:;\"><i class=\"icon-folder-open\"></i><span class=\"title\">");
                        htmlHeader.AppendLine(box.SelectSingleNode("name").InnerText);
                        htmlHeader.AppendLine("</span><span class=\"arrow\"></span></a>");
                        htmlHeader.AppendLine("<ul class=\"sub-menu\">");
                        isCheckPermisson = bool.Parse(box.SelectSingleNode("@checkpermission").InnerText);
                        rows = box.SelectNodes("rows/row");
                        foreach (XmlNode row in rows) {
                            if (isCheckPermisson && tblPermissions.Select("Permission_ID=" + row.SelectSingleNode("Permission_ID").InnerText).Length == 1 || !isCheckPermisson) {
                                isPermision = true;
                                html.AppendLine("<li class=\"" + ((row.Attributes["isvisible"] != null) ? "hidden" : string.Empty) + " " + (row.SelectSingleNode("Cpmode").InnerText == CurrentMenuItem ? "active" : string.Empty) + "\">");
                                isCount = bool.Parse(row.SelectSingleNode("IsCount").InnerText);
                                a = "<a href=\"/office/{0}.aspx\">{1} {2}</a>";
                                if (isCount)
                                    a = string.Format(a, row.SelectSingleNode("Cpmode").InnerText, row.SelectSingleNode("MenuName").InnerText, "(<span>" + MenuCommon.getNewsCountStr(row.SelectSingleNode("Cpmode").InnerText, isClear) + "</span>)");
                                else
                                    a = string.Format(a, row.SelectSingleNode("Cpmode").InnerText, row.SelectSingleNode("MenuName").InnerText, string.Empty);

                                html.AppendLine(a);
                                html.AppendLine("</li>");
                            }
                        }
                        if (isPermision)  htmlHeader.Append(html.ToString());
                        htmlHeader.AppendLine("</ul></li>");
                        
                        if (isPermision) ltrHtml.Text += htmlHeader.ToString();
                    }
                }

                MainSecurity objSecurity = new MainSecurity();
                Role objRole = objSecurity.GetRole(Page.User.Identity.Name);

                if (Page.User.Identity.Name == "admin" || objRole.isAdministrator || objRole.isTongBienTap || objRole.isQuanTriKenh)
                {
                    aUser.Visible = true;
                  
                }
            }
        }

        private void getCurrentMenuItem() {
            string strMode = Request.QueryString["cpmode"];
            if (strMode.IndexOf(",") > 0) strMode = strMode.Substring(0, strMode.IndexOf(","));
            //else if (strMode.IndexOf("_") > 0) strMode = strMode.Split('_')[1];
               
            XmlDocument doc = MenuCommon.getXML();
            XmlNode ownerCpMode = (XmlNode)doc.SelectSingleNode("//Cpmode[text() ='" + strMode + "']");
            if (ownerCpMode != null)
            {
                CurrentMenuItem = strMode;
                var parentNode = ownerCpMode.ParentNode;
                if (parentNode != null && parentNode.Attributes["ownerCpMode"] !=null && !string.IsNullOrEmpty(parentNode.Attributes["ownerCpMode"].InnerText))
                    CurrentMenuItem = parentNode.Attributes["ownerCpMode"].InnerText;
            }
        
        }

        protected void itemLogOut_Click(object sender, EventArgs e) {
            HttpCookie cookie = Request.Cookies["PortalUser"];
            if (cookie != null) {
                cookie.Values["AC"] = "";
                //cookie.Values["PW"] = "";
                DateTime dt = DateTime.Now;
                dt.AddDays(-1);
                cookie.Expires = dt;
                Response.Cookies.Add(cookie);

            }
            if (Request.Cookies["pu"] != null) Response.Cookies.Remove("pu");

            FormsAuthentication.SignOut();
            Context.User = null;
            Response.Redirect("/Login.aspx");
        }

       
    }
}