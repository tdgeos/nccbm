// JScript 文件
function CreatePie(total1,table_x,table_y,all_width,all_height,table_type)
{
 //参数含义(传递的数组，横坐标，纵坐标，图表的宽度，图表的高度,图表的类型)
 //纯ASP代码生成图表函数3——饼图
 //作者：龚鸣(Passwordgm) QQ:25968152 MSN:passwordgm@sina.com Email:passwordgm@sina.com
 //本人非常愿意和ASP,VML,FLASH的爱好者在HTTP://topclouds.126.com进行交流和探讨
 //版本1.0 最后修改日期 2003-8-11
 //非常感谢您使用这个函数，请您使用和转载时保留版权信息，这是对作者工作的最好的尊重。
//***************************************************************************************
//修改说明：
//    本代码经原作者同意，由 awaysrain（绝对零度）修改为javascript。
//    最后修改日期 2003-9-22，测试环境为IE 6.0.2500.1106
//    因本人水平有限，修改中难免有错误，请大家及时指正。  
//***************************************************************************************
 var tmdColor1 = new Array();
 
 tmdColor1[0] = "#d1ffd1";
 tmdColor1[1] = "#ffbbbb";
 tmdColor1[2] = "#ffe3bb";
 tmdColor1[3] = "#cff4f3";
 tmdColor1[4] = "#d9d9e5";
 tmdColor1[5] = "#ffc7ab";
 tmdColor1[6] = "#ecffb7";
 tmdColor1[7] = "#d1ffd3";
 tmdColor1[8] = "#ffbbbb";
 tmdColor1[9] = "#ffe4bb";
 tmdColor1[10] = "#cff4f1";
 tmdColor1[11] = "#d9d4e5";
 tmdColor1[12] = "#ffc4ab";
 tmdColor1[13] = "#ecffb8";
 
 var tmdColor2 = new Array();
 
 tmdColor2[0] = "#00ff00";
 tmdColor2[1] = "#ff0000";
 tmdColor2[2] = "#ff9900";
 tmdColor2[3] = "#33cccc";
 tmdColor2[4] = "#666699";
 tmdColor2[5] = "#993300";
 tmdColor2[6] = "#99cc00";
 tmdColor2[7] = "#998744";
 tmdColor2[8] = "#996655";
 tmdColor2[9] = "#ff9900";
 tmdColor2[10] = "#33cccc";
 tmdColor2[11] = "#666699";
 tmdColor2[12] = "#996699";
 tmdColor2[13] = "#ff00ff";
 
 var tmp0 = total1.split(",");
 var tmp1;
 var tmp2= new Array();
 var tmp3= new Array();
 
 for(var j=0; j < tmp0.length; j++)
 {
     tmp1 = tmp0[j].split("|");
     tmp2[j] = tmp1[0];
     tmp3[j] = tmp1[1]*3;
 }
 
 var total= new Array(tmp2,tmp3)
 
 var tb_color = new Array(tmdColor1,tmdColor2);
 var tb_height = 30;
 var total_no = total[0].length;
 var totalpie = 0;
 for(var i=0;i<total_no;i++)
 {
  totalpie += total[1][i];
 } 

 var PreAngle = 0;
 for(var i=0;i<total_no;i++)
 { 
  document.write("<v:shape id='_x0000_s1025' alt='' style='position:absolute;left:" + table_x + "px;top:" + table_y + "px;width:" + all_width + "px;height:" + all_height + "px;z-index:1' coordsize='1500,1400' o:spt='100' adj='0,,0' path='m750,700ae750,700,750,700," + parseInt(23592960*PreAngle) + "," + parseInt(23592960*total[1][i]/totalpie) + "xe' fillcolor='" + tb_color[0][i] + "' strokecolor='#FFFFFF'><v:fill color2='" + tb_color[1][i] + "' rotate='t' focus='100%' type='gradient'/><v:stroke joinstyle='round'/><v:formulas/><v:path o:connecttype='segments'/></v:shape>");
  PreAngle += total[1][i] / totalpie;
 }
 
 if(table_type=="A")
 {
  document.write("<v:rect id='_x0000_s1025' style='position:absolute;left:" + (table_x + all_width + 20) + "px;top:" + (table_y + 20) + "px;width:185px;height:" + (total_no * tb_height + 20) + "px;z-index:1'/>");
  for(var i=0;i<total_no;i++)
  {   
   document.write("<v:shape id='_x0000_s1025' type='#_x0000_t202' alt='' style='position:absolute;left:" + (table_x + all_width + 25) + "px;top:" + (table_y+30+(i)*tb_height) + "px;width:150px;height:" + tb_height + "px;z-index:1'>");
   document.write("<v:textbox inset='0px,0px,0px,0px'><table cellspacing='3' cellpadding='0' width='100%' height='100%'><tr><td align='left'>" + total[0][i] + "(" + Math.round(total[1][i]/totalpie*100) +"%)</td></tr></table></v:textbox></v:shape>");
   document.write("<v:rect id='_x0000_s1040' alt='' style='position:absolute;left:" + (table_x + all_width + 165) + "px;top:" + (table_y + 30 + (i)*tb_height+3) + "px;width:30px;height:20px;z-index:1' fillcolor='" + tb_color[0][i] + "'><v:fill color2='" + tb_color[1][i] + "' rotate='t' focus='100%' type='gradient'/></v:rect>");
   //显示比例数
   //document.write("<v:shape id='_x0000_s1025' type='#_x0000_t202' alt='' style='position:absolute;left:" + (table_x+all_width+110) + "px;top:" + (table_y+30+(i)*tb_height) + "px;width:60px;height:" + tb_height + "px;z-index:1'>");
   //document.write("<v:textbox inset='0px,0px,0px,0px'><table cellspacing='3' cellpadding='0' width='100%' height='100%'><tr><td align='left'>" + Math.round(parseFloat(total[1][i]*100/totalpie)*100)/100 + "%</td></tr></table></v:textbox></v:shape>");
  }
 }
 
 if(table_type == "B")
 {
  var pie = 3.14159265358979;
  var TempPie = 0;
  for(var i=0;i<total_no;i++)
  {
   var TempAngle = pie * 2 * (total[1][i] / (totalpie * 2) + TempPie);
   var x1 = table_x + all_width/2 + Math.cos(TempAngle) * all_width * 3/8;
   var y1 = table_y + all_height/2 - Math.sin(TempAngle) * all_height * 3/8;
   var x2 = table_x + all_width/2 + Math.cos(TempAngle) * all_width * 3/4;
   var y2 = table_y + all_height/2 - Math.sin(TempAngle) * all_height * 3/4;
 
   if(x2>table_x + all_width/2)
   {
    x3 = x2 + 20;
    x4 = x3;
   }
   else
   {
    x3 = x2 - 20;
    x4 = x3 - 100;
   }
   document.write("<v:oval id='_x0000_s1027' style='position:absolute;left:" + (x1 - 2) + "px;top:" + (y1 - 2) + "px;width:4px;height:4px; z-index:2' fillcolor='#111111' strokecolor='#111111'/>");
   document.write("<v:line id='_x0000_s1025' alt='' style='position:absolute;left:0;text-align:left;top:0;z-index:1' from='" + x1 + "px," + y1 + "px' to='" + x2 + "px," + y2 + "px' coordsize='21600,21600' strokecolor='#111111' strokeweight='1px'></v:line>");
   document.write("<v:line id='_x0000_s1025' alt='' style='position:absolute;left:0;text-align:left;top:0;z-index:1' from='" + x2 + "px," + y2 + "px' to='" + x3 + "px," + y2 + "px' coordsize='21600,21600' strokecolor='#111111' strokeweight='1px'></v:line>");
   document.write("<v:shape id='_x0000_s1025' type='#_x0000_t202' alt='' style='position:absolute;left:" + x4 + "px;top:" + (y2 - 10) + "px;width:100px;height:20px;z-index:1'>");
   document.write("<v:textbox inset='0px,0px,0px,0px'><table cellspacing='3' cellpadding='0' width='100%' height='100%'><tr><td align='left'>" + total[0][i] + " " + Math.round(parseFloat(total[1][i] * 100/ totalpie)*100)/100 + "%</td></tr></table></v:textbox></v:shape>")
   TempPie += total[1][i]/totalpie;  
  }
 }
}
