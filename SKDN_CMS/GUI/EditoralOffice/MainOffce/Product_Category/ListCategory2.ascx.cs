using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral;
using DFISYS.BO.Editoral.ProductColor;
using DFISYS.BO.Editoral.Product_Category;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Product_Category
{
    public partial class ListCategory2 : System.Web.UI.UserControl
    {
        ProductColorController pcc = new ProductColorController();

        ProductCategoryController ch = new ProductCategoryController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                grvColor.Attributes.Add("ria-describedby", "sample_editable_1_info");

                grvColor.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvColor.HeaderRow.Attributes.Add("role", "row");

            }
        }
        private void BindData()
        {
            List<ProductCategory> dtParent = ch.GetCatParent();
         
            if (dtParent!=null && dtParent.Any())
            {
                grvColor.DataSource = dtParent;
                grvColor.DataBind();
               
            }
            else
            {
                dtParent.Add(new ProductCategory(0, string.Empty, string.Empty, string.Empty, string.Empty, 0, false, 0, string.Empty, 0));
                grvColor.DataSource = dtParent;
                grvColor.DataBind();
                grvColor.Rows[0].Visible = false;
            }
        }
        protected void grvCategories_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddNew")
            {
                GridViewRow editRow = grvColor.FooterRow;
                ProductCategory newProductCategory = new ProductCategory(
                     0,
                     (editRow.FindControl("txt_New_Product_Category_Name") as HtmlInputText).Value,
                     (editRow.FindControl("txt_New_Product_Category_Name") as HtmlInputText).Value,
                     (editRow.FindControl("txt_New_Product_Category_Desc") as TextBox).Text,
                     (editRow.FindControl("txt_New_Product_Category_Desc") as TextBox).Text, 0,
                     (editRow.FindControl("chkNewIsHidden") as CheckBox).Checked, 1,
                     (editRow.FindControl("txt_New_Product_Category_Image") as HtmlInputText).Value, 0
                     );

                ch.InsertCategory(newProductCategory);
                
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
            ProductCategory newProductCategory = new ProductCategory(
                Convert.ToInt32((editRow.FindControl("hiddenColorID") as HiddenField).Value),
                (editRow.FindControl("txt_Product_Category_Name") as HtmlInputText).Value,
                (editRow.FindControl("txt_Product_Category_Name") as HtmlInputText).Value,
                (editRow.FindControl("txt_Product_Category_Desc") as TextBox).Text,
                (editRow.FindControl("txt_Product_Category_Desc") as TextBox).Text, 0,
                (editRow.FindControl("chkIsHidden") as CheckBox).Checked, 1,
                (editRow.FindControl("txt_Product_Category_Image") as HtmlInputText).Value, 0
                );

            ch.UpdateCategory(newProductCategory);
          
            grvColor.EditIndex = -1;
            BindData();
        }

        protected void grvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow editRow = grvColor.Rows[e.RowIndex];
            ch.DeleteCategory(Convert.ToInt32((editRow.FindControl("hiddenColorID") as HiddenField).Value));
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