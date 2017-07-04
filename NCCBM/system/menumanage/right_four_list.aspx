<%@ Page Language="C#" AutoEventWireup="true" Theme="blue" Inherits="system_menumanage_right_four_list" Codebehind="right_four_list.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>右侧四级菜单</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin-top:0" >
    <form id="form1" runat="server">
    <div  class="mefont">
        <table style="height:400px" cellspacing="0" cellpadding="3" width="100%" border="0">
					<tr align="center" >
						<td  colspan="6"  style="width:100%;background-color:C3CFDF; height: 25px;" >
						    <a href="top_one_list.aspx"><b>顶端一级菜单</b></a><b> | </b>
							<a href="left_two_list.aspx"><b>左侧二级菜单</b></a><b> | </b>
							<a href="left_three_list.aspx"><b>左侧三级菜单</b></a><b> | </b>
							<a href="right_four_list.aspx"><b>右侧四级菜单</b></a>
						</td>
					</tr>
					<tr>
					    <td style=" width:15%">
					        <b>顶端一级菜单：</b> 
					    </td>
                        <td style=" width:20%">
                                <asp:DropDownList ID="top_one_list" runat="server"  Width="142px"  AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="Title" DataValueField="T_ID" OnSelectedIndexChanged="top_one_list_SelectedIndexChanged"></asp:DropDownList>
                        </td>
					    <td style=" width:15%">
					        <b>左侧二级菜单：</b> 
					    </td>
                        <td style=" width:20%">
                                <asp:DropDownList ID="left_two_list" runat="server"  Width="142px"  AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Title" DataValueField="T_ID" OnSelectedIndexChanged="left_two_list_SelectedIndexChanged" ></asp:DropDownList>
                        </td>
					    <td style=" width:15%">
					        <b>左侧三级菜单：</b> 
					    </td>
                        <td style=" width:20%">
                                <asp:DropDownList ID="left_three_list" runat="server"  Width="142px"  AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="Title" DataValueField="T_ID" OnSelectedIndexChanged="left_three_list_SelectedIndexChanged" ></asp:DropDownList>
                        </td>
					</tr>
					<tr align="center" valign="top" style="height:80%; width:100%"  >
					    <td colspan="6" style="height: 80%">
                            <asp:GridView ID="GVRightFourList" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" DataKeyNames="T_ID" SkinID="Blue1"
                                Width="100%" OnSelectedIndexChanged="GVRightFourList_SelectedIndexChanged" OnRowDataBound="GVRightFourList_RowDataBound" DataSourceID="GridviewSqlDataSource">
                                <Columns>
                                    <asp:BoundField DataField="T_ID" HeaderText="序号" ReadOnly="True" SortExpression="T_ID" Visible="false" />
                                    <asp:BoundField DataField="Title" HeaderText="右侧四级菜单" SortExpression="Title" />
                                    <asp:BoundField DataField="Authority_Desc" HeaderText="说明"  />
                                    <asp:BoundField DataField="OrderLevel" HeaderText="排序顺序" SortExpression="OrderLevel" />
                                    <asp:BoundField DataField="Remark" HeaderText="备注"  />
                                    <asp:TemplateField HeaderText="修改" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="modify" runat="server" CausesValidation="False" CommandName="Select" Text="修改"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="delete" runat="server" CausesValidation="False" CommandName="Delete"
                                                Text="删除"  OnClientClick=" javascript:return confirm('确定要删除吗？删除后数据将不可恢复！')"  ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                 <emptydatatemplate>
                                       无有效数据！                 
                                </emptydatatemplate> 

                            </asp:GridView>
					    
					    </td>
					</tr>
					
					<tr>
					    <td colspan="6" align="center"></td>
					</tr>
					<tr>
					    <td colspan="6" align="center" style="height: 44px">
				            <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" id="btnMainMenuAdd" runat="server" Width="65px" Text="新增" onclick="btnMainMenuAdd_Click"></asp:button><br/>
					    </td>
					
					</tr>					
				</table>    
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>"
            SelectCommand="SELECT [T_ID], [Title] FROM [T_System_APPLICATION] WHERE ([Authority_Level] = @Authority_Level)">
            <SelectParameters>
                <asp:Parameter DefaultValue="1" Name="Authority_Level" Type="Double" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>"
            SelectCommand="SELECT [T_ID], [Title] FROM [T_System_APPLICATION] WHERE (([Authority_Level] = @Authority_Level) AND ([Parent_ID] = @Parent_ID))">
            <SelectParameters>
                <asp:Parameter DefaultValue="2" Name="Authority_Level" Type="Double" />
                <asp:ControlParameter ControlID="top_one_list" Name="Parent_ID" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>"
            SelectCommand="SELECT [T_ID], [Title] FROM [T_System_APPLICATION] WHERE (([Authority_Level] = @Authority_Level) AND ([Parent_ID] = @Parent_ID))">
            <SelectParameters>
                <asp:Parameter DefaultValue="3" Name="Authority_Level" Type="Double" />
                <asp:ControlParameter ControlID="left_two_list" Name="Parent_ID" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="GridviewSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>" 
        SelectCommand="SELECT * FROM [T_System_APPLICATION] WHERE (([Authority_Level] = @Authority_Level) AND ([Parent_ID] = @Parent_ID)) order by OrderLevel" 
        DeleteCommand="DELETE FROM [T_System_APPLICATION] WHERE [T_ID] = @T_ID" 
        InsertCommand="INSERT INTO [T_System_APPLICATION] ([Title], [Parent_ID], [Authority_Level], [OrderLevel], [NavigateUrl], [Target], [Authority_Desc], [Authority_Status], [Remark]) VALUES (@Title, @Parent_ID, @Authority_Level, @OrderLevel, @NavigateUrl, @Target, @Authority_Desc, @Authority_Status, @Remark)" UpdateCommand="UPDATE [T_System_APPLICATION] SET [Title] = @Title, [Parent_ID] = @Parent_ID, [Authority_Level] = @Authority_Level, [OrderLevel] = @OrderLevel, [NavigateUrl] = @NavigateUrl, [Target] = @Target, [Authority_Desc] = @Authority_Desc, [Authority_Status] = @Authority_Status, [Remark] = @Remark WHERE [T_ID] = @T_ID">
            <SelectParameters>
                <asp:Parameter DefaultValue="4" Name="Authority_Level" Type="Double" />
                <asp:ControlParameter ControlID="left_three_list" Name="Parent_ID" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="T_ID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Title" Type="String" />
                <asp:Parameter Name="Parent_ID" Type="Int32" />
                <asp:Parameter Name="Authority_Level" Type="Double" />
                <asp:Parameter Name="OrderLevel" Type="Double" />
                <asp:Parameter Name="NavigateUrl" Type="String" />
                <asp:Parameter Name="Target" Type="String" />
                <asp:Parameter Name="Authority_Desc" Type="String" />
                <asp:Parameter Name="Authority_Status" Type="Double" />
                <asp:Parameter Name="Remark" Type="String" />
                <asp:Parameter Name="T_ID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Title" Type="String" />
                <asp:Parameter Name="Parent_ID" Type="Int32" />
                <asp:Parameter Name="Authority_Level" Type="Double" />
                <asp:Parameter Name="OrderLevel" Type="Double" />
                <asp:Parameter Name="NavigateUrl" Type="String" />
                <asp:Parameter Name="Target" Type="String" />
                <asp:Parameter Name="Authority_Desc" Type="String" />
                <asp:Parameter Name="Authority_Status" Type="Double" />
                <asp:Parameter Name="Remark" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
