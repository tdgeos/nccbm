<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_menumanage_left_three_add" Codebehind="left_three_add.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>左侧三级菜单</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin-top:0" >
    <form id="form1" runat="server">
    <div class="mefont">
        <table style="width:100%" border="0" cellpadding="0" cellspacing="0">
			<tr align="center">
				<td   style="width:100%;height: 25px;" >
					<a href="top_one_list.aspx"><b>顶端一级菜单</b></a><b> | </b>
					<a href="left_two_list.aspx"><b>左侧二级菜单</b></a><b> | </b>
					<a href="left_three_list.aspx"><b>左侧三级菜单</b></a><b> | </b>
					<a href="right_four_list.aspx"><b>右侧四级菜单</b></a>
				</td>
			</tr>
            <td  class="tdhead" style=" height: 30px">
               <h3 style="text-align:center">添 加 左 侧 三 级 菜 单</h3>
            </td>
        </table>
        <table style="width:100%" border="1"  cellpadding="3" cellspacing="1" class="metable">
              <tr>
                <td colspan="2" class="tdinput" style="height: 20px">&nbsp;<strong>基本项</strong></td>
              </tr>
          <tr>
                <td class="tdinput" style="width:30%; height: 20px;">&nbsp;顶端一级菜单：</td>
                <td class="tdinput" style="width: 60%; height: 20px;">&nbsp;<asp:DropDownList ID="top_one_list" Width="245px"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="top_one_list_SelectedIndexChanged" DataSourceID="SqlDataSource1" DataTextField="Title" DataValueField="T_ID" TabIndex="1">
                    </asp:DropDownList></td>
          </tr>         
           <tr>
                <td class="tdinput" style="width:30%; height: 20px;">&nbsp;左侧二级菜单：</td>
                <td class="tdinput" style="width: 60%; height: 20px;">&nbsp;<asp:DropDownList ID="left_two_list" runat="server" Width="245px" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Title" DataValueField="T_ID" TabIndex="2">
                    </asp:DropDownList></td>
          </tr>          
 
            <tr>
                <td class="tdinput" style="width: 30%; height: 20px;">&nbsp;左侧三级菜单名称：</td>
                <td class="tdinput" style="width: 60%; height: 20px;"> &nbsp;<asp:TextBox ID="txtTitle" runat="server" AutoPostBack="true" OnTextChanged="checkname" Width="242px" TabIndex="3"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                            ID="validor1" runat="server" ControlToValidate="txtTitle" Text="必填项目"></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr>
                <td class="tdinput" style="width:30%; height: 20px;"> &nbsp;目标：</td>
                <td class="tdinput" style="width: 60%; height: 20px;">
                    &nbsp;<asp:DropDownList ID="selected_position" runat="server" Width="245px" AutoPostBack="True" AppendDataBoundItems="true" TabIndex="4">
                    <asp:ListItem Value="rightmenu" Text="右上侧"></asp:ListItem>    
                    <asp:ListItem Value="rightmain" Text="右下侧"></asp:ListItem>  
                      </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" ControlToValidate="selected_position" Text="必填项目"></asp:RequiredFieldValidator>
                  </td>
            </tr>         
            <tr>
                <td class="tdinput" style="width:30%; height: 20px;">&nbsp;说明：</td>
                <td class="tdinput" style="width: 60%; height: 20px;"> &nbsp;<asp:TextBox ID="txtAuthority_Desc" runat="server" Width="242px" TabIndex="5" ></asp:TextBox> </td>
          </tr>
          <tr>
                <td class="tdinput"style="width:30%; height: 20px;">&nbsp;URL：</td>
                <td class="tdinput" style="width: 60%; height: 20px;"> &nbsp;<asp:TextBox ID="txtNavigateUrl" runat="server" Width="242px" TabIndex="6" ></asp:TextBox> </td>
          </tr>

				<tr >
					<td class="tdinput" style="width:30%; height: 20px;">&nbsp;排序顺序：</td>
					<td class="tdinput" style="height: 20px">&nbsp;<asp:TextBox id="txtOrderLevel" runat="server" Width="242px" TabIndex="7"></asp:TextBox>（请输入一个不小于零的自然数）
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" display="dynamic"
                         controltovalidate="txtOrderLevel" errormessage="输入不正确,请重新输入!" validationexpression="^\+?[1-9][0-9]*$" />
                         </td>
				</tr>
				<tr >
					<td class="tdinput" style="width:30%; height: 20px;">&nbsp;备注：</td>
					<td class="tdinput" style="height: 20px">&nbsp;<asp:TextBox id="txtRemark" runat="server" TextMode="MultiLine" Width="272px" Height="48px" TabIndex="8"></asp:TextBox></td>
				</tr>
				<tr>
					<td style="height:30px; width: 30%;" align="center" class="tdinput"></td>
					<td class="tdinput"><asp:Button CssClass="button" ForeColor="White" Font-Size="Small" id="btnAdd" runat="server" Text="确定 >>" onclick="btnAdd_Click" TabIndex="9"></asp:Button></td>
				</tr>       
				</table>
				   
    </div>
    </form>
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
</body>
</html>
