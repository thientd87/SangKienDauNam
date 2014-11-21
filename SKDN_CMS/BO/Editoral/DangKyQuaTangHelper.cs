using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DFISYS.Core.DAL;

namespace DFISYS.BO.Editoral
{
    public class DangKyQuaTangHelper
    {
        public static DataTable SelectDangKyQuaTang()
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.SelectQuery("Select * from DangKy order by ID DESC");
            }
            return dt;
        }
    }
}