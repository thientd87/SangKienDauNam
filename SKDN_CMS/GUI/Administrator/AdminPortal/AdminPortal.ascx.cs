namespace Portal.GUI.Administrator.AdminPortal
{
    using System;
    using System.Collections;
    using System.Web;
    using iiuga.Web.UI;
    using Portal.API;
    using System.Web.Security;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI;
    using Portal.User.Security;
    /// <summary>
    ///		Summary description for AdminPortal.
    /// </summary>
    public partial class AdminPortal : Module,IPostBackEventHandler
    {
        private string CurrentParentReference = "";
        private string CurrentReference = "";
        private string CurrentTemplateReference = "";
        private string CurrentTemplateType = "";

        #region IPostBackEventHandler Members

        public void RaisePostBackEvent(string args)
        {
            SelectTab(args);
            if(string.Empty.Equals(args))
            {
                SelectTemplate("");   
            }
        }

        #region TabEventHandler

        #region OnSave

        protected void OnSave(object sender, PortalDefinition.Tab t)
        {
            BuildTree();
            SelectTab(CurrentParentReference);
        }

        #endregion

        #region OnCancel

        protected void OnCancel(object sender, PortalDefinition.Tab t)
        {
            BuildTree();
            SelectTab(CurrentParentReference);
            SelectTemplate("");
        }

        #endregion

        #region OnDelete

        protected void OnDelete(object sender, PortalDefinition.Tab t)
        {
            BuildTree();
            SelectTab(CurrentParentReference);
        }

        #endregion

        #region OnSaveTemplate

        protected void OnSaveTemplate(object sender, PortalDefinition.Tab t)
        {
            BuildTree();
            SelectTab(CurrentParentReference);
        }

        #endregion

        #endregion

        #region TemplateEventHandler

        #region OnSave

        protected void OnSave(object sender, TemplateDefinition.Template t)
        {
            SelectTab("");
            SelectTemplate(""); 
        }

        #endregion

        #region OnCancel

        protected void OnCancel(object sender, TemplateDefinition.Template t)
        {
            SelectTab("");
            SelectTemplate(""); 
        }

        #endregion

        #region OnDelete

        protected void OnDelete(object sender, TemplateDefinition.Template t)
        {
            SelectTab("");
            SelectTemplate(""); 
        }

        #endregion

        #endregion

        #region Read and Write ViewState

        protected override void LoadViewState(object bag)
        {
            base.LoadViewState(bag);
            CurrentReference = (string) ViewState["CurrentReference"];
            CurrentParentReference = (string) ViewState["CurrentParentReference"];
        }

        protected override object SaveViewState()
        {
            ViewState["CurrentReference"] = CurrentReference;
            ViewState["CurrentParentReference"] = CurrentParentReference;
            return base.SaveViewState();
        }

        #endregion

        #region BuildTree

        internal void BuildTree()
        {
            PortalDefinition pd = PortalDefinition.Load();
            tree.Elements[0].Elements.Clear();

            InternalBuildTree(pd.tabs, tree.Elements[0]);

            tree.Elements[0].Expand();
            tree.Elements[0].Text = "<a class=\"Menu_link\" href=" +
                                    Page.GetPostBackClientHyperlink(this, "") +
                                    ">Portal</a>";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabs">IList - can be a Array or ArrayList</param>
        /// <param name="parent"></param>
        private void InternalBuildTree(ArrayList tabs, TreeElement parent)
        {
            foreach (PortalDefinition.Tab t in tabs)
            {
                int n = parent.Elements.Add(t.title);
                parent.Elements[n].Key = t.reference;
                parent.Elements[n].Text = "<a class=\"Menu_link\" href=" +
                                          Page.GetPostBackClientHyperlink(this, t.reference) +
                                          ">" + t.title + "</a>";

                if (t.tabs != null && t.tabs.Count != 0)
                {
                    InternalBuildTree(t.tabs, parent.Elements[n]);
                    //parent.Elements[n].Expand();
                }
                else
                {
                    parent.Elements[n].ImageIndex = 0;
                }
            }
        }

        #endregion

        #region Tab Functions

        #region SelectTab

        public void SelectTab(string reference)
        {
            PortalDefinition pd = PortalDefinition.Load();
            CurrentReference = reference;
            if (reference == "") // Root Node
            {
                //Hidden TemplateEdit and TabEdit when load first time
                TabCtrl.Visible = false;
                TabListCtrl.Visible = true;

                //Load data to TabList
                CurrentParentReference = "";
                TabListCtrl.LoadData(pd);
            }
            else
            {
                //Hidden Template control when edit Tab
                TemplateCtrl.Visible = false;
                TemplateListCtrl.Visible = false;

                PortalDefinition.Tab t = pd.GetTab(reference);
                CurrentParentReference = t.parent != null ? t.parent.reference : "";
                TabListCtrl.LoadData(t);
                TabCtrl.Visible = true;
                TabCtrl.LoadData(reference);
            }
        }

        #endregion

        #region AddTab

        public void AddTab()
        {
            PortalDefinition pd = PortalDefinition.Load();
            PortalDefinition.Tab t = PortalDefinition.Tab.Create();

            if (CurrentReference == "") // Root Node
            {
                pd.tabs.Add(t);
            }
            else
            {
                PortalDefinition.Tab pt = pd.GetTab(CurrentReference);
                pt.tabs.Add(t);
            }

            pd.Save();

            BuildTree();
            SelectTab(t.reference);
        }

        #endregion

        #region Move Tab

        internal void MoveTabUp(int idx)
        {
            if (idx <= 0) return;

            PortalDefinition pd = PortalDefinition.Load();
            ArrayList a = null;
            if (CurrentReference == "")
            {
                // Root
                a = pd.tabs;
            }
            else
            {
                PortalDefinition.Tab pt = pd.GetTab(CurrentReference);
                a = pt.tabs;
            }

            PortalDefinition.Tab t = (PortalDefinition.Tab) a[idx];
            a.RemoveAt(idx);
            a.Insert(idx - 1, t);

            pd.Save();

            // Rebind
            BuildTree();
            SelectTab(CurrentReference);
        }

        internal void MoveTabDown(int idx)
        {
            PortalDefinition pd = PortalDefinition.Load();
            ArrayList a = null;
            if (CurrentReference == "")
            {
                // Root
                a = pd.tabs;
            }
            else
            {
                PortalDefinition.Tab pt = pd.GetTab(CurrentReference);
                a = pt.tabs;
            }

            if (idx >= a.Count - 1) return;

            PortalDefinition.Tab t = (PortalDefinition.Tab) a[idx];
            a.RemoveAt(idx);
            a.Insert(idx + 1, t);

            pd.Save();

            // Rebind
            BuildTree();
            SelectTab(CurrentReference);
        }

        #endregion

        #endregion

        #region Template Functions

        #region SelectTemplate

        public void SelectTemplate(string reference)
        {
            TemplateDefinition td = TemplateDefinition.Load();
            CurrentTemplateReference = reference;
            if (reference == "") // Root Node
            {
                //Hidden TabEdit and TemplateEdit when Load first time
                TemplateCtrl.Visible = false;
                TemplateListCtrl.Visible = true;

                //Load data to TemplateList
                TemplateListCtrl.LoadData(td);
            }
            else
            {
                //Hidden TabEdit and TabList control when Edit Template
                TabListCtrl.Visible = false;
                TabCtrl.Visible = false;

                //Show TemplateEdit but hidden TemplateList
                TemplateDefinition.Template t = td.GetTemplate(reference);
                TemplateListCtrl.Visible = false;
                TemplateCtrl.Visible = true;
                TemplateCtrl.LoadData(reference);
            }
        }

        public void FilterTemplateByType(string templateType)
        {
            TemplateDefinition td = TemplateDefinition.Load();
            CurrentTemplateType = templateType;
            if (templateType == "") // Don't filter
            {
                //Hidden TabEdit and TemplateEdit when Load first time
                TemplateCtrl.Visible = false;
                TemplateListCtrl.Visible = true;

                //Load data to TemplateList
                TemplateListCtrl.LoadData(td);
            }
            else
            {
                //Hidden TabEdit and TemplateEdit when Load first time
                TemplateCtrl.Visible = false;
                TemplateListCtrl.Visible = true;

                //Load data to TemplateList
                TemplateListCtrl.LoadData(td, templateType);
            }
        }

        #endregion

        #region AddTemplate

        public void AddTemplate()
        {
            TemplateDefinition td = TemplateDefinition.Load();
            TemplateDefinition.Template t = TemplateDefinition.Template.Create();

            td.templates.Add(t);

            td.Save();

            SelectTemplate(t.reference);
        }

        #endregion

        #endregion

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkUsers.NavigateUrl = "~/users.aspx";
            lnkEditoral.NavigateUrl = "~/office.aspx";
            //lnkCat.NavigateUrl = "~/category.aspx";
            lnkChuyenMuc.NavigateUrl = "~/chuyenmuc.aspx";

            MainSecurity objsec = new MainSecurity();
            Role objrole = objsec.GetRole(Page.User.Identity.Name, Portal.API.Config.CurrentChannel);
            if (objrole.isQuanTriKenh && !objrole.isAdministrator && !objrole.isTongBienTap && !objrole.isPhuTrachKenh)
            {
                div3.Visible = false;
                //div2.Visible = false;
            }
				
            if (!IsPostBack)
            {
                TabCtrl.Visible = false;
                BuildTree();
                SelectTab("");
                SelectTemplate("");
            }
        }

        protected void itemLogOut_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["PortalUser"];
            if (cookie != null)
            {
                cookie.Values["AC"] = "";
                cookie.Values["PW"] = "";
                DateTime dt = DateTime.Now;
                dt.AddDays(-1);
                cookie.Expires = dt;
                Response.Cookies.Add(cookie);
            }
            FormsAuthentication.SignOut();
            Response.Redirect("/Login.aspx");
        }
    }
}