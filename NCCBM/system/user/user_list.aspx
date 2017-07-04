<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_user_System_user" Theme="blue" Codebehind="user_list.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>用户管理</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    
</head>
<body style="margin-top:0">
    <form id="form1" runat="server">
    <!--  设置查询区-->
    <div id="divQuery" class="mefont" runat="server">
        <asp:Label ID="lblEmployee_Name" runat="server" Text="用户名" Width="50px"></asp:Label>
        <asp:TextBox ID="txtName" runat="server" Width="87px" TabIndex="1"></asp:TextBox>
        <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="cmdQuery" runat="server" OnClick="cmdQuery_Click" Text="快速查询" TabIndex="2" /><br />
    </div> 
    <div>
    <!--  GridView区-->
        <asp:GridView ID="Gravuser" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpID" DataSourceID="sqlDS_DBMA" SkinID="Blue1" AllowPaging="True" Width="840px" OnSelectedIndexChanged="Gravuser_SelectedIndexChanged" OnPageIndexChanging="Gravuser_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="EmpID" HeaderText="用户名" ReadOnly="True" SortExpression="EmpID">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PassWord" HeaderText="密码" SortExpression="PassWord" Visible="False">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Role_Name" HeaderText="用户组名称" SortExpression="Role_Name">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PlaceID" HeaderText="所属区块"  />
                 <asp:BoundField DataField="State" HeaderText="状态"  />
             <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Select"
                            Text="修改"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="Select"
                            Text="删除" OnClientClick=" javascript:return confirm('您确定要停用吗？')"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <emptydatatemplate>
                无有效数据.                 
            </emptydatatemplate> 
            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
        </asp:GridView>
     <!--    使用hidden存取查询关键字的值-->
        <asp:HiddenField ID="hidden_Select" runat="server" />
    </div>
     <!--   数据源-->
        <asp:SqlDataSource ID="sqlDS_DBMA" runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>" SelectCommand="SELECT DISTINCT T_System_USER.PassWord, T_System_USER.RoleID, T_System_Role.Role_Name, T_System_USER.EmpID, (CASE T_System_USER.PlaceID WHEN 0 THEN '全部' WHEN 1 THEN '韩城' WHEN 2 THEN '临汾' WHEN 3 THEN '忻州' END) AS PlaceID, (CASE T_System_USER.State WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_System_USER INNER JOIN T_System_Role ON T_System_USER.RoleID = T_System_Role.Role_ID" >
        </asp:SqlDataSource>
    </form>
</body>
</html>
