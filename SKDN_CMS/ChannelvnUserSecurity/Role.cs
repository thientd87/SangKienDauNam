using System;
using System.Collections.Generic;
using System.Text;

namespace DFISYS.User.Security
{
    public class Role
    {
        public bool isQuanTriKenh = false;
        public bool _isThuKyToaSoan = false;
        public bool isThuKyChuyenMuc = false;
        public bool isBienTapVien = false;
        public bool isPhongVien = false;
        public bool isQuanTriQuangCao = false;
        public bool isCustomUser = false;
        public bool isPhuTrachKenh = false;
        public bool isAdministrator = false;
        public bool isTongBienTap = false;
        public bool isEveryOne = false;
        public bool isPhongVan = false;
        public bool isGiaoLuuTrucTuyen = false;

		public bool isThuKyToaSoan
		{
			set { _isThuKyToaSoan = value; }
			get { return _isThuKyToaSoan || isTongBienTap || isPhuTrachKenh; }
		}
    }
    public class RoleConst
    {
        public const int QuanTriKenh = 1;
        public const int ThuKyToaSoan = 2;
        public const int ThuKyChuyenMuc = 3;
        public const int BienTapVien = 4;
        public const int PhongVien = 5;
        public const int QuanTriQuangCao = 6;
        public const int CustomUser = 7;
        public const int PhuTrachKenh = 8;
        public const int Administrator = 9;
        public const int TongBienTap = 10;
        public const int EveryOne = 11;
		public const int PhongVan = 12;
        public const int GiaoLuuTrucTuyen = 13;
    }
}
