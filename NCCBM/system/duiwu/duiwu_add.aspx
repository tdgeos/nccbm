<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="duiwu_add.aspx.cs" Inherits="NCCBM.system.duiwu.duiwu_add" %>

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
              <h3 style="text-align:center">添加施工队伍数据</h3></td>
          </tr> 
    </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" width="100%">
        <tr>
        <td>&nbsp;<span style=" font-size: 10pt;">队伍名称</span></td>
            <td>                                                          
                <asp:TextBox ID="TB_name" runat="server"   Width="200px" TabIndex="2"></asp:TextBox>
            </td>
        <td>&nbsp;<span style=" font-size: 10pt;">简称</span></td>

            <td>                                                        
                <asp:TextBox ID="TB_jiancheng" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        <tr>   
            <td>&nbsp;<span style=" font-size: 10pt;">资质</span></td> 
            <td>                                                         
                <asp:TextBox ID="TB_zizhi" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
            </td>   
            <td>&nbsp;<span style=" font-size: 10pt;">队伍规模</span></td> 
            <td>                                                           
                <asp:TextBox ID="TB_guimo" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
            </td>
        </tr>   
        <tr> 
            <td>&nbsp;<span style=" font-size: 10pt;">联系电话</span></td>
            <td>                                                       
                <asp:TextBox ID="TB_dianhua" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
            </td>
            <td>&nbsp;<span style=" font-size: 10pt;">电子邮箱</span></td>
            <td>                                                       
                <asp:TextBox ID="TB_youxiang" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
            </td>
        </tr>   
        <tr> 
            <td>&nbsp;<span style=" font-size: 10pt;">办公地点</span></td>
            <td>                                                       
                <asp:TextBox ID="TB_didian" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
            </td>
            <td>&nbsp;<span style=" font-size: 10pt;">区块</span></td>  
            <td>
                <asp:TextBox ID="TB_qukuai" runat="server" Width="200px" TabIndex="2"></asp:TextBox>                
            </td>
        </tr>  
        <tr> 
            <td>&nbsp;<span style=" font-size: 10pt;">施工类型</span></td>
            <td>                                                       
                <asp:TextBox ID="TB_zjoryl" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
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
