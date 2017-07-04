<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wjcx.aspx.cs" Inherits="NCCBM.Query.zj.wjcx" %>
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
    <form id="form0" name="form0" method="post" action="">
        <input name="rsHid" type="hidden" id="rsHid" runat="server" />
    </form>
    <form id="form1" runat="server">
    <div>
        <div>
            <table width="850px">
                <tr>
                    <td style="height:35px;background-image:url(../../css/res/current.png);background-repeat:no-repeat;">
                       <script type="text/javascript">
                           function SetTitle() {
                               var sz = document.form0.elements["rsHid"].value;
                               if (sz == "1") {
                                   document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">区块查询</h3>");
                               }
                               if (sz == "2") {
                                   document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">井号查询</h3>");
                               }
                               if (sz == "3") {
                                   document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">设计井深</h3>");
                               }
                               if (sz == "4") {
                                   document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">完钻井深</h3>");
                               }
                               if (sz == "5") {
                                   document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">开钻日期</h3>");
                               }
                               if (sz == "6") {
                                   document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">完钻日期</h3>");
                               }
                               if (sz == "7") {
                                   document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">建井周期</h3>");
                               }
                               if (sz == "8") {
                                   document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">最大井斜</h3>");
                               }
                               if (sz == "9") {
                                   document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">监督人</h3>");
                               }
                               if (sz == "10") {
                                   document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">施工队伍</h3>");
                               }
                           }
                           SetTitle();
		            </script> 
                    </td>
                </tr>
            </table>
            <table class="badtable">
                <tr>
                    <td>查询区块：<asp:DropDownList ID="ddlQukuai" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlQukuai_SelectedIndexChanged"></asp:DropDownList></td>
                    <td id="td1" runat="server">
                        &nbsp;&nbsp;&nbsp;&nbsp;完井日期：<asp:TextBox ID="tbWjrqBegin" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>
                        至<asp:TextBox ID="tbWjrqEnd" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox></td>

                    <td id="td_jh" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;查询井号：<asp:TextBox ID="tbJinghao" runat="server" Width="80px"></asp:TextBox></td>
                    <td id="td_js1" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;设计井深：
                        <asp:DropDownList ID="ddlJs1" runat="server">
                            <asp:Listitem>全部</asp:Listitem>
                            <asp:Listitem>小于500米</asp:Listitem>
                            <asp:Listitem>500-599米</asp:Listitem>
                            <asp:Listitem>600-699米</asp:Listitem>
                            <asp:Listitem>700-799米</asp:Listitem>
                            <asp:Listitem>800-899米</asp:Listitem>
                            <asp:Listitem>900-999米</asp:Listitem>
                            <asp:Listitem>大于999米</asp:Listitem>
                        </asp:DropDownList>
                    </td>
                    <td id="td_js2" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;完钻井深：
                        <asp:DropDownList ID="ddlJs2" runat="server">
                            <asp:Listitem>全部</asp:Listitem>
                            <asp:Listitem>小于500米</asp:Listitem>
                            <asp:Listitem>500-599米</asp:Listitem>
                            <asp:Listitem>600-699米</asp:Listitem>
                            <asp:Listitem>700-799米</asp:Listitem>
                            <asp:Listitem>800-899米</asp:Listitem>
                            <asp:Listitem>900-999米</asp:Listitem>
                            <asp:Listitem>大于999米</asp:Listitem>
                        </asp:DropDownList>
                    </td>
                    <td id="td_kzrq" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;开钻日期：<asp:TextBox ID="tbKzrqBegin" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>
                        至<asp:TextBox ID="tbKzrqEnd" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox></td>
                    <td id="td_wzrq" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;完钻日期：<asp:TextBox ID="tbWzrqBegin" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>
                        至<asp:TextBox ID="tbWzrqEnd" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox></td>
                    <td id="td_jjzq" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;建井周期：
                        <asp:DropDownList ID="ddlJjzq" runat="server">
                            <asp:ListItem Text="全部"></asp:ListItem>
                            <asp:ListItem Text="小于15天"></asp:ListItem>
                            <asp:ListItem Text="15-25天"></asp:ListItem>
                            <asp:ListItem Text="26—40天"></asp:ListItem>
                            <asp:ListItem Text="41—60天"></asp:ListItem>
                            <asp:ListItem Text="61—90天"></asp:ListItem>
                            <asp:ListItem Text="大于90天"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td id="td_zdjx" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;最大井斜：
                        <asp:DropDownList ID="ddlZdjx" runat="server">
                            <asp:ListItem Text="全部"></asp:ListItem>
                            <asp:ListItem Text="小于1.5度"></asp:ListItem>
                            <asp:ListItem Text="1.5-2度"></asp:ListItem>
                            <asp:ListItem Text="2-3.5度"></asp:ListItem>
                            <asp:ListItem Text="大于3.5度"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td id="td_jd" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;监督人：<asp:DropDownList ID="ddlJianduren" runat="server"></asp:DropDownList></td>
                    <td id="td_dw" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;施工队伍：<asp:DropDownList ID="ddlDuiwu" runat="server" Width="150px"></asp:DropDownList></td>

                    <script type="text/javascript">
                        function SetWhere() {
                            var sz = document.form0.elements["rsHid"].value;
                            if (sz == "1") {
                                document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js1.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js2.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_kzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_wzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jjzq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_zdjx.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                            }
                            if (sz == "2") {
                                document.getElementById("<%= td_js1.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js2.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_kzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_wzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jjzq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_zdjx.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                            }
                            if (sz == "3") {
                                document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js2.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_kzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_wzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jjzq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_zdjx.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                            }
                            if (sz == "4") {
                                document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js1.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_kzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_wzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jjzq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_zdjx.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                            }
                            if (sz == "5") {
                                document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js1.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js2.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_wzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jjzq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_zdjx.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                            }
                            if (sz == "6") {
                                document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js1.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js2.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_kzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jjzq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_zdjx.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                            }
                            if (sz == "7") {
                                document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js1.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js2.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_kzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_wzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_zdjx.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                            }
                            if (sz == "8") {
                                document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js1.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js2.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_kzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_wzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jjzq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                            }
                            if (sz == "9") {
                                document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js1.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js2.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_kzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_wzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jjzq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_zdjx.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                            }
                            if (sz == "10") {
                                document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js1.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_js2.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_kzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_wzrq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jjzq.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_zdjx.ClientID%>").style.display = "none";
                                document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                            }
                        }
                        SetWhere();
		        </script>

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
