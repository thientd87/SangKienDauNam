using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.Core.DAL;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Newspreview
{
    public partial class preview : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack )
            {
                string strnews = Request.QueryString["news"] != null ? Request.QueryString["news"].ToString() : "";
                string strFilmId = Request.QueryString["filmid"] != null ? Request.QueryString["filmid"].ToString() : "";
                if (strnews != "")
                {
                    tbNews.Visible = true;
                    long lngNewsID = 0;
                    try
                    {
                        lngNewsID = Convert.ToInt64(strnews);
                    }
                    catch
                    {
                        lngNewsID = 0;
                    }

                    NewsRow objNewRow;
                    using (MainDB objDb = new MainDB())
                    {
                        objNewRow = objDb.NewsCollection.GetByPrimaryKey(lngNewsID);
                    }
                    if (objNewRow != null)
                    {
                        ltrNewsTitle.Text = objNewRow.News_Title;
                        ltrImageNote.Text = objNewRow.News_ImageNote;
                        if ((objNewRow.News_Image != "") && (objNewRow.News_Image != null))
                        {
                            imgNewsAvatar.ImageUrl = "/" + objNewRow.News_Image;
                            imgNewsAvatar.Width = 120;
                        }
                        else
                            tblImg.Visible = false;

                        ltrNewsInit.Text = objNewRow.News_InitialContent;
                        ltrNewsDetail.Text = objNewRow.News_Content;
                        if ((objNewRow.News_Relation != "") && (objNewRow.News_Relation) != null)
                        {
                            String NewsRange = "(" + objNewRow.News_Relation + ")";
                            DataTable table = DFISYS.BO.Editoral.Newslist.NewslistHelper.SelectNewsByRangeId(NewsRange);                            
                            if (table.Rows.Count == 0)
                            {
                                divNewsRelation.Visible = false;
                            }
                            else
                            {
                                dlNewsRelation.DataSource = table;
                                dlNewsRelation.DataBind();
                                divNewsRelation.Visible = true;
                            }
                        }
                        else
                        {
                            divNewsRelation.Visible = false;
                        }
                    }
                }
                
                
            }
        }
    }
}