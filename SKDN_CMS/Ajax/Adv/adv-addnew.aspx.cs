using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DFISYS.BO.CoreBO;
using DFISYS.BO.Editoral.Category;
using DFISYS.CoreBO.Common;
//using AddIns.BO;

namespace AddIns.ajax {
    public partial class adv_addnew : System.Web.UI.Page {

        public Advertisments adver;
        protected void Page_Load(object sender, EventArgs e) {
            if (String.IsNullOrWhiteSpace(this.Page.User.Identity.Name)) return;
            
            if (!IsPostBack) {
                BindData();
            }
            
        }

        private void BindData() {

            Adv_Page_Position objPage = new Adv_Page_Position();
            DataTable _pages = CategoryHelper.GetCategoriesByParent(0);//.SelectAllLike("");//.AdvGetAllPages();
            if (_pages != null && _pages.Rows.Count > 0)
            {
                cblPages.DataSource = _pages;
                cblPages.DataBind();

                cblPages.Items.Insert(0, new ListItem("Trang chủ", "0"));
            }

            if (cblPages.Items.Count > 0)
            {
                foreach (ListItem item in cblPages.Items)
                {
                    item.Attributes.Add("cid", item.Value);
                }
            }


            Adv_Position objPos = new Adv_Position();
            List<Adv_Position> _pos = objPos.SelectAllLike("");// AdvHelper.AdvGetAllPositions();
            if (_pos != null && _pos.Count > 0)
            {
                ddlPos.DataSource = _pos;
                ddlPos.DataBind();
            }
            //DataTable _pos = AdvHelper.AdvGetAllPositions();
            //if (_pos != null && _pos.Rows.Count > 0) {
            //    ddlPos.DataSource = _pos;
            //    ddlPos.DataBind();
            //}

            if (Request.QueryString["page"] != null && Request.QueryString["page"].ToString() != "")
                cblPages.SelectedValue = Request.QueryString["page"].ToString();

            if (Request.QueryString["pos"] != null && Request.QueryString["pos"].ToString() != "")
                ddlPos.SelectedValue = Request.QueryString["pos"].ToString();

            if (Request.QueryString["AdvId"] != null && Request.QueryString["AdvId"].ToString() != "") {
                int advID = function.Obj2Int(Request.QueryString["AdvId"]);
                hidAdvId.Value = advID.ToString();

                btnSave.OnClientClick = String.Format("Save({0}, {1})", advID, "false");
                btnDelete.OnClientClick = String.Format("return Delete({0})", advID);
                if (advID > 0)
                {
                    Advertisments objAdver = new Advertisments();
                    objAdver.AdvID = advID;
                    objAdver = objAdver.SelectOne();
                    this.adv_name.Text = objAdver.Name.ToString();
                    this.adv_link.Text = objAdver.FilePath;
                    this.adv_type.SelectedValue = objAdver.Type.ToString();
                    adv_embed.Text = objAdver.Embed;
                    adv_description.Text = objAdver.Description;
                    adv_link.Text = objAdver.Link;
                    adv_startdate.Text = objAdver.StartDate.ToString("MM/dd/yyyy");
                    adv_enddate.Text = objAdver.EndDate.ToString("MM/dd/yyyy");

                    adv_isRotate.Checked = objAdver.IsRotate;// Convert.ToBoolean(row["IsRotate"]);
                    //adv_isActive.Checked = objAdver.IsActive;// Convert.ToBoolean(row["IsActive"]);
                    adv_order.Text = objAdver.Order.ToString();// row["Order"].ToString();

                    List<Adv_Page_Position> lsPagePos = new List<Adv_Page_Position>();
                    lsPagePos = objPage.SelectAllByAdvID(advID);

                    //DataTable pages = AdvHelper.GetAllPosByAd(advID);
                    if (lsPagePos != null && lsPagePos.Count > 0)
                    {
                        foreach (ListItem item in cblPages.Items)
                        {
                            for (int i = 0; i < lsPagePos.Count; i++)
                            {
                                if (item.Value == lsPagePos[i].CatID.ToString())
                                    item.Selected = true;
                            }
                        }
                    }
                }
                //DataTable _details = AdvHelper.AdvDetails(advID);
                //if (_details != null && _details.Rows.Count > 0) {
                //    DataRow row = _details.Rows[0];
                //    adv_name.Text = row["Name"].ToString();
                //    txtSelectedFile.Text = row["FilePath"].ToString();
                //    adv_type.SelectedValue = row["Type"].ToString();
                //    adv_embed.Text = row["Embed"].ToString();
                //    adv_description.Text = row["Description"].ToString();
                //    adv_link.Text = row["Link"].ToString();
                //    adv_startdate.Text = Convert.ToDateTime(row["StartDate"]).ToString("MM/dd/yyyy");
                //    adv_enddate.Text = Convert.ToDateTime(row["EndDate"]).ToString("MM/dd/yyyy");

                //    adv_isRotate.Checked = Convert.ToBoolean(row["IsRotate"]);
                //    adv_isActive.Checked = Convert.ToBoolean(row["IsActive"]);
                //    adv_order.Text = row["Order"].ToString();

                //    DataTable pages = AdvHelper.GetAllPosByAd(advID);
                //    if (pages != null && pages.Rows.Count > 0) {
                //        foreach (ListItem item in cblPages.Items) {
                //            for (int i = 0; i < pages.Rows.Count; i++) {
                //                if (item.Value == pages.Rows[i]["CatID"].ToString())
                //                    item.Selected = true;
                //            }
                //        }
                //    }

                //}
            }
        }


    }
}