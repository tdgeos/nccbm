<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="NCCBM.Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
    <%--<meta http-equiv="X-UA-Compatible" content="IE=edge" />--%>
    <title>煤层气工程质量监控信息管理平台</title>
    <link rel="shortcut icon" href="css/res/ico.png" type="Styles/res/x-icon" />
    <link rel="stylesheet" media="all" type="text/css" href="css/login.css" />
</head>
<body>
    <div id="mlogo">
	    <a title="煤层气工程质量监控信息管理平台" class="logo"></a>    
    </div>
        <div id="zbj">
            <a class="zbj"></a>
        </div>
    <div id="bj">
        <a class="bj"></a>
        <div id="ad">
            <a class="ad"></a>
        </div>
        <form id="form1" class="login" runat="server">
        <div style="margin-left:15px">
        <h2 style="margin-top:-35px">用户登陆</h2>
        <fieldset class="inputs">
                <h3 style="margin-top:-5px"><label>用户名</label>
                <input runat="server" id="username" type="text" class="username_img" placeholder="输入用户名" maxlength="16" utofocus="1"   style="TEXT-TRANSFORM:lowercase;" /></h3>
                <h3 style="margin-top:-5px"><label>密　码</label>
                <input runat="server" id="password" type="password" class="password_img" placeholder="输入密码" maxlength="32"   /></h3>
        </fieldset>
        <fieldset class="actions"  style="margin-top:-5px">
            <asp:Button ID="login_in" runat="server" class="submit" Text="登  陆" OnClick="Login_In" />
            <asp:Button ID="reset" runat="server" class="submit" Text="重  置" Style="float: right" />
        </fieldset></div>
        </form>
    </div>
    <br />
    <div id="bottom">
        <a class="bottom"></a>
    </div>
    <div style="margin-left: 20px; margin-top: -55px;">
        <h4 style="color:#f0f0f0;">
            版权所有：中联煤层气国家工程研究中心有限责任公司</h4>
    </div>
    <div style="text-align: right; margin-top: -55px">
        <h6 style="color:#f0f0f0;">
            Copyright &copy; 2012&nbsp;&nbsp;
            <br />
            推荐分辨率:1024Х768&nbsp;&nbsp;&nbsp;
        </h6>
    </div>
</body>
</html>
