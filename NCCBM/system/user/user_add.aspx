<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_user_user_add" Codebehind="user_add.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加用户</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    
</head>
<body style="margin-top:0;font-size: 12pt">
    <form id="form1" runat="server">
    <div>
          <table width="100%" cellpadding="0" cellspacing="0" class="metable" style="height:50px">
            <td  class="tdhead" style=" height: 30px">
               <h3 style="text-align:center">添 加 用 户 信 息</h3>
            </td>
          </table>
<table width="100%" border="1"  class="metable" cellpadding="3" cellspacing="1" style="height:50px">
      <tr >
        <td>&nbsp;<span style=" font-size: 10pt;">用户名：</span></td>
        <td>
            <asp:DropDownList ID="drp1EmpID" runat="server" Width="210px" TabIndex="1" ></asp:DropDownList></td>
     
        <td>&nbsp;<span style=" font-size: 10pt;">密码：</span></td>
        <td>
            <asp:TextBox ID="txtPassWord" runat="server"  Width="200px" TabIndex="2" TextMode="Password"></asp:TextBox>
            </td>
      </tr>	
      <tr >
        <td>&nbsp;<span style=" font-size: 10pt;">用户组：</span></td>
        <td>
            <asp:DropDownList ID="drop2RoleID" runat="server" Width="210px" TabIndex="3" ></asp:DropDownList></td>
      
        <td>&nbsp;<span style=" font-size: 10pt;">所属区块：</span></td>
        <td>
            <asp:DropDownList ID="lstQukuai" runat="server" Width="210px" TabIndex="3" >
                <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                <asp:ListItem Text="韩城" Value="韩城"></asp:ListItem>
                <asp:ListItem Text="临汾" Value="临汾"></asp:ListItem>
                <asp:ListItem Text="忻州" Value="忻州"></asp:ListItem>
            </asp:DropDownList></td>
      </tr>
		<tr >
           <td align="center" style="height:30px" colspan="4">
             <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="addnew" runat="server" Text="确定" TabIndex="4" OnClick="addnew_Click" />
     	  &nbsp;&nbsp;<asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="btnreturn" runat="server"  TabIndex="5" Text="返回" OnClick="btnreturn_Click" CausesValidation="false"/></td>
     	</tr>
</table>
      <div>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" controltovalidate="txtPassWord"  runat="server" ErrorMessage="密码不能为空"></asp:RequiredFieldValidator><br />
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" controltovalidate="drp1EmpID"  runat="server" ErrorMessage="请选择用户名"></asp:RequiredFieldValidator><br />
      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" controltovalidate="drop2RoleID"  runat="server" ErrorMessage="请选择用户组"></asp:RequiredFieldValidator>
          <br />
          <asp:HiddenField ID="hidden_Select" runat="server" />
     </div>	
     </div>
    </form>
</body>
</html>
