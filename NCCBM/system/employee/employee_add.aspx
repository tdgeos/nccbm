<%@ Page Language="C#" AutoEventWireup="true" Inherits="NCCBM.system.employee.system_employee_EMPLOYEE_add"  CodeBehind="employee_add.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>增加人员</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    <link href="../../css/jquery-ui-1.8.20.custom.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/common.js"></script>
    <script type="text/javascript" src="../../js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-ui-1.8.20.custom.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-ui.zh-CN.js"></script>
    <script type="text/javascript" src="../../js/datetime.js"></script>
    

</head>
<body >
    <form id="form1" runat="server">
    <div>

          <table width="100%" cellpadding="0" cellspacing="0" class="metable" style="height:10px">
            <td  class="tdhead" style=" height: 30px">
               <h3 style="text-align:center">添 加 人 员 信 息</h3>
            </td>
          </table>
          <table border="1" cellpadding="3" cellspacing="1" class="metable" width="100%" style="height:50px" >

            <tr>
                <td>
                    &nbsp;<span style="font-size: 10pt;">用户名：</span>
                </td>
                <td>
                    <asp:TextBox ID="txtEmployee_ID" runat="server" Width="200px" TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<span style="font-size: 10pt;">真实姓名：</span>
                </td>
                <td>
                    <asp:TextBox ID="txtEmployee_Name" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<span style="font-size: 10pt;">性别：</span>
                </td>
                <td>
                    <asp:RadioButtonList ID="radlGender" runat="server" Width="210px" TabIndex="3" RepeatLayout="Flow"
                        Font-Size="Small"  RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">男</asp:ListItem>
                        <asp:ListItem Value="1">女</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    &nbsp;<span style="font-size: 10pt;">出生年月：</span>
                </td>
                <td>
                    <asp:TextBox ID="txtBirthday" runat="server" Width="200px" TabIndex="4" CssClass="datepicker-sr"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<span style="font-size: 10pt;">部门：</span>
                </td>
                <td>
                    <asp:DropDownList ID="dropBranch_ID" runat="server" Width="210px" TabIndex="5">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;<span style="font-size: 10pt;">职务：</span>
                </td>
                <td>
                    <asp:DropDownList ID="dropFunc_ID" runat="server" Width="210px" TabIndex="6">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<span style="font-size: 10pt;">HSE证书_起始日期：</span>
                </td>
                <td>
                    <asp:TextBox ID="tbHSE1" runat="server" Width="200px" TabIndex="11" CssClass="datepicker-jstime"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<span style="font-size: 10pt;">HSE证书_结束日期：</span>
                </td>
                <td>
                    <asp:TextBox ID="tbHSE2" runat="server" Width="200px" TabIndex="12" CssClass="datepicker-jstime"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<span style="font-size: 10pt;">硫化氢证书_起始日期：</span>
                </td>
                <td>
                    <asp:TextBox ID="tbLHQ1" runat="server" Width="200px" TabIndex="11" CssClass="datepicker-jstime"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<span style="font-size: 10pt;">硫化氢证书_结束日期：</span>
                </td>
                <td>
                    <asp:TextBox ID="tbLHQ2" runat="server" Width="200px" TabIndex="12" CssClass="datepicker-jstime"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<span style="font-size: 10pt;">井控证书_起始日期：</span>
                </td>
                <td>
                    <asp:TextBox ID="tbJK1" runat="server" Width="200px" TabIndex="11" CssClass="datepicker-jstime"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<span style="font-size: 10pt;">井控证书_结束日期：</span>
                </td>
                <td>
                    <asp:TextBox ID="tbJK2" runat="server" Width="200px" TabIndex="12" CssClass="datepicker-jstime"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<span style="font-size: 10pt;">监督证书_起始日期：</span>
                </td>
                <td>
                    <asp:TextBox ID="tbJD1" runat="server" Width="200px" TabIndex="11" CssClass="datepicker-jstime"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<span style="font-size: 10pt;">监督证书_结束日期：</span>
                </td>
                <td>
                    <asp:TextBox ID="tbJD2" runat="server" Width="200px" TabIndex="12" CssClass="datepicker-jstime"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<span style="font-size: 10pt;">办公电话：</span>
                </td>
                <td>
                    <asp:TextBox ID="txtTel" runat="server" Width="200px" TabIndex="7"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<span style="font-size: 10pt;">手机：</span>
                </td>
                <td>
                    <asp:TextBox ID="txtMobilTel" runat="server" Width="200px" TabIndex="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<span style="font-size: 10pt;">住宅电话：</span>
                </td>
                <td>
                    <asp:TextBox ID="txtJTel" runat="server" Width="200px" TabIndex="9"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<span style="font-size: 10pt;">Email：</span>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Width="200px" TabIndex="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 30px" colspan="4">
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="addnew" runat="server" Text="确定" TabIndex="11" OnClick="addnew_Click" />
                    &nbsp;&nbsp;<asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="btnreturn" runat="server" TabIndex="12" Text="返回" OnClick="btnreturn_Click"
                        CausesValidation="false" />
                </td>
            </tr>
        </table>
        <div>
            <asp:RequiredFieldValidator ID="valreEmployee_ID" ControlToValidate="txtEmployee_ID"
                runat="server" ErrorMessage="用户名不能为空"></asp:RequiredFieldValidator><br />
            <asp:RequiredFieldValidator ID="valreEmployeeName" ControlToValidate="txtEmployee_Name"
                runat="server" ErrorMessage="真实姓名不能为空"></asp:RequiredFieldValidator><br />
            <%-- <asp:RegularExpressionValidator ID="RegularExpressionMobilTel" runat="server" display="dynamic"                 controltovalidate="txtMobilTel" errormessage="手机号码输入有误" validationexpression="(13\d{9}(;13\d{9})*)|(15\d{9}(;15\d{9})*)" /><br /><br />--%>
            <asp:RegularExpressionValidator ID="RegularExpressionEmail" runat="server" Display="dynamic"
                ControlToValidate="txtEmail" ErrorMessage="请填写正确的Email地址" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
        </div>
    </div>
    </form>
</body>
</html>
