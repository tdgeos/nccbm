<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActionControl.ascx.cs" Inherits="NCCBM.Menu.ActionControl" %>
<table border="0" cellspacing="0" cellpadding="0" width="100%" ID="Table1">
					<tr>
						<td height="28" align="left" class="textTitle">&nbsp; <span id="LabelPageTitle" class="textTitle">&nbsp;</span><asp:Button ID="AddBtn" runat="server" Text="增 加" OnClick="AddBtn_Click" />
                            <asp:Button ID="ModifyBtn" runat="server" OnClientClick="return CheckModify(document.all('GridView1_ctl01_AllChk'));" Text="修 改" OnClick="ModifyBtn_Click"  />
                            <asp:Button ID="DeleteBtn" runat="server" OnClientClick="if(!confirm('确认要删除选中数据吗？'))return false;" Text="删 除" OnClick="DeleteBtn_Click"  />
                            <asp:Button ID="QueryBtn" runat="server" Text="查 询" OnClick="QueryBtn_Click" />
                            </td>
						<td height="28" align="right">
                            &nbsp;</td>
					</tr>
				</table>