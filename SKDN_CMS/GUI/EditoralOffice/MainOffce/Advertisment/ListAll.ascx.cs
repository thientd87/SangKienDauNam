using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DFISYS.BO.CoreBO;
using DFISYS.BO.Editoral.Category;
using DFISYS.BO.Editoral.ProductColor;
using DFISYS.CoreBO.Common;

namespace AddIns.GUI.EditoralOffice.MainOffce.Advertisment
{
    public partial class ListAll : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            Adv_Position objPos = new Adv_Position();
            List<Adv_Position> _pos = objPos.SelectAllLike("");// AdvHelper.AdvGetAllPositions();
            if (_pos != null && _pos.Count > 0)
            {
                ddlPosition.DataSource = _pos;
                ddlPosition.DataBind();

                ddlPosition.Items.Insert(0, new ListItem("chon vi tri", "0"));
            }
            Adv_Page_Position objPage = new Adv_Page_Position();
            DataTable _pages = CategoryHelper.GetCategoriesByParent(0);//.SelectAllLike("");//.AdvGetAllPages();
            if (_pages != null && _pages.Rows.Count > 0)
            {
                ddlPages.DataSource = _pages;
                ddlPages.DataBind();

                ddlPages.Items.Insert(0, new ListItem("Trang chủ", "0"));
            }

            Advertisments obj = new Advertisments();
            List<Advertisments> dt = new List<Advertisments>();
            dt = obj.SelectAllLike("");
            if (dt != null && dt.Count > 0)
            {
                grdList.DataSource = dt;
                grdList.DataBind();
                grdList.Attributes.Add("ria-describedby", "sample_editable_1_info");
                grdList.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdList.HeaderRow.Attributes.Add("role", "row");
            }
            
        }
        protected void grdList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow editRow = grdList.Rows[e.RowIndex];
            Advertisments adv =  new Advertisments();
            adv.AdvID = Convert.ToInt32((editRow.FindControl("hiddenAdvID") as HiddenField).Value);
            adv.Delete();
           
            BindData();


        }
       

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Advertisments obj = new Advertisments();
            List<Advertisments> dt = new List<Advertisments>();
            int page = 0, pos = 0;
            page = function.Obj2Int(this.ddlPages.SelectedValue);
            pos = function.Obj2Int(this.ddlPosition.SelectedValue);
            dt = obj.SelectAllLikeByPagePosition("%" + this.txtKeyword.Text.Trim() + "%", page, pos);
            grdList.DataSource = dt;
            grdList.DataBind();
        }
    }
}