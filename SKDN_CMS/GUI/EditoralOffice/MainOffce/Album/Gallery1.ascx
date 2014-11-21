<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Gallery1.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Tool.Gallery1" %>
<div class="container-fluid">
	<!-- BEGIN PAGE HEADER-->   
	<div class="row-fluid">
		<div class="span12">
			<h3 class="page-title">
				Gallery Manager <small>Quản lý album</small>
			</h3>
		</div>
	</div>
      <!-- END PAGE HEADER-->
    <!-- BEGIN PAGE CONTENT-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet box blue">
                 <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-edit"></i>Gallery detail</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload">
                        </a>
                    </div>
                </div>
                	<div class="portlet-body form">
								<div class="tabbable portlet-tabs">
									<div class="tab-content">
										<div class="tab-pane active form-horizontal">
											 <div class="control-group">
													<label class="control-label">Tên Gallery</label>
													<div class="controls">
													    <input type="text" id="txtName" runat="server" value="<%#TxtGallery%>" />
													</div>
												</div>
                                            <asp:GridView ID="GridView1"  CssClass="table table-striped table-hover table-bordered dataTable" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="Object_ID" EnableModelValidation="True" OnRowDataBound="GridView1_RowDataBound"
                                                    OnRowUpdating="GridView1_RowUpdating" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                                    OnRowDeleting="GridView1_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField DataField="Object_ID" HeaderText="Object_ID" InsertVisible="False"
                                                            ReadOnly="True" SortExpression="Object_ID" Visible="False">
                                                           
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Loại" SortExpression="Object_Type">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="cboObject_Type" runat="server">
                                                                    <asp:ListItem Value="1">Image</asp:ListItem>
                                                                    <%--<asp:ListItem Value="2">Video</asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Object_TypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ảnh" SortExpression="Object_Url">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtObject_Url" runat="server" Text='<%# Bind("Object_Url") %>'></asp:TextBox>
                                                                <asp:Literal ID="lbl" runat="server"></asp:Literal>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Image Height="150px" ID="Image1" ImageUrl='<%#Bind("Object_Url") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle  Height="150px" Width="300px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ghi chú" SortExpression="Object_Note">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtObject_Note" runat="server" Text='<%# Bind("Object_Note") %>'
                                                                    TextMode="MultiLine"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Object_Note") %>'></asp:Label>
                                                            </ItemTemplate>
                                                           
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="STT" SortExpression="STT">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtStt" runat="server" Text='<%# Bind("STT") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("STT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" CssClass="AcctionDelete" CausesValidation="False"
                                                                    CommandName="Delete" ImageUrl="~/Images/delete_event.png" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="lstcol7" />
                                                            <ItemStyle CssClass="lstcol7" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                              <table class="table table-striped table-hover table-bordered dataTable themmoi" cellspacing="0" rules="all" border="1"  style="border-collapse:collapse;">
                                              </table>
										</div>
										<div>Bạn có thể thêm nhiều file.</div>
                                            <div class="table-toolbar">
                                                <div class="btn-group">
                                                    <input id="btnThem" type="button" class="btn green"  value="Thêm" >
                                                     
                                                </div>
                                                <div class="btn-group">
                                                    &nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" CssClass="btn green" Text="Lưu" OnClick="btnAdd_Click" />  &nbsp;&nbsp;
                                                    </div>
                                            </div>
									</div>
								</div>
							</div>
            </div>
	    </div>
    </div>
</div>

<script type="text/javascript">

    function avatar_loadValue(arrImage) {
        if (arrImage.length > 0) {
            arrImage[0] = arrImage[0].substr(arrImage[0].indexOf("/Images/Uploaded/"));
            $("#" + $('#current').val()).val(arrImage[0]);
        }
    }
    function openpreview(sUrl, w, h) {
        var winX = 0;
        var winY = 0;
        if (parseInt(navigator.appVersion) >= 4) {
            winX = (screen.availWidth - w) * 0.5;
            winY = (screen.availHeight - h) * 0.5;
        }
        var newWindow = window.open(sUrl, "", "scrollbars,resizable=yes,status=yes, width=" + w + ",height=" + h + ",left=" + winX + ",top=" + winY);
    }
    function chooseFile(type, txtID) {
        $('#current').val(txtID)
        txtID = document.getElementById(txtID).value;
        openpreview("/GUI/EditoralOffice/MainOffce/FileManager/default.aspx?function=" + type + "_loadValue&mode=single&share=share&i=" + encodeURIComponent(txtID), 900, 700);
    }
    var combobox = "<select class='selecttype'><option value='1'>Image</option></select>";
    var Object_Url = "<input class='Object_Url' type=\"text\" />";
    var Object_Note = "<textarea class='Object_Note' cols=\"30\" style=\"width:400px\" rows=\"2\"></textarea>";
    var STT = "<input class='STT' type=\"text\" value='0' />";
    var htmlEdit = "<img alt='' src='/Images/edit.png' />";
    var htmlAddAndCancel = "<img id='ObjectAdd' alt='' src='/Images/ico-add.gif' /><img id='ObjectCancel' alt='' src='/Images/delete_event.png' />";
    $(document).ready(function () {
        $(".AcctionDelete").live("click", function () {
            var check = confirm("Bạn có chắc xóa bản ghi này");
            if (!check) {
                return false;
            }
        });
        var i = 0;
        $("#btnThem").live("click", function () {

            if (i == 0) {
                $(".themmoi").append("<tr><th class='col1'>Loại</th><th class='col2'>Ảnh</th><th class='col3'>Ghi chú</th><th class='col4'>STT</th><th class='col5'></th></tr>");
            }
            i++;
            var newItem = "<tr><td class='col1' style=\"width:130px\">" + combobox + "</td><td class='' style=\"width:330px\"><input id='Object_Url_" + i + "' class='Object_Url' type=\"text\" style=\"width:300px\"  /><img src=\"/images/icons/folder.gif\" onclick=\"openInfo('/FileManager/index.html?field_name=Object_Url_" + i + "',900,700)\" style=\"cursor: pointer;\" /></td><td class='col3' style=\"width:430px\">" + Object_Note + "</td><td class='col4'>" + STT + "</td><td class=''><img class='ObjectDelete' alt='' src='/Images/delete_event.png' /></td></tr>";
            $(".themmoi").append(newItem);
        });
        $("#<%=btnAdd.ClientID %>").live("click", function () {

            var count = $(".Object_Url").length;
            if (count <= 0) {
                alert("Bạn chưa thêm mới file");
                return false;
            }
            else {
                var allJsoin = "";

                $(".Object_Url").each(function () {
                    var _Object_Type = $(this).parent().siblings(".col1").find("select").val();

                    var _Object_Url = $(this).val();
                    var _Object_Note = $(this).parent().siblings(".col3").find("textarea").val();
                    var _STT = $(this).parent().siblings(".col4").find("input").val();

                    if (_Object_Url.length > 0) {
                        allJsoin += '{"Object_Type":"' + _Object_Type + '","Object_Url":"' + _Object_Url + '","Object_Note":"' + _Object_Note + '","STT":"' + _STT + '"},';

                    }
                });
                var name = $("#txtName").val();
                $("#<%=hdfValue.ClientID %>").val(allJsoin);
                $("#<%=GalleryName.ClientID %>").val(name);


            }
        });
        $(".ObjectDelete").live("click", function () {
            $(this).parent().parent().remove();
        });
    });
</script>
<body>
    <input type="hidden" id="current" />
    <asp:HiddenField ID="hdfValue" runat="server" />
    <asp:HiddenField ID="GalleryName" runat="server" />
    <div class='objecttitle'>
        <div style="float: left">
            :&nbsp;</div>
        
    </div>
    
    
  
    <div>
        
      
    </div>
    <div id="albumimages">
    </div>
