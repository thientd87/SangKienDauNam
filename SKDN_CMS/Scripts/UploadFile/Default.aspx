<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Scripts/UploadFile/_Default.aspx.cs" Inherits="DFISYS.Scripts.UploadFile._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script>

        function file_uploadFinish()
        {
	        if (window.parent)
	        {
		        window.parent.file_uploadFinish();
	        }
	        window.parent.document.getElementById('UploadFileContainer').style.display = 'none';
        }
        function window_cancelUpload()
        {
	        if (window.parent)
	        {
		        window.parent.hideModalPopup('UploadFileContainer');
		        //window.parent.file_uploadFinish();
	        }
	        return false;
        }
    </script>



</head>
<body>

    <form id="form1" runat="server">
        <table align="center" border="0" cellpadding="2" cellspacing="2" width="100%">
				<tr valign="bottom">
					<td align="center" valign="top">
						<table border="0" cellpadding="2" cellspacing="2" width="100%">
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
									<input type="button"  value="Đóng" onclick="window_cancelUpload();" />
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
    
    </form>
</body>
</html>

