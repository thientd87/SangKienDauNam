using System;
using System.Collections.Generic;
using System.Text;
//using MemcachedProviders.Cache;

namespace DFISYS
{
    public class LogAction
    {
        public const int LogType_BaiNoiBat = 1;
        public const int LogType_BaiViet = 2;
        public const int LogType_BaiViet_XB = 3;
        public const int LogType_Tag = 4;
        public const int LogType_Thread = 5;
        

        public const string LogAction_Sua = "Bài viết đã được sửa";
        public const string LogAction_Tao_Gui_ChoBT = "Tạo bài viết và gửi lên chờ biên tập";
        public const string LogAction_Tao_Gui_ChoDuyet = "Tạo bài viết và gửi lên chờ duyệt";
        public const string LogAction_Tao = "Bài viết đã được tạo";
        public const string LogAction_TaoXB = "Bài viết đã được tạo và xuất bản";
        public const string LogAction_SuaGuiChoBT = "Sửa và gửi lên chờ biên tập";
        public const string LogAction_SuaGuiChoDuyet = "Sửa và gửi lên chờ duyệt";
        public const string LogAction_NhanBaiBT = "Sửa và nhận bài biên tập";
        public const string LogAction_NhanBaiDuyet = "Sửa và nhận bài duyệt";
        public const string LogAction_SuaXB = "Sửa và xuất bản bài";
        public const string LogAction_SuaBaiXB = "Sửa bài xuất bản";
        public const string LogAction_SuaBai = "Sửa bài";
        public const string LogAction_GuiChoBT = "Gửi lên chờ biên tập";
        public const string LogAction_GuiChoDuyet = "Gửi lên chờ duyệt";
        public const string LogAction_XB = "Xuất bản bài";
        public const string LogAction_TraLai = "Trả lại bài";
        public const string LogAction_XoaTam = "Xóa tạm bài";
        public const string LogAction_GuiBai = "Gửi bài";
        public const string LogAction_GoBai = "Gỡ bài";

        
        public const string LogAction_LuongSuKien_Sua = "Sửa luồng sự kiện";
        public const string LogAction_LuongSuKien_Xoa = "Xóa luồng sự kiện";
        public const string LogAction_LuongSuKien_TinNong = "Chọn làm tin nóng";
        public const string LogAction_LuongSuKien_SuKienChinh = "Chọn làm sự kiện chính";
        public const string LogAction_LuongSuKien_TinTuyenDung = "Chọn làm tin tuyển dụng";
        public const string LogAction_LuongSuKien_TinDuHoc = "Chọn làm tin du học";
        public const string LogAction_LuongSuKien_BaiLuongSuKien_Xoa = "Xóa bài trong luồng sự kiện";
        public const string LogAction_LuongSuKien_BaiLuongSuKien_ThemMoi = "Thêm mới bài trong luồng sự kiện";
        public const string LogAction_3BaiNoiBat_Sua = "<b>Sửa 3 bài nổi bật</b> bởi ";

        public const string LogAction_Edit_Title = "Sửa tiêu đề";
        public const string LogAction_Edit_Sapo = "Sửa sapo";
        public const string LogAction_Edit_Content = "Sửa nội dung";
        public const string LogAction_Edit_Image = "Sửa avatar";
        public const string LogAction_Edit_Other = "Sửa others";


        public const string LogAction_Tag_CreateTag = "Tạo tag";
        public const string LogAction_Tag_UpdateTag = "Sửa tag";
        public const string LogAction_Tag_DeleteTag = "Xóa tag";

        public const string LogAction_Thread_CreateThread = "Tạo dòng sự kiện";
        public const string LogAction_Thread_UpdateThread = "Sửa dòng sự kiện";
        public const string LogAction_Thread_DeleteThread = "Xóa dòng sự kiện";
        public const string LogAction_Thread_UpdateOrderThread = "Cập nhật STT dòng sự kiện";
        public const string LogAction_ThreadDetail_Add = "Gán bài vào dòng sự kiện";
        public const string LogAction_ThreadDetail_Delete = "Xóa bài ở dòng sự kiện";

        public LogAction()
        {
        }

        //public static void InsertMemCache(string User, DateTime LogDate, string Action, int Type, Int64 Object_ID)
        //{
        //    try
        //    {
        //        object[] objMem = new object[] { LogDate, Action, Type, Object_ID, User };
        //        string memName = System.Configuration.ConfigurationManager.AppSettings["Channel"] + "_" + User.ToLower();
        //        object memCache = DistCache.Get(memName);
        //        if (memCache == null)
        //            memCache = new List<object>();
        //        ((List<object>)memCache).Add(objMem);
        //        DistCache.Add(memName, memCache);
        //    }
        //    catch { }
        //}

    }
}
