<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_branch_add_page" Codebehind="branch_add.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>添加部门</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    
</head>
<body style="font-size: 12pt;margin-top:0">
    <form id="form1" runat="server">
        <div>
        <table width="100%" cellpadding="0" cellspacing="0" class="metable" style="height:50px">
            <tr>
                <td  class="tdhead" style=" height: 30px">
                    <h3 style="text-align:center">添 加 部 门 信 息</h3>
                </td>
            </tr>
        </table>

        <table style="width:100%;height:50px" border="1" cellpadding="3" cellspacing="1" class="metable">
            <tr >
                <td>
                    &nbsp;&nbsp;<span style=" font-size: 10pt;">部门名称：</span>
                </td>
                <td>
                    <asp:TextBox ID="txtBranchName" runat="server"  Width="200" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valrBranchName" runat="server" ControlToValidate="txtBranchName" Text="必填项目"></asp:RequiredFieldValidator>
                </td>
                <td>
                    &nbsp;&nbsp;<span style=" font-size: 10pt;">上级部门名称：</span>
                </td>
                <td>
                    <asp:DropDownList ID="txtFAtherName" runat="server" Width="200px" TabIndex="2"></asp:DropDownList>
                </td>
            </tr> 	
            <tr >
                <td align="center" colspan="4">
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="addnew" runat="server" Text="确定" TabIndex="3" OnClick="addnew_Click" />
     	            &nbsp;&nbsp;<asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="btnreturn" runat="server"  TabIndex="4" Text="返回" CausesValidation="false" OnClick="btnreturn_Click"/>
     	        </td>
            </tr>
        </table>	
        </div>
    </form>
</body>
</html>
