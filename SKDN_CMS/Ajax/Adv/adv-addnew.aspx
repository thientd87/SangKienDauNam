<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adv-addnew.aspx.cs" Inherits="AddIns.ajax.adv_addnew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .left{float:left}
        #adv_add { width: 800px; height: 420px; }
        #adv_add h3 { background-color: #E5E5E5; margin: 0 0 8px 0; width: 97%; padding: 5px; }
        #adv_add label { width: 120px; display: block; float: left; }
        #adv_add input[type='text'] { float: left; clear: right; width: 250px; }
        #adv_add select { float: left; clear: right; width: 150px; }
        #adv_add ul { margin: 0; padding: 0; }
        #adv_add li { float: left; padding: 5px 0; width: 100%; }
        span.require{color:Red}
        #cblPages{height:100px;overflow-x:hidden;overflow-y:scroll;display:block;width:250px;float:left;border: 1px solid #B6BFC6; padding: 5px; -moz-border-radius: 3px; -webkit-border-radius: 3px; margin-right: 4px; font-size: 11px; }        
        #cblPages label{float:none;width:200px !important}
        #cblPages input, #adv_type input{float:left;margin-right:5px}
        #adv_type label{width:65px}
        .ml20{margin-left:20px}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="adv_add">
            <h3>Quản lý quảng cáo</h3>
            <ul style="list-style-type: none">
            <li>
                <div style="float:left">
                   <label>Trang</label>
                    <asp:CheckBoxList ID="cblPages" runat="server" DataTextField="Cat_Name" DataValueField="Cat_ID">
                    </asp:CheckBoxList>
                </div>
                <div style="float:left;margin-left:15px">
                    <span class="left">Vị trí</span>
                    <asp:DropDownList ID="ddlPos" runat="server" DataTextField="PosName" DataValueField="PosID" CssClass="ml20">
                    </asp:DropDownList>
                </div>
                <br />
            </li>
            <li>
                <label>Tên quảng cáo</label><asp:TextBox ID="adv_name" ClientIDMode="Static" runat="server"></asp:TextBox><span class="require">*</span><br />
            </li>
            <li>
                <label>Loại quảng cáo</label>
                <asp:RadioButtonList ID="adv_type" ClientIDMode="Static" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">Ảnh</asp:ListItem>
                    <asp:ListItem Value="2">Flash</asp:ListItem>
                    <asp:ListItem Value="3">Box nhúng</asp:ListItem>
                </asp:RadioButtonList>
                <br />
            </li>
            <li>
                <label>Đường dẫn</label><asp:TextBox ID="txtSelectedFile"  ClientIDMode="Static" Width="600" runat="server"></asp:TextBox><span class="require">*</span>&nbsp;<img src="/images/icons/folder.gif" onclick="chooseFile('avatar', 'txtSelectedFile');" style="cursor:pointer"/></li>
            <li style="display:none">
                <label>Mã nhúng</label><asp:TextBox ID="adv_embed" ClientIDMode="Static"  runat="server" TextMode="MultiLine" Width="600" ></asp:TextBox><br /></li>
            <li>
                <label>Link</label><asp:TextBox ID="adv_link"  ClientIDMode="Static" runat="server" Width="600"></asp:TextBox></li>
            <li>
                <label>Mô tả</label><asp:TextBox ID="adv_description"  ClientIDMode="Static" runat="server" Width="600" TextMode="MultiLine"></asp:TextBox><br />
            </li>
            <li>
                <label>Ngày bắt đầu</label><asp:TextBox ID="adv_startdate" ClientIDMode="Static"  runat="server"></asp:TextBox><span class="require">*</span><img src="/images/calendar.png" /></li>
            <li>
                <label>Ngày kết thúc</label><asp:TextBox ID="adv_enddate" ClientIDMode="Static"  runat="server"></asp:TextBox><span class="require">*</span><img src="/images/calendar.png" /></li>
            <li>
                <label>Sắp xếp</label><asp:TextBox ID="adv_order" ClientIDMode="Static"  runat="server" Text="0"></asp:TextBox></li>
            <li>
                <%--<label>Hoạt động</label><asp:CheckBox ID="adv_isActive" ClientIDMode="Static"  runat="server" Checked="true" /></li>
            <li>--%>
                <label>Tự động chuyển</label><asp:CheckBox ID="adv_isRotate" ClientIDMode="Static" runat="server" Checked="false" /></li>
            <li>
                <label>&nbsp;</label>
                    <asp:Button runat="server" CssClass="button white"
                        Text="Lưu" ID="btnSave"/> &nbsp; 
                        &nbsp; 
                    <asp:Button 
                        runat="server" CssClass="button white"
                        Text="Xóa" ID="btnDelete" /> &nbsp; 
                    <input class="button white" type="button" onclick="$.facebox.close()" 
                        value="Đóng"/>
            </li>
        </ul>
        </div>
    <asp:HiddenField ID="hidAdvId" runat="server" />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            var var_name = $("input[name='adv_type']:checked").val();
            if (var_name == 3) {
                $('#txtSelectedFile').parent().fadeOut('slow');
                $('#adv_embed').parent().fadeIn('slow');
            }
            else {
                $('#txtSelectedFile').parent().fadeIn('slow');
                $('#adv_embed').parent().fadeOut('slow');
            }
        });



        $(function () {
            //console.log($('#hidAdvId').val());
            chooseFile = function (type, txtID) {
                txtID = document.getElementById(txtID).value;
                openpreview("/GUI/EditoralOffice/MainOffce/FileManager/default.aspx?function=" + type + "_loadValue&mode=single&share=share&i=" + encodeURIComponent(txtID), 900, 700);
            };

            avatar_loadValue = function (arrImage) {
                if (arrImage.length > 0) {
                    arrImage[0] = arrImage[0].substr(arrImage[0].indexOf('Images/Uploaded/'));
                    server_getElementById('txtSelectedFile').value = arrImage[0];
                }
            };

            $('#adv_type input').change(function () {
                if ($(this).val() == 3) {
                    $('#txtSelectedFile').parent().fadeOut('slow');
                    $('#adv_embed').parent().fadeIn('slow');
                }
                else {
                    $('#txtSelectedFile').parent().fadeIn('slow');
                    $('#adv_embed').parent().fadeOut('slow');
                }
            });

            $('#adv_startdate, #adv_enddate').simpleDatepicker();
            ajaxCallback = function (_url, params, callback) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: (params),
                    url: _url,
                    success: function (response) {
                        var result = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;

                        if ($.isFunction(callback)) {
                            callback(result);
                        }
                    },
                    error: function (xhr, status) {
                        alert(status);
                    }
                });
            }

            $('#save').click(function () {
                if ($('#ddlPos').val() == 0) {
                    alert('Bạn chưa chọn vị trí');
                    return false;
                }
                if ($('#adv_name').val() == '') {
                    alert('Bạn chưa nhập tên quảng cáo');
                    return false;
                }
                if ($('#txtSelectedFile').val() == '' && $('#adv_type input:checked').val() != 3) {
                    alert('Chưa có đường dẫn đến file quảng cáo');
                    return false;
                }
                if ($('#adv_startdate').val() == '') {
                    alert('Bạn chưa chọn ngày bắt đầu');
                    return false;
                }
                if ($('#adv_enddate').val() == '') {
                    alert('Bạn chưa chọn ngày kết thúc');
                    return false;
                }

                var fields = $('#adv_add *').serializeArray();
                var tmp = "{";
                $(fields).each(function (i, field) {
                    //                    if (field.id != 'adv_startdate' && field.id != 'adv_enddate')
                    //                        tmp += "'" + field.name + "':'" + encodeURIComponent(field.value) + "',";
                    //                    else
                    tmp += "'" + field.name + "':'" + field.value + "',";
                    //console.log(field);
                });

                tmp = tmp.slice(0, -1);

                if (tmp.indexOf('adv_isRotate') == -1)
                    tmp = tmp + ",\'adv_isRotate\': \'false\'";
                if (tmp.indexOf('adv_isActive') == -1)
                    tmp = tmp + ",\'adv_isActive\':\'false\'";

                var _pages = new Array();
                var _selectedItems = $('#cblPages').find('input:checkbox:checked');

                if (_selectedItems.length > 0) {
                    _selectedItems.each(function (index, value) {
                        //console.log($(this).parent().attr('cid'));
                        _pages.push($(this).parent().attr('cid'));
                    });
                }

                tmp += ",\'adv_pages\':\'" + _pages + "\'";

                var adv_id = $('#hidAdvId').val();
                if (adv_id != "") {
                    tmp += ",\'adv_id\':\'" + adv_id + "\'";
                }

                tmp += "}";

                //console.log(tmp);

                if (adv_id == "") {
                    ajaxCallback('/AddInService.asmx/AddNewAdv', tmp, function (result) {
                        if (result.ok == false)
                            alert(result.message);
                        else {
                            $.ajax({
                                type: "GET",
                                data: ({ page: _pages[0], pos: $('#<%=ddlPos.ClientID %>').val() }),
                                url: '/ajax/ListAdv.aspx',
                                success: function (response) {
                                    $('#adv_container').html(response);
                                    $(document).trigger('close.facebox');

                                    $('.ddlPages').val(_pages[0]);
                                    $('.ddlPoss').val($('#<%=ddlPos.ClientID %>').val());
                                },
                                error: function (xhr, status) {
                                    alert(status);
                                }
                            });
                        }
                    });
                }
                else {
                    ajaxCallback('/AddInService.asmx/UpdateAdv', tmp, function (result) {
                        if (result.ok == false)
                            alert(result.message);
                        else {
                            $.ajax({
                                type: "GET",
                                data: ({ page: _pages[0], pos: $('#<%=ddlPos.ClientID %>').val() }),
                                url: '/ajax/ListAdv.aspx',
                                success: function (response) {
                                    $('#adv_container').html(response);
                                    $(document).trigger('close.facebox');

                                    $('.ddlPages').val(_pages[0]);
                                    $('.ddlPoss').val($('#<%=ddlPos.ClientID %>').val());
                                },
                                error: function (xhr, status) {
                                    alert(status);
                                }
                            });
                        }
                    });
                }
                //

            });

            $('#close').click(function () {
                $(document).trigger('close.facebox');
            });

        });
    </script>
</body>
</html>
