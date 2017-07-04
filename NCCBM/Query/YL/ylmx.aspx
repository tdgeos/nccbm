<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ylmx.aspx.cs" Inherits="NCCBM.Query.yl.ylmx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href ="../../css/web.css" type ="text/css" rel ="Stylesheet" />
    <link href="../../css/superTables.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="850px">
            <tr>
                <td align="center"><h3>压裂数据查询 - 详细信息</h3></td>
            </tr>
        </table>
        

        <asp:Label ID="lblCD" runat="server" Text="" Visible="false"></asp:Label>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <cc1:TabContainer ID="tc1" runat="server" ActiveTabIndex="0" Height="340px" Width="840px" ScrollBars="Vertical">
            <cc1:TabPanel ID="tp3" runat="server" HeaderText="压裂检查">
                <ContentTemplate>
                    <table border="1" cellpadding="3" cellspacing="1" width="810px">
                        <tr>
                            <td align="right" style="width:120px;">井号</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_yljc1" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td align="right" style="width:120px;">层号</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_yljc2" runat="server" Width="280px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">液体检测-前置液-浊度-罐底1</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_yljc3" runat="server" Width="280px"></asp:TextBox>
                            </td>   
                            <td align="right">液体检测-前置液-浊度-罐底2</td> 
                            <td>                                                           
                                <asp:TextBox ID="TB_yljc4" runat="server" Width="280px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">液体检测-前置液-盐度-罐顶</td>
                            <td>                                                       
                                <asp:TextBox ID="TB_yljc5" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td align="right">液体检测-前置液-盐度-罐底</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_yljc6" runat="server" Width="280px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td align="right">液体检测-携砂液-浊度-罐底1</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_yljc7" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td align="right">液体检测-携砂液-浊度-罐底2</td>  
                            <td>
                                <asp:TextBox ID="TB_yljc8" runat="server" Width="280px"></asp:TextBox>                
                            </td>
                        </tr>
                        <tr>
                            <td align="right">液体检测-携砂液-盐度-罐顶</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_yljc9" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td align="right">液体检测-携砂液-盐度-罐底</td>  
                            <td>
                                <asp:TextBox ID="TB_yljc10" runat="server" Width="280px"></asp:TextBox>                
                            </td>
                        </tr>
                        <tr>
                            <td align="right">支撑剂浊度检测-中细砂</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_yljc11" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td align="right">支撑剂浊度检测-中砂</td>  
                            <td>
                                <asp:TextBox ID="TB_yljc12" runat="server" Width="280px"></asp:TextBox>                
                            </td>
                        </tr>
                        <tr>
                            <td align="right">支撑剂浊度检测-粗砂</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_yljc13" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td align="right">HSE检查情况</td>  
                            <td>
                                <asp:TextBox ID="TB_yljc14" runat="server" Width="280px"></asp:TextBox>                
                            </td>
                        </tr>
                        <tr>
                            <td align="right">监督人</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_yljc15" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td align="right">施工队伍</td>  
                            <td>
                                <asp:TextBox ID="TB_yljc16" runat="server" Width="280px"></asp:TextBox>                
                            </td>
                        </tr>
                        <tr>
                            <td align="right">备注</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_yljc17" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td align="right">日期</td>  
                            <td>
                                <asp:TextBox ID="TB_yljc18" runat="server" Width="280px"></asp:TextBox>                
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tp1" runat="server" HeaderText="射孔">
                <ContentTemplate>
                    <table border="1" cellpadding="10" cellspacing="1" width="810px">
                        <tr>
                            <td align="right" style="width:120px;">井号</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_sk1" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td align="right" style="width:120px;">层位</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_sk2" runat="server" Width="280px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">射孔日期</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_sk3" runat="server" Width="280px"></asp:TextBox>
                            </td>   
                            <td align="right">射孔井段</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_sk4" runat="server" Width="280px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">射开厚度</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_sk5" runat="server" Width="280px"></asp:TextBox>
                            </td>   
                            <td align="right">设计弹数</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_sk6" runat="server" Width="280px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">实发弹数</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_sk7" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td align="right">软探砂面</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_sk8" runat="server" Width="280px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td align="right">硬探砂面</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_sk9" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td align="right">人工井底</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_sk10" runat="server" Width="280px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">监督人</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_sk11" runat="server" Width="280px"></asp:TextBox>
                            </td>  
                            <td align="right">备注</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_sk12" runat="server" Width="280px"></asp:TextBox>
                            </td>
                        </tr>         
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tp2" runat="server" HeaderText="压裂施工">
                <ContentTemplate>
                    <table border="1" cellpadding="2" cellspacing="1" width="810px">
                        <tr>
                            <td align="right" style="width:125px;">井号</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_1" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right" style="width:120px;">层位</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_2" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right" style="width:120px;">施工日期</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_3" runat="server" Width="130px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td align="right">煤层顶深</td> 
                            <td>                                                           
                                <asp:TextBox ID="TB_4" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">煤层底深</td>
                            <td>                                                       
                                <asp:TextBox ID="TB_5" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">煤层厚度</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_6" runat="server" Width="130px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td align="right">射孔顶深</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_7" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">射孔底深</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_8" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">射开厚度</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_9" runat="server" Width="130px"></asp:TextBox>
                            </td>   
                        </tr>              
                        <tr>
                            <td align="right">压裂液类型</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_10" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">设计前置液</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_11" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">实际前置液</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_12" runat="server" Width="130px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td align="right">设计携砂液</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_13" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">实际携砂液</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_14" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">携砂液最低压力</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_15" runat="server" Width="130px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td align="right">携砂液最高压力</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_16" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">携砂液平均压力</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_17" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">平均排量</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_18" runat="server" Width="130px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td align="right">设计顶替液</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_19" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">实际顶替液</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_20" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">设计总液量</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_21" runat="server" Width="130px"></asp:TextBox>
                            </td>   
                        </tr>              
                        <tr>
                            <td align="right">实际总液量</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_22" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">设计中细砂</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_23" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">设计中砂</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_24" runat="server" Width="130px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td align="right">设计粗砂</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_25" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">设计总砂量</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_26" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">实际中细砂</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_27" runat="server" Width="130px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td align="right">实际中砂</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_28" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">实际粗砂</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_29" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">实际总砂量</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_30" runat="server" Width="130px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td align="right">设计平均砂比</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_31" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">实际平均砂比</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_32" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">破裂压力</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_33" runat="server" Width="130px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td align="right">停泵压力</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_34" runat="server" Width="130px"></asp:TextBox>
                            </td> 
                            <td align="right">30秒后降至</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_35" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">是否合格</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_36" runat="server" Width="130px"></asp:TextBox>
                            </td>  
                        </tr>
                        <tr> 
                            <td align="right">监督人</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_37" runat="server" Width="130px"></asp:TextBox>
                            </td> 
                            <td align="right">施工队伍</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_38" runat="server" Width="130px"></asp:TextBox>
                            </td> 
                            <td align="right">区块</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_39" runat="server" Width="130px"></asp:TextBox>
                            </td> 
                        </tr>
                        <tr>  
                            <td align="right">是否压完</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_40" runat="server" Width="130px"></asp:TextBox>
                            </td> 
                            <td align="right">压裂工艺方法</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_41" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td align="right">完成砂量百分比</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_42" runat="server" Width="130px"></asp:TextBox>
                            </td> 
                        </tr> 
                        <tr> 
                            <td align="right">设备运转情况</td> 
                            <td align="left"colspan="5">
                                <asp:TextBox ID="TB_43" runat="server" Height="50px" Width="670px" TextMode="MultiLine"></asp:TextBox>                
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tp4" runat="server" HeaderText="失败原因说明">
                <ContentTemplate>
                    <table border="1" cellpadding="10" cellspacing="1" width="810px">
                        <tr>
                            <td align="right" style="width:130px;">井号</td>
                            <td>                                                          
                                <asp:TextBox ID="TB_sb1" runat="server" Width="670px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr> 
                            <td align="right">层号</td> 
                            <td>                                                        
                                <asp:TextBox ID="TB_sb2" runat="server" Width="670px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr> 
                            <td align="right">压裂日期</td> 
                            <td>                                                         
                                <asp:TextBox ID="TB_sb3" runat="server" Width="670px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr> 
                            <td align="right">施工描述</td> 
                            <td align="left"colspan="5">
                                <asp:TextBox ID="TB_sb4" runat="server" Height="50px" Width="670px" TextMode="MultiLine"></asp:TextBox>                
                            </td>
                        </tr>
                        <tr> 
                            <td align="right">失败原因分析</td> 
                            <td align="left"colspan="5">
                                <asp:TextBox ID="TB_sb5" runat="server" Width="670px"></asp:TextBox>                
                            </td>
                        </tr>  
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>

        <table>
            <tr>
                <td align="center" style="width:840px;">
                    <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Small" 
                        ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
