<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplatePreview.aspx.cs" Inherits="Portal.GUI.Administrator.AdminPortal.TemplatePreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .child-table
        {
	        border-left: solid 1px #ACACAC;
	        border-top: solid 1px #ACACAC;
	        border-collapse: collapse; 
        }
        .child-table td
        { 
	        border-right: solid 1px #ACACAC;
	        border-bottom: solid 1px #ACACAC;	
        }
        .formbody 
        {
            font-family: verdana; 
            font-size: .7em;     
        }
        .child-cell
        {
	        border-left: solid 1px #ACACAC;
	        border-top: solid 1px #ACACAC;
	        border-right: solid 1px #ACACAC;
	        border-bottom: solid 1px #ACACAC;
	        border-collapse: collapse;	
        }
    </style>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:table id="Table1" runat="server" Width="100%" HorizontalAlign="Center" BorderWidth="1px">
         </asp:table>         
    </div>
    </form>
</body>
</html>
