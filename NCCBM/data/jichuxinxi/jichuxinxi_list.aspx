<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jichuxinxi_list.aspx.cs" Inherits="NCCBM.data.jichuxinxi.jichuxinxi_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link  href="../../../css/jquery-ui-1.8.20.custom.css" rel="stylesheet" type="text/css" />
    <link  href="../../../css/superTables.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui.zh-CN.js" type="text/javascript"></script>
    <script src="../../../js/superTables.js" type="text/javascript"></script>
    <script src="../../../js/jquery.superTable.js" type="text/javascript"></script>
    <script src="../../../js/datetime.js" type="text/javascript"></script>

    <script runat="server" language="c#">
        public void GridViewRowBound(object sender,GridViewRowEventArgs e)
        {
            NCCBM.EXGridView.GridViewRowBound(sender, e);
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="badtable">
            <asp:TextBox ID="tbPageIndex" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="tbQk" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="tbZt" runat="server" Visible="false"></asp:TextBox>
        </div>
        <table>
        <tr>
            <td>
            <asp:Label ID="Label8" runat="server" Text="区块:"></asp:Label>
            <asp:DropDownList ID="lstQukuai" runat="server"></asp:DropDownList>

            <asp:Label ID="Label1" runat="server" Text="　井号:"></asp:Label>
            <asp:TextBox ID="tbJinghao" runat="server"></asp:TextBox>

            <asp:Label ID="Label9" runat="server" Text="　当前状态:"></asp:Label>
            <asp:DropDownList ID="lstZhuangtai" runat="server">
                <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                <asp:ListItem Text="钻进" Value="钻进"></asp:ListItem>
                <asp:ListItem Text="下套管" Value="下套管"></asp:ListItem>
                <asp:ListItem Text="固井" Value="固井"></asp:ListItem>
                <asp:ListItem Text="完井" Value="完井"></asp:ListItem>
                <asp:ListItem Text="未压裂" Value="未压裂"></asp:ListItem>
                <asp:ListItem Text="已压裂" Value="已压裂"></asp:ListItem>
                <asp:ListItem Text="下泵" Value="下泵"></asp:ListItem>
                <asp:ListItem Text="交井" Value="交井"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="btnShaixuan" 
                        runat="server" Text="筛选" onclick="btnShaixuan_Click" />
            </td>
        </tr>
        <tr>
            <td>
            <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        </table>

        <asp:GridView ID="GridView2" runat="server" Width="830px"
            AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center"
            CssClass="Table100" AllowPaging="true" 
            OnPageIndexChanging="Page_IndexChanging" 
            OnRowCommand="GridView2_RowCommand" 
            OnRowDataBound="GridViewRowBound" ShowHeader="true">

            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>

            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle CssClass="tableLine" />
            <EditRowStyle BackColor="#9EB6CE" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle CssClass="tableLineHeader" />
            <AlternatingRowStyle CssClass="tableLineAlternating" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="序号"/>
                <asp:BoundField DataField="jinghao" HeaderText="井号"/>
                <asp:BoundField DataField="x" HeaderText="横坐标"/>
                <asp:BoundField DataField="y" HeaderText="纵坐标"/>
                <asp:BoundField DataField="h" HeaderText="海拔"/>
                <asp:BoundField DataField="jingbie" HeaderText="井别"/>
                <asp:BoundField DataField="jingxing" HeaderText="井型"/>
                <asp:BoundField DataField="shejijingshen" HeaderText="设计井深"/>
                <asp:BoundField DataField="shigongdanwei" HeaderText="施工单位" />
                <asp:BoundField DataField="kaizuanriqi" HeaderText="开钻日期" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="dangqianzhuangtai" HeaderText="当前状态" />
                <asp:BoundField DataField="qukuai" HeaderText="区块" />
                <asp:BoundField DataField="shengchanriqi" HeaderText="生产日期" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="beizhu" HeaderText="备注" />


                <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" Text="修改" CommandName="xiugai"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDel" runat="server" CausesValidation="False" Text="删除" CommandName="shanchu" OnClientClick="JavaScript:return confirm('确定要删除吗？')"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField> 
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
