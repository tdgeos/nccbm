<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gj_list.aspx.cs" Inherits="NCCBM.data.zj.gj.gj_list" %>
<%@ Register Src="../../../Menu/ActionControl.ascx" TagName="ActionControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="X1.Pager" Assembly="X1.Pager" %>
<%@ Register TagPrefix="cc1" Namespace="NCCBM" Assembly="NCCBM" %>
<%@ Register Src="../../../MyPagerBar.ascx" TagName="myPager" TagPrefix="uc1" %>

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
                <asp:BoundField DataField="id" HeaderText="序号" />
                <asp:BoundField DataField="riqi" HeaderText="上报日期" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="fuzejiandu" HeaderText="负责监督" />
                <asp:BoundField DataField="duihao" HeaderText="队号" />
                <asp:BoundField DataField="jinghao" HeaderText="井号" />
                <asp:BoundField DataField="wanzuanjingshen" HeaderText="完钻井深" />
                <asp:BoundField DataField="shigongriqi" HeaderText="施工日期" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="fangao_sheji" HeaderText="设计水泥返高" />
                <asp:BoundField DataField="fangao_shiji" HeaderText="实际水泥返高" />
                <asp:BoundField DataField="midu_sheji" HeaderText="设计水泥浆密度" />
                <asp:BoundField DataField="midu_shiji" HeaderText="实际水泥浆密度" />
                <asp:BoundField DataField="jiangyongliang_sheji" HeaderText="设计水泥浆用量" />
                <asp:BoundField DataField="jiangyongliang_shiji" HeaderText="实际水泥浆用量" />
                <asp:BoundField DataField="yongliang_sheji" HeaderText="设计水泥用量" />
                <asp:BoundField DataField="yongliang_shiji" HeaderText="实际水泥用量" />
                <asp:BoundField DataField="dingtiliang_sheji" HeaderText="设计顶替量" />
                <asp:BoundField DataField="dingtiliang_shiji" HeaderText="实际顶替量" />
                <asp:BoundField DataField="pengya_sheji" HeaderText="设计碰压" />
                <asp:BoundField DataField="pengya_shiji" HeaderText="实际碰压" />
                <asp:BoundField DataField="shigongwenti" HeaderText="施工中存在问题" />
                <asp:BoundField DataField="fuzaqingkuang" HeaderText="复杂情况" />
                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                <asp:BoundField DataField="jiancefangshi" HeaderText="检测方式" />
                <asp:BoundField DataField="place" HeaderText="区块" />
                <asp:BoundField DataField="fujian" HeaderText="附件" />

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
