using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO;

namespace DFISYS.Ajax.Tool
{
    public partial class EditSupportOnline : System.Web.UI.Page
    {
        public string ID = string.Empty;
        public string FullName = string.Empty;
        public string Yahoo = string.Empty;
        public string Skype = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Kiểm tra Login hay chưa
            //if (String.IsNullOrWhiteSpace(Page.User.Identity.Name)) return;
            if (String.IsNullOrWhiteSpace(this.Page.User.Identity.Name)) return;

            //Kiểm tra nếu Login và có quyền được duyệt, xóa comment thì sẽ được thực hiện

            if (Request.QueryString["Id"] != null && Request.QueryString["action"] != null && Request.QueryString["action"].ToString().ToLower().Equals("delete"))
            {
                ID = Request.QueryString["Id"];
                SupportOnline_Helper.DeleteSupportOnline(ID);
                return;
            }

            if (Request.QueryString["Id"] != null && Request.QueryString["post"] == null)
            {
                ID = Request.QueryString["Id"];

                btnSave.OnClientClick = String.Format("Save({0}, {1})", ID, "false");
                btnDelete.OnClientClick = String.Format("return Delete({0})", ID);

                var cmdItem = SupportOnline_Helper.SelectOneSupportOnline(ID);
                if (cmdItem != null &&cmdItem.Rows.Count>0)
                {
                    txtName.Text = cmdItem.Rows[0]["FullName"].ToString();
                    txtYahoo.Text = cmdItem.Rows[0]["Yahoo"].ToString();
                    txtSkype.Text = cmdItem.Rows[0]["Skype"].ToString();
                    txtSTT.Text = cmdItem.Rows[0]["STT"].ToString();
                    txtMobile.Text = cmdItem.Rows[0]["Mobile"].ToString();
                    txtGroupName.Text = cmdItem.Rows[0]["GroupName"].ToString();

                }
                return;
            }

            if (Request.Form.Count > 0)
            {
                int Id = Convert.ToInt32(Request.QueryString["Id"]);
                //int stt=0;
                //try 
                //{	        
                //    stt = int.Parse(Request.Form["txtSTT"]);
                //}
                //catch (Exception)
                //{
                //}
                if (Id !=0)
                {
                    SupportOnline_Helper.UpdateSupportOnline(Id, Request.Form["txtName"], Request.Form["txtYahoo"], Request.Form["txtSkype"], Request.Form["txtMobile"], Request.Form["txtGroupName"], Convert.ToInt32(Request.Form["txtSTT"]));
                }
                else
                {
                    SupportOnline_Helper.InsertSupportOnline(Request.Form["txtName"], Request.Form["txtYahoo"], Request.Form["txtSkype"],Request.Form["txtMobile"], Request.Form["txtGroupName"], Convert.ToInt32(Request.Form["txtSTT"]));
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //int Id = Convert.ToInt32(Request.QueryString["Id"]);
            //if(Id!=0)
            //SupportOnline_Helper.UpdateSupportOnline(Id, txtName.Text, txtYahoo.Text, txtSkype.Text);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}