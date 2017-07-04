<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xtg_edit.aspx.cs" Inherits="NCCBM.data.zj.xtg.xtg_edit" %>

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
              <h3 style="text-align:center">修 改 下 套 管 数 据</h3></td>
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
            <td>完钻日期</td>
            <td>                                                       
                <asp:TextBox ID="TB_wanzuanriqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>
            <td>套管下深</td> 
            <td>                                                        
                <asp:TextBox ID="TB_taoguanxiashen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>      
        </tr>
        <tr>
            <td>钢级</td> 
            <td>                                                        
                <asp:TextBox ID="TB_gangji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>尺寸</td> 
            <td>                                                         
                <asp:TextBox ID="TB_chicun" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>丝扣</td>
            <td>                                                       
                <asp:TextBox ID="TB_sikou" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>     
        <tr>  
            <td>外观</td> 
            <td>                                                        
                <asp:TextBox ID="TB_waiguan" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>平均壁厚</td> 
            <td>                                                        
                <asp:TextBox ID="TB_pingjunbihou" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>平均外径</td>
            <td>                                                        
                <asp:TextBox ID="TB_pingjunwaijing" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
        </tr>   
         <tr>
            <td>密封脂</td>
            <td>                                                         
                <asp:TextBox ID="TB_mifengzhi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>紧扣</td> 
            <td>                                                          
                <asp:TextBox ID="TB_jingkou" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>浮箍、浮鞋检查</td> 
            <td>                                                         
                <asp:TextBox ID="TB_fugu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>
        <tr>  
            <td>套管数据表是否按要求填写</td> 
            <td>                                                         
                <asp:TextBox ID="TB_taoguanshuju" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>套管批次编号是否与物资部出库单一致</td>
            <td>                                                         
                <asp:TextBox ID="TB_taoguanpici" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>存在问题</td> 
            <td>                                                          
                <asp:TextBox ID="TB_cunzaiwenti" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>
        <tr>
            <td>整改内容及措施</td>
            <td>                                                        
                <asp:TextBox ID="TB_zhenggaicuoshi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>检测方式</td>
            <td>                                                        
                <asp:TextBox ID="TB_jiancefangshi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>套管厂家</td>
            <td>                                                        
                <asp:TextBox ID="TB_taoguanchangjia" runat="server" CssClass="inputwidth"></asp:TextBox>
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
              &nbsp;&nbsp;<asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="btnreturn" runat="server"  TabIndex="11" Text="返回" 
                     onclick="btnreturn_Click1"/>
     	  </td>
        </tr>      
    </table>
    </form>
</body>
</html>
