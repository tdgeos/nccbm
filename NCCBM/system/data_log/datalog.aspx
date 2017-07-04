<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="datalog.aspx.cs" Inherits="NCCBM.system.data_log.datalog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    <link href="../../css/jquery-ui-1.8.20.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui.zh-CN.js" type="text/javascript"></script>
    <script src="../../js/datetime.js" type="text/javascript"></script>
</head>
<body style="min-height:400px">
    <form id="form1" runat="server">
    <div>
        <table width="850px">
            <tr>
                <td>
                    <h3 style="text-align:center">数据维护日志</h3>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="维护日期:"></asp:Label>
                    <asp:TextBox ID="tbKaishiRiqi" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Text="至"></asp:Label>
                    <asp:TextBox ID="tbJieshuRiqi" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>

                    <asp:Label ID="Label8" runat="server" Text="　区块:"></asp:Label>
                    <asp:DropDownList ID="ddlQukuai" runat="server">
                        <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                        <asp:ListItem Text="韩城" Value="韩城"></asp:ListItem>
                        <asp:ListItem Text="临汾" Value="临汾"></asp:ListItem>
                        <asp:ListItem Text="忻州" Value="忻州"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:Label ID="Label9" runat="server" Text="　数据表:"></asp:Label>
                    <asp:DropDownList ID="ddlTable" runat="server">
                        <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                        <asp:ListItem Text="钻进" Value="钻进"></asp:ListItem>
                        <asp:ListItem Text="下套管" Value="下套管"></asp:ListItem>
                        <asp:ListItem Text="固井" Value="固井"></asp:ListItem>
                        <asp:ListItem Text="完井" Value="完井"></asp:ListItem>
                        <asp:ListItem Text="完井数据统计表" Value="完井数据统计表"></asp:ListItem>
                        <asp:ListItem Text="压裂检查" Value="压裂检查"></asp:ListItem>
                        <asp:ListItem Text="射孔" Value="射孔"></asp:ListItem>
                        <asp:ListItem Text="压裂施工" Value="压裂施工"></asp:ListItem>
                        <asp:ListItem Text="失败原因说明" Value="失败原因说明"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label5" runat="server" Text="　用户:"></asp:Label>
                    <asp:TextBox ID="tbUser" runat="server" Width="100px"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="btnSelect" 
                        runat="server" Text="刷新" onclick="select_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>

        <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False" 
            CssClass="Table100" 
            AllowPaging="true" 
            OnPageIndexChanging="Page_IndexChanging" 
            ShowHeader="true">
            
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle CssClass="tableLine" />
            <EditRowStyle BackColor="#9EB6CE" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle CssClass="tableLineHeader" />
            <AlternatingRowStyle CssClass="tableLineAlternating" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="序号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="yonghuming" HeaderText="用户名" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="riqi" HeaderText="日期" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="leixing" HeaderText="维护方式" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="biaoming" HeaderText="表名" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="cenghao" HeaderText="层号" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
        </asp:GridView>
        </div>
    </form>
</body>
</html>
