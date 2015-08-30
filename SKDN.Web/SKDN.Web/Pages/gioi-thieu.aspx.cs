using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BO;

namespace SKDN.Web.Pages
{
    public partial class gioi_thieu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dtAboutUs = SKDNHelper.GetAboutUsPage(1);
                if (dtAboutUs != null && dtAboutUs.Rows.Count > 0)
                {
                    ltrAboutUs.Text = dtAboutUs.Rows[0]["AboutUs"].ToString();
                    ltrSponsor.Text = dtAboutUs.Rows[0]["Sponsor"].ToString();
                    ltrMission.Text = dtAboutUs.Rows[0]["Mission"].ToString();

                    ltrImageAboutUs.Text = dtAboutUs.Rows[0]["AboutUsImage"] != null &&
                                           !string.IsNullOrEmpty(dtAboutUs.Rows[0]["AboutUsImage"].ToString())
                        ? "<img src=\"" + Utility.GetImageLink(dtAboutUs.Rows[0]["AboutUsImage"].ToString()) + "\" style=\"max-width:428px; float: left\"/>"
                        : string.Empty;
                    ltrSponsorImage.Text = dtAboutUs.Rows[0]["SponsorImage"] != null &&
                                         !string.IsNullOrEmpty(dtAboutUs.Rows[0]["SponsorImage"].ToString())
                      ? "<img src=\"" + Utility.GetImageLink(dtAboutUs.Rows[0]["SponsorImage"].ToString()) + "\" style=\"max-width:428px; float: left\"/>"
                      : string.Empty;

                  

                    ltrMissionImage.Text = dtAboutUs.Rows[0]["MIssionImage"] != null &&
                                        !string.IsNullOrEmpty(dtAboutUs.Rows[0]["MIssionImage"].ToString())
                     ? "<img src=\"" + Utility.GetImageLink(dtAboutUs.Rows[0]["MIssionImage"].ToString()) + "\" style=\"max-width:428px; float: left\"/>"
                     : string.Empty;
                }
            }
        }
    }
}