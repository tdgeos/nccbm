<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ylsg_list.aspx.cs" Inherits="NCCBM.data.yl.ylsg.ylsg_list" %>
<%@ Register Src="../../../Menu/ActionControl.ascx" TagName="ActionControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="X1.Pager" Assembly="X1.Pager" %>
<%@ Register TagPrefix="cc1" Namespace="NCCBM" Assembly="NCCBM" %>
<%@ Register Src="../../../MyPagerBar.ascx" TagName="myPager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
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
            $("#GridView2").toSuperTable({ width: "845px", height: "291px", fixedCols: 3 }).find("tr:even").addClass("altRow");
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
        <asp:Label ID="Label3" runat="server" Text="施工日期:"></asp:Label>
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
            OnRowDataBound="GridViewRowBound" 
            ShowHeader="true" 
            PagerSettings-Visible="false"
            ShowFooter="false">

            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle CssClass="tableLine" />
            <EditRowStyle BackColor="#9EB6CE" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle CssClass="tableLineHeader" />
            <AlternatingRowStyle CssClass="tableLineAlternating" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="序号" />
                <asp:BoundField DataField="jinghao" HeaderText="井号" />
                <asp:BoundField DataField="cengwei" HeaderText="层位" />
                <asp:BoundField DataField="shigongriqi" HeaderText="施工日期"  DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="mcjd_dingjie" HeaderText="煤层顶深" />
                <asp:BoundField DataField="mcjd_dijie" HeaderText="煤层底深" />
                <asp:BoundField DataField="meicenghoudu" HeaderText="煤层厚度" />
                <asp:BoundField DataField="skjd_dingjie" HeaderText="射孔顶深" />
                <asp:BoundField DataField="skjd_dijie" HeaderText="射孔底深" />
                <asp:BoundField DataField="shekaihoudu" HeaderText="射开厚度" />
                <asp:BoundField DataField="yalieyeleix" HeaderText="压裂液类型" />
                <asp:BoundField DataField="qzy_sheji" HeaderText="设计前置液" />
                <asp:BoundField DataField="qzy_shiji" HeaderText="实际前置液" />
                <asp:BoundField DataField="xsy_sheji" HeaderText="设计携沙液" />
                <asp:BoundField DataField="xsy_shiji" HeaderText="实际携沙液" />
                <asp:BoundField DataField="xsy_zuidiyali" HeaderText="携沙液最低压力" SortExpression="xsy_zuidiyali" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="xsy_zuigaoyali" HeaderText="携沙液最高压力" SortExpression="xsy_zuigaoyali" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="xsy_pingjunyali" HeaderText="携沙液平均压力" SortExpression="xsy_pingjunyali" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="xsy_pingjunpailiang" HeaderText="平均排量" SortExpression="xsy_pingjunpailiang" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="dty_sheji" HeaderText="设计顶替液" SortExpression="dty_sheji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="dty_shiji" HeaderText="实际顶替液" SortExpression="dty_shiji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="zyl_sheji" HeaderText="设计总液量" SortExpression="zyl_sheji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="zyl_shiji" HeaderText="实际总液量" SortExpression="zyl_shiji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jsl_shejizhongxisha" HeaderText="设计中细砂量" SortExpression="jsl_shejizhongxisha" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jsl_shejizhongsha" HeaderText="设计中砂量" SortExpression="jsl_shejizhongsha" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jsl_shejicusha" HeaderText="设计粗砂量" SortExpression="jsl_shejicusha" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jsl_shejizongshaliang" HeaderText="设计加砂总量" SortExpression="jsl_shejizongshaliang" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jsl_shijizhongxisha" HeaderText="实际中细砂量" SortExpression="jsl_shijizhongxisha" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jsl_shijizhongsha" HeaderText="实际中砂量" SortExpression="jsl_shijizhongsha" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jsl_shijicusha" HeaderText="实际粗砂量" SortExpression="jsl_shijicusha" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jsl_shijizongshaliang" HeaderText="实际加砂总量" SortExpression="jsl_shijizongshaliang" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="pjsb_sheji" HeaderText="设计平均砂比" SortExpression="pjsb_sheji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="pjsb_shiji" HeaderText="实际平均砂比" SortExpression="pjsb_shiji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="polieyali" HeaderText="破裂压力" SortExpression="polieyali" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="tingbengyali" HeaderText="停泵压力" SortExpression="tingbengyali" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="dang30miaohoujiangzhi" HeaderText="30min后降至" SortExpression="dang30miaohoujiangzhi" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shifouhege" HeaderText="是否成功" SortExpression="shifouhege" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jianduren" HeaderText="监督人" SortExpression="jianduren" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" SortExpression="shigongduiwu" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="qukuai" HeaderText="区块" />
                <asp:BoundField DataField="shifouyawan" HeaderText="是否压完" SortExpression="shifouyawan" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shigongleixing" HeaderText="压裂工艺方法" SortExpression="shigongleixing" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="wanchengbaifenbi" HeaderText="完成设计砂量百分比"/>
                <asp:BoundField DataField="teshuqingkshuoming" HeaderText="设备运转情况" SortExpression="teshuqingkshuoming" ItemStyle-Width="100px"/>
                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                <asp:BoundField DataField="fujian" HeaderText="附件" SortExpression="fujian" ItemStyle-Width="50px"/>

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
