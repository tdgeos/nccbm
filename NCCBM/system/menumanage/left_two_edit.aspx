<%@ Page Language="C#" AutoEventWireup="true" Inherits="system_menumanage_left_two_edit" Codebehind="left_two_edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改左侧二级菜单</title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" /></head>
<body style="margin-top:0" >
    <form id="form1" runat="server">
    <div  class="mefont">
        <table style="width:100%" border="0" cellpadding="0" cellspacing="0">
				<tr align="center">
					<td   style="width:100%;background-color:C3CFDF; height: 25px;" >
						<a href="top_one_list.aspx"><b>顶端一级菜单</b></a><b> | </b>
						<a href="left_two_list.aspx"><b>左侧二级菜单</b></a><b> | </b>
						<a href="left_three_list.aspx"><b>左侧三级菜单</b></a><b> | </b>
						<a href="right_four_list.aspx"><b>右侧四级菜单</b></a>
					</td>
				</tr>            
				<tr>
            <td  class="tdhead" style=" height: 30px">
               <h3 style="text-align:center">修 改 左 侧 二 级 菜 单</h3>
            </td>
            </tr>
        </table>
        <table style="width:100%" border="1" cellpadding="3" cellspacing="1" class="metable">
          <tr>
                <td class="tdinput" style="width:30%; height: 20px;">&nbsp;顶端一级菜单：</td>
                <td class="tdinput" style="width: 60%; height: 20px;">
                    &nbsp;<asp:DropDownList ID="top_one_list" runat="server"  Width="242px" DataSourceID="SqlDataSource1" DataTextField="Title" DataValueField="T_ID">
                    </asp:DropDownList></td>
          </tr>                
          <tr>
                <td class="tdinput" style="width:30%; height: 20px;">&nbsp;左侧二级菜单名称：</td>
                <td class="tdinput" style="width: 60%; height: 20px;">
                    &nbsp;<asp:TextBox ID="txtTitle" runat="server" Width="242px" AutoPostBack="true" OnTextChanged="checkname" TabIndex="1" ></asp:TextBox>
                   <asp:RequiredFieldValidator id="validor1" runat="server" controltovalidate="txtTitle" text="必填项目"/></td>
          </tr>
          <tr>
                <td class="tdinput" style="width:30%; height: 20px;">&nbsp;说明：</td>
                <td class="tdinput" style="width: 60%; height: 20px;">
                    &nbsp;<asp:TextBox ID="txtAuthority_Desc" runat="server" Width="242px" TabIndex="2" ></asp:TextBox> </td>
          </tr>
          <tr>
                <td class="tdinput" style="width:30%; height: 20px;">&nbsp;URL：</td>
                <td class="tdinput" style="width: 60%; height: 20px;">
                    &nbsp;<asp:TextBox ID="txtNavigateUrl" runat="server" Width="242px" TabIndex="3" ></asp:TextBox> </td>
          </tr>

				<tr>
					<td class="tdinput" style="width:30%; height: 36px;">&nbsp;排序顺序：</td>
					<td class="tdinput" style="height: 36px">
						&nbsp;<asp:TextBox id="txtOrderLevel" runat="server" Width="242px" TabIndex="4"></asp:TextBox>（请输入一个不小于零的自然数）<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" display="dynamic"
                     controltovalidate="txtOrderLevel" errormessage="输入不正确,请重新输入!" validationexpression="^\+?[1-9][0-9]*$" /> </td>
				</tr>
				<tr class="tdinput">
					<td class="tdinput" style="width:30%; height: 20px;">&nbsp;备注：</td>
					<td class="tdinput">
							&nbsp;<asp:TextBox id="txtRemark" runat="server" TextMode="MultiLine" Width="272px" Height="48px" TabIndex="5"></asp:TextBox></td>
				</tr>
				<tr>
					<td style="height:35px" align="center" class="tdinput"><span class="p20"> </span></td>
					<td class="tdinput">
						<asp:Button CssClass="button" ForeColor="White" Font-Size="Small" id="btnAdd" runat="server" Text="确定 >>" onclick="btnAdd_Click" TabIndex="6"></asp:Button></td>
				</tr>       
				</table>
				   
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:_NCCBM %>"
        SelectCommand="SELECT [T_ID], [Title] FROM [T_System_APPLICATION] WHERE ([Authority_Level] = @Authority_Level)">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="Authority_Level" Type="Double" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
