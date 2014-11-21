using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;

using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Runtime.InteropServices;
using Portal.BO.Editoral.AdvManagement;

namespace Portal
{
    /// <summary>
    /// Summary description for AddInService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    [ScriptService]
    public class AddInService : System.Web.Services.WebService
    {

         
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPositionByPage(int pageId)
        {
            return "{}";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AddNewAdv(string adv_embed, string adv_link, string adv_name, string adv_type, string adv_pages, string ddlPos, string txtSelectedFile, string adv_startdate, string adv_enddate, string adv_order, string adv_isActive, [Optional, DefaultParameterValue("false")] string adv_isRotate, string adv_description, string adv_width, string adv_height)
        {

            if (String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name)) return string.Empty;

            try
            {
                int _order = 0;
                Int32.TryParse(adv_order, out _order);
                // System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                DataTable _result = AdvHelper.InsertNewAdv(adv_name, Convert.ToDateTime(adv_startdate), Convert.ToDateTime(adv_enddate), adv_embed, adv_description, adv_isActive == "on" ? true : false, adv_isRotate == "on" ? true : false, _order, Convert.ToInt32(adv_type), adv_link, txtSelectedFile, Convert.ToInt32(adv_width), Convert.ToInt32(adv_height));
                if (_result != null && _result.Rows.Count > 0)
                {
                    int _advID = Convert.ToInt32(_result.Rows[0][0]);

                    if (!string.IsNullOrEmpty(adv_pages))
                    {
                        AdvHelper.InsertAdvPositionDetails(_advID, Convert.ToInt32(ddlPos), adv_pages);
                    }

                    return "{ok : true, message: \"\"}";
                }

                return "{ok : false, message: \"Thêm mới không thành công\"}";
            }
            catch (Exception ex)
            {
                return "{ok : false, message: \"" + ex.Message + "\"}";
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateAdv(string adv_id, string adv_embed, string adv_link, string adv_name, string adv_type, string adv_pages, string ddlPos, string txtSelectedFile, string adv_startdate, string adv_enddate, string adv_order, string adv_isActive, [Optional, DefaultParameterValue("false")] string adv_isRotate, string adv_description,string adv_width,string adv_height)
        {
            if (String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name)) return string.Empty;

            try
            {
                int _order = 0;
                Int32.TryParse(adv_order, out _order);

                int _AdvID = 0;
                Int32.TryParse(adv_id, out _AdvID);

                DataTable _result = AdvHelper.Update(_AdvID, adv_name, Convert.ToDateTime(adv_startdate), Convert.ToDateTime(adv_enddate), adv_embed, adv_description, adv_isActive == "on" ? true : false, adv_isRotate == "on" ? true : false, _order, Convert.ToInt32(adv_type), adv_link, txtSelectedFile, Convert.ToInt32(adv_width), Convert.ToInt32(adv_height));

                if (!string.IsNullOrEmpty(adv_pages))
                {
                    AdvHelper.UpdateAdvPositionDetails(_AdvID, Convert.ToInt32(ddlPos), adv_pages);

                    return "{ok : true, message: \"\"}";
                }

                return "{ok : false, message: \"Cập nhật không thành công\"}";
            }
            catch (Exception ex)
            {
                return "{ok : false, message: \"" + ex.Message + "\"}";
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteAdv(string adv_id)
        {
            if (String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name)) return string.Empty;

            try
            {
                AdvHelper.AdvDelete(adv_id);
                return "{ok : true, message: \"Xóa thành công\"}";
            }
            catch (Exception ex)
            {
                return "{ok : false, message: \"" + ex.Message + "\"}";
            }
        }
    }
}
