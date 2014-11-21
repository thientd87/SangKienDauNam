<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListThread.aspx.cs" Inherits="Portal.ListThread" %>

<%@ Register Src="GUI/EditoralOffice/MainOffce/NewsThread/ListThread.ascx" TagName="ListThread"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Newsedit.css" rel="stylesheet" type="text/css" />
    <link href="/styles/common.css" rel="stylesheet" type="text/css" />
    <link href="/styles/autopro.css" rel="stylesheet" type="text/css" />
    <link href="/styles/pcal.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:ListThread ID="ListThread1" runat="server" />
    
    </div>
    </form>
</body>
</html>
