using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.GUI.Administrator.AdminPortal
{
    /// <summary>
    ///		Summary description for ColumnList.
    /// </summary>
    public partial class ColumnList : UserControl
    {
        public event Tab.ColumnEventHandler AddColumn = null;
        public event Tab.ColumnEventHandler AddSubColumn = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Put user code to initialize the page here
        }

        public void LoadColumnList(PortalDefinition.Tab _objTab)
        {
            LoadColumnList(_objTab.Columns);
            lnkAddColumn.Visible = true;
            lnkAddSubColumn.Visible = false;
            lnkAddColumn.CommandArgument = _objTab.reference;
            lblColumnListTitle.LanguageRef = "ColumnList";
        }

        public void LoadColumnList(TemplateDefinition.Template _objTemplate)
        {
            LoadColumnList(_objTemplate.Columns);
            lnkAddColumn.Visible = true;
            lnkAddSubColumn.Visible = false;
            lnkAddColumn.CommandArgument = _objTemplate.reference;
            lblColumnListTitle.LanguageRef = "ColumnList";
        }

        public void LoadColumnList(string _strColumnRef)
        {
            PortalDefinition _objPortal = PortalDefinition.Load();
            PortalDefinition.Column _objColumn = _objPortal.GetColumn(_strColumnRef);

            if (_objColumn != null)
            {
                LoadColumnList(_objColumn.Columns);
                lnkAddColumn.Visible = false;
                lnkAddSubColumn.Visible = true;
                lnkAddSubColumn.CommandArgument = _objColumn.ColumnReference;
                lblColumnListTitle.LanguageRef = "SubColumnList";
            }
        }

        private void LoadColumnList(ArrayList _arrColumns)
        {
            ArrayList _arrColumnList = new ArrayList();
            foreach (PortalDefinition.Column _objColumn in _arrColumns)
            {
                DisplayColumnItem _dciColumn = new DisplayColumnItem();

                _dciColumn.m_Name = _objColumn.ColumnName;
                _dciColumn.m_Reference = _objColumn.ColumnReference;

                _arrColumnList.Add(_dciColumn);
            }
            dgrColumns.DataSource = _arrColumnList;
            dgrColumns.DataBind();
        }

        protected void OnAddColumn(object sender, CommandEventArgs e)
        {
            if (Parent is Tab)
            {
                PortalDefinition pd = PortalDefinition.Load();
                PortalDefinition.Tab t = pd.GetTab(e.CommandArgument.ToString());

                if (t != null)
                {
                    PortalDefinition.Column _objNewColumn = PortalDefinition.Column.Create(t);

                    t.Columns.Add(_objNewColumn);

                    pd.Save();

                    if (AddColumn != null)
                    {
                        AddColumn(this, _objNewColumn);
                    }
                }
            }
            else if (Parent is Template)
            {
                TemplateDefinition td = TemplateDefinition.Load();
                TemplateDefinition.Template t = td.GetTemplate(e.CommandArgument.ToString());

                if (t != null)
                {
                    PortalDefinition.Column _objNewColumn = PortalDefinition.Column.Create(t);

                    t.Columns.Add(_objNewColumn);

                    td.Save();

                    if (AddColumn != null)
                    {
                        AddColumn(this, _objNewColumn);
                    }
                }
            }
        }

        protected void OnAddSubColumn(object sender, CommandEventArgs e)
        {
            if (Parent is Tab)
            {
                PortalDefinition pd = PortalDefinition.Load();
                PortalDefinition.Column _objCurrentColumn = pd.GetColumn(e.CommandArgument.ToString());
                if (_objCurrentColumn != null)
                {
                    PortalDefinition.Column _objNewColumn = PortalDefinition.Column.Create(_objCurrentColumn);

                    _objCurrentColumn.Columns.Add(_objNewColumn);

                    pd.Save();
                    if (AddSubColumn != null)
                    {
                        AddSubColumn(this, _objNewColumn);
                    }
                }
            }
            else if (Parent is Template)
            {
                TemplateDefinition td = TemplateDefinition.Load();
                PortalDefinition.Column _objCurrentColumn = td.GetColumn(e.CommandArgument.ToString());
                if (_objCurrentColumn != null)
                {
                    PortalDefinition.Column _objNewColumn = PortalDefinition.Column.Create(_objCurrentColumn);

                    _objCurrentColumn.Columns.Add(_objNewColumn);

                    td.Save();
                    if (AddSubColumn != null)
                    {
                        AddSubColumn(this, _objNewColumn);
                    }
                }
            }
        }

        protected void OnEditColumn(object sender, CommandEventArgs e)
        {
            if (Parent is Tab)
            {
                ((Tab) Parent).EditColumn(e.CommandArgument.ToString());
            }
            else if (Parent is Template)
            {
                ((Template) Parent).EditColumn(e.CommandArgument.ToString());
            }
        }

        protected void OnMoveColumnLeft(object sender, CommandEventArgs e)
        {
            if (Parent is Tab)
            {
                ((Tab) Parent).MoveColumnLeft(e.CommandArgument.ToString());
            }
            else if (Parent is Template)
            {
                ((Template) Parent).MoveColumnLeft(e.CommandArgument.ToString());
            }
        }

        protected void OnMoveColumnRight(object sender, CommandEventArgs e)
        {
            if (Parent is Tab)
            {
                ((Tab) Parent).MoveColumnRight(e.CommandArgument.ToString());
            }
            else if (Parent is Template)
            {
                ((Template) Parent).MoveColumnRight(e.CommandArgument.ToString());
            }
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion

        #region Nested type: DisplayColumnItem

        /// <summary>
        /// Wrapper Class for the Column Object.
        /// </summary>
        public class DisplayColumnItem
        {
            internal int m_Index = 0;
            internal string m_Name = "";
            internal string m_Reference = "";

            /// <summary>
            /// Column Name
            /// </summary>
            public string ColumnName
            {
                get { return m_Name; }
            }

            /// <summary>
            /// Column Index
            /// </summary>
            public int ColumnIndex
            {
                get { return m_Index; }
            }

            /// <summary>
            /// Column Reference
            /// </summary>
            public string ColumnReference
            {
                get { return m_Reference; }
            }
        }

        #endregion
    }
}