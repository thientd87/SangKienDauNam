using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.BO.Editoral;
using DFISYS.BO.Editoral.Category;
using DFISYS.BO.Editoral.Product_Category;
using DFISYS.CoreBO.Common;
using Telerik.WebControls;

namespace MobileShop.GUI.Back_End.Categories
{
    public partial class ListCategories : System.Web.UI.UserControl
    {
        ProductCategoryController proCateController = new ProductCategoryController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindTreeView();
                ProductCategoryController ch = new ProductCategoryController();
                ch.BuildTreeCat(cb_P_catID);
            }
        }

        protected void BindTreeView()
        {
            rtvCatList.Nodes.Clear();
            ProductCategoryController ch = new ProductCategoryController();
            //ProductHelper ph = new ProductHelper();
            List<ProductCategory> dtParent = ch.GetCatParent();
            if(dtParent!=null&&dtParent.Count>0)
            {
                foreach (ProductCategory dr in dtParent)
                {
                    RadTreeNode root = new RadTreeNode();
                    root.Text = "<span style=\"font-weight:bold;font-size:14px;font-family:'Open Sans', sans-serif\">" + dr.Product_Category_Name + "</span>";
                    root.Value = dr.ID.ToString();
                    List<ProductCategory> dtChild = ch.GetCatChildren(dr.ID);
                    if(dtChild!=null&&dtChild.Count>0)
                    {
                        foreach (ProductCategory drChild in dtChild)
                        {
                           // string itemCount = " (<span style='color:red; font-weight:bold'>" + ph.GetProductCountByCatID(drChild["Cat_ID"].ToString()) + "</span>)";
                            string itemCount = "";
                            RadTreeNode child = new RadTreeNode();
                            child.Text = "<span style=\"font-size:12px;font-family:'Open Sans', sans-serif\">" + drChild.Product_Category_Name +
                                         "</span>" + itemCount;
                            child.Value = drChild.ID.ToString();
                            List<ProductCategory> dtChildLevel3 = ch.GetCatChildren(drChild.ID);
                            if (dtChildLevel3 != null && dtChildLevel3.Count > 0)
                            {
                                foreach (ProductCategory drChildLevel3 in dtChildLevel3)
                                {
                                    string itemCountLevel3 = "";
                                    RadTreeNode childLevel3 = new RadTreeNode();
                                    childLevel3.Text = "<span style=\"font-size:12px;font-family:'Open Sans', sans-serif\">" + drChildLevel3.Product_Category_Name +
                                                 "</span>" + itemCountLevel3;
                                    childLevel3.Value = drChildLevel3.ID.ToString();
                                    child.Nodes.Add(childLevel3);
                                }
                                
                            }
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
            ProductCategory dt = ch.GetCategoryByCatID(CatId);
            if(dt!=null)
            {
                txt_cat_name.Value = dt.Product_Category_Name;
                txt_cat_name_en.Value = dt.Product_Category_Name_En;
                txt_cat_desc.Value = dt.Product_Category_Desc;
                txt_cat_desc_en.Value = dt.Product_Category_Desc_En;
                isActive.Checked = Convert.ToBoolean(dt.IsActive);
                txtSelectedFile.Value = dt.Product_Category_Image;
                txtOrder.Value = dt.Product_Category_Order.ToString();
                if (!string.IsNullOrEmpty(dt.Product_Category_Image))
                {
                    img.Visible = true;
                    img.ImageUrl = "/" + dt.Product_Category_Image;
                    
                }
                else
                {
                    img.Visible = false;
                }
                hdfImage.Value = dt.Product_Category_Image;
                if (dt.Product_Category_CatParent_ID.ToString() == "0") 
                    cbIsParent.Checked = true;
                cb_P_catID.SelectedValue = dt.Product_Category_CatParent_ID.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            ProductCategory pcObj = new ProductCategory(0, txt_cat_name.Value, txt_cat_name_en.Value, txt_cat_desc.Value, txt_cat_desc_en.Value, Convert.ToInt32(cb_P_catID.SelectedValue), isActive.Checked, 1, txtSelectedFile.Value, Convert.ToInt32(txtOrder.Value));
            proCateController.InsertCategory(pcObj);
            Response.Redirect(Request.RawUrl);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ProductCategory pcObj = new ProductCategory(Convert.ToInt32(rtvCatList.SelectedNode.Value), txt_cat_name.Value, txt_cat_name_en.Value, txt_cat_desc.Value, txt_cat_desc_en.Value, Convert.ToInt32(cb_P_catID.SelectedValue), isActive.Checked, 1, txtSelectedFile.Value,Convert.ToInt32(txtOrder.Value));
            proCateController.UpdateCategory(pcObj);
            BindingData(Convert.ToInt32(rtvCatList.SelectedNode.Value));
            Response.Redirect(Request.RawUrl);
           
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            img.Visible = false;
            btnUpdate.Visible = false;
            txt_cat_name.Value = "";
            txt_cat_name_en.Value = "";
            txt_cat_desc.Value ="";
            txt_cat_desc_en.Value = "";
            isActive.Checked = true;
            txtSelectedFile.Value = "";
            hdfImage.Value = "";
            cbIsParent.Checked = false;
            cb_P_catID.SelectedValue ="0";
            trLSP.Visible = true;
            txtOrder.Value = "0";

        }

       

        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            ProductCategoryController ch = new ProductCategoryController();
            ch.DeleteCategory(Convert.ToInt32(rtvCatList.SelectedNode.Value));
            Response.Redirect(Request.RawUrl);
        }

        protected void cbIsParent_CheckedChanged(object sender, EventArgs e)
        {
            if(cbIsParent.Checked ==true)
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