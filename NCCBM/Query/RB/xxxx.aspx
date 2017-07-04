<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xxxx.aspx.cs" Inherits="NCCBM.Query.rb.xxxx" %>
<%@ Register Src="../../MyPagerBar.ascx" TagName="myPager" TagPrefix="uc1" %>

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

    <script type="text/javascript">        //565
        $(function () {
            $("#gv_zj").toSuperTable({ width: "845px", height: "315px", fixedCols: 5, headerRows: 2 }).find("tr:even").addClass("altRow");
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $("#gv_xtg").toSuperTable({ width: "845px", height: "291px", fixedCols: 5 }).find("tr:even").addClass("altRow");
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $("#gv_gj").toSuperTable({ width: "845px", height: "315px", fixedCols: 5, headerRows: 2 }).find("tr:even").addClass("altRow");
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $("#gv_wj").toSuperTable({ width: "845px", height: "315px", fixedCols: 5, headerRows: 2 }).find("tr:even").addClass("altRow");
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
        <table width="850px">
            <tr>
                <td align="center"><h3>钻井日报查询 - 详细信息</h3></td>
            </tr>
        </table>

        <table>
            <tr>
                <td align="center" style="width:800px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnZj" runat="server" ForeColor="#0000ff" Font-Underline="true" >钻进</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnXtg" runat="server" ForeColor="#0000ff" Font-Underline="true" >下套管</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnGj" runat="server" ForeColor="#0000ff" Font-Underline="true" >固井</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnWj" runat="server" ForeColor="#0000ff" Font-Underline="true" >完井</asp:LinkButton>
                    <br />
                </td>
                <td>
                    <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" 
                        ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" />
                </td>
            </tr>
        </table>

        <br />
        <asp:Label ID="Label1" runat="server" Text="没有查询到数据。" Visible="false" Width="800px" style=" margin-left:50px;"></asp:Label>

        <asp:GridView ID="gv_zj" 
            runat="server" 
            AutoGenerateColumns="False" 
            CssClass="Table100" 
            AllowPaging="true" 
            OnPageIndexChanging="gv_zj_Page_IndexChanging" 
            OnRowCreated="gv_zj_RowCreated" 
            OnRowDataBound="GridViewRowBound" 
            ShowHeader="false" 
            PagerSettings-Visible="false" 
            Visible="false">

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
            </Columns>
        </asp:GridView>

        <asp:GridView ID="gv_xtg" 
            runat="server" 
            AutoGenerateColumns="False" 
            CssClass="Table100" 
            AllowPaging="true" 
            OnPageIndexChanging="gv_xtg_Page_IndexChanging" 
            OnRowCreated="gv_xtg_RowCreated" 
            OnRowDataBound="GridViewRowBound" 
            ShowHeader="false" 
            PagerSettings-Visible="false" 
            Visible="false">

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
                <asp:BoundField DataField="pingjunbihou" HeaderText="平均壁厚" SortExpression="pingjunbihou"/>
                <asp:BoundField DataField="pingjunwaijing" HeaderText="平均外径" SortExpression="pingjunwaijing"/>
                <asp:BoundField DataField="mifengzhi" HeaderText="密封脂" SortExpression="mifengzhi"/>
                <asp:BoundField DataField="jingkou" HeaderText="紧扣" SortExpression="jingkou"/>
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
            </Columns>
        </asp:GridView>

        <asp:GridView ID="gv_gj" 
            runat="server" 
            AutoGenerateColumns="False" 
            CssClass="Table100" 
            AllowPaging="true" 
            OnPageIndexChanging="gv_gj_Page_IndexChanging" 
            OnRowCreated="gv_gj_RowCreated" 
            OnRowDataBound="GridViewRowBound" 
            ShowHeader="false" 
            PagerSettings-Visible="false" 
            Visible="false">

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
                <asp:BoundField DataField="jinghao" HeaderText="井号" sortexpression="jinghao"/>
                <asp:BoundField DataField="wanzuanjingshen" HeaderText="完钻井深" sortexpression="wanzuanjingshen" />
                <asp:BoundField DataField="shigongriqi" HeaderText="施工日期" sortexpression="shigongriqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="fangao_sheji" HeaderText="水泥返高(设计)" sortexpression="fangao_sheji"/>
                <asp:BoundField DataField="fangao_shiji" HeaderText="水泥返高(实际)" sortexpression="fangao_shiji"/>
                <asp:BoundField DataField="midu_sheji" HeaderText="水泥浆密度(设计)" sortexpression="midu_sheji"/>
                <asp:BoundField DataField="midu_shiji" HeaderText="水泥浆密度(实际)" sortexpression="midu_shiji"/>
                <asp:BoundField DataField="jiangyongliang_sheji" HeaderText="水泥浆用量(设计)" sortexpression="jiangyongliang_sheji"/>
                <asp:BoundField DataField="jiangyongliang_shiji" HeaderText="水泥浆用量(实际)" sortexpression="jiangyongliang_shiji"/>
                <asp:BoundField DataField="yongliang_sheji" HeaderText="水泥用量(设计)" sortexpression="yongliang_sheji"/>
                <asp:BoundField DataField="yongliang_shiji" HeaderText="水泥用量(实际)" sortexpression="yongliang_shiji"/>
                <asp:BoundField DataField="dingtiliang_sheji" HeaderText="顶替量(设计)" sortexpression="dingtiliang_sheji"/>
                <asp:BoundField DataField="dingtiliang_shiji" HeaderText="顶替量(实际)" sortexpression="dingtiliang_shiji"/>
                <asp:BoundField DataField="pengya_sheji" HeaderText="碰压(设计)" sortexpression="pengya_sheji"/>
                <asp:BoundField DataField="pengya_shiji" HeaderText="碰压(实际)" sortexpression="pengya_shiji"/>
                <asp:BoundField DataField="shigongwenti" HeaderText="施工中存在问题" sortexpression="shigongwenti"/>
                <asp:BoundField DataField="fuzaqingkuang" HeaderText="复杂情况" sortexpression="fuzaqingkuang"/>
                <asp:BoundField DataField="beizhu" HeaderText="备注" sortexpression="beizhu"/>
                <asp:BoundField DataField="jiancefangshi" HeaderText="检测方式" sortexpression="jiancefangshi"/>
                <asp:BoundField DataField="place" HeaderText="区块" />
                <asp:BoundField DataField="fujian" HeaderText="附件" sortexpression="fujian"/>
            </Columns>
        </asp:GridView>

        <asp:GridView ID="gv_wj" 
            runat="server" 
            AutoGenerateColumns="False" 
            CssClass="Table100" 
            AllowPaging="true" 
            OnPageIndexChanging="gv_wj_Page_IndexChanging" 
            OnRowCreated="gv_wj_RowCreated" 
            OnRowDataBound="GridViewRowBound" 
            ShowHeader="false" 
            PagerSettings-Visible="false" 
            Visible="false">

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
                <asp:BoundField DataField="biaozhiguan_sheji" HeaderText="标志管位置(设计)" SortExpression="biaozhiguan_sheji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="biaozhiguan_shiji" HeaderText="标志管位置(实际)" SortExpression="biaozhiguan_shiji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="rengongjingdi_sheji" HeaderText="人工井底(设计)" SortExpression="rengongjingdi_sheji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="rengongjingdi_shiji" HeaderText="人工井底(实际)" SortExpression="rengongjingdi_shiji" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="gujingzhiliangCBL" HeaderText="固井质量(CBL)" SortExpression="gujingzhiliangCBL" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="gujingzhiliangVDL" HeaderText="固井质量(VDL)" SortExpression="gujingzhiliangVDL" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shuipingpiancha_bzh" HeaderText="产套偏差(标准)" SortExpression="shuipingpiancha_bzh" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shuipingpiancha_hor" HeaderText="产套偏差(水平)" SortExpression="shuipingpiancha_hor" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shuipingpiancha_ver" HeaderText="产套偏差(垂直)" SortExpression="shuipingpiancha_ver" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shiya_start" HeaderText="试压(开始)" SortExpression="shiya_start" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shiya_end" HeaderText="试压(结束)" SortExpression="shiya_end" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="gangbanhanjie_up" HeaderText="环形干板焊接(上缘)" SortExpression="gangbanhanjie_up" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="gangbanhanjie_down" HeaderText="环形钢板焊接(下缘)" SortExpression="gangbanhanjie_down" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jingkougaodu" HeaderText="井口高度" SortExpression="jingkougaodu" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="shikouwanhao" HeaderText="丝扣是否完好" SortExpression="shikouwanhao" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="cunzaiwenti" HeaderText="存在问题" SortExpression="cunzaiwenti" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="fuzaqingkuang" HeaderText="复杂情况" SortExpression="fuzaqingkuang" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="beizhu" HeaderText="备注" SortExpression="beizhu" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="jiancefangshi" HeaderText="检测方式" SortExpression="jiancefangshi" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="place" HeaderText="区块" />
                <asp:BoundField DataField="fujian" HeaderText="附件" SortExpression="fujian" ItemStyle-Wrap="false"/>
            </Columns>
        </asp:GridView>
        <div>
            <uc1:myPager ID="commPage1" runat="server"/>
        </div>
        <asp:Label ID="lblCD" runat="server" Text="" Visible="false"></asp:Label>
    </form>
</body>
</html>
