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


namespace Portal.GUI.EditoralOffice.MainOffce.NewsThread
{
    public partial class ThreadEdit : System.Web.UI.UserControl
    {
        private Int32 Thread_ID;
        ChannelVN.CoreBO.Threads.ThreadHelper thread = new ChannelVN.CoreBO.Threads.ThreadHelper();

        #region private method
        private void BindData()
        {
            DataTable table = thread.ThreadSelectOne(Thread_ID);
            if (table.Rows.Count != 0)
            {
                txtThreatTitle.Text = table.Rows[0]["Title"].ToString();
                chkIsFocus.Checked = Convert.ToBoolean(table.Rows[0]["Thread_isForcus"]);
                DataTable temp = thread.GetThreads(table.Rows[0]["Thread_RT"].ToString());
                listThread.DataSource = temp;
                listThread.DataBind();

                String IDs = String.Empty;
                if (temp.Rows.Count > 0)
                {
                    for (int i = 0; i < temp.Rows.Count; i++)
                        IDs += temp.Rows[i]["Thread_ID"].ToString() + ",";
                    IDs += temp.Rows[temp.Rows.Count - 1]["Thread_ID"].ToString();
                }
                threadIDs.Value = IDs;
                if (IDs == String.Empty)
                    IDs = "0";

                lbChonChuDe.OnClientClick = "openpreview('/ListCatThread.aspx?Hid=" + threadIDs.ClientID + "&Control=" + listThread.ClientID + "&IsCat=false&ID=" + IDs + "',500,500) ;return false;";

                        //for (int i = 0; i < temp.Rows.Count; i++)
                        //{
                        //listThread.SelectedValue = ;
                        //    for (int j = 0; j < listThread.Items.Count; j++)
                        //    {
                        //        if (listThread.Items[j].Value == temp.Rows[i]["Thread_ID"].ToString())
                        //            listThread.Items[j].Selected = true;
                        //    }
                        //}

                        temp = thread.GetCats(table.Rows[0]["Thread_RC"].ToString());
                //for (int i = 0; i < temp.Rows.Count; i++)
                //{
                    //listThread.SelectedValue = ;
                //    for (int j = 0; j < listCat.Items.Count; j++)
                //    {
                //        if (listCat.Items[j].Value == temp.Rows[i]["Cat_ID"].ToString())
                //            listCat.Items[j].Selected = true;
                //    }
               // }


                listCat.DataSource = temp;
                listCat.DataBind();

                IDs = String.Empty;
                if (temp.Rows.Count > 0)
                {
                    for (int i = 0; i < temp.Rows.Count; i++)
                        IDs += temp.Rows[i]["Cat_ID"].ToString() + ",";
                    IDs += temp.Rows[temp.Rows.Count - 1]["Cat_ID"].ToString();
                }
                catIDs.Value = IDs;
                if (IDs == String.Empty)
                    IDs = "0";

                lbDanhMuc.OnClientClick = "openpreview('/listCatThread.aspx?Hid=" + catIDs.ClientID + "&Control=" + listCat.ClientID + "&IsCat=true&ID=" + IDs + "',500,500) ;return false;";
            }
            else
            {
               
            }
        }

        private String GetListIDs(ListBox lb)
        {
            String strReturn = String.Empty;
            //for(listThread.GetSelectedIndices)

            /*
            int[] listThreadSelect = lb.GetSelectedIndices();
            if (listThreadSelect.Length > 0)
            {
                for (int i = 0; i < listThreadSelect.Length - 1; i++)
                    strReturn += lb.Items[listThreadSelect[i]].Value.ToString() + ",";
                strReturn += lb.Items[listThreadSelect[listThreadSelect.Length - 1]].Value.ToString();
            }
             */

            if (lb.Items.Count > 0)
            {
                for (int i = 0; i < lb.Items.Count - 1; i++)
                    strReturn += lb.Items[i].Value + ",";
                strReturn += lb.Items[lb.Items.Count - 1].Value;
            }

            //strReturn.TrimEnd(',');
            return strReturn;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Portal.BO.Editoral.Category.CategoryHelper.BindListCat(listCat);
                //listCat.Items.RemoveAt(0);

                //listThread.DataSource = thread.RunSql("SELECT Thread_ID,Title FROM NewsThread");
                //listThread.DataValueField = "Thread_ID";
                //listThread.DataTextField = "Title";
                //listThread.DataBind();

                if (Request.QueryString["NewsRef"] != "")
                {
                    Thread_ID = Convert.ToInt32(Request.QueryString["NewsRef"]);
                    BindData();
                }
                else
                {
                    lbChonChuDe.OnClientClick = "openpreview('/ListCatThread.aspx?Hid=" + threadIDs.ClientID + "&Control=" + listThread.ClientID + "&IsCat=false&ID=0',500,500) ;return false;";
                    lbDanhMuc.OnClientClick = "openpreview('/listCatThread.aspx?Hid=" + catIDs.ClientID + "&Control=" + listCat.ClientID + "&IsCat=true&ID=0',500,500) ;return false;";
                }
            }
        }

        protected void txtSave_Click(object sender, EventArgs e)
        {
            if (txtThreatTitle.Text.Trim() == "") return;
            if (Request.QueryString["NewsRef"] != "")            
                //Update
                //thread.ThreadUpdate(txtThreatTitle.Text, chkIsFocus.Checked, fuLogo.FileName, GetListIDs(listCat), GetListIDs(listThread),Convert.ToInt32(Request.QueryString["NewsRef"]));            
                thread.ThreadUpdate(txtThreatTitle.Text, chkIsFocus.Checked, fuLogo.PostedFile, catIDs.Value, threadIDs.Value, Convert.ToInt32(Request.QueryString["NewsRef"]));            
            else            
                //Insert
                //thread.ThreadInsert(txtThreatTitle.Text, chkIsFocus.Checked, fuLogo.FileName, GetListIDs(listCat), GetListIDs(listThread));
                thread.ThreadInsert(txtThreatTitle.Text, chkIsFocus.Checked, fuLogo.PostedFile, catIDs.Value, threadIDs.Value);

            Response.Redirect("/office/listthread.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/office/listthread.aspx");
        }
    }
}