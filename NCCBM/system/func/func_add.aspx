<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_fun_fun_add" Codebehind="func_add.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    
</head>
<body style="margin-top:0;font-size: 12pt">
    <form id="form1" runat="server">
    <div>

          <table width="100%" cellpadding="0" cellspacing="0" class="metable" style="height:50px">
            <td  class="tdhead" style=" height: 30px">
               <h3 style="text-align:center">添 加 职 务 信 息</h3>
            </td>
          </table>
   <table width="100%" border="1" class="metable"  cellpadding="3" cellspacing="1">
       <tr >
        <td >&nbsp;<span style=" font-size: 10pt;">职务名称：</span></td>
        <td >
            <asp:TextBox ID="txtFunc_Name" runat="server"   Width="200" TabIndex="1"></asp:TextBox>
            </td>
      </tr>
      <tr >
           <td align="center" colspan="4">
             <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="addnew" runat="server" Text="确定" TabIndex="2" OnClick="addnew_Click" />
              &nbsp;&nbsp;<asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="btnreturn" runat="server"  TabIndex="3" Text="返回" CausesValidation="false" OnClick="btnreturn_Click"/>
     	  </td>
     	</tr>
     	
    </table>
     <div>
     <asp:RequiredFieldValidator ID="valreNAME" controltovalidate="txtFunc_Name"  runat="server" ErrorMessage="职务名称不能为空"></asp:RequiredFieldValidator>
     </div>
    </div>
    </form>
</body>
</html>
