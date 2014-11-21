using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral;
using DFISYS.BO.Editoral.NewsMedia;
using DFISYS.BO.Editoral.Newsedit;
using DFISYS.BO.Editoral.Product;
using DFISYS.BO.Editoral.ProductColor;
using DFISYS.BO.Editoral.ProductGift;
using DFISYS.BO.Editoral.Product_Category;
using DFISYS.Core.DAL;

namespace DFISYS.GUI.EditoralOffice.MainOffce.ProductManager
{
    public partial class AddProduct : System.Web.UI.UserControl
    {
        ProductCategoryController proCatController = new ProductCategoryController();
        ProductGiftController proColorController = new ProductGiftController();
        ProductController productController =  new ProductController();
        protected int ID ;
        //Use for related media
        protected string tmpID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindDropDownList();
                if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                {
                    ID = Convert.ToInt32(Request.QueryString["pid"]);
                    BindData();
                }
                else
                {
                    tmpID = Guid.NewGuid().ToString();
                }
            }
           
        }
        private void BindData()
        {
            if(ID>0)
            {
                var productObj = productController.SelectProduct(ID);
                if(productObj!=null)
                {
                    txt_Name.Value = productObj.ProductName;
                    txt_Name_En.Value = productObj.ProductName_En;
                    txt_Summary.Value = productObj.ProductSumary;
                    txt_Sum_En.Value = productObj.ProductSumary_En;
                    txtSelectedFile.Value = productObj.ProductAvatar;
                    NewsContent.Text = productObj.ProductDescription;
                    NewsContent_En.Text = productObj.ProductDescription_En;
                    txt_Cost.Value = productObj.ProductCost.ToString();
                    ddlCategory.SelectedValue = productObj.ProductCategory.ToString();
                    txt_Video.Text = productObj.ProductVideo;
                    txt_tags.Value = productObj.ProductTag;
                    ddlLayout.SelectedValue = productObj.ProductLayout.ToString();
                    cb_IsActive.Checked = productObj.IsActive;
                    ddlGift.SelectedValue = productObj.ProductColor.ToString();
                    try
                    {
                        
                         hdMedia.Value = NewsMediaHelper.Get_ObjectId_By_FilmId(productObj.Id.ToString());

                         if (hdMedia.Value.TrimEnd(',').Length > 0)
                         {
                             
                             BindToDropdown(cboMedia, NewsEditHelper.Get_Media_By_ListsId("Object_ID", "Object_Url", "MediaObject", hdMedia.Value));
                         }
                    }
                    catch (Exception ex)
                    {

                    }


                    if (productObj.ProductOtherCat != null)
                    {
                        string[] strOthers = productObj.ProductOtherCat.Split(",".ToCharArray());
                        for (int i = 0; i < lstOtherCat.Items.Count; i++)
                        {
                            foreach (string strItem in strOthers)
                                if (strItem == lstOtherCat.Items[i].Value)
                                {
                                    lstOtherCat.Items[i].Selected = true;
                                    break;
                                }
                        }
                    }
                }
            }
        }
        private void BindToDropdown(ListBox cbo, string text)
        {
            string[] str = HttpUtility.HtmlEncode(text).Replace("#;#", ">").Replace(";#", "<").Split('>');
            string[] lItem;
            foreach (string s in str)
            {
                if (s == "") continue;
                lItem = s.Split('<');
                lItem[1] = HttpUtility.HtmlDecode(lItem[1]);
                if (lItem[1].Replace("\\", "/").IndexOf("/") != -1) lItem[1] = lItem[1].Substring(lItem[1].LastIndexOf("/") + 1, lItem[1].Length - lItem[1].LastIndexOf("/") - 1);
                cbo.Items.Add(new ListItem(lItem[1], lItem[0]));
            }
        }
        private void BindDropDownList()
        {
            //Bind Category
            proCatController.BuildTreeCat(ddlCategory);
            proCatController.BuildCheckBoxListCat(lstOtherCat);
           //Bind Layout
            ddlGift.DataSource = proColorController.SellectAllProductGift();
            ddlGift.DataValueField = "ID";
            ddlGift.DataTextField = "ProductGift";
            ddlGift.DataBind();
            ddlGift.Items.Insert(0, new ListItem("-----Select gift type-----", "0"));


           

        }

       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                ID = Convert.ToInt32(Request.QueryString["pid"]);
            }
            var productObj = new Product();
            productObj.Id = ID;
            productObj.ProductAvatar = txtSelectedFile.Value;
            productObj.ProductCategory = Convert.ToInt32(ddlCategory.SelectedValue);
            productObj.ProductVideo = txt_Video.Text;
            productObj.ProductCost = Convert.ToInt64(txt_Cost.Value);
            productObj.ProductDescription = NewsContent.Text;
            productObj.ProductDescription_En = NewsContent_En.Text;
            productObj.ProductLayout = Convert.ToInt32(ddlLayout.SelectedValue);
            productObj.ProductName = txt_Name.Value;
            productObj.ProductName_En = txt_Name_En.Value;
            productObj.ProductSumary = txt_Summary.Value;
            productObj.ProductSumary_En = txt_Sum_En.Value;
            productObj.ProductTag = txt_tags.Value;
            productObj.IsActive = cb_IsActive.Checked;
            productObj.ProductType = Convert.ToInt32(productType.SelectedValue);
            productObj.ProductColor = Convert.ToInt32(ddlGift.SelectedValue);
            string strOtherCat = "";
            foreach (ListItem item in lstOtherCat.Items)
            {
                if (item.Selected)
                    strOtherCat += item.Value + ",";
            }
            if (strOtherCat != "")
                strOtherCat = strOtherCat.Substring(0, strOtherCat.Length - 1);
            productObj.ProductOtherCat = strOtherCat;

            productObj = productController.ProductInsertUpdate(productObj);

            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                UpdateRelatedMedia(productObj.Id.ToString(), productObj.Id.ToString());
            }
            else
            {
                UpdateRelatedMedia(productObj.Id.ToString(),tmpID);
            }
            Response.Redirect("/office/productlist.aspx");
        }

        protected void UpdateRelatedMedia(string productID,string tmpID)
        {

            #region Cap nhap vao bang MediaObject
            // Xoa tat ca News_Media cu theo NewsID
            string strObjectId = NewsMediaHelper.Get_ObjectId_By_FilmId(tmpID);
            if (strObjectId != null && strObjectId != "")
            {
                string[] mediaIds = strObjectId.Split(",".ToCharArray());
                foreach (string strMediaId in mediaIds)
                    NewsMediaHelper.DeleteNews_Media_Film_Object_By_FilmIdAndObjectId(tmpID, strMediaId);
            }

            //truong hop co media object
            if (!string.IsNullOrEmpty(hdMedia.Value))
            {
                News_MediaRow objMrow = null;
                string[] mediaIds = hdMedia.Value.Split(",".ToCharArray());
                using(MainDB objDb = new MainDB())
                {
                    foreach (string strMediaId in mediaIds)
                    {
                        objMrow = new News_MediaRow();
                        objMrow.Film_ID = productID;
                        objMrow.Object_ID = int.Parse(strMediaId);
                        objDb.News_MediaCollection.Insert(objMrow);
                    }
                }
            }

            #endregion
        }
    }
}