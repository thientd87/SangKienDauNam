using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.AboutUs;

namespace DFISYS.GUI.EditoralOffice.MainOffce.AboutUs
{
    public partial class AboutUs : System.Web.UI.UserControl
    {
        public AboutUsObject site;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                site = AboutUsController.SelectSiteAboutUs(1);
                txtAboutUs.Text = site.AboutUs;
                txtMission.Text = site.Mission;
                txtSponsor.Text = site.Sponsor;
                txtAboutUsImage.Text = site.AboutUsImage;
                txtMissionImage.Text = site.MissionImage;
                txtSponsorImage.Text = site.SponsorImage;
                txtHuongDanDangKy.Text = site.HuongDanDangKy;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            site = new AboutUsObject();
            site.Id = 1;
            site.AboutUs = txtAboutUs.Text;
            site.Mission = txtMission.Text;
            site.Sponsor = txtSponsor.Text;
            site.AboutUsImage = txtAboutUsImage.Text;
            site.MissionImage = txtMissionImage.Text;
            site.SponsorImage = txtSponsorImage.Text;
            site.IsActive = true;
            site.HuongDanDangKy = txtHuongDanDangKy.Text;
                 
            AboutUsController.InsertUpdateAboutUs(site);
            this.Page.RegisterClientScriptBlock("successfull", "<script>alert(\"Cập nhật thành công!\")</script>");
        }
    }
}