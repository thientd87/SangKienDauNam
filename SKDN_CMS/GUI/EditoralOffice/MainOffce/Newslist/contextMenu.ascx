<%@ Control Language="C#" AutoEventWireup="true" Codebehind="contextMenu.ascx.cs"
	Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Newslist.contextMenu" %>
<ul id="contextmenu">
	<li><a rel="colorbox" class="xemtruoc" href="javascript:void()">Xem trước</a></li>
	<li><a class="suanoidung" onclick="suanoidung(); return false;" href="javascript:void()">Sửa nội dung</a></li>
	<li><a class="gobo" onclick="gobo(); return false;" href="javascript:void()">Gỡ bỏ</a></li>     
	<li><a class="xemthongtinbandocgui" onclick="xemthongtinbandocgui(); return false;" href="javascript:void()" style="display:none">Xem thông tin bạn đọc gửi</a></li>
	<li><a class="xemnhanxet" onclick="xemnhanxet(); return false;" href="javascript:void()">Xem nhận xét</a></li>
	<li><a class="xoatam" onclick="xoatam(); return false;" href="javascript:void()">Xóa tạm</a></li>
	<li><a class="guilen" onclick="guilen(); return false;" href="javascript:void()">Gửi lên</a></li>
	<li><a class="trave" onclick="trave(); return false;" href="javascript:void()">Trả về</a></li>
	<li><a class="xuatban" onclick="xuatban(); return false;" href="javascript:void()">Xuất bản</a></li>
	<li><a class="xoatam" onclick="xoathat(); return false;" href="javascript:void()">Xóa</a></li>
	
</ul>

<script type="text/javascript"> var linkpreview = '/preview/default.aspx?news='; var isSendDirectly = <%=isSendDirectly.ToString().ToLower() %>; </script>

