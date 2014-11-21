using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Portal.BO.Editoral.LogNews
{

    public enum LogType : byte
    { 
        News = 1,
        Vote = 2,
        Login = 3,
        FlowEvent = 4,
        Comment = 5,
        PhotoCategory = 6,
        PhotoAlbum = 7,
        PhotoDetail = 8
    }

    public class LogListName
    {
        public static String TEMPLIST = "Danh sách bài lưu tạm, ";
        public static String SENDLIST = "Danh sách bài đã gửi chờ biên tập, ";
        public static String SENDAPPROVALLIST = "Danh sách bài gửi chờ duyệt, ";
        public static String DELLLIST = "Danh sách bài xóa tạm, ";
        public static String EDITWAITLIST = "Danh sách bài chờ biên tập, ";
        public static String EDITINGLIST = "Danh sách bài biên tập, ";
        public static String APPROVINGLIST = "Danh sách bài nhận duyệt, ";
        public static String APPROVINGWAITINGLIST = "Danh sách bài chờ duyệt, ";
        public static String PUBLISHEDLISH = "Danh sách bài đã xuất bản, ";
        public static String BACKLIST = "Danh sách bài trả lại, ";
        public static String REMOVELIST = "Danh sách bài đã gỡ bỏ, ";

        public static String FLOW_EVENT = "Luồng sự kiện, ";
        public static String COMMENT = "Comment, ";
        public static String VOTE = "Bình chọn, ";
    }

    public class Log
    {
        public String GetLogType(int logType)
        {
            String strLogType = String.Empty;
            switch (logType)
            { 
                case 1:
                    strLogType = "News";
                    break;
                case 2:
                    strLogType = "Vote";
                    break;
                default:
                    strLogType = "Login";
                    break;
            }
            return strLogType;
        }

        public static String LOGIN = "Login";
        public static String LOGOUT = "Logout";

        public static String PHOTO_CAPNHAT_CHUTHICH = "Cập nhật chú thích '{0}' ảnh cho album '{1}'";
        public static String PHOTO_XOA = "Xóa {0} ảnh của album '{1}'";
        public static String PHOTO_THEM = "Thêm {0} ảnh cho album '{1}'";

        public static String THEMMOI_PHOTOCATEGORY = "Thêm mới Photo Category tiêu đề '{0}'";
        public static String CAPNHAT_PHOTOCATEGORY = "Cập nhật Photo Category tiêu đề '{0}'";
        public static String XOA_PHOTOCATEGORY = "Xóa Photo Category tiêu đề '{0}'";

        public static String THEM_CAUHOI_VOTE = "Thêm câu hỏi '{0}' cho bình chọn '{1}'";
        public static String SUA_CAUHOI_VOTE = "Sửa câu hỏi '{0}' cho bình chọn '{1}'";
        public static String XOA_CAUHOI_VOTE = "Xóa câu hỏi '{0}' cho bình chọn '{1}'";

        public static String CAPNHAT_FLOWEVENT = "Cập nhật luồng sự kiện '{0}'";
        public static String THEMMOI_FLOWEVENT = "Thêm mới luồng sự kiện '{0}'";
        public static String XOA_FLOAEVENT = "Xóa luồng sự kiện '{0}'";

        public static String DUATIN_FLOWEVENT = "Đưa tin '{0}' vào luồng sự kiện '{1}'";
        public static String XOABAI_FLOWEVENT = "Xóa tin '{0}' ra khỏi luồng sự kiện '{1}'";

        public static String CAPNHAT_VOTE = "Cập nhật bình chọn tiêu đề '{0}'";
        public static String XOA_VOTE = "Xóa bình chọn tiêu đề '{0}'";
        public static String THEM_VOTE = "Thêm mới bình chọn tiêu đề '{0}'";

        public static String XOA_COMMENT = "Xóa comment của bài viết '{0}, người gửi '{1}'";
        public static String CAPNHAT_COMMENT = "Cập nhật comment của bài viết '{0}', người gửi '{1}'";
        public static String DONGY_COMMENT = "Đống ý comment của bài viết '{0}'";

        public static String ALBUM_TAO_VA_LUU = "Tạo mới và lưu album '{0}'";
        public static String ALBUM_TAO_VA_GUITHANG = "Tạo mới và gửi thẳng album '{0}'";
        public static String ALBUM_SUA_VA_LUU = "Sửa và lưu album '{0}'";
        public static String ALBUM_SUA_VA_GUI_THANG = "Sửa và gửi thẳng album '{0}'";
        public static String ALBUM_SUA_VA_XUATBAN = "Sửa và xuất bản album '{0}'";
        public static String ALBUM_TAO_VA_XUATBAN = "Tạo và xuất bản album '{0}'";
        public static String ALBUM_LUU_LAI = "Lưu lại album '{0}'";
        public static String ALBUM_GUI_THANG = "Gửi thẳng album '{0}'";
        public static String ALBUM_XUAT_BAN = "Xuất bản album '{0}'";
        public static String ALBUM_GUI_LEN = "Gửi lên album '{0}'";
        public static String ALBUM_XOA_TAM = "Xóa tạm album '{0}'";
        public static String ALBUM_XOA_HAN = "Xóa hẳn album '{0}'";
        public static String ALBUM_TRA_VE = "Trả về album '{0}'";
        public static String ALBUM_GO_BO = "Gỡ bỏ album '{0}'";

        public static String LUU_LAI = "Lưu lại bài viết '{0}'";
        public static String GUI_THANG = "Gửi thẳng bài viêt '{0}'";
        public static String VIET_VA_GUI_THANG = "Viết và gửi thẳng bài viết '{0}'";
        public static String SUA_VA_GUI_THANG = "Sửa và gửi thẳng bài viết '{0}'";
        public static String XUAT_BAN = "Xuất bản bài viết '{0}'";
        public static String XUAT_BAN_THEM_MOI = "Xuất bản mới bài viết '{0}'";
        public static String XUAT_BAN_SUA_BAI = "Xuất bản sửa bài viết '{0}'";
        public static String XOA_BAI_LIEN_QUAN = "Xóa bài liên quan đến bài viết '{0}'";
        public static String XOA_MEDIA_LIEN_QUAN = "Xóa media liên quan bài viết '{0}'";
        public static String TIEU_DIEM = "Chuyển sang trạng thái tiêu điểm, ";
        public static String KHONG_TIEU_DIEM = "Chuyển sang trạng thái không phải là tiêu điểm, ";
        public static String GUI_LEN = "Gửi lên bài viết '{0}'";
        public static String XOA_TAM = "Xóa tạm bài viết '{0}'";
        public static String TRA_VE = "Trả về bài viết '{0}'";
        public static String GO_BO = "Gỡ bỏ bài viết '{0}'";
        public static String XOA_BAI = "Xóa bài bài viết '{0}'";
        public static String GUI_SANG_DANH_SACH_LUU_TAM_THOI = "Chuyển sang danh sách lưu tạm thời bài viết '{0}'";
        public static String TRA_TIN = "Trả tin bài '{0}'";


        public static String THONG_THUONG = "Chuyển sang trạng thái thông thường bài viết '{0}'";
        public static String NOI_BAT = "Nổi bật bài viết '{0}'";
        public static String NOI_BAT_MUC = "Nổi bật mục bài viết '{0}'";
    }
}
