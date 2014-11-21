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

namespace Portal.GUI.Administrator.GenerateTabs
{
    public partial class EditionTypeList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvData.EditIndex = -1;
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "NewRow".ToLower())
            {
                GridViewRow grdrow = this.gvData.FooterRow;
                if (grdrow != null)
                {
                    TextBox txtEditionName = grdrow.FindControl("txtEditionName") as TextBox;
                    TextBox txtEditionDes = grdrow.FindControl("txtEditionDes") as TextBox;
                    TextBox txtEditionDisplayURL = grdrow.FindControl("txtEditionDisplayURL") as TextBox;
                    if (txtEditionName != null)
                    {
                        if (!txtEditionName.Text.Trim().Equals(""))
                        {
                            odsData.InsertParameters[0].DefaultValue = txtEditionName.Text;
                            odsData.InsertParameters[1].DefaultValue = txtEditionDes.Text;
                            odsData.InsertParameters[2].DefaultValue = txtEditionDisplayURL.Text;
                            odsData.Insert();
                        }

                        odsData.Select();
                        gvData.DataBind();
                    }
                }
            }
        }

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow grdrow = this.gvData.Rows[e.RowIndex];
            if (grdrow != null)
            {
                ImageButton btnSave = grdrow.FindControl("imgSave") as ImageButton;                
                if (btnSave != null)
                {
                    odsData.DeleteParameters[0].DefaultValue = btnSave.CommandArgument;
                    odsData.Delete();

                    odsData.Select();
                    gvData.DataBind();
                }
            }
        }

        protected void gvData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvData.EditIndex = e.NewEditIndex;
        }

        protected void gvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow grdrow = this.gvData.Rows[e.RowIndex];
            if (grdrow != null)
            {
                TextBox txtEditEditionName = grdrow.FindControl("txtEditEditionName") as TextBox;
                TextBox txtEditEditionDes = grdrow.FindControl("txtEditEditionDes") as TextBox;
                TextBox txtEditEditionDisplayURL = grdrow.FindControl("txtEditEditionDisplayURL") as TextBox;
                ImageButton btnSave = grdrow.FindControl("imgSave") as ImageButton;
                if (btnSave != null)
                {
                    if (!txtEditEditionName.Text.Trim().Equals(""))
                    {
                        odsData.UpdateParameters[2].DefaultValue = btnSave.CommandArgument;
                        odsData.UpdateParameters[0].DefaultValue = txtEditEditionName.Text;
                        odsData.UpdateParameters[1].DefaultValue = txtEditEditionDes.Text;
                        odsData.UpdateParameters[3].DefaultValue = txtEditEditionDisplayURL.Text;
                        odsData.Update();
                    }
                }
                odsData.Select();
                gvData.DataBind();
            }

        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer)
            {
                if (DataBinder.Eval(e.Row.DataItem, "EditionType_ID").ToString() == "0")
                {
                    e.Row.Cells.RemoveAt(3);
                    e.Row.Cells.RemoveAt(2);
                    e.Row.Cells[1].Attributes.Add("colspan", "4");
                    e.Row.Cells[1].Attributes.Add("align", "center");
                }
            }
        }


    }
}