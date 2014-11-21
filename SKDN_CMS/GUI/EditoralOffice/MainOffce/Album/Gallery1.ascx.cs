using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO;
using DFISYS.Core.DAL;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Tool
{
    public partial class Gallery1 : UserControl
    {
        public int Id = 0;
        private string user = string.Empty;
        public string TxtGallery = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
                user = Page.User.Identity.Name;
                Id = Convert.ToInt32(Request.QueryString["Id"]);
                TxtGallery = Request.QueryString["Name"];
                if (!String.IsNullOrEmpty(user))
                {
                    if (!IsPostBack)
                    {
                        BinData(Id);
                    }
                }
                else
                    this.Visible = false;
        }

        public void BinData(int News_Id)
        {
            if (News_Id > 0)
            {
                Gallery galleryHelper =  new Gallery();
                galleryHelper.ID = News_Id;
                Gallery gallery = galleryHelper.SelectOne();
                if (gallery != null)
                {
                    txtName.Value = gallery.Name;
                    MediaObject objMediaObject = new MediaObject();
                    var Data = objMediaObject.MediaObject_getbyGallleryId(News_Id);
                    if (Data != null && Data.Count > 0)
                    {
                        GridView1.DataSource = Data;
                        GridView1.DataBind();
                    }
                }
                
               

            }
        }

        [System.Web.Services.WebMethod]
        public static string MediaObject_Add(int Object_Type, string Object_Url, string Object_Note, int STT,
                                             int News_ID,int galleryId)
        {
            string strReturn = string.Empty;
            ;
            string UserID = string.Empty;
            MediaObject objMediaObject = new MediaObject();
            strReturn = objMediaObject.MediaObject_Add(Object_Type, Object_Url, Object_Note, STT, UserID, News_ID,galleryId);
            return strReturn;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            //
            TextBox txtObject_Url = (TextBox) e.Row.FindControl("txtObject_Url");

            Literal lbl = (Literal) e.Row.FindControl("lbl");
            if (txtObject_Url != null && lbl != null)
            {
                lbl.Text =
                    String.Format(
                        "<img src=\"/images/icons/folder.gif\" onclick=\"chooseFile('avatar', '{0}')\" style=\"cursor: pointer;\" />",
                        txtObject_Url.ClientID);
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList cboObject_Type = (DropDownList) GridView1.Rows[e.RowIndex].FindControl("cboObject_Type");
            TextBox txtObject_Url = (TextBox) GridView1.Rows[e.RowIndex].FindControl("txtObject_Url");
            TextBox txtObject_Note = (TextBox) GridView1.Rows[e.RowIndex].FindControl("txtObject_Note");
            TextBox txtStt = (TextBox) GridView1.Rows[e.RowIndex].FindControl("txtStt");
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
            catch
            {
                objMediaObject.STT = 0;
            }
            strReturn = objMediaObject.MediaObject_Update(objMediaObject);
            if (string.IsNullOrEmpty(strReturn))
            {
                GridView1.EditIndex = -1;
                BinData(Id);
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BinData(Id);
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BinData(Id);
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
            string galleryName = txtName.Value;
            if (val.Length > 0&&galleryName.Length>0)
            {
                val = "[" + val.TrimEnd(',') + "]";
                JavaScriptSerializer objSerial = new JavaScriptSerializer();
                var data = objSerial.Deserialize<List<SubmitForm>>(val);
                if (data.Count > 0)
                {
                    var ga = new Gallery();
                    ga.Name = galleryName;
                    if(Id==0)
                        Id = ga.Insert();
                    MediaObject objMediaObject = new MediaObject();
                    string strReturn = string.Empty;
                    data.ForEach(p =>
                                     {
                                         int Object_Type = Convert.ToInt32(p.Object_Type);
                                         int STT = Convert.ToInt32(p.STT);
                                         strReturn += objMediaObject.MediaObject_Add(Object_Type, p.Object_Url, p.Object_Note, STT, string.Empty, 0, Id);

                                     });
                    BinData(Id);
                    if (strReturn.Length > 0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                                                                    "<script type=\"text/javascript\">alert('" +
                                                                    strReturn + "'); window.location.href='/office/addgallery.aspx?id=" + Id + "'</script>");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("alert",
                                                       "<script type=\"text/javascript\">alert('Thêm mới thành công'); window.location.href='/office/addgallery.aspx?id=" + Id + "'</script>");
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
                BinData(Id);
            }
        }

    }

    public class SubmitForm
    {
        public string Object_Type { get; set; }
        public string Object_Url { get; set; }
        public string Object_Note { get; set; }
        public string STT { get; set; }
    }
}
