using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Nextcom.Citinews.Core.Library
{
    public class ObjectHelper
    {
        public static DataTable FillDataTable(IDataReader dr)
        {
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }

        public static T FillObject<T>(IDataReader dr)
        {

            T objTarget = default(T);
            if (dr == null) return objTarget;
            try
            {
                if (dr.Read())
                {
                    objTarget = Activator.CreateInstance<T>();
                    var objProperties = objTarget.GetType().GetProperties();
                    var lstMatchFields = MatchFields(dr, objProperties);
                    for (int i = 0; i < lstMatchFields.Count; i++)
                    {
                        var property = lstMatchFields[i];
                        if (property.CanWrite && !Convert.IsDBNull(dr[property.Name]))
                        {
                            property.SetValue(objTarget, dr[property.Name], null);
                        }
                    }
                }
            }
            finally
            {
                dr.Close();
            }
            return objTarget;
        }

        public static List<PropertyInfo> MatchFields(IDataReader reader, PropertyInfo[] objProperties)
        {
            var lstMatchFields = new List<PropertyInfo>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                for (int j = 0; j < objProperties.Length; j++)
                {
                    if (objProperties[j].Name == reader.GetName(i))
                    {
                        lstMatchFields.Add(objProperties[j]);
                        break;
                    }
                }
            }
            return lstMatchFields;
        }

        public static List<T> FillCollection<T>(IDataReader dr)
        {
            var list = new List<T>();
            if (dr == null) return list;
            try
            {
                var objTarget = Activator.CreateInstance<T>();
                var objProperties = objTarget.GetType().GetProperties();
                var lstMatchFields = MatchFields(dr, objProperties);
                while (dr.Read())
                {
                    objTarget = Activator.CreateInstance<T>();
                    for (int j = 0; j < lstMatchFields.Count; j++)
                    {
                        if (lstMatchFields[j].CanWrite && !Convert.IsDBNull(dr[lstMatchFields[j].Name]))
                        {
                            lstMatchFields[j].SetValue(objTarget, dr[lstMatchFields[j].Name], null);
                        }
                    }
                    list.Add(objTarget);
                }
            }
            catch(Exception ee)
            {
                
            }
            finally
            {
                dr.Close();
            }
            return list;
        }
    }
}