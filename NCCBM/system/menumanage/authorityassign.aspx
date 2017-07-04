<%@ Page Language="C#" AutoEventWireup="true" Inherits="authorityassign" Theme="blue" Codebehind="authorityassign.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>权限分配</title>  
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />  
       
    <script type="text/javascript" src="../../js/MyTreeView.js"></script>  
</head>


<body style="margin-top:0" >
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sdsRole"  runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>" SelectCommand="SELECT * FROM [T_System_Role] ORDER BY [Role_ID]"></asp:SqlDataSource>
                    
        <!--  设置查询区-->
                <table width="850px">
            <tr>
                <td topmargin="0"  style="height:35px;background-image:url(../../css/res/current.png);background-repeat:no-repeat;">
                   <h3 style="color:#eee; margin-top:7px; margin-left:15px;">权限分配</h3>
                </td>
            </tr>
 
            <tr>
                <td>
                    <div id="divQuery" runat="server">
                        <asp:Label ID="Label2" runat="server" Text="用户组名称" ></asp:Label>
                        <asp:TextBox ID="txtName" runat="server" Width="10%" TabIndex="1"></asp:TextBox>
                        <asp:Button CssClass="button" ForeColor="White" Font-Size="Small"  ID="cmdQuery" runat="server" OnClick="cmdQuery_Click" Text="快速查询" TabIndex="2" /><br />
                    </div>                
                </td>
            </tr>
        </table>
        <table  style="width:100%">
            <tr>
                <td style="width:50%;height:100%"  align="left" valign="top">
                    <asp:GridView ID="grdRole" DataKeyNames="Role_ID" runat="server" DataSourceID="sdsRole" AutoGenerateColumns="False" SkinID="Blue1" OnSelectedIndexChanged="RoleSelectChanged" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Role_ID" HeaderText="组ID" ReadOnly="True" />
                            <asp:BoundField DataField="Role_Name" HeaderText="组名称" />
                            <asp:TemplateField HeaderText="权限">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRole" runat="server" CausesValidation="False" CommandName="Select"
                                        Text="分配"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                                 <emptydatatemplate>
                                       无有效数据！                 
                                </emptydatatemplate> 
                    </asp:GridView>
                </td>
                <td style="width:50%;height:100%">
                <table style="width:50%;height:100%">
                    <tr>
                        <td style="width:50%">                    
                            <div id="divName" runat="server" visible="false">
                            <asp:Label ID="Label1" Text="用户组：" runat="server" ></asp:Label>
                            <asp:Label ID="lblRoleName" runat="server"  Width="12%"></asp:Label>
                            <asp:Button CssClass="button" ForeColor="White" Font-Size="Small"  ID="cmdUpdate" runat="server" OnClick="cmdUpdate_Click" Text="分配" />
                            <asp:Button CssClass="button" ForeColor="White" Font-Size="Small"  ID="cmdRestore" runat="server" OnClick="cmdRestore_Click" Text="返回" />
                            <asp:HiddenField ID="hidRoleID" runat="server" />
                            </div>                    
                        </td>
                    </tr>
                    <tr>
                        <td>                    
                            <div id="divMenu" runat="server" visible="false" style="border-width:5px;border-color:White;overflow:auto;width:330px;height:500px">
                            <asp:TreeView ID="tvMenu" runat="server" ExpandDepth="2" NodeWrap="true"  ShowLines="True" >
                            </asp:TreeView>
                            </div>
                        </td>
                    </tr>
                </table>
                    
                    
                </td>
            </tr>
       </table>
    </form>
</body>
</html>
