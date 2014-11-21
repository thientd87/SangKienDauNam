namespace DFISYS.GUI.Share
{
	using System;
	using DFISYS.Core.DAL;
	using System.Data;
	/// <summary>
	///		Summary description for CatList.
	/// </summary>
	public partial class CatList : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(! IsPostBack)
			{
				DataTable tbCat=buildCat();
				cboCats.DataSource=tbCat;
				cboCats.DataMember=tbCat.TableName;
				cboCats.DataValueField=tbCat.Columns[0].ColumnName;
				cboCats.DataTextField=tbCat.Columns[1].ColumnName;
				cboCats.DataBind();
			}
		}
		private  DataTable buildCat()
		{
			DataTable objCatTable;
			using(MainDB objDb=new MainDB())
			{
				objCatTable=objDb.CategoryCollection.GetAllAsDataTable();
			}
			DataSet objds = new DataSet();
			objds.Tables.Add(objCatTable);
			//tạo ra table mới chứa dữ liệu theo định dạng
			DataTable dtNewCat=new DataTable(objCatTable.TableName);
			DataColumn dcCatID=new DataColumn("Cat_ID");
			DataColumn dcCatName=new DataColumn("Cat_Name");
			dtNewCat.Columns.Add(dcCatID);
			dtNewCat.Columns.Add(dcCatName);
			//thêm giá trị mặc định
			object[] objDefautRow=new object[2];
			objDefautRow[0]=-1;
			objDefautRow[1]="-- Tất cả các danh mục --";
			dtNewCat.Rows.Add(objDefautRow);

			objds.Relations.Add("NodeRelation", objds.Tables[0].Columns["Cat_ID"], objds.Tables[0].Columns["Cat_ParentID"]);
			foreach(DataRow dbRow in objds.Tables[0].Rows)
			{
				if(dbRow.IsNull("Cat_ParentID"))
				{
					object[] objCurrRow=new object[2];
					objCurrRow[0]=dbRow.ItemArray[0];
					objCurrRow[1]=dbRow.ItemArray[1];
					dtNewCat.Rows.Add(objCurrRow);
					addChildRow(dbRow,ref dtNewCat);
				}
			}
			
			return  dtNewCat;
		}
		private void addChildRow(DataRow _parentRow, ref DataTable _newTable)
		{
			foreach (DataRow childRow in _parentRow.GetChildRows("NodeRelation"))
			{
				object[] objCurrRow=new object[2];
				objCurrRow[0]=childRow.ItemArray[0];
				objCurrRow[1]="--"+childRow.ItemArray[1].ToString();
				_newTable.Rows.Add(objCurrRow);
				addChildRow(childRow,ref _newTable);
			}
		}
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

		}
		#endregion

		protected void cboCats_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}
		#region controlproperties
		public string CurrentSelectVal
		{
			set
			{
				cboCats.SelectedIndex=cboCats.Items.IndexOf(cboCats.Items.FindByValue(value));
			}
			get
			{
				return cboCats.SelectedValue;
			}
		}
		#endregion
	}
}
