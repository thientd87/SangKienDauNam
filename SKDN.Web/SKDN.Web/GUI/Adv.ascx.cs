using System;
using System.Collections.Generic;
using BO;

namespace SKDN.Web.GUI
{
    public partial class Adv : System.Web.UI.UserControl
    {
        int postId = 0;
        string css = string.Empty;
        string _target = "_blank";
        int catid = Lib.QueryString.CategoryID;
        private int advID = 0;
        public int CatId { set { catid = value; } }
        public int PositionId { set { postId = value; } }
        public int AdvID { set { advID = value; } }

        public string Target { set { _target = value; } }
        public string ClassName { set { css = value; } }
        string ImgFormat = "<div class=\"{2}\"><a target=\"{3}\" alt=\"http://xevaphongcach.net\" href=\"{0}\"><img border=\"0\" src=\"{1}\"/></a></div>";
        string FlashFormat = "<div class=\"{3}\"><embed  src=\"{0}\"/></div>";
        string EmbedFormat = "<div class=\"{1}\">{0}</div>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string html = String.Empty;
                AdvHelper ah = new AdvHelper();
               // List<AdvDoc> doc = ah.GetAdvByPosition(catid, this.postId);
                List<AdvDoc> doc = ah.GetAdvByPositionById(advID);
                if (doc != null)
                {
                    AdvDoc item;
                    for (int i = 0; i < doc.Count; i++)
                    {
                       
                        item = doc[i];
                        if (!string.IsNullOrEmpty(item.FilePath))
                        switch(item.Type)
                        {
                            case 1:
                                html += String.Format(ImgFormat, item.Link.Length > 0 ? item.Link : "javascript:void(0);", item.FilePath.StartsWith("http") ? item.FilePath : Utility.ImagesStorageUrl + "/" + item.FilePath, css,_target);
                                break;
                            case 2:
                                html += String.Format(FlashFormat, item.FilePath.StartsWith("http") ? item.FilePath : Utility.ImagesStorageUrl + "/" + item.FilePath, item.Width, item.Height, css);
                                break;
                            case 3:
                                html += String.Format(EmbedFormat, item.Embed, css);
                                break;
                        }

                    }
                    ltrContent.Text = html;
                }
                else
                {
                    this.Visible = false;
                }
            }
        }
    }
}