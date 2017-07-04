<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_role" Theme="blue" Codebehind="role_list.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>用户组</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    

</head>
<body style="margin-top:0">
    <form id="form1" runat="server" >
    <!--  设置查询区-->
    <div id="divQuery" class="mefont" runat="server">
        <asp:Label ID="lblEmployee_Name" runat="server" Text="用户组名称" Width="90px"></asp:Label>
        <asp:TextBox ID="txtName" runat="server" Width="87px" TabIndex="2"></asp:TextBox>
        <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="cmdQuery" runat="server" OnClick="cmdQuery_Click" Text="快速查询" TabIndex="1"/><br />
    </div> 
    <!--  GridView区-->
    <div>
        <asp:GridView ID="Gravrole" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" DataKeyNames="Role_ID" DataSourceID="sqlDS_DBMA" SkinID="Blue1" Width="840px" OnSelectedIndexChanged="Gravrole_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Role_ID" HeaderText="用户组ID" ReadOnly="True" Visible="False">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Role_Name" HeaderText="用户组名称" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="State" HeaderText="状态" />
             <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Select"
                            Text="修改"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="Select"
                            Text="删除" OnClientClick=" javascript:return confirm('确定要删除吗？删除后数据将不可用！')"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
       <emptydatatemplate>
                无有效数据.                 
            </emptydatatemplate> 
            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
        </asp:GridView>
        </div>
     <!--    使用hidden存取查询关键字的值-->
        <asp:HiddenField ID="hidden_Select" runat="server" />
 
     <!--   数据源-->
        <asp:SqlDataSource ID="sqlDS_DBMA" runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>"
            DeleteCommand="DELETE FROM [T_System_Role] WHERE [Role_ID] = @Role_ID" InsertCommand="INSERT INTO [T_System_Role] ([Role_ID], [Role_Name]) VALUES (@Role_ID, @Role_Name)"
            SelectCommand="SELECT Role_ID, Role_Name, (CASE T_System_Role.state WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_System_Role ORDER BY Role_ID" UpdateCommand="UPDATE [T_System_Role] SET [Role_Name] = @Role_Name WHERE [Role_ID] = @Role_ID">
            <DeleteParameters>
                <asp:Parameter Name="Role_ID" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Role_Name" Type="String" />
                <asp:Parameter Name="Role_ID" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Role_ID" Type="String" />
                <asp:Parameter Name="Role_Name" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
