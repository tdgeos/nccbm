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
    public partial class wjcx : System.Web.UI.Page
    {
        private int nPageSize = common.GetPageSize();
        private int nCols = 94;
        private int nFujianIndex = 92;
        private int nCD = 1;

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

            string cd = Request.QueryString["CD"];
            rsHid.Value = cd;
            nCD = Int32.Parse(cd);

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

                resetJianduren();
                resetDuiwu();

                GridView2.ShowFooter = false;
                GridView2.PageSize = nPageSize;

                string sql = "";
                if(ddlQukuai.SelectedItem.Text == "全部") sql = "select * from Xls_Zj_Rbb_Tjb where 1=1 ";
                else sql = "select * from Xls_Zj_Rbb_Tjb where 1=1 and place='" + ddlQukuai.SelectedItem.Text + "'";
                lblWhere.Text = sql;
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
            string sql = lblWhere.Text;
            DataTable dt = DataBaseHelper.query(sql);
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
                        String str2 = "q," + nCD + "," + gv_qk + "," + gv_index + "," + info;
                        lbtn.PostBackUrl = "~/Fujian.aspx?FJ=" + str + "&Page=" + str2;
                        lbtn.Text = "查看";
                        lbtn.CausesValidation = false;
                        GridView2.Rows[i - 1].Cells[nFujianIndex].Controls.Add(lbtn);
                    }
                }
            }
            //SetDivePage();
        }

        private DataTable getData()
        {
            string sql = lblWhere.Text;
            return DataBaseHelper.query(sql);
        }

        public void Query_Click(object sender, EventArgs e)
        {
            result_number.Text = "";

            string qk = ddlQukuai.SelectedItem.Text;
            string rqWjBegin = tbWjrqBegin.Text;
            string rqWjEnd = tbWjrqEnd.Text;
            string jh = tbJinghao.Text;
            int js1 = ddlJs1.SelectedIndex;
            int js2 = ddlJs2.SelectedIndex;
            string rqKzBegin = tbKzrqBegin.Text;
            string rqKzEnd = tbKzrqEnd.Text;
            string rqWzBegin = tbWzrqBegin.Text;
            string rqWzEnd = tbWzrqEnd.Text;
            int jjzq = ddlJjzq.SelectedIndex;
            int zdjx = ddlZdjx.SelectedIndex;
            string jdr = ddlJianduren.SelectedItem.Text;
            string dw = ddlDuiwu.SelectedItem.Text;

            string sql = "select * from Xls_Zj_Rbb_Tjb where 1=1 ";

            if (qk != "全部") sql += " and place='" + qk + "'";

            if (rqWjBegin == "" && rqWjEnd != "")
            {
                result_number.ForeColor = System.Drawing.Color.Red;
                result_number.Text = "请先输入一个起始完井日期。";
                return;
            }
            if (rqWjBegin != "" && rqWjEnd == "")
            {
                result_number.ForeColor = System.Drawing.Color.Red;
                result_number.Text = "请先输入一个截止完井日期。";
                return;
            }
            if (rqWjBegin != "" && rqWjEnd != "") sql += " and wanjingshijian<='" + rqWjEnd + "' and wanjingshijian>='" + rqWjBegin + "'";

            if (nCD == 2)
            {
                if (jh != "") sql += " and jinghao like '%" + jh + "%'";
            }

            if (nCD == 3)
            {
                if (js1 == 1) sql += " and jbsj_shejijingshen<500";
                if (js1 == 2) sql += " and jbsj_shejijingshen>=500 and jbsj_shejijingshen<600";
                if (js1 == 3) sql += " and jbsj_shejijingshen>=600 and jbsj_shejijingshen<700";
                if (js1 == 4) sql += " and jbsj_shejijingshen>=700 and jbsj_shejijingshen<800";
                if (js1 == 5) sql += " and jbsj_shejijingshen>=800 and jbsj_shejijingshen<900";
                if (js1 == 6) sql += " and jbsj_shejijingshen>=900 and jbsj_shejijingshen<1000";
                if (js1 == 7) sql += " and jbsj_shejijingshen>='1000'";
            }

            if (nCD == 4)
            {
                if (js2 == 1) sql += " and wanzuanjingshen<500";
                if (js2 == 2) sql += " and wanzuanjingshen>=500 and wanzuanjingshen<600";
                if (js2 == 3) sql += " and wanzuanjingshen>=600 and wanzuanjingshen<700";
                if (js2 == 4) sql += " and wanzuanjingshen>=700 and wanzuanjingshen<800";
                if (js2 == 5) sql += " and wanzuanjingshen>=800 and wanzuanjingshen<900";
                if (js2 == 6) sql += " and wanzuanjingshen>=900 and wanzuanjingshen<1000";
                if (js2 == 7) sql += " and wanzuanjingshen>=1000";
            }

            if (nCD == 5)
            {
                if (rqKzBegin == "" && rqKzEnd != "")
                {
                    result_number.ForeColor = System.Drawing.Color.Red;
                    result_number.Text = "请先输入一个起始日期。";
                    return;
                }
                if (rqKzBegin != "" && rqKzEnd == "")
                {
                    result_number.ForeColor = System.Drawing.Color.Red;
                    result_number.Text = "请先输入一个截止日期。";
                    return;
                }
                if (rqKzBegin != "" && rqKzEnd != "") sql += " and kaizuanriqi<='" + rqKzEnd + "' and kaizuanriqi>='" + rqKzBegin + "'";
            }

            if (nCD == 6)
            {
                if (rqWzBegin == "" && rqWzEnd != "")
                {
                    result_number.ForeColor = System.Drawing.Color.Red;
                    result_number.Text = "请先输入一个起始日期。";
                    return;
                }
                if (rqWzBegin != "" && rqWzEnd == "")
                {
                    result_number.ForeColor = System.Drawing.Color.Red;
                    result_number.Text = "请先输入一个截止日期。";
                    return;
                }
                if (rqWzBegin != "" && rqWzEnd != "") sql += " and wanzuanriqi<='" + rqWzEnd + "' and wanzuanriqi>='" + rqWzBegin + "'";
            }

            if (nCD == 7)
            {
                if (jjzq == 1) sql += " and zuanjingzhouqi<15";
                if (jjzq == 2) sql += " and zuanjingzhouqi>=15 and zuanjingzhouqi<=25";
                if (jjzq == 3) sql += " and zuanjingzhouqi>=26 and zuanjingzhouqi<=40";
                if (jjzq == 4) sql += " and zuanjingzhouqi>=41 and zuanjingzhouqi<=60";
                if (jjzq == 5) sql += " and zuanjingzhouqi>=61 and zuanjingzhouqi<=90";
                if (jjzq == 6) sql += " and zuanjingzhouqi>90";
            }

            if (nCD == 8)
            {
                if (zdjx == 1) sql += " and jszlsj_zuidajingxie<1.5";
                if (zdjx == 2) sql += " and jszlsj_zuidajingxie>=1.5 and jszlsj_zuidajingxie<2";
                if (zdjx == 3) sql += " and jszlsj_zuidajingxie>=2 and jszlsj_zuidajingxie<3.5";
                if (zdjx == 4) sql += " and jszlsj_zuidajingxie>=3.5";
            }

            if (nCD == 9)
            {
                if (jdr != "全部") sql += " and fuzejiandu like '%" + jdr + "%'";
            }

            if (nCD == 10)
            {
                if (dw != "全部") sql += " and shigongduiwu like '%" + dw + "%'";
            }

            lblWhere.Text = sql;
            GridView2_DataBind();
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

        protected void ddlQukuai_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetJianduren();
            resetDuiwu();
        }

        private void resetDuiwu()
        {
            string qk = ddlQukuai.SelectedItem.Text;
            string sql = "";
            if (qk == "全部") sql = "select jiancheng from T_System_Duiwu where jiancheng<>' ' and jiancheng is not null and zjoryl='钻井' group by jiancheng";
            else
            {
                sql = "select jiancheng from T_System_Duiwu where jiancheng<>' ' and jiancheng is not null and zjoryl='钻井' and qukuai='" + qk + "' group by jiancheng";
            }
            DataTable dt = DataBaseHelper.query(sql);
            ListItem[] lstItems = toListItem(dt, "jiancheng", "jiancheng");
            ddlDuiwu.Items.Clear();
            ddlDuiwu.Items.Add("全部");
            ddlDuiwu.Items.AddRange(lstItems);
        }

        private void resetJianduren()
        {
            string qk = ddlQukuai.SelectedItem.Text;
            string sql = "";
            if (qk == "全部") sql = "select Employee_Name from T_System_EMPLOYEE where State='0' and Branch_ID not in (select Branch_ID from T_System_BRANCH where Father_ID='0') group by Employee_Name";
            else
            {
                string id = getBranchId(qk);
                sql = "select Employee_Name from T_System_EMPLOYEE where State='0' and Branch_ID='" + id + "' group by Employee_Name";
            }
            DataTable dt = DataBaseHelper.query(sql);
            ListItem[] lstItems = toListItem(dt, "Employee_Name", "Employee_Name");
            ddlJianduren.Items.Clear();
            ddlJianduren.Items.Add("全部");
            ddlJianduren.Items.AddRange(lstItems);
        }

        private string getBranchId(string qk)
        {
            string sql = "select Branch_ID from T_System_BRANCH where Branch_Name like '%" + qk + "%'";
            DataTable dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                string s = dt.Rows[0][0].ToString();
                return s;
            }
            return null;
        }

        private ListItem[] toListItem(DataTable dt, string text, string value)
        {
            ListItem[] lic = new ListItem[dt.Rows.Count];
            ListItem temp;
            DataRow dr = null;
            for (int i = lic.Length - 1; i >= 0; i--)
            {
                dr = dt.Rows[i];
                temp = new ListItem(dr[text].ToString(), dr[value].ToString());
                lic[i] = temp;
            }
            return lic;
        }
    }
}