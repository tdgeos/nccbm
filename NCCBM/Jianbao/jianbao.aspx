<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jianbao.aspx.cs" Inherits="NCCBM.data.jianbao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head  id="Head1" runat="server">
<title></title>
    <link href="../css/superTables.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.20.custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.zh-CN.js" type="text/javascript"></script>
    <script src="../js/datetime.js" type="text/javascript"></script>

    <script runat="server" language="c#">
        public void GridViewRowBound(object sender,GridViewRowEventArgs e)
        {
            NCCBM.EXGridView.GridViewRowBound(sender, e);
        } 
    </script>
</head>

<body> 
    <form id="form1" runat="server" visible="true" style="height:430px; overflow:auto;" >
        <table width="830px" cellpadding="0" cellspacing="0" style="margin-top:5px;">
            <tr style="height:35px;background-image:url(../../css/res/current.png);background-repeat:no-repeat;">
                <td style="width:150px;">
                   <h3 style="color:#eee; margin-top:7px; margin-left:10px;">质量简报</h3>
                </td>
                <td>
                    <asp:Button CssClass="button-xls" ForeColor="White" Font-Size="Small" ID="Button1" runat="server" Text="查询周报" onclick="Button_Zhou_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button-xls" ForeColor="White" Font-Size="Small" ID="Button3" runat="server" Text="查询月报" onclick="Button_Yue_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button-xls" ForeColor="White" Font-Size="Small" ID="Button4" runat="server" Text="查询季报" onclick="Button_Ji_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button-xls" ForeColor="White" Font-Size="Small" ID="Button5" runat="server" Text="查询年报" onclick="Button_Nian_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button-xls" ForeColor="White" Font-Size="Small" ID="Button2" runat="server" Text="简报输出" OnClick="Button_Out_Click"/>
                </td>
            </tr>
        </table>

        <table width="830px" border="0" cellpadding="0" cellspacing="0" >
            <tr >
                <td style=" height: 30px;">
                    统计日期：
                    <asp:TextBox ID="tbRiqi"  runat="server" CssClass="inputwidth datepicker" Width="80px"></asp:TextBox>
                    <asp:Label ID="lblType" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>

        <table>
            <tr>
                <asp:Label ID="Label3" runat="server" Text="钻井总体情况" Width="820px" style="text-align:center; font-size:14px;"></asp:Label>
            </tr>
            <tr>
                <asp:Label ID="lblZtqkZj" runat="server" Text="" Width="820px" Font-Size="Small" style="text-align:center;"></asp:Label>
            </tr>
            <tr>
                <asp:GridView ID="grd_zj" runat="server" Width="820px" RowStyle-HorizontalAlign="Center" ></asp:GridView>
            </tr>
        </table>

        <br />

        <table>
            <tr>
                <asp:Label ID="yalie" runat="server" Text="压裂总体情况" Width="820px" style="text-align:center; font-size:14px;"></asp:Label>
            </tr>
            <tr>
                <asp:Label ID="lblZtqkYl" runat="server" Text="" Width="820px" Font-Size="Small" style="text-align:center;"></asp:Label>
            </tr>
            <tr>
                <asp:GridView ID="grd_yl" runat="server" Width="820px" OnRowCreated="grd_yl_RowCreated" ShowHeader="false" RowStyle-HorizontalAlign="Center"></asp:GridView>
            </tr>
        </table>

        <br />

        <table>
            <tr>
                <asp:Label ID="Label5" runat="server" Text="钻井质量监督" Width="820px" style="text-align:center; font-size:14px;"></asp:Label>
            </tr>
            <tr>
                <asp:Label ID="lblZuanjingRiqi" runat="server" Text="" Width="820px" Font-Size="Small" style="text-align:center;"></asp:Label>
            </tr>
            <tr>
                <td>
                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="820px" Height="290px" ScrollBars="Vertical">
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="开钻验收">
                        <ContentTemplate>
                            <asp:Label ID="lblKzysInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_yierkaiyanshou" runat="server" Width="790px"
                                BorderColor="Black" BorderWidth="1px" BorderStyle="Solid"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="yierkaiyanshou_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yikaishijian" HeaderText="一开验收日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="erkaishijian" HeaderText="二开验收日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yinsu" HeaderText="不合格因素（人员、证件、设备等）" ItemStyle-Width="190px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="井身质量">
                        <ContentTemplate>
                            <asp:Label ID="lblJszlInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_jingshenzhiliang" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="jingshenzhiliang_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yinsu" HeaderText="超标因素（井斜、狗腿度、方位、位移等）" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="下套管作业">
                        <ContentTemplate>
                            <asp:Label ID="lblXtgInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_xiataoguan" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="xiataoguan_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yichang" HeaderText="异常情况（套管单、现场目测等）" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="固井作业">
                        <ContentTemplate>
                            <asp:Label ID="lblGjInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_gujingzuoye" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="gujingzuoye_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yichang" HeaderText="异常情况（套管单、现场目测等）" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="处理井漏">
                        <ContentTemplate>
                            <asp:Label ID="lblJinglouInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_chulijinglou" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="chulijinglou_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="loushicengwei" HeaderText="漏失层位" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="loushiliang" HeaderText="漏失量" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jieguo" HeaderText="处理结果" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="处理涌水">
                        <ContentTemplate>
                            <asp:Label ID="lblYongshuiInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_chuliyongshui" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="chuliyongshui_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yongshuicengwei" HeaderText="涌水层位" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yongshuiliang" HeaderText="涌水量" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jieguo" HeaderText="处理结果" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
                </td>
            </tr>
        </table>

        <br />

        <table>
            <tr>
                <asp:Label ID="Label6" runat="server" Text="压裂质量监督" Width="820px" style="text-align:center; font-size:14px;"></asp:Label>
            </tr>
            <tr>
                <asp:Label ID="lblYalieRiqi" runat="server" Text="" Width="820px" Font-Size="Small" style="text-align:center;"></asp:Label>
            </tr>
            <tr>
                <td>
                <cc1:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" Width="820px" Height="290px" ScrollBars="Vertical">
                    <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="阻工因素">
                        <ContentTemplate>
                            <asp:GridView ID="grd_zugongyinsu" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="zugongyinsu_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="taoguanzhiliangwenti" HeaderText="套管质量影响天数" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="xiayu" HeaderText="下雨及其影响天数" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="gongnongguanxi" HeaderText="工农关系影响天数" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cheliangweixiu" HeaderText="车辆维修天数" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jingchangbanqian" HeaderText="井场搬迁" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="dengdaijingtaibanqian" HeaderText="等待井台搬迁" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beishuipeiye" HeaderText="备水配液" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel8" runat="server" HeaderText="入井材料">
                        <ContentTemplate>
                            <asp:GridView ID="grd_rujingcailiao" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="rujingcailiao_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="f1" HeaderText="区块" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="f2" HeaderText="压裂队" ItemStyle-Width="170px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="f3" HeaderText="100%" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="f4" HeaderText="90%-100%" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="f5" HeaderText="80%-90%" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="f6" HeaderText="80%以下" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel9" runat="server" HeaderText="施工质量">
                        <ContentTemplate>
                            <asp:Label ID="lblSgzlInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_shigongzhiliang" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="shigongzhiliang_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="place" HeaderText="区块" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cenghao" HeaderText="层位" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongmiaoshu" HeaderText="失败原因简述" ItemStyle-Width="370px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="fujian" HeaderText="备注" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
                </td>
            </tr>
        </table>

        <br />

        <table>
            <tr>
                <asp:Label ID="Label7" runat="server" Text="技术研究及其他工作" Width="820px" style="text-align:center; font-size:14px;"></asp:Label>
            </tr>
            <tr>
                <asp:Label ID="lblJishuyanjiuRiqi" runat="server" Text="" Width="820px" Font-Size="Small" style="text-align:center;"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="韩城" Width="820px" Font-Size="10pt" style="text-align:center;"></asp:Label>
                <asp:TextBox ID="txt_report_hc" runat="server" TextMode="MultiLine" Width="810px" Height="77px"></asp:TextBox>

                <asp:Label ID="Label1" runat="server" Text="临汾" Width="820px" Font-Size="10pt" style="text-align:center;"></asp:Label>
                <asp:TextBox ID="txt_report_lf" runat="server" TextMode="MultiLine" Width="810px" Height="77px"></asp:TextBox>

                <asp:Label ID="Label2" runat="server" Text="忻州" Width="820px" Font-Size="10pt" style="text-align:center;"></asp:Label>
                <asp:TextBox ID="txt_report_xz" runat="server" TextMode="MultiLine" Width="810px" Height="77px"></asp:TextBox>
            </tr>
        </table>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <%--<cc1:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0" Height="340px">
            <cc1:TabPanel ID="TabPanel_GCzongtiqingkuang" runat="server" HeaderText="工程总体情况">
                <ContentTemplate>
                    <asp:Label ID="lblZtqkRiqi" runat="server" Text="" Width="820px" Font-Size="Small" style="text-align:center;"></asp:Label>
                    <asp:Label ID="zuanjing" runat="server" Text="钻井" Width="820px" style="text-align:center;"></asp:Label>
                    <asp:GridView ID="grd_zj" runat="server" Width="820px" RowStyle-HorizontalAlign="Center" ></asp:GridView>

                    <br />

                    <asp:Label ID="yalie" runat="server" Text="压裂" Width="820px" style="text-align:center;"></asp:Label>
                    <asp:GridView ID="grd_yl" runat="server" Width="820px" OnRowCreated="grd_yl_RowCreated" ShowHeader="false" RowStyle-HorizontalAlign="Center"></asp:GridView>
                </ContentTemplate>
            </cc1:TabPanel>

            <cc1:TabPanel ID="TabPanel_zj_dxj" runat="server" HeaderText="钻井质量监督" Width="830px">
                <ContentTemplate>
                <asp:Label ID="lblZuanjingRiqi" runat="server" Text="" Width="820px" Font-Size="Small" style="text-align:center;"></asp:Label>

                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="820px" Height="290px" ScrollBars="Vertical">
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="开钻验收">
                        <ContentTemplate>
                            <asp:Label ID="lblKzysInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_yierkaiyanshou" runat="server" Width="790px"
                                BorderColor="Black" BorderWidth="1px" BorderStyle="Solid"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="yierkaiyanshou_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yikaishijian" HeaderText="一开验收日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="erkaishijian" HeaderText="二开验收日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yinsu" HeaderText="不合格因素（人员、证件、设备等）" ItemStyle-Width="190px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="井身质量">
                        <ContentTemplate>
                            <asp:Label ID="lblJszlInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_jingshenzhiliang" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="jingshenzhiliang_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yinsu" HeaderText="超标因素（井斜、狗腿度、方位、位移等）" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="下套管作业">
                        <ContentTemplate>
                            <asp:Label ID="lblXtgInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_xiataoguan" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="xiataoguan_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yichang" HeaderText="异常情况（套管单、现场目测等）" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="固井作业">
                        <ContentTemplate>
                            <asp:Label ID="lblGjInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_gujingzuoye" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="gujingzuoye_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yichang" HeaderText="异常情况（套管单、现场目测等）" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="处理井漏">
                        <ContentTemplate>
                            <asp:Label ID="lblJinglouInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_chulijinglou" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="chulijinglou_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="loushicengwei" HeaderText="漏失层位" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="loushiliang" HeaderText="漏失量" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jieguo" HeaderText="处理结果" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="处理涌水">
                        <ContentTemplate>
                            <asp:Label ID="lblYongshuiInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_chuliyongshui" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="chuliyongshui_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongduiwu" HeaderText="施工队伍" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jiandu" HeaderText="监督人" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yongshuicengwei" HeaderText="涌水层位" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="yongshuiliang" HeaderText="涌水量" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cuoshi" HeaderText="处理措施" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jieguo" HeaderText="处理结果" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
                </ContentTemplate>
            </cc1:TabPanel>

            <cc1:TabPanel ID="TabPanel10" runat="server" HeaderText="压裂质量监督" Width="830px">
                <ContentTemplate>
                <asp:Label ID="lblYalieRiqi" runat="server" Text="" Width="820px" Font-Size="Small" style="text-align:center;"></asp:Label>

                <cc1:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" Width="820px" Height="290px" ScrollBars="Vertical">
                    <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="阻工因素">
                        <ContentTemplate>
                            <asp:GridView ID="grd_zugongyinsu" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="zugongyinsu_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="taoguanzhiliangwenti" HeaderText="套管质量影响天数" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="xiayu" HeaderText="下雨及其影响天数" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="gongnongguanxi" HeaderText="工农关系影响天数" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cheliangweixiu" HeaderText="车辆维修天数" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jingchangbanqian" HeaderText="井场搬迁" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="dengdaijingtaibanqian" HeaderText="等待井台搬迁" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beishuipeiye" HeaderText="备水配液" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel8" runat="server" HeaderText="入井材料">
                        <ContentTemplate>
                            <asp:GridView ID="grd_rujingcailiao" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="rujingcailiao_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="f1" HeaderText="区块" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="f2" HeaderText="压裂队" ItemStyle-Width="170px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="f3" HeaderText="100%" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="f4" HeaderText="90%-100%" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="f5" HeaderText="80%-90%" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="f6" HeaderText="80%以下" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel9" runat="server" HeaderText="施工质量">
                        <ContentTemplate>
                            <asp:Label ID="lblSgzlInfo" runat="server" Text="" Width="790px" ></asp:Label>
                            <asp:GridView ID="grd_shigongzhiliang" runat="server" Width="790px"
                                AutoGenerateColumns="False" 
                                CssClass="Table100" AllowPaging="true" 
                                OnPageIndexChanging="shigongzhiliang_IndexChanging" 
                                OnRowDataBound="GridViewRowBound" ShowHeader="true">
            
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tableLine" />
                                <EditRowStyle BackColor="#9EB6CE" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="tableLineHeader" />
                                <AlternatingRowStyle CssClass="tableLineAlternating" />
                                <Columns>
                                    <asp:BoundField DataField="place" HeaderText="区块" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="cenghao" HeaderText="层位" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="shigongmiaoshu" HeaderText="失败原因简述" ItemStyle-Width="370px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                    <asp:BoundField DataField="fujian" HeaderText="备注" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="12px"/>
                                </Columns>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                                    PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
                </ContentTemplate>
            </cc1:TabPanel>

            <cc1:TabPanel ID="TabPanel_zh_zhijing" runat="server" HeaderText="技术研究及其他工作" Width="200px"  ScrollBars="Both">
                <ContentTemplate>
                    <asp:Label ID="lblJishuyanjiuRiqi" runat="server" Text="" Width="820px" Font-Size="Small" style="text-align:center;"></asp:Label>
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="韩城" Width="820px" Font-Size="10pt" style="text-align:center;"></asp:Label>
                    <asp:TextBox ID="txt_report_hc" runat="server" TextMode="MultiLine" Width="810px" Height="77px"></asp:TextBox>
                    <br /><br />

                    <asp:Label ID="Label1" runat="server" Text="临汾" Width="820px" Font-Size="10pt" style="text-align:center;"></asp:Label>
                    <asp:TextBox ID="txt_report_lf" runat="server" TextMode="MultiLine" Width="810px" Height="77px"></asp:TextBox>
                    <br /><br />

                    <asp:Label ID="Label2" runat="server" Text="忻州" Width="820px" Font-Size="10pt" style="text-align:center;"></asp:Label>
                    <asp:TextBox ID="txt_report_xz" runat="server" TextMode="MultiLine" Width="810px" Height="77px"></asp:TextBox>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>--%>

        <asp:Label ID="dataZuanjing" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="dataYalie" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="dataKzys" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="dataJszl" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="dataXtgzy" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="dataGjzy" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="dataCljl" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="dataClys" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="dataZgys" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="dataRjcl" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="dataSgzl" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblRiqi" runat="server" Text="" Visible="false"></asp:Label>
    </form>
</body>
</html>
