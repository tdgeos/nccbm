using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.Query.zj
{
    public partial class export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = Request.QueryString["sql"];
            DataTable dt = DataBaseHelper.query(sql);
            gridView.DataSource = dt;
            gridView.DataBind();
            for (int i = 1; i <= gridView.Rows.Count; i++)
            {
                gridView.Rows[i - 1].Cells[0].Text = i.ToString();
            }
            Response.Clear();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode("新建电子表格.xls"));
        }

        protected void gridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                Literal newCells = new Literal();
                newCells.Text += "序号</th>" +
                      "<th rowspan='3' nowrap>上报日期</th>" +
                      "<th rowspan='3' nowrap>负责监督</th>" +
                      "<th rowspan='3' nowrap>施工单位</th>" +
                      "<th rowspan='3' nowrap>井号</th>" +
                      "<th rowspan='3' nowrap>完钻井深</th>" +
                      "<th rowspan='3' nowrap>开钻日期</th>" +
                      "<th rowspan='3' nowrap>完钻日期</th>" +
                      "<th rowspan='3' nowrap>完井日期</th>" +
                      "<th rowspan='3' nowrap>钻井周期</th>" +
                      "<th rowspan='3' nowrap>钻机型号</th>" +

                      "<th colspan='4' nowrap>一开验收</th>" +
                      "<th colspan='4' nowrap>二开验收</th>" +
                      "<th colspan='1' nowrap>表套数据</th>" +
                      "<th colspan='9' nowrap>生产套管数据</th>" +
                      "<th colspan='2' nowrap>取心数据</th>" +
                      "<th colspan='6' nowrap>固井施工数据</th>" +
                      "<th colspan='2' nowrap>试压</th>" +
                      "<th colspan='3' nowrap>完井检查</th>" +

                      "<th rowspan='3' nowrap>备注</th>" +
                      "<th rowspan='3' nowrap>编号</th>" +
                      "<th rowspan='3' nowrap>小结日期</th>" +

                      "<th colspan='4' nowrap>井身结构</th>" +
                      "<th colspan='4' nowrap>煤层数据</th>" +
                      "<th colspan='10' nowrap>井身质量数据</th>" +
                      "<th colspan='7' nowrap>下套管、固井数据</th>" +

                      "<th rowspan='3' nowrap>备注</th>" +

                      "<th colspan='4' nowrap>基本数据</th>" +
                      "<th colspan='4' nowrap>设计井深结构</th>" +
                      "<th colspan='13' nowrap>定向数据</th>" +

                      "<th rowspan='3' nowrap>区块</th>" +
                      "<th rowspan='3' nowrap>附件</th>" +
                      "</tr><tr>";
                newCells.Text +=
                      "<th class=tableLinedown rowspan='2' nowrap>日期</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>存在问题</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>是否同意开钻</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>复验情况</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>日期</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>存在问题</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>是否同意开钻</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>复验情况</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>表套下深</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>套管厂家及钢级</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>套管外观检查情况</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>套管编号核查情况</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>套管下深</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>入井长套数量</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>剩余长套管数</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>入井短套数量</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>剩余短套管数</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>短套位置</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>取心回次</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>总收获率</th>" +

                      "<th class=tableLinedown rowspan='2' nowrap>固井队</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>前置液(设计/实际)</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>注水泥浆量(设计/实际)</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>替浆量(设计/实际)</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>碰压情况(设计/实际)</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>水泥浆密度(设计/实际)</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>日期</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>降压情况</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>井口水平</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>井口高度</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>井口焊接</th>" +

                      "<th class=tableLinedown rowspan='2' nowrap>一开</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>表套</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>二开</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>产套</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>3#(煤顶-煤底)</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>5#(煤顶-煤底)</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>11#(煤顶-煤底)</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>其他煤层</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>靶心距</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>最大井斜</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>最大位移</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>最大全角变化率</th>" +

                      "<th class=tableLinedown colspan='2' nowrap>造斜段连续三点全角变化率</th>" +
                      "<th class=tableLinedown colspan='2' nowrap>稳斜段连续三点全角变化率</th>" +

                      "<th class=tableLinedown rowspan='2' nowrap>全井段井径扩大率</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>煤层段井径扩大率</th>" +

                      "<th class=tableLinedown colspan='5' nowrap>电测套管情况</th>" +
                      "<th class=tableLinedown colspan='1' nowrap>固井情况</th>" +

                      "<th class=tableLinedown rowspan='2' nowrap>电测评价</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>井别</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>井型</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>地理位置</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>设计井深</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>一开</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>表套下深</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>二开</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>产套下深</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>井口横坐标</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>井口纵坐标</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>井口海拔</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>井底垂深</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>靶点横坐标</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>靶点纵坐标</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>靶方位</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>靶点垂深</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>靶点位移</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>磁偏角</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>大门方向</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>造斜段</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>设计井斜</th>" +
                      "</tr><tr>";
                newCells.Text +=
                      "<th class=tableLinedown nowrap>最大连接数据</th>" +
                      "<th class=tableLinedown nowrap>连续三点最大平均值</th>" +
                      "<th class=tableLinedown nowrap>最大连接数据</th>" +
                      "<th class=tableLinedown nowrap>连续三点最大平均值</th>" +

                      "<th class=tableLinedown nowrap>短套设计位置</th>" +
                      "<th class=tableLinedown nowrap>短套实测位置</th>" +
                      "<th class=tableLinedown nowrap>阻流环深度</th>" +
                      "<th class=tableLinedown nowrap>遇阻深度</th>" +
                      "<th class=tableLinedown nowrap>煤层段接箍位置</th>" +
                      "<th class=tableLinedown nowrap>水泥返深</th>" +
                      "</tr>";

                TableCellCollection cells = e.Row.Cells;
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.RowSpan = 3;
                headerCell.Wrap = false;
                headerCell.Controls.Add(newCells);
                rowHeader.Cells.Add(headerCell);
                rowHeader.Visible = true;
                gridView.Controls[0].Controls.AddAt(0, rowHeader);
            }
        }
    }
}