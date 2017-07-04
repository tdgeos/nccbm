<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_branch_rightmain" Theme="blue" Codebehind="branch_list.aspx.cs" %>

<!--    使用主题blue，共用样式-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>部门</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    
</head>
<body style="margin-top:0">
    <form id="frmMain" runat="server">
        <!--   数据源-->
        <asp:SqlDataSource ID="sqlDS_DBMA" runat="server"  ConnectionString="<%$ ConnectionStrings:_NCCBM %>"
            SelectCommand="SELECT Branch_ID, Branch_Name, (SELECT Branch_Name FROM T_System_BRANCH WHERE (Branch_ID = t.Father_ID)) AS Father_Name, (CASE t.state WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_System_BRANCH t ORDER BY Branch_ID" >
            <DeleteParameters>
                <asp:Parameter Name="original_Branch_ID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Branch_Name" Type="String" />
                <asp:Parameter Name="Father_ID" Type="Int32" />
                <asp:Parameter Name="original_Branch_ID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Branch_Name" Type="String" />
                <asp:Parameter Name="Father_ID" Type="Int32" />
            </InsertParameters>
        </asp:SqlDataSource>
        <!--  设置查询区-->
        <div id="divQuery" class="mefont" runat="server">
        <asp:Label ID="lblFatherName" runat="server" Text="部门名称" Width="70px"></asp:Label>
        <asp:TextBox ID="txtName" runat="server" Width="87px"></asp:TextBox>
        <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="cmdQuery" 
                runat="server" OnClick="cmdQuery_Click" Text="快速查询" /><br />
        </div>
        <!--  GridView区-->
        <asp:GridView ID="grdvBranch" runat="server" Width="840px" AllowPaging="True"
            AutoGenerateColumns="False" DataKeyNames="Branch_ID" DataSourceID="sqlDS_DBMA" SkinID="Blue1" OnSelectedIndexChanged="grdv_Branch_SelectedIndexChanged" OnRowDeleting="grdv_Branch_Deleting" PageSize="3">
            <Columns>
                <asp:BoundField DataField="Branch_ID" HeaderText="部门ID" InsertVisible="False"
                    ReadOnly="True" Visible="False"  />
                <asp:TemplateField HeaderText="部门名称">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Branch_Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="TxtaddBranchName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtaddBranchName" Text="必填项目"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBatherName" runat="server" Text='<%# Bind("Branch_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="上级部门名称" SortExpression="Father_Name">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditFatherName" runat="server" Text='<%# Eval("Father_Name") %>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="DpdFatherName" runat="server">
                        </asp:DropDownList>
                        <asp:LinkButton ID="lbtnMakesure" runat="server" OnClick="lbtnMakesure_Click">确定</asp:LinkButton>
                        <asp:LinkButton ID="lbtnCancel" runat="server">取消</asp:LinkButton>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("Father_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="State" HeaderText="状态" />
                <asp:TemplateField HeaderText="操作" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Select"
                            Text="修改"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="Select"
                            Text="删除" OnClientClick=" javascript:return confirm('您确定要停用吗？')" OnClick="lbtnDelete_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <emptydatatemplate>
                无有效数据                 
            </emptydatatemplate> 
            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
        </asp:GridView>
        <!--    使用hidden存取查询关键字的值-->
        <asp:HiddenField ID="hidden_Select" runat="server" />
    </form>
</body>
</html>
