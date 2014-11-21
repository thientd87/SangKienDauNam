using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using AddIns.BO;
using System.Configuration;
using DFISYS.BO.CoreBO;


namespace AddIns.ajax {
    public partial class advPreview : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack)
                BindData();
        }

        private void BindData() {
            Advertisments obj = new Advertisments();
            obj.AdvID = Convert.ToInt32(Request.QueryString["id"]);
            DataTable dt = obj.SelectOneDataTable();

            if (dt != null && dt.Rows.Count >0 )
            {
                string _tmp = string.Empty;
                
                string _imageDomain = ConfigurationManager.AppSettings["ImageUrl"].ToString();

                switch (dt.Rows[0]["Type"].ToString())
                {
                    //Ảnh
                    case "1":
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FilePath"].ToString()))
                            _tmp = "<img src=\"" + _imageDomain + dt.Rows[0]["FilePath"] + "\" border=\"0\">";
                        break;
                   

                    default:
                        break;
                }

                ltrAdv.Text = _tmp;
            }
        }
    }
}