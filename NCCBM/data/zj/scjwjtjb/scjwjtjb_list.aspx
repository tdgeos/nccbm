<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scjwjtjb_list.aspx.cs" Inherits="NCCBM.data.zj.scjwjtjb.scjwjtjb_list" %>
<%@ Register Src="../../../Menu/ActionControl.ascx" TagName="ActionControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="X1.Pager" Assembly="X1.Pager" %>
<%@ Register TagPrefix="cc1" Namespace="NCCBM" Assembly="NCCBM" %>
<%@ Register Src="../../../MyPagerBar.ascx" TagName="myPager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
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
            $("#GridView2").toSuperTable({ width: "845px", height: "337px", fixedCols: 4, headerRows: 3 }).find("tr:even").addClass("altRow");
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
        <asp:Label ID="Label3" runat="server" Text="完井日期:"></asp:Label>
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
                <asp:BoundField DataField="id" />
                <asp:BoundField DataField="fuzejiandu" />
                <asp:BoundField DataField="shigongduiwu" />
                <asp:BoundField DataField="jinghao" />
                <asp:BoundField DataField="wanzuanjingshen" />
                <asp:BoundField DataField="kaizuanriqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="wanzuanriqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="wanjingshijian" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="zuanjingzhouqi" />
                <asp:BoundField DataField="zuanjixinghao" />
                <asp:BoundField DataField="ykys_riqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="ykys_cunzaiwenti" />
                <asp:BoundField DataField="ykys_shifoutongyikaizuan" />
                <asp:BoundField DataField="ykys_fuyanqingkuang" />
                <asp:BoundField DataField="ekys_riqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="ekys_cunzaiwenti" />
                <asp:BoundField DataField="ekys_shifoutongyikaizuan" />
                <asp:BoundField DataField="ekys_fuyanqingkuang" />
                <asp:BoundField DataField="btsj_biaotaoxiashen" />
                <asp:BoundField DataField="sctgsj_changjiagangji" />
                <asp:BoundField DataField="sctgsj_waiguanjiancha" />
                <asp:BoundField DataField="sctgsj_bianhaohecha" />
                <asp:BoundField DataField="sctgsj_taoguanxiashen" />
                <asp:BoundField DataField="sctgsj_rujingchangtao" />
                <asp:BoundField DataField="sctgsj_shengyuchangtao" />
                <asp:BoundField DataField="sctgsj_rujingduantao" />
                <asp:BoundField DataField="sctgsj_shengyuduantao" />
                <asp:BoundField DataField="sctgsj_duantaoweizhi" />
                <asp:BoundField DataField="qxsj_quxinhuici" />
                <asp:BoundField DataField="qxsj_zongshouhuolv" />
                <asp:BoundField DataField="gjsgsj_gujingdui" />
                <asp:BoundField DataField="gjsgsj_qianzhiye" />
                <asp:BoundField DataField="gjsgsj_zhushuinijiangliang" />
                <asp:BoundField DataField="gjsgsj_tijingliang" />
                <asp:BoundField DataField="gjsgsj_pengyaqingkuang" />
                <asp:BoundField DataField="gjsgsj_shuinijiangmidu" />
                <asp:BoundField DataField="sy_riqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="sy_yajiangqingkuang" />
                <asp:BoundField DataField="wjjc_jingkoushuiping" />
                <asp:BoundField DataField="wjjc_jingkougaodu" />
                <asp:BoundField DataField="wjjc_jingkouhanjie" />
                <asp:BoundField DataField="beizhu1" />
                <asp:BoundField DataField="bianhao" />
                <asp:BoundField DataField="xiaojieriqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="jsjg_yikai" />
                <asp:BoundField DataField="jsjg_biaotao" />
                <asp:BoundField DataField="jsjg_erkai" />
                <asp:BoundField DataField="jsjg_chantao" />
                <asp:BoundField DataField="mcsj_3" />
                <asp:BoundField DataField="mcsj_5" />
                <asp:BoundField DataField="mcsj_11" />
                <asp:BoundField DataField="mcsj_qitameiceng" />
                <asp:BoundField DataField="jszlsj_baxinju" />
                <asp:BoundField DataField="jszlsj_zuidajingxie" />
                <asp:BoundField DataField="jszlsj_zuidaweiyi" />
                <asp:BoundField DataField="jszlsj_zuidaquanjiao" />
                <asp:BoundField DataField="jszlsj_zxd1_zuidalianxu" />
                <asp:BoundField DataField="jszlsj_zxd1_lianxusandian" />
                <asp:BoundField DataField="jszlsj_zxd2_zuidalianxu" />
                <asp:BoundField DataField="jszlsj_zxd2_lianxusandian" />
                <asp:BoundField DataField="jszlsj_quanjingduan" />
                <asp:BoundField DataField="jszlsj_meicengduan" />
                <asp:BoundField DataField="xtggjsj_dctg_duantaoshejiweizhi" />
                <asp:BoundField DataField="xtggjsj_dctg_duantaoshiceweizhi" />
                <asp:BoundField DataField="xtggjsj_dctg_zuliuhuanshendu" />
                <asp:BoundField DataField="xtggjsj_yuzushendu" />
                <asp:BoundField DataField="xtggjsj_dctg_meicengduanjieguweizhi" />
                <asp:BoundField DataField="xtggjsj_gjqk_shuinifanshen" />
                <asp:BoundField DataField="xtggjsj_diancepingjia" />
                <asp:BoundField DataField="beizhu2" />
                <asp:BoundField DataField="jbsj_jingbie" />
                <asp:BoundField DataField="jbsj_jingxing" />
                <asp:BoundField DataField="jbsj_diliweizhi" />
                <asp:BoundField DataField="jbsj_shejijingshen" />
                <asp:BoundField DataField="sjjsjg_yikai" />
                <asp:BoundField DataField="sjjsjg_taoguanxiashen" />
                <asp:BoundField DataField="sjjsjg_erkai" />
                <asp:BoundField DataField="sjjsjg_chantaoxiashen" />
                <asp:BoundField DataField="dxsj_jingkouzuobiao_x" />
                <asp:BoundField DataField="dxsj_jingkouzuobiao_y" />
                <asp:BoundField DataField="dxsj_jingkouhaiba" />
                <asp:BoundField DataField="dxsj_jingdichuishen" />
                <asp:BoundField DataField="dxsj_badianzuobiao_x" />
                <asp:BoundField DataField="dxsj_badianzuobiao_y" />
                <asp:BoundField DataField="dxsj_badianfangwei" />
                <asp:BoundField DataField="dxsj_badianchuishen" />
                <asp:BoundField DataField="dxsj_badianweiyi" />
                <asp:BoundField DataField="dxsj_cipianjiao" />
                <asp:BoundField DataField="dxsj_damenfangxiang" />
                <asp:BoundField DataField="dxsj_zaoxieduan" />
                <asp:BoundField DataField="dxsj_shejijingxie" />
                <asp:BoundField DataField="place" HeaderText="区块" />
                <asp:BoundField DataField="fujian" />

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
