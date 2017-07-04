<%@ Page Language="C#" AutoEventWireup="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>请重新登陆</title>

    <style type="text/css">
        .menu,.header,.container,.copyright{width:950px;margin:0 auto;}
        .error{padding:110px 0 180px 475px;background:url(css/res/erroricon.png) no-repeat 184px 44px;font-family:"微软雅黑";}
        .error .err_title{text-indent:-999em;}
        .error .sec_title{font-size:16px;color:#54616a;}
        .error .oper{margin-top:20px;}
        .error .oper a{font-size:14px;font-weight:700;text-decoration:underline;}
    </style>
</head>

<body>
    <br /><br /><br /><br />
    <div class="container">
        <div class="error" style="margin-left:-50px">
            <p class="sec_title">用户登陆超时或未登陆,请重新登陆。</p>
            <p class="oper">
                <a href="Login.aspx">
                    <img src="css/res/again.png" border="0" title="" alt=""/>
                </a>
            </p>
        </div>
    </div>
</body>
</html>
