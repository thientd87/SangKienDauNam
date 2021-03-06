using System;
using System.Data;
using System.Web.UI.WebControls;
using DAL;


namespace BO
{
    public class ProductCategoryHelper
    {
        public ProductCategoryHelper()
        {
        }
        public DataTable GetListCateParentPagging(int pageIndex,int pageSize,int imgWidth)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.Web_ListProductCatPagging(pageIndex, pageSize);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("Cat_URL")) dt.Columns.Add("Cat_URL");
                if (!dt.Columns.Contains("Image")) dt.Columns.Add("Image");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Cat_URL"] = Utility.CatLink(dt.Rows[i]["ID"].ToString(),
                                                             dt.Rows[i]["Product_Category_CatParent_ID"].ToString(),
                                                             Utility.UnicodeToKoDauAndGach(dt.Rows[i]["Product_Category_Name"].ToString()), "2");

                    dt.Rows[i]["Image"] = dt.Rows[i]["Product_Category_Image"] != null ? Utility.GetThumbNail(dt.Rows[i]["Product_Category_Name"].ToString(), dt.Rows[i]["Cat_URL"].ToString(), dt.Rows[i]["Product_Category_Image"].ToString(), imgWidth) : String.Empty;
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        public int GetProductCategoryParentCount(int pageSize)
        {
          
            int totalPage = 0;
            if (totalPage == 0)
            {
                DataTable tbl = null;
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_ListProductCatCount();
                    if (tbl != null)
                    {
                        totalPage = Utility.ConvertToInt(tbl.Rows[0][0]);
                        totalPage = (totalPage - 1) / pageSize + 1;
                    }
                    else
                    {
                        totalPage = 1;
                    }

                }
               
            }
            return totalPage;
        }

        public DataTable GetAllProductColor()
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.proc_SelectProductColorsAll();
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("Color_URL")) dt.Columns.Add("Color_URL");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Color_URL"] = Utility.ColorLink(dt.Rows[i]["ID"].ToString(),dt.Rows[i]["ColorName"].ToString());
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        public DataTable GetProductColorByID(int ID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.proc_SelectProductColor(ID);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("Color_URL")) dt.Columns.Add("Color_URL");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Color_URL"] = Utility.ColorLink(dt.Rows[i]["ID"].ToString(), dt.Rows[i]["ColorName"].ToString());
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        public void DeleteCategory(string Cat_ID)
        {
            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.DeletetblCategory(Cat_ID);
            } 
        }
        public DataTable UpdateCategory(int Cat_ID, int Cat_ParentID, string Logo, string Cat_Name)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.InsertUpdatetblCategory(Cat_ParentID, Logo,Cat_ID, Cat_Name);
            }
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CatID"></param>
        /// <returns></returns>
        public DataTable GetCategoryByCatID(int CatID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelecttblCategory(CatID);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("Cat_URL")) dt.Columns.Add("Cat_URL");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Cat_URL"] = Utility.CatLink(dt.Rows[i]["ID"].ToString(),
                                                             dt.Rows[i]["Product_Category_CatParent_ID"].ToString(),
                                                             Utility.UnicodeToKoDauAndGach(dt.Rows[i]["Product_Category_Name"].ToString()), "2");
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        public DataTable GetCategoryByCatParentID(int CatID, int pageIndex, int pageSize)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.proc_SelectProductCategory_Paged(pageSize, pageIndex,CatID);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("Cat_URL")) dt.Columns.Add("Cat_URL");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Cat_URL"] = Utility.CatLink(dt.Rows[i]["ID"].ToString(),
                                                             dt.Rows[i]["Product_Category_CatParent_ID"].ToString(),
                                                             Utility.UnicodeToKoDauAndGach(dt.Rows[i]["Product_Category_Name"].ToString()), "2");
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        public int GetCategoryByCatParentID_Count(int catID, int PageSize)
        {
            string CachName = "Microf_DanhSachTin_Count" + catID + PageSize;
            int totalPage = 0;
            if (totalPage == 0)
            {
                DataTable tbl = null;
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.proc_SelectProductCategory_Count(catID);
                    if (tbl != null)
                    {
                        totalPage = Utility.ConvertToInt(tbl.Rows[0][0]);
                        totalPage = (totalPage - 1) / PageSize + 1;
                    }
                    else
                    {
                        totalPage = 1;
                    }

                }
              //  Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CachName, totalPage);
            }
            return totalPage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cat_ParentID"></param>
        /// <param name="Logo"></param>
        /// <param name="Cat_Name"></param>
        /// <returns></returns>
        public DataTable InsertCategory(int Cat_ParentID, string Logo, string Cat_Name)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.InserttblCategory(Cat_ParentID, Logo, Cat_Name);
            }
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <returns>Danh sach cac Loai san pham chinh cap 1</returns>
        public  DataTable GetCatParent()
        {
            DataTable dt;
            using(MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelectCatParent();
            }
            if(dt!=null&& dt.Rows.Count>0)
            {
                if (!dt.Columns.Contains("Cat_URL")) dt.Columns.Add("Cat_URL");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Cat_URL"] = Utility.CatLink(dt.Rows[i]["ID"].ToString(),
                                                             dt.Rows[i]["Product_Category_CatParent_ID"].ToString(),
                                                             Utility.UnicodeToKoDauAndGach(dt.Rows[i]["Product_Category_Name"].ToString()), "2");
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="CatID">CatID cua loai san pham cap 1</param>
        /// <returns>Danh sach cac Loai san pham chinh cap 2</returns>
        public DataTable GetCatChildren(int CatID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelectCatChildren(CatID);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("Cat_URL")) dt.Columns.Add("Cat_URL");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Cat_URL"] = Utility.CatLink(dt.Rows[i]["ID"].ToString(),
                                                             dt.Rows[i]["Product_Category_CatParent_ID"].ToString(),
                                                             Utility.UnicodeToKoDauAndGach(dt.Rows[i]["Product_Category_Name"].ToString()), "2");
                }
                dt.AcceptChanges();
            }
            return dt;
        }


        public void BuildTreeCat(DropDownList cb_Cate)
        {
            cb_Cate.Items.Clear();
            using(MainDB db = new MainDB())
            {
                DataTable dtParent = GetCatParent();
                cb_Cate.Items.Add(new ListItem("-----Chọn tất cả -----", "0"));
                if(dtParent!=null&&dtParent.Rows.Count>0)
                {
                    foreach (DataRow dr in dtParent.Rows)
                    {
                        cb_Cate.Items.Add(new ListItem(dr["Cat_name"].ToString().ToUpper(),dr["Cat_ID"].ToString()));
                        DataTable dtChild = GetCatChildren(Convert.ToInt32(dr["Cat_ID"]));
                        if(dtChild!=null&&dtChild.Rows.Count>0)
                        {
                            foreach (DataRow drr in dtChild.Rows)
                            {
                                cb_Cate.Items.Add(new ListItem("→" + drr["Cat_Name"].ToString(), drr["Cat_ID"].ToString()));   
                            }
                        }
                    }
                }
            }
        }
    }
}
