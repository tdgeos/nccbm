<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_fun_fun" Theme="blue" Codebehind="func_list.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    
</head>
<body style="margin-top:0">
    <form id="form1" runat="server" >
    <!--  设置查询区-->
    <div id="divQuery" class="mefont" runat="server">
        <asp:Label ID="lblEmployee_Name" runat="server" Text="职务名称" Width="70px"></asp:Label>
        <asp:TextBox ID="txtName" runat="server" Width="87px"></asp:TextBox>
        <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="cmdQuery" runat="server" OnClick="cmdQuery_Click" Text="快速查询" /><br />
    </div> 
    <!--  GridView区-->
    <div>
        <asp:GridView ID="Gravfun" runat="server" AllowPaging="True"
            AutoGenerateColumns="False" DataKeyNames="Func_ID" DataSourceID="sqlDS_DBMA" SkinID="Blue1" Width="840px" OnSelectedIndexChanged="Gravfun_SelectedIndexChanged" >
            <Columns>
                <asp:BoundField DataField="Func_Name" HeaderText="职务名称" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Func_ID" HeaderText="职务ID" ReadOnly="True" Visible="False" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="State" HeaderText="状态" SortExpression="State" />
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
        </div>
     <!--    使用hidden存取查询关键字的值-->
        <asp:HiddenField ID="hidden_Select" runat="server" />
 
     <!--   数据源-->
        <asp:SqlDataSource ID="sqlDS_DBMA" runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>"
            DeleteCommand="DELETE FROM [T_SYSTEM_FUNC] WHERE [Func_ID] = @Func_ID" InsertCommand="INSERT INTO [T_SYSTEM_FUNC] ([Func_Name]) VALUES (@Func_Name)"
            SelectCommand="SELECT Func_ID, Func_Name, (CASE T_SYSTEM_FUNC.state WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_SYSTEM_FUNC ORDER BY Func_ID" UpdateCommand="UPDATE [T_SYSTEM_FUNC] SET [Func_Name] = @Func_Name WHERE [Func_ID] = @Func_ID">
            <DeleteParameters>
                <asp:Parameter Name="Func_ID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Func_Name" Type="String" />
                <asp:Parameter Name="Func_ID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Func_Name" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    </form>
</body></html>
