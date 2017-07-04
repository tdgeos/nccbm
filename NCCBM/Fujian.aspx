<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fujian.aspx.cs" Inherits="NCCBM.Fujian" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body,html
        {
            min-height:400px;
            height:100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="height:100%;">
        <div>
            <asp:Label ID="Label2" runat="server" Text="附件" Width="60px" Font-Size="18pt"></asp:Label>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            &nbsp;&nbsp;
            <asp:LinkButton ID="lbDownLoad" runat="server" >下载</asp:LinkButton>
            &nbsp;&nbsp;
            <asp:LinkButton ID="lbDelete" runat="server" Visible="false" >删除</asp:LinkButton>
            &nbsp;&nbsp;
            <asp:LinkButton ID="lbReturn" runat="server" >返回</asp:LinkButton>
        </div>

        <div id="list" style="width:200px; height:100%; float:left; border-width:thin; border-color:#d0d0f0; border-style:solid;">
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" ></asp:Panel>
        </div>

        <div style="margin-left:5px; width:600px; height:100%; float:left; border-width:thin; border-color:#d0d0f0; border-style:solid;">
            <asp:Image ID="Image1" runat="server" Width="600px" Height="100%" ImageUrl=""/>
        </div>

        

        <div>
            <asp:Label ID="lblDir" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblFjs" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblPage" runat="server" Text="" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>