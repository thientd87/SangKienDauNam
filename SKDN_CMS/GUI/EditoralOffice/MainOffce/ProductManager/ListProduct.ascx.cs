using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral;
using DFISYS.BO.Editoral.Product;
using DFISYS.BO.Editoral.ProductColor;
using DFISYS.BO.Editoral.Product_Category;

namespace DFISYS.GUI.EditoralOffice.MainOffce.ProductManager
{
    public partial class ListProduct : System.Web.UI.UserControl
    {
        ProductController pcc = new ProductController();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindData();
                grvProduct.Attributes.Add("ria-describedby", "sample_editable_1_info");
                grvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvProduct.HeaderRow.Attributes.Add("role", "row");

            }
        }
        private void BindData()
        {
            List<Product> listColor = pcc.ProductsSelectAll();
            grvProduct.DataSource = listColor;
            grvProduct.DataBind();

        }
        protected void grvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvProduct.PageIndex = e.NewPageIndex;
            BindData();
        }
        protected void repFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ChangePage")
            {
                grvProduct.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
                BindData();
            }
        }

        protected void grvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow editRow = grvProduct.Rows[e.RowIndex];
            pcc.DelteProduct(Convert.ToInt32((editRow.FindControl("hiddenColorID") as HiddenField).Value));
            BindData();


        }
        protected void grvProduct_DataBound(object sender, EventArgs e)
        {

            GridViewRow pagerRow = grvProduct.BottomPagerRow;

            if (pagerRow != null)
            {

                Repeater repFooter = (Repeater)pagerRow.Cells[0].FindControl("repFooter");

                List<int> pages = new List<int>();

                for (int i = 0; i < grvProduct.PageCount; i++)
                {
                    pages.Add(i + 1);
                }

                repFooter.DataSource = pages;
                repFooter.DataBind();
            }
        }
        protected void GrdListNewsRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
               
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var a = (Product)e.Row.DataItem;
                var ltrColor = (Literal)e.Row.FindControl("ltrColor");
                var ltrCategory = (Literal)e.Row.FindControl("ltrCategory");
                var ltrLinkWeb = (Literal)e.Row.FindControl("ltrLinkWeb");
                

                if(a.ProductColor>0)
                {
                    ProductColorObject pColor = (new ProductColorController()).SelectProductColor(a.ProductColor);
                    if(pColor!=null)
                    ltrColor.Text = "<div style=\"float:right;width: 15px; height: 15px; display: inline-block; background:" + pColor.ColorCode + " \"></div>";
                }
                if(a.ProductCategory>0)
                {
                    ProductCategory pCategory = (new ProductCategoryController()).GetCategoryByCatID(a.ProductCategory);
                    if (pCategory != null)
                    ltrCategory.Text = pCategory.Product_Category_Name;
                    int catParentID = 0;
                    if (pCategory.Product_Category_CatParent_ID != 0)
                    {
                        catParentID =
                            (new ProductCategoryController()).GetCategoryByCatID(pCategory.Product_Category_CatParent_ID)
                                .ID;
                    }

                    string channelName = System.Configuration.ConfigurationManager.AppSettings["HomeLink"].TrimEnd('/') + "/products";

                    string url = "<a href=\"" + String.Format(channelName + "/{0}p{1}c{2}/{3}.htm", a.Id, catParentID, a.ProductCategory, StrProcess.UnicodeToKoDauAndGach(HttpUtility.HtmlDecode(a.ProductName))) + "\"  class=\"btn mini yellow\" target=\"_blank\">Preview</a>";
                    ltrLinkWeb.Text = url;
                }

            }
        }
    }
}