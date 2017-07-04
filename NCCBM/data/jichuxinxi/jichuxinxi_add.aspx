<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jichuxinxi_add.aspx.cs" Inherits="NCCBM.data.jichuxinxi.jichuxinxi_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
              <h3 style="text-align:center">添加井基础信息数据</h3></td>
          </tr> 
    </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" width="850px">
        <tr>
            <td style="width:100px;">井号</td>
            <td>                                                          
                <asp:TextBox ID="tbJinghao" CssClass="inputwidth" runat="server"></asp:TextBox>
            </td>
            <td style="width:100px;">横坐标</td> 
            <td>                                                        
                <asp:TextBox ID="tbX" CssClass="inputwidth" runat="server"></asp:TextBox>
            </td>
            <td style="width:100px;">纵坐标</td> 
            <td>                                                         
                <asp:TextBox ID="tbY"  CssClass="inputwidth" runat="server"></asp:TextBox>
            </td>   
        </tr>
        <tr>   
            <td>海拔</td> 
            <td>                                                           
                <asp:TextBox ID="tbH" CssClass="inputwidth" runat="server"></asp:TextBox>
            </td>
            <td>井别</td>
            <td>                                                       
                <asp:TextBox ID="tbJingbie" CssClass="inputwidth" runat="server"></asp:TextBox>
            </td>
            <td>井型</td>
            <td>                                                       
                <asp:TextBox ID="tbJingxing" CssClass="inputwidth" runat="server"></asp:TextBox>
            </td>
        </tr>   
        <tr>   
            <td>设计井深</td> 
            <td>                                                           
                <asp:TextBox ID="tbShejijingshen" CssClass="inputwidth" runat="server"></asp:TextBox>
            </td>
            <td>施工单位</td>
            <td>                                                       
                <asp:TextBox ID="tbShigongdanwei" CssClass="inputwidth" runat="server"></asp:TextBox>
            </td>
            <td>开钻日期</td> 
            <td>                                                           
                <asp:TextBox ID="tbKaizuanriqi" runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>
        </tr> 
        <tr>   
            <td>当前状态</td>
            <td>                                                       
                <asp:TextBox ID="tbDangqianzhuangtai" CssClass="inputwidth" runat="server"></asp:TextBox>
            </td>
            <td>区块</td> 
            <td>                                                           
                <asp:TextBox ID="tbQukuai" CssClass="inputwidth" runat="server"></asp:TextBox>
            </td>
            <td>生产日期</td>
            <td>                                                       
                <asp:TextBox ID="tbGengxinriqi" runat="server"  CssClass="inputwidth datepicker"></asp:TextBox>
            </td>
        </tr> 
        <tr>   
            <td>备注</td>
            <td>                                                       
                <asp:TextBox ID="tbBeizhu" CssClass="inputwidth" runat="server"></asp:TextBox>
            </td>
        </tr>  
        <tr>
             <td align="center" colspan="6">
             <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="btnOk" 
                     runat="server" Text="确定" onclick="btnOk_Click" />
              &nbsp;&nbsp;
              <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" 
                     ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click1" />
     	  </td>
        </tr>      
    </table>

    <br /><br />

    <table width="850px">
        <tr >                
            <td style=" height: 30px">
              <h3 style="text-align:center">批量添加井基础信息数据</h3>
            </td>
        </tr> 
        <tr>
            <td>
                文件：
                <asp:TextBox ID="tbDataFile" runat="server" width="195px" ></asp:TextBox>
                <asp:FileUpload ID="fuData" runat="server" style="filter: alpha(opacity=0);
                    width:65px; height:auto; cursor: hand; opacity: 0; position: absolute; z-index: 2;" 
                    onchange="javascript:__doPostBack('lbData','')" />
                <input id="Button4" type="button" value="浏 览"  class="button-query" style="Color:#fff;" />
                <asp:LinkButton ID="lbData" runat="server"></asp:LinkButton>
                <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" 
                     ID="Button1" runat="server" Text="导 入" onclick="Button1_Click"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
