<%@ Control Language="C#" AutoEventWireup="true" Codebehind="MediaObjectList.ascx.cs"
	Inherits="DFISYS.GUI.EditoralOffice.MainOffce.NewsMedia.MediaObjectList" %>
<%@ Register Src="~/GUI/EditoralOffice/MainOffce/Media/UserControl/cltPopupMedia.ascx"
	TagName="cltPopupMedia" TagPrefix="uc1" %>
<%@ Register Src="~/GUI/EditoralOffice/MainOffce/Media/UserControl/ctlPopupView.ascx"
	TagName="ctlPopupView" TagPrefix="uc2" %>

<script language="javascript" src="/scripts/Grid.js"></script>

<script type="text/javascript" src="/Scripts/library.js"></script>

<link href="/styles.css" rel="stylesheet" type="text/css" />
<link href="/styles/core.css" rel="stylesheet" type="text/css" />
<link href="/styles/portal.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="/Styles/Newsedit.css" />

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/core.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/events.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/css.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/coordinates.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/drag.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/swfobject.js"></script>

<script language="javascript">


    function GetControlByName(id)
    {
        var ctr = document.getElementById("hidPrefix");        
        return document.getElementById(ctr.value + id);
    }
    window.onload = function() 
    {
    
        var coordinates = ToolMan.coordinates();
        var drag = ToolMan.drag();
        
        var loginForm = document.getElementById("loginForm");
        if (loginForm)
        drag.createSimpleGroup(loginForm);
        
        var divMedia = document.getElementById("divMedia");
        if (divMedia)
        drag.createSimpleGroup(divMedia);
        
        var divFlash = document.getElementById("divFlash");
        if (divFlash)
        drag.createSimpleGroup(divFlash);
        
        
    }

    function Preview(strObjectUrl,ObjectType)
    {
        if(ObjectType == '1')
        {
            ImgResize('/Images/Uploaded/Share/Media/Picture/'+strObjectUrl,500);
        }
        else
        {
            playMedia('/Images/Uploaded/Share/Media/Video/'+strObjectUrl);
        }
            
    }
    
    function ImgResize(imgPath,thumbSize)
    {
        document.getElementById("loginForm").style.top = "100px";
        document.getElementById("loginForm").style.left = "400px";
        document.getElementById("loginForm").style.visibility = "visible";
        document.getElementById("loginForm").style.display="inline";
        document.getElementById("loginForm").style.position ="absolute";
        document.getElementById("fPic").src = imgPath;
        
         var newImg=new Image();
        newImg.src=imgPath;
        var _width=newImg.width;
        var _height=newImg.height;
        
        if(_width>thumbSize)
        {
            _height=Math.round(_height*thumbSize/_width);
            _width=thumbSize;
        }
        else if(_height>thumbSize)
        {
            _width=Math.round(_width*thumbSize/_height);
            _height=thumbSize;
        }
        
        if(_width == 0 || _height == 0)
        {
             newImg.src=imgPath;
            _width=newImg.width;
            _height=newImg.height;
        }
        
        var currImg=document.getElementById("fPic");
        currImg.src=newImg.src;
        currImg.width=_width;
        currImg.height=_height;
     }
     
     function CloseFormImage()
     {
        document.getElementById("fPic").src = "";
        document.getElementById("loginForm").style.visibility = "hidden";
     }
     
     function playMedia(mediaPath)
     {
       
        document.getElementById("divMedia").style.top = "100px";
        document.getElementById("divMedia").style.left = "400px";
        document.getElementById("divMedia").style.visibility = "visible";
        document.getElementById("divMedia").style.display="inline";
        document.getElementById("divMedia").style.position ="absolute";
        mediaPlayer = document.getElementById("mediaPlayer");
        mediaPlayer.fileName = mediaPath;
        mediaPlayer.play();
    }
    
    function CloseFormMedia(){
        mediaPlayer.stop();
        document.getElementById("divMedia").style.visibility = "hidden";
    }
    
    function ValidateSearch()
    {
        
    }
    function ValidateUpload()
    {
        var fileName = "txtFileName";        
        var isValid = false;
        var contrl ;
        for (var i = 5 ; i > 0 ; i--)
        {
            contrl = GetControlByName(fileName + i.toString());            
            if (contrl && contrl.value.length > 0) isValid = true;
        }      
        if (!isValid) 
        {
            alert("Bạn chưa chọn File để Upload");
            GetControlByName("txtFileName1").focus();
            return false;
        }
        return true;        
    }
    
    
</script>

<table cellpadding="0" cellspacing="0" border="0" width="100%" class="ms-formbody">
	<tr>
		<td colspan="3" class="Edit_Head_Cell">
			Danh sách các media liên quan
		</td>
	</tr>
	<tr>
		<td>
			<table cellpadding="0" cellspacing="5" width="100%">
				<tr>
					<td colspan="2">
						<asp:GridView Width="100%" ID="grdMedia" runat="server" BorderWidth="1px" BorderColor="#DFDFDF"
							HeaderStyle-CssClass="grdHeader" RowStyle-CssClass="grdItem" ShowFooter="true"
							EmptyDataText="Hien chua co du lieu" AlternatingRowStyle-CssClass="grdAlterItem"
							AutoGenerateColumns="false" AllowPaging="true" DataSourceID="objNewsMediaSource"
							PageSize="12" OnRowCommand="grdMedia_RowCommand" OnRowCancelingEdit="grdMedia_RowCancelingEdit"
							OnRowDataBound="grdMedia_RowDataBound" OnRowDeleting="grdMedia_RowDeleting" OnRowEditing="grdMedia_RowEditing"
							OnRowUpdating="grdMedia_RowUpdating">
							<Columns>
								<asp:TemplateField HeaderStyle-Width="2%">
									<HeaderTemplate>
										<input type="checkbox" id="chkAll" onclick="CheckAll();" />
									</HeaderTemplate>
									<ItemTemplate>
										<input type="checkbox" name="chkSelect" id="chkSelect<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>"
											value="<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>" onclick="NewsChecked('<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>')"
											<%#IsCheck(Convert.ToString(DataBinder.Eval(Container.DataItem,"object_id")))%> />
										<input type="hidden" name="hidNewsTitle<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>"
											value="<%#DataBinder.Eval(Container.DataItem,"Object_Url")%>" id="hidTitle<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Tên file" HeaderStyle-Width="40%">
									<ItemTemplate>
										<a href="javascript:preview('<%# Eval("Object_Url") %>', <%# Eval("Object_Type") %>)">
											<%# Eval("Object_Url") %>
										</a>
										<asp:HiddenField ID="hdfObject_Url" Value='<%# Eval("Object_Url") %>' runat="server" />
									</ItemTemplate>
									<%--<EditItemTemplate>
										<asp:FileUpload ID="flEObject" CssClass="ms-input" runat="server" Width="100%" />
									</EditItemTemplate>--%>
								</asp:TemplateField>
								<asp:TemplateField HeaderStyle-Width="15%" HeaderText="Kiểu" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:Literal ID="ltrType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Object_Type")%>' />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList ID="cboType" runat="server" Width="100%">
											<asp:ListItem Value="1" Text="Hình ảnh"></asp:ListItem>
											<asp:ListItem Value="2" Text="Video"></asp:ListItem>
										</asp:DropDownList>
									</EditItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Chú thích cho file" HeaderStyle-Width="28%" ItemStyle-HorizontalAlign="Center">
									<ItemStyle HorizontalAlign="left" Wrap="true" />
									<ItemTemplate>
										<asp:Literal ID="ltrNote" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Object_Note")%>' />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="txtENote" CssClass="ms-input" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Object_Note")%>'
											Width="98%" />
									</EditItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Tuỳ chọn" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
									<ItemTemplate>
										<asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Images/icons/edit.gif" AlternateText="Sửa nội dung"
											CausesValidation="False" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>'>
										</asp:ImageButton>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/Images/icons/save.gif" AlternateText="Lưu lại"
											ToolTip="Lưu lại" CommandName="Update" CausesValidation="False" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>'>
										</asp:ImageButton>
										&nbsp;
										<asp:ImageButton ID="imgCancel" BorderWidth="0" runat="server" ImageUrl="~/Images/icons/stop.gif"
											AlternateText="Tạm dừng thay đổi" CommandName="Cancel" CausesValidation="False">
										</asp:ImageButton>
										&nbsp;
										<asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Images/icons/cancel.gif"
											AlternateText="Xóa nội dung này" CommandName="Delete" CausesValidation="False"></asp:ImageButton>
									</EditItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Xóa" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:ImageButton ImageUrl="/images/icons/delete.gif" OnClientClick="return confirm('Bạn có muốn xóa không ?');"
											ID="ibnDelete" runat="server" CommandName="DeleteMedia" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>' />
									</ItemTemplate>
								</asp:TemplateField>
								<%--<asp:TemplateField HeaderText="Xem" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<a href="javascript:Preview('<%#DataBinder.Eval(Container.DataItem,"Object_Url")%>','<%#DataBinder.Eval(Container.DataItem,"Object_Type")%>')">
											<img src="/images/icons/preview.gif" border="0" /></a>
									</ItemTemplate>
								</asp:TemplateField>--%>
							</Columns>
							<RowStyle CssClass="grdItem" />
							<HeaderStyle CssClass="grdHeader" />
							<AlternatingRowStyle CssClass="grdAlterItem" />
							<PagerSettings Visible="false" />
						</asp:GridView>
					</td>
				</tr>
				<tr>
					<td valign="top" align="left" colspan="2" style="padding-top: 15px">
						<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr>
								<td width="20%">
									Xem trang&nbsp;<asp:DropDownList ID="cboPage" runat="Server" DataTextField="Text"
										DataValueField="Value" AutoPostBack="true" DataSourceID="objdspage" OnSelectedIndexChanged="cboPage_SelectedIndexChanged">
									</asp:DropDownList>
								</td>
								<td align="right" style="height: 19px" class="Menuleft_Item" style="display: none">
									<a id="a"></a><a onclick="GoselectAll();" href="#a">Chọn tất cả</a>&nbsp;| <a onclick="GoUnselectAll();"
										href="#a">Bỏ chọn</a><asp:Literal ID="Literal0" Text="&nbsp;|" runat="server"></asp:Literal>
									<asp:LinkButton ID="lnkAddMedia" OnClientClick="return AddRelateNews();" runat="server"
										Visible="false" CssClass="normalLnk" OnClick="lnkAddMedia_Click">Thêm vào bài</asp:LinkButton>
									<asp:Literal ID="ltrAddMedia" Text="&nbsp;|" runat="server" Visible="false"></asp:Literal>
									<asp:LinkButton ID="lnkRealDel" OnClientClick="return confirm('Bạn có muốn xóa những file đã chọn hay không?')"
										runat="server" CssClass="normalLnk" OnClick="lnkRealDel_Click">Xóa các file</asp:LinkButton>
									<asp:Literal ID="ltrRealDel" Text="&nbsp;|" runat="server"></asp:Literal>
									<asp:LinkButton ID="lnkSelectedMedia" runat="server" CssClass="normalLnk" OnClick="lnkSelectedMedia_Click">Hiển thị media đã chọn</asp:LinkButton>
									<asp:Literal ID="ltrSelectedMedia" Text="&nbsp;|" runat="server"></asp:Literal>
									<asp:LinkButton ID="lnkShowAllMedia" runat="server" CssClass="normalLnk" OnClick="lnkShowAllMedia_Click">Hiển thị tất cả media</asp:LinkButton>
									<asp:Literal ID="ltrShowAllMedia" Text="&nbsp;|" runat="server"></asp:Literal>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td height="10">
		</td>
	</tr>
	<tr>
		<td>
			<hr />
		</td>
	</tr>
	<tr>
		<td colspan="3" class="Edit_Head_Cell">
			Chọn các media liên quan
		</td>
	</tr>
	<tr>
		<td height="10">
		</td>
	</tr>
	<tr>
		<td width="100%" align="center">
			<table cellpadding="2" cellspacing="0" align="center" style="width: 100%; border: 1px solid #79A4D2;">
				<tr>
					<td class="grdHeader" style="width: 27px">
						STT</td>
					<td class="grdHeader">
						Ảnh
					</td>
					<td class="grdHeader">
						Kiểu
					</td>
					<td class="grdHeader" style="width: 297px">
						Chú thích cho file
					</td>
				</tr>
				<tr>
					<td align="center" style="width: 27px">
						1</td>
					<td width="200px">
						<asp:TextBox ID="txtFileName1" CssClass="ms-input" runat="server"></asp:TextBox>
					</td>
					<td width="100px">
						<asp:DropDownList ID="cboType1" CssClass="ms-input" runat="server" Width="100">
							<asp:ListItem Value="1" Text="H&#236;nh ảnh"></asp:ListItem>
							<asp:ListItem Value="2" Text="Video"></asp:ListItem>
						</asp:DropDownList>
					</td>
					<td align="center">
						<asp:TextBox ID="txtTitle1" CssClass="ms-input" runat="server" Width="200px"></asp:TextBox>
					</td>
				</tr>
				<tr class="grdAlterItem">
					<td align="center" style="width: 27px">
						2</td>
					<td width="200px">
						<asp:TextBox ID="txtFileName2" CssClass="ms-input" runat="server"></asp:TextBox>
					</td>
					<td width="100px">
						<asp:DropDownList ID="cboType2" CssClass="ms-input" runat="server" Width="100">
							<asp:ListItem Value="1" Text="H&#236;nh ảnh"></asp:ListItem>
							<asp:ListItem Value="2" Text="Video"></asp:ListItem>
						</asp:DropDownList>
					</td>
					<td align="center">
						<asp:TextBox ID="txtTitle2" CssClass="ms-input" runat="server" Width="200px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td align="center" style="width: 27px">
						3</td>
					<td width="200px">
						<asp:TextBox ID="txtFileName3" CssClass="ms-input" runat="server"></asp:TextBox>
					</td>
					<td width="100px">
						<asp:DropDownList ID="cboType3" CssClass="ms-input" runat="server" Width="100">
							<asp:ListItem Value="1" Text="H&#236;nh ảnh"></asp:ListItem>
							<asp:ListItem Value="2" Text="Video"></asp:ListItem>
						</asp:DropDownList>
					</td>
					<td align="center">
						<asp:TextBox ID="txtTitle3" CssClass="ms-input" runat="server" Width="200px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td align="center" style="width: 27px; background-color: #f8f8f8">
						4</td>
					<td width="200px" bgcolor="#f8f8f8">
						<asp:TextBox ID="txtFileName4" CssClass="ms-input" runat="server"></asp:TextBox>
					</td>
					<td width="100px">
						<asp:DropDownList ID="cboType4" runat="server" Width="100">
							<asp:ListItem Value="1" Text="H&#236;nh ảnh"></asp:ListItem>
							<asp:ListItem Value="2" Text="Video"></asp:ListItem>
						</asp:DropDownList>
					</td>
					<td align="center">
						<asp:TextBox ID="txtTitle4" CssClass="ms-input" runat="server" Width="200px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td align="center" style="width: 27px">
						5</td>
					<td width="200px">
						<asp:TextBox ID="txtFileName5" CssClass="ms-input" runat="server"></asp:TextBox>
					</td>
					<td width="100px">
						<asp:DropDownList ID="cboType5" runat="server" Width="100">
							<asp:ListItem Value="1" Text="H&#236;nh ảnh"></asp:ListItem>
							<asp:ListItem Value="2" Text="Video"></asp:ListItem>
						</asp:DropDownList>
					</td>
					<td align="center">
						<asp:TextBox ID="txtTitle5" CssClass="ms-input" runat="server" Width="200px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
					</td>
					<td>
						<input type="button" onclick="return chonfiletuthuvien()" value="Chọn các file từ thư viện" />
					</td>
					<td>
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td height="5" style="width: 27px">
					</td>
					<td height="5">
					</td>
				</tr>
				<tr>
					<td align="left" style="padding-left: 10px; width: 27px">
					</td>
					<td align="left" style="padding-left: 20px; width: 27px">
						<asp:Button ID="btnAddMediaObject" CssClass="ms-input" runat="server" Text="Cập nhật"
							OnClientClick="return ValidateUpload();" OnClick="btnAddMediaObject_Click" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<asp:ObjectDataSource ID="objdspage" runat="server" SelectMethod="getPage" TypeName="DFISYS.BO.Editoral.NewsMedia.NewsMediaHelper"
	OldValuesParameterFormatString="original_{0}">
	<SelectParameters>
		<asp:ControlParameter DefaultValue="0" ControlID="grdMedia" Name="numPage" PropertyName="PageCount"
			Type="Int32" />
	</SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="objNewsMediaSource" runat="server" SelectMethod="GetMediaObjlist"
	InsertMethod="createMediaObject" UpdateMethod="UpdateMediaObj" DeleteMethod="DelMediaObj"
	SelectCountMethod="GetMediaRowsCount" TypeName="DFISYS.BO.Editoral.NewsMedia.NewsMediaHelper"
	EnablePaging="true" MaximumRowsParameterName="PageSize" StartRowIndexParameterName="StartRow">
	<SelectParameters>
		<asp:Parameter Name="strWhere" DefaultValue="" Type="string" />
	</SelectParameters>
	<UpdateParameters>
		<asp:Parameter Name="_obj_id" Type="int32" DefaultValue="0" />
		<%--<asp:Parameter Name="_obj_url" Type="string" />--%>
		<asp:Parameter Name="_obj_type" Type="int16" />
		<asp:Parameter Name="_obj_note" Type="string" />
	</UpdateParameters>
	<DeleteParameters>
		<asp:Parameter Name="_selected_id" Type="string" DefaultValue="" />
	</DeleteParameters>
	<InsertParameters>
		<asp:Parameter Name="_obj_url" Type="string" />
		<asp:Parameter Name="_obj_type" Type="Int16" />
		<asp:Parameter Name="_obj_note" Type="string" />
		<asp:Parameter Name="_obj_user" Type="string" />
		<asp:QueryStringParameter Name="strNewsId" QueryStringField="newsid" DefaultValue=""
			Type="string" />
		<asp:QueryStringParameter Name="strFilmId" QueryStringField="filmid" DefaultValue=""
			Type="string" />
	</InsertParameters>
</asp:ObjectDataSource>
<div id="loginForm" class="Loginvisible" style="visibility: hidden; display: none">
	<uc2:ctlPopupView ID="CtlPopupView2" runat="server" />
</div>
<div id="divMedia" class="Loginvisible" style="visibility: hidden; display: none">
	<uc1:cltPopupMedia ID="CltPopupMedia1" runat="server"></uc1:cltPopupMedia>
</div>
<input type="hidden" id="hidPrefix" name="hidPrefix" value="<% = ClientID %>_" />
<input type="hidden" id="hidTitle" runat="server" />
<input type="hidden" runat="server" id="txt_news_checked" />
<input type="hidden" runat="server" id="txt_news_title_checked" />
<div style="position:absolute; left:200px; top:10px; border:solid 5px #303030; display:none; background-color:#e0e0e0;" onclick="this.style.display='none';" title="Bấm vào đây để đóng cửa sổ" id="preview"></div>
<script language="javascript">
  
    var _strNews_id;
    var _strNews_Title;
    
    if(document.all)
        window.attachEvent("onload",isContinue);
    else
        window.addEventListener("load",isContinue, false)
        
    function NewsChecked(news_id)
    {
        if(document.getElementById('chkSelect'+news_id).checked)
        {
            _strNews_id = _strNews_id + "," + news_id;
            _strNews_Title = _strNews_Title + news_id + ";#" + document.getElementById("hidTitle" + news_id).value + "#;#";
        }
        else
        {
            _strNews_id = _strNews_id.replace(","+news_id,"");
            
            var title_not_check = document.getElementById("hidTitle" + news_id).value;
            _strNews_Title = _strNews_Title.replace(news_id + ";#" + title_not_check + "#;#","");
        }
        
        var txt_news_checked = GetControlByName("txt_news_checked");
        txt_news_checked.value = _strNews_id;
        
        var txt_news_title_checked = GetControlByName("txt_news_title_checked");
        txt_news_title_checked.value = _strNews_Title;
    }
    
    function Page_Load()
    {
        /*var txt_news_checked = GetControlByName("txt_news_checked");
         _strNews_id = txt_news_checked.value;
         
         var txt_news_title_checked = GetControlByName("txt_news_title_checked");
        _strNews_Title = txt_news_title_checked.value;*/
        
        
        var lists = document.getElementsByName("chkSelect");  
        var ctr = document.getElementById("<% = hidTitle.ClientID %>");
        var control;   
        var strMediaNews = "";
        
        for (var i = 0 ; i < lists.length; i++)
        {
            ctr.value += lists[i].value + ";#" + document.getElementById("hidTitle" + lists[i].value).value + "#;#";      
            strMediaNews += "," + lists[i].value;                          
        }
        
        if(strMediaNews.length > 0)
            strMediaNews = strMediaNews.substring(1);
            
        
        
        
        AssignRelatedNews('<%=strMediaID%>', strMediaNews, GetControlId("hidTitle"),true);
        window.opener.BindDataForDropdown('hdMedia','hdMediaTitle','cboMedia','hdMedia');
    }
    
    function isContinue()
    {
        var strCapNhap = '<%=isCapNhap%>';
        if(strCapNhap == 'ok')
        {
            if(!confirm('Bạn đã chọn media đi kèm thành công ! Bạn có muốn tiếp tục chọn thêm media đi kèm nữa không ?'))
            { 
                window.close();  
            }
        }
    }
    
    onload = Page_Load;
    
  
    function GetControlId(id)
    {
        return ("<% = ClientID %>_" + id);
    } 
  
    
    function AddRelateNews()
    {
        /*var isSelect = false;
        var lists = document.getElementsByName("chkSelect");  
        var ctr = document.getElementById("<% = hidTitle.ClientID %>");
        var control;             
        for (var i = 0 ; i < lists.length; i++)
        {
            if (lists[i].checked) 
            {
                isSelect = true;
                control = lists[i].parentElement.parentElement;
                ctr.value +=lists[i].value + ";#" + control.childNodes[1].innerText + "#;#";                                
            }
        }
        if (!isSelect)
        {
            alert("Bạn chưa chọn file.");
            return false;
        }
        else
        {
            return confirm('Bạn có thêm những file đã chọn hay không?');
        }*/
        
        return confirm('Bạn có thêm những file đã chọn hay không?');
    }  
    
    
    //  bacth [2:06 PM 6/5/2008]
    function chonfiletuthuvien()
    {
		openpreview('/GUI/EditoralOffice/MainOffce/FileManager/default.aspx?function=media_loadvalue&mode=multi&share=', 900, 700);
		return false;
    }
    //  bacth: bind vao textbox từ mảng đường dẫn trả về từ cửa sổ quản lý file [2:06 PM 6/5/2008]
    function media_loadvalue(arr)
    {
		var arrControls = new Array();
		arrControls.push('<%=txtFileName1.ClientID %>');
		arrControls.push('<%=txtFileName2.ClientID %>');
		arrControls.push('<%=txtFileName3.ClientID %>');
		arrControls.push('<%=txtFileName4.ClientID %>');
		arrControls.push('<%=txtFileName5.ClientID %>');
		
		var trimPattern = '/Images/Uploaded/(.*?)$';
		var re;
		var m;
		var i = 0, j = 0;
		while (i < arr.length && j < arrControls.length)
		{
			re = new RegExp(trimPattern, 'gi');
			m = re.exec(arr[i]);
			if (document.getElementById(arrControls[j]).value == '')
			{
				document.getElementById(arrControls[j]).value = m[1];
				i++;
			}
			j++;
		}
    }
    function preview(path, typeIndex)
    {
		var folder = '<%= ConfigurationManager.AppSettings["ImageUrl"] %>';
		if (typeIndex == 1)
			folder += 'images/Uploaded/Share/Media/picture/';
		else
			folder += 'images/Uploaded/Share/Media/video/';
		path = folder + path;
		
		var preview = document.getElementById('preview');
		
		var dotIndex = path.lastIndexOf('.');
		var extension = path.substr(dotIndex+1).toLowerCase();
		
		if (extension == 'gif' || extension == 'jpg' || extension == 'bmp' || extension == 'png')
		{
			preview.innerHTML = '<img src="' + path + '" />';
			preview.style.display = 'block';
		}
		else if (extension == 'swf')
		{
			var html = '<div onclick="javascript:close()"><object codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000">' +
						'<param value="sameDomain" name="allowScriptAccess"/>' +
						'<param value="' + path + '" name="movie"/>' +
						'<param value="high" name="quality"/>' +
						'<embed pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" allowscriptaccess="sameDomain" quality="high" src="' + path + '"/>' +
						'</object></div>';
			preview.innerHTML = html;
			preview.style.display = 'block';
		}
		else if (extension == 'flv')
		{
			var date_now = new Date()
			var id = date_now.getDate() + "" + date_now.getMonth() +""+ date_now.getFullYear() +""+ date_now.getHours() +""+ date_now.getMinutes() +""+ date_now.getSeconds() +""+ date_now.getMilliseconds(); 
			var sHTML = '<p id="FlashPlayer' + id + '" class="Normal" align="center">'+ 
						'<img src="/AssetManager/CustomObjects/images/FLVPlayer.jpg" />' +
						'<br>Bạn cài  <a href="http://www.macromedia.com/go/getflashplayer">Flash Player</a> để xem được Clip này.</p>';
			
			preview.innerHTML = sHTML;             
		    
			var sFlashPlayer144 = new SWFObject("/AssetManager/CustomObjects/mediaplayer.swf","playlist",'400','380',"7");
			sFlashPlayer144.addParam("allowfullscreen","true");
			sFlashPlayer144.addVariable("file",path);
			sFlashPlayer144.addVariable("displayheight",'280');
			sFlashPlayer144.addVariable("width",'400');
			sFlashPlayer144.addVariable("height",'300');
			sFlashPlayer144.addVariable("backcolor","0x00407F");
			sFlashPlayer144.addVariable("frontcolor","0xFEFEFE");
			sFlashPlayer144.addVariable("lightcolor","0xFFFFFF");
			sFlashPlayer144.addVariable("shuffle","false");
			sFlashPlayer144.addVariable("WMODE","transparet");
			sFlashPlayer144.addVariable("repeat","list");
			sFlashPlayer144.write('FlashPlayer' + id); 	
		    
			preview.style.display = 'block';
		}
		else if (extension == 'mp3' || extension == 'wmv' || extension == 'wma')
		{
			
			var WMP7;
			var videoHeight = ' height="300" ';
			var videoWidth = 'width="300" ';
			var link = path;
			try
			{
				if ( navigator.appName != "Netscape" )
				{
					WMP7 = new ActiveXObject('WMPlayer.OCX');
				}
			}
			catch (error)
			{
				;
			}
			var HTML = '';

			// Windows Media Player 7 Code
			if ( WMP7 )
			{
				HTML +=  ('<OBJECT '+videoHeight+' ' + videoWidth + ' classid="clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6" VIEWASTEXT>');
				HTML +=  ('<PARAM NAME="URL" VALUE="'+link+'">');	
				HTML +=  ('<param name="autostart" value="true">');
				HTML +=  ('<param name="showcontrols" value="true">');	
				HTML +=  ('<param name="playcount" value="9999">');
				HTML +=  ('<param name="autorewind" value="1"><param name="wmode" value="opaque" />');
				HTML +=  ('<PARAM NAME="Volume" VALUE="100">');
				HTML +=  ('</OBJECT>');	
			}

			// Windows Media Player 6.4 Code
			else
			{
				HTML +=  ('<OBJECT  classid="CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95" ');
				HTML +=  ('codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,4,5,715" ');
				HTML +=  (' ' + videoHeight + ' ' + videoWidth + ' ');
				HTML +=  ('standby="Loading Microsoft Windows Media Player components..." ');
				HTML +=  ('type="application/x-oleobject" VIEWASTEXT> ');
				HTML +=  ('<PARAM NAME="FileName"           VALUE="'+link+'">');
				HTML +=  ('<PARAM NAME="TransparentAtStart" Value="false">');
				HTML +=  ('<PARAM NAME="AutoStart"          Value="true">');
				HTML +=  ('<PARAM NAME="AnimationatStart"   Value="false">');
				HTML +=  ('<PARAM NAME="ShowControls"       Value="false">');
				HTML +=  ('<PARAM NAME="ShowDisplay"	 value ="false">');
				HTML +=  ('<PARAM NAME="playCount" VALUE="999">');
				HTML +=  ('<PARAM NAME="displaySize" 	 Value="0"><param name="wmode" value="opaque" />');
				HTML +=  ('<PARAM NAME="Volume" VALUE="100">');
				HTML +=  ('<Embed type="application/x-mplayer2" ');
				HTML +=  ('pluginspage= ');
				HTML +=  ('"http://www.microsoft.com/Windows/MediaPlayer/" ');
				HTML +=  ('src="'+link+'" ');
				HTML +=  ('Name=MediaPlayer  wmode="transparent"');
				HTML +=  ('transparentAtStart=0 ');
				HTML +=  ('autostart=1 ');
				HTML +=  ('playcount=999 ');
				HTML +=  ('volume=100');
				HTML +=  ('animationAtStart=0 ');
				HTML +=  (' ' + videoHeight + ' ' + videoWidth + ' ');	
				HTML +=  ('displaySize=0></embed> ');
				HTML +=  ('</OBJECT> ');
			}
			preview.innerHTML = HTML;
			preview.style.display = 'block';
		}
		preview.innerHTML = '<div style="background-color:#a0a0a0; padding:5px 10px 5px 10px; color:black;"><span style="font-weight:bold;">Khung xem trước</span><a href="javascript:closePreview()" style="margin-left:300px; color:black;">Đóng</a></div><div style="border:solid 10px #d0d0d0; text-align:center;">' + preview.innerHTML + '</div>';
    }
    function closePreview()
    {
		document.getElementById('preview').style.display = 'none';
    }
</script>

