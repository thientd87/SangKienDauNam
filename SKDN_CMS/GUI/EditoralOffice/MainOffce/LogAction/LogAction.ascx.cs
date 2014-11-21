using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Portal.GUI.EditoralOffice.MainOffce.LogAction
{
    public partial class LogAction : Portal.API.Module
    {   
        #region private method
        /*
        private void CreateDataSource()
        {
            _sort = (String)ViewState["sort"];
            LogHelper logHelper = new LogHelper();
            DataView dv = logHelper.SelectAll(_sort, startIndex, dgData.PageSize,ref totalRows);

            dgData.VirtualItemCount = totalRows;
            dgData.DataSource = dv;
            dgData.DataBind();
        }
        */

        private String CreateStringWhere()
        {
            String strWhere = String.Empty;
            bool IsFirst = true;

            if (!txtUserName.Text.Trim().Equals(String.Empty))
            {
                if (IsFirst)
                {
                    strWhere += "WHERE UserName LIKE N'%" + txtUserName.Text.Trim() + "%'";
                    IsFirst = false;
                }
                else
                {
                    strWhere += " AND UserName LIKE N'%" + txtUserName.Text.Trim() + "%'";
                }
            }

            if (!txtHanhDong.Text.Trim().Equals(String.Empty))
            {
                if (IsFirst)
                {
                    strWhere += " WHERE Action LIKE N'%" + txtHanhDong.Text.Trim() + "%' ";
                    IsFirst = false;
                }
                else
                {
                    strWhere += " AND Action LIKE N'%" + txtHanhDong.Text.Trim() + "%' ";
                }
            }

            if (!txtStartDate.Text.Trim().Equals(String.Empty) && !txtEndDate.Text.Trim().Equals(String.Empty) && txtStartDate.Text.Trim().Equals(txtEndDate.Text.Trim()))
            {
                String StartDate = txtStartDate.Text;
                StartDate = StartDate.Substring(3, 2) + "/" + StartDate.Substring(0, 2) + "/" + StartDate.Substring(6, 4);
                if (IsFirst)
                {
                    strWhere += "WHERE Convert(nvarchar,CreatedDate,101) = '" + StartDate + "'";
                    IsFirst = false;
                }
                else
                {
                    strWhere += " AND Convert(nvarchar,CreatedDate,101) = '" + StartDate + "'";
                }
                return strWhere;
            }

            if (!txtStartDate.Text.Trim().Equals(String.Empty))
            {
                String StartDate = txtStartDate.Text;
                StartDate = StartDate.Substring(3, 2) + "/" + StartDate.Substring(0, 2) + "/" + StartDate.Substring(6, 4);
                if (IsFirst)
                {
                    strWhere += "WHERE CreatedDate >= '" + StartDate + "'";
                    IsFirst = false;
                }
                else
                {
                    strWhere += " AND CreatedDate >= '" + StartDate + "'";
                }
            }
            if (!txtEndDate.Text.Trim().Equals(String.Empty))
            {
                String EndDate = txtEndDate.Text;
                EndDate = EndDate.Substring(3, 2) + "/" + EndDate.Substring(0, 2) + "/" + EndDate.Substring(6, 4);
                if (IsFirst)
                {
                    strWhere += "WHERE CreatedDate <= '" + EndDate + "'";
                    IsFirst = false;
                }
                else
                {
                    strWhere += " AND CreatedDate <= '" + EndDate + "'";
                }
            }
            return strWhere;
        }
        #endregion
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
            }
        }

        #endregion
        #region Phan Trang
        protected void drPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvDataAction.PageIndex = Convert.ToInt32(drPage.SelectedValue);
        }

        protected void gvDataAction_PageIndexChanged(object sender, EventArgs e)
        {
            gvDataAction.SelectedIndex = -1;
        }
        #endregion
        #region Tim Kiem
        protected void txtFind_Click(object sender, EventArgs e)
        {
            String strWhere = CreateStringWhere();
            odsLogAction.SelectParameters["Sort"].DefaultValue = strWhere;
            gvDataAction.PageIndex = 0;
            gvDataAction.DataBind();
            odsPage.SelectParameters["numPage"].DefaultValue = gvDataAction.PageCount.ToString();            
        }
        #endregion
        #region RowDataBound
        protected void gvDataAction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label label = (Label)e.Row.FindControl("lblType");
                int type = Convert.ToInt32(label.Text);
                Log log = new Log();
                label.Text = log.GetLogType(type);
            }
        }
        #endregion

        protected void odsPage_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }
        #region Old Code
        /*
        #region Phan Trang
        protected void dgData_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgData.CurrentPageIndex = e.NewPageIndex;
            startIndex = e.NewPageIndex;
            CreateDataSource();
        }
        #endregion
        #region Xap xep
        protected void dgData_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            ViewState["sort"] = e.SortExpression;
            _sort = (String)ViewState["sort"];
            startIndex = dgData.CurrentPageIndex;
            CreateDataSource();
        }
        #endregion
        #region ItemCreated When Data Binding
        protected void dgData_ItemCreated(object sender, DataGridItemEventArgs e)
        {           
            ListItemType itemType = e.Item.ItemType;
            switch (itemType)
            {
                case ListItemType.Pager:
                    TableCell pager = (TableCell)e.Item.Controls[0];
                    int countControl = pager.Controls.Count;
                    for (int i = 0; i < countControl; i += 2)
                    {
                        Object o = pager.Controls[i];
                        if (o is LinkButton)
                        {
                            LinkButton lb = (LinkButton)o;
                            lb.ToolTip = "Chuyển đến trang " + lb.Text;
                            lb.Text = "[" + lb.Text + "]";
                            lb.CssClass = "dgPagerLinks";
                        }
                        else
                        {
                            Label label = (Label)o;
                            label.Text = "Trang: " + label.Text;
                            label.CssClass = "dgPagerText";
                        }
                    }
                    break;
                case ListItemType.Header:
                    int countColumn = dgData.Columns.Count;
                    for (int i = 0; i < countColumn; i++)
                    {
                        TableCell header = (TableCell)e.Item.Cells[i];
                        if (dgData.Columns[i].SortExpression != String.Empty)
                        {
                            header.ToolTip = "Sắp xếp theo: " + dgData.Columns[i].HeaderText;
                            try
                            {
                                Object o = header.Controls[0];
                                if (o is LinkButton)
                                {
                                    LinkButton lb = (LinkButton)o;
                                    lb.CssClass = "grdLinkHeader";
                                }
                            }
                            catch { }
                        }
                    }
                    break;
                case ListItemType.Footer:
                    TableCell footer = e.Item.Cells[0];
                    ImageButton DeleteButton = (ImageButton)footer.Controls[1];
                    DeleteButton.Attributes.Add("onclick",
                        "return confirm('Are you sure you want to delete the selected row(s)?');");
                    break;
            }
        }
        #endregion

        #region Delete LogAction
        protected void dgData_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            int i = 0;
            CheckBox cb;
            Int64 log_id ;
            LogHelper logHelper = new LogHelper();
            foreach (DataGridItem dgi in dgData.Items)
            {
                cb = (CheckBox)dgi.Cells[0].Controls[1];
                if (cb.Checked)
                {
                    log_id = Convert.ToInt64(dgData.DataKeys[i]);
                    logHelper.Delete(log_id);
                }
                i++;
            }
            dgData.CurrentPageIndex = 0;
            startIndex = dgData.CurrentPageIndex;            
            CreateDataSource();
        }
        #endregion
        */
        #endregion
    }
}