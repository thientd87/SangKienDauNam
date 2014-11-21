using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral;
using DFISYS.BO.Editoral.Category;
using DFISYS.BO.Editoral.Product_Category;
using DFISYS.Core.DAL;
using Telerik.WebControls;

namespace DFISYS.GUI.EditoralOffice.MainOffce.NewsCategoryManager
{
    public partial class ListNewsCategory : System.Web.UI.UserControl
    {
        ProductCategoryController proCateController = new ProductCategoryController();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTreeView();
                CategoryHelper.bindAllCat(cb_P_catID);
            }
        }

        protected void BindTreeView()
        {
            rtvCatList.Nodes.Clear();
           // ProductCategoryController ch = new ProductCategoryController();
            //ProductHelper ph = new ProductHelper();
            //List<ProductCategory> dtParent = ch.GetCatParent();
            DataTable dtParent = CategoryHelper.GetCategoriesByParent(0);
            if (dtParent != null && dtParent.Rows.Count > 0)
            {
                foreach (DataRow dr in dtParent.Rows)
                {
                    RadTreeNode root = new RadTreeNode();
                    root.Text = "<span style=\"font-weight:bold;font-size:14px;font-family:'Open Sans', sans-serif\">" + dr["Cat_Name"] + "</span>";
                    root.Value = dr["Cat_ID"].ToString();
                    DataTable dtChild = CategoryHelper.GetCategoriesByParent(Convert.ToInt32(dr["Cat_ID"].ToString()));
                    if (dtChild != null && dtChild.Rows.Count > 0)
                    {
                        foreach (DataRow drChild in dtChild.Rows)
                        {
                            // string itemCount = " (<span style='color:red; font-weight:bold'>" + ph.GetProductCountByCatID(drChild["Cat_ID"].ToString()) + "</span>)";
                            string itemCount = "";
                            RadTreeNode child = new RadTreeNode();
                            child.Text = "<span style=\"font-size:12px;font-family:'Open Sans', sans-serif\">" + drChild["Cat_Name"] +
                                         "</span>" + itemCount;
                            child.Value = drChild["Cat_ID"].ToString();
                            root.Nodes.Add(child);
                        }
                    }

                    rtvCatList.Nodes.Add(root);

                }
                rtvCatList.ExpandAllNodes();
            }
        }

        protected void BindingData(int CatId)
        {
            ProductCategoryController ch = new ProductCategoryController();
            DataTable dt = CategoryHelper.GetCategoryByID(CatId);
            if (dt != null)
            {
                txt_cat_name.Value = dt.Rows[0]["Cat_Name"].ToString();
                txt_cat_name_en.Value =  dt.Rows[0]["Cat_Name_En"].ToString();
                txt_cat_desc.Value = dt.Rows[0]["Cat_Description"].ToString();
                txt_cat_desc_en.Value = dt.Rows[0]["Cat_Description_En"].ToString();
                isActive.Checked = !Convert.ToBoolean(dt.Rows[0]["Cat_IsHidden"]);
                txt_Order.Value = dt.Rows[0]["Cat_Order"].ToString();
               // txtSelectedFile.Value = dt.Product_Category_Image;
              //  if (!string.IsNullOrEmpty(dt.Product_Category_Image))
              //  {
               //     img.Visible = true;
              //      img.ImageUrl = "/" + dt.Product_Category_Image;

               // }
               // else
                //{
                    //img.Visible = false;
                //}
                //hdfImage.Value = dt.Product_Category_Image;
               if (dt.Rows[0]["Cat_ParentID"].ToString() == "0")
                    cbIsParent.Checked = true;
                cb_P_catID.SelectedValue = dt.Rows[0]["Cat_ParentID"].ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CategoryHelper.CreateCat2(txt_cat_name.Value, txt_cat_name_en.Value, string.Empty, Convert.ToInt32(txt_Order.Value), txt_cat_desc.Value, txt_cat_desc_en.Value, string.Empty, Convert.ToInt32(cb_P_catID.SelectedValue), 1, string.Empty, false, !isActive.Checked, string.Empty, true);
         
            Response.Redirect(Request.RawUrl);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            CategoryHelper.UpdateCate(Convert.ToInt32(rtvCatList.SelectedNode.Value), txt_cat_name.Value, txt_cat_name_en.Value, txt_cat_desc.Value, txt_cat_desc_en.Value, string.Empty, Convert.ToInt32(cb_P_catID.SelectedValue), Convert.ToInt32(txt_Order.Value), 1, string.Empty, false, !isActive.Checked, true);
            Response.Redirect(Request.RawUrl);
           
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            img.Visible = false;
            btnUpdate.Visible = false;
        }



        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            
            CategoryHelper.Delete(Convert.ToInt32(rtvCatList.SelectedNode.Value));
            Response.Redirect(Request.RawUrl);
        }

        protected void cbIsParent_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsParent.Checked == true)
                trLSP.Visible = false;
            else
                trLSP.Visible = true;
        }

        protected void rtvCatList_NodeClick(object o, RadTreeNodeEventArgs e)
        {
            tblEdit.Visible = true;
            BindingData(Convert.ToInt32(rtvCatList.SelectedNode.Value));
            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }
    }
}