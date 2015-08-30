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
            <img src="/Images/btnQuyDinhThamGia.png"/>
            <div class="content-quy-dinh-tham-gia scrollpanel no4">
                <asp:Literal runat="server" ID="ltrHuongDanDangKy"></asp:Literal>
                <%--<b>1.&emsp;	Tên chương trình: “Sáng kiến vì cộng đồng”</b><br/>
                <b>2.&emsp;	Đơn vị tổ chức: Tạp chí Cộng sản, Bộ Khoa học và Công nghệ, Trung ương Hội LHTN Việt Nam. </b><br/>
                <b>3.&emsp;	Hướng dẫn tham gia:<br/></b>
                &emsp;&emsp;<b>Cách 1</b>: Tham gia qua website chương trình: www.sangkienvicongdong.vn<br/>
                &emsp;&emsp;-&emsp;	Bước 1: Tải <b>“Bản đăng ký tham gia”</b>.<br/>
                &emsp;&emsp;-&emsp;	Bước 2: Điền thông tin bắt buộc vào <b>“Bản đăng ký tham gia”</b>.<br/>
                &emsp;&emsp;-&emsp;	Bước 3: Gửi <b>“Bản đăng ký tham gia”</b> và các tài liệu khác: video, thiết kế đồ họa, ảnh chụp…. (nếu có).<br/>
                &emsp;&emsp;<b>Cách 2</b>: Tham gia bằng cách gửi bài trực tiếp:<br/>
                &emsp;&emsp;-&emsp;	Bước 1: In “Bản đăng ký tham gia”.<br/>
                &emsp;&emsp;-&emsp;	Bước 2: Điền thông tin bắt buộc vào “Bản đăng ký tham gia”.<br/>
                &emsp;&emsp;-&emsp;	Bước 3: Gửi “Bản đăng ký tham gia” và các tài liệu khác: video, thiết kế đồ họa, ảnh chụp…. (nếu có) về Trung tâm Tình nguyện Quốc gia, 62 Bà Triệu, Hà Nội. Ngoài phong bì ghi rõ: “Đăng ký tham gia Chương trình Sáng kiến vì cộng đồng”.<br/>
                <b>4.&emsp;	Đối tượng tham gia:<br/></b>
                &emsp;&emsp;Là công dân Việt Nam hoặc người ngoại quốc đang sinh sống tại Việt Nam, tuổi từ 15 đến 35.<br/>
                <b>5.&emsp;	Tiêu chí lựa chọndự án:<br/></b>
                &emsp;&emsp;-&emsp;	Tính mới, sáng tạo: ý tưởng không trùng với bất cứ ý tưởng nào được tác giả khác công bố. Tại bất cứ thời điểm nào các tác giả khác chứng minh được vấn đề vi phạm bản quyền thì sáng kiến đó sẽ bị tước quyền tham gia, giải thưởng. <br/>
                &emsp;&emsp;-&emsp;	Dự án có tính khả thi nhằm giải quyết một hoặc nhiều vấn đề cụ thể đang tồn tại trong xã hội. Dự án mục tiêu mang lại lợi ích chung cho cả cộng đồng.<br/>
                &emsp;&emsp;-&emsp;	Chấp nhận các dự án đã hoặc đang triển khai quy mô nhỏ để áp dụng quy mô toàn quốc.<br/>
                <b>6.&emsp;	Thời gian đăng ký tham gia:<br/></b>
                &emsp;&emsp;-&emsp;	22/06/2015 đến hết 23:59 ngày 31/07/2015.<br/>
                <b>7.&emsp;	Các giai đoạn cuộc thi:<br/></b>
                &emsp;&emsp;-&emsp;	Giai đoạn 1 (22/06/2015 – 31/07/2015): Gửi dự án tham gia<br/>
                &emsp;&emsp;-&emsp;	Giai đoạn 2 (8/2015 – 11/2015): <br/>
                &emsp;&emsp;&emsp;	Thành lập hội đồng xét duyệt các dự án.<br/>
                &emsp;&emsp;&emsp;	Hội thảo kết hợp Sàn giao dịch các sáng kiến. <br/>
                &emsp;&emsp;-&emsp;	Giai đoạn 3:<br/>
                &emsp;&emsp;&emsp;	Lễ tuyên dương<br/>
                <b>8.&emsp;	Các hạng mục giải thưởng:<br/></b>
                &emsp;&emsp;-&emsp;	01 Giải nhất trị giá: 30,000,000VND và ngân sách thực hiện dự án dưới sự giám sát của BTC.<br/>
                &emsp;&emsp;-&emsp;	02 Giải nhì: 20,000,000VND.<br/>
                &emsp;&emsp;-&emsp;	02 Giả ba: 15,000,000 VNĐ.<br/>
                &emsp;&emsp;-&emsp;	Top 10 ý tưởng xuất sắc: kỷ niệm chương.<br/>
                &emsp;&emsp;-&emsp;	Top 50 ý tưởng xuất sắc: kỷ niệm chương.<br/>
                <b>9.&emsp;	Một số quy định khác<br/></b>
                &emsp;&emsp;•&emsp;	Bài dự thi bằng Tiếng Việt. <br/>
                &emsp;&emsp;•&emsp;	BTC có toàn quyền sử dụng toàn bộ nội dung, hình ảnh, bài viết tham gia chương trình cho các mục đích truyền thông khác trong quá trình cuộc thi diễn ra và cả khi đã kết thúc cuộc thi mà không cần phải xin phép trước hay trả phí cho nhóm hoặc thành viên đăng ký.<br/>
                &emsp;&emsp;•&emsp;	Người tham dự phải tự chịu trách nhiệm về tính chính xác của các thông tin cá nhân đăng kí.<br/>
                &emsp;&emsp;•&emsp;	Quyết định của BTC là quyết định cuối cùng. Mọi tranh chấp, khiếu nại, thắc mắc, về quyết định của BTC đều không có giá trị.<br/>
                &emsp;&emsp;•&emsp;	Tất cả người đăng kí tham gia chương trình phải đồng ý với tất cả điều kiện của BTC đưa ra.<br/>
                &emsp;&emsp;•&emsp;	Dự án đạt giải nhất được triển khai dưới sự giám sát toàn quyền của BTC. Nhóm dự án đóng vai trò tư vấn.<br/>
                &emsp;&emsp;•&emsp;	Nội dung sáng kiến và bình luận của những người tham gia phải văn minh, lịch sự, phù hợp với văn hóa Việt Nam hoặc không vi phạm các quy định pháp luật hiện hành của Nhà nước nước CHXHCN Việt Nam.<br/>
--%>

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
           
        });
        
    </script>
    <style>
       
    </style>
</asp:Content>

