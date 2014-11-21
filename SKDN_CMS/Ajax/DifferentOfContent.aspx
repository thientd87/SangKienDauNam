<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DifferentOfContent.aspx.cs" Inherits="DFISYS.Ajax.DifferentOfContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .button{
    background-color: #4E8BCE;
    border-color: #D8DFEA #0E1F5B #0E1F5B #D8DFEA;
    border-style: solid;
    border-width: 1px;
    color: #FFFFFF;
    cursor: pointer;
    font-family: 'Lucida Grande',Tahoma,Verdana,Arial,sans-serif;
    font-size: 11px;
    font-weight: bold;
    margin-right: 2px;
    padding: 4px 18px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="overflow:hidden; padding:10px; width:600px; font-size:12px; font-family:Arial">
        <div style="overflow:hidden; padding-bottom:5px"><b>Tiêu đề:</b> <asp:Literal runat="server" ID="ltrTitle" ></asp:Literal></div>
        <div style="overflow:hidden; padding-bottom:5px"><b>Tóm tắt:</b> <asp:Literal runat="server" ID="ltrInit" ></asp:Literal></div>
        <div style="overflow:hidden; padding-bottom:5px"><b>Nội dung:</b><br /><br /><div style="overflow-y:scroll; height:300px; border: solid 1px #333; padding:5px "><asp:Literal runat="server" ID="ltrContent" ></asp:Literal></div></div>    

        <div style="">
            <input type="button" id="btnUpdate" onclick="GoToEditPublisheNews('<%=Request.QueryString["nid"] %>')" value="Tiếp tục cập nhật" class="button"/>
        </div>

    </div>
    <script type="text/javascript" language="javascript">
        function GoToEditPublisheNews(newsID) {
            window.parent.location.href = "/office/editpublist,publishedlist/" + newsID + ".aspx?source=/office/publishedlist.aspx";
        }
    </script>
    </form>
</body>
</html>
