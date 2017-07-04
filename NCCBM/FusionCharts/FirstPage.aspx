<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstPage.aspx.cs" Inherits="NCCBM.FusionCharts.FirstPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="../css/superTables.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.20.custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.zh-CN.js" type="text/javascript"></script>
    <script src="../js/datetime.js" type="text/javascript"></script>

    <script type="text/javascript" src="./JS/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="./JS/jquery-ui-1.8.20.custom.min.js"></script>
    <script type="text/javascript" src="./JS/jquery-ui.zh-CN.js"></script>
        <script type="text/javascript" language="javascript">
            function Close() {
                window.close();
                window.parent.document.getElementById("rsFrame").src = "";
            }
            function ShowChart(type) {
                var features = "height=600,width=800,resizable=yes,status=no,toolbar=no,menubar=no,location=no,scrollbars=yes";
                window.open('chartPage.aspx?charType=' + type, 'snowy', features);
            }
    </script>

</head>

<body>
    <form id="form1" runat="server">
        <table width="850px" border="0" cellpadding="0" cellspacing="0" style="margin-top:5px;">
            <tr style="height:35px;background-image:url(../../css/res/current.png);background-repeat:no-repeat;">
                <td style="width:200px;">
                   <h3 style="color:#eee; margin-top:7px; margin-left:10px;">总体情况</h3>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button1" runat="server" Text="周统计" OnClick="Btn_Zhou_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button2" runat="server" Text="月统计" OnClick="Btn_Yue_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button3" runat="server" Text="季统计" OnClick="Btn_Ji_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button4" runat="server" Text="年统计" OnClick="Btn_Nian_Click"/>
                </td>
            </tr>
        </table>
        <table width="840px" border="0" cellpadding="0" cellspacing="0">
            <tr >
                <td>
                    统计区块：
                    <asp:DropDownList ID="ddl_zj_qukuai" runat="server"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    统计日期：
                    <asp:TextBox ID="tbRiqi"  runat="server" CssClass="inputwidth datepicker" Width="80px"></asp:TextBox>
                </td>
                <%--<td>
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button1" runat="server" Text="周统计" OnClick="Btn_Zhou_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button2" runat="server" Text="月统计" OnClick="Btn_Yue_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button3" runat="server" Text="季度统计" OnClick="Btn_Ji_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button4" runat="server" Text="年统计" OnClick="Btn_Nian_Click"/>
                </td>--%>
            </tr>
        </table>

        <table cellspacing="5">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="钻井总体情况数据统计表" Width="825px" style="text-align:center;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblZj" runat="server" Text="" Width="825px" style="text-align:center;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gv_zj" Visible="true" runat="server" HeaderStyle-Wrap="false" Width="825px" Height="145px" HeaderStyle-BorderColor="Black" RowStyle-HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black">
                        <emptydatatemplate>
                            无有效数据.                 
                        </emptydatatemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr></tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="压裂总体情况数据统计表" Width="825px" style="text-align:center;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblYl" runat="server" Text="" Width="825px" style="text-align:center;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gv_yl" Visible="true" runat="server" HeaderStyle-Wrap="false" Width="825px" Height="145px" HeaderStyle-BorderColor="Black" RowStyle-HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black">
                        <emptydatatemplate>
                            无有效数据.                 
                        </emptydatatemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
