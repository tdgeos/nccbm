<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs"
    Inherits="NCCBM.ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>错误</title>
</head>
<style type="text/css">
    .menu,.header,.container,.copyright{width:950px;margin:0 auto;}
.error{padding:110px 0 180px 475px;background:url(css/res/erroricon.png) no-repeat 184px 44px;font-family:"微软雅黑";}
.error .err_title{text-indent:-999em;}
.error .sec_title{font-size:16px;color:#54616a;}
.error .oper{margin-top:20px;}
.error .oper a{font-size:14px;font-weight:700;text-decoration:underline;}
</style>
<body>
<br /><br /><br /><br />
    <div class="container">
        <div class="error" style="margin-left:-50px">
            <P class="sec_title"><asp:Label ID="message" runat="server"></asp:Label></P>
            <p class="oper"><a href="javascript :;" onClick="javascript :history.back(-1);"><img src="css/res/return.png" border="0" title="返回"></a></P>
            <a id="url" runat="server" href="Login.aspx"></a>
        </div>
    </div>
</body>
</html>
