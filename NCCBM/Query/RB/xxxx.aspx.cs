using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.Query.rb
{
    public partial class xxxx : System.Web.UI.Page
    {
        private static DataTable dt_zj = null;
        private static DataTable dt_xtg = null;
        private static DataTable dt_gj = null;
        private static DataTable dt_wj = null;
        private int nPageSize = common.GetPageSize();
        private int type = 1;
        private int nFujianCol = 25;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int userPlaceId = Int32.Parse(HttpContext.Current.Session["PlaceID"].ToString());
            }
            catch (NullReferenceException ne)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            lbtnZj.Command += new CommandEventHandler(lbtn_zj_Command);
            lbtnXtg.Command += new CommandEventHandler(lbtn_xtg_Command);
            lbtnGj.Command += new CommandEventHandler(lbtn_gj_Command);
            lbtnWj.Command += new CommandEventHandler(lbtn_wj_Command);

            if (!Page.IsPostBack)
            {
                lbtnZj.ForeColor = System.Drawing.Color.Purple;
                lbtnXtg.ForeColor = System.Drawing.Color.Blue;
                lbtnGj.ForeColor = System.Drawing.Color.Blue;
                lbtnWj.ForeColor = System.Drawing.Color.Blue;

                gv_zj.ShowFooter = false;
                gv_zj.PageSize = nPageSize;

                gv_xtg.ShowFooter = false;
                gv_xtg.PageSize = nPageSize;

                gv_gj.ShowFooter = false;
                gv_gj.PageSize = nPageSize;

                gv_wj.ShowFooter = false;
                gv_wj.PageSize = nPageSize;

                string jh = Request.QueryString["JH"];
                string[] ss = jh.Split(',');
                lblCD.Text = ss[1] + "," + ss[2] + "," + ss[3] + "," + ss[4] + "," + ss[5] + "," + ss[6];
                getData(ss[0]);
                showData();
            }
            SetDivePage();
        }

        void showData()
        {
            if (type == 1)
            {
                gv_zj.Visible = true;
                gv_xtg.Visible = false;
                gv_gj.Visible = false;
                gv_wj.Visible = false;
                if (gv_zj.Rows.Count == 0) Label1.Visible = true;
                else Label1.Visible = false;
            }
            if (type == 2)
            {
                gv_zj.Visible = false;
                gv_xtg.Visible = true;
                gv_gj.Visible = false;
                gv_wj.Visible = false;
                if (gv_xtg.Rows.Count == 0) Label1.Visible = true;
                else Label1.Visible = false;
            }
            if (type == 3)
            {
                gv_zj.Visible = false;
                gv_xtg.Visible = false;
                gv_gj.Visible = true;
                gv_wj.Visible = false;
                if (gv_gj.Rows.Count == 0) Label1.Visible = true;
                else Label1.Visible = false;
            }
            if (type == 4)
            {
                gv_zj.Visible = false;
                gv_xtg.Visible = false;
                gv_gj.Visible = false;
                gv_wj.Visible = true;
                if (gv_wj.Rows.Count == 0) Label1.Visible = true;
                else Label1.Visible = false;
            }
            SetDivePage();
        }

        void getData(string jinghao)
        {
            string sql = "select * from Xls_Zj_Rbb_Zj where jinghao='" + jinghao + "' order by riqi desc";
            dt_zj = DataBaseHelper.query(sql);
            gv_zj_DataBind();

            sql = "select * from Xls_Zj_Rbb_Xtg where jinghao='" + jinghao + "' order by riqi desc";
            dt_xtg = DataBaseHelper.query(sql);
            gv_xtg_DataBind();

            sql = "select * from Xls_Zj_Rbb_Gj where jinghao='" + jinghao + "' order by riqi desc";
            dt_gj = DataBaseHelper.query(sql);
            gv_gj_DataBind();

            sql = "select * from Xls_Zj_Rbb_Wj where jinghao='" + jinghao + "' order by riqi desc";
            dt_wj = DataBaseHelper.query(sql);
            gv_wj_DataBind();
        }

        private void SetDivePage()
        {
            GridView gv = null;
            if (type == 1) gv = gv_zj;
            if (type == 2) gv = gv_xtg;
            if (type == 3) gv = gv_gj;
            if (type == 4) gv = gv_wj;

            if (gv.Visible == false || gv.PageCount == 1)
            {
                commPage1.Visible = false;
                return;
            }
            commPage1.Visible = true;
            WEE.MyPagerBar.BindDataDelegate f = null;
            if (type == 1) f = new WEE.MyPagerBar.BindDataDelegate(gv_zj_DataBind);
            if (type == 2) f = new WEE.MyPagerBar.BindDataDelegate(gv_xtg_DataBind);
            if (type == 3) f = new WEE.MyPagerBar.BindDataDelegate(gv_gj_DataBind);
            if (type == 4) f = new WEE.MyPagerBar.BindDataDelegate(gv_wj_DataBind);
            int recCount = 0;
            if (type == 1) recCount = dt_zj.Rows.Count;
            if (type == 2) recCount = dt_xtg.Rows.Count;
            if (type == 3) recCount = dt_gj.Rows.Count;
            if (type == 4) recCount = dt_wj.Rows.Count;
            commPage1.SetTarget(gv, f, gv.PageSize, recCount);
        }

        void lbtn_zj_Command(object sender, CommandEventArgs e)
        {
            lbtnZj.ForeColor = System.Drawing.Color.Purple;
            lbtnXtg.ForeColor = System.Drawing.Color.Blue;
            lbtnGj.ForeColor = System.Drawing.Color.Blue;
            lbtnWj.ForeColor = System.Drawing.Color.Blue;
            type = 1;
            showData();
        }

        void lbtn_xtg_Command(object sender, CommandEventArgs e)
        {
            lbtnZj.ForeColor = System.Drawing.Color.Blue;
            lbtnXtg.ForeColor = System.Drawing.Color.Purple;
            lbtnGj.ForeColor = System.Drawing.Color.Blue;
            lbtnWj.ForeColor = System.Drawing.Color.Blue;
            type = 2;
            showData();
        }

        void lbtn_gj_Command(object sender, CommandEventArgs e)
        {
            lbtnZj.ForeColor = System.Drawing.Color.Blue;
            lbtnXtg.ForeColor = System.Drawing.Color.Blue;
            lbtnGj.ForeColor = System.Drawing.Color.Purple;
            lbtnWj.ForeColor = System.Drawing.Color.Blue;
            type = 3;
            showData();
        }

        void lbtn_wj_Command(object sender, CommandEventArgs e)
        {
            lbtnZj.ForeColor = System.Drawing.Color.Blue;
            lbtnXtg.ForeColor = System.Drawing.Color.Blue;
            lbtnGj.ForeColor = System.Drawing.Color.Blue;
            lbtnWj.ForeColor = System.Drawing.Color.Purple;
            type = 4;
            showData();
        }

        protected void gv_zj_RowCreated(object sender, EventArgs e1)
        {
            GridViewRowEventArgs e = e1 as GridViewRowEventArgs;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                rowHeader.CssClass = "tableLineHeader";
                Literal newCells = new Literal();
                newCells.Text = "序号</th>" +
                      "<th rowspan='2' nowrap>上报日期</th>" +
                      "<th rowspan='2' nowrap>负责监督</th>" +
                      "<th rowspan='2' nowrap>队号</th>" +
                      "<th rowspan='2' nowrap>井号</th>" +
                      "<th rowspan='2' nowrap>开钻日期</th>" +
                      "<th rowspan='2' nowrap>设计井深</th>" +
                      "<th rowspan='2' nowrap>目前井深</th>" +
                      "<th rowspan='2' nowrap>当日进尺</th>" +
                      "<th rowspan='2' nowrap>工况</th>" +
                      "<th rowspan='2' nowrap>密度</th>" +
                      "<th rowspan='2' nowrap>粘度</th>" +
                      "<th rowspan='2' nowrap>井段</th>" +
                      "<th colspan='2' nowrap>井斜</th>" +
                      "<th colspan='2' nowrap>方位</th>" +
                      "<th colspan='2' nowrap>狗腿</th>" +
                      "<th rowspan='2' nowrap>HSE情况</th>" +
                      "<th rowspan='2' nowrap>复杂情况</th>" +
                      "<th rowspan='2' nowrap>整改内容及促措施</th>" +
                      "<th rowspan='2' nowrap>备注</th>" +
                      "<th rowspan='2' nowrap>检测方式</th>" +
                      "<th rowspan='2' nowrap>区块</th>" +
                      "<th rowspan='2' nowrap>附件</th>" +
                      "</tr><tr>";
                newCells.Text += @"                         
                      <th class=tableLinedown nowrap>设计</th>
                      <th class=tableLinedown nowrap>实际</th>
                      <th class=tableLinedown nowrap>设计</th>
                      <th class=tableLinedown nowrap>实际</th>
                      <th class=tableLinedown nowrap>设计</th>
                      <th class=tableLinedown nowrap>实际</th>
                      </tr>";


                TableCellCollection cells = e.Row.Cells;
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.RowSpan = 2;
                headerCell.Controls.Add(newCells);
                rowHeader.Cells.Add(headerCell);
                rowHeader.Cells.Add(headerCell);
                rowHeader.Visible = true;
                gv_zj.Controls[0].Controls.AddAt(0, rowHeader);
            }
        }

        protected void gv_zj_Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_zj.PageIndex = e.NewPageIndex;
            gv_zj_DataBind();
        }

        private void gv_zj_DataBind()
        {
            gv_zj.DataSource = dt_zj;
            gv_zj.DataBind();
            for (int i = 1; i <= gv_zj.Rows.Count; i++)
            {
                int n = gv_zj.PageIndex * nPageSize + i - 1;
                gv_zj.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
                String qk = dt_zj.Rows[n]["place"].ToString().Trim();
                String jh = dt_zj.Rows[n]["jinghao"].ToString().Trim();
                int gv_index = gv_zj.PageIndex;
                if (MyTools.IsHaveFujian(qk, "钻进", jh))
                {
                    LinkButton lbtn = new LinkButton();
                    String str = qk + ",钻进," + jh;
                    String str2 = "q," + jh + "," + lblCD.Text;
                    lbtn.PostBackUrl = "~/Fujian.aspx?FJ=" + str + "&Page=" + str2;
                    lbtn.Text = "查看";
                    lbtn.CausesValidation = false;
                    gv_zj.Rows[i - 1].Cells[nFujianCol].Controls.Add(lbtn);
                }
            }
        }

        protected void gv_xtg_RowCreated(object sender, EventArgs e1)
        {
            GridViewRowEventArgs e = e1 as GridViewRowEventArgs;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                rowHeader.CssClass = "tableLineHeader";
                Literal newCells = new Literal();
                newCells.Text = "序号</th>" +
                      "<th rowspan='1' nowrap>上报日期</th>" +
                      "<th rowspan='1' nowrap>负责监督</th>" +
                      "<th rowspan='1' nowrap>队号</th>" +
                      "<th rowspan='1' nowrap>井号</th>" +
                      "<th rowspan='1' nowrap>完钻井深</th>" +
                      "<th rowspan='1' nowrap>完钻日期</th>" +
                      "<th rowspan='1' nowrap>套管下深</th>" +
                      "<th rowspan='1' nowrap>钢级</th>" +
                      "<th rowspan='1' nowrap>尺寸</th>" +
                      "<th rowspan='1' nowrap>丝扣</th>" +
                      "<th rowspan='1' nowrap>外观</th>" +
                      "<th rowspan='1' nowrap>平均壁厚</th>" +
                      "<th rowspan='1' nowrap>平均外径</th>" +
                      "<th rowspan='1' nowrap>密封脂</th>" +
                      "<th rowspan='1' nowrap>紧扣</th>" +
                      "<th rowspan='1' nowrap>浮箍、浮鞋检查</th>" +
                      "<th rowspan='1' nowrap>套管数据表是否按要求填写</th>" +
                      "<th rowspan='1' nowrap>套管批次、编号是否与物资部出库单一致</th>" +
                      "<th rowspan='1' nowrap>存在问题</th>" +
                      "<th rowspan='1' nowrap>整改措施</th>" +
                      "<th rowspan='1' nowrap>备注</th>" +
                      "<th rowspan='1' nowrap>检测方式</th>" +
                      "<th rowspan='1' nowrap>套管厂家</th>" +
                      "<th rowspan='1' nowrap>区块</th>" +
                      "<th rowspan='1' nowrap>附件</th>";

                TableCellCollection cells = e.Row.Cells;
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.RowSpan = 1;
                headerCell.Controls.Add(newCells);
                rowHeader.Cells.Add(headerCell);
                rowHeader.Cells.Add(headerCell);
                rowHeader.Visible = true;
                gv_xtg.Controls[0].Controls.AddAt(0, rowHeader);
            }
        }

        protected void gv_xtg_Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_xtg.PageIndex = e.NewPageIndex;
            gv_xtg_DataBind();
        }

        private void gv_xtg_DataBind()
        {
            gv_xtg.DataSource = dt_xtg;
            gv_xtg.DataBind();
            for (int i = 1; i <= gv_xtg.Rows.Count; i++)
            {
                int n = gv_xtg.PageIndex * nPageSize + i - 1;
                gv_xtg.Rows[i - 1].Cells[0].Text = (n + 1).ToString();

                String qk = dt_xtg.Rows[n]["place"].ToString().Trim();
                String jh = dt_xtg.Rows[n]["jinghao"].ToString().Trim();
                int gv_index = gv_xtg.PageIndex;
                if (MyTools.IsHaveFujian(qk, "下套管", jh))
                {
                    LinkButton lbtn = new LinkButton();
                    String str = qk + ",下套管," + jh;
                    String str2 = "q," + jh + "," + lblCD.Text;
                    lbtn.PostBackUrl = "~/Fujian.aspx?FJ=" + str + "&Page=" + str2;
                    lbtn.Text = "查看";
                    lbtn.CausesValidation = false;
                    gv_xtg.Rows[i - 1].Cells[nFujianCol].Controls.Add(lbtn);
                }
            }
        }

        protected void gv_gj_RowCreated(object sender, EventArgs e1)
        {
            GridViewRowEventArgs e = e1 as GridViewRowEventArgs;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                rowHeader.CssClass = "tableLineHeader";
                Literal newCells = new Literal();
                newCells.Text = "序号</th>" +
                     "<th rowspan='2' nowrap>上报日期</th>" +
                     "<th rowspan='2' nowrap>负责监督</th>" +
                     "<th rowspan='2' nowrap>队号</th>" +
                     "<th rowspan='2' nowrap>井号</th>" +
                     "<th rowspan='2' nowrap>完钻井深</th>" +
                     "<th rowspan='2' nowrap>施工日期</th>" +
                     "<th colspan='2' nowrap>水泥返高</th>" +
                     "<th colspan='2' nowrap>水泥浆密度</th>" +
                     "<th colspan='2' nowrap>水泥浆用量</th>" +
                     "<th colspan='2' nowrap>水泥用量</th>" +
                     "<th colspan='2' nowrap>顶替量</th>" +
                     "<th colspan='2' nowrap>碰压</th>" +
                     "<th rowspan='2' nowrap>施工中存在问题</th>" +
                     "<th rowspan='2' nowrap>复杂情况</th>" +
                     "<th rowspan='2' nowrap>备注</th>" +
                     "<th rowspan='2' nowrap>检测方式</th>" +
                     "<th rowspan='2' nowrap>区块</th>" +
                     "<th rowspan='2' nowrap>附件</th>" +
                     "</tr><tr>";
                newCells.Text += @"                         
                      <th class=tableLinedown nowrap>设计</th>
                      <th class=tableLinedown nowrap>实际</th>
                      <th class=tableLinedown nowrap>设计</th>
                      <th class=tableLinedown nowrap>实际</th>
                      <th class=tableLinedown nowrap>设计</th>
                      <th class=tableLinedown nowrap>实际</th>
                      <th class=tableLinedown nowrap>设计</th>
                      <th class=tableLinedown nowrap>实际</th>
                      <th class=tableLinedown nowrap>设计</th>
                      <th class=tableLinedown nowrap>实际</th>
                      <th class=tableLinedown nowrap>设计</th>
                      <th class=tableLinedown nowrap>实际</th>
                      </tr>";

                TableCellCollection cells = e.Row.Cells;
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.RowSpan = 2;
                headerCell.Controls.Add(newCells);
                rowHeader.Cells.Add(headerCell);
                rowHeader.Cells.Add(headerCell);
                rowHeader.Visible = true;
                gv_gj.Controls[0].Controls.AddAt(0, rowHeader);
            }
        }

        protected void gv_gj_Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_gj.PageIndex = e.NewPageIndex;
            gv_gj_DataBind();
        }

        private void gv_gj_DataBind()
        {
            gv_gj.DataSource = dt_gj;
            gv_gj.DataBind();
            for (int i = 1; i <= gv_gj.Rows.Count; i++)
            {
                int n = gv_gj.PageIndex * nPageSize + i - 1;
                gv_gj.Rows[i - 1].Cells[0].Text = (n + 1).ToString();

                String qk = dt_gj.Rows[n]["place"].ToString().Trim();
                String jh = dt_gj.Rows[n]["jinghao"].ToString().Trim();
                int gv_index = gv_gj.PageIndex;
                if (MyTools.IsHaveFujian(qk, "固井", jh))
                {
                    LinkButton lbtn = new LinkButton();
                    String str = qk + ",固井," + jh;
                    String str2 = "q," + jh + "," + lblCD.Text;
                    lbtn.PostBackUrl = "~/Fujian.aspx?FJ=" + str + "&Page=" + str2;
                    lbtn.Text = "查看";
                    lbtn.CausesValidation = false;
                    gv_gj.Rows[i - 1].Cells[nFujianCol-1].Controls.Add(lbtn);
                }
            }
        }

        protected void gv_wj_RowCreated(object sender, EventArgs e1)
        {
            GridViewRowEventArgs e = e1 as GridViewRowEventArgs;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                rowHeader.CssClass = "tableLineHeader";
                Literal newCells = new Literal();
                newCells.Text = "序号</th>" +
                      "<th rowspan='2' nowrap>上报日期</th>" +
                      "<th rowspan='2' nowrap>负责监督</th>" +
                      "<th rowspan='2' nowrap>队号</th>" +
                      "<th rowspan='2' nowrap>井号</th>" +
                      "<th colspan='2' nowrap>标志管位置</th>" +
                      "<th colspan='2' nowrap>人工井底</th>" +
                      "<th colspan='2' nowrap>固井质量</th>" +
                      "<th colspan='3' nowrap>产套偏差</th>" +
                      "<th colspan='2' nowrap>试压</th>" +
                      "<th colspan='2' nowrap>环形钢板焊接</th>" +
                      "<th rowspan='2' nowrap>井口高度</th>" +
                      "<th rowspan='2' nowrap>丝扣是否完好</th>" +
                      "<th rowspan='2' nowrap>存在问题</th>" +
                      "<th rowspan='2' nowrap>复杂情况</th>" +
                      "<th rowspan='2' nowrap>备注</th>" +
                      "<th rowspan='2' nowrap>检测方式</th>" +
                      "<th rowspan='2' nowrap>区块</th>" +
                      "<th rowspan='2' nowrap>附件</th>" +
                      "</tr><tr>";
                newCells.Text += @"                         
                      <th class=tableLinedown nowrap>设计</th>
                      <th class=tableLinedown nowrap>实际</th>
                      <th class=tableLinedown nowrap>设计</th>
                      <th class=tableLinedown nowrap>实际</th>
                      <th class=tableLinedown nowrap>VBL</th>
                      <th class=tableLinedown nowrap>VDL</th>
                      <th class=tableLinedown nowrap>标准</th>
                      <th class=tableLinedown nowrap>水平</th>
                      <th class=tableLinedown nowrap>垂直</th>
                      <th class=tableLinedown nowrap>开始</th>
                      <th class=tableLinedown nowrap>结束</th>
                      <th class=tableLinedown nowrap>上缘</th>
                      <th class=tableLinedown nowrap>下缘</th>
                      </tr>";

                TableCellCollection cells = e.Row.Cells;
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.RowSpan = 2;
                headerCell.Controls.Add(newCells);
                rowHeader.Cells.Add(headerCell);
                rowHeader.Cells.Add(headerCell);
                rowHeader.Visible = true;
                gv_wj.Controls[0].Controls.AddAt(0, rowHeader);
            }
        }

        protected void gv_wj_Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_wj.PageIndex = e.NewPageIndex;
            gv_wj_DataBind();
        }

        private void gv_wj_DataBind()
        {
            gv_wj.DataSource = dt_wj;
            gv_wj.DataBind();
            for (int i = 1; i <= gv_wj.Rows.Count; i++)
            {
                int n = gv_wj.PageIndex * nPageSize + i - 1;
                gv_wj.Rows[i - 1].Cells[0].Text = (n + 1).ToString();

                String qk = dt_wj.Rows[n]["place"].ToString().Trim();
                String jh = dt_wj.Rows[n]["jinghao"].ToString().Trim();
                int gv_index = gv_wj.PageIndex;
                if (MyTools.IsHaveFujian(qk, "完井", jh))
                {
                    LinkButton lbtn = new LinkButton();
                    String str = qk + ",完井," + jh;
                    String str2 = "q," + jh + "," + lblCD.Text;
                    lbtn.PostBackUrl = "~/Fujian.aspx?FJ=" + str + "&Page=" + str2;
                    lbtn.Text = "查看";
                    lbtn.CausesValidation = false;
                    gv_wj.Rows[i - 1].Cells[nFujianCol].Controls.Add(lbtn);
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("rbcx.aspx?CD=" + lblCD.Text);
        }
    }
}