<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YL_Import.aspx.cs" Inherits="NCCBM.Import.YL_Import" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/superTables.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.20.custom.css" rel="stylesheet" type="text/css" />

    <link href="../js/jquery.uploadify.2.1/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../js/jquery.uploadify.2.1/default.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.uploadify.2.1/jquery-1.3.2.min.js" type="text/javascript"></script>
    
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>

    <script src="../js/jquery.uploadify.2.1/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../js/jquery.uploadify.2.1/swfobject.js" type="text/javascript"></script>
    <script src="../js/jquery.uploadify.2.1/uploadify.js" type="text/javascript"></script>
    
    <script src="../js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.zh-CN.js" type="text/javascript"></script>
    <script src="../js/datetime.js" type="text/javascript"></script>
    
    <link href="../../js/jquery.uploadify.2.1/default.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function msgbox(msg) {
            if (window.showModalDialog("MakeSure.aspx?messageid=" + msg, "", "status=no;dialogWidth=800px;dialogHeight=500px;menu=no;resizeable=yes;scroll=yes;center=yes;edge=raise") == "OK") {
                document.location.href = "ZJ_Import.aspx";
            }
        } 
    </script>
    
    <script runat="server" language="c#">
        public void GridViewRowBound(object sender,GridViewRowEventArgs e)
        {
            NCCBM.EXGridView.GridViewRowBound(sender, e);
        } 
    </script>


</head>

<body>

    <form id="form" runat="server">             
        <table width="850px">
            <tr>
                <td topmargin="0"  style="height:35px;background-image:url(../../css/res/current.png);background-repeat:no-repeat;">
                   <h3 style="color:#eee; margin-top:7px; margin-left:4px;">压 裂 数 据 导 入</h3>
                </td>
            </tr>
        </table>

        <%--数据导入--%>
        <div style="width:400px; height:300px;float:left; border-width:thin; border-color:#d0d0f0; border-style:solid;">
        <table border="0" cellpadding="3" cellspacing="0">
            <tr>
                <td>
                    区块:
                    <asp:DropDownList ID="lstQukuai" runat="server"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    日期:
                    <asp:TextBox ID="tbRiqi" runat="server" CssClass="datepicker"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:350px;">
                    压裂质量日报:
                    <asp:TextBox ID="tbYlsgzl" runat="server" width="195px" ></asp:TextBox>
                    <asp:FileUpload ID="fuYlsgzl" runat="server" style="filter: alpha(opacity=0);
                        width:65px; height:auto; cursor: hand; opacity: 0; position: absolute; z-index: 2;" 
                        onchange="javascript:__doPostBack('lbYlsgzl','')" />
                    <input id="Button1" type="button" value="浏 览"  class="button-query" style="Color:#fff;" />
                    <asp:LinkButton ID="lbYlsgzl" runat="server"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    压裂检查:
                    <asp:DropDownList ID="ddlYljc" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:350px;">
                    压裂工程日报:
                    <asp:TextBox ID="tbDataFile" runat="server" width="195px" ></asp:TextBox>
                    <asp:FileUpload ID="fuData" runat="server" style="filter: alpha(opacity=0);
                        width:65px; height:auto; cursor: hand; opacity: 0; position: absolute; z-index: 2;" 
                        onchange="javascript:__doPostBack('lbData','')" />
                    <input id="Button4" type="button" value="浏 览"  class="button-query" style="Color:#fff;" />
                    <asp:LinkButton ID="lbData" runat="server"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    射孔:
                    <asp:DropDownList ID="ddlSk" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    压裂施工:
                    <asp:DropDownList ID="ddlYlsg" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    失败原因说明:
                    <asp:DropDownList ID="ddlYc" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="btnCheck" runat="server" 
                        Text="验 证" OnClick="check_click" Height="20px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="btnImport" runat="server" 
                        Text="导 入" OnClick="import_click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblData" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        </div>

        <div style="width:430px; height:300px; margin-left:10px; float:left; border-width:thin; border-color:#d0d0f0; border-style:solid;">
        <table border="0" cellpadding="3" cellspacing="0" style="margin-left:3px;">
            <tr>
                <td>
                    附件:
                    <asp:TextBox ID="tbFujian" runat="server" width="195px" ></asp:TextBox>
                    <asp:FileUpload ID="fuFujian" runat="server" style="filter: alpha(opacity=0);
                        width:65px; height: auto; cursor: hand; opacity: 0; position: absolute; z-index: 2;" 
                        onchange="javascript:__doPostBack('lbFujian','')" />
                    <input id="Button2" type="button" value="浏 览"  class="button-query"　
                        style="Color:#fff;" />
                    <asp:LinkButton ID="lbFujian" runat="server"></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAddFujian" runat="server" CssClass="button-query" ForeColor="White" Font-Size="Small" Text="添加" OnClick="addfj_click" />
                </td>
            </tr>
            <tr>
                <td>
                    对应井号:
                    <asp:TextBox ID="tbJinghao" runat="server" Width="120px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    对应层号:
                    <asp:TextBox ID="tbCenghao" runat="server" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    对应表名:
                    <asp:DropDownList ID="ddlFJTable" runat="server">
                        <asp:Listitem>压裂检查</asp:Listitem>
                        <asp:Listitem>射孔</asp:Listitem>
                        <asp:Listitem>压裂施工</asp:Listitem>
                        <asp:Listitem>加砂量不足说明</asp:Listitem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    对应区块:
                    <asp:DropDownList ID="ddlFJQukuai" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="border-width:thin; border-color:#d0d0f0; border-style:solid; width:416px;">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="187px" ></asp:Panel >
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button3" runat="server" CssClass="button-query" ForeColor="White" Font-Size="Small" Text="上传" OnClick="uploadfj_click" />
                    <asp:Label ID="lblUpload" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        </div>

        <asp:Label ID="lblGongcheng" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblZhiliang" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblFujianList" runat="server" Text="" Visible="false"></asp:Label>



















        <%--导入历史--%>
        <div style="min-height:400px">
        
        <table width="100%">
            <tr>
                <td>
                    <h3 style="text-align:center">压裂数据导入历史记录</h3>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="导入日期:"></asp:Label>
                    <asp:TextBox ID="tbKaishiRiqi" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Text="至"></asp:Label>
                    <asp:TextBox ID="tbJieshuRiqi" runat="server" Width="80px" CssClass="datepicker"></asp:TextBox>
                    <asp:Label ID="Label8" runat="server" Text="　区块:"></asp:Label>
                    <asp:DropDownList ID="lstLishiQukuai" runat="server"></asp:DropDownList>
                    <asp:Label ID="Label9" runat="server" Text="　数据表:"></asp:Label>
                    <asp:DropDownList ID="lstLishiTable" runat="server">
                        <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                        <asp:ListItem Text="今日工作" Value="今日工作"></asp:ListItem>
                        <asp:ListItem Text="明日计划" Value="明日计划"></asp:ListItem>
                        <asp:ListItem Text="射孔" Value="射孔"></asp:ListItem>
                        <asp:ListItem Text="压前检查" Value="压前检查"></asp:ListItem>
                        <asp:ListItem Text="压裂施工" Value="压裂施工"></asp:ListItem>
                        <asp:ListItem Text="压后排液" Value="压后排液"></asp:ListItem>
                        <asp:ListItem Text="下泵" Value="下泵"></asp:ListItem>
                        <asp:ListItem Text="失败原因说明" Value="失败原因说明"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label5" runat="server" Text="　用户:"></asp:Label>
                    <asp:TextBox ID="tbUser" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="导入数量:"></asp:Label>
                    <asp:TextBox ID="tbNumBegin" runat="server" Width="80px"></asp:TextBox>
                    <asp:Label ID="Label12" runat="server" Text="至"></asp:Label>
                    <asp:TextBox ID="tbNumEnd" runat="server" Width="80px"></asp:TextBox>
                    <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="Button5" 
                        runat="server" Text="刷新" onclick="Button5_Click" />
                    <asp:Label ID="lblLishi" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>

        <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False" 
            CssClass="Table100" AllowPaging="true" 
            OnPageIndexChanging="Page_IndexChanging" 
            OnRowDataBound="GridViewRowBound" ShowHeader="true">

            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle CssClass="tableLine" />
            <EditRowStyle BackColor="#9EB6CE" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle CssClass="tableLineHeader" />
            <AlternatingRowStyle CssClass="tableLineAlternating" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="序号" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="riqi" HeaderText="日期" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="qukuai" HeaderText="区块" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="name" HeaderText="表名" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="usr" HeaderText="用户" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="number" HeaderText="导入数量" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                PageButtonCount="25" PreviousPageText="上一页"  Position="Bottom"/>
        </asp:GridView>
        </div>
    </form>
</body>
</html>
