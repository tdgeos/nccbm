<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xtg_list.aspx.cs" Inherits="NCCBM.data.zj.xtg.xtg_list" %>
<%@ Register Src="../../../Menu/ActionControl.ascx" TagName="ActionControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="X1.Pager" Assembly="X1.Pager" %>
<%@ Register TagPrefix="cc1" Namespace="NCCBM" Assembly="NCCBM" %>
<%@ Register Src="../../../MyPagerBar.ascx" TagName="myPager" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>    

    <link  href="../../../css/jquery-ui-1.8.20.custom.css" rel="stylesheet" type="text/css" />
    <link  href="../../../css/superTables.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui.zh-CN.js" type="text/javascript"></script>
    <script src="../../../js/superTables.js" type="text/javascript"></script>
    <script src="../../../js/jquery.superTable.js" type="text/javascript"></script>
    <script src="../../../js/datetime.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $("#GridView2").toSuperTable({ width: "845px", height: "291px", fixedCols: 5 }).find("tr:even").addClass("altRow");
        });
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
        <div class="badtable">
        <asp:Label ID="Label1" runat="server" Text="区块:"></asp:Label>
        <asp:DropDownList ID="ddlPlace" runat="server"></asp:DropDownList>
        <asp:Label ID="Label3" runat="server" Text="导入日期:"></asp:Label>
        <asp:TextBox ID="tbKaishiRiqi" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="至"></asp:Label>
        <asp:TextBox ID="tbJieshuRiqi" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>
        <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="Button5" 
            runat="server" Text="筛选" onclick="Button5_Click" />
        <asp:Label ID="lblRiqi" runat="server" Text=""></asp:Label>
        <asp:TextBox ID="tbPageIndex" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="tbQukuai" runat="server" Visible="false"></asp:TextBox>
        </div>

        <asp:GridView ID="GridView2" runat="server" 
            AutoGenerateColumns="False" 
            CssClass="Table100" 
            AllowPaging="true" 
            OnPageIndexChanging="Page_IndexChanging" 
            OnRowCommand="GridView2_RowCommand" 
            OnRowCreated="GridView2_RowCreated" 
            OnRowDataBound="GridViewRowBound" 
            ShowHeader="false" 
            PagerSettings-Visible="false" 
            ShowFooter="false">

            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle CssClass="tableLine" />
            <EditRowStyle BackColor="#9EB6CE" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle CssClass="tableLineHeader" />
            <AlternatingRowStyle CssClass="tableLineAlternating" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="序号" SortExpression="id"/>
                <asp:BoundField DataField="riqi" HeaderText="上报日期" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="fuzejiandu" HeaderText="负责监督" SortExpression="fuzejiandu"/>
                <asp:BoundField DataField="duihao" HeaderText="队号" SortExpression="duihao"/>
                <asp:BoundField DataField="jinghao" HeaderText="井号" SortExpression="jinghao"/>
                <asp:BoundField DataField="wanzuanjingshen" HeaderText="完钻井深" SortExpression="wanzuanjingshen"/>
                <asp:BoundField DataField="wanzuanriqi" HeaderText="完钻日期" SortExpression="wanzuanriqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="taoguanxiashen" HeaderText="套管下深" SortExpression="taoguanxiashen"/>
                <asp:BoundField DataField="gangji" HeaderText="钢级" SortExpression="gangji"/>
                <asp:BoundField DataField="chicun" HeaderText="尺寸" SortExpression="chicun"/>
                <asp:BoundField DataField="sikou" HeaderText="丝扣" SortExpression="sikou"/>
                <asp:BoundField DataField="waiguan" HeaderText="外观" SortExpression="waiguan"/>
                <asp:BoundField DataField="pingjunbihou" HeaderText="平均壁厚(抽查10跟)" SortExpression="pingjunbihou"/>
                <asp:BoundField DataField="pingjunwaijing" HeaderText="平均外径(抽查10跟)" SortExpression="pingjunwaijing"/>
                <asp:BoundField DataField="mifengzhi" HeaderText="密封脂(5根抽查1次)" SortExpression="mifengzhi"/>
                <asp:BoundField DataField="jingkou" HeaderText="紧扣(5根抽查1次)" SortExpression="jingkou"/>
                <asp:BoundField DataField="fukuang" HeaderText="浮箍、浮鞋检查" SortExpression="fukuang"/>
                <asp:BoundField DataField="taoguanshuju" HeaderText="套管数据表是否按要求填写" SortExpression="taoguanshuju"/>
                <asp:BoundField DataField="taoguanpici" HeaderText="套管批次、编号是否与物资部出库单一致" SortExpression="taoguanpici"/>
                <asp:BoundField DataField="cunzaiwenti" HeaderText="存在问题" SortExpression="cunzaiwenti"/>
                <asp:BoundField DataField="zhenggaicuoshi" HeaderText="整改措施" SortExpression="zhenggaicuoshi"/>
                <asp:BoundField DataField="beizhu" HeaderText="备注" SortExpression="beizhu"/>
                <asp:BoundField DataField="jiancefangshi" HeaderText="巡检方式" SortExpression="jiancefangshi"/>
                <asp:BoundField DataField="taoguanchangjia" HeaderText="套管厂家" SortExpression="taoguanchangjia"/>
                <asp:BoundField DataField="place" HeaderText="区块" />
                <asp:BoundField DataField="fujian" HeaderText="附件" SortExpression="fujian"/>

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
        <div>
            <uc1:myPager ID="commPage1" runat="server"/>
        </div>
    </form>
</body>
</html>
