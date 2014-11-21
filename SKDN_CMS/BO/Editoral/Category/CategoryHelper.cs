using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using DFISYS.Core.DAL;
using System.ComponentModel;
using System.Collections;
using DFISYS.User.Security;
using System.Web.Caching;
using DFISYS.User.Db;
using System.Web.UI.WebControls;

namespace DFISYS.BO.Editoral.Category {
    public static class CategoryHelper {

        private static string LoadAllCat(DropDownList cb_Cate, ListBox lb_Cate, CheckBoxList cbl_Cate) {
            string strResult = "";
            using (DFISYS.Core.DAL.MainDB objDB = new DFISYS.Core.DAL.MainDB()) {
                DataTable parentRows = objDB.FrontEndStoreProcedure.Category_GetListByWhere(" Where (Cat_ParentID =0 OR Cat_ParentID is null) ", " Order By Cat_Order ");

                if (cb_Cate != null)
                    cb_Cate.Items.Add(new ListItem("-----Chọn tất cả -----", "0"));
                if (lb_Cate != null)
                    lb_Cate.Items.Add(new ListItem("-----Chọn chuyên mục khác -----", "0"));

                DataTable childRows;
                DataTable childLevel3Rows;
                foreach (DataRow aRow in parentRows.Rows) {
                    childRows = objDB.FrontEndStoreProcedure.Category_GetListByWhere(" Where Cat_ParentID=" + aRow["Cat_ID"] + " ", " Order By Cat_Order ");

                    if (cb_Cate != null)
                        cb_Cate.Items.Add(new ListItem("" + aRow["Cat_Name"].ToString().ToUpper() + "", aRow["Cat_ID"] + ""));
                    if (lb_Cate != null)
                        lb_Cate.Items.Add(new ListItem("" + aRow["Cat_Name"].ToString().ToUpper() + "", aRow["Cat_ID"] + ""));
                    if (cbl_Cate != null)
                        cbl_Cate.Items.Add(new ListItem("" + aRow["Cat_Name"].ToString().ToUpper() + "", aRow["Cat_ID"] + ""));

                    strResult += aRow["Cat_ID"] + ",";
                    foreach (DataRow RowChild in childRows.Rows) {
                        if (cb_Cate != null)
                            cb_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], Convert.ToString(RowChild["Cat_ID"])));
                        if (lb_Cate != null)
                            lb_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], Convert.ToString(RowChild["Cat_ID"])));
                        if (cbl_Cate != null)
                            cbl_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], RowChild["Cat_ID"] + ""));

                        strResult += RowChild["Cat_ID"] + ",";

                        // Xu ly o tang 3
                        childLevel3Rows = objDB.FrontEndStoreProcedure.Category_GetListByWhere(" Where Cat_ParentID=" + RowChild["Cat_ID"] + " ", " Order By Cat_Order ");
                        if (childLevel3Rows != null) {
                            foreach (DataRow RowChildLevel3Category in childLevel3Rows.Rows) {
                                if (cb_Cate != null)
                                    cb_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], Convert.ToString(RowChildLevel3Category["Cat_ID"])));
                                if (lb_Cate != null)
                                    lb_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], Convert.ToString(RowChildLevel3Category["Cat_ID"])));
                                if (cbl_Cate != null)
                                    cbl_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], RowChildLevel3Category["Cat_ID"] + ""));

                                strResult += RowChildLevel3Category["Cat_ID"] + ",";
                            }
                        }
                    }

                }
                return strResult;
            }
        }

        private static string LoadAllCatWithEdition(DropDownList cb_Cate, ListBox lb_Cate, CheckBoxList cbl_Cate, string editionType)
        {
            string strResult = "";
            using (DFISYS.Core.DAL.MainDB objDB = new DFISYS.Core.DAL.MainDB())
            {
                DataTable parentRows = objDB.FrontEndStoreProcedure.Category_GetListByWhere(" Where (Cat_ParentID =0 OR Cat_ParentID is null) and EditionType_ID = " + editionType, " Order By Cat_Order ");

                if (cb_Cate != null)
                    cb_Cate.Items.Add(new ListItem("-----Chọn tất cả -----", "0"));
                if (lb_Cate != null)
                    lb_Cate.Items.Add(new ListItem("-----Chọn chuyên mục khác -----", "0"));

                DataTable childRows;
                DataTable childLevel3Rows;
                foreach (DataRow aRow in parentRows.Rows)
                {
                    childRows = objDB.FrontEndStoreProcedure.Category_GetListByWhere(" Where Cat_ParentID=" + aRow["Cat_ID"] + " and EditionType_ID = " + editionType, " Order By Cat_Order ");

                    if (cb_Cate != null)
                        cb_Cate.Items.Add(new ListItem("" + aRow["Cat_Name"].ToString().ToUpper() + "", aRow["Cat_ID"] + ""));
                    if (lb_Cate != null)
                        lb_Cate.Items.Add(new ListItem("" + aRow["Cat_Name"].ToString().ToUpper() + "", aRow["Cat_ID"] + ""));
                    if (cbl_Cate != null)
                        cbl_Cate.Items.Add(new ListItem("" + aRow["Cat_Name"].ToString().ToUpper() + "", aRow["Cat_ID"] + ""));

                    strResult += aRow["Cat_ID"] + ",";
                    foreach (DataRow RowChild in childRows.Rows)
                    {
                        if (cb_Cate != null)
                            cb_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], Convert.ToString(RowChild["Cat_ID"])));
                        if (lb_Cate != null)
                            lb_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], Convert.ToString(RowChild["Cat_ID"])));
                        if (cbl_Cate != null)
                            cbl_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], RowChild["Cat_ID"] + ""));

                        strResult += RowChild["Cat_ID"] + ",";

                        // Xu ly o tang 3
                        childLevel3Rows = objDB.FrontEndStoreProcedure.Category_GetListByWhere(" Where Cat_ParentID=" + RowChild["Cat_ID"] + " and EditionType_ID = " + editionType, " Order By Cat_Order ");
                        if (childLevel3Rows != null)
                        {
                            foreach (DataRow RowChildLevel3Category in childLevel3Rows.Rows)
                            {
                                if (cb_Cate != null)
                                    cb_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], Convert.ToString(RowChildLevel3Category["Cat_ID"])));
                                if (lb_Cate != null)
                                    lb_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], Convert.ToString(RowChildLevel3Category["Cat_ID"])));
                                if (cbl_Cate != null)
                                    cbl_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], RowChildLevel3Category["Cat_ID"] + ""));

                                strResult += RowChildLevel3Category["Cat_ID"] + ",";
                            }
                        }
                    }

                }
                return strResult;
            }
        }


        private static string LoadCategoryByPermission(DropDownList cb_Cate, ListBox lb_Cate, CheckBoxList cbl_Cate) {
            string strResult = "";
            MainSecurity objSec = new MainSecurity();
            Role objrole = objSec.GetRole(HttpContext.Current.User.Identity.Name);
            DataTable objParentCat = objSec.getParentCategoryAssigned(HttpContext.Current.User.Identity.Name, DFISYS.API.Config.CurrentChannel);

            if (cb_Cate != null)
                cb_Cate.Items.Add(new ListItem("-----Chọn tất cả -----", "0"));
            if (lb_Cate != null)
                lb_Cate.Items.Add(new ListItem("-----Chọn chuyên mục khác -----", "0"));

            DataTable childRows = new DataTable();
            DataTable childLevel3Rows = new DataTable();
            using (DFISYS.Core.DAL.MainDB objDB = new DFISYS.Core.DAL.MainDB()) {
                foreach (DataRow objrow in objParentCat.Rows) {
                    childRows = objDB.FrontEndStoreProcedure.Category_GetListByWhere(" Where Cat_ParentID=" + objrow["Cat_ID"] + " ", " Order By Cat_Order ");
                    strResult += objrow["Cat_ID"] + ",";

                    if (cb_Cate != null)
                        cb_Cate.Items.Add(new ListItem("" + objrow["Cat_Name"].ToString().ToUpper() + "", objrow["Cat_ID"] + ""));
                    if (lb_Cate != null)
                        lb_Cate.Items.Add(new ListItem("" + objrow["Cat_Name"].ToString().ToUpper() + "", objrow["Cat_ID"] + ""));
                    if (cbl_Cate != null)
                        cbl_Cate.Items.Add(new ListItem("" + objrow["Cat_Name"].ToString().ToUpper() + "", objrow["Cat_ID"] + ""));


                    foreach (DataRow RowChild in childRows.Rows) {
                        if (cb_Cate != null)
                            cb_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], Convert.ToString(RowChild["Cat_ID"])));
                        if (lb_Cate != null)
                            lb_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], Convert.ToString(RowChild["Cat_ID"])));
                        if (cbl_Cate != null)
                            cbl_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], RowChild["Cat_ID"] + ""));

                        strResult += RowChild["Cat_ID"] + ",";

                        // Xu ly o tang 3
                        childLevel3Rows = objDB.FrontEndStoreProcedure.Category_GetListByWhere(" Where Cat_ParentID=" + RowChild["Cat_ID"] + " ", " Order By Cat_Order ");
                        if (childLevel3Rows.Rows.Count > 0) {
                            foreach (DataRow RowChildLevel3Category in childLevel3Rows.Rows) {
                                if (cb_Cate != null)
                                    cb_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], Convert.ToString(RowChildLevel3Category["Cat_ID"])));
                                if (lb_Cate != null)
                                    lb_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], Convert.ToString(RowChildLevel3Category["Cat_ID"])));
                                if (cbl_Cate != null)
                                    cbl_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], RowChildLevel3Category["Cat_ID"] + ""));

                                strResult += RowChildLevel3Category["Cat_ID"] + ",";
                            }
                        }
                    }
                }
            }

            return strResult;
        }

        private static string LoadCategoryByPermissionWithEditionType(DropDownList cb_Cate, ListBox lb_Cate, CheckBoxList cbl_Cate, string editionType)
        {
            string strResult = "";
            MainSecurity objSec = new MainSecurity();
            Role objrole = objSec.GetRole(HttpContext.Current.User.Identity.Name);
            DataTable objParentCat = objSec.getParentCategoryAssigned(HttpContext.Current.User.Identity.Name, DFISYS.API.Config.CurrentChannel, Convert.ToInt32(editionType));

            if (cb_Cate != null)
                cb_Cate.Items.Add(new ListItem("-----Chọn tất cả -----", "0"));
            if (lb_Cate != null)
                lb_Cate.Items.Add(new ListItem("-----Chọn chuyên mục khác -----", "0"));

            DataTable childRows = new DataTable();
            DataTable childLevel3Rows = new DataTable();
            using (DFISYS.Core.DAL.MainDB objDB = new DFISYS.Core.DAL.MainDB())
            {
                foreach (DataRow objrow in objParentCat.Rows)
                {
                    childRows = objDB.FrontEndStoreProcedure.Category_GetListByWhere(" Where Cat_ParentID=" + objrow["Cat_ID"] + " and EditionType_ID = " + editionType, " Order By Cat_Order ");
                    strResult += objrow["Cat_ID"] + ",";

                    if (cb_Cate != null)
                        cb_Cate.Items.Add(new ListItem("" + objrow["Cat_Name"].ToString().ToUpper() + "", objrow["Cat_ID"] + ""));
                    if (lb_Cate != null)
                        lb_Cate.Items.Add(new ListItem("" + objrow["Cat_Name"].ToString().ToUpper() + "", objrow["Cat_ID"] + ""));
                    if (cbl_Cate != null)
                        cbl_Cate.Items.Add(new ListItem("" + objrow["Cat_Name"].ToString().ToUpper() + "", objrow["Cat_ID"] + ""));


                    foreach (DataRow RowChild in childRows.Rows)
                    {
                        if (cb_Cate != null)
                            cb_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], Convert.ToString(RowChild["Cat_ID"])));
                        if (lb_Cate != null)
                            lb_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], Convert.ToString(RowChild["Cat_ID"])));
                        if (cbl_Cate != null)
                            cbl_Cate.Items.Add(new ListItem("→" + RowChild["Cat_Name"], RowChild["Cat_ID"] + ""));

                        strResult += RowChild["Cat_ID"] + ",";

                        // Xu ly o tang 3
                        childLevel3Rows = objDB.FrontEndStoreProcedure.Category_GetListByWhere(" Where Cat_ParentID=" + RowChild["Cat_ID"] + "  and EditionType_ID = " + editionType, " Order By Cat_Order ");
                        if (childLevel3Rows.Rows.Count > 0)
                        {
                            foreach (DataRow RowChildLevel3Category in childLevel3Rows.Rows)
                            {
                                if (cb_Cate != null)
                                    cb_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], Convert.ToString(RowChildLevel3Category["Cat_ID"])));
                                if (lb_Cate != null)
                                    lb_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], Convert.ToString(RowChildLevel3Category["Cat_ID"])));
                                if (cbl_Cate != null)
                                    cbl_Cate.Items.Add(new ListItem("→→" + RowChildLevel3Category["Cat_Name"], RowChildLevel3Category["Cat_ID"] + ""));

                                strResult += RowChildLevel3Category["Cat_ID"] + ",";
                            }
                        }
                    }
                }
            }

            return strResult;
        }

        /// <summary>
        /// Ham thuc hien bind du lieu ve danh muc cho mot dropdown control
        /// </summary>
        /// <param name="cb_Cate">Dropdown can bind</param>
        /// <returns>Tra ve mot chuoi cac id cua cay duoc cach nhau boi dau ","</returns>
        public static string Treebuild(DropDownList cb_Cate) {
            cb_Cate.Items.Clear();
            string strResult = "";

            // Doi voi truong hop luu Cache
            string str_item_cache = HttpContext.Current.Cache.Get("Treebuild_" + HttpContext.Current.User.Identity.Name) as string;
            strResult = HttpContext.Current.Cache.Get("Treebuild_strResult" + HttpContext.Current.User.Identity.Name) as string;
            if (!string.IsNullOrEmpty(str_item_cache) && !string.IsNullOrEmpty(strResult)) {
                #region Da luu cache
                //strResult = HttpContext.Current.Cache.Get("Treebuild_strResult" + HttpContext.Current.User.Identity.Name) as string;

                string[] strArItem = str_item_cache.Split('!');
                string[] strTextValue;
                ListItem item = new ListItem();
                foreach (string strItem in strArItem) {
                    item = new ListItem();
                    strTextValue = strItem.Split('|');
                    item.Text = strTextValue[0];
                    item.Value = strTextValue[1];
                    cb_Cate.Items.Add(item);
                }
                #endregion
            }
            else {
                #region Doi voi truong hop chua luu Cache
                // Doi voi truong hop chua luu cache
                //thuc hien lay danh sach cat ma nguoi hien tai duoc phep - Check neu nguoi hien tai khong phai la TBT - TKTS thi moi ap dung
                MainSecurity objSec = new MainSecurity();
                Role objrole = objSec.GetRole(HttpContext.Current.User.Identity.Name);
                if (objrole.isPhuTrachKenh || objrole.isThuKyToaSoan || objrole.isTongBienTap) {
                    strResult = LoadAllCat(cb_Cate, null, null);
                }
                else {
                    strResult = LoadCategoryByPermission(cb_Cate, null, null);
                }
                if (strResult != "")
                    strResult = strResult.Substring(0, strResult.Length - 1);

                str_item_cache = "";
                foreach (ListItem item in cb_Cate.Items) {
                    str_item_cache += "!" + item.Text + "|" + item.Value;
                }

                if (str_item_cache.Trim() != "")
                    str_item_cache = str_item_cache.Substring(1);

                HttpContext.Current.Cache.Insert("Treebuild_" + HttpContext.Current.User.Identity.Name, str_item_cache, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert("Treebuild_strResult" + HttpContext.Current.User.Identity.Name, strResult, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                #endregion
            }
            return strResult;
        }

        public static string TreebuildAllCat(DropDownList cb_Cate) {
            cb_Cate.Items.Clear();
            string strResult = "";

            string str_item_cache = HttpContext.Current.Cache.Get("TreebuildAllCat_" + HttpContext.Current.User.Identity.Name) as string;
            if (!string.IsNullOrEmpty(str_item_cache)) {
                #region Da Luu Cache

                strResult = HttpContext.Current.Cache.Get("TreebuildAllCat_strResult" + HttpContext.Current.User.Identity.Name) as string;

                // Doi voi truong hop luu Cache
                string[] strArItem = str_item_cache.Split('!');
                string[] strTextValue;
                ListItem item = new ListItem();
                foreach (string strItem in strArItem) {
                    item = new ListItem();
                    strTextValue = strItem.Split('|');
                    item.Text = strTextValue[0];
                    item.Value = strTextValue[1];
                    cb_Cate.Items.Add(item);
                }
                #endregion
            }
            else {
                #region Doi voi truong hop chua luu Cache
                strResult = LoadAllCat(cb_Cate, null, null);

                if (strResult != "")
                    strResult = strResult.Substring(0, strResult.Length - 1);

                str_item_cache = "";
                foreach (ListItem item in cb_Cate.Items) {
                    str_item_cache += "!" + item.Text + "|" + item.Value;
                }

                if (str_item_cache.Trim() != "")
                    str_item_cache = str_item_cache.Substring(1);

                HttpContext.Current.Cache.Insert("TreebuildAllCat_" + HttpContext.Current.User.Identity.Name, str_item_cache, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert("TreebuildAllCat_strResult" + HttpContext.Current.User.Identity.Name, strResult, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                #endregion
            }

            return strResult;
        }

        public static string TreebuildAllCatWithEditionType(DropDownList cb_Cate, string EditionType)
        {
            cb_Cate.Items.Clear();
            string strResult = "";

            string str_item_cache = HttpContext.Current.Cache.Get("TreebuildAllCatWithEditionType_" + HttpContext.Current.User.Identity.Name + EditionType) as string;
            if (!string.IsNullOrEmpty(str_item_cache))
            {
                #region Da Luu Cache

                strResult = HttpContext.Current.Cache.Get("TreebuildAllCatWithEditionType_strResult" + HttpContext.Current.User.Identity.Name + EditionType) as string;

                // Doi voi truong hop luu Cache
                string[] strArItem = str_item_cache.Split('!');
                string[] strTextValue;
                ListItem item = new ListItem();
                foreach (string strItem in strArItem)
                {
                    item = new ListItem();
                    strTextValue = strItem.Split('|');
                    item.Text = strTextValue[0];
                    item.Value = strTextValue[1];
                    cb_Cate.Items.Add(item);
                }
                #endregion
            }
            else
            {
                #region Doi voi truong hop chua luu Cache
                strResult = LoadAllCatWithEdition(cb_Cate, null, null, EditionType);

                if (strResult != "")
                    strResult = strResult.Substring(0, strResult.Length - 1);

                str_item_cache = "";
                foreach (ListItem item in cb_Cate.Items)
                {
                    str_item_cache += "!" + item.Text + "|" + item.Value;
                }

                if (str_item_cache.Trim() != "")
                    str_item_cache = str_item_cache.Substring(1);

                HttpContext.Current.Cache.Insert("TreebuildAllCatWithEditionType_" + HttpContext.Current.User.Identity.Name + EditionType, str_item_cache, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert("TreebuildAllCatWithEditionType_strResult" + HttpContext.Current.User.Identity.Name + EditionType, strResult, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                #endregion
            }

            return strResult;
        }

        public static string GetCatIDByUser() {
            string strResult = HttpContext.Current.Cache.Get("GetCatIDByUser_strResult" + HttpContext.Current.User.Identity.Name) as string;
            if (string.IsNullOrEmpty(strResult)) {
                #region Doi voi truong hop chua luu Cache
                // Doi voi truong hop chua luu cache
                //thuc hien lay danh sach cat ma nguoi hien tai duoc phep - Check neu nguoi hien tai khong phai la TBT - TKTS thi moi ap dung
                MainSecurity objSec = new MainSecurity();
                Role objrole = objSec.GetRole(HttpContext.Current.User.Identity.Name);
                if (objrole.isPhuTrachKenh || objrole.isThuKyToaSoan || objrole.isTongBienTap) {
                    strResult = LoadAllCat(null, null, null);
                }
                else {
                    strResult = LoadCategoryByPermission(null, null, null);
                }
                if (strResult != "")
                    strResult = strResult.Substring(0, strResult.Length - 1);


                HttpContext.Current.Cache.Insert("GetCatIDByUser_strResult" + HttpContext.Current.User.Identity.Name, strResult, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                #endregion
            }

            return strResult;
        }


        public static void BindCheckBoxListCat(CheckBoxList cb_Cate) {
            cb_Cate.Items.Clear();
            string strResult = "";


            string str_item_cache = HttpContext.Current.Cache.Get("CheckBoxList_" + HttpContext.Current.User.Identity.Name) as string;
            if (!string.IsNullOrEmpty(str_item_cache)) {
                // Doi voi truong hop da luu Cache
                string[] strArItem = str_item_cache.Split('!');
                string[] strTextValue;
                ListItem item = new ListItem();
                foreach (string strItem in strArItem) {
                    item = new ListItem();
                    strTextValue = strItem.Split('|');
                    item.Text = strTextValue[0];
                    item.Value = strTextValue[1];
                    cb_Cate.Items.Add(item);
                }

            }
            else {
                #region Doi voi truong hop chua luu cache
                //thuc hien lay danh sach cat ma nguoi hien tai duoc phep - Check neu nguoi hien tai khong phai la TBT - TKTS thi moi ap dung
                MainSecurity objSec = new MainSecurity();
                Role objrole = objSec.GetRole(HttpContext.Current.User.Identity.Name);
                if (objrole.isPhuTrachKenh || objrole.isThuKyToaSoan || objrole.isTongBienTap) {

                    strResult = LoadAllCat(null, null, cb_Cate);
                }
                else {
                    strResult = LoadCategoryByPermission(null, null, cb_Cate);
                }

                str_item_cache = "";
                foreach (ListItem item in cb_Cate.Items) {
                    str_item_cache += "!" + item.Text + "|" + item.Value;
                }

                if (str_item_cache.Trim() != "")
                    str_item_cache = str_item_cache.Substring(1);

                HttpContext.Current.Cache.Insert("CheckBoxList_" + HttpContext.Current.User.Identity.Name, str_item_cache, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                #endregion
            }
        }

        public static void BindCheckBoxListCat(CheckBoxList cb_Cate, int editionType)
        {
            cb_Cate.Items.Clear();
            string strResult = "";


            string str_item_cache = HttpContext.Current.Cache.Get("CheckBoxList_" + HttpContext.Current.User.Identity.Name + editionType) as string;
            if (!string.IsNullOrEmpty(str_item_cache))
            {
                // Doi voi truong hop da luu Cache
                string[] strArItem = str_item_cache.Split('!');
                string[] strTextValue;
                ListItem item = new ListItem();
                foreach (string strItem in strArItem)
                {
                    item = new ListItem();
                    strTextValue = strItem.Split('|');
                    item.Text = strTextValue[0];
                    item.Value = strTextValue[1];
                    cb_Cate.Items.Add(item);
                }

            }
            else
            {
                #region Doi voi truong hop chua luu cache
                //thuc hien lay danh sach cat ma nguoi hien tai duoc phep - Check neu nguoi hien tai khong phai la TBT - TKTS thi moi ap dung
                MainSecurity objSec = new MainSecurity();
                Role objrole = objSec.GetRole(HttpContext.Current.User.Identity.Name);
                if (objrole.isPhuTrachKenh || objrole.isThuKyToaSoan || objrole.isTongBienTap)
                {

                    strResult = LoadAllCatWithEdition(null, null, cb_Cate, editionType.ToString());
                }
                else
                {
                    strResult = LoadCategoryByPermissionWithEditionType(null, null, cb_Cate, editionType.ToString());
                }

                str_item_cache = "";
                foreach (ListItem item in cb_Cate.Items)
                {
                    str_item_cache += "!" + item.Text + "|" + item.Value;
                }

                if (str_item_cache.Trim() != "")
                    str_item_cache = str_item_cache.Substring(1);

                HttpContext.Current.Cache.Insert("CheckBoxList_" + HttpContext.Current.User.Identity.Name + editionType, str_item_cache, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                #endregion
            }
        }


        public static void BindListCat(ListBox cb_Cate) {
            cb_Cate.Items.Clear();
            string strResult = "";

            string str_item_cache = HttpContext.Current.Cache.Get("BindListCat_" + HttpContext.Current.User.Identity.Name) as string;
            if (!string.IsNullOrEmpty(str_item_cache)) {
                // Doi voi truong hop da luu Cache
                string[] strArItem = str_item_cache.Split('!');
                string[] strTextValue;
                ListItem item = new ListItem();
                foreach (string strItem in strArItem) {
                    item = new ListItem();
                    strTextValue = strItem.Split('|');
                    item.Text = strTextValue[0];
                    item.Value = strTextValue[1];
                    cb_Cate.Items.Add(item);
                }
            }
            else {
                #region Doi voi truong hop chua luu cache
                //thuc hien lay danh sach cat ma nguoi hien tai duoc phep - Check neu nguoi hien tai khong phai la TBT - TKTS thi moi ap dung
                MainSecurity objSec = new MainSecurity();
                Role objrole = objSec.GetRole(HttpContext.Current.User.Identity.Name);
                if (objrole.isPhuTrachKenh || objrole.isThuKyToaSoan || objrole.isTongBienTap) {

                    strResult = LoadAllCat(null, cb_Cate, null);
                }
                else {
                    strResult = LoadCategoryByPermission(null, cb_Cate, null);
                }

                str_item_cache = "";
                foreach (ListItem item in cb_Cate.Items) {
                    str_item_cache += "!" + item.Text + "|" + item.Value;
                }

                if (str_item_cache.Trim() != "")
                    str_item_cache = str_item_cache.Substring(1);

                HttpContext.Current.Cache.Insert("BindListCat_" + HttpContext.Current.User.Identity.Name, str_item_cache, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                #endregion
            }
        }

        public static void BindListCatDropDown(DropDownList cb_Cate) {
            cb_Cate.Items.Clear();
            string strResult = "";

            string str_item_cache = HttpContext.Current.Cache.Get("BindListCat_" + HttpContext.Current.User.Identity.Name) as string;
            if (!string.IsNullOrEmpty(str_item_cache)) {
                // Doi voi truong hop da luu Cache
                string[] strArItem = str_item_cache.Split('!');
                string[] strTextValue;
                ListItem item = new ListItem();
                foreach (string strItem in strArItem) {
                    item = new ListItem();
                    strTextValue = strItem.Split('|');
                    item.Text = strTextValue[0];
                    item.Value = strTextValue[1];
                    cb_Cate.Items.Add(item);
                }
            }
            else {
                #region Doi voi truong hop chua luu cache
                //thuc hien lay danh sach cat ma nguoi hien tai duoc phep - Check neu nguoi hien tai khong phai la TBT - TKTS thi moi ap dung
                MainSecurity objSec = new MainSecurity();
                Role objrole = objSec.GetRole(HttpContext.Current.User.Identity.Name);
                if (objrole.isPhuTrachKenh || objrole.isThuKyToaSoan || objrole.isTongBienTap) {

                    strResult = LoadAllCat(cb_Cate, null, null);
                }
                else {
                    strResult = LoadCategoryByPermission(cb_Cate, null, null);
                }

                str_item_cache = "";
                foreach (ListItem item in cb_Cate.Items) {
                    str_item_cache += "!" + item.Text + "|" + item.Value;
                }

                if (str_item_cache.Trim() != "")
                    str_item_cache = str_item_cache.Substring(1);

                HttpContext.Current.Cache.Insert("BindListCat_" + HttpContext.Current.User.Identity.Name, str_item_cache, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                #endregion
            }
        }

        public static void BindListCatDropDownWithEdition(DropDownList cb_Cate, string editionType)
        {
            cb_Cate.Items.Clear();
            string strResult = "";

            string str_item_cache = HttpContext.Current.Cache.Get("BindListCatWithEdition_" + HttpContext.Current.User.Identity.Name + editionType) as string;
            if (!string.IsNullOrEmpty(str_item_cache))
            {
                // Doi voi truong hop da luu Cache
                string[] strArItem = str_item_cache.Split('!');
                string[] strTextValue;
                ListItem item = new ListItem();
                foreach (string strItem in strArItem)
                {
                    item = new ListItem();
                    strTextValue = strItem.Split('|');
                    item.Text = strTextValue[0];
                    item.Value = strTextValue[1];
                    cb_Cate.Items.Add(item);
                }
            }
            else
            {
                #region Doi voi truong hop chua luu cache
                //thuc hien lay danh sach cat ma nguoi hien tai duoc phep - Check neu nguoi hien tai khong phai la TBT - TKTS thi moi ap dung
                MainSecurity objSec = new MainSecurity();
                Role objrole = objSec.GetRole(HttpContext.Current.User.Identity.Name);
                if (objrole.isPhuTrachKenh || objrole.isThuKyToaSoan || objrole.isTongBienTap)
                {

                    strResult = LoadAllCatWithEdition(cb_Cate, null, null, editionType);
                }
                else
                {
                    strResult = LoadCategoryByPermissionWithEditionType(cb_Cate, null, null, editionType);
                }

                str_item_cache = "";
                foreach (ListItem item in cb_Cate.Items)
                {
                    str_item_cache += "!" + item.Text + "|" + item.Value;
                }

                if (str_item_cache.Trim() != "")
                    str_item_cache = str_item_cache.Substring(1);

                HttpContext.Current.Cache.Insert("BindListCatWithEdition_" + HttpContext.Current.User.Identity.Name + editionType, str_item_cache, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                #endregion
            }
        }


        public static void bindAllCat(DropDownList cb_Cate) {

            string str_item_cache = HttpContext.Current.Cache.Get("bindAllCat_" + HttpContext.Current.User.Identity.Name) as string;
            if (!string.IsNullOrEmpty(str_item_cache)) {
                // Doi voi truong hop Luu Cache
                string[] strArItem = str_item_cache.Split('!');
                string[] strTextValue;
                ListItem item = new ListItem();
                foreach (string strItem in strArItem) {
                    item = new ListItem();
                    strTextValue = strItem.Split('|');
                    item.Text = strTextValue[0];
                    item.Value = strTextValue[1];
                    cb_Cate.Items.Add(item);
                }
            }
            else {
                #region Doi Voi truong hop chua luu cache
                LoadAllCat(cb_Cate, null, null);

                str_item_cache = "";
                foreach (ListItem item in cb_Cate.Items) {
                    str_item_cache += "!" + item.Text + "|" + item.Value;
                }

                if (str_item_cache.Trim() != "")
                    str_item_cache = str_item_cache.Substring(1);

                HttpContext.Current.Cache.Insert("bindAllCat_" + HttpContext.Current.User.Identity.Name, str_item_cache, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                #endregion
            }
        }

        public static void CleanCacheCategory() {
            #region Xóa cache lưu Category
            DataTable dtUser;
            using (DFISYS.User.Db.MainDB objDb = new DFISYS.User.Db.MainDB()) {
                dtUser = objDb.UserCollection.GetAllAsDataTable();
            }
            foreach (DataRow dr in dtUser.Rows) {
                HttpContext.Current.Cache.Remove("Treebuild_" + dr["User_ID"]);
                HttpContext.Current.Cache.Remove("Treebuild_strResult" + dr["User_ID"]);

                HttpContext.Current.Cache.Remove("TreebuildAllCat_" + dr["User_ID"]);
                HttpContext.Current.Cache.Remove("TreebuildAllCat_strResult" + dr["User_ID"]);

                HttpContext.Current.Cache.Remove("GetCatIDByUser_strResult" + dr["User_ID"]);

                HttpContext.Current.Cache.Remove("BindListCat_" + dr["User_ID"]);

                HttpContext.Current.Cache.Remove("bindAllCat_" + dr["User_ID"]);

                HttpContext.Current.Cache.Remove("CheckBoxList_" + dr["User_ID"]);

            }

            #endregion
        }

        public static ArrayList getCatInfo(int _catid) {
            DFISYS.Core.DAL.CategoryRow objrow;
            ArrayList objresult = new ArrayList();
            using (DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB()) {
                objrow = objDb.CategoryCollection.GetByPrimaryKey(_catid);
            }
            if (objrow != null) {
                objresult.Add(objrow.Cat_Name);
                objresult.Add(objrow.Cat_Description);
                objresult.Add(objrow.Cat_DisplayURL);
                objresult.Add(objrow.Cat_ParentID);
                if (!objrow.IsCat_isColumnNull)
                    objresult.Add(objrow.Cat_isColumn);
                if (!objrow.IsCat_isHiddenNull)
                    objresult.Add(objrow.Cat_isHidden);
                objresult.Add(objrow.Cat_Icon);
                objresult.Add(objrow.Cat_ViewNum);

                //SonPC
                objresult.Add(objrow.EditionType_ID);
                //SonPC
            }
            return objresult;
        }

        public static DFISYS.Core.DAL.CategoryRow getCatInfoAsCategoryRow(int _catid) {
            DFISYS.Core.DAL.CategoryRow objrow;
            ArrayList objresult = new ArrayList();
            using (DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB()) {
                objrow = objDb.CategoryCollection.GetByPrimaryKey(_catid);
            }
            if (objrow != null)
                return objrow;

            objrow = new DFISYS.Core.DAL.CategoryRow();
            return objrow;
        }


        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void CreateCat(string _catname, string _catdes, string _caturl, int _catparent, int _catedition, string _caticon, bool _catiscolumn, bool _catishide) {
            CleanCacheCategory();

            DFISYS.Core.DAL.CategoryRow objrow = new DFISYS.Core.DAL.CategoryRow();
            objrow.Cat_Name = _catname;
            objrow.Cat_Description = _catdes;
            objrow.Cat_DisplayURL = _caturl;
            objrow.Cat_ParentID = _catparent;
            objrow.EditionType_ID = _catedition;
            objrow.Cat_Icon = _caticon;
            objrow.Cat_isColumn = _catiscolumn;
            objrow.Cat_isHidden = _catishide;
            EditionTypeRow objEditRow = null;
            DFISYS.Core.DAL.CategoryRow objParentRow = null;
            DFISYS.Core.DAL.CategoryRow objrow_find_parent = null;

            DFISYS.Core.DAL.CategoryRow[] objTopRows;
            int intMaxOrder = 0;
            using (DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB()) {
                if (_catparent != 0) {
                    int cat_id_find_parent = GetCatParent(Convert.ToInt32(_catparent));
                    objrow_find_parent = objDb.CategoryCollection.GetByPrimaryKey(cat_id_find_parent);
                    _catedition = objrow_find_parent.EditionType_ID;
                }

                //if (_catparent == 0)
                //    objEditRow = objDb.EditionTypeCollection.GetByPrimaryKey(_catedition);
                //else
                //{
                objParentRow = objDb.CategoryCollection.GetByPrimaryKey(_catparent);
                //    objEditRow = objDb.EditionTypeCollection.GetByPrimaryKey(objParentRow.EditionType_ID);
                //}

                objEditRow = objDb.EditionTypeCollection.GetByPrimaryKey(_catedition);

                objTopRows = objDb.CategoryCollection.GetTopAsArray(1, "Cat_ParentID=" + _catparent, "Cat_Order DESC");
                if (objTopRows.Length == 1) {
                    if (objTopRows[0].IsCat_OrderNull)
                        intMaxOrder = 1;
                    else
                        intMaxOrder = objTopRows[0].Cat_Order + 1;
                }
                objrow.Cat_Order = intMaxOrder;
                objrow.EditionType_ID = _catedition;
                objDb.CategoryCollection.Insert(objrow);
            }

            #region Cập nhập vào bảng Category ở database User
            DFISYS.User.Db.CategoryRow objCateUserRow = new DFISYS.User.Db.CategoryRow();
            objCateUserRow.Cat_ID = objrow.Cat_ID;
            objCateUserRow.Cat_Name = objrow.Cat_Name;
            objCateUserRow.Cat_ParentID = objrow.Cat_ParentID;
            objCateUserRow.Cat_Order = objrow.Cat_Order;
            objCateUserRow.Channel_ID = DFISYS.API.Config.CurrentChannel;
            using (DFISYS.User.Db.MainDB objDb = new DFISYS.User.Db.MainDB()) {
                objDb.CategoryCollection.Insert(objCateUserRow);
            }
            #endregion

            //cap nhat thong tin ve tab moi khi them cat
            string strEditionRef = "";
            string strParentTabRef = "";
            if (objEditRow != null) {
                //strEditionRef = objEditRow.EditionDisplayURL;
                //SonPC
                String UseEditionType = "false";
                if (UseEditionType.ToLower().IndexOf("false") >= 0)
                    strEditionRef = "home";
                else
                    strEditionRef = objEditRow.EditionDisplayURL;

                //SonPC
            }
            if (objParentRow != null) {
                strParentTabRef = objParentRow.Cat_DisplayURL;
            }
            //thuc hien lay doi tuong GenTabHelper
            DFISYS.BO.Gentabhelper objHelp = new Gentabhelper();
            objHelp.AddCategoryTab(strEditionRef, strParentTabRef, _caturl, _catname);

        }
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void UpdateCate(int _catid, string _catname,string catname_en, string _catdes,string catdes_en, string _caturl, int _catparent,int catorder, int _catedition, string _caticon, bool _catiscolumn, bool _catishide,bool isActive) {

            CleanCacheCategory();

            DFISYS.Core.DAL.CategoryRow objrow;
            DFISYS.Core.DAL.EditionTypeRow objEditRow = null;
            DFISYS.Core.DAL.CategoryRow objParentRow = null;
            DFISYS.Core.DAL.CategoryRow objrow_find_parent = null;

            string strOldUrl = "";
            using (DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB()) {
                objrow = objDb.CategoryCollection.GetByPrimaryKey(_catid);
                objParentRow = objDb.CategoryCollection.GetByPrimaryKey(objrow.Cat_ParentID);
                objEditRow = objDb.EditionTypeCollection.GetByPrimaryKey(objrow.EditionType_ID);

                if (_catparent != 0) {
                    int cat_id_find_parent = GetCatParent(Convert.ToInt32(_catparent));
                    objrow_find_parent = objDb.CategoryCollection.GetByPrimaryKey(cat_id_find_parent);
                    _catedition = objrow_find_parent.EditionType_ID;
                }
            }
            if (objrow != null) {
                strOldUrl = objrow.Cat_DisplayURL;
                objrow.Cat_Name = _catname;
                objrow.Cat_Description = _catdes;
                objrow.Cat_DisplayURL = _caturl;
                objrow.Cat_ParentID = _catparent;
                objrow.EditionType_ID = _catedition;
                //objrow.EditionType_ID = objrow_find_parent.EditionType_ID;
                if (_caticon != null && _caticon != "")
                    objrow.Cat_Icon = _caticon;
                objrow.Cat_Order = catorder;
                objrow.Cat_isColumn = _catiscolumn;
                objrow.Cat_isHidden = _catishide;
                objrow.Cat_Name_En = catname_en;
                objrow.Cat_Description_En = catdes_en;
                objrow.IsActive = isActive;
                using (DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB()) {
                    objDb.CategoryCollection.Update(objrow);
                }

                //cap nhat lai tabref neu co su thay doi ten o displayUrl
                //string strEditionRef = "";
                //string strParentTabRef = "";
                //string strNewRef = "";
                //string strOldRef = "";
                //if (objEditRow != null) {
                //    String UseEditionType = System.Configuration.ConfigurationSettings.AppSettings["UseEditionType"];
                //    if (UseEditionType.ToLower().IndexOf("false") >= 0)

                //        strEditionRef = System.Configuration.ConfigurationSettings.AppSettings["PrefixEditionType"];
                //    else
                //        strEditionRef = objEditRow.EditionDisplayURL;



                //    strOldRef = strEditionRef + ".";
                //    strNewRef = strEditionRef + ".";
                //}
                //if (objParentRow != null) {
                //    strParentTabRef = objParentRow.Cat_DisplayURL;
                //    strOldRef = strOldRef + "." + strParentTabRef + ".";
                //    strNewRef = strNewRef + "." + strParentTabRef + ".";
                //}
                //strOldRef = strOldRef + strOldUrl;
                //strNewRef = strNewRef + _caturl;
                //DFISYS.BO.Gentabhelper objHelp = new Gentabhelper();
                //objHelp.SyncCategoryTab(strOldRef, strNewRef, _catname, strEditionRef, strParentTabRef, _caturl, _catname);

                //#region Cập nhập vào bảng Category ở database User
                //DFISYS.User.Db.CategoryRow objCateUserRow = new DFISYS.User.Db.CategoryRow();
                //using (DFISYS.User.Db.MainDB objDb = new DFISYS.User.Db.MainDB()) {
                //    objCateUserRow = objDb.CategoryCollection.GetByPrimaryKey(objrow.Cat_ID);
                //}
                //objCateUserRow.Cat_Name = objrow.Cat_Name;
                //objCateUserRow.Cat_ParentID = objrow.Cat_ParentID;
                //objCateUserRow.Cat_Order = objrow.Cat_Order;
                //objCateUserRow.Channel_ID = DFISYS.API.Config.CurrentChannel;
                //using (DFISYS.User.Db.MainDB objDb = new DFISYS.User.Db.MainDB()) {
                //    objDb.CategoryCollection.Update(objCateUserRow);
                //}

                //#endregion
            }
        }

        public static string GetChildCatIdByCatParentId(int cat_Parent_Id) {
            DataTable dt;
            string strCatId = "";
            using (DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB()) {
                dt = objDb.CategoryCollection.GetAsDataTable(" Cat_ParentID = " + cat_Parent_Id.ToString(), "");
            }

            foreach (DataRow dr in dt.Rows) {
                strCatId += "," + dr["Cat_ID"];
            }
            if (strCatId.Trim() != "")
                strCatId = strCatId.Substring(1);

            return strCatId;
        }

        public static string GetTabReferenceByCatId(int catId) {
            string tab = string.Empty;

            DFISYS.Core.DAL.MainDB db = new DFISYS.Core.DAL.MainDB();
            DFISYS.Core.DAL.CategoryRow category = db.CategoryCollection.GetByPrimaryKey(catId);
            if (category != null) {
                tab = category.Cat_DisplayURL;
                while (category.Cat_ParentID != 0) {
                    category = db.CategoryCollection.GetByPrimaryKey(category.Cat_ParentID);
                    if (category != null) tab = category.Cat_DisplayURL + "." + tab;
                }
                tab = db.EditionTypeCollection.GetByPrimaryKey(category.EditionType_ID).EditionDisplayURL + "." + tab;
            }

            return tab;
        }

        public static bool Delete(int catId) {
            CleanCacheCategory();
            using (DFISYS.Core.DAL.MainDB db = new DFISYS.Core.DAL.MainDB())
                db.StoredProcedures.Category_Delete(catId);
             
            return true;
        }

        public static DataTable GetCategoriesByParent(int parentId) {
            DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB();
            return objDb.CategoryCollection.GetAsDataTable(" Cat_ParentID = " + parentId, "Cat_Order asc");
        }

        public static DataTable GetCategoryByID(int ID)
        {
            DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB();
            return objDb.CategoryCollection.GetAsDataTable(" Cat_ID = " + ID, "Cat_Order asc");
        }

        public static DataTable GetCategoriesByParentAndEditionType(int parentId, string editionType)
        {
            DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB();
            return objDb.CategoryCollection.GetAsDataTable("Cat_ParentID = " + parentId + " and EditionType_ID = " + editionType, "Cat_Order asc");
        }

        public static DFISYS.Core.DAL.CategoryRow CreateCat2(string name,string name_En, string keyWords, int order, string description,string description_en, string displayURL, int parentCatId, int editionId, string icon, bool isColumn, bool isHidden, string tabCloneRef,bool isActive) {
            CleanCacheCategory();
            #region Cập nhật vào bảng Category ở database dữ liệu chính [DFISYS.Core.DAL.CategoryRow]
            DFISYS.Core.DAL.CategoryRow objrow = new DFISYS.Core.DAL.CategoryRow();
            objrow.Cat_Name = name;
            objrow.Cat_KeyWords = keyWords;
            objrow.Cat_Description = description;
            objrow.Cat_DisplayURL = displayURL;
            objrow.Cat_ParentID = parentCatId;
            objrow.EditionType_ID = editionId;
            objrow.Cat_Icon = icon;
            objrow.Cat_Order = order;
            objrow.Cat_isColumn = isColumn;
            objrow.Cat_isHidden = isHidden;
            objrow.Cat_Name_En = name_En;
            objrow.Cat_Description_En = description_en;
            objrow.IsActive = isActive;
            DFISYS.Core.DAL.MainDB db = new DFISYS.Core.DAL.MainDB();
            db.CategoryCollection.Insert(objrow);

            #endregion

            

            return objrow;
        }

        public static bool UpdateCategoryRow(DFISYS.Core.DAL.CategoryRow row) {
            CleanCacheCategory();

            using (DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB()) {
                objDb.CategoryCollection.Update(row);
            }


            return true;
        }

        public static int GetCatParent(Int32 Cate_ID) {
            DFISYS.Core.DAL.CategoryRow objrow;
            using (DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB()) {
                objrow = objDb.CategoryCollection.GetByPrimaryKey(Cate_ID);
                if (objrow.Cat_ParentID == 0)
                    return Cate_ID;
                else
                    return GetCatParent(objrow.Cat_ParentID);
            }
        }

        public static bool CheckUpdateCate(int Cate_ID, int NewsParent, bool IsFirst) {
            if (NewsParent == 0)
                return true;
            if (IsFirst && (Cate_ID == NewsParent))
                return false;
            using (DFISYS.Core.DAL.MainDB db = new DFISYS.Core.DAL.MainDB()) {
                DFISYS.Core.DAL.CategoryRow objrow;
                objrow = db.CategoryCollection.GetByPrimaryKey(NewsParent);
                if (Cate_ID == objrow.Cat_ParentID)
                    return false;
                else return CheckUpdateCate(Cate_ID, objrow.Cat_ParentID, false);
            }
        }

        public static DataTable GetListCategory(string strNotInCatId) {
            strNotInCatId = "-1";
            if (strNotInCatId.Trim() == "")
                strNotInCatId = "-1";

            DataTable dtCat = new DataTable();
            DataTable objresult = new DataTable();
            DataTable dtz = new DataTable();
            DataTable dt3 = new DataTable();
            object[] objarr = null;
            DFISYS.Core.DAL.MainDB objDbCore = new DFISYS.Core.DAL.MainDB();

            try {
                dtCat = objDbCore.FrontEndStoreProcedure.Category_GetListByWhere(" Where (Cat_ParentID = 0 or Cat_ParentID is null) AND Cat_ID NOT IN (" + strNotInCatId + ") ", "");
                dtCat.Columns.Add(new DataColumn("ID"));
                dtCat.Columns.Add(new DataColumn("Title"));
                objresult = dtCat.Clone();

                foreach (DataRow objrow in dtCat.Rows) {
                    objarr = objrow.ItemArray;
                    objarr.SetValue(objrow["Cat_ID"], objarr.Length - 2);
                    objarr.SetValue(objrow["Cat_Name"], objarr.Length - 1);
                    objresult.Rows.Add(objarr);

                    // Get cac chuyen muc con
                    dtz = objDbCore.FrontEndStoreProcedure.Category_GetListByWhere(" Where Cat_ParentID = " + objrow["Cat_ID"] + " AND Cat_ID NOT IN (" + strNotInCatId + ") ", "");
                    dtz.Columns.Add(new DataColumn("ID"));
                    dtz.Columns.Add(new DataColumn("Title"));

                    foreach (DataRow drz in dtz.Rows) {
                        drz["Cat_Name"] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drz["Cat_Name"];
                        objarr = drz.ItemArray;
                        objarr.SetValue(drz["Cat_ID"], objarr.Length - 2);
                        objarr.SetValue(drz["Cat_Name"], objarr.Length - 1);
                        objresult.Rows.Add(objarr);

                        #region Chuyen muc con cap 3
                        // Get cac chuyen muc con cap 3
                        dt3 = objDbCore.FrontEndStoreProcedure.Category_GetListByWhere(" Where Cat_ParentID = " + drz["Cat_ID"] + " AND Cat_ID NOT IN (" + strNotInCatId + ") ", "");
                        dt3.Columns.Add(new DataColumn("ID"));
                        dt3.Columns.Add(new DataColumn("Title"));

                        if (dt3.Rows.Count > 0) {
                            foreach (DataRow dr3 in dt3.Rows) {
                                dr3["Cat_Name"] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr3["Cat_Name"];
                                objarr = dr3.ItemArray;
                                objarr.SetValue(dr3["Cat_ID"], objarr.Length - 2);
                                objarr.SetValue(dr3["Cat_Name"], objarr.Length - 1);
                                objresult.Rows.Add(objarr);
                            }
                        }
                        #endregion


                    }
                }
            }

            catch (Exception e) {
            }

            dtCat.Dispose();
            dtz.Dispose();
            return objresult;
        }


        public static void CleanCachePermission() {
            #region Permission
            DataTable dtUser;
            using (DFISYS.User.Db.MainDB objDb = new DFISYS.User.Db.MainDB()) {
                dtUser = objDb.UserCollection.GetAllAsDataTable();
            }
            foreach (DataRow dr in dtUser.Rows) {
                HttpContext.Current.Cache.Remove("GetPermission1_" + dr["User_ID"]);

            }

            #endregion
        }

        public static DFISYS.Core.DAL.CategoryRow DetailsCategory(int CatID) {
            DFISYS.Core.DAL.CategoryRow objrow = null;
            using (DFISYS.Core.DAL.MainDB objDb = new DFISYS.Core.DAL.MainDB()) {
                objrow = objDb.CategoryCollection.GetByPrimaryKey(CatID);
            }

            return objrow;
        }
    }
}
