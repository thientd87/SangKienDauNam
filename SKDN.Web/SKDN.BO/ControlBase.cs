using System;
using System.IO;
using System.Text;
using System.Web.UI;

namespace BO
{
    public class ControlBase : UserControl
    {
        public ControlBase()
        {
            this.m_ManualUpdate = false;
        }
        private bool m_ManualUpdate;
        public bool ManualUpdate
        {
            get { return this.m_ManualUpdate; }
            set { this.m_ManualUpdate = value; }
        }

        private string m_ControlCacheName;
        public string ControlCacheName
        {
            get { return this.m_ControlCacheName; }
            set { this.m_ControlCacheName = value; }
        }

     

        private string m_ParentCategory;
        public string ParentCategory
        {
            get { return this.m_ParentCategory; }
            set { this.m_ParentCategory = value; }
        }

        public void SetProperty(string key, object value)
        {
            System.Reflection.PropertyInfo property = this.GetType().GetProperty(key);
            if (null != property)
            {
                property.SetValue(this, Convert.ChangeType(value, property.PropertyType), null);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder strBuilder = new StringBuilder();
            using (StringWriter strWriter = new StringWriter(strBuilder))
            {
                using (HtmlTextWriter htmlWriter = new HtmlTextWriter(strWriter))
                {
                    base.Render(htmlWriter);
                    string html = strBuilder.ToString();
                    writer.Write(html);

                    
                }
            }
        }

    }
}
