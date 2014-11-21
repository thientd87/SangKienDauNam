<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GalleryMng.aspx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Tool.GalleryMng" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>     
    <script src="/Scripts/library.js" type="text/javascript"></script>
    <style type="text/css">
        .themmoi { border-bottom: 1px solid; margin-top: 25px; width: 860px; }
        .themmoi tr {}
        .themmoi td,.themmoi th { border-top: 1px solid; border-left: 1px solid;  padding: 5px; }
        .themmoi .col1 { width: 75px; }
        .themmoi .col2 { width: 165px; }
        .themmoi .col3 { width: 540px; }
        .themmoi .col3 *{ width:95%; }
        .themmoi .col4 { width: 55px; }
        .themmoi .col5 { border-right: 1px solid; width: 20px; }
        .themmoi .col4 input { width: 50px; }
        .ObjectDelete {  cursor:pointer; }
        .lstImages { width: 860px; }
        .lstImages .lstcol2 { width:75px;}
        .lstImages .lstcol3 {  width: 165px; }
        .lstImages .lstcol4 {  width: 540px; }
        .lstImages .lstcol4 *{ width:95%; }
        .lstImages .lstcol5 {  width: 55px; text-align: center; }
        .lstImages .lstcol6 { width: 20px; }
        .lstImages .lstcol7 {}
        .objecttitle {font-size: 18px; font-weight: bold; padding: 10px;}
    </style>
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
        var combobox = "<select class='selecttype'><option value='1'>Image</option><option value='2'>Video</option></select>";
        var Object_Url = "<input class='Object_Url' type=\"text\" />";
        var Object_Note = "<textarea class='Object_Note' cols=\"20\" rows=\"2\"></textarea>";
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
                var newItem = "<tr><td class='col1'>" + combobox + "</td><td class='col2'><input id='Object_Url_" + i + "' class='Object_Url' type=\"text\" /><img src=\"/images/icons/folder.gif\" onclick=\"chooseFile('avatar', 'Object_Url_" + i + "')\" style=\"cursor: pointer;\" /></td><td class='col3'>" + Object_Note + "</td><td class='col4'>" + STT + "</td><td class='col5'><img class='ObjectDelete' alt='' src='/Images/delete_event.png' /></td></tr>";
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

                        //                        var _Object_Type = Base64.encode($(this).parent().siblings(".col1").find("select").val());

                        //                        var _Object_Url = Base64.encode($(this).val());
                        //                        var _Object_Note = Base64.encode($(this).parent().siblings(".col3").find("textarea").val());
                        //                        var _STT = Base64.encode($(this).parent().siblings(".col4").find("input").val());

                        var _Object_Type = $(this).parent().siblings(".col1").find("select").val();

                        var _Object_Url = $(this).val();
                        var _Object_Note = $(this).parent().siblings(".col3").find("textarea").val();
                        var _STT = $(this).parent().siblings(".col4").find("input").val();

                        if (_Object_Url.length > 0) {
                            allJsoin += '{"Object_Type":"' + _Object_Type + '","Object_Url":"' + _Object_Url + '","Object_Note":"' + _Object_Note + '","STT":"' + _STT + '"},';
                        }
                    });
                    $("#<%=hdfValue.ClientID %>").val(allJsoin);
                    window.close();
                }
            });
            $(".ObjectDelete").live("click", function () {
                $(this).parents("tr").remove();
            });

            $("#btnThem").live("click", function () {
                window.close();
            });

        });
    </script>

   


</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="current" />
    <asp:HiddenField ID="hdfValue" runat="server" />
    <div id="MediaMng" news_id='<%=News_ID %>'>
    <div class='objecttitle'>
        <asp:Literal ID="lbl" runat="server"></asp:Literal>
    </div>
        <asp:GridView ID="GridView1" CssClass="lstImages" runat="server" AutoGenerateColumns="False" DataKeyNames="Object_ID"
            EnableModelValidation="True" OnRowDataBound="GridView1_RowDataBound" OnRowUpdating="GridView1_RowUpdating"
            OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="Object_ID" HeaderText="Object_ID" InsertVisible="False"
                    ReadOnly="True" SortExpression="Object_ID" Visible="False" >
                <HeaderStyle CssClass="lstcol1" />
                <ItemStyle CssClass="lstcol1" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Loại" SortExpression="Object_Type">
                    <EditItemTemplate>                        
                        <asp:DropDownList ID="cboObject_Type" runat="server">
                            <asp:ListItem Value="1">Image</asp:ListItem>
                            <asp:ListItem Value="2">Video</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Object_TypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="lstcol2" />
                    <ItemStyle CssClass="lstcol2" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ảnh" SortExpression="Object_Url">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtObject_Url" runat="server" Text='<%# Bind("Object_Url") %>'></asp:TextBox>
                        <asp:Literal ID="lbl" runat="server"></asp:Literal>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Image Height="100px" ID="Image1" ImageUrl='<%#Bind("Object_Url") %>' runat="server" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="lstcol3" />
                    <ItemStyle CssClass="lstcol3" Width="100px" Height="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ghi chú" SortExpression="Object_Note">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtObject_Note" runat="server" Text='<%# Bind("Object_Note") %>'
                            TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Object_Note") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="lstcol4" />
                    <ItemStyle CssClass="lstcol4" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="STT" SortExpression="STT">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStt" runat="server" Text='<%# Bind("STT") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("STT") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="lstcol5" />
                    <ItemStyle CssClass="lstcol5" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                            Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%--<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="Edit"></asp:LinkButton>--%>
                            <asp:ImageButton ID="ImageButton1" CausesValidation="False" CommandName="Edit" runat="server" ImageUrl="~/Images/edit.png" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="lstcol6" />
                    <ItemStyle CssClass="lstcol6" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <%--<asp:LinkButton ID="LinkButton4" runat="server" CssClass="AcctionDelete" CausesValidation="False"
                            CommandName="Delete" Text="Delete"></asp:LinkButton>--%>
                        <asp:ImageButton ID="ImageButton2" runat="server" CssClass="AcctionDelete" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/delete_event.png" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="lstcol7" />
                    <ItemStyle CssClass="lstcol7" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div>Bạn có thể thêm nhiều file.</div>
        <table cellpadding="0" cellspacing="0" class="themmoi">
            
        </table >
        <div>
            <input id="btnThem" type="button" value="Thêm" />
            <asp:Button ID="btnAdd" runat="server" Text="Lưu" OnClick="btnAdd_Click" />
            <asp:Button ID="btnClose" runat="server" Text="Đóng" />
        </div>
    </div>
   
    <div id="albumimages">
        

    </div>
    </form>
</body>
</html>
