<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_role_role_edit" Codebehind="role_edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改</title>
 <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    
</head>
<body style="margin-top:0;font-size: 12pt">
    <form id="form1" runat="server">
    <div>
          <table width="100%" cellpadding="0" cellspacing="0" class="metable" style="height:50px">
            <td  class="tdhead" style=" height: 30px">
               <h3 style="text-align:center">修 改 用 户 组</h3>
            </td>
          </table>
   <table width="100%" border="1"  class="metable" cellpadding="3" cellspacing="1" style="height:50px">
       <tr >
        <td>&nbsp;<span style=" font-size: 10pt;">用户组名称：</span></td>
        <td>
            <asp:TextBox ID="txtRole_Name" runat="server"  Width="140" TabIndex="1"></asp:TextBox>
            </td>
        <td>&nbsp;<span style=" font-size: 10pt;">使用状态：</span></td>
        <td>
            <asp:RadioButtonList ID="radlstate" runat="server" RepeatLayout="Flow" AppendDataBoundItems="true"   TabIndex="2"  Font-Size="Small"  RepeatDirection="Horizontal" CssClass="inputwidth" >
        <asp:ListItem  Value="0" >可用 </asp:ListItem>
        <asp:ListItem  Value="1">停用 </asp:ListItem>
        </asp:RadioButtonList></td>
      </tr> 
      <tr >
           <td align="center" style="height:30" colspan="4">
             <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="addnew" runat="server" Text="确定" TabIndex="3" OnClick="addnew_Click" />
              &nbsp;&nbsp;<asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="btnreturn" runat="server"  TabIndex="4" CausesValidation="false" Text="返回" OnClick="btnreturn_Click"/>
     	  </td>
     	</tr>
     	</table>
     	<div>
     	<asp:RequiredFieldValidator ID="valreNAME" controltovalidate="txtRole_Name"  runat="server" ErrorMessage="用户组名称不能为空"></asp:RequiredFieldValidator>
        </div>
    
        
    </div>
    </form>
</body>
</html>
