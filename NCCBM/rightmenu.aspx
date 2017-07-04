<%@ Page Language="C#" AutoEventWireup="true" Inherits="rightmenu" Codebehind="rightmenu.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="css/superTables.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .submit
        {
            width:84px;
            height:25px;
            background-image:url(css/res/fourmenu.png);
            background-repeat:repeat-x;
        }
    </style>
</head>
<body style="height:35px;background-image:url(../../css/res/current.png);background-repeat:no-repeat;">
    <h3 style="color:#eee; margin-top:-27px; margin-left:15px;">
        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
    </h3>
    <script type ="text/javascript" language="javascript">
        window.parent.rightmain.location.href="<%=szDefaultURL %>";
    </script>
</body>
</html>
