<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jsyj_add.aspx.cs" Inherits="NCCBM.data.Report.jsyj.jsyj_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../css/superTables.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/jquery-ui-1.8.20.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui.zh-CN.js" type="text/javascript"></script>
    <script src="../../../js/datetime.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="850px" border="0" cellpadding="0" cellspacing="0" >
          <tr >                
            <td style=" height: 30px">
              <h3 style="text-align:center">添加技术研究及其他工作数据</h3></td>
          </tr> 
        </table>
        <asp:Label ID="Label3" runat="server" Text="日期：" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="txtRiqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="韩城" Width="850px" Font-Size="10pt" style="text-align:center;"></asp:Label>
        <asp:TextBox ID="txt_report_hc" runat="server" TextMode="MultiLine" Width="832px" Height="77px"></asp:TextBox>
        <br /><br />

        <asp:Label ID="Label1" runat="server" Text="临汾" Width="850px" Font-Size="10pt" style="text-align:center;"></asp:Label>
        <asp:TextBox ID="txt_report_lf" runat="server" TextMode="MultiLine" Width="832px" Height="77px"></asp:TextBox>
        <br /><br />

        <asp:Label ID="Label2" runat="server" Text="忻州" Width="850px" Font-Size="10pt" style="text-align:center;"></asp:Label>
        <asp:TextBox ID="txt_report_xz" runat="server" TextMode="MultiLine" Width="832px" Height="77px"></asp:TextBox>
        <br /><br />

        <table>
            <tr>
                <td align="center" style="width:850px;">
                    <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="btnOk" runat="server" Text="确定" OnClick="btnOk_Click"/>
                    &nbsp;&nbsp;<asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click"/>
                </td>
            </tr>      
        </table>
    </form>
</body>
</html>
