<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentNewsReturn.aspx.cs" Inherits="Portal.CommentNewsReturn" %>

<%@ Register Src="GUI/EditoralOffice/MainOffce/Newslist/ShowCommentReturn.ascx" TagName="ShowCommentReturn"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comment bài viết trả về</title>
    <link href="/styles/Newsedit.css" rel="stylesheet" type="text/css" />
    <link href="/styles/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:ShowCommentReturn id="ShowCommentReturn1" runat="server">
        </uc1:ShowCommentReturn></div>
    </form>
</body>
</html>
