<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chart.aspx.cs" Inherits="NCCBM.FusionCharts.chart" %>

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
                    <div id="chartdiv" align="left"></div>

                    <script type="text/javascript">
                        function DrawPic() {
                            /*
                            var sz = document.rsForm.elements["rsHid"].value;
                            if (sz == "") return;
                            var ss = sz.split(";");
                            if (ss.length != 6) return;
                            var type = ss[0];
                            var cap = ss[1];
                            var subcap = ss[2];
                            var label = ss[3];
                            var legend = ss[4];
                            var value = ss[5];

                            var colors = new Array();
                            colors[0] = "AFD8F8";
                            colors[1] = "F6BD0F";
                            colors[2] = "8BBA00";
                            colors[3] = "FF8E46";
                            colors[4] = "44FA88";
                            colors[5] = "C1C2FF";
                            colors[6] = "A0FF40";

                            var fieldX = "";
                            var fieldY = "";
                            var szXML = "<chart palette='2' caption='" + cap +
                                        "' imageSave='1' imageSaveURL='FusionChartsSave.aspx' subCaption='" + subcap +
                                        "' baseFontSize='13' xAxisName='" + fieldX + "' yAxisName='" + fieldY +
                                        "' showValues='0' decimals='0' formatNumberScale='0' useRoundEdges='1'>";

                            var szCategories = "<categories>";
                            var labels = label.split(",");

                            for (var i = 0; i < labels.length; i++) {
                                szCategories += "<category label='" + labels[0] + "' />";
                            }
                            szCategories += "</categories>";

                            var szDataSet = new Array();

                            var legends = legend.split(",");
                            var values = value.split(",");
                            for (var i = 0; i < legends.length; i++) {
                                szDataSet[i] = "<dataset seriesname='" + legends[i] + "' color='" + colors[i] + "' showvalues='0'>";
                                var valuess = values[i].split("|");
                                for (var j = 0; j < valuess.length; j++) {
                                    szDataSet[i] += "<set value='" + valuess[j] + "' />";
                                }
                                szDataSet[i] += "</dataset>";
                            }

                            szXML += szCategories;

                            for (var i = 0; i < legends.length; i++) {
                                szXML += szDataSet[i];
                            }

                            szXML += "</chart>";

                            var chartType = "";
                            if (type == "0") chartType = "FusionCharts/MSColumn2D.swf";
                            if (type == "1") chartType = "FusionCharts/MSLine.swf";
                            if (type == "2") chartType = "FusionCharts/Pie2D.swf";

                            var chart = new FusionCharts(chartType, "ChartId", "400", "280", "0", "0");
                            chart.setDataXML(szXML);
                            chart.render("chartdiv");
                            */
                            var sz = document.rsForm.elements["rsHid"].value;
                            if (sz == "") return;
                            var ss = sz.split(";");
                            if (ss.length != 2) return;
                            var labels = ss[0].split(",");
                            var values = ss[1].split(",");

                            var szXML = "<graph showNames='1' decimalPrecision='0' baseFont='Arial' baseFontSize='12'>";
                            for (var i = 0; i < labels.length; i++) {
                                szXML += "<set name='" + labels[i] + "' value='" + values[i] + "' />";
                            }
                            szXML += "</graph>";

                            var chart = new FusionCharts("FusionCharts/Pie2D.swf", "ChartId", "400", "280", "0", "0");
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
