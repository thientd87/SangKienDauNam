using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BO;

namespace SKDN.Web.Pages
{
    public partial class dang_ky_tham_gia : PageBase
    {

        private static string userName = System.Configuration.ConfigurationManager.AppSettings["MailUserName"];
        private static string password = System.Configuration.ConfigurationManager.AppSettings["MailPassword"];
        private static string smtp = System.Configuration.ConfigurationManager.AppSettings["MailSmtp"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static bool SendMail(string recipient, string subject, string body, HttpPostedFile attachmentFilename)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                NetworkCredential basicCredential = new NetworkCredential(userName, password);
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(userName);

                // setup up the host, increase the timeout to 5 minutes
                smtpClient.Host = smtp;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = basicCredential;
                smtpClient.Timeout = (60 * 5 * 1000);

                message.From = fromAddress;
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                message.To.Add(recipient);

                if (attachmentFilename != null)
                {
                    Attachment attachment = new Attachment(attachmentFilename.InputStream, attachmentFilename.FileName);
                    message.Attachments.Add(attachment);
                }

                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
            
        }

        protected void btnNopBai_Click(object sender, EventArgs e)
        {
            string subject = txt_group.Value + ":" + txt_Name.Value;
            string body = "Bài dự thi Sáng Kiến Đầu Năm của nhóm <b>"+txt_group.Value + ":" + txt_Name.Value+"</b>" +
                          "<br/> Email:" + txtEmail.Value +"<br/> Tel:"+ txtTel.Value;
            if (SendMail(userName, subject, body, txtFile.PostedFile))
            {
                this.Page.RegisterClientScriptBlock("alert",
                    "<script>alert(\"Bạn đã gửi dự án thành công\"); window.location.href='/'</script>");
            }
            else
            {
                this.Page.RegisterClientScriptBlock("alert",
                    "<script>alert(\"Có lỗi xảy ra, bạn vui lòng thử lại sau\"); window.location.href='/'</script>");
            }
        }
    }
}