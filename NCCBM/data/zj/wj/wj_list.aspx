<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wj_list.aspx.cs" Inherits="NCCBM.data.zj.wj.wj_list" %>
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
            $("#GridView2").toSuperTable({ width: "845px", height: "315px", fixedCols: 5, headerRows: 2 }).find("tr:even").addClass("altRow");
        });
    </script>

    <script runat="server" language="c#">
        public void GridViewRowBound(object sender,GridViewRowEventArgs e){
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
                <asp:BoundField DataField="id" HeaderText="序号" SortExpression="id" ItemStyle-Width="50px"/>
                <asp:BoundField DataField="riqi" HeaderText="上报日期" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="fuzejiandu" HeaderText="负责监督" SortExpression="fuzejiandu" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="duihao" HeaderText="队号" SortExpression="duihao" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jinghao" HeaderText="井号" SortExpression="jinghao" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="biaozhiguan_sheji" HeaderText="设计标志管位置" SortExpression="biaozhiguan_sheji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="biaozhiguan_shiji" HeaderText="实际标志管位置" SortExpression="biaozhiguan_shiji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="rengongjingdi_sheji" HeaderText="设计人工井底" SortExpression="rengongjingdi_sheji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="rengongjingdi_shiji" HeaderText="实际人工井底" SortExpression="rengongjingdi_shiji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="gujingzhiliangCBL" HeaderText="固井质量CBL" SortExpression="gujingzhiliangCBL" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="gujingzhiliangVDL" HeaderText="固井质量VDL" SortExpression="gujingzhiliangVDL" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shuipingpiancha_bzh" HeaderText="标准产套偏差" SortExpression="shuipingpiancha_bzh" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shuipingpiancha_hor" HeaderText="水平产套偏差" SortExpression="shuipingpiancha_hor" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shuipingpiancha_ver" HeaderText="垂直产套偏差" SortExpression="shuipingpiancha_ver" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shiya_start" HeaderText="开始试压" SortExpression="shiya_start" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shiya_end" HeaderText="结束试压" SortExpression="shiya_end" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="gangbanhanjie_up" HeaderText="上缘环形干板焊接" SortExpression="gangbanhanjie_up" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="gangbanhanjie_down" HeaderText="下缘环形钢板焊接" SortExpression="gangbanhanjie_down" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jingkougaodu" HeaderText="井口高度" SortExpression="jingkougaodu" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shikouwanhao" HeaderText="丝扣是否完好" SortExpression="shikouwanhao" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="cunzaiwenti" HeaderText="存在问题" SortExpression="cunzaiwenti" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="fuzaqingkuang" HeaderText="复杂情况" SortExpression="fuzaqingkuang" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="beizhu" HeaderText="备注" SortExpression="beizhu" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jiancefangshi" HeaderText="检测方式" SortExpression="jiancefangshi" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="place" HeaderText="区块" />
                <asp:BoundField DataField="fujian" HeaderText="附件" SortExpression="fujian" ItemStyle-Wrap="false"/>

                <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" Text="修改" CommandName="xiugai"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False" ItemStyle-Wrap="false">
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
