<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xb_add.aspx.cs" Inherits="NCCBM.data.yl.xb.xb_add" %>

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
    <table Width="842px" border="0" class="metable" cellpadding="0" cellspacing="0" >
          <tr >                
            <td  class="tdhead" style=" height: 30px">
              <h3 style="text-align:center">增 加 下 泵 数 据</h3></td>
          </tr> 
    </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" Width="842px">
        <tr>
            <td>下泵日期</td>
            <td>                                                          
                <asp:TextBox ID="TB_xiabengriqi" runat="server" CssClass="datepicker"></asp:TextBox>
            </td>
            <td>井号</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jinghao" runat="server" required="1" ></asp:TextBox>
            </td>
            <td>施工内容</td> 
            <td>                                                         
                <asp:TextBox ID="TB_shigongneirong" runat="server"></asp:TextBox>
            </td>   
        </tr>
        <tr>
            <td>完井情况</td> 
            <td>                                                           
                <asp:TextBox ID="TB_wanjingqingkuang" runat="server"></asp:TextBox>
            </td>
            <td>验收情况</td>
            <td>                                                       
                <asp:TextBox ID="TB_yanshouqingkuang" runat="server"></asp:TextBox>
            </td>
            <td>井台情况</td> 
            <td>                                                         
                <asp:TextBox ID="TB_jingtaiqingkuang" runat="server"></asp:TextBox>
            </td>   
        </tr>
        <tr>
            <td>施工单位</td>
            <td>                                                          
                <asp:TextBox ID="TB_shigongdanwei" runat="server"></asp:TextBox>
            </td>
            <td>区块</td> 
            <td>                                                        
                <asp:TextBox ID="TB_qukuai" runat="server"></asp:TextBox>
            </td>   
            <td>日期</td>
            <td>                                                          
                <asp:TextBox ID="TB_riqi" runat="server" CssClass="datepicker"></asp:TextBox>
            </td>  
        </tr>
                <tr> 
            <td style="width:95px">备注</td>  
            <td align="left"colspan="5">
                <asp:TextBox ID="TB_beizhu" runat="server" Height="100px" Width="730px" TextMode="MultiLine"></asp:TextBox>                
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
