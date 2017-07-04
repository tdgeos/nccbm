<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gj_add.aspx.cs" Inherits="NCCBM.data.zj.gj.gj_add" %>

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
               <h3 style="text-align:center">增 加 固 井 数 据</h3>
            </td>
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
            <td>完钻井深</td> 
            <td>                                                           
                <asp:TextBox ID="TB_wanzuanjingshen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>施工日期</td>
            <td>                                                       
                <asp:TextBox ID="TB_shigongriqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>
            <td>水泥返高(设计)</td> 
            <td>                                                        
                <asp:TextBox ID="TB_fangao_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>      
        </tr>
        <tr>
        
            <td>水泥返高(实际)</td> 
            <td>                                                        
                <asp:TextBox ID="TB_fangao_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>水泥浆密度(设计)</td> 
            <td>                                                         
                <asp:TextBox ID="TB_midu_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>水泥浆密度(实际)</td>
            <td>                                                       
                <asp:TextBox ID="TB_midu_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>     
        <tr>  
            <td>水泥浆用量(设计)</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jiangyongliang_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>水泥浆用量(实际)</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jiangyongliang_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>水泥用量(设计)</td>
            <td>                                                        
                <asp:TextBox ID="TB_yongliang_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
        </tr>   
        <tr>
            <td>水泥用量(实际)</td>
            <td>                                                         
                <asp:TextBox ID="TB_yongliang_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>顶替量(设计)</td> 
            <td>                                                          
                <asp:TextBox ID="TB_dingtiliang_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>顶替量(实际)</td> 
            <td>                                                         
                <asp:TextBox ID="TB_dingtiliang_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>
        <tr>  
            <td>碰压(设计)</td> 
            <td>                                                         
                <asp:TextBox ID="TB_pengya_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>碰压(实际)</td>
            <td>                                                         
                <asp:TextBox ID="TB_pengya_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>施工中存在问题</td> 
            <td>                                                          
                <asp:TextBox ID="TB_shigongwenti" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>
        <tr>
            <td>复杂情况</td>
            <td>                                                        
                <asp:TextBox ID="TB_fuzaqingkuang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>检测方式</td>
            <td>                                                        
                <asp:TextBox ID="TB_jiancefangshi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>区块</td>
            <td>                                                        
                <asp:TextBox ID="TB_qukuai" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>  
        <tr>
            <td>日期</td>
            <td>                                                        
                <asp:TextBox ID="TB_riqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
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
                &nbsp;&nbsp;
                <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="btnreturn" runat="server"  TabIndex="11" Text="返回" onclick="btnreturn_Click1"/>
     	    </td>
        </tr>   
    </table>
    </form>
</body>
</html>
