<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ylsg_edit.aspx.cs" Inherits="NCCBM.data.yl.ylsg.ylsg_edit" %>

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
              <h3 style="text-align:center">修 改 压 裂 施 工 数 据</h3></td>
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
             <td style="width:100px;">施工日期</td> 
            <td>                                                         
                <asp:TextBox ID="TB_shigongriqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>   
        </tr>
        <tr>
            <td>煤层顶深</td> 
            <td>                                                           
                <asp:TextBox ID="TB_meicengjingduan_dingjie" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>煤层底深</td>
            <td>                                                       
                <asp:TextBox ID="TB_meicengjingduan_dijie" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>煤层厚度</td> 
            <td>                                                         
                <asp:TextBox ID="TB_meicenghoudu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
        </tr>
        <tr>
            <td>射孔顶深</td>
            <td>                                                          
                <asp:TextBox ID="TB_shekongjingduan_dingjie" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>射孔底深</td> 
            <td>                                                        
                <asp:TextBox ID="TB_shekongjingduan_dijie" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>射开厚度</td> 
            <td>                                                         
                <asp:TextBox ID="TB_shekaihoudu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
        </tr>
        <tr>
            <td>压裂液类型</td>
            <td>                                                          
                <asp:TextBox ID="TB_yalieyeleixing" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>设计前置液</td> 
            <td>                                                        
                <asp:TextBox ID="TB_qianzhiye_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>实际前置液</td> 
            <td>                                                         
                <asp:TextBox ID="TB_qianzhiye_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
        </tr>
        <tr>  
            <td>设计携沙液</td> 
            <td>                                                         
                <asp:TextBox ID="TB_xieshaye_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>实际携沙液</td> 
            <td>                                                        
                <asp:TextBox ID="TB_xieshaye_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>携沙液最低压力</td> 
            <td>                                                        
                <asp:TextBox ID="TB_xieshaye_zdyl" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>  
        <tr>  
            <td>携沙液最高压力</td> 
            <td>                                                         
                <asp:TextBox ID="TB_xieshaye_zgyl" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>携沙液平均压力</td> 
            <td>                                                        
                <asp:TextBox ID="TB_xieshaye_pjyl" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>平均排量</td> 
            <td>                                                        
                <asp:TextBox ID="TB_xieshaye_pjpl" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>  
        <tr>  
            <td>设计顶替液</td> 
            <td>                                                         
                <asp:TextBox ID="TB_dingtiye_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>实际顶替液</td> 
            <td>                                                        
                <asp:TextBox ID="TB_dingtiye_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>设计总液量</td> 
            <td>                                                        
                <asp:TextBox ID="TB_zongyeliang_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>  
        <tr>  
            <td>实际总液量</td> 
            <td>                                                         
                <asp:TextBox ID="TB_zongyeliang_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>设计中细砂量</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jsl_shejizhongxisha" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>设计中砂量</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jsl_shejizhongsha" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>  
        <tr>  
            <td>设计粗砂量</td> 
            <td>                                                         
                <asp:TextBox ID="TB_jsl_shejicusha" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>设计加砂总量</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jsl_shejizongshaliang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>实际中细砂量</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jsl_shijizhongxisha" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>  
        <tr>  
            <td>实际中砂量</td> 
            <td>                                                         
                <asp:TextBox ID="TB_jsl_shijizhongsha" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>实际粗砂量</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jsl_shijicusha" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>实际加砂总量</td> 
            <td>                                                        
                <asp:TextBox ID="TB_jsl_shijizongshaliang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>  
        <tr>  
            <td>设计平均砂比</td> 
            <td>                                                         
                <asp:TextBox ID="TB_pingjunshabi_sheji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>实际平均砂比</td> 
            <td>                                                        
                <asp:TextBox ID="TB_pingjunshabi_shiji" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>破裂压力</td> 
            <td>                                                        
                <asp:TextBox ID="TB_polieyali" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>  
        <tr>  
            <td>停泵压力</td> 
            <td>                                                         
                <asp:TextBox ID="TB_tingbengyali" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>30min后降至</td> 
            <td>                                                        
                <asp:TextBox ID="TB_30fenzhong" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>是否成功</td> 
            <td>                                                        
                <asp:TextBox ID="TB_shifouhege" runat="server" CssClass="inputwidth"></asp:TextBox>
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
            <td>区块</td> 
            <td>                                                        
                <asp:TextBox ID="TB_qukuai" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr> 
        <tr>  
            <td>是否压完</td> 
            <td>                                                         
                <asp:TextBox ID="TB_shifouyawan" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>压裂工艺方法</td> 
            <td>                                                        
                <asp:TextBox ID="TB_shigongleixing" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
            <td>完成设计砂量的百分比</td> 
            <td>                                                        
                <asp:TextBox ID="TB_wanchengbaifenbi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
        </tr> 
        <tr> 
            <td>设备运转情况</td> 
            <td >
                <asp:TextBox ID="TB_teshuqingkuang" runat="server" ></asp:TextBox>                
            </td>
        </tr>  
        <tr> 
            <td>备注</td> 
            <td align="left"colspan="5">
                <asp:TextBox ID="TB_beizhu" runat="server" Height="100px" Width="740px" TextMode="MultiLine"></asp:TextBox>                
            </td>
        </tr> 

        <tr>
            <td align="center" colspan="6">
                <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="addnew" runat="server" Text="确定" OnClick="addnew_Click"/>
                &nbsp;&nbsp;
                <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" ID="btnreturn" runat="server" Text="返回" onclick="btnreturn_Click"/>
     	    </td>
        </tr>      
    </table>
    </form>
</body>
</html>
