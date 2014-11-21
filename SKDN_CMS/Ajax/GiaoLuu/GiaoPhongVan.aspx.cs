using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.CoreBO.Common;
using DFISYS.BO.CoreBO;
using System.Data;
using DFISYS.User.Db;
using DFISYS.User.Security;
namespace DFISYS.Ajax.GiaoLuu
{
    public partial class GiaoPhongVan : System.Web.UI.Page
    {
        public int sourseID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.Page.User.Identity.Name)) return;
            if (!IsPostBack)
            {
                DFISYS.GUI.Users.Business.User obj = new GUI.Users.Business.User();
                lsUserNew.DataTextField = "User_ID";
                lsUserNew.DataValueField = "User_ID";
                lsUserNew.DataSource = obj.GetAllUser(RoleConst.GiaoLuuTrucTuyen);
                lsUserNew.DataBind();
                sourseID = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["sourseID"]);
                btInsert.OnClientClick = string.Format("InsertItem({0})", sourseID);
            }



            if (Request.QueryString["sourseID"] != null && Request.QueryString["post"] == null)
            {
                sourseID = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["sourseID"]);
                ChannelResponse obj = new ChannelResponse();
                List<ChannelResponse> lsChannel = new List<ChannelResponse>();
                lsChannel = obj.SelectAllBySourseID(sourseID);
                this.grdListPhongVan.DataSource = lsChannel;
                this.grdListPhongVan.DataBind();
                return;
            }

            if (Request.QueryString["action"] == "deleteitem")
            {
                var objdel = new ChannelResponse();
                int iddel = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["id"]);
                objdel.ChannelResponse_ID = iddel;
                objdel.Delete();
            }
            if (Request.QueryString["action"] == "insert")
            {
                var objInsert = new ChannelResponse();
                String name = Request.QueryString["name"].ToString();
                String user = Request.QueryString["user"].ToString();
                bool active = DFISYS.CoreBO.Common.function.Obj2Boolean(Request.QueryString["active"]);
                sourseID = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["sourseID"]);

                objInsert.Sourse_ID = sourseID;
                objInsert.ChannelResponse_NameManager = name;
                objInsert.UserID = user;
                objInsert.IsActive = active;
                objInsert.Insert();
               
            }



            if (Request.Form.Count > 0)
            {
                int id = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["id"]);
                var obj = new ChannelResponse();
                if (Request.QueryString["action"] == "deleteitem")
                {
                    obj.ChannelResponse_ID = id;
                    obj.Delete();
                }

                String name = Request.QueryString["name"].ToString();
                String user = Request.QueryString["user"].ToString();
                bool active = DFISYS.CoreBO.Common.function.Obj2Boolean(Request.QueryString["active"]);
                if (id > 0)
                {
                    obj.ChannelResponse_ID = id;
                    obj = obj.SelectOne();
                    obj.ChannelResponse_NameManager = name;
                    obj.UserID = user;
                    obj.IsActive = active;
                    obj.Update();
                }
               
            }
        }
        protected void grdListPhongVan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ChannelResponse data = e.Row.DataItem as ChannelResponse;
                DropDownList cboUser = e.Row.FindControl("lsUser") as DropDownList;
                DFISYS.GUI.Users.Business.User obj = new GUI.Users.Business.User();
                cboUser.DataTextField = "User_ID";
                cboUser.DataValueField = "User_ID";
                cboUser.DataSource = obj.GetAllUser(RoleConst.GiaoLuuTrucTuyen);
                cboUser.DataBind();

                var item = cboUser.Items.FindByValue(data.UserID);//.Selected = true;
                if (item != null)
                    item.Selected = true;

            }
        }

    }
       

}