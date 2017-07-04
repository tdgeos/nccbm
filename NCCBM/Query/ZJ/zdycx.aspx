<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zdycx.aspx.cs" Inherits="NCCBM.Query.zj.zdycx" %>
<%@ Register Src="../../MyPagerBar.ascx" TagName="myPager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href ="../../css/web.css" type ="text/css" rel ="Stylesheet" />
    <link href="../../FusionCharts/ui-lightness/jquery-ui-1.8.20.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    
    <script src="../../FusionCharts/JS/FusionCharts.js" type="text/javascript"></script>
    <link href="../../css/jquery-ui-1.8.20.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui.zh-CN.js" type="text/javascript"></script>
    <script src="../../js/datetime.js" type="text/javascript"></script>
    <script src="../../js/superTables.js" type="text/javascript"></script>
    <script src="../../js/jquery.superTable.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(function () {
            $("#GridView2").toSuperTable({ width: "845px", height: "337px", fixedCols: 4, headerRows: 3 }).find("tr:even").addClass("altRow");
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
    <div>
        <div>
            <table width="850px">
                <tr>
                    <td style="height:35px;background-image:url(../../css/res/current.png);background-repeat:no-repeat;">
                        <h3 style="color:#eee; margin-top:7px; margin-left:15px;">自定义查询</h3>
                    </td>
                </tr>
            </table>
            <table class="badtable">
                <tr>
                    <td style="width:240px">查询区块：<asp:DropDownList ID="ddlQukuai" runat="server"></asp:DropDownList></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;一开验收日期：<asp:TextBox ID="tbYikaiBegin" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>
                        至<asp:TextBox ID="tbYikaiEnd" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;一开验收是否通过：
                        <asp:DropDownList ID="ddlYikai" runat="server">
                            <asp:Listitem>全部</asp:Listitem>
                            <asp:Listitem>是</asp:Listitem>
                            <asp:Listitem>否</asp:Listitem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>井身质量是否超标：
                        <asp:DropDownList ID="ddlJszl" runat="server">
                            <asp:Listitem>全部</asp:Listitem>
                            <asp:Listitem>是</asp:Listitem>
                            <asp:Listitem>否</asp:Listitem>
                        </asp:DropDownList></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;二开验收日期：<asp:TextBox ID="tbErkaiBegin" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>
                        至<asp:TextBox ID="tbErkaiEnd" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;二开验收是否通过：
                        <asp:DropDownList ID="ddlErkai" runat="server">
                            <asp:Listitem>全部</asp:Listitem>
                            <asp:Listitem>是</asp:Listitem>
                            <asp:Listitem>否</asp:Listitem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>下套管作业是否异常：
                        <asp:DropDownList ID="ddlXtg" runat="server">
                            <asp:Listitem>全部</asp:Listitem>
                            <asp:Listitem>是</asp:Listitem>
                            <asp:Listitem>否</asp:Listitem>
                        </asp:DropDownList></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;固井作业是否异常：
                        <asp:DropDownList ID="ddlGj" runat="server">
                            <asp:Listitem>全部</asp:Listitem>
                            <asp:Listitem>是</asp:Listitem>
                            <asp:Listitem>否</asp:Listitem>
                        </asp:DropDownList></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;是否出现漏失：
                        <asp:DropDownList ID="ddlJl" runat="server">
                            <asp:Listitem>全部</asp:Listitem>
                            <asp:Listitem>是</asp:Listitem>
                            <asp:Listitem>否</asp:Listitem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>是否出现涌水：
                        <asp:DropDownList ID="ddlYs" runat="server">
                            <asp:Listitem>全部</asp:Listitem>
                            <asp:Listitem>是</asp:Listitem>
                            <asp:Listitem>否</asp:Listitem>
                        </asp:DropDownList></td>
                    <td> 
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="btnQuery" runat="server" Text="查询" OnClick="Query_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button CssClass="button-xls" ForeColor="White" Font-Size="Small" ID="btnExtport" runat="server" Text="输出Excel" OnClick="Extport_Click"/>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="result_number" runat="server"></asp:Label>
                        <asp:Label ID="lblWhere" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        
        <asp:GridView ID="GridView2" 
            CssClass="Table100" 
            AutoGenerateColumns="false" 
            runat="server" 
            AllowSorting="True" 
            AllowPaging="True" 
            BorderStyle="Solid" 
            BorderWidth="1" 
            ShowHeader="false" 
            OnPageIndexChanging="Page_IndexChanging" 
            OnRowDataBound="GridViewRowBound" 
            OnRowCreated="gridView_RowCreated" 
            PagerSettings-Visible="false">

            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle CssClass="tableLine" />
            <EditRowStyle BackColor="#9EB6CE" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle CssClass="tableLineHeader" />
            <AlternatingRowStyle CssClass="tableLineAlternating" />

            <Columns >
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
                <asp:BoundField DataField="place" />
                <asp:BoundField DataField="fujian" />
            </Columns>
        </asp:GridView>
        <div>
            <uc1:myPager ID="commPage1" runat="server"/>
        </div>
    </div>
    </form>
</body>
</html>
