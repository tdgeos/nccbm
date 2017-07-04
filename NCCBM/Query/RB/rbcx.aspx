<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rbcx.aspx.cs" Inherits="NCCBM.Query.rb.rbcx" %>
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
        <table width="850px">
            <tr>
                <td style="height:35px;background-image:url(../../css/res/current.png);background-repeat:no-repeat;" >
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
                                document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">监督人</h3>");
                            }
                            if (sz == "4") {
                                document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">施工队伍</h3>");
                            }
                            if (sz == "5") {
                                document.write("<h3 style=\"color:#eee; margin-top:7px; margin-left:15px;\">自定义</h3>");
                            }
                        }
                        SetTitle();
		            </script> 
                </td>
            </tr>
        </table>
        <table class="badtable">
            <tr>
                <td>查询区块：<asp:DropDownList ID="ddlQukuai" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlQukuai_SelectedIndexChanged"></asp:DropDownList></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;查询日期：<asp:TextBox ID="tbRiqiBegin" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>
                            至<asp:TextBox ID="tbRiqiEnd" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox></td>
                <td id="td_jh" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;查询井号：<asp:TextBox ID="tbJinghao" runat="server" Width="80px"></asp:TextBox></td>
                <td id="td_jd" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;监督人：<asp:DropDownList ID="ddlJianduren" runat="server"></asp:DropDownList></td>
                <td id="td_dw" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;施工队伍：<asp:DropDownList ID="ddlDuiwu" runat="server"></asp:DropDownList></td>

                <script type="text/javascript">
                    function SetWhere() {
                        var sz = document.form0.elements["rsHid"].value;
                        //alert(sz);
                        if (sz == "1") {
                            document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                            document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                            document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                        }
                        if (sz == "2") {
                            document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                            document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                        }
                        if (sz == "3") {
                            document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                            document.getElementById("<%= td_dw.ClientID%>").style.display = "none";
                        }
                        if (sz == "4") {
                            document.getElementById("<%= td_jh.ClientID%>").style.display = "none";
                            document.getElementById("<%= td_jd.ClientID%>").style.display = "none";
                        }
                        if (sz == "5") {
                        }
                    }
                    SetWhere();
		        </script>

                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="query_btn" runat="server" 
                        Text="查询" OnClick="Query_Click" />
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

        <asp:GridView ID="gv" 
            CssClass="Table100" 
            AutoGenerateColumns="false" 
            runat="server" 
            AllowSorting="True" 
            AllowPaging="True" 
            BorderStyle="Solid" 
            BorderWidth="1" 
            ShowHeader="true" 
            OnPageIndexChanging="Page_IndexChanging" 
            OnRowDataBound="GridViewRowBound" 
            OnRowCommand="GridView_RowCommand" 
            PagerSettings-Visible="true" Width="840px" RowStyle-HorizontalAlign="Center">

            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle CssClass="tableLine" />
            <EditRowStyle BackColor="#9EB6CE" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle CssClass="tableLineHeader" />
            <AlternatingRowStyle CssClass="tableLineAlternating" />

            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                PageButtonCount="10" PreviousPageText="上一页"  Position="Bottom"/>

            <Columns >
                <asp:BoundField DataField="id" HeaderText="序号" ItemStyle-Width="50px" />
                <asp:BoundField DataField="fuzejiandu" HeaderText="负责监督" ItemStyle-Width="120px" />
                <asp:BoundField DataField="duihao" HeaderText="队号" ItemStyle-Width="120px" />
                <asp:BoundField DataField="jinghao" HeaderText="井号" ItemStyle-Width="100px" />
                <asp:BoundField DataField="muqianjingshen" HeaderText="目前井深" ItemStyle-Width="80px" />
                <asp:BoundField DataField="dangrijinchi" HeaderText="当日进尺" ItemStyle-Width="80px" />
                <asp:BoundField DataField="gongkuang" HeaderText="工况" ItemStyle-Width="150px" />
                <asp:BoundField DataField="fuzaqingkuang" HeaderText="复杂情况" ItemStyle-Width="150px" />
                <asp:BoundField DataField="riqi" HeaderText="日期" ItemStyle-Width="80px" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="80px" />
                <asp:BoundField DataField="fujian" HeaderText="附件" ItemStyle-Width="80px" />
                <asp:TemplateField HeaderText="明细" ItemStyle-Width="80px" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnInfo" runat="server" CausesValidation="False" Text="详细信息" CommandName="info" Visible="true"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField> 
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
