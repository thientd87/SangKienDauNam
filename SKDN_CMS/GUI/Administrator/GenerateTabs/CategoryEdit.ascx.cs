namespace Portal.GUI.Administrator.GenerateTabs {
    using System;
    using Portal.Core.DAL;
    using System.Collections;

    public partial class CategoryEdit : System.Web.UI.UserControl {

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                Portal.BO.Editoral.Category.CategoryHelper.Treebuild(cboCat);
                Portal.BO.Editoral.EditionType.EditionTypeHelper.SelectAllForDropdownlist(drEditionType);
                //odsEditionType.Select();
                //drEditionType.DataBind();

                if (Request.QueryString["parent"] != null)
                    cboCat.SelectedValue = Request.QueryString["parent"].ToString();
                if (Request.QueryString["NewsRef"] != "") {
                    btnSave.Visible = false;
                    int intCurrentCat = 0;
                    try {
                        intCurrentCat = Convert.ToInt32(Request.QueryString["NewsRef"].ToString());
                    } catch { }
                    hdCatID.Value = intCurrentCat.ToString();
                    BindCatForm(Portal.BO.Editoral.Category.CategoryHelper.getCatInfo(intCurrentCat));
                } else
                    btnUpdate.Visible = false;
            }
        }

        private void BindCatForm(ArrayList _arr) {
            try {
                txtCategoryName.Text = _arr[0].ToString();
                if (_arr[1] != null)
                    txtCategoryDescription.Text = _arr[1].ToString();

                if (_arr[2] != null)
                    txtCategoryDisplayURL.Text = _arr[2].ToString();

                if (_arr[3] != null)
                    cboCat.SelectedValue = _arr[3].ToString();

                if (_arr[4] != null)
                    chkIsColumn.Checked = Convert.ToBoolean(_arr[4]);

                if (_arr[5] != null)
                    chkIsHidden.Checked = Convert.ToBoolean(_arr[5]);

                if (_arr[8] != null)
                    drEditionType.SelectedValue = _arr[8].ToString();
            } catch { }


        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
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
        private void InitializeComponent() {

        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e) {
            objCatSource.Insert();
            Response.Redirect("~/category/catlist.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e) {
            bool checkUpdate = Portal.BO.Editoral.Category.CategoryHelper.CheckUpdateCate(Convert.ToInt32(hdCatID.Value), Convert.ToInt32(cboCat.SelectedValue), true);
            if (checkUpdate) {
                objCatSource.Update();
                Response.Redirect("~/category/catlist.aspx");
            } else
                Page.RegisterClientScriptBlock("category", "<script language='javascript'>alert('Chuyên mục cha không hợp lệ')</script>");
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect("~/category/catlist.aspx");
        }
    }
}
