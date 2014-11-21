using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral.Category;
using DFISYS.BO.Editoral.ProductColor;
using DFISYS.Core.DAL;

namespace DFISYS.GUI.EditoralOffice.MainOffce.ProductColor
{
    public partial class List : System.Web.UI.UserControl
    {
        ProductColorController pcc = new ProductColorController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindData();
                grvColor.Attributes.Add("ria-describedby", "sample_editable_1_info");
                
                grvColor.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvColor.HeaderRow.Attributes.Add("role", "row");

            }
        }
        private void BindData()
        {
            List<ProductColorObject> listColor = pcc.SellectAllProductColor();
            grvColor.DataSource = listColor;
            grvColor.DataBind();
            
        }
        protected void grvCategories_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddNew")
            {
                GridViewRow editRow = grvColor.FooterRow;
          
                pcc.UpdateProductColor(new ProductColorObject()
                {
                    Id = 0,
                    ColorCode = (editRow.FindControl("txt_NewColorCode") as HtmlInputText).Value,
                    ColorName = (editRow.FindControl("txt_NewColorName") as HtmlInputText).Value,
                    IsActive = (editRow.FindControl("chkNewIsHidden") as CheckBox).Checked
                });
                grvColor.ShowFooter = false;
                BindData();
            }
           
        }
        protected void grvCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvColor.EditIndex = -1;
            BindData();
        }
        protected void grvCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow editRow = grvColor.Rows[grvColor.EditIndex];
          
            pcc.UpdateProductColor(new ProductColorObject()
            {
                Id = Convert.ToInt32((editRow.FindControl("hiddenColorID") as HiddenField).Value),
                ColorCode = (editRow.FindControl("txt_ColorCode") as HtmlInputText).Value, 
                ColorName = (editRow.FindControl("txt_ColorName") as HtmlInputText).Value, 
                IsActive = (editRow.FindControl("chkIsHidden") as CheckBox).Checked 
            });
            grvColor.EditIndex = -1;
            BindData();
        }

        protected void grvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow editRow = grvColor.Rows[e.RowIndex];
            pcc.DeleteProductColor(new ProductColorObject() { Id = Convert.ToInt32((editRow.FindControl("hiddenColorID") as HiddenField).Value) });
            BindData();
            
           
        }

        protected void grvCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            grvColor.EditIndex = -1;
            grvColor.SelectedIndex = -1;
            BindData();
           
        }

       

        protected void grvCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvColor.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void btnAddNewColor_Click(object sender, EventArgs e)
        {
            
            grvColor.ShowFooter = true;
            BindData();
        }
    }
}