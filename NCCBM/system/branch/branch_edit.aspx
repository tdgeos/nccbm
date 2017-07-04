<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_branch_edit_page" Codebehind="branch_edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改</title>
<link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    
</head>
<body style="font-size: 12pt;margin-top:0">
    <form id="form1" runat="server">
    <div>

<table width="100%" cellpadding="0" cellspacing="0" class="metable" style="height:50px">
            <td  class="tdhead" style=" height: 30px">
               <h3 style="text-align:center">修 改 部 门 信 息</h3>
              </td>
</table>
<table width="100%" border="1"  class="metable" cellpadding="3" cellspacing="1" style="height:50px">
      <tr >
        <td>&nbsp;<span style=" font-size: 10pt;">部门名称：</span></td>
        <td>
            <asp:TextBox ID="txtBranchName" runat="server"  Width="200" TabIndex="1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valrBranchName" runat="server" ControlToValidate="txtBranchName" Text="必填项目"></asp:RequiredFieldValidator></td>
      
        <td>&nbsp;<span style=" font-size: 10pt;">上级部门名称：</span></td>
        <td>
            <asp:DropDownList ID="txtFAtherName" runat="server" Width="206px" TabIndex="2"></asp:DropDownList></td>
      </tr> 
      <tr >
        <td>&nbsp;<span style=" font-size: 10pt;">使用状态：</span></td>
        <td>
            <asp:RadioButtonList ID="radlstate" runat="server" RepeatLayout="Flow" AppendDataBoundItems="true"  TabIndex="3"  Font-Size="Small"  RepeatDirection="Horizontal" CssClass="inputwidth" >
        <asp:ListItem  Value="0">可用 </asp:ListItem>
        <asp:ListItem  Value="1">停用 </asp:ListItem>
        </asp:RadioButtonList></td>
      </tr> 
		<tr >
           <td align="center" style="height:30" colspan="4">
             <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="makesure" runat="server" Text="确定" TabIndex="4" OnClick="makesure_Click" />
     	     &nbsp;&nbsp;<asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="btnreturn" runat="server"  TabIndex="5" Text="返回" CausesValidation="false" OnClick="btnreturn_Click"/>
     	  </td>
     	</tr>
</table>	
     </div>
    </form>
</body>
</html>
