<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ykys_add.aspx.cs" Inherits="NCCBM.data.Report.ykys.ykys_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../css/superTables.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/jquery-ui-1.8.20.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui.zh-CN.js" type="text/javascript"></script>
    <script src="../../../js/datetime.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">    
    <table width="850px" border="0" class="metable" cellpadding="0" cellspacing="0" >
          <tr >                
            <td  class="tdhead" style=" height: 30px">
              <h3 style="text-align:center">增 加 一 开 验 收 数 据</h3></td>
          </tr> 
   </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" width="850px" >
        <tr>
            <td style="width:100px;">录入日期</td>                                                      
            <td>                                                          
                <asp:TextBox ID="TB_lururiqi" runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>
            <td style="width:100px;">区块</td> 
            <td>                                                        
                <asp:TextBox ID="TB_qukuai" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td style="width:100px;">井号</td> 
            <td>                                                         
                <asp:TextBox ID="TB_jinghao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
        </tr>
        <tr>   
            <td>施工队伍</td> 
            <td>                                                           
                <asp:TextBox ID="TB_shigongduiwu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>监督</td>
            <td>                                                       
                <asp:TextBox ID="TB_jiandu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>一开验收时间</td> 
            <td>                                                        
                <asp:TextBox ID="TB_yikaishijian" runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>      
        </tr>
        <tr>
        
            <td>二开验收时间</td> 
            <td>                                                        
                <asp:TextBox ID="TB_erkaishijian" runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td> 
        </tr>
        <tr>
            <td>不合格因素</td> 
            <td align="left"colspan="5">                                                         
                <asp:TextBox ID="TB_yinsu" runat="server" Height="100px" Width="740px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>处理措施</td>
            <td align="left"colspan="5">                                                       
                <asp:TextBox ID="TB_cuoshi" runat="server" Height="100px" Width="740px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>      
        <tr> 
            <td>备注</td>  
            <td align="left"colspan="5">
                <asp:TextBox ID="TB_beizhu" runat="server" Height="100px" Width="740px" TextMode="MultiLine"></asp:TextBox>                
            </td>
        </tr>  
        <tr>
            <td align="center" colspan="6">
                <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="addnew" runat="server" Text="确定" OnClick="addnew_Click"/>
                &nbsp;&nbsp;<asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="btnreturn" runat="server"  TabIndex="11" Text="返回" onclick="btnreturn_Click1"/>
            </td>
        </tr>      
    </table>
    </form>
</body>
</html>


