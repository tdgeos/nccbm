<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sk_edit.aspx.cs" Inherits="NCCBM.data.yl.sk.sk_edit" %>

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
              <h3 style="text-align:center">修 改 射 孔 数 据</h3></td>
          </tr> 
    </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" width="850px">      
        <tr>
             <td style="width:100px;">井号</td>
            <td>                                                          
                <asp:TextBox ID="TB_jinghao" runat="server"  CssClass="inputwidth"></asp:TextBox>
            </td>
             <td style="width:100px;">层位</td> 
            <td>                                                        
                <asp:TextBox ID="TB_cengwei" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
             <td style="width:100px;">射孔日期</td> 
            <td>                                                         
                <asp:TextBox ID="TB_shekongriqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>   
        </tr>
        <tr>
            <td>射孔井段</td>
            <td>                                                          
                <asp:TextBox ID="TB_shekongjingduan" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>设开厚度</td> 
            <td>                                                         
                <asp:TextBox ID="TB_shekaihoudu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>设计弹数</td>
            <td>                                                          
                <asp:TextBox ID="TB_shejidanshu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>实发弹数</td> 
            <td>                                                        
                <asp:TextBox ID="TB_shifadanshu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>软探砂面</td> 
            <td>                                                         
                <asp:TextBox ID="TB_ruantanshamian" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>硬探砂面</td>
            <td>                                                          
                <asp:TextBox ID="TB_yingtanshamian" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>人工井底</td> 
            <td>                                                        
                <asp:TextBox ID="TB_rengongjingdi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>监督人</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jianduren" runat="server" CssClass="inputwidth"></asp:TextBox>
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
