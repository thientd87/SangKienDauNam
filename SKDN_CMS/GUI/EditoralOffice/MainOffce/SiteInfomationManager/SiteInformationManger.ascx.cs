using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral.SiteInformation;

namespace DFISYS.GUI.EditoralOffice.MainOffce.SiteInfomationManager
{
    public partial class SiteInformationManger : System.Web.UI.UserControl
    {
        public SiteInformation site;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                site = SiteInformationController.SelectSiteInformation(1);
                if (site != null)
                {
                    txt_name.Text = site.SiteName;
                    txt_description.Text = site.SiteDescription;
                    txt_keyword.Text = site.SiteKeyword;
                    txt_Footer.Text = site.SiteFooter;
                    txt_address.Text = site.SiteAddress;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            site = new SiteInformation();
            site.Id = 1;
            site.SiteName = txt_name.Text;
            site.SiteDescription = txt_description.Text;
            site.SiteKeyword = txt_keyword.Text;
            site.SiteAddress = txt_address.Text;
            site.SiteFooter = txt_Footer.Text;
            SiteInformationController.InsertUpdateSIteInformation(site);
            this.Page.RegisterClientScriptBlock("successfull", "<script>alert(\"Cập nhật thành công!\")</script>");
        }
    }
}