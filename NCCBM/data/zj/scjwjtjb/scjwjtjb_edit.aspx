<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scjwjtjb_edit.aspx.cs" Inherits="NCCBM.data.zj.scjwjtjb.scjwjtjb_edit" %>
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
    <table width="850px" border="0" class="metable" cellpadding="0" cellspacing="0" >
        <tr >                
            <td  class="tdhead" style=" height: 30px">
                <h3 style="text-align:center">钻井施工监督基础数据统计信息</h3>
            </td>
        </tr> 
    </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" width="850px" >
        <tr>
            <td style="width:100px">负责监督</td>
             <td>                                                         
                <asp:TextBox ID="TB_fuzejiandu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td style="width:100px">施工单位</td> 
             <td>                                                       
                <asp:TextBox ID="TB_shigongdanwei" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td style="width:100px">井号</td> 
             <td>                                                        
                <asp:TextBox ID="TB_jinghao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>   
        </tr>
        <tr>   
            <td>完钻井深</td> 
            <td>                                                           
                <asp:TextBox ID="TB_wanzuanjingshen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>开钻日期</td>
            <td>                                                       
                <asp:TextBox ID="TB_kaizuanriqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>
            <td>完钻日期</td> 
            <td>                                                        
                <asp:TextBox ID="TB_wanzuanriqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>      
        </tr>
        <tr>
            <td>完井时间</td> 
            <td>                                                        
                <asp:TextBox ID="TB_wanjingshijian"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td> 
            <td>钻井周期</td> 
            <td>                                                         
                <asp:TextBox ID="TB_zuanjingzhouqi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>钻机型号</td> 
            <td>                                                         
                <asp:TextBox ID="TB_zuanjixinghao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>     
        <tr>  
            <td>一开验收_日期</td>
            <td>                                                       
                <asp:TextBox ID="TB_ykys_riqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>
            <td>一开验收_存在问题</td> 
            <td>                                                        
                <asp:TextBox ID="TB_ykys_cunzaiwenti" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>一开验收_是否同意开钻</td> 
            <td>                                                        
                <asp:TextBox ID="TB_ykys_shifoutongyi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
         </tr>   
        <tr>
            <td>一开验收_复验情况</td>
            <td>                                                        
                <asp:TextBox ID="TB_ykys_fuyanqingkuang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>二开验收_日期</td>
            <td>                                                         
                <asp:TextBox ID="TB_ekys_riqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>
            <td>二开验收_存在问题</td> 
            <td>                                                          
                <asp:TextBox ID="TB_ekys_cunzaiwenti" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>       
        <tr>  
            <td>二开验收_是否同意开钻</td> 
            <td>                                                         
                <asp:TextBox ID="TB_ekys_shifoutongyi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>二开验收_复验情况</td> 
            <td>                                                         
                <asp:TextBox ID="TB_ekys_fuyanqingkuang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>表套数据_表套下深</td>
            <td>                                                         
                <asp:TextBox ID="TB_btsj_biaotaoxiashen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>生产套管数据_套管厂家及钢级</td> 
            <td>                                                          
                <asp:TextBox ID="TB_sctgsj_changjia" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>生产套管数据_套管外观检查情况</td>
            <td>                                                        
                <asp:TextBox ID="TB_sctgsj_waiguan" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>生产套管数据_套管编号核查情况</td>
            <td>                                                        
                <asp:TextBox ID="TB_sctgsj_bianhao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>  
        <tr>  
            <td>生产套管数据_套管下深</td>
            <td>                                                        
                <asp:TextBox ID="TB_sctgsj_taoguanxiashen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>生产套管数据_入井长套数量</td> 
            <td>                                                         
                <asp:TextBox ID="TB_sctgsj_rujingchangtao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>生产套管数据_剩余长套管数</td>
            <td>                                                         
                <asp:TextBox ID="TB_sctgsj_shengyuchangtao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>生产套管数据_入井短套数量</td> 
            <td>                                                          
                <asp:TextBox ID="TB_sctgsj_rujingduantao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>生产套管数据_剩余短套管数</td>
            <td>                                                        
                <asp:TextBox ID="TB_sctgsj_shengyuduantao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>生产套管数据_短套位置</td>
            <td>                                                        
                <asp:TextBox ID="TB_sctgsj_duantaoweizhi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>  
        <tr>  
            <td>取心数据_取心回次</td>
            <td>                                                        
                <asp:TextBox ID="TB_qxsj_quxinhuici" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>取心数据_总收获率</td> 
            <td>                                                         
                <asp:TextBox ID="TB_qxsj_zongshouhuolv" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>固井施工数据_固井队</td>
            <td>                                                         
                <asp:TextBox ID="TB_gjsgsj_gujingdui" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>固井施工数据_前置液（设计/实际）</td> 
            <td>                                                          
                <asp:TextBox ID="TB_gjsgsj_qianzhiye" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>固井施工数据_注水泥浆量（设计/实际）</td>
            <td>                                                        
                <asp:TextBox ID="TB_gjsgsj_zhushuinijiang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>固井施工数据_替浆量（设计/实际）</td>
            <td>                                                        
                <asp:TextBox ID="TB_gjsgsj_tijiangliang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>  
        <tr>  
            <td>固井施工数据_碰压情况（设计/实际）</td>
            <td>                                                        
                <asp:TextBox ID="TB_gjsgsj_pengya" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>固井施工数据_水泥浆密度（设计/实际）</td> 
            <td>                                                         
                <asp:TextBox ID="TB_gjsgsj_midu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>试压_日期</td>
            <td>                                                         
                <asp:TextBox ID="TB_sy_riqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>试压_压降情况</td> 
            <td>                                                          
                <asp:TextBox ID="TB_sy_yajiang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td>完井检查_井口水平</td> 
            <td>                                                          
                <asp:TextBox ID="TB_wjjc_shuiping" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td> 
            <td>完井检查_井口高度</td>
            <td>                                                        
                <asp:TextBox ID="TB_wjjc_gaodu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
        </tr>
        <tr> 
            <td>完井检查_井口焊接</td>
            <td>                                                        
                <asp:TextBox ID="TB_wjjc_hanjie" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>备注</td>
            <td>                                                        
                <asp:TextBox ID="TB_beizhu1" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>  
    </table>


    <table width="850px" border="0" class="metable" cellpadding="0" cellspacing="0" >
        <tr >                
            <td  class="tdhead" style=" height: 30px">
                <h3 style="text-align:center">完井电测数据信息</h3>
            </td>
        </tr> 
    </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" width="850px">
        <tr>  
            <td style="width:100px">编号</td> 
             <td>                                                         
                <asp:TextBox ID="TB_bianhao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>  
            <td style="width:100px">小结日期</td>
             <td>                                                       
                <asp:TextBox ID="TB_xiaojieriqi"  runat="server" CssClass="inputwidth datepicker"></asp:TextBox>
            </td>    
            <td style="width:100px">井身结构_一开</td>
             <td>                                                       
                <asp:TextBox ID="TB_jsjg_yikai" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>      
        <tr>
            <td>井身结构_表套</td>
            <td>                                                        
                <asp:TextBox ID="TB_jsjg_biaotao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>井身结构_二开</td>
            <td>                                                        
                <asp:TextBox ID="TB_jsjg_erkai" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>井身结构_产套</td>
            <td>                                                        
                <asp:TextBox ID="TB_jsjg_chantao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>煤层数据_韩城:3#/临汾:5#/忻州:4+5#</td>
            <td>                                                        
                <asp:TextBox ID="TB_mcsj_3" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>煤层数据_韩城:5#/临汾:8#/忻州:8+9#</td>
            <td>                                                        
                <asp:TextBox ID="TB_mcsj_5" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>煤层数据_韩城:11#</td>
            <td>                                                        
                <asp:TextBox ID="TB_mcsj_11" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>煤层数据_其他煤层</td>
            <td>                                                        
                <asp:TextBox ID="TB_mcsj_qita" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>井身质量数据_靶心距</td>
            <td>                                                        
                <asp:TextBox ID="TB_jszlsj_baxinju" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>井身质量数据_最大井斜</td>
            <td>                                                        
                <asp:TextBox ID="TB_jszlsj_jingxie" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>井身质量数据_最大位移</td>
            <td>                                                        
                <asp:TextBox ID="TB_jszlsj_weiyi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>井身质量数据_最大全角变化率</td>
            <td>                                                        
                <asp:TextBox ID="TB_jszlsj_quanjiao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>井身质量数据_造斜段连续三点全角变化率_最大连续数据</td>
            <td>                                                        
                <asp:TextBox ID="TB_jszlsj_zxd_lianxu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>井身质量数据_造斜段连续三点全角变化率_连续三点最大平均值</td>
            <td>                                                        
                <asp:TextBox ID="TB_jszlsj_zxd_pingjun" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>井身质量数据_稳斜段连续三点全角变化率_最大连续数据</td>
            <td>                                                        
                <asp:TextBox ID="TB_jszlsj_wxd_lianxu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>井身质量数据_稳斜段连续三点全角变化率_连续三点最大平均值</td>
            <td>                                                        
                <asp:TextBox ID="TB_jszlsj_wxd_pingjun" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>            
            <td>井身质量数据_全井段井径扩大率</td>
            <td>                                                        
                <asp:TextBox ID="TB_jszlsj_quanjingduan" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>井身质量数据_煤层段井径扩大率</td>
            <td>                                                        
                <asp:TextBox ID="TB_jszlsj_meicengduan" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>下套管、固井数据_电测套管情况_短套设计位置</td>
            <td>                                                        
                <asp:TextBox ID="TB_xtggjsj_dctgqk_shejiweizhi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>下套管、固井数据_电测套管情况_短套实测位置</td>
            <td>                                                        
                <asp:TextBox ID="TB_xtggjsj_dctgqk_shiceweizhi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>下套管、固井数据_电测套管情况_阻流环深度</td>
            <td>                                                        
                <asp:TextBox ID="TB_xtggjsj_dctgqk_zuliuhuan" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>下套管、固井数据_电测套管情况_遇阻深度</td>
            <td>                                                        
                <asp:TextBox ID="TB_xtggjsj_dctgqk_yuzushendu" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>下套管、固井数据_电测套管情况_煤层段接箍位置</td>
            <td>                                                        
                <asp:TextBox ID="TB_xtggjsj_dctgqk_jieguweizhi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>下套管、固井数据_固井情况_水泥返深</td>
            <td>                                                        
                <asp:TextBox ID="TB_xtggjsj_gjqk_shuinifanshen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>下套管、固井数据_电测评价</td>
            <td>                                                        
                <asp:TextBox ID="TB_xtggjsj_diancepingjia" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>备注</td>
            <td>                                                        
                <asp:TextBox ID="TB_beizhu2" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>
    </table>

    <table width="850px" border="0" class="metable" cellpadding="0" cellspacing="0" >
        <tr >                
            <td  class="tdhead" style=" height: 30px">
                <h3 style="text-align:center">钻井设计数据登记信息</h3>
            </td>
        </tr> 
    </table>
    <table border="1" cellpadding="3" cellspacing="1" class="metable" width="850px">
        <tr>
            <td style="width:100px">基本数据_井别</td>
             <td>                                                       
                <asp:TextBox ID="TB_jbsj_jingbie" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td style="width:100px">基本数据_井型</td>
             <td>                                                       
                <asp:TextBox ID="TB_jbsj_jingxing" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td style="width:100px">基本数据_地理位置</td>
             <td>                                                       
                <asp:TextBox ID="TB_jbsj_diliweizhi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr>     
        <tr>            
            <td>基本数据_设计井深</td>
            <td>                                                        
                <asp:TextBox ID="TB_jbsj_shejijingshen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>设计井身结构_一开</td>
            <td>                                                        
                <asp:TextBox ID="TB_sjjsjg_yikai" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>设计井身结构_表套下深</td>
            <td>                                                        
                <asp:TextBox ID="TB_sjjsjg_biaotaoxiashen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>设计井身结构_二开</td>
            <td>                                                        
                <asp:TextBox ID="TB_sjjsjg_erkai" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>设计井身结构_产套下深</td>
            <td>                                                        
                <asp:TextBox ID="TB_sjjsjg_chantaoxiashen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>定向数据_井口横坐标</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_jingkou_x" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>定向数据_井口纵坐标</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_jingkou_y" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>定向数据_井口海拔</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_jingkou_h" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>定向数据_井底垂深</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_jingdichuishen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>定向数据_靶点横坐标</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_badian_x" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>定向数据_靶点纵坐标</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_badian_y" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
            <td>定向数据_靶点方位</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_badianfangwei" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>定向数据_靶点垂深</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_badianchuishen" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>定向数据_靶点位移</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_badianweiyi" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>定向数据_磁偏角</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_cipianjiao" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
        </tr> 
        <tr>
            <td>定向数据_大门方向</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_damenfangxiang" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>定向数据_造斜段</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_zaoxieduan" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>
            <td>定向数据_设计井斜</td>
            <td>                                                        
                <asp:TextBox ID="TB_dxsj_shejijingxie" runat="server" CssClass="inputwidth"></asp:TextBox>
            </td>    
        </tr> 
    </table>

    <table border="1" cellpadding="3" cellspacing="1" class="metable" width="850px">
        <tr>  
            <td align="center" colspan="6">
                <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="btnOk" runat="server" Text="确定" onclick="btnOk_Click"/>
                &nbsp;&nbsp;
                <asp:Button CssClass="button-query" ForeColor="White" Font-Size="Medium" ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click"/>
     	    </td>
        </tr>
    </table>
    </form>
</body>
</html>
