<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zj_list.aspx.cs" Inherits="NCCBM.data.zj.zj.zj_list" %>
<%@ Register Src="../../../Menu/ActionControl.ascx" TagName="ActionControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="X1.Pager" Assembly="X1.Pager" %>
<%@ Register TagPrefix="cc1" Namespace="NCCBM" Assembly="NCCBM" %>
<%@ Register Src="../../../MyPagerBar.ascx" TagName="myPager" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
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

    <script type="text/javascript"> 
        $(function () {
            $("#GridView2").toSuperTable({ width: "845px", height: "315px", fixedCols: 5, headerRows: 2 }).find("tr:even").addClass("altRow");
        });
    </script>

    <script runat="server" language="c#">
        public void GridViewRowBound(object sender,GridViewRowEventArgs e)
        {
            NCCBM.EXGridView.GridViewRowBound(sender, e);
        } 
    </script>

</head>
<body style="min-height:350px;">
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
                <asp:BoundField DataField="kaizuanriqi" HeaderText="开钻日期" SortExpression="kaizuanriqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="shejijingshen" HeaderText="设计井深" SortExpression="shejijingshen"/>
                <asp:BoundField DataField="muqianjingshen" HeaderText="目前井深" SortExpression="muqianjingshen"/>
                <asp:BoundField DataField="dangrijinchi" HeaderText="当日进尺" SortExpression="dangrijinchi"/>
                <asp:BoundField DataField="gongkuang" HeaderText="工况" SortExpression="gongkuang"/>
                <asp:BoundField DataField="midu" HeaderText="密度" SortExpression="midu"/>
                <asp:BoundField DataField="niandu" HeaderText="粘度" SortExpression="niandu"/>
                <asp:BoundField DataField="jingduan" HeaderText="井段" SortExpression="jingduan"/>
                <asp:BoundField DataField="jingxie_sheji" HeaderText="井斜(设计)" SortExpression="jingxie_sheji"/>
                <asp:BoundField DataField="jingxie_shiji" HeaderText="井斜(实际)" SortExpression="jingxie_shiji"/>
                <asp:BoundField DataField="fangwei_sheji" HeaderText="方位(设计)" SortExpression="fangwei_sheji"/>
                <asp:BoundField DataField="fangwei_shiji" HeaderText="方位(实际)" SortExpression="fangwei_shiji"/>
                <asp:BoundField DataField="goutui_sheji" HeaderText="狗腿(设计)" SortExpression="goutui_sheji"/>
                <asp:BoundField DataField="goutui_shiji" HeaderText="狗腿(实际)" SortExpression="goutui_shiji"/>
                <asp:BoundField DataField="HSEqingkuang" HeaderText="HSE情况" SortExpression="HSEqingkuang"/>
                <asp:BoundField DataField="fuzaqingkuang" HeaderText="复杂情况" SortExpression="fuzaqingkuang"/>
                <asp:BoundField DataField="zhenggaineirong" HeaderText="整改内容及促措施" SortExpression="zhenggaineirong"/>
                <asp:BoundField DataField="beizhu" HeaderText="备注" SortExpression="beizhu"/>
                <asp:BoundField DataField="jiancefangshi" HeaderText="巡检方式" SortExpression="jiancefangshi"/>
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
