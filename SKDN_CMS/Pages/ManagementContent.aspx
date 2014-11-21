<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagementContent.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="DFISYS.ManagementContent" %>
<%@ Register Src="~/GUI/Share/AdminTab.ascx" TagName="AdminTab" TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta name="ROBOTS" content="NOINDEX,NOFOLLOW" />
    <title>Dolly Lens CMS | Admin Dashboard</title>
    <script type="text/javascript">        ie = document.all ? true : false;</script>
      <script src="/styles/metronic/plugins/jquery-1.10.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/facebox.js" type="text/javascript"></script>
    <script src="/Scripts/cms.jquery.autosave.js" type="text/javascript"></script>

    <link href="/Styles/theme/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/js/superfish.js" type="text/javascript"></script>
    <script src="/Scripts/js/jquery.cookie.js" type="text/javascript"></script>
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
   <link href="/styles/metronic/plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css"/>
   <link href="/styles/metronic/plugins/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" type="text/css"/>
   <link href="/styles/metronic/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
   <link href="/styles/metronic/css/style-metro.css" rel="stylesheet" type="text/css"/>
   <link href="/styles/metronic/css/style.css" rel="stylesheet" type="text/css"/>
   <link href="/styles/metronic/css/style-responsive.css" rel="stylesheet" type="text/css"/>
   <link href="/styles/metronic/css/themes/default.css" rel="stylesheet" type="text/css" id="style_color"/>
   <link href="/styles/metronic/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css"/>
   <!-- END GLOBAL MANDATORY STYLES -->
   <!-- BEGIN PAGE LEVEL STYLES --> 
   <link rel="stylesheet" type="text/css" href="/styles/metronic/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css"/>
   <link href="/styles/metronic/plugins/gritter/css/jquery.gritter.css" rel="stylesheet" type="text/css"/>
 
</head>
<body  class="">
    <form id="RenderTable" runat="server" enctype="multipart/form-data">
       
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc2:AdminTab ID="tab" runat="server"></uc2:AdminTab>
   
    <script type="text/javascript" language="javascript" src="/scripts/library.js"></script>
    <script type="text/javascript" language="javascript" src="/scripts/Grid.js"></script>
    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
                    src="/Scripts/DatePicker/ipopeng.htm" scrolling="no" frameborder="0" style="visibility: visible;
                    z-index: 999; position: absolute; top: -500px; left: -500px;"></iframe>
                    <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
  <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
   <!-- BEGIN CORE PLUGINS -->
    <script src="/styles/metronic/scripts/app.js" type="text/javascript"></script>
   <script src="/styles/metronic/plugins/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>
   <!-- IMPORTANT! Load jquery-ui-1.10.1.custom.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->
   <script src="/styles/metronic/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.js" type="text/javascript"></script>    
   <script src="/styles/metronic/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
   <script src="/styles/metronic/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="/styles/metronic/plugins/jquery.blockui.min.js" type="text/javascript"></script>  
   <script src="/styles/metronic/plugins/jquery.cookie.min.js" type="text/javascript"></script>
   <script src="/styles/metronic/plugins/uniform/jquery.uniform.js" type="text/javascript" ></script>
   <script src="/styles/metronic/plugins/gritter/js/jquery.gritter.js" type="text/javascript"></script>
   <script src="/styles/metronic/scripts/ui-jqueryui.js"></script>
   <script>
       jQuery(document).ready(function () {
           App.init(); // initlayout and core plugins
          // UIJQueryUI.init();
           $("ul.sub-menu li.active").each(function () {
               var liNodeParent = $(this).parent().parent();
               liNodeParent.addClass("active");
               liNodeParent.find("span.arrow").removeClass("arrow").addClass("selected");
           });

       });
   </script>
    </form>
</body>
</html>
