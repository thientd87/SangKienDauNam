using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DFISYS.User.Security
{
	public class Permission
	{
		public bool isTao_Moi_Bai = false;
		public bool isLuu_Tam_Bai = false;
		public bool isXem_DS_Bai_Da_Gui = false;
		public bool isBien_Tap_Bai = false;
		public bool isDuyet_Bai = false;
		public bool isXuat_Ban_Bai = false;
		public bool isDuyet_Comment_Bai = false;
		public bool isTao_Moi_Tin_Anh = false;
		public bool isLuu_Tam_Tin_Anh = false;
		public bool isXem_DS_Tin_Anh_Da_Gui = false;
		public bool isBien_Tap_Tin_Anh = false;
		public bool isDuyet_Tin_Anh = false;
		public bool isXuat_Ban_Tin_Anh = false;
		public bool isDuyet_Comment_Tin_Anh = false;
		public bool isXem_Log = false;
		public bool isQuan_Ly_Media = false;
		public bool isTao_User_Va_Phan_Quyen = false;
		public bool isThong_Ke_Bai_Viet = false;
		public bool isThong_Ke_Chuyen_Muc = false;
		public bool isThong_Ke_Theo_User = false;
		public bool isThong_Ke_Toan_Trang = false;
		public bool isQuan_Ly_Binh_Chon = false;
		public bool isQuan_Ly_Luong_Su_Kien = false;
		public bool isQuan_Ly_Loai_Tin_Anh = false;

		public bool isXem_Danh_Sach_Bai_Tra_Lai = false;
		public bool isXem_Danh_Sach_Bai_Xoa_Tam = false;
		public bool isXem_Danh_Sach_Album_Tra_Lai = false;
		public bool isXem_Danh_Sach_Album_Xoa_Tam = false;

		public bool isXem_Danh_Sach_Crawler = false;
		public bool isHoiDap = false;
		public bool isMenu = false;
		public bool isBanDocViet = false;
		public bool isBaiThaoLuan = false;
		public bool isBaiThaoLuanDaXuatBan = false;

		public bool isChamNhuanBut = false;
		public bool isDatBaiNoiBatTrangChu = false;

	}
	public class PermissionConst
	{
		public const int Tao_Moi_Bai = 14;
		public const int Luu_Tam_Bai = 16;
		public const int Xem_DS_Bai_Da_Gui = 17;
		public const int Bien_Tap_Bai = 18;
		public const int Duyet_Bai = 19;
		public const int Xuat_Ban_Bai = 20;
		public const int Duyet_Comment_Bai = 21;
		public const int Tao_Moi_Tin_Anh = 22;
		public const int Luu_Tam_Tin_Anh = 23;
		public const int Xem_DS_Tin_Anh_Da_Gui = 24;
		public const int Bien_Tap_Tin_Anh = 26;
		public const int Duyet_Tin_Anh = 34;
		public const int Xuat_Ban_Tin_Anh = 47;
		public const int Duyet_Comment_Tin_Anh = 48;
		public const int Xem_Log = 49;
		public const int Quan_Ly_Media = 50;
		public const int Tao_User_Va_Phan_Quyen = 51;
		public const int Thong_Ke_Bai_Viet = 52;
		public const int Thong_Ke_Chuyen_Muc = 53;
		public const int Thong_Ke_Theo_User = 54;
		public const int Thong_Ke_Toan_Trang = 55;
		public const int Quan_Ly_Binh_Chon = 70;
		public const int Quan_Ly_Luong_Su_Kien = 71;
		public const int Quan_Ly_Loai_Tin_Anh = 72;

		public const int Xem_Danh_Sach_Bai_Tra_Lai = 73;
		public const int Xem_Danh_Sach_Bai_Xoa_Tam = 74;

        //public const int Xem_Danh_Sach_Album_Tra_Lai = 75;
        //public const int Xem_Danh_Sach_Album_Xoa_Tam = 76;
		public const int Xem_Danh_Sach_Crawler = 77;
		public const int HoiDap = 78;
		public const int Menu = 79;
		public const int BanDocViet = 80;
		public const int BaiThaoLuan = 81;
		public const int BaiThaoLuanDaXuatBan = 82;
		public const int DatBaiNoiBatTrangChu = 83;		

		
	}
}
