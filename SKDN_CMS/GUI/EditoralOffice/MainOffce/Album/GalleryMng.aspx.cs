using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DFISYS.Core.DAL;
using System.Web.Script.Serialization;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Tool
{
    public partial class GalleryMng : System.Web.UI.Page
    {
        public decimal News_ID = 0;
        string user = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //user = Page.User.Identity.Name;

            //if (!String.IsNullOrEmpty(user))
            //{
            //    News_ID = NewsId();
            //    if (!IsPostBack)
            //    {
            //        BinData(News_ID);
            //    }
            //}
            //else
            //    this.Visible = false;
        }
        private decimal NewsId()
        {
            return Convert.ToDecimal(Request.QueryString["News_Id"]);
        }
        public void BinData(decimal News_Id)
        {
            if (News_Id > 0)
            {
                MediaObject objMediaObject = new MediaObject();
                var Data = objMediaObject.MediaObject_GetAllItemByNews_ID(News_Id);
                if (Data != null && Data.Count > 0)
                {
                    lbl.Text = "Tên tin:" + Data.FirstOrDefault().News_Title;
                }
                    GridView1.DataSource = Data;
                    GridView1.DataBind();
                
            }
        }
        [System.Web.Services.WebMethod]
        public static string MediaObject_Add(int Object_Type, string Object_Url, string Object_Note, int STT, int News_ID)
        {
            string strReturn = string.Empty; ;
            string UserID = string.Empty;
            MediaObject objMediaObject = new MediaObject();
            strReturn = objMediaObject.MediaObject_Add(Object_Type, Object_Url, Object_Note, STT, UserID, News_ID,0);
            return strReturn;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            //
            TextBox txtObject_Url = (TextBox)e.Row.FindControl("txtObject_Url");

            Literal lbl = (Literal)e.Row.FindControl("lbl");
            if (txtObject_Url != null && lbl != null)
            {
                lbl.Text = String.Format("<img src=\"/images/icons/folder.gif\" onclick=\"chooseFile('avatar', '{0}')\" style=\"cursor: pointer;\" />", txtObject_Url.ClientID);
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList cboObject_Type = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("cboObject_Type");
            TextBox txtObject_Url = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtObject_Url");
            TextBox txtObject_Note = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtObject_Note");
            TextBox txtStt = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtStt");
            MediaObject objMediaObject = new MediaObject();
            string strReturn = string.Empty;
            objMediaObject.Object_ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());

            objMediaObject.Object_Type = Convert.ToInt32(cboObject_Type.SelectedValue);
            objMediaObject.Object_Url = txtObject_Url.Text;
            objMediaObject.Object_Note = txtObject_Note.Text;
            try
            {
                objMediaObject.STT = Convert.ToInt32(txtStt.Text);
            }
            catch { objMediaObject.STT = 0; }
            strReturn = objMediaObject.MediaObject_Update(objMediaObject);
            if (string.IsNullOrEmpty(strReturn))
            {
                GridView1.EditIndex = -1;
                BinData(News_ID);
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BinData(News_ID);
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BinData(News_ID);
        }
        public string Decode(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
        public string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string val = hdfValue.Value;
            //val = Decode(val);
            if (val.Length > 0)
            {
                val = "[" + val.TrimEnd(',') + "]";
                JavaScriptSerializer objSerial = new JavaScriptSerializer();
                var data = objSerial.Deserialize<List<SubmitForm>>(val);
                if (data.Count > 0)
                {
                    MediaObject objMediaObject = new MediaObject();
                    string strReturn = string.Empty;
                    data.ForEach(p =>
                    {
                        int Object_Type = Convert.ToInt32(p.Object_Type);
                        int STT = Convert.ToInt32(p.STT);
                        strReturn += objMediaObject.MediaObject_Add(Object_Type, p.Object_Url, p.Object_Note, STT, string.Empty, News_ID,0);

                        //int Object_Type = Convert.ToInt32(base64Decode(p.Object_Type));
                        //int STT = Convert.ToInt32(base64Decode(p.STT));
                        //strReturn += objMediaObject.MediaObject_Add(Object_Type,base64Decode(p.Object_Url),base64Decode(p.Object_Note), STT, string.Empty, News_ID);
                    });
                    BinData(News_ID);
                    if (strReturn.Length > 0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script type=\"text/javascript\">alert('" + strReturn + "')</script>");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("alert", "<script type=\"text/javascript\">alert('Thêm mới thành công')</script>");
                    }
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Object_ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());
            if (Object_ID > 0)
            {
                MediaObject objMediaObject = new MediaObject();
                string strReturn = string.Empty;
                strReturn = objMediaObject.MediaObject_Delete(Object_ID);
                BinData(News_ID);
            }
        }

    }
    //public class SubmitForm
    //{
    //    public string Object_Type { get; set; }
    //    public string Object_Url { get; set; }
    //    public string Object_Note { get; set; }
    //    public string STT { get; set; }
    //}
}