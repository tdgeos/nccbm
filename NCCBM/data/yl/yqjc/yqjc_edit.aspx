<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yqjc_edit.aspx.cs" Inherits="NCCBM.data.yl.yqjc.yqjc_edit" %>

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
              <h3 style="text-align:center">修 改 压 前 检 查 数 据</h3></td>
          </tr> 
    </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" Width="850px">
        <tr>
             <td style="width:100px;">井号</td>
            <td>                                                          
                <asp:TextBox ID="TB_jinghao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
             <td style="width:100px;">层位</td> 
            <td>                                                        
                <asp:TextBox ID="TB_cengwei" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
             <td style="width:100px;">检查日期</td> 
            <td>                                                         
                <asp:TextBox ID="TB_jianchariqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>   
        </tr>
        <tr>
            <td>液体检测_前置液_浊度_罐顶</td> 
            <td>                                                           
                <asp:TextBox ID="TB_ytjc_qzy_zd_guanding" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>液体检测_前置液_浊度_罐底</td>
            <td>                                                       
                <asp:TextBox ID="TB_ytjc_qzy_zd_guandi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>液体检测_前置液_盐度_罐顶</td> 
            <td>                                                         
                <asp:TextBox ID="TB_ytjc_qzy_yd_guanding" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
        </tr>
        <tr>
            <td>液体检测_前置液_盐度_罐底</td>
            <td>                                                          
                <asp:TextBox ID="TB_ytjc_qzy_yd_guandi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>液体检测_携砂液_浊度_罐顶</td> 
            <td>                                                        
                <asp:TextBox ID="TB_ytjc_xsy_zd_guanding" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>液体检测_携砂液_浊度_罐底</td> 
            <td>                                                         
                <asp:TextBox ID="TB_ytjc_xsy_zd_guandi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
        </tr>
        <tr>
            <td>液体检测_携砂液_盐度_罐顶</td>
            <td>                                                          
                <asp:TextBox ID="TB_ytjc_xsy_yd_guanding" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>液体检测_携砂液_盐度_罐底</td> 
            <td>                                                        
                <asp:TextBox ID="TB_ytjc_xsy_yd_guandi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>支撑剂浊度检测_中细砂</td> 
            <td>                                                        
                <asp:TextBox ID="TB_zcjzdjc_zhongxisha" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>支撑剂浊度检测_中砂</td>
            <td>                                                          
                <asp:TextBox ID="TB_zcjzdjc_zhongsha" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>支撑剂浊度检测_粗砂</td> 
            <td>                                                        
                <asp:TextBox ID="TB_zcjzdjc_cusha" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>HSE检查情况</td> 
            <td>                                                        
                <asp:TextBox ID="TB_hse" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>  
            <td>监督人</td> 
            <td>                                                         
                <asp:TextBox ID="TB_jianduren" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>施工队伍</td> 
            <td>                                                         
                <asp:TextBox ID="TB_shigongduiwu" runat="server" CssClass="inputwidth"></asp:TextBox>
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
                <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="btnOk" runat="server" Text="确定" onclick="btnOk_Click"/>
                &nbsp;&nbsp;
                <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click"/>
     	    </td>
        </tr>      
    </table>
    </form>
</body>
</html>
