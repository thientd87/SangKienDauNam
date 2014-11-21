using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.API;

namespace Portal.GUI.Administrator.AdminPortal
{
    public partial class TemplateList : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void OnEditTemplate(object sender, CommandEventArgs args)
        {
            ((AdminPortal) Parent).SelectTemplate(((string) args.CommandArgument));
        }

        protected void OnAddTemplate(object sender, EventArgs args)
        {
            ((AdminPortal) Parent).AddTemplate();
        }

        protected void OnFilterTemplateByType(object sender, EventArgs args)
        {
            ((AdminPortal)Parent).FilterTemplateByType(ddrTemplateType.SelectedValue);
        }

        public void LoadData(TemplateDefinition td)
        {
            LoadData(td.templates);
        }

        private void LoadData(ArrayList subTemplateList)
        {
            ArrayList templateList = new ArrayList();
            foreach (TemplateDefinition.Template t in subTemplateList)
            {
                DisplayTemplateItem dt = new DisplayTemplateItem();
                templateList.Add(dt);

                dt.m_Type = t.type;
                dt.m_Reference = t.reference;
            }

            Templates.DataSource = templateList;
            Templates.DataBind();

            //Load data to TemplateType dropdownList
            string[] _arrTemplateType = "".Split(',');
            ArrayList _arrTemplateTypeList = new ArrayList();
            _arrTemplateTypeList.Add(string.Empty);

            for (int i = 0; i < _arrTemplateType.Length; i++)
            {
                _arrTemplateTypeList.Add(_arrTemplateType[i]);
            }
            ddrTemplateType.DataSource = _arrTemplateTypeList;
            ddrTemplateType.DataBind();
        }

        public void LoadData(TemplateDefinition td, string templateType)
        {
            LoadData(td.templates,templateType);
        }

        private void LoadData(ArrayList subTemplateList, string templateType)
        {
            ArrayList templateList = new ArrayList();
            foreach (TemplateDefinition.Template t in subTemplateList)
            {
                DisplayTemplateItem dt = new DisplayTemplateItem();
                if(templateType.Equals(t.type))
                {
                    templateList.Add(dt);

                    dt.m_Type = t.type;
                    dt.m_Reference = t.reference;    
                }
            }

            Templates.DataSource = templateList;
            Templates.DataBind();
        }



        #region Nested type: DisplayTemplateItem

        /// <summary>
        /// Wrapper Class for the Template Object.
        /// </summary>
        public class DisplayTemplateItem
        {
            internal string m_Reference = "";
            internal string m_Type = "";

            /// <summary>
            /// Template Type
            /// </summary>
            public string Type
            {
                get { return m_Type; }
            }

            /// <summary>
            /// Column Reference
            /// </summary>
            public string Reference
            {
                get { return m_Reference; }
            }
        }

        #endregion
    }
}