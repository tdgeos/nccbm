<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zj_edit.aspx.cs" Inherits="NCCBM.data.zj.zj.zj_edit" %>

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
              <h3 style="text-align:center">修 改 钻 进 数 据</h3></td>
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
            <td>开钻日期</td> 
            <td>                                                           
                <asp:TextBox ID="TB_kaizuanriqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>
            <td>设计井深</td>
            <td>                                                       
                <asp:TextBox ID="TB_shejijingshen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>目前井深</td> 
            <td>                                                        
                <asp:TextBox ID="TB_muqianjingshen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>      
        </tr>
        <tr>
            <td>当日进尺</td> 
            <td>                                                        
                <asp:TextBox ID="TB_dangrijinchi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>工况</td> 
            <td>                                                         
                <asp:TextBox ID="TB_gongkuang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>密度</td>
            <td>                                                       
                <asp:TextBox ID="TB_midu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>     
        <tr>  
            <td>粘度</td> 
            <td>                                                        
                <asp:TextBox ID="TB_niandu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>井段</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jingduan" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>井斜(设计)</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jingxie_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
        </tr>   
        <tr>
            <td>井斜(实际)</td>
            <td>                                                        
                <asp:TextBox ID="TB_jingxie_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>方位(设计)</td>
            <td>                                                         
                <asp:TextBox ID="TB_fangwei_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>方位(实际)</td> 
            <td>                                                          
                <asp:TextBox ID="TB_fangwei_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>
        <tr>
            <td>狗腿度(设计)</td> 
            <td>                                                         
                <asp:TextBox ID="TB_goutui_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>狗腿度(实际)</td> 
            <td>                                                         
                <asp:TextBox ID="TB_goutui_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>HSE情况</td>
            <td>                                                         
                <asp:TextBox ID="TB_HSEqingkuang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>复杂情况</td> 
            <td>                                                          
                <asp:TextBox ID="TB_fuzaqingkuang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>整改内容</td>
            <td>                                                        
                <asp:TextBox ID="TB_zhenggaineirong" runat="server" CssClass="inputwidth"></asp:TextBox>
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
                <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="addnew" runat="server" Text="确定" OnClick="addnew_Click"/>
                &nbsp;&nbsp;<asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="btnreturn" runat="server"  TabIndex="11" Text="返回" onclick="btnreturn_Click1"/>
     	    </td>
        </tr>      
    </table>
    </form>
</body>
</html>