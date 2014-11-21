using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Text;

namespace Portal.BO {

    /// <summary>
    /// DataTable to JSON converter
    /// </summary>
    public class JSONConverter : JavaScriptConverter {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer rserializer) {
            throw new NotImplementedException("Deserialize is not implemented.");
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer) {
            DataTable dt = obj as DataTable;
            Dictionary<string, object> result = new Dictionary<string, object>();

            if (dt != null && dt.Rows.Count > 0) {
                // List for row values
                List<object> rowValues = new List<object>();

                foreach (DataRow dr in dt.Rows) {
                    // Dictionary for col name / col value
                    Dictionary<string, object> colValues = new Dictionary<string, object>();

                    foreach (DataColumn dc in dt.Columns) {
                        colValues.Add(dc.ColumnName, // col name
                         (string.Empty == dr[dc].ToString()) ? null : dr[dc]); // col value
                    }

                    // Add values to row
                    rowValues.Add(colValues);
                }

                // Add rows to serialized object
                result["rows"] = rowValues;
            }

            return result;
        }

        public override IEnumerable<Type> SupportedTypes {
            //Define the DataTable as a supported type.
            get {
                return new System.Collections.ObjectModel.ReadOnlyCollection<Type>(
                 new List<Type>(
                  new Type[] { typeof(DataTable) }
                 )
                );
            }
        }

        public static string FromDataTable(DataTable dt) {
            string rowDelimiter = "";

            StringBuilder result = new StringBuilder("[");
            foreach (DataRow row in dt.Rows) {
                result.Append(rowDelimiter);
                result.Append(FromDataRow(row));
                rowDelimiter = ",";
            }
            result.Append("]");

            return result.ToString();
        }

        public static string FromDataRow(DataRow row) {
            DataColumnCollection cols = row.Table.Columns;
            string colDelimiter = "";

            StringBuilder result = new StringBuilder("{");
            for (int i = 0; i < cols.Count; i++) { // use index rather than foreach, so we can use the index for both the row and cols collection
                result.Append(colDelimiter).Append("\"")
                      .Append(cols[i].ColumnName).Append("\":")
                      .Append(JSONValueFromDataRowObject(row[i], cols[i].DataType));

                colDelimiter = ",";
            }
            result.Append("}");
            return result.ToString();
        }

        // possible types:
        // http://msdn.microsoft.com/en-us/library/system.data.datacolumn.datatype(VS.80).aspx
        private static Type[] numeric = new Type[] {typeof(byte), typeof(decimal), typeof(double), 
                                     typeof(Int16), typeof(Int32), typeof(SByte), typeof(Single),
                                     typeof(UInt16), typeof(UInt32), typeof(UInt64)};

        // I don't want to rebuild this value for every date cell in the table
        private static long EpochTicks = new DateTime(1970, 1, 1).Ticks;

        private static string JSONValueFromDataRowObject(object value, Type DataType) {
            // null
            if (value == DBNull.Value) return "null";

            // numeric
            if (Array.IndexOf(numeric, DataType) > -1)
                return value.ToString(); // TODO: eventually want to use a stricter format

            // boolean
            if (DataType == typeof(bool))
                return ((bool)value) ? "true" : "false";

            // date -- see http://weblogs.asp.net/bleroy/archive/2008/01/18/dates-and-json.aspx
            if (DataType == typeof(DateTime))
                return "\"\\/Date(" + new TimeSpan(((DateTime)value).ToUniversalTime().Ticks - EpochTicks).TotalMilliseconds.ToString() + ")\\/\"";

            // TODO: add Timespan support
            // TODO: add Byte[] support

            //TODO: this would be _much_ faster with a state machine
            // string/char  
            return "\"" + value.ToString().Replace(@"\", @"\\").Replace(Environment.NewLine, @"\n").Replace("\"", @"\""") + "\"";
        }

    }
}