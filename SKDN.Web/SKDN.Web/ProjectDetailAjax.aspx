<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectDetailAjax.aspx.cs" Inherits="SKDN.Web.ProjectDetailAjax" %>

<!DOCTYPE html>

<html xmlns:fb="http://ogp.me/ns/fb#">
<head runat="server">
</head>
<body>
    <form id="form1" runat="server">
           <div id="fb-root"></div>
<script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&appId=1528142057440204&version=v2.0";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
      <div class="projectContent">
            <div class="col-Content">
                <asp:Literal runat="server" ID="ltrImage"></asp:Literal>
                <div class="content-project  scrollpanel no4">
                    <asp:Literal runat="server" ID="ltrContentProject"></asp:Literal>
                </div>
            </div>
            <div class="col-Comment">
                <div class="comment-wrapper">
                    <fb:comments href="<% = Request.Url.DnsSafeHost+ Request.RawUrl%>" width="520" numposts="100" colorscheme="light"></fb:comments>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
    </form>
</body>
</html>
