using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;

namespace BO
{
    public class SeoHelper
    {
        public static void UpdateSeo(Int32 ID, int Cat_ID, string SEO_Title, string SEO_Desscription, string SEO_Keyword)
        {
            using (MainDB _db = new MainDB())
            {
                _db.StoredProcedures.SEO_Update(ID, Cat_ID, SEO_Title, SEO_Desscription, SEO_Keyword);
            }
        }
        public static DataTable SelectSeoByCat(int Cat_ID)
        {
            string CacheName = "SEO_SelectByCatID" + Cat_ID;
            DataTable dt = Utility.GetFromCache<DataTable>(CacheName);
            if(dt==null)
            {
                using (MainDB _db = new MainDB())
                {
                    dt = _db.StoredProcedures.SEO_SelectByCatID(Cat_ID);
                }
                Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.Seo_Category, CacheName, dt);
            }
            

            return dt;
        }
    }
}
