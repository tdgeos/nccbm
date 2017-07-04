<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tongji_Sgzl.aspx.cs" Inherits="NCCBM.FusionCharts.Tongji_Sgzl" %>

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
    <script runat="server" language="c#">
        public void GridViewRowBound(object sender,GridViewRowEventArgs e)
        {
            NCCBM.EXGridView.GridViewRowBound(sender, e);
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="850px">
            <tr>
                <td style="height:35px;background-image:url(../../css/res/current.png);background-repeat:no-repeat;">
                   <h3 style="color:#eee; margin-top:7px; margin-left:10px;">施工质量</h3>
                </td>
            </tr>
        </table>

        <table width="840px" border="0" cellpadding="0" cellspacing="10">
            <tr >
                <td>
                    统计区块：
                    <asp:DropDownList ID="ddl_zj_qukuai" runat="server"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    统计日期：
                    <asp:TextBox ID="tbRiqi"  runat="server" Width="100px" CssClass="inputwidth datepicker"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbZhu" runat="server" Text="柱状图" Checked="True" 
                        GroupName="picType" oncheckedchanged="rbZhu_CheckedChanged" 
                        AutoPostBack="True" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbXian" runat="server" Text="曲线图" GroupName="picType" 
                        AutoPostBack="True" oncheckedchanged="rbXian_CheckedChanged" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbBing" runat="server" Text="饼图" GroupName="picType" 
                        AutoPostBack="True" oncheckedchanged="rbBing_CheckedChanged" />
                </td>
            </tr>
            <tr >
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button5" runat="server" Text="本周" OnClick="Btn_Zhou_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button6" runat="server" Text="本月" OnClick="Btn_Yue_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button7" runat="server" Text="本季" OnClick="Btn_Ji_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="Button8" runat="server" Text="本年" OnClick="Btn_Nian_Click"/>
                </td>
            </tr>
        </table>

        <table cellspacing="5">
            <tr>
                <td>
                    <asp:Label ID="lblTable" runat="server" Text="施工质量" Width="400px" style="text-align:center;"></asp:Label>
                    <br />
                    <asp:Label ID="lblRiqi" runat="server" Text="" Width="400px" style="text-align:center;"></asp:Label>
                </td>
                <td>
                    <div style="width:400px; float:left; margin-left:10px; margin-bottom:20px;" align="center">
                        <asp:LinkButton ID="lbtnHc" runat="server" ForeColor="#0000ff" Font-Underline="true" Visible="false" >韩城</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnLf" runat="server" ForeColor="#0000ff" Font-Underline="true" Visible="false" >临汾</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnXz" runat="server" ForeColor="#0000ff" Font-Underline="true" Visible="false" >忻州</asp:LinkButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="width:400px; height:300px;float:left;">
                        <asp:GridView ID="gv" runat="server" Width="400px" Height="280px" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="zongshu" HeaderText="压裂次数" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="jingshu" HeaderText="失败次数" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="chenggonglv" HeaderText="一次成功率" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td>
                    <div style="width:400px; height:300px;float:left;margin-left:10px; margin-top:0px;">
                        <iframe id="img" runat="server" width="400px" height="280px" frameborder="0" marginheight="0" marginwidth="0" align="middle" border="0"></iframe>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblData" runat="server" Text="" Visible="false"></asp:Label>
    </form>
</body>
</html>
