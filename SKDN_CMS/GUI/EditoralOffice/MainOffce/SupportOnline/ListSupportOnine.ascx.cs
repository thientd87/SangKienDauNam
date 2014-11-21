using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DFISYS.BO;

namespace DFISYS.GUI.EditoralOffice.MainOffce.SupportOnline
{
    public partial class ListSupportOnine : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                grdListSupport.Attributes.Add("ria-describedby", "sample_editable_1_info");
                grdListSupport.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdListSupport.HeaderRow.Attributes.Add("role", "row");

            }
        }
        private void BindData()
        {
            var tbl = SupportOnline_Helper.SelectSupportOnline();
            grdListSupport.DataSource = tbl;
            grdListSupport.DataBind();

        }
        protected void grvCategories_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddNew")
            {
                GridViewRow editRow = grdListSupport.FooterRow;

                SupportOnline_Helper.InsertSupportOnline((editRow.FindControl("txt_NewFullName") as HtmlInputText).Value,
                    (editRow.FindControl("txt_NewYahoo") as HtmlInputText).Value,
                    (editRow.FindControl("txt_NewSkype") as HtmlInputText).Value,
                    (editRow.FindControl("txt_NewMobile") as HtmlInputText).Value, "", 1);
                    
                grdListSupport.ShowFooter = false;
                BindData();
            }

        }
        protected void grvCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdListSupport.EditIndex = -1;
            BindData();
        }
        protected void grvCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow editRow = grdListSupport.Rows[grdListSupport.EditIndex];

            SupportOnline_Helper.UpdateSupportOnline(Convert.ToInt32((editRow.FindControl("hiddenColorID") as HiddenField).Value),
                (editRow.FindControl("txtFullName") as HtmlInputText).Value,
                    (editRow.FindControl("txtYahoo") as HtmlInputText).Value,
                    (editRow.FindControl("txtSkype") as HtmlInputText).Value,
                    (editRow.FindControl("txtMobile") as HtmlInputText).Value, "", 1);
               
            grdListSupport.EditIndex = -1;
            BindData();
        }

        protected void grvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow editRow = grdListSupport.Rows[e.RowIndex];
            SupportOnline_Helper.DeleteSupportOnline((editRow.FindControl("hiddenColorID") as HiddenField).Value);
            BindData();


        }

        protected void grvCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

            grdListSupport.EditIndex = -1;
            grdListSupport.SelectedIndex = -1;
            BindData();

        }



        protected void grvCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdListSupport.EditIndex = e.NewEditIndex;
            BindData();
        }
       
     
        
        protected void btnAddNewColor_Click(object sender, EventArgs e)
        {

            grdListSupport.ShowFooter = true;
            BindData();
        }
    }
}