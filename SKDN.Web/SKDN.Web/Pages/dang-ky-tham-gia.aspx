<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dang-ky-tham-gia.aspx.cs" Inherits="SKDN.Web.Pages.dang_ky_tham_gia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/jscroll/jquery.mousewheel-3.1.3.js"></script>
    <script src="/Scripts/jscroll/jquery.scrollpanel-0.5.0.js"></script>
    <div class="dang-ky-tham-gia-wrapper">
        <div class="form-dang-ky">
            <img src="/Images/btnDangKyThamGia.png" style="float: left"/>
            <a href="/Form_Dang_Ky.docx" class="btn-download">Tải bản đăng ký</a>
            <br/>
            <br/>
             <br/>
            <div class="control-group">
				<div class="control-label">Tên dự án</div>
				<div class="controls">
					<input runat="server" id="txt_Name" type="text"/>
				</div>
			</div>
            <div class="control-group">
				<div class="control-label">Tên nhóm</div>
				<div class="controls">
					<input runat="server" id="txt_group" type="text"/>
				</div>
			</div>
            <div class="control-group">
				<div class="control-label">Email</div>
				<div class="controls">
					<input runat="server" id="txtEmail" type="text" />
				</div>
			</div>
            <div class="control-group">
				<div class="control-label">Điện thoại</div>
				<div class="controls">
					<input runat="server" id="txtTel" type="text"  />
				</div>
			</div>
             <div class="control-group">
				<div class="control-label">File đính kèm</div>
				<div class="controls">
					<input runat="server" id="txtFile" type="file"  /><span style="font-size: 10px"><i>(Dung lượng không quá 20MB)</i></span>
				</div>
			</div>
            <div class="control-group">
                <asp:Button runat="server" runat="server" ID="btnNopBai" CssClass="btn-nopbai" Text="Nộp Bài" OnClick="btnNopBai_Click"/>
            </div>
            <span style="font-size: 11px">Lưu ý: Mỗi dự án tham gia phải bao gồm "Bản đăng kí tham gia" (bắt buộc) theo mẫu của chương trình và các tài liệu bổ sung, minh họa của nhóm (Nếu có)</span>
        </div>
        <div class="quy-dinh-tham-gia">
            <img src="/Images/btnQuyDinhThamGia.png" style="margin-left: 30px"/>
            <div class="content-quy-dinh-tham-gia scrollpanel no4">
                <b>1.&emsp; Tên chương trình: “Sáng kiến đầu năm”<br/></b>
                <b>2.&emsp;	Đơn vị tổ chức: Đài Truyền Hình Việt Nam (VTV) và Tạp Chí Cộng sản<br/></b>
                <b>3.&emsp;	Đối tượng tham gia:<br/></b>
                &emsp;&emsp;-&emsp;Là công dân Việt Nam hoặc người ngoại quốc đang sinh sống tại Việt Nam. Không giới hạn độ tuổi.<br/>
                <b>4.&emsp;	Tiêu chí lựa chọn dự án:<br/></b>
                &emsp;&emsp;-&emsp; Ý tưởng đơn giản, khả thi, sáng tạo nhằm giải quyết một hoặc nhiều vấn đề cụ thể đang tồn tại trong xã hội và  mang lại lợi ích chung cho cả cộng đồng.<br/>
                &emsp;&emsp;-&emsp;Chấp nhận các dự án đã hoặc đang triển khai quy mô nhỏ để áp dụng quy mô toàn quốc.<br/>
                <b>5.&emsp;	Thời gian đăng ký tham gia:<br/></b>
                &emsp;&emsp;-&emsp;	Từ 00:00 ngày 01/12/2014 đến hết 23:59 ngày 31/12/2014<br/>
                <b>6.&emsp;	Các giai đoạn cuộc thi<br/></b>
                &emsp;&emsp;-&emsp;	Vòng 1 (01/12/2014 – 31/12/2014): Gửi dự án tham gia<br/>
                &emsp;&emsp;Từ 01/12/2014 đến 31/12/2014: Các thí sinh và các nhóm dự thi gửi đề xuất ý tưởng của mình lên website sangkiendaunam.vn/dangkithamgia. Sau khi gửi dự án tham gia, tất cả dự án sẽ được cập nhật lên website và kết quả sau vòng một sẽ được thông báo đến các nhóm dự thi.
                <br/>
               &emsp;&emsp; Bài dự thi phải bao gồm phần <b><u>đơn đăng ký bắt buộc</u></b> (tải về từ trang web của chương trình) và các sản phẩm khác: hình ảnh minh họa, video thuyết trình… của các nhóm (nếu có)<br/>
               &emsp;&emsp; -&emsp;	Vòng 2 (01/01/2015 – 10/01/2015): Vòng sơ loại<br/>
                &emsp;&emsp;10 dự án tốt nhất vòng 1 sẽ tham gia buổi thuyết trình để chọn 03 dự án xuất sắc nhất vào chung kết. BTC sẽ hỗ trợ chi phí di chuyển đối với các nhóm dự thi ngoài khu vực Hà Nội.<br/>
                &emsp;&emsp;-&emsp;	Vòng 3: Đêm chung kết<br/>
                &emsp;&emsp;Top 3 ý tưởng xuất sắc nhất sẽ thuyết trình về ý tưởng và chi tiết triển khai dự án với ban giám khảo. Chương trình được ghi hình và phát sóng trên VTV. BTC sẽ hỗ trợ chi phí di chuyển đối với các nhóm dự thi ngoài khu vực Hà Nội.<br/>
                <b>7.&emsp;	Các hạng mục giải thưởng:<br/></b>
                &emsp;&emsp;-&emsp;	01 Giải nhất đêm chung kết trị giá: 30,000,000VND và 300,000,000VND ngân sách thực hiện dự án dưới sự giám sát của BTC.<br/>
                &emsp;&emsp;-&emsp;	02 Giải nhì đêm chung kết: 15,000,000VND<br/>
                &emsp;&emsp;-&emsp;	Top 10 ý tưởng xuất sắc: kỉ niệm chương<br/>
                &emsp;&emsp;-&emsp;	Top 50 ý tưởng xuất sắc: kỉ niệm chương<br/>
                <b>8.&emsp;	Một số quy định khác<br/></b>
                &emsp;&emsp;•&emsp;	BTC có toàn quyền sử dụng toàn bộ nội dung, hình ảnh, bài viết tham gia chương trình cho các mục đích truyền thông khác trong quá trình cuộc thi diễn ra và cả khi đã kết thúc cuộc thi mà không cần phải xin phép trước hay trả phí cho nhóm hoặc thành viên đăng ký.<br/>
                &emsp;&emsp;•&emsp;	Người tham dự phải tự chịu trách nhiệm về tính chính xác của các thông tin cá nhân đăng kí.<br/>
                &emsp;&emsp;•&emsp;	Quyết định của BTC là quyết định cuối cùng. Mọi tranh chấp, khiếu nại, thắc mắc, về quyết định của BTC đều không có giá trị.<br/>
                &emsp;&emsp;•&emsp;	Tất cả người đăng kí tham gia chương trình phải đồng ý với tất cả điều kiện của BTC đưa ra.<br/>
                &emsp;&emsp;•&emsp;	Dự án đạt giải nhất được triển khai dưới sự giám sát toàn quyền của BTC. Nhóm dự án đóng vai trò tư vấn.<br/>
                &emsp;&emsp;•&emsp;	Nội dung ý tưởng và bình luận của những người tham gia phải văn minh, lịch sự, phù hợp với văn hóa Việt Nam hoặc không vi phạm các quy định pháp luật hiện hành của Nhà nước nước CHXHCN Việt Nam.<br/>

            </div>
            <input type="checkbox"  id="checkbox1" class="checkbox" />
            <label for="checkbox1">Tôi đã đọc và đồng ý</label> 
            
          
        </div>
        <div class="clearfix"></div>
    </div>
    <script type="text/javascript">
        $(document).ready(function()
        {
            $('.content-quy-dinh-tham-gia').scrollpanel({
               
            });
            //$('#checkbox1').click(function () {
            //    $(".btn-download").toggle(this.checked);
            //});
        });
        
    </script>
    <style>
       
    </style>
</asp:Content>

