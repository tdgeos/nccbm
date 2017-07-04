<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_fun_System_EMPLOYEE"  Theme="blue" Codebehind="employee_list.aspx.cs" %>
<%@ Register Src="../../Menu/ActionControl.ascx" TagName="ActionControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="X1.Pager" Assembly="X1.Pager" %>
<%@ Register TagPrefix="cc1" Namespace="NCCBM" Assembly="NCCBM" %>
<%@ Register Src="../../MyPagerBar.ascx" TagName="myPager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>人员管理</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
    <link  href="../../css/jquery-ui-1.8.20.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui.zh-CN.js" type="text/javascript"></script>
    <script src="../../js/superTables.js" type="text/javascript"></script>
    <script src="../../js/jquery.superTable.js" type="text/javascript"></script>
    <script src="../../js/datetime.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $("#GridView1").toSuperTable({ width: "845px", height: "294px", fixedCols: 2, headerRows: 2 }).find("tr:even").addClass("altRow");
        });
    </script>

</head>
<body style="margin-top:0; min-height:350px;">
    <form id="form1" runat="server">
    <!--  设置查询区-->
    <div id="divQuery" class="mefont" runat="server">
        <asp:Label ID="lblEmployee_Name" runat="server" Text="姓名" Width="35px"></asp:Label>
        <asp:TextBox ID="txtName" runat="server" Width="87px"></asp:TextBox>
        <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="cmdQuery" runat="server" OnClick="cmdQuery_Click" Text="快速查询" /><br />
    </div> 
    <div>
        <asp:Label ID="zjInfo" runat="server" Text=""></asp:Label>
    </div>
       <!--  GridView区-->
        <asp:GridView ID="GridView1" DataKeyNames="ID" runat="server" 
            DataSourceID="sqlDS_DBMA" SkinID="Blue1" 
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
            AutoGenerateColumns="False" 
            CssClass="Table100" AllowPaging="true" 
            OnPageIndexChanging="GridView1_PageIndexChanging" 
            OnRowCreated="GridView1_RowCreated" 
            ShowHeader="false" PagerSettings-Visible="false">

            <Columns>
                <asp:BoundField DataField="Employee_ID" HeaderText="用户名" ReadOnly="True" ItemStyle-Width="80px" />
                <asp:BoundField DataField="Employee_Name" HeaderText="真实姓名" ItemStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Gender" HeaderText="性别" ItemStyle-Width="40px"/>
                <asp:BoundField DataField="Birthday" HeaderText="出生日期" ItemStyle-Width="80px"  DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False"/>
                <asp:BoundField DataField="Branch_Name" HeaderText="部门" ItemStyle-Width="150px">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Func_Name" HeaderText="职务" ItemStyle-Width="50px">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="XMMCS" HeaderText="管辖项目" Visible="false" />
                <asp:BoundField DataField="HSE1" HeaderText="HSE证书(起始日期)" ItemStyle-Width="120px"/>
                <asp:BoundField DataField="HSE2" HeaderText="HSE证书(结束日期)" ItemStyle-Width="120px"/>
                <asp:BoundField DataField="LHQ1" HeaderText="硫化氢证书(起始日期)" ItemStyle-Width="140px"/>
                <asp:BoundField DataField="LHQ2" HeaderText="硫化氢证书(结束日期)" ItemStyle-Width="140px"/>
                <asp:BoundField DataField="JK1" HeaderText="井控证书(起始日期)" ItemStyle-Width="120px"/>
                <asp:BoundField DataField="JK2" HeaderText="井控证书(结束日期)" ItemStyle-Width="120px"/>
                <asp:BoundField DataField="JD1" HeaderText="监督证书(起始日期)" ItemStyle-Width="120px"/>
                <asp:BoundField DataField="JD2" HeaderText="监督证书(结束日期)" ItemStyle-Width="120px"/>
                <asp:BoundField DataField="Tel" HeaderText="电话" ItemStyle-Width="80px"/>
                <asp:BoundField DataField="MobilTel" HeaderText="手机" ItemStyle-Width="80px"/>
                <asp:BoundField DataField="JTel" HeaderText="住宅电话" ItemStyle-Width="80px"/>
                <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="state" HeaderText="状态" ItemStyle-Width="40px"/>
                <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False" ItemStyle-Width="50px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Select"
                            Text="修改"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ShowHeader="False" Visible="False" ItemStyle-Width="50px">
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
        <div>
            <uc1:myPager ID="commPage1" runat="server"/>
        </div>
        <!--    使用hidden存取查询关键字的值-->
        <asp:HiddenField ID="hidden_Select" runat="server" />
         <!--   数据源-->
        <asp:SqlDataSource ID="sqlDS_DBMA" runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>"  SelectCommand="SELECT T_System_EMPLOYEE.Employee_ID, T_System_EMPLOYEE.Employee_Name, (CASE T_System_EMPLOYEE.Gender WHEN 0 THEN '男' WHEN 1 THEN '女' END) AS Gender, T_System_EMPLOYEE.Birthday, T_System_EMPLOYEE.Branch_ID, T_System_EMPLOYEE.Func_ID, T_System_EMPLOYEE.XMMCS, T_System_EMPLOYEE.HSE1, T_System_EMPLOYEE.HSE2, T_System_EMPLOYEE.LHQ1, T_System_EMPLOYEE.LHQ2,T_System_EMPLOYEE.JK1, T_System_EMPLOYEE.JK2, T_System_EMPLOYEE.JD1, T_System_EMPLOYEE.JD2,T_System_EMPLOYEE.Tel, T_System_EMPLOYEE.MobilTel, T_System_EMPLOYEE.JTel, T_System_EMPLOYEE.Email, T_System_BRANCH.Branch_Name, T_SYSTEM_FUNC.Func_Name, (CASE T_System_EMPLOYEE.state WHEN 0 THEN '在职' WHEN 1 THEN '离职' END) AS state, T_System_EMPLOYEE.ID FROM T_System_EMPLOYEE INNER JOIN T_System_BRANCH ON T_System_EMPLOYEE.Branch_ID = T_System_BRANCH.Branch_ID INNER JOIN T_SYSTEM_FUNC ON T_System_EMPLOYEE.Func_ID = T_SYSTEM_FUNC.Func_ID Where T_System_EMPLOYEE.State='0' order by T_System_EMPLOYEE.Branch_ID,T_System_EMPLOYEE.Func_ID">
            <UpdateParameters>
                <asp:Parameter Name="Employee_Name" Type="String" />
                <asp:Parameter Name="Gender" Type="Boolean" />
                <asp:Parameter Name="Birthday" Type="DateTime" />
                <asp:Parameter Name="Branch_ID" Type="Int32" />
                <asp:Parameter Name="Func_ID" Type="Int32" />
                <asp:Parameter Name="HSE1" Type="DateTime" />
                <asp:Parameter Name="HSE2" Type="DateTime" />
                <asp:Parameter Name="LHQ1" Type="DateTime" />
                <asp:Parameter Name="LHQ2" Type="DateTime" />
                <asp:Parameter Name="JK1" Type="DateTime" />
                <asp:Parameter Name="JK2" Type="DateTime" />
                <asp:Parameter Name="JD1" Type="DateTime" />
                <asp:Parameter Name="JD2" Type="DateTime" />
                <asp:Parameter Name="Tel" Type="String" />
                <asp:Parameter Name="MobilTel" Type="String" />
                <asp:Parameter Name="JTel" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="Employee_ID" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
