<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chartJszl.aspx.cs" Inherits="NCCBM.FusionCharts.chartJszl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="JS/FusionCharts.js"></script>
</head>
<body>
    <form name="rsForm" method="post" action="" id="rsForm">
        <input name="rsHid" type="hidden" id="rsHid" runat="server" />
        <table width="98%" border="0" cellspacing="0" cellpadding="0" align="left">
            <tr> 
                <td valign="top" class="text" align="left">
                    <div id="chartdiv" align="left"> 
                    </div>

                    <script type="text/javascript">
                        function DrawPic() {
                            var sz = document.rsForm.elements["rsHid"].value;
                            var type = "";
                            var caption = "";
                            var fieldX, fieldY, szXML;
                            var k, num, num2, num3;

                            if (sz != "") {
                                var nodes = sz.split(","), tmp, tmp1, tmp2, tmp3;
                                var cap, subcap;
                                if (nodes[0] == "qb") {
                                    tmp = nodes[1].split(",");
                                    cap = "井身质量" + caption;
                                    subcap = nodes[1];
                                    fieldX = "";
                                } else if (nodes[0] == "hc") {
                                    cap = "韩城井身质量" + caption;
                                    subcap = nodes[1];
                                    fieldX = "";
                                } else if (nodes[0] == "lf") {
                                    cap = "临汾井身质量" + caption;
                                    subcap = nodes[1];
                                    fieldX = "";
                                } else if (nodes[0] == "xz") {
                                    cap = "忻州井身质量" + caption;
                                    subcap = nodes[1];
                                    fieldX = "";
                                }

                                var szCategories;
                                szCategories = "<categories>";
                                var szDataSet = new Array();
                                var yType = nodes[2];
                                if (yType == "0") {
                                    type = "FusionCharts/MSColumn2D.swf";
                                }
                                if (yType == "1") {
                                    type = "FusionCharts/MSLine.swf";
                                }

                                fieldY = "";
                                szXML = "<chart palette='2' caption='" + cap + "' imageSave='1' imageSaveURL='FusionChartsSave.aspx' subCaption='" + subcap + "' baseFontSize='13' xAxisName='" + fieldX + "' yAxisName='" + fieldY + "' showValues='0' decimals='0' formatNumberScale='0' useRoundEdges='1'>";

                                var colors = new Array();
                                colors[0] = "AFD8F8";
                                colors[1] = "F6BD0F";
                                colors[2] = "8BBA00";
                                colors[3] = "FF8E46";
                                colors[4] = "44FA88";
                                colors[5] = "C1C2FF";
                                colors[6] = "A0FF40";

                                tmp1 = nodes[3].split(";");
                                num = tmp1.length;
                                for (var i = 0; i < num; i++) {
                                    szDataSet[i] = "<dataset seriesname='" + tmp1[i] + "' color='" + colors[i] + "' showvalues='0'>";
                                }

                                tmp1 = nodes[4].split("$");
                                num = tmp1.length;
                                for (var i = 0; i < num; i++) {
                                    tmp = tmp1[i].split(";");
                                    szCategories = szCategories + "<category label='" + tmp[0] + "' />";
                                    num2 = tmp.length;
                                    for (var j = 1; j < num2; j++) {
                                        num3 = num2 - 1;
                                        szDataSet[(j - 1)] = szDataSet[(j - 1)] + "<set value='" + tmp[j] + "' />";
                                    }
                                }

                                for (k = 0; k < num3; k++) {
                                    szDataSet[k] = szDataSet[k] + "</dataset>";
                                }

                                szCategories = szCategories + "</categories>";
                            }

                            szXML += szCategories;

                            for (k = 0; k < num3; k++) {
                                szXML += szDataSet[k];
                            }
                            szXML += "</chart>";
                            var chart = new FusionCharts(type, "ChartId", "400", "280", "0", "0");
                            chart.setDataXML(szXML);
                            chart.render("chartdiv");
                        }

                        DrawPic();
		            </script> 
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
