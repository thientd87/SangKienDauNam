using System;
using System.Data;

using Portal.Core.DAL;

namespace Portal.BO.Editoral.AdvManagement {
    public class AdvHelper {

        public static DataTable AdvGetAllPages() {
            DataTable _result = null;

            using (MainDB _db = new MainDB()) {
                _result = _db.StoredProcedures.adv_GetAllCategories();
            }

            return _result;
        }
        public static void AdvDelete(string AdvID)
        {
            

            using (MainDB _db = new MainDB())
            {
                _db.AnotherNonQuery("Delete from Advertisments where AdvId = " + AdvID);
            }

           
        }
        public static DataTable AdvGetAllPositions() {
            DataTable _result = null;

            using (MainDB _db = new MainDB()) {
                _result = _db.StoredProcedures.adv_GetAllPositions();
            }

            return _result;
        }

        public static DataTable InsertNewAdv(string Name, DateTime StartDate, DateTime EndDate, string Embed, string Description, bool isActive, bool isRotate, int Order, int Type, string Link, string FilePath, int width, int height)
        {
            DataTable _result = null;

            using (MainDB _db = new MainDB()) {
                _result = _db.StoredProcedures.adv_InsertNewAdv(Name, StartDate, EndDate, Embed, Description, isActive, isRotate, Order, Type, Link, FilePath,width,height);
            }

            return _result;
        }

        public static DataTable Update(int AdvID, string Name, DateTime StartDate, DateTime EndDate, string Embed, string Description, bool isActive, bool isRotate, int Order, int Type, string Link, string FilePath, int width,int height) {
            DataTable _result = null;

            using (MainDB _db = new MainDB()) {
                _result = _db.StoredProcedures.adv_Update(AdvID, Name, StartDate, EndDate, Embed, Description, isActive, isRotate, Order, Type, Link, FilePath,width,height);
            }

            return _result;
        }

        public static void InsertAdvPositionDetails(int AdvID, int Position, string Pages) {
            using (MainDB _db = new MainDB()) {

                if (Pages.IndexOf(',') != -1) {
                    string[] arr = Pages.Split(',');
                    if (arr.Length > 0) {
                        foreach (string page in arr) {
                            _db.StoredProcedures.adv_InsertAdvPositionDetails(Position, Convert.ToInt32(page), AdvID);
                        }
                    }
                }
                else {
                    _db.StoredProcedures.adv_InsertAdvPositionDetails(Position, Convert.ToInt32(Pages), AdvID);
                }

            }
        }

        public static DataTable AdvDetails(int AdvID) {
            DataTable _result = null;

            using (MainDB _db = new MainDB()) {
                _result = _db.StoredProcedures.adv_Details(AdvID);
            }

            return _result;
        }

        public static DataTable GetAdvByPageAndPositions(int Category, int Position) {
            DataTable _result = null;

            using (MainDB _db = new MainDB()) {
                _result = _db.StoredProcedures.adv_GetAdvByPageAndPositions(Category, Position);
            }

            return _result;
        }

        public static DataTable GetAllPosByAd(int AdvID) {
            DataTable _result = null;

            using (MainDB _db = new MainDB()) {
                _result = _db.StoredProcedures.adv_GetAllPosByAd(AdvID);
            }

            return _result;
        }

        internal static void UpdateAdvPositionDetails(int _AdvID, int Position, string adv_pages) {
            
            using (MainDB _db = new MainDB()) {
                _db.StoredProcedures.adv_UpdateAdvPositionDetails(_AdvID, Position, adv_pages);
            }
        }


    }
}