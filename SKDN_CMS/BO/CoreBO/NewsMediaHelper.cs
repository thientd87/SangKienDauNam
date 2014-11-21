using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using DFISYS.Core.DAL;
using System.ComponentModel;
namespace DFISYS.CoreBO
{
	public class NewsMediaHelper
	{
		public NewsMediaHelper() { }
        //#region GetMediaObjlist dung store
        //[DataObjectMethod(DataObjectMethodType.Select)]
        //public DataTable GetMediaObjlist(string strWhere, int PageSize, int StartRow)
        //{
        //    //lay thong tin ve menh de where de loc thong tin
        //    if (strWhere == null)
        //        strWhere = "";
        //    DataTable objresult;
        //    int intPageNum = StartRow / PageSize + 1;
        //    //lay duoc du lieu cua tat ca nhung thang co trang thai la status can lay
        //    using (MainDB objdb = new MainDB())
        //    {
        //        //objresult = objdb.MediaObjectCollection.GetPageAsDataTable(intPageNum, PageSize, strWhere, "Object_ID DESC"); ;
        //        objresult = objdb.StoredProcedures.vc_MediaObject_SelectList(strWhere, StartRow, PageSize);
        //    }
        //    objresult.Columns.Add(new DataColumn("News_ID", Type.GetType("System.String")));
        //    objresult.Columns.Add(new DataColumn("Film_ID", Type.GetType("System.String")));

        //    int pos = 0;

        //    DataTable dt;
        //    for (int i = 0; i < objresult.Rows.Count; i++)
        //    {
        //        pos = objresult.Rows[i]["Object_Url"].ToString().LastIndexOf("/");
        //        objresult.Rows[i]["Object_Url"] = objresult.Rows[i]["Object_Url"].ToString().Substring(pos + 1);
        //        // news_id = 
        //        using (MainDB objMainDB = new MainDB())
        //        {
        //            //dt = objMainDB.SelectQuery(" Select News_ID,Film_Id from news_media Where Object_Id =  " + objresult.Rows[i]["Object_ID"] + " ");
        //            dt = objMainDB.StoredProcedures.vc_Execute_Sql(" Select News_ID,Film_Id from news_media Where Object_Id =  " + objresult.Rows[i]["Object_ID"] + " ");
        //        }

        //        if (dt.Rows.Count > 0)
        //        {
        //            objresult.Rows[i]["News_ID"] = dt.Rows[0][0];
        //            objresult.Rows[i]["Film_ID"] = dt.Rows[0][1];
        //        }
        //    }

        //    DataTable objTemp = objresult.Clone();
        //    if (objresult.Rows.Count == 0)
        //    {
        //        DataRow objrow = objTemp.NewRow();
        //        objrow["Object_ID"] = 0;
        //        objrow["Object_Type"] = 0;
        //        objrow["Object_Value"] = "";
        //        objrow["Object_Url"] = "";
        //        objrow["Object_Note"] = "";

        //        objrow["UserID"] = HttpContext.Current.User.Identity.Name;

        //        //SonPC Modified
        //        objrow["News_ID"] = "";
        //        objrow["Film_ID"] = "";
        //        //SonPC Modified

        //        objTemp.Rows.Add(objrow);
        //        objresult.Dispose();
        //        return objTemp;
        //    }
        //    return objresult;
        //}
        //public DataTable GetMediaObjlist_Old(string strWhere, int PageSize, int StartRow)
        //{
        //    //lay thong tin ve menh de where de loc thong tin
        //    if (strWhere == null)
        //        strWhere = "";
        //    DataTable objresult;
        //    int intPageNum = StartRow / PageSize + 1;
        //    //lay duoc du lieu cua tat ca nhung thang co trang thai la status can lay
        //    using (MainDB objdb = new MainDB())
        //    {
        //        objresult = objdb.MediaObjectCollection.GetPageAsDataTable(intPageNum, PageSize, strWhere, "Object_ID DESC"); ;
        //    }

        //    objresult.Columns.Add(new DataColumn("News_ID", Type.GetType("System.String")));
        //    objresult.Columns.Add(new DataColumn("Film_ID", Type.GetType("System.String")));

        //    int pos = 0;

        //    DataTable dt;
        //    for (int i = 0; i < objresult.Rows.Count; i++)
        //    {
        //        pos = objresult.Rows[i]["Object_Url"].ToString().LastIndexOf("/");
        //        objresult.Rows[i]["Object_Url"] = objresult.Rows[i]["Object_Url"].ToString().Substring(pos + 1);
        //        // news_id = 
        //        using (MainDB objMainDB = new MainDB())
        //        {
        //            dt = objMainDB.SelectQuery(" Select News_ID,Film_Id from news_media Where Object_Id =  " + objresult.Rows[i]["Object_ID"] + " ");
        //        }

        //        if (dt.Rows.Count > 0)
        //        {
        //            objresult.Rows[i]["News_ID"] = dt.Rows[0][0];
        //            objresult.Rows[i]["Film_ID"] = dt.Rows[0][1];
        //        }



        //    }

        //    DataTable objTemp = objresult.Clone();
        //    if (objresult.Rows.Count == 0)
        //    {
        //        DataRow objrow = objTemp.NewRow();
        //        objrow["Object_ID"] = 0;
        //        objrow["Object_Type"] = 0;
        //        objrow["Object_Value"] = "";
        //        objrow["Object_Url"] = "";
        //        objrow["Object_Note"] = "";
        //        objrow["UserID"] = HttpContext.Current.User.Identity.Name;
        //        objTemp.Rows.Add(objrow);
        //        objresult.Dispose();
        //        return objTemp;
        //    }
        //    return objresult;
        //}
        //#endregion
	 
        //#region GetMediaRowsCount dung store
        //public int GetMediaRowsCount(string strWhere)
        //{
        //    if (strWhere == null)
        //        strWhere = "";
        //    DataTable objresult;
        //    using (MainDB objdb = new MainDB())
        //    {
        //        //objresult = objdb.MediaObjectCollection.GetAsDataTable(strWhere, ""); ;
        //        objresult = objdb.StoredProcedures.vc_MediaObject_SelectList_Count(strWhere);
        //    }
        //    if (objresult.Rows.Count == 0)
        //        return 0;
        //    else return Convert.ToInt32(objresult.Rows[0][0]);
        //    //return objresult.Rows.Count;
        //}
		public int GetMediaRowsCount_Old(string strWhere)
		{
			if (strWhere == null)
				strWhere = "";
			DataTable objresult;
			using (MainDB objdb = new MainDB())
			{
				objresult = objdb.MediaObjectCollection.GetAsDataTable(strWhere, ""); ;
			}
			if (objresult.Rows.Count == 0)
				return 1;
			return objresult.Rows.Count;
		}
		//#endregion
		[DataObjectMethod(DataObjectMethodType.Select)]
		public DataTable getPage(int numPage)
		{
			int intPagenum = numPage;
			DataTable objTb = new DataTable();
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
		[DataObjectMethod(DataObjectMethodType.Insert)]
		public void createMediaObject(string _obj_url, int _obj_type, string _obj_note, string _obj_user, string strNewsId, string strFilmId)
		{
			MediaObjectRow objrow = new MediaObjectRow();
			objrow.Object_Note = _obj_note;
			objrow.Object_Type = _obj_type;
			if (_obj_url != "" && _obj_url != null)
			{
				string strType = "Picture";
				if (_obj_type != 1)
					strType = "Video";
				_obj_url = "images/uploaded/Share/Media/" + strType + "/" + _obj_url;
				objrow.Object_Url = _obj_url;
			}
			objrow.UserID = _obj_user;
			using (MainDB objdb = new MainDB())
			{
				objdb.MediaObjectCollection.Insert(objrow);
			}

			// Khi upload moi thi update luon vao Object nay cho bai viet nay
			if (strNewsId != null)
			{
				News_MediaRow objMrow = null;
				using (MainDB objDb = new MainDB())
				{
					objMrow = new News_MediaRow();
					objMrow.News_ID = Convert.ToInt64(strNewsId);
					objMrow.Object_ID = objrow.Object_ID;
					objDb.News_MediaCollection.Insert(objMrow);

				}
			}
			else
				if (strFilmId != null)
				{
					News_MediaRow objMrow = null;
					using (MainDB objDb = new MainDB())
					{
						objMrow = new News_MediaRow();
						objMrow.Film_ID = strFilmId;
						objMrow.Object_ID = objrow.Object_ID;
						objDb.News_MediaCollection.Insert(objMrow);

					}
				}
				else
				{
					HttpContext.Current.Session["Object_Id"] = HttpContext.Current.Session["Object_Id"] + "," + objrow.Object_ID.ToString();
				}
		}
		[DataObjectMethod(DataObjectMethodType.Update)]
		public void UpdateMediaObj(int _obj_id, int _obj_type, string _obj_note)
		{
			MediaObjectRow objrow;
			using (MainDB objDb = new MainDB())
			{
				objrow = objDb.MediaObjectCollection.GetByPrimaryKey(_obj_id);
			}
			if (objrow != null)
			{
				objrow.Object_Type = _obj_type;
				objrow.Object_Note = _obj_note;
				//if (_obj_url != "" && _obj_url != null)
				//{
				//    string strFolder = "images/uploaded/Share/Media/";
				//    if (objrow.Object_Type != 1)
				//        strFolder += "Video/";
				//    else
				//        strFolder += "Picture/";

				//    Portal.FileHelper.DelImgFile(strFolder, _obj_url);

				//    objrow.Object_Url = strFolder + _obj_url;
				//    //thuc hien xoa file cu
				//}
				using (MainDB objDb = new MainDB())
				{
					objDb.MediaObjectCollection.Update(objrow);
				}

			}
		}

		#region Delete_News_Media_ByNewsIdAndObjectId Da Chuyen Sang Store
		public static void Delete_News_Media_ByNewsIdAndObjectId(long _news_id, string _strMediaId)
		{
			using (MainDB objDb = new MainDB())
			{
				//objDb.SelectQuery(" Delete News_Media Where news_id = " + _news_id + " And Object_Id = " + _strMediaId + " And (Film_Id Is Null Or Film_ID = -1 ) ");
				objDb.StoredProcedures.vc_Execute_Sql(" Delete News_Media Where news_id = " + _news_id + " And Object_Id = " + _strMediaId + " And (Film_Id Is Null Or Film_ID = -1 ) ");
			}
		}
		public static void Delete_News_Media_ByNewsIdAndObjectId(long _news_id, string _strMediaId, MainDB objDb)
		{
			objDb.StoredProcedures.vc_Execute_Sql(" Delete News_Media Where news_id = " + _news_id + " And Object_Id = " + _strMediaId + " And (Film_Id Is Null Or Film_ID = -1 ) ");
		}
		#endregion
		#region Get_News_Media_By_NewsId Da chuyen sang store
		public static DataTable Get_News_Media_By_NewsId(long _news_id)
		{
			DataTable result;
			using (MainDB objDb = new MainDB())
			{
				//result = objDb.SelectQuery(" Select * From News_Media Where news_id = " + _news_id + " And Object_Id >0 And (Film_Id IS NULL OR Film_ID = -1) ");
				result = objDb.StoredProcedures.vc_Execute_Sql(" Select * From News_Media Where news_id = " + _news_id + " And Object_Id >0 And (Film_Id IS NULL OR Film_ID = -1) ");
			}
			return result;
		}
		#endregion
		#region Get_ObjectId_By_NewsId da chuyen sang store
		public static string Get_ObjectId_By_NewsId(long _news_id)
		{
			DataTable result = Get_News_Media_By_NewsId(_news_id);
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
		#endregion
		#region Get_ObjectId_By_FilmId da chuyen sang store
		public static string Get_ObjectId_By_FilmId(int film_id)
		{
			DataTable result;
			using (MainDB objDb = new MainDB())
			{
				//result = objDb.SelectQuery(" select Object_ID from news_media Where Film_ID = " + film_id + " And Object_ID > 0 And (News_ID IS NULL OR News_ID = -1) ");
				result = objDb.StoredProcedures.vc_Execute_Sql(" select Object_ID from news_media Where Film_ID = " + film_id + " And Object_ID > 0 And (News_ID IS NULL OR News_ID = -1) ");
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
		#endregion
		#region Check_Exist_News_Media_ByFilmId da chuyen sang store
		public static bool Check_Exist_News_Media_ByFilmId(string _strFilm_Id, string _strObject_Id)
		{
			DataTable result;
			using (MainDB objDb = new MainDB())
			{
				//result = objDb.SelectQuery(" Select count(*) From News_Media Where Film_Id = " + _strFilm_Id + " And Object_Id = " + _strObject_Id + " And (news_id IS NULL OR news_id = -1) ");
				result = objDb.StoredProcedures.vc_Execute_Sql(" Select count(*) From News_Media Where Film_Id = " + _strFilm_Id + " And Object_Id = " + _strObject_Id + " And (news_id IS NULL OR news_id = -1) ");
			}
			return (result.Rows[0][0].ToString() == "1");
		}
		#endregion
		#region Check_Exist_News_Media_ByNewsId da chuyen sang store
		public static bool Check_Exist_News_Media_ByNewsId(string _strNews_id, string _strObject_Id)
		{
			DataTable result;
			using (MainDB objDb = new MainDB())
			{
				//result = objDb.SelectQuery(" Select count(*) From News_Media Where news_id = " + _strNews_id + " And Object_Id = " + _strObject_Id + " And (Film_Id IS NULL OR Film_ID = -1) ");
				result = objDb.StoredProcedures.vc_Execute_Sql(" Select count(*) From News_Media Where news_id = " + _strNews_id + " And Object_Id = " + _strObject_Id + " And (Film_Id IS NULL OR Film_ID = -1) ");
			}
			return (result.Rows[0][0].ToString() == "1");
		}
		#endregion
		#region Check_Exist_News_Media_ByObjectId da chuyen sang store
		public static bool Check_Exist_News_Media_ByObjectId(string _strObject_Id)
		{
			DataTable result;
			using (MainDB objDb = new MainDB())
			{
				//result = objDb.SelectQuery(" Select count(*) From News_Media Where Object_Id = " + _strObject_Id );
				result = objDb.StoredProcedures.vc_Execute_Sql(" Select count(*) From News_Media Where Object_Id = " + _strObject_Id);
			}
			return (result.Rows[0][0].ToString() == "1");
		}
		#endregion
		#region DeleteNews_Media_Film_Object_By_FilmIdAndObjectId da chuyen sang store
		public static void DeleteNews_Media_Film_Object_By_FilmIdAndObjectId(int filmId, int object_id)
		{
			using (MainDB objDb = new MainDB())
			{
				//objDb.SelectQuery(" Delete News_Media Where Film_ID = " + filmId.ToString() + " And Object_ID = " + object_id + " AND (News_ID = -1 OR News_ID IS NULL ) ");
				objDb.StoredProcedures.vc_Execute_Sql(" Delete News_Media Where Film_ID = " + filmId.ToString() + " And Object_ID = " + object_id + " AND (News_ID = -1 OR News_ID IS NULL ) ");
			}
		}
		#endregion

		[DataObjectMethod(DataObjectMethodType.Delete)]
		public void DelMediaObj(string _selected_id)
		{
			if (_selected_id != null)
			{
				if (_selected_id.IndexOf(",") <= 0)
				{
					//DeleteFile(_selected_id);
					try
					{
						int intid = Convert.ToInt32(_selected_id);
						using (MainDB objDB = new MainDB())
						{

							objDB.MediaObjectCollection.DeleteByPrimaryKey(intid);

						}
					}
					catch { }
				}
				else
				{
					string[] objTempSel = _selected_id.Split(',');
					//foreach (string temp in objTempSel)
					//{
					//    DeleteFile(temp.Trim());
					//}
					try
					{
						using (MainDB objDB = new MainDB())
						{
							objDB.MediaObjectCollection.Delete("Object_ID in (" + _selected_id + ")");
						}
					}
					catch { }
				}
			}

		}
	}
}
