using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral;
using DFISYS.BO.Editoral.Product;
using DFISYS.BO.Editoral.Product_Category;
using Telerik.WebControls;

namespace DFISYS.GUI.EditoralOffice.MainOffce.ProductManager
{
    public partial class LayoutListProduct : System.Web.UI.UserControl
    {
        ProductCategoryController proCateController = new ProductCategoryController();
        ProductController proController = new ProductController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<ProductCategory> dtChild = proCateController.GetCatChildren(1);
                if (dtChild != null && dtChild.Count > 0)
                {
                    cb_P_catID.DataSource = dtChild;
                    cb_P_catID.DataTextField = "Product_Category_Name";
                    cb_P_catID.DataValueField = "ID";
                    cb_P_catID.DataBind();
                    BindData(Convert.ToInt32(cb_P_catID.Items[0].Value));
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hiddenValue.Value))
            {
                int Cat_ID = Convert.ToInt32(cb_P_catID.SelectedValue);
                proCateController.DeleteCategoryLayout(Cat_ID);
                string[] arrGrid = hiddenValue.Value.TrimEnd('$').Split('$');
                foreach (string gridItem in arrGrid)
                {
                    proCateController.InsertCategoryLayout(Cat_ID, Convert.ToInt32(gridItem.Split('_')[0].Replace("container", "")), Convert.ToInt32(gridItem.Split('_')[1]));
                }
                this.Page.RegisterClientScriptBlock("successfull", "<script>alert(\"Cập nhật thành công!\")</script>");
                BindData(Cat_ID);

            }
            

        }

        protected void BindData(int Cat_ID)
        {
            DataTable dtGrid = proCateController.GetCategoryLayoutByCatID(Cat_ID);
            string selectedProduct = "";
            string grid = "";
            if (dtGrid != null && dtGrid.Rows.Count>0)
            {
                for (int i = 0; i < dtGrid.Rows.Count && i < 20; i++)
                {
                    grid += "<div class=\"gridItem\" id=\"container" + (i + 1) + "\">";
                    if (dtGrid.Rows[i]["ProductID"].ToString() != "0")
                    {
                        selectedProduct += dtGrid.Rows[i]["ProductID"] + ",";
                        grid += " <div class=\"productItem\" data-id=\"" + dtGrid.Rows[i]["ProductID"] + "\"><img src=\"/" + dtGrid.Rows[i]["ProductAvatar"] + "\" style=\"max-height: 60px; max-width: 60px\"/></div> ";
                    }
                    grid += "</div>";

                }
                if (dtGrid.Rows.Count < 20)
                {
                    for (int i = dtGrid.Rows.Count; i < 20; i++)
                    {
                        grid += "<div class=\"gridItem\" id=\"container" + (i + 1) + "\">";

                        grid += "</div>";

                    }
                }
                ltrGrid.Text = grid;
                selectedProduct = selectedProduct.TrimEnd(',');
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    grid += "<div class=\"gridItem\" id=\"container" + (i + 1) + "\">";
                    
                    grid += "</div>";

                }
                ltrGrid.Text = grid;
            }
            string where = "ProductCategory = " + Cat_ID;
            where += !string.IsNullOrEmpty(selectedProduct) ? " and ID not in(" + selectedProduct + ")" : string.Empty;
            List<Product> dtListProduct = proController.ProductsSelectDynamic(where, "");
            // if (dtListProduct != null && dtListProduct.Count > 0)
            {
                rptListProduct.DataSource = dtListProduct;
                rptListProduct.DataBind();
            }
        }
        protected void cb_P_catID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Cat_ID = Convert.ToInt32(cb_P_catID.SelectedValue);
            BindData(Cat_ID);
        }
    }
}