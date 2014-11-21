using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DFISYS.Ajax.WebLinks
{
    public partial class EditWebLink : System.Web.UI.Page
    {
        public int webLinkID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Kiểm tra Login hay chưa
            //if (String.IsNullOrWhiteSpace(Page.User.Identity.Name)) return;
            if (String.IsNullOrWhiteSpace(this.Page.User.Identity.Name)) return;
            var webLink = new DFISYS.BO.CoreBO.WebLink();

            //string action = Request.QueryString["action"] != null ? Request.QueryString["action"].ToString() : string.Empty;
            //int webLinkId = Request.QueryString["webLinkId"] != null ? Convert.ToInt32(Request.QueryString["webLinkId"]) : 0;

            //if (action.Equals("delete"))
            //{
            //    webLink = new DFISYS.BO.CoreBO.WebLink();
            //    webLink.WebLink_ID = webLinkId;
            //    webLink.Delete();
            //}
            //else
            //{
            //    webLink = new DFISYS.BO.CoreBO.WebLink();
            //    webLink.WebLink_ID = webLinkId;
            //    var webLinkObject = webLink.SelectOne();
            //    if (webLinkObject.WebLink_ID > 0)
            //    {
            //        webLink.Update();
            //    }

            //    else
            //    {
            //        webLink.Insert();
            //    }

            //}



            //Kiểm tra nếu Login và có quyền được duyệt, xóa comment thì sẽ được thực hiện


            if (Request.QueryString["webLinkId"] != null && Request.QueryString["action"] != null && Request.QueryString["action"].ToString().ToLower().Equals("delete"))
            {
                webLinkID = Convert.ToInt32(Request.QueryString["webLinkId"]);
                webLink = new DFISYS.BO.CoreBO.WebLink();
                webLink.WebLink_ID = webLinkID;
                webLink.Delete();
                return;
            }

            if (Request.QueryString["webLinkId"] != null && Request.QueryString["post"] == null)
            {
                webLinkID = Convert.ToInt32(Request.QueryString["webLinkId"]);

                btnSave.OnClientClick = String.Format("Save({0}, {1})", webLinkID, "false");
                btnDelete.OnClientClick = String.Format("return Delete({0})", webLinkID);

                webLink = new DFISYS.BO.CoreBO.WebLink();
                webLink.WebLink_ID = webLinkID;
                var cmdItem = webLink.SelectOne();
                if (cmdItem != null)
                {
                    txtContent.Text = cmdItem.WebLink_Description;
                    txtName.Text = cmdItem.WebLink_Name;
                    txtURL.Text = cmdItem.WebLink_URL;
                    txtSTT.Text = cmdItem.WebLink_Order.ToString();
                }
                return;
            }

            if (Request.Form.Count > 0)
            {
                webLinkID = Convert.ToInt32(Request.QueryString["webLinkId"]);

                webLink = new DFISYS.BO.CoreBO.WebLink();
                webLink.WebLink_ID = webLinkID;
                var cmdItem = webLink.SelectOne();
                webLink.WebLink_Description = Request.Form["txtContent"].ToString();
                webLink.WebLink_Name = Request.Form["txtName"].ToString();
                webLink.WebLink_URL = Request.Form["txtURL"].ToString();
                int stt=0;
                try 
	            {	        
		            stt = int.Parse(Request.Form["txtSTT"].ToString());
	            }
	            catch (Exception)
	            {
	            }
                webLink.WebLink_Order = stt;
                if (webLinkID > 0)
                {
                    webLink.Update();
                }
                else
                {
                    webLink.Insert();
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}