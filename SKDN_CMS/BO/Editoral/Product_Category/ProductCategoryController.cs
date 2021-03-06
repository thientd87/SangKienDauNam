using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DFISYS.Core.DAL;
using Nextcom.Citinews.Core.Library;

namespace DFISYS.BO.Editoral.Product_Category
{
    public class ProductCategoryController
    {
        public ProductCategoryController()
        {
        }
        public void DeleteCategory(int Cat_ID)
        {
            new ProductCategoryDAL().DeletetblCategory(Cat_ID);
        }
        public void UpdateCategory(ProductCategory pcObj)
        {
            ProductCategory pc = new ProductCategory();
          
            new ProductCategoryDAL().InsertUpdatetblCategory(pcObj);           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CatID"></param>
        /// <returns></returns>
        public ProductCategory GetCategoryByCatID(int CatID)
        {
            return ObjectHelper.FillObject<ProductCategory>(new ProductCategoryDAL().SelecttblCategory(CatID));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cat_ParentID"></param>
        /// <param name="Logo"></param>
        /// <param name="Cat_Name"></param>
        /// <returns></returns>
        public void InsertCategory(ProductCategory obj)
        {
            ProductCategory pc = new ProductCategory();
            new ProductCategoryDAL().InsertUpdatetblCategory(obj); 
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <returns>Danh sach cac Loai san pham chinh cap 1</returns>
        public List<ProductCategory> GetCatParent()
        {
            return ObjectHelper.FillCollection<ProductCategory>(new ProductCategoryDAL().SelectCatParent());
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="CatID">CatID cua loai san pham cap 1</param>
        /// <returns>Danh sach cac Loai san pham chinh cap 2</returns>
        public List<ProductCategory> GetCatChildren(int CatID)
        {
            return ObjectHelper.FillCollection<ProductCategory>(new ProductCategoryDAL().SelectCatChildren(CatID));
        }


        public void BuildTreeCat(DropDownList cb_Cate)
        {
            cb_Cate.Items.Clear();
            ProductCategoryController db = new ProductCategoryController();
            List<ProductCategory> listParent = db.GetCatParent();
                cb_Cate.Items.Add(new ListItem("-----Select category -----", "0"));
                if (listParent != null && listParent.Count > 0)
                {
                    foreach (ProductCategory pc in listParent)
                    {
                        cb_Cate.Items.Add(new ListItem(pc.Product_Category_Name.ToUpper(),pc.ID.ToString()));
                        List<ProductCategory>  dtChild = GetCatChildren(pc.ID);
                        if(dtChild!=null&&dtChild.Count>0)
                        {
                            foreach (ProductCategory pcChild in dtChild)
                            {
                                cb_Cate.Items.Add(new ListItem("→" + pcChild.Product_Category_Name, pcChild.ID.ToString()));
                                List<ProductCategory> dtChildLevel3 = GetCatChildren(pcChild.ID);
                                if (dtChildLevel3 != null && dtChildLevel3.Count > 0)
                                {
                                    foreach (var productCategory in dtChildLevel3)
                                    {
                                        cb_Cate.Items.Add(new ListItem("→→" + productCategory.Product_Category_Name, productCategory.ID.ToString()));
                                    }
                                }
                            }
                        }
                    }
                }
            
        }

        public void BuildCheckBoxListCat(CheckBoxList cb_Cate)
        {
            cb_Cate.Items.Clear();
            ProductCategoryController db = new ProductCategoryController();
            List<ProductCategory> listParent = db.GetCatParent();
           // cb_Cate.Items.Add(new ListItem("-----Select category -----", "0"));
            if (listParent != null && listParent.Count > 0)
            {
                foreach (ProductCategory pc in listParent)
                {
                    cb_Cate.Items.Add(new ListItem(pc.Product_Category_Name.ToUpper(), pc.ID.ToString()));
                    List<ProductCategory> dtChild = GetCatChildren(pc.ID);
                    if (dtChild != null && dtChild.Count > 0)
                    {
                        foreach (ProductCategory pcChild in dtChild)
                        {
                            cb_Cate.Items.Add(new ListItem("→" + pcChild.Product_Category_Name, pcChild.ID.ToString()));
                            List<ProductCategory> dtChildLevel3 = GetCatChildren(pcChild.ID);
                            if (dtChildLevel3 != null && dtChildLevel3.Count > 0)
                            {
                                foreach (var productCategory in dtChildLevel3)
                                {
                                    cb_Cate.Items.Add(new ListItem("→→" + productCategory.Product_Category_Name, productCategory.ID.ToString()));
                                }
                            }
                        }
                    }
                }
            }

        }

        public DataTable GetCategoryLayoutByCatID(int Cat_ID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.proc_CategoryLayout_Select(Cat_ID);
            }
            return dt;
        }
        public void InsertCategoryLayout(int Cat_ID, int CellIndex, int ProductID)
        {
            
            using (MainDB db = new MainDB())
            {
                 db.StoredProcedures.proc_CategoryLayout_Insert(Cat_ID,CellIndex,ProductID);
            }
            
        }
        public void DeleteCategoryLayout(int Cat_ID)
        {

            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.proc_CategoryLayout_Delete(Cat_ID);
            }

        }
    }
}
