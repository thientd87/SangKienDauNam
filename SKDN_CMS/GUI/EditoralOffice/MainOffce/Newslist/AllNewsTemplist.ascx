<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllNewsTemplist.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.Newslist.AllNewsTemplist" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI" TagPrefix="asp" %>

<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
<link href="/styles/Coress.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="/Styles/Newsedit.css" />
<link href="/styles/pcal.css" rel="stylesheet" type="text/css" />
<link href="/styles/common.css" rel="stylesheet" type="text/css" />
<link href="/styles/Core.css" rel="stylesheet" type="text/css" />
<link href="/styles/autopro.css" rel="stylesheet" type="text/css" />
<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />

<script language="JavaScript" type="text/javascript" src="/scripts/init.js"></script>
<script language="JavaScript" type="text/javascript" src="/scripts/autopro.js"></script>
<script language="JavaScript" type="text/javascript" src="/scripts/shortcut.js"></script>
<script type="text/javascript" src="/scripts/ajax.js"></script>
<script language="javascript" src="/scripts/Grid.js"></script>

<script language="javascript">
    ctx = new ContextInfo();
    ctx.HttpPath = "";
    ctx.HttpRoot = "";
    ctx.imagesPath = "/Images/";
    ctx1 = ctx;
</script>





<table width="100%">
	<tr>
		<td class="Edit_Head_Cell">
			Danh sách tất cả các bài lưu tạm</td>
	</tr>
</table>
<div id="AlertDiv" class="AlertStyle">
</div>
<asp:UpdatePanel ID="panel" runat="server">
	<ContentTemplate>
	    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="ms-formbody">
	        <tr>
	            <td>
	                <table cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td>
								<table cellpadding="0" cellspacing="0" width="100%">
									<tr>
										<td style="padding-left: 10px" class="ms-formbody" width="120px">
											Chọn chuyên mục
										</td>
										<td>
											<asp:DropDownList ID="ddlChuyenmuc" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChuyenmuc_SelectedIndexChanged" >
											</asp:DropDownList>
										</td>
									</tr>
								</table>
							</td>
							<td>
								<table id="tblFilter" runat="server" cellpadding="0" cellspacing="0" width="100%">
									<tr>
										<td style="padding-left: 10px" class="ms-formbody" align="right">
											<fieldset style="width: 320px">
												<legend>Xem tin theo ngày</legend>
												<table cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td class="ms-formbody">
															Từ&nbsp;
														</td>
														<td>
															<asp:TextBox CssClass="ms-input" MaxLength="10" ID="txtCalendar" Width="75px" runat="server" />
														</td>
														<td>
															<a onclick="if(self.gfPop)gfPop.fPopCalendar(document.RenderTable.<% = ClientID %>_txtCalendar);return false;"
																href="javascript:void(0)">
																<img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
																	align="absMiddle" border="0">
															</a>
														</td>
														<td class="ms-formbody">
															&nbsp;đến&nbsp;
														</td>
														<td>
															<asp:TextBox CssClass="ms-input" MaxLength="10" ID="txtToDate" Width="75px" runat="server" />
														</td>
														<td>
															<a onclick="if(self.gfPop)gfPop.fPopCalendar(document.RenderTable.<% = ClientID %>_txtToDate);return false;"
																href="javascript:void(0)">
																<img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
																	align="absMiddle" border="0">
															</a>
														</td>
														<td>
															&nbsp;<asp:ImageButton ID="btnFilter" runat="server" OnClientClick="return Filter();"
																Width="22px" Height="21px" ImageUrl="/images/Icons/search.gif" OnClick="btnFilter_Click" />
														</td>
													</tr>
												</table>
											</fieldset>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td style="height: 18px">
								&nbsp;</td>
						</tr>
						<tr>
							<td colspan="2">
								<asp:GridView Width="100%" ID="grdListNews" runat="server" HeaderStyle-CssClass="grdHeader"
									BorderWidth="1px" BorderColor="#DFDFDF" RowStyle-CssClass="grdItem" EmptyDataText="<span style='color:Red'><b>Không có bài !</b></span>"
									AlternatingRowStyle-CssClass="grdAlterItem" AutoGenerateColumns="False" AllowPaging="True"
									AllowSorting="true" DataSourceID="objListNewsSource" PageSize="40" OnSelectedIndexChanging="grdListNews_SelectedIndexChanging" OnRowDataBound="grdListNews_RowDataBound">
									<Columns>
										<asp:TemplateField HeaderText="Tiêu đề bài viết" SortExpression="News_Title">
											<ItemStyle CssClass="ms-vb-title" Height="26px"  />
											<ItemTemplate>
												<table class="ms-unselectedtitle" width="100%" border="0" cellspacing="0" cellpadding="1"
													id="tblParent" runat="server" itemid='<%#Eval("News_Id")%>' cpmode='<%# Request.QueryString["cpmode"].ToString() %>'
													onmouseover=" OnItem(this)" ctxname="ctx" index="<%#Container.DisplayIndex +2 %>">
													<tr>
														<td class="ms-vb" width="100%">
															<a onclick="javascript:openpreview('/preview/family.aspx?news=<%#Eval("News_Id")%>',900,700);return false;"
																href="#">
																<%#Eval("News_Title")%>
															</a><em title="Số từ trong bài">(<%#Eval("WordCount")%> từ)</em> 
														</td>
														<td>
															<img src="/Images/blank.gif" width="13">
														</td>
													</tr>
												</table>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Người tạo">
											<ItemStyle CssClass="ms-vb-title" />
											<ItemTemplate>
												<%#Eval("News_Author")%>
											</ItemTemplate>
											<ItemStyle Width="70px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Ngày tạo" HeaderStyle-CssClass="desc">
											<ItemStyle CssClass="ms-vb-title" />
											<ItemTemplate>
												<nobr>
												    <%# Convert.ToDateTime(Eval("News_CreateDate")).ToString("dd/MM/yyyy hh:mm")%>
												</nobr>
											</ItemTemplate>
											<ItemStyle Width="70px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="<nobr>Tiêu điểm</nobr>" SortExpression="News_isFocus">
											<ItemStyle CssClass="ms-vb-title" />
											<ItemTemplate>
												<center>
													<asp:CheckBox ID="chkIsFocus" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"News_isFocus")%>'
														CausesValidation="False" />
												</center>
											</ItemTemplate>
											<HeaderStyle Width="30px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Loại tin" SortExpression="News_mode">
											<ItemStyle CssClass="ms-vb-title" />
											<ItemTemplate>
												<asp:DropDownList ID="cboIsHot" runat="server" CssClass="ms-input" CausesValidation="False">
													<asp:ListItem Value="0" Text="Thông Thường"></asp:ListItem>
													<asp:ListItem Value="1" Text="Nổi bật mục"></asp:ListItem>
													<asp:ListItem Value="2" Text="Nổi bật trang chủ"></asp:ListItem>
												</asp:DropDownList>
											</ItemTemplate>
											<ItemStyle Width="50px" />
										</asp:TemplateField>
									</Columns>
									<RowStyle CssClass="grdItem" />
									<HeaderStyle CssClass="grdHeader" />
									<AlternatingRowStyle CssClass="grdAlterItem" />
									<PagerStyle CssClass="paging" />
								</asp:GridView>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td style="padding-top: 10px; height: 106px;">
					<table id="tblSearch" runat="server" cellpadding="2" cellspacing="2" border="0" style="width: 100%;
						height: 60px; border: 1px solid #b8c1ca; background-color: #E5E5E5; clear: both">
						<tr>
							<td width="100" style="padding-left: 10px" class="ms-formbody">
								Từ khóa</td>
							<td>
								<asp:TextBox ID="txtKeyword" CssClass="ms-long" runat="server"></asp:TextBox>
								<asp:Button ID="btnSearch" CssClass="ms-input" runat="server" Text="Tìm ki&#7871;m" OnClick="btnSearch_Click"/>
							</td>
						</tr>
						<tr>
						    <td width="100" style="padding-left: 10px" class="ms-formbody">
						        Người tạo
						    </td>
						    <td>
						        <asp:TextBox ID="txtNguoiTao" CssClass="ms-long" runat="server"></asp:TextBox>
						    </td>
						</tr>
						<tr>
							<td style="padding-left: 10px" class="ms-formbody">
								Thuộc nhóm tin</td>
							<td>
								<asp:DropDownList ID="cboCategory" runat="server">
								</asp:DropDownList>
							</td>
						</tr>
					</table>
				</td>
			</tr>
	    </table>
	</ContentTemplate>
</asp:UpdatePanel>
<asp:ObjectDataSource ID="objListNewsSource" runat="server" SelectMethod="News_GetAllNewsTemplist"
	SelectCountMethod="News_GetAllNewsTemplistCount"
	TypeName="Portal.BO.Editoral.Newslist.NewslistHelper" EnablePaging="true" MaximumRowsParameterName="PageSize" StartRowIndexParameterName="StartRow">
	<SelectParameters>
		<asp:Parameter Name="strWhere" DefaultValue="" Type="String" />
	</SelectParameters>
</asp:ObjectDataSource>

<script language="javascript">

    function AddListMenuItems(m, ctx)
    {   
	    VC_AddMenuItem(m,ctx,"Xem trước","newitem.gif","openpreview('/preview/family.aspx?news=" + currentItemID + "',900,700)");
    }

     function GetControlByName(id)
     {
        return document.getElementById("<% = ClientID %>_" + id);
     } 
        
    function OnLoad()
    {
        shortcut.add("Enter",function() {
            var btnSearch = GetControlByName("btnSearch");
            btnSearch.click();
        });
    }
    
    if(document.all)
        window.attachEvent("onload",OnLoad);
    else
        window.addEventListener("load",OnLoad, false);

</script>
<iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
	src="/Scripts/DatePicker/ipopeng.htm" scrolling="no" frameborder="0" style="visibility: visible;
	z-index: 999; position: absolute; top: -500px; left: -500px;"></iframe>