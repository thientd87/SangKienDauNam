<%@ Control Language="C#" AutoEventWireup="true" Codebehind="AlbumImages.ascx.cs"
	Inherits="Portal.GUI.EditoralOffice.MainOffce.Media.AlbumImages" %>
<%@ Register Src="UserControl/cltPopupMedia.ascx" TagName="cltPopupMedia" TagPrefix="uc1" %>
<%@ Register Src="UserControl/ctlPopupView.ascx" TagName="ctlPopupView" TagPrefix="uc2" %>
<%@ Register Src="UserControl/cltPopupFlash.ascx" TagName="cltPopupFlash" TagPrefix="uc3" %>
<link href="/styles/Gallery/Common.css" rel="stylesheet" type="text/css" />
<link href="/styles/common.css" rel="stylesheet" type="text/css" />
<link href="/styles/Newsedit.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="/styles/Portal.css" type="text/css" />
<link href="/styles/Core.css" rel="stylesheet" type="text/css" />
<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />

<script language="javascript" type="text/javascript" src="/scripts/Gallery/Intelliworks.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/core.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/events.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/css.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/coordinates.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Grid.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/drag.js"></script>

<script language="JavaScript" type="text/javascript" src="/scripts/Gallery/swfobject.js"></script>

<script language="JavaScript" type="text/javascript" src="/AssetManager/js/ImageZoom.js"></script>

<script language="JavaScript" type="text/JavaScript">
    
        window.onload = function() 
        {
            var coordinates = ToolMan.coordinates();
	        var drag = ToolMan.drag();
	        
            var loginForm = document.getElementById("loginForm");
	        drag.createSimpleGroup(loginForm);
	        
	        var divMedia = document.getElementById("divMedia");
	        drag.createSimpleGroup(divMedia);
	        
	        var divFlash = document.getElementById("divFlash");
	        drag.createSimpleGroup(divFlash);
	        
        }
        function ImgResize(imgPath,thumbSize){
        
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
            else
            {
                var currImg=document.getElementById("fPic");
                currImg.src=newImg.src;
                currImg.width=_width;
                currImg.height=_height;
            }
        }
        
        function ImgResizeViewByList(imgPath,thumbSize){
        
            document.getElementById("loginForm").style.top = "50px";
            document.getElementById("loginForm").style.left = "50px";
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
            else
            {
                var currImg=document.getElementById("fPic");
                currImg.src=newImg.src;
                currImg.width=_width;
                currImg.height=_height;
            }
        }
        
        function CloseFormImage(){
            document.getElementById("fPic").src = "";
            document.getElementById("loginForm").style.visibility = "hidden";
        }
        
        function playMedia(mediaPath){
        
            flashPath = flashPath.toLowerCase();
            
            document.getElementById("divFlash").style.top = "100px";
            document.getElementById("divFlash").style.left = "400px";
            document.getElementById("divFlash").style.visibility = "visible";
            document.getElementById("divFlash").style.display="inline";
            document.getElementById("divFlash").style.position ="absolute";
            
            if(flashPath.indexOf('.swf') > 0)
            {
                var so = new SWFObject(flashPath, "sotester", "300", "300", "9");
                so.write("flashcontent");
            }
            else
            {
                
                var sFlashPlayer144 = new SWFObject("/images/share/mediaplayer.swf","playlist","300","300","9");
                sFlashPlayer144.addParam("allowfullscreen","true");
                sFlashPlayer144.addVariable("file",flashPath );
                sFlashPlayer144.addVariable("displayheight","300");
                sFlashPlayer144.addVariable("width","300");
                sFlashPlayer144.addVariable("height","300");
                sFlashPlayer144.addVariable("backcolor","0x00407F");
                sFlashPlayer144.addVariable("frontcolor","0xFEFEFE");
                sFlashPlayer144.addVariable("lightcolor","0xFFFFFF");
                sFlashPlayer144.addVariable("shuffle","false");
                sFlashPlayer144.addVariable("autostart","true");
                sFlashPlayer144.addVariable("repeat","list");
                sFlashPlayer144.write("flashcontent");
                
            }
        }
        
        function playMediaByViewList(mediaPath){
        
            flashPath = flashPath.toLowerCase();
            
            document.getElementById("divFlash").style.top = "50px";
            document.getElementById("divFlash").style.left = "50px";
            document.getElementById("divFlash").style.visibility = "visible";
            document.getElementById("divFlash").style.display="inline";
            document.getElementById("divFlash").style.position ="absolute";
            
            if(flashPath.indexOf('.swf') > 0)
            {
                var so = new SWFObject(flashPath, "sotester", "300", "300", "9");
                so.write("flashcontent");
            }
            else
            {
                
                var sFlashPlayer144 = new SWFObject("/images/portal/share/mediaplayer.swf","playlist","300","300","9");
                sFlashPlayer144.addParam("allowfullscreen","true");
                sFlashPlayer144.addVariable("file",flashPath );
                sFlashPlayer144.addVariable("displayheight","300");
                sFlashPlayer144.addVariable("width","300");
                sFlashPlayer144.addVariable("height","300");
                sFlashPlayer144.addVariable("backcolor","0x00407F");
                sFlashPlayer144.addVariable("frontcolor","0xFEFEFE");
                sFlashPlayer144.addVariable("lightcolor","0xFFFFFF");
                sFlashPlayer144.addVariable("shuffle","false");
                sFlashPlayer144.addVariable("autostart","true");
                sFlashPlayer144.addVariable("repeat","list");
                sFlashPlayer144.write("flashcontent");
                
            }
        }
        
        function CloseFormMedia(){
            mediaPlayer.stop();
            document.getElementById("divMedia").style.visibility = "hidden";
        }
        
              
        function playFlash(flashPath){
            
            document.getElementById("divFlash").style.top = "100px";
            document.getElementById("divFlash").style.left = "400px";
            document.getElementById("divFlash").style.visibility = "visible";
            document.getElementById("divFlash").style.display="inline";
            document.getElementById("divFlash").style.position ="absolute";
            var so = new SWFObject(flashPath, "sotester", "300", "300", "9");
            so.write("flashcontent");
        }
        
        function playFlashByViewList(flashPath){
            
            document.getElementById("divFlash").style.top = "50px";
            document.getElementById("divFlash").style.left = "50px";
            document.getElementById("divFlash").style.visibility = "visible";
            document.getElementById("divFlash").style.display="inline";
            document.getElementById("divFlash").style.position ="absolute";
            var so = new SWFObject(flashPath, "sotester", "300", "300", "9");
            so.write("flashcontent");
        }
        
        function CloseFormFlash(){
            document.getElementById("flashcontent").innerHTML = "";
            document.getElementById("divFlash").style.visibility = "hidden";
        }
        
        function ViewImage(imagePath){
            
            document.getElementById("divViewImage").style.top = "50px";
            document.getElementById("divViewImage").style.left = "50px";
            document.getElementById("divViewImage").style.visibility = "visible";
            document.getElementById("divViewImage").style.display="inline";
            document.getElementById("divViewImage").style.position ="absolute";
            var so = new SWFObject(flashPath, "sotester", "300", "300", "9");
            so.write("flashcontent");
        }
        
        
        function SelectAll()
        {
            var elm=document.forms[0].elements;
            for(i=0;i<elm.length;i++)
            if(elm[i].type=="checkbox")
            {
                elm[i].click();
            }
            return false;
        }
            
     
       
        
</script>

<style type="text/css">   
    	#Drag{
		    cursor:hand;
		    position:absolute;
	    }  
    	#ImgDrag{
		    cursor:hand;
		    position:absolute;
	    }  
        .imgparent
        {
            position:static; cursor:hand;visibility:visible;
        }
        .imgOver
        {
	        position:absolute;
	        visibility:visible;
	        cursor:hand;
        }
        .imgOut
        {
	        position:absolute;
	        visibility:hidden;
	        cursor: default;
        }
        .hupdate
        {
	        font-family:Arial; color:White; font-size:12px; font-weight:bold; border-right: #666699 1px solid; border-top: #666699 1px solid; border-left: #666699 1px solid; border-bottom: #666699 1px solid;
        }
        .bupdate
        {
	        width: 100%;background-color:steelblue;color: white;
        }
        .gridBorder
        {
           border: #79A4D2 1px solid;
           font-family:Arial; 
           color:black; 
           font-size:12px;
        }
        .borderImg
        {
          border:solid 1px steelblue;
          background-color:White;
          position:absolute; 
          cursor:hand;
          visibility:hidden; 
          FILTER: progid:DXImageTransform.Microsoft.Shadow(color=#888888,strength=5,direction=135); 
        }
        .gallery_border
        {
	        border-color: #B8C1CA;
	        border-width: 1px;
	        border-style: solid;
	        height:140px;
	        width:123px;
        }
        .gallery_Desc
        {
	        color: black;
	        font-family: Tahoma;
	        font-size: 8pt;
	        font-weight: bold;
            padding-top:2px;
        }
        .popup_txt
        {
	        color:#ffffff;
	        font-family: Trebuchet MS;
	        font-size:11;
        }
        .bluegrad_bold
        {
	        filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr= '#E0F1FF' , endColorStr= '#FFFFFF' , gradientType= '1' );
	        font-family: Trebuchet MS;
	        font-size: 11;
	        color:#DF4E1F;
	        font-weight:bold;
        }   
        .topHelp{
          z-index:1000;
        }  
        .dragImage{
            position:absolute; left:20; top:10; z-index:0
        }
        
        #flashcontent {
		    border: solid 1px #000;
		    width: 300px;
		    height: 300px;
		    float: left;
		    margin: 15px 20px;
	    }
     </style>
<table cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr>
		<td style="height: 10px">
		</td>
	</tr>
	<tr>
		<td style="height: 24px">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td align="left">
						<asp:ImageButton ID="UpBtn" runat="server" EnableViewState="False" CommandArgument="../"
							CommandName="SelectFolder" ImageUrl="~/Images/icons/Up.gif" ToolTip="Up one level"
							OnClick="UpBtn_Click"></asp:ImageButton>
						<asp:ImageButton ID="GoRoot" runat="server" EnableViewState="False" CommandArgument="/"
							CommandName="SelectFolder" ImageUrl="~/Images/icons/Home.gif" AlternateText="Back to Root Directory"
							OnClick="GoRoot_Click"></asp:ImageButton>
					</td>
					<td>
						<asp:TextBox ID="txtNewFolder" runat="server" CssClass="ms-long" Width="120px"></asp:TextBox>
						<asp:Button ID="btnCreateNewFolder" runat="server" CssClass="ms-input" OnClick="btnCreateNewFolder_Click"
							Text="Tạo thư mục mới" /></td>
					<td align="right">
						<div id="divTool" style="display: none; overflow: auto">
							<img src="/images/icons/btn_zoomout.gif" alt="Thu nhỏ ảnh" onmouseover="ZoomOut();"
								onmouseout="clearzoom();" />
							<img src="/images/icons/btn_FIT.gif" alt="Vừa với khung" onclick="FixSizeMax();" />
							<img src="/images/icons/btn_Restore.gif" alt="Kích thước thực" onclick="Restore();" />
							<img src="/images/icons/btn_zoomin.gif" alt="Phóng to ảnh" onmouseover="ZoomIn();"
								onmouseout="clearzoom();" />&nbsp; &nbsp;<asp:DropDownList ID="cbxViewBy" runat="server"
									OnSelectedIndexChanged="cbxViewBy_SelectedIndexChanged" AutoPostBack="true" Visible="False">
									<asp:ListItem>Thumbnails</asp:ListItem>
									<asp:ListItem Selected="True">List</asp:ListItem>
								</asp:DropDownList>
						</div>
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
			<!-- Modified 23012008 -->
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td valign="top">
						<asp:GridView Width="100%" ID="grdListImage" runat="server" HeaderStyle-CssClass="grdHeader"
							RowStyle-CssClass="grdItem" AlternatingRowStyle-CssClass="grdAlterItem" AutoGenerateColumns="False"
							AllowPaging="True" OnRowCommand="grdListFile_RowCommand" OnRowEditing="grdListImage_RowEditing"
							OnPageIndexChanging="grdListImage_PageIndexChanging" OnRowDataBound="grdListImage_DataBound">
							<PagerSettings Mode="NumericFirstLast" PageButtonCount="6" />
							<PagerStyle BackColor="White" HorizontalAlign="Center" CssClass="ms-input" />
							<Columns>
								<asp:TemplateField>
									<HeaderTemplate>
										<input type="checkbox" id="chkAll" onclick="CheckAll();" />
									</HeaderTemplate>
									<ItemTemplate>
										<input type="checkbox" id="chkSelect" name="chkSelect" value='<%#Eval("FileName")%>' />
									</ItemTemplate>
									<HeaderStyle Width="22px" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="T&#234;n file">
									<ItemStyle HorizontalAlign="Left" />
									<ItemTemplate>
										<a href='<%# "javascript:showImage(\""+ Eval("FullName") +"\")" %>'>
											<img alt="" src="<%#Eval("Avatar")%>" width="10" border="0" />
											<asp:LinkButton ID="lnbSelectFile" CommandName="SelectFile" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName_IsFile")%>'
												CssClass='normalLnk' runat="server">
                                 <%#Eval("FileShortName") %>
											</asp:LinkButton>
										</a>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Size">
									<ItemTemplate>
										<%#Eval("Size")%>
									</ItemTemplate>
									<ItemStyle Width="10%" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Ng&#224;y Tạo" Visible="False">
									<ItemTemplate>
										<%#Eval("Modified")%>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="X&#243;a">
									<ItemTemplate>
										<asp:LinkButton ID="lnbDeleteFile" CommandName="DeleteFile" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName_IsFile")%>'
											CssClass='normalLnk' runat="server" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');">
                                 <img src="/images/icons/delete.gif" border=0 />
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" Width="5%" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Chọn">
									<ItemTemplate>
										<asp:ImageButton ID="btnPreview" CommandName="SelectFile" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName_IsFile")%>'
											CssClass='normalLnk' runat="server" ImageUrl="/images/PollManager/Add.png" />
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" Width="5%" />
								</asp:TemplateField>
							</Columns>
							<RowStyle CssClass="grdItem" />
							<HeaderStyle CssClass="grdHeader" />
							<AlternatingRowStyle CssClass="grdAlterItem" />
						</asp:GridView>
					</td>
					<td style="width: 400px; border-style: solid; border-width: 1px" valign="top">
						<div id="divImageShow" style="width: 400px; height: 277px; background: rgb(255, 255, 255) none repeat scroll 0%;
							overflow: auto;">
						</div>
					</td>
				</tr>
			</table>
			<!-- End Modified 23012008 -->
		</td>
	</tr>
	<tr>
		<td colspan="2" align="left">
			<asp:GridView CellPadding="4" BackColor="#ffffff" ID="GridView1" runat="server" ShowHeader="False"
				AllowPaging="True" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="grdAlterItem"
				HeaderStyle-CssClass="grdHeader" RowStyle-CssClass="grdItem" HorizontalAlign="Center"
				PageSize="3" Width="96%" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
				OnRowCommand="GridView1_RowCommand" GridLines="None">
				<PagerSettings Mode="NumericFirstLast" PageButtonCount="6" Position="Bottom" />
				<PagerStyle BackColor="White" HorizontalAlign="Center" />
				<RowStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Blue" HorizontalAlign="Center"
					VerticalAlign="Middle" />
				<Columns>
					<asp:TemplateField>
						<ItemTemplate>
							<table id="tb1" runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<img alt="" style="border: solid 2px #ffffff; visibility: visible; position: static;
											cursor: hand;" src='/images/Uploaded/<%=_SetFullFolder%>/Thumbnails/ChildThumb/<%#DataBinder.Eval(Container.DataItem,"FileName1")%>'
											onmouseover="OnOverImage(this,'<%#DataBinder.Eval(Container.DataItem,"FileName1")%>');"
											onmouseout="OnOffImage(this);" id="img1" runat="server" />
										<table bgcolor="#ffffff" class="borderImg" style="position: absolute; visibility: hidden;"
											onmouseover="imgOver(this);" onmouseout="imgOut(this)" id='img1<%#DataBinder.Eval(Container.DataItem,"FileName1")%>'
											cellpadding="0" cellspacing="0" border="0" align="center">
											<tbody>
												<tr>
													<td valign="top">
														<a onclick="javascript:ImgResize('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName1")%>',500)">
															<img name="ViewImg" src='/images/Uploaded/<%=_SetFullFolder%>/Thumbnails/<%#DataBinder.Eval(Container.DataItem,"FileName1")%>'
																alt='<%#DataBinder.Eval(Container.DataItem,"FileName1")%>' border="0"></a>
													</td>
												</tr>
											</tbody>
										</table>
										<div class="gallery_Desc">
											<asp:Label ID="l1" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName1")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="chkSelect" name="chkSelect" value='<%#Eval("FileName1")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="cmdDelete1" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName1")%>'
														CommandName="DeleteFile1" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='folder1' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<asp:LinkButton ID="linkbutton1" CommandName="SelectFolder" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName1")%>'
											CssClass='normalLnk' runat="server">
                            <img  src="/images/icons/folder.JPG" alt="" style="border:solid 2px #ffffff;visibility:visible;position:static; cursor:hand;" />
										</asp:LinkButton>
										<div class="gallery_Desc">
											<asp:Label ID="Label1" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName1")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox1" name="chkSelect" value='<%#Eval("FileName1")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="LinkButton5" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName1")%>'
														CommandName="DeleteFolder1" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='flash1' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<a href="#" onclick="playFlash('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName1")%>')">
											<img src="/images/icons/flash.jpg" alt="<%#DataBinder.Eval(Container.DataItem,"FileName1")%>"
												style="border: solid 2px #ffffff; visibility: visible; position: static; cursor: hand;" />
										</a>
										<div class="gallery_Desc">
											<asp:Label ID="lblFlash1" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName1")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox2" name="chkSelect" value='<%#Eval("FileName1")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="lnkFlash1" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName1")%>'
														CommandName="DeleteFile1" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='media1' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<a href="#" onclick="javascript:playMedia('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName1")%>');">
											<img src="/images/icons/windowmedia.jpg" alt="<%#DataBinder.Eval(Container.DataItem,"FileName1")%>"
												style="border: solid 2px #ffffff; visibility: visible; position: static; cursor: hand;" />
										</a>
										<div class="gallery_Desc">
											<asp:Label ID="lblMedia1" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName1")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox3" name="chkSelect" value='<%#Eval("FileName1")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="lnkMedia1" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName1")%>'
														CommandName="DeleteFile1" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</ItemTemplate>
						<ItemStyle VerticalAlign="Top" />
					</asp:TemplateField>
					<asp:TemplateField>
						<ItemTemplate>
							<table id="tb2" runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<img alt="" style="border: solid 2px #ffffff; visibility: visible; position: static;
											cursor: hand;" src='/images/Uploaded/<%=_SetFullFolder%>/Thumbnails/ChildThumb/<%#DataBinder.Eval(Container.DataItem,"FileName2")%>'
											onmouseover="OnOverImage(this,'<%#DataBinder.Eval(Container.DataItem,"FileName2")%>');"
											onmouseout="OnOffImage(this);" id="img2" runat="server" />
										<table bgcolor="#ffffff" class="borderImg" style="position: absolute; visibility: hidden;"
											onmouseover="imgOver(this);" onmouseout="imgOut(this)" id='img2<%#DataBinder.Eval(Container.DataItem,"FileName2")%>'
											cellpadding="0" cellspacing="0" border="0" align="center">
											<tbody>
												<tr>
													<td valign="top">
														<a onclick="javascript:ImgResize('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName2")%>',500)">
															<img name="ViewImg" src='/images/Uploaded/<%=_SetFullFolder%>/Thumbnails/<%#DataBinder.Eval(Container.DataItem,"FileName2")%>'
																alt='<%#DataBinder.Eval(Container.DataItem,"FileName2")%>' border="0"></a>
													</td>
												</tr>
											</tbody>
										</table>
										<div class="gallery_Desc">
											<asp:Label ID="l2" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName2")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="chkSelect" name="chkSelect" value='<%#Eval("FileName2")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="cmdDelete2" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName2")%>'
														CommandName="DeleteFile2" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='folder2' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<asp:LinkButton ID="linkbutton2" CommandName="SelectFolder" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName2")%>'
											CssClass='normalLnk' runat="server">
                            <img  src="/images/icons/folder.JPG" alt="" style="border:solid 2px #ffffff;visibility:visible;position:static; cursor:hand;" />
										</asp:LinkButton>
										<div class="gallery_Desc">
											<asp:Label ID="Label2" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName2")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox4" name="chkSelect" value='<%#Eval("FileName2")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="LinkButton6" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName2")%>'
														CommandName="DeleteFolder2" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='flash2' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<a href="#" onclick="playFlash('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName2")%>')">
											<img src="/images/icons/flash.jpg" alt="<%#DataBinder.Eval(Container.DataItem,"FileName2")%>"
												style="border: solid 2px #ffffff; visibility: visible; position: static; cursor: hand;" />
										</a>
										<div class="gallery_Desc">
											<asp:Label ID="lblFlash2" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName2")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox5" name="chkSelect" value='<%#Eval("FileName2")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="lnkFlash2" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName2")%>'
														CommandName="DeleteFile2" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='media2' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<a href="#" onclick="javascript:playMedia('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName2")%>');">
											<img src="/images/icons/windowmedia.jpg" alt="<%#DataBinder.Eval(Container.DataItem,"FileName2")%>"
												style="border: solid 2px #ffffff; visibility: visible; position: static; cursor: hand;" />
										</a>
										<div class="gallery_Desc">
											<asp:Label ID="lblMedia2" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName2")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox6" name="chkSelect" value='<%#Eval("FileName2")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="lnkMedia2" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName2")%>'
														CommandName="DeleteFile2" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</ItemTemplate>
						<ItemStyle VerticalAlign="Top" />
					</asp:TemplateField>
					<asp:TemplateField>
						<ItemTemplate>
							<table id="tb3" runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<img alt="" style="border: solid 2px #ffffff; visibility: visible; position: static;
											cursor: hand;" src='/images/Uploaded/<%=_SetFullFolder%>/Thumbnails/ChildThumb/<%#DataBinder.Eval(Container.DataItem,"FileName3")%>'
											onmouseover="OnOverImage(this,'<%#DataBinder.Eval(Container.DataItem,"FileName3")%>');"
											onmouseout="OnOffImage(this);" id="img3" runat="server" />
										<table bgcolor="#ffffff" class="borderImg" style="position: absolute; visibility: hidden;"
											onmouseover="imgOver(this);" onmouseout="imgOut(this)" id='img3<%#DataBinder.Eval(Container.DataItem,"FileName3")%>'
											cellpadding="0" cellspacing="0" border="0" align="center">
											<tbody>
												<tr>
													<td valign="top">
														<a onclick="javascript:ImgResize('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName3")%>',500)">
															<img name="ViewImg" src='/images/Uploaded/<%=_SetFullFolder%>/Thumbnails/<%#DataBinder.Eval(Container.DataItem,"FileName3")%>'
																alt='<%#DataBinder.Eval(Container.DataItem,"FileName3")%>' border="0"></a>
													</td>
												</tr>
											</tbody>
										</table>
										<div class="gallery_Desc">
											<asp:Label ID="l3" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName3")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="chkSelect" name="chkSelect" value='<%#Eval("FileName3")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="cmdDelete3" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName3")%>'
														CommandName="DeleteFile3" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='folder3' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<asp:LinkButton ID="linkbutton3" CommandName="SelectFolder" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName3")%>'
											CssClass='normalLnk' runat="server">
                            <img  src="/images/icons/folder.JPG" alt="" style="border:solid 2px #ffffff;visibility:visible;position:static; cursor:hand;" />
										</asp:LinkButton>
										<div class="gallery_Desc">
											<asp:Label ID="Label3" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName3")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox7" name="chkSelect" value='<%#Eval("FileName3")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="LinkButton7" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName3")%>'
														CommandName="DeleteFolder3" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='flash3' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<a href="#" onclick="playFlash('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName3")%>')">
											<img src="/images/icons/flash.jpg" alt="<%#DataBinder.Eval(Container.DataItem,"FileName3")%>"
												style="border: solid 2px #ffffff; visibility: visible; position: static; cursor: hand;" />
										</a>
										<div class="gallery_Desc">
											<asp:Label ID="lblFlash3" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName3")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox8" name="chkSelect" value='<%#Eval("FileName3")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="lnkFlash3" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName3")%>'
														CommandName="DeleteFile3" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='media3' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<a href="#" onclick="javascript:playMedia('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName3")%>');">
											<img src="/images/icons/windowmedia.jpg" alt="<%#DataBinder.Eval(Container.DataItem,"FileName3")%>"
												style="border: solid 2px #ffffff; visibility: visible; position: static; cursor: hand;" />
										</a>
										<div class="gallery_Desc">
											<asp:Label ID="lblMedia3" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName3")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox9" name="chkSelect" value='<%#Eval("FileName3")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="lnkMedia3" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName3")%>'
														CommandName="DeleteFile3" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</ItemTemplate>
						<ItemStyle VerticalAlign="Top" />
					</asp:TemplateField>
					<asp:TemplateField>
						<ItemTemplate>
							<table id="tb4" runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<img alt="" style="border: solid 2px #ffffff; visibility: visible; position: static;
											cursor: hand;" src='/images/Uploaded/<%=_SetFullFolder%>/Thumbnails/ChildThumb/<%#DataBinder.Eval(Container.DataItem,"FileName4")%>'
											onmouseover="OnOverImage(this,'<%#DataBinder.Eval(Container.DataItem,"FileName4")%>');"
											onmouseout="OnOffImage(this);" id="img4" runat="server" />
										<table bgcolor="#ffffff" class="borderImg" style="position: absolute; visibility: hidden;"
											onmouseover="imgOver(this);" onmouseout="imgOut(this)" id='img4<%#DataBinder.Eval(Container.DataItem,"FileName4")%>'
											cellpadding="0" cellspacing="0" border="0" align="center">
											<tbody>
												<tr>
													<td valign="top">
														<a onclick="javascript:ImgResize(/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName4")%>',500)">
															<img name="ViewImg" src='/images/Uploaded/<%=_SetFullFolder%>/Thumbnails/<%#DataBinder.Eval(Container.DataItem,"FileName4")%>'
																alt='<%#DataBinder.Eval(Container.DataItem,"FileName4")%>' border="0"></a>
													</td>
												</tr>
											</tbody>
										</table>
										<div class="gallery_Desc">
											<asp:Label ID="l4" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName4")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="chkSelect" name="chkSelect" value='<%#Eval("FileName4")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="cmdDelete4" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName4")%>'
														CommandName="DeleteFile4" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='folder4' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<asp:LinkButton ID="linkbutton4" CommandName="SelectFolder" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName4")%>'
											CssClass='normalLnk' runat="server">
                            <img  src="/images/icons/folder.JPG" alt="" style="border:solid 2px #ffffff;visibility:visible;position:static; cursor:hand;" />
										</asp:LinkButton>
										<div class="gallery_Desc">
											<asp:Label ID="Label4" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName4")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox10" name="chkSelect" value='<%#Eval("FileName4")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="LinkButton8" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName4")%>'
														CommandName="DeleteFolder4" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='flash4' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<a href="#" onclick="playFlash('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName4")%>')">
											<img src="/images/icons/flash.jpg" alt="<%#DataBinder.Eval(Container.DataItem,"FileName4")%>"
												style="border: solid 2px #ffffff; visibility: visible; position: static; cursor: hand;" />
										</a>
										<div class="gallery_Desc">
											<asp:Label ID="lblFlash4" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName4")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox11" name="chkSelect" value='<%#Eval("FileName4")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="lnkFlash4" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName4")%>'
														CommandName="DeleteFile4" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id='media4' runat="server" cellpadding="0" bgcolor="#f8f8f8" cellspacing="0"
								border="0" class="gallery_border">
								<tr>
									<td valign="middle">
										<a href="#" onclick="javascript:playMedia('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName4")%>');">
											<img src="/images/icons/windowmedia.jpg" alt="" style="border: solid 2px #ffffff;
												visibility: visible; position: static; cursor: hand;" />
										</a>
										<div class="gallery_Desc">
											<asp:Label ID="lblMedia4" Text='<%#DataBinder.Eval(Container.DataItem,"FileShortName4")%>'
												runat="server" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
											<tr>
												<td valign="top" align="left" style="width: 18px;">
													<input type="checkbox" id="Checkbox12" name="chkSelect" value='<%#Eval("FileName4")%>' />
												</td>
												<td align="right" style="padding-right: 3px;">
													<asp:LinkButton runat="server" CssClass="normalLnk" Text="Xóa" ID="lnkMedia4" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileName4")%>'
														CommandName="DeleteFile4" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</ItemTemplate>
						<ItemStyle VerticalAlign="Top" />
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
			
		</td>
	</tr>
	<tr>
		<td style="font-size: 8pt; font-family: Tahoma; height: 24px;" align="right">
			<a onclick="return SelectAll();" href="#" class="normalLnk">Chọn tất cả</a>&nbsp;|
			<a onclick="return SelectAll();" href="#" class="normalLnk">Bỏ chọn</a>&nbsp;|
			<asp:LinkButton ID="LinkDelete" runat="server" OnClick="LinkDelete_Click" CssClass="normalLnk"
				OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');">Xóa file đã chọn</asp:LinkButton>
		</td>
	</tr>
	<tr>
		<td style="padding-top: 10px;">
			<table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 80px;
				border: 1px solid #79A4D2; background-color: #E5E5E5; clear: both">
				<tr>
					<td align="left" class="ms-input">
						Tìm theo từ khóa:
						<asp:TextBox ID="txtKeyword" runat="server" Width="100px" CssClass="ms-long"></asp:TextBox>&nbsp;
						<asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click"
							CssClass="ms-input" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td style="padding-top: 10px" height="10">
		</td>
	</tr>
	<tr>
		<td>
			<table align="center" border="0" cellpadding="1" cellspacing="0" width="100%">
				<tr valign="bottom">
					<td align="center" valign="top">
						<table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td align="left" style="text-align: right;" class="ms-input">
									File thu 1:&nbsp;&nbsp;</td>
								<td align="left" style="text-align: left">
									<input id="file2" runat="server" name="file2" style="font-size: 8pt" type="file"
										class="ms-input" />
								</td>
							</tr>
							<tr>
								<td align="left" style="text-align: right; height: 22px;" class="ms-input">
									File thu 2:&nbsp;&nbsp;</td>
								<td align="left" style="text-align: left; height: 22px;">
									<input id="file3" runat="server" name="file3" style="font-size: 8pt" type="file"
										class="ms-input" />
								</td>
							</tr>
							<tr>
								<td align="left" style="text-align: right" class="ms-input">
									File thu 3:&nbsp;&nbsp;</td>
								<td align="left" style="text-align: left">
									<input id="file4" runat="server" name="file4" style="font-size: 8pt" type="file"
										class="ms-input" />
								</td>
							</tr>
							<tr>
								<td align="left" style="text-align: right" class="ms-input">
									File thu 4:&nbsp;&nbsp;</td>
								<td align="left" style="text-align: left">
									<input id="file1" runat="server" name="file1" style="font-size: 8pt" type="file"
										class="ms-input" />
								</td>
							</tr>
							<tr>
								<td align="left" style="text-align: right" class="ms-input">
									File thu 5:&nbsp;&nbsp;</td>
								<td align="left" style="text-align: left">
									<input id="file5" runat="server" name="file5" style="font-size: 8pt" type="file"
										class="ms-input" />
								</td>
							</tr>
							<tr>
								<td align="left" style="width: 240px; padding-top: 4px; height: 22px;">
								</td>
								<td align="left" style="text-align: left; height: 22px;">
									<asp:Button ID="btnSave" runat="server" CssClass="normalCtrl" OnClick="btnSave_Click"
										Text="Tải file" Width="80px" />
									<!--<input id="btnBack" runat="server" class="normalCtrl" onclick="window.history.back();"
										style="width: 70px" type="button" value="Quay lai" />-->
									<!--<br /><br />
									<input id="btnUploadMultifile" class="ms-input" type="button" onclick="eraseCookie('path_images');createCookie('path_images','<%= GetPathToUploadFile() %>',1); openpreview_a('/AssetManager/upload_imagealbum.asp?id=<%= GetPathToUploadFile() %>',800,600)"
										value="Chuyển cách upload file khác" />-->
								</td>
							</tr>
							<tr>
								<td align="left" style="width: 240px; padding-top: 4px">
								</td>
								<td align="left" style="text-align: left">
									<asp:Label ID="lblFileError" runat="server" CssClass="msgWarning"></asp:Label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<div id="loginForm" class="Loginvisible" style="visibility: hidden; display: none;
	left: 782px; top: 521px;">
	<uc2:ctlPopupView ID="CtlPopupView2" runat="server" />
</div>
<div id="divMedia" class="Loginvisible" style="visibility: hidden; display: none;
	left: 107px; top: 520px;">
	<uc1:cltPopupMedia ID="CltPopupMedia1" runat="server"></uc1:cltPopupMedia>
</div>
<div id="divFlash" class="Loginvisible" style="visibility: hidden; display: none;
	left: 447px; top: 519px;">
	<uc3:cltPopupFlash ID="cltPopupFlash1" runat="server"></uc3:cltPopupFlash>
</div>

<script type="text/javascript">
    function showImage(fullName)
    {
        if(fullName == "")
        {
            document.getElementById("divImageShow").innerHTML = "";
            document.getElementById("divTool").style.display = "none";
        }
        else
        {
            document.getElementById("divImageShow").innerHTML = "<img src='"+fullName+"' alt='' id='imgShow' name='imgShow'>";
            document.getElementById("divTool").style.display = "";
        }
    }
    function ZoomIn()
    {
        zoom(document.getElementById("imgShow").width,document.getElementById("imgShow").height,"imgShow","in");
    }
    function ZoomOut()
    {
        zoom(document.getElementById("imgShow").width,document.getElementById("imgShow").height,"imgShow","out");
    }
    
    
    function FixSizeMax()
    {
    	pw = document.getElementById('divImageShow').style.width.replace('px', '').replace('%','');
	    ph = document.getElementById('divImageShow').style.height.replace('px', '').replace('%','');
	    iw = document.getElementById('imgShow').width;
	    ih = document.getElementById('imgShow').height;		    
	    
	    if(parseInt(iw) > parseInt(ih))
	    {	
		    zoom('','','imgShow','restore');
		    zoom(pw - 70,ih*(pw-70)/iw,'imgShow','out');		    
		    clearzoom();		    
	    }
	    else
	    {
		    zoom('','','imgShow','restore');
		    zoom(iw*(ph - 70)/ih, ph-70,'imgShow','out');
		    clearzoom();
	    }
    }
    function Restore()
    {
        zoom('','','imgShow','restore');
    }
    
    
function createCookie(name,value,days) {
	if (days) {
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
	}
	else var expires = "";
	document.cookie = name+"="+value+expires+"; path=/";
}

function readCookie(name) {
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for(var i=0;i < ca.length;i++) {
		var c = ca[i];
		while (c.charAt(0)==' ') c = c.substring(1,c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	}
	return null;
}

function eraseCookie(name) {
	createCookie(name,"",-1);
}    



</script>

