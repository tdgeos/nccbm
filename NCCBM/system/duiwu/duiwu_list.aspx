<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="duiwu_list.aspx.cs" Theme="blue" Inherits="NCCBM.system.duiwu.duiwu_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    
</head>
<body style="min-height:490px;">
    <form id="form1" runat="server">
    <div id="divQuery" class="mefont" runat="server">
        <asp:Label ID="lblName" runat="server" Text="队伍名称：" ></asp:Label>
        <asp:TextBox ID="tbName" runat="server" ></asp:TextBox>
        <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="cmdQuery" 
            runat="server" Text="快速查询" onclick="cmdQuery_Click" /><br />
        <asp:TextBox ID="tbPageIndex" runat="server" Visible="false"></asp:TextBox>
    </div> 
    <div>
        <asp:GridView ID="GridView2" runat="server"
            AutoGenerateColumns="False" 
            SkinID="Blue1" 
            OnRowCommand="GridView2_RowCommand" 
            AllowPaging="True" 
            OnPageIndexChanging="Page_IndexChanging" 
            Width="840px">
            <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" />
            <Columns>
                <asp:BoundField DataField="name" HeaderText="队伍名称" />
                <asp:BoundField DataField="jiancheng" HeaderText="简称" />
                <asp:BoundField DataField="zizhi" HeaderText="资质" />
                <asp:BoundField DataField="guimo" HeaderText="队伍规模" />
                <asp:BoundField DataField="dianhua" HeaderText="联系电话" />
                <asp:BoundField DataField="youxiang" HeaderText="电子邮箱" />
                <asp:BoundField DataField="bangongdidian" HeaderText="办公地点" />
                <asp:BoundField DataField="qukuai" HeaderText="区块" />
                <asp:BoundField DataField="zjoryl" HeaderText="施工类型" />
                <asp:BoundField DataField="beizhu" HeaderText="备注" />

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
    </div>
    </form>
</body>
</html>
