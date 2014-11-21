using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.CoreBO;
using DFISYS.BO.Editoral.Category;
using DFISYS.CoreBO.Common;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Advertisment
{
    public partial class AddBanner : System.Web.UI.UserControl
    {
        public Advertisments adver;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.Page.User.Identity.Name)) return;

            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {

            Adv_Page_Position objPage = new Adv_Page_Position();
            DataTable _pages = CategoryHelper.GetCategoriesByParent(0);
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
            
           

            if (Request.QueryString["AdvId"] != null && Request.QueryString["AdvId"].ToString() != "")
            {
                int advID = function.Obj2Int(Request.QueryString["AdvId"]);
               // hidAdvId.Value = advID.ToString();

                btnSave.OnClientClick = String.Format("Save({0}, {1})", advID, "false");
               // btnDelete.OnClientClick = String.Format("return Delete({0})", advID);
                if (advID > 0)
                {
                    Advertisments objAdver = new Advertisments();
                    objAdver.AdvID = advID;
                    objAdver = objAdver.SelectOne();
                    this.adv_name.Text = objAdver.Name.ToString();
                   
                    this.adv_type.SelectedValue = objAdver.Type.ToString();
                    //adv_embed.Text = objAdver.Embed;
                    adv_description.Text = objAdver.Description;
                    txtIcon.Value = objAdver.Link;
                    ui_date_picker_range_from.Value = objAdver.StartDate.ToString("MM/dd/yyyy");
                    ui_date_picker_range_to.Value = objAdver.EndDate.ToString("MM/dd/yyyy");
                    txtSelectedFile.Value = objAdver.FilePath;
                    adv_isRotate.Checked = objAdver.IsRotate;// Convert.ToBoolean(row["IsRotate"]);
                    adv_isActive.Checked = objAdver.IsActive;// Convert.ToBoolean(row["IsActive"]);
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
                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int advID = 0;
            if(!string.IsNullOrEmpty(Request.QueryString["advId"]))
                advID = function.Obj2Int(Request.QueryString["advId"]);

            adver = new Advertisments();
            adver.AdvID = advID;
            if (advID > 0)
                adver = adver.SelectOne();
            adver.Name = adv_name.Text;
            adver.FilePath = txtSelectedFile.Value;
            adver.StartDate = !string.IsNullOrEmpty(ui_date_picker_range_from.Value)?Convert.ToDateTime(ui_date_picker_range_from.Value):DateTime.Now;
            adver.EndDate = !string.IsNullOrEmpty(ui_date_picker_range_to.Value)?Convert.ToDateTime(ui_date_picker_range_to.Value):DateTime.Now.AddYears(3);
            adver.Embed = "";
            adver.Description = adv_description.Text;
            adver.IsActive = adv_isActive.Checked;
            adver.IsRotate = false;
            adver.Link = txtIcon.Value;
            adver.Order = function.Obj2Int(adv_order.Text);
            adver.Type = function.Obj2Int(adv_type.SelectedValue);
            //adver.Width = function.Obj2Int(Request.Form["txtContent"].ToString());
            //adver.Height = function.Obj2Int(Request.Form["txtContent"].ToString());

            if (advID > 0)
            {
                adver.Update();
            }
            else
            {
                adver.Insert();
            }
            Response.Redirect("/office/bannerlist.aspx");
            
        }
    }
}