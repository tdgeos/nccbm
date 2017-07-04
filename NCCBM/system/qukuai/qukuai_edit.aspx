<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qukuai_edit.aspx.cs" Inherits="NCCBM.system.qukuai.qukuai_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" class="metable" cellpadding="0" cellspacing="0" >
          <tr >                
            <td  class="tdhead" style=" height: 30px">
              <h3 style="text-align:center">修改区块数据</h3></td>
          </tr> 
    </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" width="100%">
        <tr>
        <td>&nbsp;<span style=" font-size: 10pt;">区块名称</span></td>
            <td>                                                          
                <asp:TextBox ID="TB_mingcheng" runat="server"   Width="200px" TabIndex="2"></asp:TextBox>
            </td>
        <td>&nbsp;<span style=" font-size: 10pt;">地理位置</span></td>

            <td>                                                        
                <asp:TextBox ID="TB_weizhi" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        <tr>   
            <td>&nbsp;<span style=" font-size: 10pt;">简介</span></td> 
            <td>                                                         
                <asp:TextBox ID="TB_jianjie" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
            </td>   
            <td>&nbsp;<span style=" font-size: 10pt;">备注</span></td> 
            <td>                                                           
                <asp:TextBox ID="TB_beizhu" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
            </td>
        </tr>   
        <tr>
             <td align="center" colspan="6">
             <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" ID="btnOk" 
                     runat="server" Text="确定" onclick="btnOk_Click" />
              &nbsp;&nbsp;
              <asp:Button CssClass="button" ForeColor="White" Font-Size="Small" 
                     ID="btnCancel" runat="server" Text="返回" onclick="btnCancel_Click" />
     	  </td>
        </tr>      
    </table>
    </form>
</body>
</html>
