<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_user_user_edit" Codebehind="user_edit.aspx.cs" %>

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
               <h3 style="text-align:center">修 改 用 户 信 息</h3>
            </td>
          </table>
<table width="100%" border="1"  class="metable" cellpadding="3" cellspacing="1" style="height:50px">
      <tr >
        <td>&nbsp;<span style=" font-size: 10pt;">用户名：</span></td>
        <td>
            <asp:DropDownList ID="drp1EmpID" runat="server" Width="210px" TabIndex="1" DataSourceID="SqlDataSource1" DataTextField="Employee_ID" DataValueField="Employee_ID"></asp:DropDownList></td>
    
        <td>&nbsp;<span style=" font-size: 10pt;">密码：</span></td>
        <td >
            <asp:TextBox ID="txtPassWord" runat="server"  Width="200px" TabIndex="2"></asp:TextBox>
            </td>
      </tr>	
      <tr >
        <td>&nbsp;<span style=" font-size: 10pt;">用户组：</span></td>
        <td>
            <asp:DropDownList ID="drop2RoleID" runat="server" Width="210px" TabIndex="3"></asp:DropDownList></td>
      
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
        <td>&nbsp;<span style=" font-size: 10pt;">使用状态：</span></td>
        <td>
            <asp:RadioButtonList ID="radlstate" runat="server" RepeatLayout="Flow" AppendDataBoundItems="true"   TabIndex="4"  Font-Size="Small"  RepeatDirection="Horizontal" Width="142px" >
        <asp:ListItem  Value="0" >可用 </asp:ListItem>
        <asp:ListItem  Value="1">停用 </asp:ListItem>
        </asp:RadioButtonList></td>
        <td></td><td></td>
      </tr> 
		<tr >
           <td align="center" style="height:29px" colspan="4">
             <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="addnew" runat="server" Text="更新" TabIndex="5" OnClick="addnew_Click" />
     	  &nbsp;&nbsp;<asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="btnreturn" runat="server"  TabIndex="6" Text="返回" OnClick="btnreturn_Click" CausesValidation="false"/></td>
     	</tr>
</table>
      <div>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" controltovalidate="txtPassWord"  runat="server" ErrorMessage="密码不能为空"></asp:RequiredFieldValidator><br />
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" controltovalidate="drp1EmpID"  runat="server" ErrorMessage="请选择用户名"></asp:RequiredFieldValidator><br />
      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" controltovalidate="drop2RoleID"  runat="server" ErrorMessage="请选择用户组"></asp:RequiredFieldValidator>
     </div>		
     </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>" SelectCommand="SELECT Employee_ID FROM T_System_EMPLOYEE">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>" SelectCommand="SELECT [Role_ID], [Role_Name] FROM [T_System_Role]">
        </asp:SqlDataSource>
    </form>
</body>
</html>
