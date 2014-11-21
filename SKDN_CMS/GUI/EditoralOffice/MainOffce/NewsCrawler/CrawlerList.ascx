<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CrawlerList.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.NewsCrawler.CrawlerList" %>
<script src="/Scripts/jquery.tips.js" type="text/javascript"></script>
<style>
    .tip { color: #000; background: #fff; display: none; padding: 10px; position: absolute; z-index: 1000; -webkit-border-radius: 3px; -moz-border-radius: 3px; border-radius: 3px; width:500px;border:solid 3px #999;font-size:12pt;font-family:Times New Roman}
    input[type='button'], input[type='submit'] { background-position: 0% 0%; padding: 2px 8px 4px; height: 28px; border: 1px solid #ccc; color: #000; -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; white-space: nowrap; vertical-align: middle; cursor: pointer; overflow: visible; outline: 0 none; background-image: -webkit-gradient(linear,left top,left bottom,from(#ffffff),to(#efefef)); background-color: #f6f6f6; background-repeat: repeat; background-attachment: scroll; margin-right: 10px; }
    input[type='button']:hover, input[type='submit']:hover { background-position: 0% 0%; border-color: #999; outline: 0; -moz-box-shadow: 0 0 3px #999; -webkit-box-shadow: 0 0 3px #999; -khtml-box-shadow: 0 0 3px #999; box-shadow: 0 0 3px #999; -ms-filter: "progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#EFEFEF)"; background: -webkit-gradient(linear,left top,left bottom,from(#ffffff),to(#ebebeb)); background: -moz-linear-gradient(top,  #ffffff,  #ebebeb); background-color: #f3f3f3; background-repeat: repeat; background-attachment: scroll; }
    div.bottom{float:left;margin:5px 0 0 0;border:solid 1px #DFDFDF;padding:5px 0;width:100%}
    .delete_selected{margin-bottom:5px}
    #tab_ctl15_ctl00_grdList tr:hover{background-color:#FFFFCC}
</style>
<h3 style="background-color: #DFDFDF;margin: 5px 0;padding: 5px;">Danh sách tin chứng khoán chờ xử lý</h3>
<input type="button" class="delete_selected" value="Xóa tin đã chọn" style="clear:both"/>
<asp:GridView ID="grdList" runat="server" AllowPaging="true" AlternatingRowStyle-CssClass="grdAlterItem" AutoGenerateColumns="False" EmptyDataText="Không có bản ghi nào" HeaderStyle-CssClass="grdHeader"
	PagerSettings-Mode="Numeric" DataKeyNames="ID" PagerStyle-HorizontalAlign="Center" PageSize="100" RowStyle-CssClass="grdItem" Width="100%" OnRowCommand="grdList_RowCommand" OnPageIndexChanging="grdList_PageIndexChanging">
	<PagerSettings Mode="NumericFirstLast" />
	<Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <input type="checkbox" id="selectAll"/>
            </HeaderTemplate>
            <ItemTemplate>
                <input type="checkbox" value="<%#Eval("ID") %>"/>
            </ItemTemplate>
            <HeaderStyle Width="4%" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
		<asp:TemplateField HeaderText="Tiêu đề">
			<HeaderStyle Width="40%" />
			<ItemTemplate>
                &nbsp;&nbsp;<a href="/GUI/EditoralOffice/MainOffce/NewsCrawler/ImagesSuggestion.aspx?id=<%#Eval("ID") %>&source=<%#Eval("News_Source")%>" rel="fb"><%#Eval("News_Title")%></a>&nbsp;<a target="_blank" href="<%#Eval("News_Source")%>"><img border="0" src="http://gafin.vn/Images/url_icon.gif"></a>
			</ItemTemplate>
            <ItemStyle CssClass="item" />
		</asp:TemplateField>
        <%--<asp:TemplateField HeaderText="Nguồn">
		    <ItemTemplate>
				<center>
					<%# Eval("News_Source") != null && Eval("News_Source").ToString().IndexOf('/') != -1 ? "<a href=\"" + Eval("News_Source") + "\" target=\"_blank\">" + Eval("News_Source").ToString().Split('/')[2] + "</a>" : ""%>
				</center>
			</ItemTemplate>
			<ItemStyle Width="10%"/>
		</asp:TemplateField>--%>
        <asp:TemplateField HeaderText="Nguồn">
		    <ItemTemplate>
				<center>
					<%# Eval("SourceName")%>
				</center>
			</ItemTemplate>
			<ItemStyle Width="10%"/>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Ngày XB gốc">
		    <ItemTemplate>
				<center>
                    <%#Eval("CrawledDate", "{0:dd/MM/yyyy - HH:mm}")%>
				</center>
			</ItemTemplate>
			<ItemStyle Width="12%"/>
		</asp:TemplateField>
        <asp:TemplateField HeaderText="Ngày copy">
		    <ItemTemplate>
				<center>
                    <%#Eval("CopyTime", "{0:dd/MM/yyyy - HH:mm}")%>
				</center>
			</ItemTemplate>
			<ItemStyle Width="12%"/>
		</asp:TemplateField>
        <asp:TemplateField HeaderText="Copy">
		    <ItemTemplate>
				<a href="/GUI/EditoralOffice/MainOffce/NewsCrawler/ImagesSuggestion.aspx?id=<%#Eval("ID") %>&source=<%#Eval("News_Source")%>" rel="fb"><img src="/images/page_copy.png" border="0" /></a>
			</ItemTemplate>
			<ItemStyle Width="5%" HorizontalAlign="Center"/>
		</asp:TemplateField>
	</Columns>
	<RowStyle CssClass="grdItem" />
	<PagerStyle HorizontalAlign="Center" CssClass="paging"/>
	<HeaderStyle CssClass="grdHeader" />
	<AlternatingRowStyle CssClass="grdAlterItem" />
</asp:GridView>
<br />
<input type="button" class="delete_selected" value="Xóa tin đã chọn"/>

<script type="text/javascript">
    $(function () {
        $('#selectAll').change(function () {
            $('#<%=grdList.ClientID %> :input:checkbox').attr('checked', $(this).attr('checked'));
        });

        $('.delete_selected').click(function () {
            var arr = new Array();

            var selected = $('#<%=grdList.ClientID %> :input:checkbox:checked');
            if (selected.length > 0) {
                if (confirm('Bạn có muốn xóa những tin đã chọn')) {
                    selected.each(function (index, value) {
                        arr.push($(value).val());
                    });

                    $.ajax({
                        type: "POST",
                        url: "/Ajax/ajax.aspx",
                        data: "SelectedItems=" + arr,
                        success: function (msg) {
                            selected.each(function (index, value) {
                                var _p = $(value).parents().get(1);

                                $(_p).fadeOut(300, function () {
                                    $(this).remove();
                                });
                            });
                        }
                    });
                }
            }
            else
                alert('Bạn chưa chọn tin để xóa');

        });

        $('.item a').ttips();
        $('a[rel=fb]').facebox();
    });
</script>
