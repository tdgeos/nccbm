<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tools.aspx.cs" Inherits="NCCBM.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
        <asp:Label ID="lblInfo" runat="server" Text="Label"></asp:Label>
        <br />

        <div style="width:80px; height:100px; border-width:thin; border-color:#d0d0f0; border-style:solid;">
            <asp:LinkButton ID="LinkButton1" runat="server" Text="testqwertyuiopasdfghjklzxcvbnm"></asp:LinkButton>
        </div>
        
    </div>
    </form>
</body>
</html>
