<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MailGenerator.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal runat="server" Text="<%$ Resources:resource, Login%>" /></title>
    <script src="scripts/jquery/jquery-1.12.4.js"></script>
    <script src="scripts/bootstrap/bootstrap.js"></script>
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/MailGenerator.css" rel="stylesheet"/>
</head>
<body>
<div class="wrapper">
    <form class="form-signin" runat="server">       
        <h2 class="form-signin-heading"><asp:Literal runat="server" Text="<%$ Resources:resource, PleaseSignIn%>" /></h2>
        <asp:TextBox type="email" class="form-control" ID="txtEmail" name="username" placeholder="<%$ Resources:resource, Email %>" required="" autofocus=""  runat="server"/>
        <asp:TextBox type="password" class="form-control" ID="txtPassword" name="password" placeholder="<%$ Resources:resource, Password %>" required="" runat="server"/>      
        <div id="remember" class="checkbox">
            <label>
                <asp:CheckBox type="checkbox" id="chkRememberMe" runat="server" /> <asp:Literal runat="server" Text="<%$ Resources:resource, RememberMe%>" />
            </label>
        </div>
        <asp:Button runat="server" id="btnSubmit" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnSubmit_OnClick" Text="<%$ Resources:resource, Login %>"></asp:Button>
    </form>
</div>
</body>
</html>