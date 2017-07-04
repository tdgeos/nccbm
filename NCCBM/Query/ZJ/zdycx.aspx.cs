using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace NCCBM.Query.zj
{
    public partial class zdycx : System.Web.UI.Page
    {
        private int nPageSize = common.GetPageSize();
        private int nCols = 94;
        private int nFujianIndex = 92;

        protected void Page_Load(object sender, EventArgs e)
        {
            int userPlaceId = 0;
            try
            {
                userPlaceId = Int32.Parse(HttpContext.Current.Session["PlaceID"].ToString());
            }
            catch (NullReferenceException ne)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            if (!IsPostBack)
            {
                if (userPlaceId == 0)
                {
                    ddlQukuai.Items.Add("全部");
                    ddlQukuai.Items.Add("韩城");
                    ddlQukuai.Items.Add("临汾");
                    ddlQukuai.Items.Add("忻州");
                }
                if (userPlaceId == 1)
                {
                    ddlQukuai.Items.Add("韩城");
                }
                if (userPlaceId == 2)
                {
                    ddlQukuai.Items.Add("临汾");
                }
                if (userPlaceId == 3)
                {
                    ddlQukuai.Items.Add("忻州");
                }

                GridView2.ShowFooter = false;
                GridView2.PageSize = nPageSize;

                GridView2_DataBind();
            }
            SetDivePage();
        }

        private void SetDivePage()
        {
            if (GridView2.Visible == false || GridView2.PageCount == 1)
            {
                commPage1.Visible = false;
                return;
            }
            commPage1.Visible = true;
            WEE.MyPagerBar.BindDataDelegate f = new WEE.MyPagerBar.BindDataDelegate(GridView2_DataBind);
            int recCount = 0;
            recCount = getData().Rows.Count;
            commPage1.SetTarget(GridView2, f, GridView2.PageSize, recCount);
        }

        private void GridView2_DataBind()
        {
            DataTable dt = getData();
            int count = dt.Rows.Count;
            if (count == 0)
            {
                GridView2.Visible = false;
                result_number.ForeColor = System.Drawing.Color.Black;
                result_number.Text = "没有查询到符合条件的记录。";
            }
            else
            {
                GridView2.Visible = true;
                result_number.ForeColor = System.Drawing.Color.Black;
                result_number.Text = "查询到" + dt.Rows.Count.ToString() + "条记录。";
                GridView2.DataSource = dt;
                GridView2.DataBind();
                for (int i = 1; i <= GridView2.Rows.Count; i++)
                {
                    int n = GridView2.PageIndex * nPageSize + i - 1;
                    GridView2.Rows[i - 1].Cells[0].Text = (n + 1).ToString();

                    String jh = dt.Rows[n]["jinghao"].ToString().Trim();
                    String qk = dt.Rows[n]["place"].ToString().Trim();
                    int gv_qk = ddlQukuai.SelectedIndex;
                    int gv_index = GridView2.PageIndex;
                    String info = "";
                    if (MyTools.IsHaveFujian(qk, "完井统计表", jh))
                    {
                        LinkButton lbtn = new LinkButton();
                        String str = qk + ",完井统计表," + jh;
                        String str2 = "q," + 0 + "," + gv_qk + "," + gv_index + "," + info;
                        lbtn.PostBackUrl = "~/Fujian.aspx?FJ=" + str + "&Page=" + str2;
                        lbtn.Text = "查看";
                        lbtn.CausesValidation = false;
                        GridView2.Rows[i - 1].Cells[nFujianIndex].Controls.Add(lbtn);
                    }
                }
            }
        }

        protected void Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
        }

        protected void gridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                Literal newCells = new Literal();
                newCells.Text += "序号</th>" +
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
                      "<th class=tableLinedown rowspan='2' nowrap>韩城:3#/临汾:5#/忻州:4+5#</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>韩城:5#/临汾:8#/忻州:8+9#</th>" +
                      "<th class=tableLinedown rowspan='2' nowrap>韩城:11#</th>" +
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
                GridView2.Controls[0].Controls.AddAt(0, rowHeader);
            }
        }

        public void Query_Click(object sender, EventArgs e)
        {
            result_number.Text = "";
            string yikaiBegin = tbYikaiBegin.Text;
            string yikaiEnd = tbYikaiEnd.Text;
            string erkaiBegin = tbErkaiBegin.Text;
            string erkaiEnd = tbErkaiEnd.Text;
            if (yikaiBegin == "" && yikaiEnd != "")
            {
                result_number.ForeColor = System.Drawing.Color.Red;
                result_number.Text = "请先输入一个起始一开验收日期。";
                return;
            }
            if (yikaiBegin != "" && yikaiEnd == "")
            {
                result_number.ForeColor = System.Drawing.Color.Red;
                result_number.Text = "请先输入一个截止一开验收日期。";
                return;
            }
            if (erkaiBegin == "" && erkaiEnd != "")
            {
                result_number.ForeColor = System.Drawing.Color.Red;
                result_number.Text = "请先输入一个起始二开验收日期。";
                return;
            }
            if (erkaiBegin != "" && erkaiEnd == "")
            {
                result_number.ForeColor = System.Drawing.Color.Red;
                result_number.Text = "请先输入一个截止二开验收日期。";
                return;
            }
            GridView2_DataBind();
        }

        private DataTable getData()
        {
            string qk = ddlQukuai.SelectedItem.Text;
            string yikaiBegin = tbYikaiBegin.Text;
            string yikaiEnd = tbYikaiEnd.Text;
            string erkaiBegin = tbErkaiBegin.Text;
            string erkaiEnd = tbErkaiEnd.Text;
            int yikai = ddlYikai.SelectedIndex;
            int erkai = ddlErkai.SelectedIndex;
            int jszl = ddlJszl.SelectedIndex;
            int xtg = ddlXtg.SelectedIndex;
            int gj = ddlGj.SelectedIndex;
            int jl = ddlJl.SelectedIndex;
            int ys = ddlYs.SelectedIndex;

            string sql = "select * from Xls_Zj_Rbb_Tjb where 1=1 ";
            if (qk != "全部") sql += " and place='" + qk + "'";

            if (yikaiBegin != "" && yikaiEnd != "")
            {
                sql += " and ykys_riqi>='" + yikaiBegin + "' and ykys_riqi<='" + yikaiEnd + "'";
            }

            if (yikai == 1)
            {
                sql += " and ykys_shifoutongyikaizuan='是'";
            }
            if (yikai == 2)
            {
                sql += " and ykys_shifoutongyikaizuan<>'是'";
            }

            if (erkaiBegin != "" && erkaiEnd != "")
            {
                sql += " and ekys_riqi>='" + erkaiBegin + "' and ekys_riqi<='" + erkaiEnd + "'";
            }

            if (erkai == 1)
            {
                sql += " and ekys_shifoutongyikaizuan='是'";
            }
            if (erkai == 2)
            {
                sql += " and ekys_shifoutongyikaizuan<>'是'";
            }

            if (jszl == 1)
            {
                sql += " and jinghao in (select jinghao from Report_jingshenzhiliang)";
            }
            if (jszl == 2)
            {
                sql += " and jinghao not in (select jinghao from Report_jingshenzhiliang)";
            }

            if (xtg == 1)
            {
                sql += " and jinghao in (select jinghao from Report_xtgzuoye)";
            }
            if (xtg == 2)
            {
                sql += " and jinghao not in (select jinghao from Report_xtgzuoye)";
            }

            if (gj == 1)
            {
                sql += " and jinghao in (select jinghao from Report_gjzuoye)";
            }
            if (gj == 2)
            {
                sql += " and jinghao not in (select jinghao from Report_gjzuoye)";
            }

            if (jl == 1)
            {
                sql += " and jinghao in (select jinghao from Report_jinglou)";
            }
            if (jl == 2)
            {
                sql += " and jinghao not in (select jinghao from Report_jinglou)";
            }

            if (ys == 1)
            {
                sql += " and jinghao in (select jinghao from Report_yongshui)";
            }
            if (ys == 2)
            {
                sql += " and jinghao not in (select jinghao from Report_yongshui)";
            }
            lblWhere.Text = sql;

            return DataBaseHelper.query(sql);
        }

        protected void Extport_Click(object sender, EventArgs e)
        {
            DataTable dt = getData();
            if (dt == null) return;
            int count = dt.Rows.Count;
            if (count == 0)
            {
                result_number.ForeColor = System.Drawing.Color.Red;
                result_number.Text = "没有记录。";
            }
            else
            {
                Response.Clear();
                Response.Redirect("export.aspx?sql=" + lblWhere.Text);
            }
        }
    }
}