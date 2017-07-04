<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zgys_edit.aspx.cs" Inherits="NCCBM.data.Report.zgys.zgys_edit" %>

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
    <table Width="850px" border="0" class="metable" cellpadding="0" cellspacing="0" >
          <tr >                
            <td  class="tdhead" style=" height: 30px">
              <h3 style="text-align:center">修 改 阻 工 因 素 数 据</h3></td>
          </tr> 
   </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" Width="850px" >
        <tr>
             <td style="width:100px;"> 录入日期</td>
            <td>                                                          
                <asp:TextBox ID="TB_lururiqi" runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>
             <td style="width:100px;"> 区块</td> 
            <td>                                                        
                <asp:TextBox ID="TB_qukuai" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
             <td style="width:100px;"> 套管质量问题</td> 
            <td>                                                         
                <asp:TextBox ID="TB_taoguan" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
        </tr>
        <tr>   
            <td>下雨及其影响天数</td> 
            <td>                                                           
                <asp:TextBox ID="TB_xiayu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>工农关系影响天数</td>
            <td>                                                       
                <asp:TextBox ID="TB_gongnong" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>车辆维修天数</td> 
            <td>                                                        
                <asp:TextBox ID="TB_cheliang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>      
        </tr>
        <tr>
        
            <td>井场搬迁</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jingchang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>等待井台搬迁</td> 
            <td>                                                         
                <asp:TextBox ID="TB_dengdaijingtai" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>备水配液</td>
            <td>                                                       
                <asp:TextBox ID="TB_beishui" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>      
        <tr> 
            <td style="width:95px">备注</td>  
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
