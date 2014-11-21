using System.Web.UI.WebControls;

namespace Intelliworks.Modules.CustomControls.PollManager
{
	using System;
	using System.Data;

	public delegate void PageChangeEventHandler(PageChangeEventArgs e);

	/// <summary>
	///		Summary description for uscPager.
	/// </summary>
	public partial class uscPager : System.Web.UI.UserControl
	{
		public event PageChangeEventHandler PageChanged;

		/// <summary>
		/// Thủ tục phát sinh sự kiện thay đổi trang được chọn
		/// </summary>
		/// <param name="e">Đối tượng lưu trữ số trang được chọn khi phát sinh sự kiện</param>
		protected virtual void OnPageChanged(PageChangeEventArgs e)
		{
			if (PageChanged != null)
			{
				CurrentPage = e.PageIndex;
				PageChanged(e);
			}
		}

		private void uscPager_Load(object sender, EventArgs e)
		{
			// Set Default Selected Page
			if (!IsPostBack)
				CurrentPage = 1;
		}

		#region Public Properties
		/// <summary>
		/// Text hiển thị của liên kết trang sau
		/// </summary>
		public string NextPageText
		{
			get{return lnkNextPage.Text;}
			set{lnkNextPage.Text = value;}
		}

		/// <summary>
		/// Text hiển thị của liên kết trang trước
		/// </summary>
		public string PreviousPageText
		{
			get{return lnkPreviousPage.Text;}
			set{lnkPreviousPage.Text = value;}
		}

		/// <summary>
		/// Text hiển thị của liên kết trang đầu
		/// </summary>
		public string FirstPageText
		{
			get{return lnkFirstPage.Text;}
			set{lnkFirstPage.Text = value;}
		}

		/// <summary>
		/// Text hiển thị của liên kết trang cuối
		/// </summary>
		public string LastPageText
		{
			get{return lnkLastPage.Text;}
			set{lnkLastPage.Text = value;}
		}

		/// <summary>
		/// Text hiển thị của liên kết trang cuối
		/// </summary>
		public string PageCaption
		{
			get{return lblPageCaption.Text;}
			set{lblPageCaption.Text = value;}
		}

		/// <summary>
		/// Số lượng trang tối đa sẽ hiển thị trong danh sách số trang
		/// </summary>
		public int DisplayNum
		{
			get {return Convert.ToInt32(lblMaxDisplay.Value);}
			set {lblMaxDisplay.Value = value.ToString();}
		}

		/// <summary>
		/// Tổng số trang cần hiển thị
		/// </summary>
		public int PageCount
		{
			get{return Convert.ToInt32(lblPageCount.Value);}
			set
			{
				lblPageCount.Value = value.ToString();

//				// Kiểm tra xem có hiển thị các link Text không
//				switch(value)
//				{
//					case 1:
//					case 0:
//						lnkFirstPage.Visible = false;
//						lnkLastPage.Visible = false;
//						lnkNextPage.Visible = false;
//						lnkPreviousPage.Visible = false;
//						rptPageList.Visible = false;
//						break;
//					default:
//						lnkFirstPage.Visible = true;
//						lnkLastPage.Visible = true;
//						lnkNextPage.Visible = true;
//						lnkPreviousPage.Visible = true;
//						rptPageList.Visible = true;
//						break;
//				}
			}
		}

		/// <summary>
		/// Chỉ số của trang hiện thời
		/// </summary>
		public int CurrentPage
		{
			get
			{
				try
				{
					int _intPage = Convert.ToInt32(lblCurrentPage.Value);
					return (_intPage > 0) ? _intPage : 1;
				}
				catch
				{
					return 1;
				}
			}
			set
			{
				// Lưu chỉ số đã chọn
				lblCurrentPage.Value = value.ToString();

				// Kiểm tra xem có hiển thị các link Text không
				if (PageCount == value)
				{
					lnkFirstPage.Visible = true;
					lnkLastPage.Visible = false;
					lnkNextPage.Visible = false;
					lnkPreviousPage.Visible = true;
				}
				if (1 == value)
				{
					lnkFirstPage.Visible = false;
					lnkLastPage.Visible = true;
					lnkNextPage.Visible = true;
					lnkPreviousPage.Visible = false;
				}
			}
		}

		/// <summary>
		/// Số Item tối đa của 1 trang
		/// </summary>
		public int PageSize
		{
			get
			{
				try
				{
					return Convert.ToInt32(lblPageSize.Value);
				}
				catch
				{
					// Nếu có lỗi thì mặc định là 10
					return 10;
				}
			}

			set
			{
				lblPageSize.Value = value.ToString();
			}
		}

		/// <summary>
		/// User can use paging feature. ReadOnly
		/// </summary>
		public bool AllowPaging
		{
			set
			{
				lnkFirstPage.Enabled = value;
				lnkLastPage.Enabled = value;
				lnkNextPage.Enabled = value;
				lnkPreviousPage.Enabled = value;
				rptPageList.Visible = value;
			}
		}
		#endregion

		#region Bind Page List
		/// <summary>
		/// Lấy danh sách số trang
		/// </summary>
		/// <returns>DataTable chứa 1 cột danh sách số trang "PageNum"</returns>
		private DataTable GetPageList()
		{
			DataTable _objPageList = new DataTable("PageList");
			_objPageList.Columns.Add("PageNum");
			object[] _ColArray = new object[1];
			for (int _intCount = 0; _intCount < PageCount; _intCount++)
			{
				_ColArray[0] = _intCount + 1;
				DataRow _objNewRow = _objPageList.NewRow();
				_objNewRow.ItemArray = _ColArray;
				_objPageList.Rows.Add(_objNewRow);
			}
			return _objPageList;
		}

		/// <summary>
		/// Hiển thị danh sách liên kết đến các trang
		/// </summary>
		public void ShowPageList()
		{
			rptPageList.DataSource = GetPageList();
			rptPageList.DataBind();
			lblPageStatus.Text = "&nbsp;" + CurrentPage.ToString() + "/" + PageCount;
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
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
			this.rptPageList.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptPageList_ItemDataBound);
			this.rptPageList.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.rptPageList_ItemCommand);
			this.Load += new EventHandler(uscPager_Load);
		}
		#endregion

		#region Navigation Handler
		protected void lnkFirstPage_Click(object sender, System.EventArgs e)
		{
			OnPageChanged(new PageChangeEventArgs(1));
		}

		protected void lnkPreviousPage_Click(object sender, System.EventArgs e)
		{
			OnPageChanged(new PageChangeEventArgs((CurrentPage > 1) ? (CurrentPage - 1) : CurrentPage ));
		}

		protected void lnkNextPage_Click(object sender, System.EventArgs e)
		{
			OnPageChanged(new PageChangeEventArgs((CurrentPage < PageCount) ? (CurrentPage + 1) : PageCount ));
		}

		protected void lnkLastPage_Click(object sender, System.EventArgs e)
		{
			OnPageChanged(new PageChangeEventArgs(PageCount));
		}

		private void rptPageList_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "ChangePage")
			{
				//LinkButton _objSelectedPage = e.Item.FindControl("lnkPageItem") as LinkButton;
				//if (_objSelectedPage != null)
				{
					OnPageChanged(new PageChangeEventArgs(Convert.ToInt32(e.CommandArgument)));
				}
			}
		}

		private void rptPageList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				DataRowView _objCurrentItem = e.Item.DataItem as DataRowView;
				if (_objCurrentItem != null)
				{
					if (Convert.ToInt32(_objCurrentItem.Row["PageNum"]) == CurrentPage)
					{
						LinkButton _objSelectedLinkButton = (e.Item.FindControl("lnkPageItem") as LinkButton);
						if (_objSelectedLinkButton != null)
						{
							_objSelectedLinkButton.Enabled = false;
						}
					}
				}
			}
		}
		#endregion
	}

	/// <summary>
	/// Lớp lưu trữ dữ liệu của sự kiện PageChange khi người sử dụng chọn hiển thị một trang nào đó.
	/// </summary>
	public class PageChangeEventArgs : EventArgs
	{
		private readonly int intNewPage;

		/// <summary>
		/// PageChange Constructor
		/// </summary>
		/// <param name="_intNewPage">Chỉ số mới được chọn</param>
		public PageChangeEventArgs(int _intNewPage)
		{
			intNewPage = _intNewPage;
		}

		/// <summary>
		/// Chỉ số của trang được chọn
		/// </summary>
		public int PageIndex
		{
			get{return intNewPage;}
		}
	}
}
