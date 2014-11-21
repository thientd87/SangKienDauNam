using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DFISYS.Ajax.Comment
{
    public partial class EditComment : System.Web.UI.Page
    {
        public Int64 commentId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Kiểm tra nếu Login và có quyền được duyệt, xóa comment thì sẽ được thực hiện

            if (String.IsNullOrWhiteSpace(this.Page.User.Identity.Name)) return;

            if (Request.QueryString["commentId"] != null && Request.QueryString["action"] != null && Request.QueryString["action"].ToString().ToLower().Equals("delete"))
            {
                commentId = Convert.ToInt64(Request.QueryString["commentId"]);
                var comment = new DFISYS.BO.CoreBO.Comment(commentId);
                comment.Delete();
                return;
            }

            if (Request.QueryString["commentId"] != null && Request.QueryString["post"] == null)
            {
                commentId = Convert.ToInt64(Request.QueryString["commentId"]);

                btnSave.OnClientClick = String.Format("Save({0}, {1})", commentId , "false");
                btnSaveApprove.OnClientClick = String.Format("Save({0}, {1})", commentId, "true");
                btnDelete.OnClientClick = String.Format("return Delete({0})", commentId);
                 
                var comment = new DFISYS.BO.CoreBO.Comment(commentId);
                var cmdItem = comment.GetOne();
                if (cmdItem != null)
                {
                    if (cmdItem.Comment_Approved)
                        btnSaveApprove.Text = "Gỡ";
                    txtContent.Text = cmdItem.Comment_Content;
                    txtEmail.Text = cmdItem.Comment_Email;
                    txtUser.Text = cmdItem.Comment_User;
                }
                return;
            }

            if (Request.Form.Count > 0)
            {
                Int64 NewsId = Convert.ToInt64(Request.QueryString["commentId"]);
                
                var comment = new DFISYS.BO.CoreBO.Comment(NewsId);
                comment = comment.GetOne();
                comment.Comment_User = Request.Form["txtUser"].ToString();
                comment.Comment_Content = Request.Form["txtContent"].ToString();
                comment.Comment_Email = Request.Form["txtEmail"].ToString();
                if (Request.QueryString["status"] != null && Request.QueryString["status"].ToString().Equals("true"))
                {
                    comment.Comment_Approved = !comment.Comment_Approved;
                    comment.Approver = this.Page.User.Identity.Name.ToLower();
                    comment.Comment_Approved_Date = DateTime.Now;
                }
                comment.Update();
            }
        }
    }
}