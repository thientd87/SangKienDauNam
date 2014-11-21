<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsthread.aspx.cs" Inherits="Portal.newsthread" %>

<%@ Register Src="GUI/EditoralOffice/MainOffce/NewsAssign/newsassign.ascx" TagName="newsassign"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Thêm tin vào sự kiện</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:newsassign id="Newsassign1" runat="server">
        </uc1:newsassign></div>
    </form>
</body>
</html>
