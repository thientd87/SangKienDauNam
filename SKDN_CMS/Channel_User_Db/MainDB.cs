#define SQL_CLIENT
using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace DFISYS.User.Db {


    public class MainDB : MainDB_Base {
        public MainDB() {
            // EMPTY
        }
        public DataTable SelectQuery(string sql) {
            IDbCommand cmd = this.CreateCommand(sql, false);
            return this.CreateDataTable(cmd);
        }
        public void AnotherNonQuery(string sql) {
            IDbCommand cmd = this.CreateCommand(sql, false);
            cmd.ExecuteNonQuery();
        }
        public DataTable CallStoredProcedure(string nameOfStored, bool returnDataTable) {
            IDbCommand cmd = this.CreateCommand(nameOfStored, true);
            if (returnDataTable) {
                return this.CreateDataTable(cmd);
            }
            else {
                cmd.ExecuteNonQuery();
                return null;
            }
        }

        public DataTable CallStoredProcedure(ArrayList listOfPara, string nameOfStored, bool returnDataTable) {
            IDbCommand cmd = this.CreateCommand(nameOfStored, true);
            for (int i = 0; i < listOfPara.Count; i++) {
                SqlParameter sp = (SqlParameter)listOfPara[i];
                AddParameter(cmd, sp.ParameterName, sp.DbType, sp.Value);
            }
            if (returnDataTable) {
                return this.CreateDataTable(cmd);
            }
            else {
                cmd.ExecuteNonQuery();
                return null;
            }
        }
        protected override IDbConnection CreateConnection() {
            return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cms_coreConnectionString"].ToString());
        }
        protected internal DataTable CreateDataTable(IDbCommand command) {
            DataTable dataTable = new DataTable();
#if ODBC
			new System.Data.Odbc.OdbcDataAdapter((System.Data.Odbc.OdbcCommand)command).Fill(dataTable);
#elif SQL_CLIENT
            new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)command).Fill(dataTable);
#else
			new System.Data.OleDb.OleDbDataAdapter((System.Data.OleDb.OleDbCommand)command).Fill(dataTable);
#endif
            return dataTable;
        }

        //bacth [3:07 PM 7/21/2008]
        public DataTable CallStoredProcedure(string nameOfStored, object[] parValues, string[] parNames, bool returnDataTable) {
            IDbCommand cmd = this.CreateCommand(nameOfStored, true);
            for (int i = 0; i < parValues.Length; i++) {
                cmd.Parameters.Add(new SqlParameter(parNames.GetValue(i).ToString(), parValues.GetValue(i)));
            }
            if (returnDataTable) {
                return this.CreateDataTable(cmd);
            }
            else {
                cmd.ExecuteNonQuery();
                return null;
            }
        }

        protected internal override string CreateSqlParameterName(string paramName) {
#if ODBC
			return "?";
#elif SQL_CLIENT
            return "@" + paramName;
#else
			return "?";
#endif
        }
        protected override string CreateCollectionParameterName(string baseParamName) {
#if ODBC
			return "@" + baseParamName;
#else
            return "@" + baseParamName;
#endif
        }
    }
    public class StrProcess
    {
        #region  StringProcess
        private LocalStore _StoreChars;
        private System.Collections.SortedList _Sortlst;
        public StrProcess()
        {
            this._StoreChars = null;
            _StoreChars = new LocalStore();
            _Sortlst = new SortedList();
            #region Initial SortList
            foreach (int _key in _StoreChars.aChar)
            {
                _Sortlst.Add(_key, 'a');
            }
            foreach (int _key in _StoreChars.AChar)
            {
                _Sortlst.Add(_key, 'A');
            }
            foreach (int _key in _StoreChars.eChar)
            {
                _Sortlst.Add(_key, 'e');
            }
            foreach (int _key in _StoreChars.EChar)
            {
                _Sortlst.Add(_key, 'E');
            }
            foreach (int _key in _StoreChars.oChar)
            {
                _Sortlst.Add(_key, 'o');
            }
            foreach (int _key in _StoreChars.OChar)
            {
                _Sortlst.Add(_key, 'O');
            }
            foreach (int _key in _StoreChars.uChar)
            {
                _Sortlst.Add(_key, 'u');
            }
            foreach (int _key in _StoreChars.UChar)
            {
                _Sortlst.Add(_key, 'U');
            }
            foreach (int _key in _StoreChars.iChar)
            {
                _Sortlst.Add(_key, 'i');
            }
            foreach (int _key in _StoreChars.IChar)
            {
                _Sortlst.Add(_key, 'I');
            }
            foreach (int _key in _StoreChars.yChar)
            {
                _Sortlst.Add(_key, 'y');
            }
            foreach (int _key in _StoreChars.YChar)
            {
                _Sortlst.Add(_key, 'Y');
            }
            _Sortlst.Add(_StoreChars.dChar, 'd');
            _Sortlst.Add(_StoreChars.DChar, 'D');
            #endregion
        }
        private void KillSpaces(ref string StrSource)
        {
            for (int i = 0; i < StrSource.Length; i++)
            {
                if (StrSource[i] == 32)
                {
                    StrSource = StrSource.Remove(i, 1);
                }

            }
            StrSource = StrSource.ToLower();
        }
        public string ConvertToNonUnicode(string StrUnicode)
        {
            string GetStr = StrUnicode;

            for (int i = 0; i < StrUnicode.Length; i++)
            {
                foreach (DictionaryEntry root in _Sortlst)
                {
                    if ((int)GetStr[i] == (int)root.Key)
                    {
                        GetStr = GetStr.Replace(GetStr[i], (char)root.Value);
                    }
                }
            }
            return GetStr;
        }
        public string StandNonUnicode(string StrUnicode)
        {
            string GetStr = StrUnicode;
            GetStr = ConvertToNonUnicode(GetStr);
            KillSpaces(ref GetStr);
            return GetStr;
        }
        public bool IsSubString(string StrSource, string StrSub)
        {
            int Sublen = StrSub.Length;
            int Indexof = 0, k = 0;
            System.Collections.Queue Addindex = new Queue(); ;
            for (int i = 0; i < StrSource.Length; i++)
            {
                if (StrSource[i] == StrSub[0])
                {
                    Addindex.Enqueue(i);
                }
            }
            while (Addindex.Count != 0)
            {
                Indexof = (int)Addindex.Dequeue();
                if (Indexof + StrSub.Length < StrSource.Length)
                {
                    for (int i = Indexof; i < Indexof + StrSub.Length; i++)
                    {
                        if (StrSource[i] == StrSub[k])
                        {
                            k++;
                        }
                        else { k = 0; break; }
                    }
                }
                if (k == StrSub.Length)
                    break;
            }
            if (k != 0)
                return true;
            else return false;
        }
        public bool IsSubString(string StrSource, string StrSub, ref int StartIndex)
        {
            int Sublen = StrSub.Length;
            int Indexof = 0, k = 0;
            System.Collections.Queue Addindex = new Queue(); ;
            for (int i = 0; i < StrSource.Length; i++)
            {
                if (StrSource[i] == StrSub[0])
                {
                    Addindex.Enqueue(i);
                }
            }
            while (Addindex.Count != 0)
            {
                Indexof = (int)Addindex.Dequeue();
                if (Indexof + StrSub.Length < StrSource.Length)
                {
                    for (int j = Indexof; j < Indexof + StrSub.Length; j++)
                    {
                        if (StrSource[j] == StrSub[k])
                        {
                            k++;
                        }
                        else { k = 0; break; }
                    }
                }
                if (k == StrSub.Length) { StartIndex = Indexof; break; }
            }
            if (k != 0)
            {
                return true;
            }
            else return false;
        }
        #endregion
    }
    public class LocalStore
    {
        #region Mang Tong Quat cua bo phone Unicode Tieng Viet
        private int[] _unicodevntable =
			{
				7857,7856,7859,7858,7861,7860,7855,7854,7863,7862,
				7847,7846,7849,7848,7851,7850,7845,7844,7853,7852,
				7873,7872,7875,7874,7877,7876,7871,7870,7879,7878,
				7891,7890,7893,7892,7895,7894,7889,7888,7897,7896,
				7901,7900,7903,7902,7905,7904,7899,7898,7907,7906,
				7915,7914,7917,7916,7919,7918,7913,7912,7921,7920,
				258,194,202,212,416,431,272,259,226,234,244,417,432,
				273,224,192,7843,7842,227,195,225,193,7841,7840,232,
				200,7867,7866,7869,7868,233,201,7865,7864,236,204,
				7881,7880,297,296,273,237,205,7883,7882,242,210,7887,
				7886,245,213,243,211,7885,7884,249,217,7911,7910,
				361,360,250,218,7909,7908,7923,7922,7927,7926,7929,
				7928,253,221,7925,7924
			};
        #endregion
        #region Cac mang cuc bo
        private int[] _achar =
			{
				7857,7859,7861,7855,7863,7847,7849,7851,7845,
				7853,259,226,224,7843,227,225,7841
			};
        private int[] _Achar =
			{
				7856,7858,7860,7854,7862,7846,7848,7850,7844,
				7852,258,794,792,1842,195,193,7840
			};
        private int[] _echar =
			{
				7873,7875,7877,7871,7879,234,232,7867,7869,
				233,7865
			};
        private int[] _Echar =
			{
				7872,7874,7876,7870,7878,202,200,7866,7868,
				201,7864
			};
        private int[] _ochar =
			{
				7891,7893,7895,7889,7897,7901,7907,7899,7905,
				7903,244,417,242,7887,245,243,7885
			};
        private int[] _Ochar =
			{
				7890,7892,7894,7888,7896,7900,7902,7904,7898,
				7906,212,416,210,7886,213,211,7884
			};
        private int[] _uchar =
			{
				7915,7917,7919,7913,7921,432,249,7911,361,250,7909
			};
        private int[] _Uchar =
			{
				7914,7916,7918,7912,7920,217,7910,360,218,7908,431
			};
        private int[] _ichar =
			{
				236,7881,279,237,7883
			};
        private int[] _Ichar =
			{
				204,7880,296,205,7882
			};
        private int[] _ychar =
			{
				7923,7927,7929,253,7925
			};
        private int[] _Ychar =
			{
				7922,7926,7928,221,7924
			};
        private int _dchar = 273;
        private int _Dchar = 272;
        #endregion
        #region Build Properties
        public int[] UnicodeVNTabe
        {
            get { return this._unicodevntable; }
        }
        public int[] aChar
        {
            get { return this._achar; }
        }
        public int[] AChar
        {
            get { return this._Achar; }
        }
        public int[] eChar
        {
            get { return this._echar; }
        }
        public int[] EChar
        {
            get { return this._Echar; }
        }
        public int[] iChar
        {
            get { return this._ichar; }
        }
        public int[] IChar
        {
            get { return this._Ichar; }
        }
        public int[] oChar
        {
            get { return this._ochar; }
        }
        public int[] OChar
        {
            get { return this._Ochar; }
        }
        public int[] uChar
        {
            get { return this._uchar; }
        }
        public int[] UChar
        {
            get { return this._Uchar; }
        }
        public int[] yChar
        {
            get { return this._ychar; }
        }
        public int[] YChar
        {
            get { return this._Ychar; }
        }
        public int dChar
        {
            get { return _dchar; }
        }
        public int DChar
        {
            get { return _Dchar; }
        }

        #endregion
    }
}

