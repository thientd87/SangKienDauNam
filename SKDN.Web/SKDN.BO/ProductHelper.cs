using System;
using System.ComponentModel;
using System.Data;
using DAL;

namespace BO
{
    public class ProductHelper
    {
        
        public ProductHelper()
        {
        }


        
        public static DataTable SearchProductByNameAndCode(string Key, int PageIndex, int PageSize, int imgWidth)
        {
           // string CacheName = "tdt_SearchFullText" + Key + PageSize + PageIndex + imgWidth;
            DataTable dt ;//= Utility.GetFromCache<DataTable>(CacheName);
           // if (dt == null)
            {
                using (MainDB db = new MainDB())
                {
                    dt = db.StoredProcedures.Web_SearchProducts(Key, PageIndex, PageSize);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!dt.Columns.Contains("CurrencyValue")) dt.Columns.Add("CurrencyValue");
                    if (!dt.Columns.Contains("URL")) dt.Columns.Add("URL");
                    if (!dt.Columns.Contains("AjaxURL")) dt.Columns.Add("AjaxURL");

                    if (!dt.Columns.Contains("Image")) dt.Columns.Add("Image");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["CurrencyValue"] = String.Format(Const.CurrentcyFormat, Convert.ToInt64(dt.Rows[i]["ProductCost"]));
                        dt.Rows[i]["URL"] = Utility.NewsDetailLinkV2(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["ProductCategory"].ToString(), dt.Rows[i]["Product_Category_CatParent_ID"].ToString(), dt.Rows[i]["Id"].ToString(), "2");
                        dt.Rows[i]["AjaxURL"] = "/ProjectDetailAjax.aspx?News_ID=" + dt.Rows[i]["Id"];
                        dt.Rows[i]["Image"] = dt.Rows[i]["ProductAvatar"] != null ? Utility.GetImageLink(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["AjaxURL"].ToString(), dt.Rows[i]["ProductAvatar"].ToString(), "avatarDuAn") : String.Empty;
                    }
                }
            }

            return dt;
        }
        public DataTable GetOtherProductInCat(string CatID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                string sql = "select P_ID,P_Name from tblProduct where P_trangthai =1 and P_CatID=" + CatID;
                dt = db.SelectQuery(sql);
            }
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetProductHot(int Row)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelectProductHot(Row);
            }
            if(dt!=null&&dt.Rows.Count>0)
            {
                dt.Columns.Add("CurrencyValue");
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    dt.Rows[i]["CurrencyValue"] = String.Format(Const.CurrentcyFormat, Convert.ToInt64(dt.Rows[i]["P_giachinhhang"]));
                   
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        /// <summary>
       
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetProductNew(int Row)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelectProductNew(Row);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("CurrencyValue");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["CurrencyValue"] = String.Format(Const.CurrentcyFormat, Convert.ToInt64(dt.Rows[i]["P_giachinhhang"]));
                }
                dt.AcceptChanges();
            }
            return dt;

        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable SelectProductByProductTypePaged(int PageSize, int PageNum, int ProductType)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.proc_ProductsSelectByProductType(PageSize, PageNum, ProductType);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CurrencyValue")) dt.Columns.Add("CurrencyValue");
                if (!dt.Columns.Contains("URL")) dt.Columns.Add("URL");
                if (!dt.Columns.Contains("AjaxURL")) dt.Columns.Add("AjaxURL");
                
                if (!dt.Columns.Contains("Image")) dt.Columns.Add("Image");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["CurrencyValue"] = String.Format(Const.CurrentcyFormat, Convert.ToInt64(dt.Rows[i]["ProductCost"]));
                    dt.Rows[i]["URL"] = Utility.NewsDetailLinkV2(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["ProductCategory"].ToString(), dt.Rows[i]["Product_Category_CatParent_ID"].ToString(), dt.Rows[i]["Id"].ToString(), "2");
                    dt.Rows[i]["AjaxURL"] = "/ProjectDetailAjax.aspx?News_ID=" + dt.Rows[i]["Id"];
                    dt.Rows[i]["Image"] = dt.Rows[i]["ProductAvatar"] != null ? Utility.GetImageLink(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["AjaxURL"].ToString(), dt.Rows[i]["ProductAvatar"].ToString(), "avatarDuAn") : String.Empty;
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        

        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageNum"></param>
        /// <param name="CatID"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable SelectProductByColorPaged(int PageSize, int PageNum, int ColorID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.proc_ProductsSelectByColorPaged(PageSize, PageNum, ColorID);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CurrencyValue")) dt.Columns.Add("CurrencyValue");
                if (!dt.Columns.Contains("URL")) dt.Columns.Add("URL");
                if (!dt.Columns.Contains("Image")) dt.Columns.Add("Image");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["CurrencyValue"] = String.Format(Const.CurrentcyFormat, Convert.ToInt64(dt.Rows[i]["ProductCost"]));
                    dt.Rows[i]["URL"] = Utility.NewsDetailLinkV2(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["ProductCategory"].ToString(), dt.Rows[i]["Product_Category_CatParent_ID"].ToString(), dt.Rows[i]["Id"].ToString(),"2");
                    dt.Rows[i]["Image"] = dt.Rows[i]["ProductAvatar"] != null ? Utility.GetImageLink(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["URL"].ToString(), dt.Rows[i]["ProductAvatar"].ToString()) : String.Empty;
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetProductByCatID_Paged(int CatID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.proc_CategoryLayout_Select(CatID);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CurrencyValue")) dt.Columns.Add("CurrencyValue");
                if (!dt.Columns.Contains("URL")) dt.Columns.Add("URL");
                if (!dt.Columns.Contains("Image")) dt.Columns.Add("Image");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["CurrencyValue"] = dt.Rows[i]["ProductID"].ToString() != "0" ? String.Format(Const.CurrentcyFormat, Convert.ToInt64(dt.Rows[i]["ProductCost"])) : "";
                    dt.Rows[i]["URL"] = dt.Rows[i]["ProductID"].ToString() != "0" ? Utility.NewsDetailLinkV2(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["ProductCategory"].ToString(), dt.Rows[i]["Product_Category_CatParent_ID"].ToString(), dt.Rows[i]["Id"].ToString(), "2") : "";
                    dt.Rows[i]["Image"] = dt.Rows[i]["ProductID"].ToString() != "0" ? Utility.GetImageLink(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["URL"].ToString(), dt.Rows[i]["ProductAvatar"].ToString()) : String.Empty;
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageNum"></param>
        /// <param name="CatID"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetProductByCatID_Paged(int PageSize,int PageNum,int CatID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelectProductsByCatID(PageSize, PageNum, CatID);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CurrencyValue")) dt.Columns.Add("CurrencyValue");
                if (!dt.Columns.Contains("URL")) dt.Columns.Add("URL");
                if (!dt.Columns.Contains("AjaxURL")) dt.Columns.Add("AjaxURL");
                if (!dt.Columns.Contains("Image")) dt.Columns.Add("Image");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["CurrencyValue"] = String.Format(Const.CurrentcyFormat, Convert.ToInt64(dt.Rows[i]["ProductCost"]));
                    dt.Rows[i]["AjaxURL"] = "/ProjectDetailAjax.aspx?News_ID=" + dt.Rows[i]["Id"];
                    dt.Rows[i]["URL"] = Utility.NewsDetailLinkV2(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["ProductCategory"].ToString(), dt.Rows[i]["Product_Category_CatParent_ID"].ToString(), dt.Rows[i]["Id"].ToString(),"2");
                    dt.Rows[i]["Image"] = dt.Rows[i]["ProductAvatar"] != null ? Utility.GetImageLink(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["AjaxURL"].ToString(), dt.Rows[i]["ProductAvatar"].ToString(), "avatarDuAn") : String.Empty;
                    
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageNum"></param>
        /// <param name="CatID"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable GetProductByTime(int PageSize, int PageNum, string orderType)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.proc_ProductsSelectOrderTime(PageSize, PageNum, orderType);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CurrencyValue")) dt.Columns.Add("CurrencyValue");
                if (!dt.Columns.Contains("URL")) dt.Columns.Add("URL");
                if (!dt.Columns.Contains("AjaxURL")) dt.Columns.Add("AjaxURL");
                if (!dt.Columns.Contains("Image")) dt.Columns.Add("Image");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["CurrencyValue"] = String.Format(Const.CurrentcyFormat, Convert.ToInt64(dt.Rows[i]["ProductCost"]));
                    dt.Rows[i]["AjaxURL"] = "/ProjectDetailAjax.aspx?News_ID=" + dt.Rows[i]["Id"];
                    dt.Rows[i]["URL"] = Utility.NewsDetailLinkV2(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["ProductCategory"].ToString(), dt.Rows[i]["Product_Category_CatParent_ID"].ToString(), dt.Rows[i]["Id"].ToString(), "2");
                    dt.Rows[i]["Image"] = dt.Rows[i]["ProductAvatar"] != null ? Utility.GetImageLink(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["AjaxURL"].ToString(), dt.Rows[i]["ProductAvatar"].ToString(), "avatarDuAn") : String.Empty;

                }
                dt.AcceptChanges();
            }
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="CatID"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public int GetProductCountByCatID(string CatID)
        {
            int Count = 0;
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.StoredProcedures.SelectCountProductByCatID(CatID);
                if(dt!=null&&dt.Rows.Count>0)
                {
                    Count = Convert.ToInt32(dt.Rows[0][0]);
                }
            }
            return Count;
        }

        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="numPage"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable GetPage(int numPage)
        {
            DataTable objTb = new DataTable();
            int intPagenum = numPage;
            objTb.Columns.Add(new DataColumn("Text", typeof(string)));
            objTb.Columns.Add(new DataColumn("Value", typeof(string)));
            for (int i = 1; i <= intPagenum; i++)
            {
                DataRow myRow = objTb.NewRow();
                myRow["Text"] = i.ToString();
                myRow["Value"] = Convert.ToString(i - 1);
                objTb.Rows.Add(myRow);
            }
            if (intPagenum == 0)
            {
                DataRow myRow = objTb.NewRow();
                myRow["Text"] = "1";
                myRow["Value"] = "0";
                objTb.Rows.Add(myRow);
            }
            return objTb;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetProductDynamic(string condition)
        {
            DataTable dt;
            using(MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelecttblProductsDynamic(condition);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("CurrencyValue");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["CurrencyValue"] = String.Format("{0:n}", Convert.ToInt64(dt.Rows[i]["P_giachinhhang"]));
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="P_ID"></param>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteProduct(string P_ID)
        {
            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.DeletetblProduct(P_ID);
            }
        }
       /// <summary>
        /// Created By DungTT
       /// </summary>
       /// <param name="P_kichthuoc"></param>
       /// <param name="P_tgthoai"></param>
       /// <param name="P_trongluong"></param>
       /// <param name="P_IsNew"></param>
       /// <param name="P_tgcho"></param>
       /// <param name="P_name"></param>
       /// <param name="P_IsFocus"></param>
       /// <param name="P_camera"></param>
       /// <param name="P_catID"></param>
       /// <param name="P_giachinhhang"></param>
       /// <param name="P_daitan"></param>
       /// <param name="P_chuthich"></param>
       /// <param name="P_gprs"></param>
       /// <param name="P_pin"></param>
       /// <param name="P_sold"></param>
       /// <param name="P_dophangiai"></param>
       /// <param name="P_id"></param>
       /// <param name="P_hongngoai"></param>
       /// <param name="P_baohanh"></param>
       /// <param name="P_amthanh"></param>
       /// <param name="P_bluetooth"></param>
       /// <param name="P_trangthai"></param>
       /// <param name="P_usb"></param>
       /// <param name="P_tinnhan"></param>
       /// <param name="P_image"></param>
       /// <param name="P_memory"></param>
       /// <param name="P_manhinh"></param>
         public void UpdatetblProduct(String P_kichthuoc, String P_tgthoai, String P_trongluong, Boolean P_IsNew, String P_tgcho, String P_name, Boolean P_IsFocus, String P_camera, Int32 P_catID, String P_giachinhhang, String P_daitan, String P_chuthich, Boolean P_gprs, String P_pin, Int32 P_sold, String P_dophangiai, Int32 P_id, Boolean P_hongngoai, String P_baohanh, String P_amthanh, Boolean P_bluetooth, Boolean P_trangthai, Boolean P_usb, String P_tinnhan, String P_image, String P_memory, String P_manhinh)
         {
             using(MainDB db = new MainDB())
             {
                 db.StoredProcedures.InsertUpdatetblProduct(P_kichthuoc, P_tgthoai, P_trongluong, P_IsNew, P_tgcho,
                                                            P_name, P_IsFocus, P_camera, P_catID, P_giachinhhang,
                                                            P_daitan, P_chuthich, P_gprs, P_pin, P_sold, P_dophangiai,
                                                            P_id, P_hongngoai, P_baohanh, P_amthanh, P_bluetooth,
                                                            P_trangthai, P_usb, P_tinnhan, P_image, P_memory, P_manhinh);
             }
         }

        /// <summary>
         /// Created By DungTT
        /// </summary>
        /// <param name="P_kichthuoc"></param>
        /// <param name="P_tgthoai"></param>
        /// <param name="P_trongluong"></param>
        /// <param name="P_IsNew"></param>
        /// <param name="P_tgcho"></param>
        /// <param name="P_name"></param>
        /// <param name="P_IsFocus"></param>
        /// <param name="P_camera"></param>
        /// <param name="P_catID"></param>
        /// <param name="P_giachinhhang"></param>
        /// <param name="P_daitan"></param>
        /// <param name="P_chuthich"></param>
        /// <param name="P_gprs"></param>
        /// <param name="P_pin"></param>
        /// <param name="P_sold"></param>
        /// <param name="P_dophangiai"></param>
        /// <param name="P_hongngoai"></param>
        /// <param name="P_baohanh"></param>
        /// <param name="P_amthanh"></param>
        /// <param name="P_bluetooth"></param>
        /// <param name="P_trangthai"></param>
        /// <param name="P_usb"></param>
        /// <param name="P_tinnhan"></param>
        /// <param name="P_image"></param>
        /// <param name="P_memory"></param>
        /// <param name="P_manhinh"></param>
        public void InsertProduct(String P_kichthuoc, String P_tgthoai, String P_trongluong, Boolean P_IsNew, String P_tgcho, String P_name, Boolean P_IsFocus, String P_camera, Int32 P_catID, String P_giachinhhang, String P_daitan, String P_chuthich, Boolean P_gprs, String P_pin, Int32 P_sold, String P_dophangiai,  Boolean P_hongngoai, String P_baohanh, String P_amthanh, Boolean P_bluetooth, Boolean P_trangthai, Boolean P_usb, String P_tinnhan, String P_image, String P_memory, String P_manhinh)
        {
            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.InserttblProduct(P_kichthuoc, P_tgthoai, P_trongluong, P_IsNew, P_tgcho,
                                                           P_name, P_IsFocus, P_camera, P_catID, P_giachinhhang,
                                                           P_daitan, P_chuthich, P_gprs, P_pin, P_sold, P_dophangiai,
                                                           P_trangthai, P_hongngoai, P_baohanh, P_amthanh, P_bluetooth,
                                                            P_usb, P_tinnhan, P_image, P_memory, P_manhinh);
            }
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="P_ID"></param>
        /// <returns></returns>
        public static DataTable GetProductByID(int P_ID)
        {
            DataTable dt;
            using(MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelecttblProduct(P_ID);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CurrencyValue")) dt.Columns.Add("CurrencyValue");
                if (!dt.Columns.Contains("URL")) dt.Columns.Add("URL");
                if (!dt.Columns.Contains("Image")) dt.Columns.Add("Image");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["CurrencyValue"] = String.Format(Const.CurrentcyFormat, Convert.ToInt64(dt.Rows[i]["ProductCost"]));
                    dt.Rows[i]["URL"] = Utility.NewsDetailLinkV2(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["ProductCategory"].ToString(), dt.Rows[i]["Product_Category_CatParent_ID"].ToString(), dt.Rows[i]["Id"].ToString(), "2");
                    dt.Rows[i]["Image"] = dt.Rows[i]["ProductAvatar"] != null ? Utility.GetImageLink(dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["URL"].ToString(), dt.Rows[i]["ProductAvatar"].ToString(),"avatarDuAn") : String.Empty;
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        public static string Get_ObjectId_By_FilmId(string film_id)
        {
            DataTable result;
            using (MainDB objDb = new MainDB())
            {
                //result = objDb.SelectQuery(" select Object_ID from news_media Where Film_ID = " + film_id + " And Object_ID > 0 And (News_ID IS NULL OR News_ID = -1) ");
                result = objDb.StoredProcedures.vc_Execute_Sql(" select Object_ID from news_media Where Film_ID = '" + film_id + "' And Object_ID > 0 And (News_ID IS NULL OR News_ID = -1) ");
            }
            if (result.Rows.Count > 0)
            {
                string sReturn = "";
                foreach (DataRow dr in result.Rows)
                {
                    sReturn += dr["Object_ID"] + ",";
                }
                sReturn = sReturn.Remove(sReturn.Length - 1, 1);
                return sReturn;
            }
            return "";
        }

        public static DataTable Get_Media_By_ListsId(string id, string text, string table, string lst)
        {
            DataTable result;
            lst = lst.TrimEnd(',');
            using (MainDB objDb = new MainDB())
            {
                result = objDb.StoredProcedures.vc_Execute_Sql(" select " + id + "," + text + " from " + table + " Where " + id + " in (" + lst + ") ");
            }

            return result;
        }


        public static DataTable GetProductGift()
        {
            DataTable result;
             using (MainDB objDb = new MainDB())
            {
                result = objDb.StoredProcedures.proc_SelectProductGiftAll();
            }
            return result;
        
        }
    }
}

