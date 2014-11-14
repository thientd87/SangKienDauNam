using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;

namespace BO
{
    public class PageBase : Page
    {
        // Fields
        private static readonly Assembly _currentAssembly = Assembly.GetExecutingAssembly();
      

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter writer2 = new StringWriter(sb))
            {
                string action = (HttpContext.Current.Items["VirtualUrl"] != null) ? HttpContext.Current.Items["VirtualUrl"].ToString() : "";
                using (RewriteFormHtmlTextWriter writer3 = new RewriteFormHtmlTextWriter(writer2, action))
                {
                    base.Render(writer3);
                    string str2 = sb.ToString().Replace("\t", " ").Replace("    ", " ").Replace("  ", " ");
                    writer.Write(str2);
                }
            }
        }

      

        // Nested Types
        private class RewriteFormHtmlTextWriter : HtmlTextWriter
        {
            // Fields
            private string _formAction;

            // Methods
            public RewriteFormHtmlTextWriter(TextWriter writer)
                : base(writer)
            {
            }

            public RewriteFormHtmlTextWriter(TextWriter writer, string action)
                : base(writer)
            {
                if (!string.IsNullOrEmpty(action))
                {
                    this._formAction = action;
                }
            }

            public override void RenderBeginTag(string tagName)
            {
                if (tagName.ToString().IndexOf("form") >= 0)
                {
                    base.RenderBeginTag(tagName);
                }
            }

            public override void WriteAttribute(string name, string value, bool fEncode)
            {
                if (!(!("action" == name) || string.IsNullOrEmpty(this._formAction)))
                {
                    value = this._formAction;
                }
                base.WriteAttribute(name, value, fEncode);
            }
        }
    }


}
