<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jsyj_list.aspx.cs" Inherits="NCCBM.data.Report.jsyj.jsyj_list" %>

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
    <script runat="server" language="c#">
        public void GridViewRowBound(object sender,GridViewRowEventArgs e)
        {
            NCCBM.EXGridView.GridViewRowBound(sender, e);
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <%--<table width="850px" border="0" class="metable" cellpadding="0" cellspacing="0" >
          <tr >                
            <td  class="tdhead" style=" height: 30px">
              <h3 style="text-align:center">技术研究及其他工作数据录入</h3></td>
          </tr> 
    </table>--%>
    <div>
        <asp:GridView ID="GridView2" runat="server" Width="840px"
            AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center"
            CssClass="Table100" AllowPaging="true" 
            OnPageIndexChanging="Page_IndexChanging" 
            OnRowCommand="GridView2_RowCommand" 
            OnRowDataBound="GridViewRowBound" ShowHeader="true">

            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>

            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle CssClass="tableLine" />
            <EditRowStyle BackColor="#9EB6CE" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle CssClass="tableLineHeader" />
            <AlternatingRowStyle CssClass="tableLineAlternating" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="序号" ItemStyle-Width="50px" ItemStyle-Font-Size="Small"/>
                <asp:BoundField DataField="lururiqi" HeaderText="录入日期" ItemStyle-Width="80px" ItemStyle-Font-Size="Small" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="hancheng" HeaderText="韩城" ItemStyle-Width="200px" ItemStyle-Font-Size="Small"/>
                <asp:BoundField DataField="linfen" HeaderText="临汾" ItemStyle-Width="200px" ItemStyle-Font-Size="Small"/>
                <asp:BoundField DataField="xinzhou" HeaderText="忻州" ItemStyle-Width="200px" ItemStyle-Font-Size="Small"/>
                
                <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False" ItemStyle-Width="50px" ItemStyle-Font-Size="Small">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" Text="修改" CommandName="xiugai"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False" ItemStyle-Width="50px" ItemStyle-Font-Size="Small">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDel" runat="server" CausesValidation="False" Text="删除" CommandName="shanchu" OnClientClick="JavaScript:return confirm('确定要删除吗？')"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField> 
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
