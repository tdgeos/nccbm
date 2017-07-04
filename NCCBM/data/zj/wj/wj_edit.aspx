<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wj_edit.aspx.cs" Inherits="NCCBM.data.zj.wj.wj_edit" %>

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
              <h3 style="text-align:center">修 改 完 井 数 据</h3></td>
          </tr> 
    </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" Width="850px" >
        <tr>
             <td style="width:100px;">负责监督</td>
            <td>                                                          
                <asp:TextBox ID="TB_fuzejiandu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
             <td style="width:100px;">队号</td> 
            <td>                                                        
                <asp:TextBox ID="TB_duihao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
             <td style="width:100px;">井号</td> 
            <td>                                                         
                <asp:TextBox ID="TB_jinghao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
        </tr>
        <tr>   
            <td>标志管位置(设计)</td> 
            <td>                                                           
                <asp:TextBox ID="TB_biaozhiguan_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>标志管位置(实际）</td>
            <td>                                                       
                <asp:TextBox ID="TB_biaozhiguan_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>人工井底(设计）</td> 
            <td>                                                        
                <asp:TextBox ID="TB_rengongjingdi_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>      
        </tr>
        <tr>
        
            <td>人工井底(实际）</td> 
            <td>                                                        
                <asp:TextBox ID="TB_rengongjingdi_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>固井质量CBL</td> 
            <td>                                                         
                <asp:TextBox ID="TB_gujingzhiliangCBL" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>固井质量VDL</td>
            <td>                                                       
                <asp:TextBox ID="TB_gujingzhiliangVDL" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>     
        <tr>  
            <td>产套偏差(标准）</td> 
            <td>                                                        
                <asp:TextBox ID="TB_shuipingpiancha_bzh" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>产套偏差(水平）</td> 
            <td>                                                        
                <asp:TextBox ID="TB_shuipingpiancha_hor" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>产套偏差(垂直）</td> 
            <td>                                                        
                <asp:TextBox ID="TB_shuipingpiancha_ver" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
        </tr>   
        <tr>
            <td>试压(开始）</td>
            <td>                                                        
                <asp:TextBox ID="TB_shiya_start" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>试压(结束）</td>
            <td>                                                         
                <asp:TextBox ID="TB_shiya_end" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>环形钢板焊接(上缘)</td> 
            <td>                                                          
                <asp:TextBox ID="TB_gangbanhanjie_up" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>   
        <tr>
            <td>环形钢板焊接(下缘)</td> 
            <td>                                                         
                <asp:TextBox ID="TB_gangbanhanjie_down" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>井口高度</td> 
            <td>                                                         
                <asp:TextBox ID="TB_jingkougaodu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>丝扣是否完好</td>
            <td>                                                         
                <asp:TextBox ID="TB_shikouwanhao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>   
        <tr>
            <td>存在问题</td> 
            <td>                                                          
                <asp:TextBox ID="TB_cunzaiwenti" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>复杂情况</td>
            <td>                                                        
                <asp:TextBox ID="TB_fuzaqingkuang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>检测方式</td>
            <td>                                                        
                <asp:TextBox ID="TB_jiancefangshi" runat="server" CssClass="inputwidth"></asp:TextBox>
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
                <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="addnew" runat="server" Text="确定" OnClick="addnew_Click" 
                    style="height: 21px"/>
                &nbsp;&nbsp;<asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="btnreturn" runat="server"  TabIndex="11" Text="返回" onclick="btnreturn_Click1"/>
     	    </td>
        </tr>      
    </table>
    </form>
</body>
</html>
