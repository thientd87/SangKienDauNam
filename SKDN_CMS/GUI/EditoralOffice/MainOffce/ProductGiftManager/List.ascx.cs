using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral.ProductColor;
using DFISYS.BO.Editoral.ProductGift;

namespace DFISYS.GUI.EditoralOffice.MainOffce.ProductGiftManager
{
    public partial class List : System.Web.UI.UserControl
    {
        ProductGiftController pcc = new ProductGiftController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                grvGiftType.Attributes.Add("ria-describedby", "sample_editable_1_info");

                grvGiftType.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvGiftType.HeaderRow.Attributes.Add("role", "row");

            }
        }
        private void BindData()
        {
            List<ProductGiftObject> listColor = pcc.SellectAllProductGift();
            grvGiftType.DataSource = listColor;
            grvGiftType.DataBind();

        }
        protected void grvCategories_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddNew")
            {
                GridViewRow editRow = grvGiftType.FooterRow;

                pcc.UpdateProductGift(new ProductGiftObject()
                {
                    Id = 0,
                    ProductGift = (editRow.FindControl("txt_NewProductGift") as HtmlInputText).Value,
                    Order = Convert.ToInt32((editRow.FindControl("txt_NewOrder") as HtmlInputText).Value),
                    IsActive = (editRow.FindControl("chkNewIsHidden") as CheckBox).Checked
                });
                grvGiftType.ShowFooter = false;
                BindData();
            }

        }
        protected void grvCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvGiftType.EditIndex = -1;
            BindData();
        }
        protected void grvCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow editRow = grvGiftType.Rows[grvGiftType.EditIndex];

            pcc.UpdateProductGift(new ProductGiftObject()
            {
                Id = Convert.ToInt32((editRow.FindControl("hiddenColorID") as HiddenField).Value),
                ProductGift = (editRow.FindControl("txt_ProductGift") as HtmlInputText).Value,
                Order = Convert.ToInt32((editRow.FindControl("txt_Order") as HtmlInputText).Value),
                IsActive = (editRow.FindControl("chkIsHidden") as CheckBox).Checked
            });
            grvGiftType.EditIndex = -1;
            BindData();
        }

        protected void grvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow editRow = grvGiftType.Rows[e.RowIndex];
            pcc.DeleteProductGift(new ProductGiftObject() { Id = Convert.ToInt32((editRow.FindControl("hiddenColorID") as HiddenField).Value) });
            BindData();


        }

        protected void grvCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

            grvGiftType.EditIndex = -1;
            grvGiftType.SelectedIndex = -1;
            BindData();

        }



        protected void grvCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvGiftType.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void btnAddNewColor_Click(object sender, EventArgs e)
        {

            grvGiftType.ShowFooter = true;
            BindData();
        }
    }
}