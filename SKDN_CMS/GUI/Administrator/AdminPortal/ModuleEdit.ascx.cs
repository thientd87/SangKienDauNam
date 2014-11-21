using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Portal.GUI.Administrator.AdminPortal
{
	using System;
	using System.Collections;
	using System.Web;
	using System.Web.UI.WebControls;
	using Portal.API;

	/// <summary>
	///		Summary description for Module.
	/// </summary>
	public partial  class ModuleEdit : System.Web.UI.UserControl
	{
		protected Roles RolesCtrl;

	
		private string CurrentReference = "";

		PortalDefinition.Module _module;
	
		public string Reference
		{
			get { return CurrentReference; } 
		}

		private string CurrentTabReference = "";
		public string TabReference
		{
			get { return CurrentTabReference; } 
		}

		public event EventHandler Save = null;
		public event EventHandler Cancel = null;
		public event EventHandler Delete = null;
		
		protected override void LoadViewState(object bag)
		{
			base.LoadViewState(bag);
			CurrentReference = (string)ViewState["CurrentReference"];
			CurrentTabReference = (string)ViewState["CurrentTabReference"];
		}
		protected override object SaveViewState()
		{
			ViewState["CurrentReference"] = CurrentReference;
			ViewState["CurrentTabReference"] = CurrentTabReference;
			return base.SaveViewState();
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected void OnValidateCBType(object sender, ServerValidateEventArgs args)
		{
			args.IsValid = cbType.SelectedIndex > 0;
		}

		protected void OnCancel(object sender, EventArgs args)
		{
			if(Cancel != null)
			{
				Cancel(this, new EventArgs());
			}
		}

		/// <summary>
		/// Lưu các thiết lập của Module
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		protected void OnSave(object sender, EventArgs args)
		{
			try
			{
				if(Page.IsValid)
				{
					// Nạp cấu trúc Portal
					PortalDefinition pd = PortalDefinition.Load();
					PortalDefinition.Tab t = pd.GetTab(CurrentTabReference);
					// Truy xuất đến cấu trúc Module hiện thời
					PortalDefinition.Module m = t.GetModule(CurrentReference);

					// Thay đổi các thông số tương ứng
					m.reference = txtReference.Text;
					m.title = HttpUtility.HtmlEncode(txtTitle.Text);
					m.type =cboPath.SelectedValue +"/"+ cbType.SelectedItem.Value;
					m.roles = RolesCtrl.GetData();
					m.CacheTime = Convert.ToInt32(txtCacheTime.Text);

					// Lưu các thông số và cấu trúc
					pd.Save();

					// Lưu các thông số khi thực thi của module
					m.LoadModuleSettings();
					m.LoadRuntimeProperties();
					for (int _intPropertyCount = 0; _intPropertyCount < rptRuntimeProperties.Items.Count; _intPropertyCount ++)
					{
						HtmlInputHidden _hihPropertyName = rptRuntimeProperties.Items[_intPropertyCount].FindControl("lblPropertyName") as HtmlInputHidden;
						TextBox _txtPropertyValue = rptRuntimeProperties.Items[_intPropertyCount].FindControl("txtPropertyValue") as TextBox;
						DropDownList _drdAvaiableValues = rptRuntimeProperties.Items[_intPropertyCount].FindControl("drdAvaiableValues") as DropDownList;

						if (_hihPropertyName != null && _txtPropertyValue != null)
						{
							string _strPropertyValue = _txtPropertyValue.Visible ? _txtPropertyValue.Text : _drdAvaiableValues.SelectedValue;
							m.moduleRuntimeSettings.SetRuntimePropertyValue(true, _hihPropertyName.Value, _strPropertyValue);
						}
					}
					m.SaveRuntimeSettings();

					CurrentReference = m.reference;

					// Phát sinh sự kiện lưu thông tin thành công
					if(Save != null)
					{
						Save(this, new EventArgs());
					}
				}
			}
			catch(Exception e)
			{
				lbError.Text = e.Message;
			}
		}
		protected void OnDelete(object sender, EventArgs args)
		{
			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(CurrentTabReference);

			PortalDefinition.Module _objDeleteModule = t.GetModule(CurrentReference);
			t.DeleteModule(CurrentReference);
			_objDeleteModule.DeleteRuntimeProperties();
			
			pd.Save();

			if(Delete != null)
			{
				Delete(this, new EventArgs());
			}

			// Hopefully we where redirected here!
		}

		public void LoadData(string tabRef, string moduleRef)
		{
			CurrentTabReference = tabRef;
			CurrentReference = moduleRef;

			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(CurrentTabReference);
			_module = t.GetModule(CurrentReference);

			// Load Module Common Properties
			txtTitle.Text = HttpUtility.HtmlDecode(_module.title);
			txtReference.Text = _module.reference;
			txtCacheTime.Text = _module.CacheTime.ToString();

            cboPath.ClearSelection();
			cbType.ClearSelection();
            //lay ve duong dan den file module tinh tu RootModule
            string strModule = _module.type, strPth = "";
            string[] strModulePath = strModule.Split("/".ToCharArray());
            if (strModulePath.Length == 2)
            {
                strPth = strModulePath[0];
                strModule = strModulePath[1];
            }
            ListItem pli = cboPath.Items.FindByValue(strPth);
			if (pli != null)
                pli.Selected = true;
            //goi load du lieu cho type
            LoadModuleTypes(strPth);
            ListItem li = cbType.Items.FindByValue(strModule);
			if(li != null)
			{
				li.Selected = true;
			}

			// Load Roles List
			RolesCtrl.LoadData(_module.roles);
            RolesCtrl.ShowRoleType = false;
			// Load Module's Runtime Properties
			_module.LoadModuleSettings();
			_module.LoadRuntimeProperties();
			rptRuntimeProperties.DataSource = _module.GetRuntimePropertiesSource(true);
			rptRuntimeProperties.DataBind();
		}
        private void LoadModuleGroup()
        {
            // Get Module List
            string[] dirs = System.IO.Directory.GetDirectories(Config.GetModulePhysicalPath());
            int idx = Config.GetModulePhysicalPath().Length;
            for (int i = 0; i < dirs.Length; i++)
            {
                dirs[i] = dirs[i].Substring(idx);
            }
            // Add empty ListItem
            ArrayList dirList = new ArrayList();
            dirList.Add("");
            dirList.AddRange(dirs);

            // Bind
            cboPath.DataSource = dirList;
            cboPath.DataBind();
        }
		private void LoadModuleTypes(string _currDir)
		{
			// Get Module List
            string path = Config.GetModulePhysicalPath();
            if (_currDir.ToLower().IndexOf("front_end") >= 0)
            {
                // Neu la Front End, thi tro toi FrontEnd
                path = System.Configuration.ConfigurationSettings.AppSettings["ControlFrontEnd"];
            }

            string[] dirs = System.IO.Directory.GetDirectories(path  + _currDir);
            int idx = (path + _currDir + @"\").Length;
			for(int i=0;i<dirs.Length;i++)
			{
				dirs[i] = dirs[i].Substring(idx);
			}
			// Add empty ListItem
			ArrayList dirList = new ArrayList();
			dirList.Add("");
			dirList.AddRange(dirs);

			// Bind
			cbType.DataSource = dirList;
			cbType.DataBind();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);

			if(!IsPostBack)
			{
                LoadModuleGroup();
				//LoadModuleTypes();
			}
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.rptRuntimeProperties.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptRuntimeProperties_ItemDataBound);

		}
		#endregion

		/// <summary>
		/// Thủ tục nạp danh sách các tham số thực thi của Module - Cho them truong hop load cac tham so truyen vao duoc lay tu Cat
        /// De lam duoc the thi quy uoc mot danh dau nao do.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rptRuntimeProperties_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				// Lấy danh sách các gái trị cho sẵn của tham số đang xét
				ArrayList _arrAvaiableValues = _module.GetAvaiblePropertyValues(true, DataBinder.Eval(e.Item.DataItem, "Name").ToString());

				// Tìm điều khiển hiển thị danh sách các giá trị cho sẵn
				DropDownList _drdAvaiableValues = e.Item.FindControl("drdAvaiableValues") as DropDownList;

				// Tìm điều khiển hiển thị giá trị nhập bằng tay của tham số đang xét
				TextBox _txtPropertyValue = e.Item.FindControl("txtPropertyValue") as TextBox;

				if (_drdAvaiableValues != null && _arrAvaiableValues != null && _arrAvaiableValues.Count > 0)
				{
					_drdAvaiableValues.Visible = true;
					_txtPropertyValue.Visible = false;
					
					// Hiển thị danh sách các giá trị cho sẵn của tham số đang xét
					ArrayList _arrDisplayPropertyValues = new ArrayList();
					foreach(ModulePropertyValue _objPropertyValue in _arrAvaiableValues)
					{
						_arrDisplayPropertyValues.Add(new ModulePropertyValueDisplayItem(_objPropertyValue.AvaiableKey, _objPropertyValue.AvaiableValue));
					}
					_drdAvaiableValues.DataTextField = "AvaiableKey";
					_drdAvaiableValues.DataValueField = "AvaiableValue";
					_drdAvaiableValues.DataSource = _arrDisplayPropertyValues;
					_drdAvaiableValues.DataBind();
                    //Cho nay co the quy dinh them datasource co the load tu cat ra.
					// Chọn lại giá trị tương ứng trong danh sách các giá trị có sẵn.
					object _strSelectedPropertyValue = DataBinder.Eval(e.Item.DataItem, "Value");
					ListItem _objSelectedPropertyValueItem = _drdAvaiableValues.Items.FindByValue(_strSelectedPropertyValue == null ? "" : _strSelectedPropertyValue.ToString());
					if (_objSelectedPropertyValueItem != null) _objSelectedPropertyValueItem.Selected = true;
				}
				else
				{
                    string strName = DataBinder.Eval(e.Item.DataItem, "Name").ToString();
                    //Neu truong hop lay cat lam thuoc tinh de set thi ten truyen vao dat cung la "catparam"
                    if (strName.ToLower() == "catparam")
                    {
                        Portal.BO.Editoral.Category.CategoryHelper.bindAllCat(_drdAvaiableValues);
                        _drdAvaiableValues.SelectedValue = DataBinder.Eval(e.Item.DataItem, "Value").ToString();
                        _drdAvaiableValues.Visible = true;
                        _txtPropertyValue.Visible = false;
                    }
                    else
                    {
                        _drdAvaiableValues.Visible = false;
                        _txtPropertyValue.Visible = true;
                    }

				}
			}
		}

        protected void cboPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strCurrPath = cboPath.SelectedValue;
            LoadModuleTypes(strCurrPath);
        }
	}

	public class ModulePropertyValueDisplayItem
	{
		private string _strAvaiableKey = "";
		private string _strAvaiableValue = "";

		public string AvaiableKey
		{
			get{return _strAvaiableKey;}
			set{_strAvaiableKey = value;}
		}

		public string AvaiableValue
		{
			get{return _strAvaiableValue;}
			set{_strAvaiableValue = value;}
		}

		public ModulePropertyValueDisplayItem(string _key, string _value)
		{
			_strAvaiableKey = _key;
			_strAvaiableValue = _value;
		}
	}
}
