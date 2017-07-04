<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sbyysm_add.aspx.cs" Inherits="NCCBM.data.yl.sbyysm.sbyysm_add" %>

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
              <h3 style="text-align:center">增 加 失 败 原 因 说 明 数 据</h3></td>
          </tr> 
    </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" Width="850px">
        <tr>
             <td style="width:100px;">井号</td>
            <td>                                                          
                <asp:TextBox ID="TB_jinghao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
             <td style="width:100px;">层号</td> 
            <td>                                                        
                <asp:TextBox ID="TB_cenghao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
             <td style="width:100px;">压裂日期</td> 
            <td>                                                         
                <asp:TextBox ID="TB_yalieriqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>   
        </tr>  
        <tr>
            <td>加砂百分比</td>
            <td>                                                          
                <asp:TextBox ID="TB_jiashabaifenbi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>施工队伍</td> 
            <td>                                                        
                <asp:TextBox ID="TB_shigongduiwu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>区块</td> 
            <td>                                                        
                <asp:TextBox ID="TB_qukuai" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>
                <tr>
            <td>施工描述</td> 
            <td align="left"colspan="5">                                                           
                <asp:TextBox ID="TB_shigongmiaoshu" runat="server" Width="758px" Height="100px"></asp:TextBox>
            </td>
        </tr>
                <tr> 
            <td>失败原因分析</td> 
            <td align="left"colspan="5">
                <asp:TextBox ID="TB_shibaiyuanyinfenxi" runat="server" Width="758px" Height="100px"></asp:TextBox>                
            </td>
        </tr>
        <tr>
             <td align="center" colspan="6">
             <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="btnOk" runat="server" Text="确定" onclick="btnOk_Click"/>
              &nbsp;&nbsp;
              <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click"/>
     	  </td>
        </tr>      
    </table>
    </form>
</body>
</html>