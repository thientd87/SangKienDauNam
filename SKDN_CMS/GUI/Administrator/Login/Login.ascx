<%@ Import Namespace="DFISYS" %>
<%@ Control EnableViewState="True" Language="c#" AutoEventWireup="true" Inherits="DFISYS.API.Module"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
    <link href="/Styles/metronic/css/pages/login.css" rel="stylesheet" type="text/css"/>
<script runat="server">
    private void Page_Load(object sender, System.EventArgs e)
    {
        HttpCookie cookie = (HttpCookie)Request.Cookies["PortalUser"];
        if (cookie != null)
        {
            try
            {
                DFISYS.ChannelUsers objUser = new DFISYS.ChannelUsers();
                if (objUser.Login(Crypto.Decrypt(cookie.Values["AC"]), Crypto.Decrypt(cookie.Values["PW"])))
                {
                    if (Session["lastPath"] != null && Session["lastPath"] != "")
                    {
                        string strLastPath = Session["lastPath"].ToString();
                        Session.Remove("lastPath");
                        Response.Redirect(strLastPath);
                    }
                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception) { }
        }

        if (Session["NotPermission"] != null)
        {
            Session.Remove("NotPermission");
        }
    }

    void OnLogin(object sender, EventArgs args)
    {
        DFISYS.ChannelUsers objUser = new DFISYS.ChannelUsers();

        //Luu bien Session EditionType khi nguoi dung dang nhap
        //Tieng Viet = 1
        //Tieng Anh = 2
        if (radioVI.Checked)
        {
            Session["EditionType"] = 1;
        }
        else if(radioEN.Checked)
        {
            Session["EditionType"] = 2;
        }
        else
        {
            Session["EditionType"] = 1;
        }
        if (objUser.Login(account.Value, password.Value))
        {

            if (Session["lastPath"] != null && Session["lastPath"] != "")
            {
                string strLastPath = Session["lastPath"].ToString();
                Session.Remove("lastPath");
                Response.Redirect(strLastPath);
            }
            Response.Redirect("Default.aspx");
        }
        else
        {
            lError.Text = "Mật khẩu hoặc tên đăng nhập không đúng";
            trMeussage.Visible = true;
        }
    }
</script>
<div class="login">
	<!-- BEGIN LOGO -->
	<div class="logo">
		<img src="/Styles/metronic/img/logo-big.png" alt="" />
        <div id="trMeussage" runat="server" Visible="false" class="error msg"><asp:Literal ID="lError" runat="server"></asp:Literal></div>
	</div>
	<!-- END LOGO -->
	<!-- BEGIN LOGIN -->
	<div class="content">
		<!-- BEGIN LOGIN FORM -->
		<form class="form-vertical login-form" action="index.html" method="post">
			<h3 class="form-title">Login to your account</h3>
			<div class="alert alert-error hide">
				<button class="close" data-dismiss="alert"></button>
				<span>Enter any username and password.</span>
			</div>
			<div class="control-group">
				<!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
				<label class="control-label visible-ie8 visible-ie9">Username</label>
				<div class="controls">
					<div class="input-icon left">
						<i class="icon-user"></i>
						<input class="m-wrap placeholder-no-fix" runat="server" type="text"  ID="account" autocomplete="off" placeholder="Username" name="username"/>
					</div>
				</div>
			</div>
			<div class="control-group">
				<label class="control-label visible-ie8 visible-ie9">Password</label>
				<div class="controls">
					<div class="input-icon left">
						<i class="icon-lock"></i>
						<input class="m-wrap placeholder-no-fix" runat="server" ID="password" type="password" autocomplete="off" placeholder="Password" name="password"/>
					</div>
				</div>
			</div>
			<div class="form-actions">
				<asp:LinkButton runat="server" CssClass="btn green pull-right" ID="lnkLogin" OnClick="OnLogin">Login <i class="m-icon-swapright m-icon-white"></i></asp:LinkButton>
				       
			</div>
		</form>
		<!-- END LOGIN FORM -->        
	
	</div>
	<!-- END LOGIN -->
	<!-- BEGIN COPYRIGHT -->
	<div class="copyright">
		2013 &copy; Trinh Duc Thien. Admin Dashboard Template.
	</div>
	<!-- END COPYRIGHT -->
    </div>
<div id="login" class="box hide">
    <h2>
        Login</h2>
    <section>
		
		<form action="dashboard.html">
			<dl>
				
                <dt><label for="adminpassword">Ngôn ngữ</label> <asp:RadioButton ID="radioVI" runat="server" Text="Tiếng Việt" GroupName="rdLanguage" Checked="True"/>
            <asp:RadioButton ID="radioEN" runat="server" Text="English" GroupName="rdLanguage"/></dt>
			</dl>
			
		</form>
	</section>
</div>
<script language="javascript">
	document.getElementById('<%=account.ClientID%>').focus();	
	function CheckEnterKey(e)
	{
	    var keycode;
	    if (e) keycode = e.which;
	    else 
        if (window.event) keycode = window.event.keyCode;
	    
		if(keycode == 13)
		{		    
			<%= Page.GetPostBackClientHyperlink(lnkLogin, "") %>
		}
	}	
	document.onkeypress = CheckEnterKey;	
</script>
