<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyPagerBar.ascx.cs" Inherits="WEE.MyPagerBar" %>
<style type="text/css">
.inputs
{
    width: 50px;
	background-color: #f1f1f1;
    border: 1px solid #aaa;
    border-radius: 5px;
    padding: 0px 3px 3px 0px;
    -moz-border-radius: 5px;
    -webkit-border-radius: 5px;
    -moz-box-shadow: 0 1px 1px #ccc inset, 0 1px 0 #fff;
    -webkit-box-shadow: 0 1px 1px #ccc inset, 0 1px 0 #fff;
    box-shadow: 0 1px 1px #ccc inset, 0 1px 0 #fff;
    }
</style>
<table>
	<tr>
		<td>页次：<font color="blue"><asp:label id="PageNumber" runat="server"></asp:label></font>页&nbsp;&nbsp;
			每页：<font color="blue"><asp:label id="PageBulk" runat="server"></asp:label></font>行&nbsp;&nbsp;
			总记录数：<font color="blue"><asp:Label id="RecordNumber" runat="server"></asp:Label></font>&nbsp;&nbsp;&nbsp;&nbsp;
		</td>
		<td>
			<asp:ImageButton id="First" runat="server" ImageUrl="css/res/home.jpg" ImageAlign="AbsMiddle" CausesValidation="False"></asp:ImageButton>
			<asp:ImageButton id="Previous" runat="server" ImageUrl="css/res/last.jpg" ImageAlign="AbsMiddle"
				CausesValidation="False"></asp:ImageButton>
			<asp:ImageButton id="Next" runat="server" ImageUrl="css/res/next.jpg" ImageAlign="AbsMiddle" CausesValidation="False"></asp:ImageButton>
			<asp:ImageButton id="Lastly" runat="server" ImageUrl="css/res/end.jpg" ImageAlign="AbsMiddle" CausesValidation="False"></asp:ImageButton>
			转到 <asp:TextBox id="SelectPage" Text="1" CssClass="inputs" maxlength="9" runat="server"></asp:TextBox>
			<asp:ImageButton id="ImageButton1" runat="server" ImageUrl="css/res/confirm.jpg" ImageAlign="AbsMiddle"
				CausesValidation="False" OnClick="Go_Click"></asp:ImageButton>
		</td>
	</tr>
</table>
