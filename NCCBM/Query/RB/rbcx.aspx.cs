using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace NCCBM.Query.rb
{
    public partial class rbcx : System.Web.UI.Page
    {
        private int nPageSize = common.GetPageSize();
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
                if (!IsPostBack)
                {
                    string user = Request.QueryString["UserName"];
                    string pwd = Request.QueryString["PassWord"];
                    string encryptedPWD = common.Desc(user, pwd, 0);
                    if (!USER_CHECK.USER_CHECKER2(user, encryptedPWD))
                    {
                        HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
                    }
                }
            }

            string cd = Request.QueryString["CD"];
            string[] tjs = cd.Split(',');
            rsHid.Value = tjs[0];
            nCD = Int32.Parse(tjs[0]);

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

                gv.ShowFooter = false;
                gv.PageSize = nPageSize;

                resetJianduren();
                resetDuiwu();

                if (tjs.Length == 6)
                {
                    int iQk = Int32.Parse(tjs[1]);
                    ddlQukuai.SelectedIndex = iQk;
                    //resetJianduren();
                    //resetDuiwu();
                    string rqBegin = tjs[2];
                    string rqEnd = tjs[3];
                    string neirong = tjs[4];
                    int iPageIndex = Int32.Parse(tjs[5]);
                    gv.PageIndex = iPageIndex;
                    if (nCD == 2) tbJinghao.Text = neirong;
                    if (nCD == 3)
                    {
                        int index = Int32.Parse(neirong);
                        ddlJianduren.SelectedIndex = index;
                    }
                    if (nCD == 4)
                    {
                        int index = Int32.Parse(neirong);
                        ddlDuiwu.SelectedIndex = index;
                    }

                    ddlQukuai.SelectedIndex = iQk;
                    tbRiqiBegin.Text = rqBegin;
                    tbRiqiEnd.Text = rqEnd;

                    string qk = ddlQukuai.SelectedItem.Text;
                    string jh = tbJinghao.Text;
                    string jdr = ddlJianduren.SelectedItem.Text;
                    string dw = ddlDuiwu.SelectedItem.Text;

                    string where_qk = "";
                    string where_rq = "";
                    string where_jh = "";
                    string where_jd = "";
                    string where_dw = "";
                    string strWhere = " where 1=1";

                    if (qk != "全部") where_qk = " and place='" + qk + "'";
                    if (rqBegin != "" && rqEnd != "") where_rq = " and riqi<='" + rqEnd + "' and riqi>='" + rqBegin + "'";
                    if (jh != "") where_jh = " and jinghao like '%" + jh + "%'";
                    if (jdr != "全部") where_jd = " and fuzejiandu like '%" + jdr + "%'";
                    if (dw != "全部") where_dw = " and duihao like '%" + dw + "%'";

                    strWhere += where_qk + where_rq;

                    if (nCD == 2)
                    {
                        strWhere += where_jh;
                    }
                    if (nCD == 3)
                    {
                        strWhere += where_jd;
                    }
                    if (nCD == 4)
                    {
                        strWhere += where_dw;
                    }

                    lblWhere.Text = strWhere;
                }
                else
                {
                    string sql = "select top 1 riqi from Xls_Zj_Rbb_Zj order by riqi desc";
                    DataTable dtTemp = DataBaseHelper.query(sql);
                    DateTime riqi = (DateTime)dtTemp.Rows[0][0];

                    string rqBegin = riqi.AddDays(-1).ToString("yyyy-MM-dd");
                    string rqEnd = riqi.ToString("yyyy-MM-dd");

                    tbRiqiBegin.Text = rqBegin;
                    tbRiqiEnd.Text = rqEnd;

                    string qk = ddlQukuai.SelectedItem.Text;
                    string jh = tbJinghao.Text;
                    string jdr = ddlJianduren.SelectedItem.Text;
                    string dw = ddlDuiwu.SelectedItem.Text;

                    string where_qk = "";
                    string where_rq = "";
                    string where_jh = "";
                    string where_jd = "";
                    string where_dw = "";
                    string strWhere = " where 1=1";

                    if (qk != "全部") where_qk = " and place='" + qk + "'";
                    if (rqBegin != "" && rqEnd != "") where_rq = " and riqi<='" + rqEnd + "' and riqi>='" + rqBegin + "'";
                    if (jh != "") where_jh = " and jinghao like '%" + jh + "%'";
                    if (jdr != "全部") where_jd = " and fuzejiandu like '%" + jdr + "%'";
                    if (dw != "全部") where_dw = " and duihao like '%" + dw + "%'";

                    strWhere += where_qk + where_rq;

                    if (nCD == 2)
                    {
                        strWhere += where_jh;
                    }
                    if (nCD == 3)
                    {
                        strWhere += where_jd;
                    }
                    if (nCD == 4)
                    {
                        strWhere += where_dw;
                    }

                    lblWhere.Text = strWhere;
                }

                DataTable dt = getData();
                if (dt == null || dt.Rows.Count == 0)
                {
                    gv.Visible = false;
                    result_number.ForeColor = System.Drawing.Color.Red;
                    result_number.Text = "没有查询到符合条件的记录。";
                }
                else
                {
                    gv.Visible = true;
                    gv.DataSource = dt;
                    gv.DataBind();
                    int iCount = dt.Rows.Count;
                    int iYichang = 0;
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        int n = gv.PageIndex * nPageSize + i;
                        gv.Rows[i].Cells[0].Text = (n + 1).ToString();
                        if (isYichang(gv.Rows[i].Cells[1].Text.Trim()))
                        {
                            gv.Rows[i].BackColor = System.Drawing.Color.Red;
                        }

                        String jh = dt.Rows[n]["jinghao"].ToString().Trim();
                        String qk = dt.Rows[n]["qukuai"].ToString().Trim();
                        int gv_qk = ddlQukuai.SelectedIndex;
                        String rqBegin = tbRiqiBegin.Text;
                        String rqEnd = tbRiqiEnd.Text;
                        int gv_index = gv.PageIndex;
                        String info = "";
                        if (nCD == 1) info = "";
                        if (nCD == 2) info = tbJinghao.Text;
                        if (nCD == 3) info = ddlJianduren.SelectedIndex.ToString();
                        if (nCD == 4) info = ddlDuiwu.SelectedIndex.ToString();
                        if (MyTools.IsHaveFujian(qk, "钻进", jh))
                        {
                            LinkButton lbtn = new LinkButton();
                            String str = qk + ",钻进," + jh;
                            String str2 = "f," + nCD + "," + gv_qk + "," + rqBegin + "," + rqEnd + "," + gv_index + "," + info;
                            lbtn.PostBackUrl = "~/Fujian.aspx?FJ=" + str + "&Page=" + str2;
                            lbtn.Text = "查看";
                            lbtn.CausesValidation = false;
                            gv.Rows[i].Cells[10].Controls.Add(lbtn);
                        }
                    }
                    for (int i = 0; i < iCount; i++)
                    {
                        if (isYichang(dt.Rows[i][1].ToString()))
                        {
                            iYichang++;
                        }
                    }
                    string bl = String.Format("{0:N2}", iYichang * 100.0f / iCount) + "%";
                    result_number.ForeColor = System.Drawing.Color.Black;
                    result_number.Text = "共查询到" + iCount + "口井，其中" + iYichang + "口出现施工异常，所占比例为" + bl + "。";
                }
            }
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvrow = null;
            try
            {
                gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            }
            catch (System.InvalidCastException ice)
            {
                return;
            }
            string jh = gv.Rows[gvrow.RowIndex].Cells[3].Text;
            string str = "";
            if (nCD == 1) str = "";
            if (nCD == 2) str = tbJinghao.Text;
            if (nCD == 3) str = ddlJianduren.SelectedIndex.ToString();
            if (nCD == 4) str = ddlDuiwu.SelectedIndex.ToString();
            Response.Redirect("xxxx.aspx?JH=" + jh + "," + nCD + "," + ddlQukuai.SelectedIndex + "," + tbRiqiBegin.Text + "," + tbRiqiEnd.Text + "," + str + "," + gv.PageIndex);
        }

        protected void Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            DataTable dt = getData();
            gv.DataSource = dt;
            gv.DataBind();

            for (int i = 0; i < gv.Rows.Count; i++)
            {
                int n = gv.PageIndex * nPageSize + i;
                gv.Rows[i].Cells[0].Text = (n + 1).ToString();

                String jh = dt.Rows[n]["jinghao"].ToString().Trim();
                String qk = dt.Rows[n]["qukuai"].ToString().Trim();
                int gv_qk = ddlQukuai.SelectedIndex;
                String rqBegin = tbRiqiBegin.Text;
                String rqEnd = tbRiqiEnd.Text;
                int gv_index = gv.PageIndex;
                String info = "";
                if (nCD == 1) info = "";
                if (nCD == 2) info = tbJinghao.Text;
                if (nCD == 3) info = ddlJianduren.SelectedIndex.ToString();
                if (nCD == 4) info = ddlDuiwu.SelectedIndex.ToString();
                if (MyTools.IsHaveFujian(qk, "钻进", jh))
                {
                    LinkButton lbtn = new LinkButton();
                    String str = qk + ",钻进," + jh;
                    String str2 = "q," + nCD + "," + gv_qk + "," + rqBegin + "," + rqEnd + "," + gv_index + "," + info;
                    lbtn.PostBackUrl = "~/Fujian.aspx?FJ=" + str + "&Page=" + str2;
                    lbtn.Text = "查看";
                    lbtn.CausesValidation = false;
                    gv.Rows[i].Cells[10].Controls.Add(lbtn);
                }
            }
        }

        protected void Query_Click(object sender, EventArgs e)
        {
            result_number.Text = "";

            string qk = ddlQukuai.SelectedItem.Text;
            string rqBegin = tbRiqiBegin.Text;
            string rqEnd = tbRiqiEnd.Text;
            string jh = tbJinghao.Text;
            string jdr = ddlJianduren.SelectedItem.Text;
            string dw = ddlDuiwu.SelectedItem.Text;

            if (rqBegin == "" && rqEnd != "")
            {
                result_number.ForeColor = System.Drawing.Color.Red;
                result_number.Text = "请先输入一个起始日期。";
                return;
            }
            if (rqBegin != "" && rqEnd == "")
            {
                result_number.ForeColor = System.Drawing.Color.Red;
                result_number.Text = "请先输入一个截止日期。";
                return;
            }

            string where_qk = "";
            string where_rq = "";
            string where_jh = "";
            string where_jd = "";
            string where_dw = "";
            string strWhere = " where 1=1";

            if (qk != "全部") where_qk = " and place='" + qk + "'";
            if (rqBegin != "" && rqEnd != "") where_rq = " and riqi<='" + rqEnd + "' and riqi>='" + rqBegin + "'";
            if (jh != "") where_jh = " and jinghao like '%" + jh + "%'";
            if (jdr != "全部") where_jd = " and fuzejiandu like '%" + jdr + "%'";
            if (dw != "全部") where_dw = " and duihao like '%" + dw + "%'";

            strWhere += where_qk + where_rq;

            if (nCD == 2)
            {
                strWhere += where_jh;
            }
            if (nCD == 3)
            {
                strWhere += where_jd;
            }
            if (nCD == 4)
            {
                strWhere += where_dw;
            }

            lblWhere.Text = strWhere;

            DataTable dt = getData();
            if (dt == null || dt.Rows.Count == 0)
            {
                gv.Visible = false;
                result_number.ForeColor = System.Drawing.Color.Red;
                result_number.Text = "没有查询到符合条件的记录。";
            }
            else
            {
                gv.Visible = true;
                gv.DataSource = dt;
                gv.DataBind();
                int iCount = dt.Rows.Count;
                int iYichang = 0;
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    int n = gv.PageIndex * nPageSize + i;
                    gv.Rows[i].Cells[0].Text = (n + 1).ToString();
                    if (isYichang(gv.Rows[i].Cells[1].Text.Trim()))
                    {
                        gv.Rows[i].BackColor = System.Drawing.Color.Red;
                    }

                    String jh2 = dt.Rows[n]["jinghao"].ToString().Trim();
                    String qk2 = dt.Rows[n]["qukuai"].ToString().Trim();
                    int gv_qk = ddlQukuai.SelectedIndex;
                    String rqBegin2 = tbRiqiBegin.Text;
                    String rqEnd2 = tbRiqiEnd.Text;
                    int gv_index = gv.PageIndex;
                    String info = "";
                    if (nCD == 1) info = "";
                    if (nCD == 2) info = tbJinghao.Text;
                    if (nCD == 3) info = ddlJianduren.SelectedIndex.ToString();
                    if (nCD == 4) info = ddlDuiwu.SelectedIndex.ToString();
                    if (MyTools.IsHaveFujian(qk2, "钻进", jh2))
                    {
                        LinkButton lbtn = new LinkButton();
                        String str = qk2 + ",钻进," + jh2;
                        String str2 = "q," + nCD + "," + gv_qk + "," + rqBegin2 + "," + rqEnd2 + "," + gv_index + "," + info;
                        lbtn.PostBackUrl = "~/Fujian.aspx?FJ=" + str + "&Page=" + str2;
                        lbtn.Text = "查看";
                        lbtn.CausesValidation = false;
                        gv.Rows[i].Cells[10].Controls.Add(lbtn);
                    }
                }
                for (int i = 0; i < iCount; i++)
                {
                    if (isYichang(dt.Rows[i][1].ToString()))
                    {
                        iYichang++;
                    }
                }
                string bl = String.Format("{0:N2}", iYichang * 100.0f / iCount) + "%";
                result_number.ForeColor = System.Drawing.Color.Black;
                result_number.Text = "共查询到" + iCount + "口井，其中" + iYichang + "口出现施工异常，所占比例为" + bl + "。";
            }
        }

        private DataTable getData()
        {
            string str = lblWhere.Text;
            string order_by = " order by riqi desc";

            List<string[]> lstJh = new List<string[]>();

            string sql = "select id,fuzejiandu,duihao,jinghao,muqianjingshen,dangrijinchi,gongkuang,fuzaqingkuang,riqi,place from Xls_Zj_Rbb_Zj " + str + order_by;
            DataTable dt1 = DataBaseHelper.query(sql);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string fzjd = dt1.Rows[i][1].ToString();
                string dh = dt1.Rows[i][2].ToString();
                string jh = dt1.Rows[i][3].ToString();
                string mqjs = dt1.Rows[i][4].ToString();
                string drjc = dt1.Rows[i][5].ToString();
                string gk = dt1.Rows[i][6].ToString();
                string fzqk = dt1.Rows[i][7].ToString();
                string rq = dt1.Rows[i][8].ToString().Split(' ')[0];
                string qk = dt1.Rows[i][9].ToString();
                bool b = false;
                for (int j = 0; j < lstJh.Count; j++)
                {
                    if (jh == lstJh[j][2])
                    {
                        b = true;
                        break;
                    }
                }
                if (!b) lstJh.Add(new string[]{fzjd, dh, jh, mqjs, drjc, gk, fzqk, rq, qk});
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("fuzejiandu");
            dt.Columns.Add("duihao");
            dt.Columns.Add("jinghao");
            dt.Columns.Add("muqianjingshen");
            dt.Columns.Add("dangrijinchi");
            dt.Columns.Add("gongkuang");
            dt.Columns.Add("fuzaqingkuang");
            dt.Columns.Add("riqi");
            dt.Columns.Add("qukuai");
            dt.Columns.Add("fujian");
            for (int i = 0; i < lstJh.Count; i++)
            {
                DataRow dr;
                dr = dt.NewRow();
                dr[0] = i + 1;
                dr[1] = lstJh[i][0];
                dr[2] = lstJh[i][1];
                dr[3] = lstJh[i][2];
                dr[4] = lstJh[i][3];
                dr[5] = lstJh[i][4];
                dr[6] = lstJh[i][5];
                dr[7] = lstJh[i][6];
                dr[8] = lstJh[i][7];
                dr[9] = lstJh[i][8];
                dt.Rows.Add(dr);
            }
            return dt;
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

        private bool isYichang(string jh)
        {
            string sql = "select jinghao from Report_gjzuoye where jinghao='" + jh + "'";
            DataTable dtTemp = DataBaseHelper.query(sql);
            if (dtTemp != null && dtTemp.Rows.Count > 0) return true;

            sql = "select jinghao from Report_xtgzuoye where jinghao='" + jh + "'";
            dtTemp = DataBaseHelper.query(sql);
            if (dtTemp != null && dtTemp.Rows.Count > 0) return true;

            sql = "select jinghao from Report_jinglou where jinghao='" + jh + "'";
            dtTemp = DataBaseHelper.query(sql);
            if (dtTemp != null && dtTemp.Rows.Count > 0) return true;

            sql = "select jinghao from Report_jingshenzhiliang where jinghao='" + jh + "'";
            dtTemp = DataBaseHelper.query(sql);
            if (dtTemp != null && dtTemp.Rows.Count > 0) return true;

            sql = "select jinghao from Report_yikaiyanshou where jinghao='" + jh + "'";
            dtTemp = DataBaseHelper.query(sql);
            if (dtTemp != null && dtTemp.Rows.Count > 0) return true;

            sql = "select jinghao from Report_yongshui where jinghao='" + jh + "'";
            dtTemp = DataBaseHelper.query(sql);
            if (dtTemp != null && dtTemp.Rows.Count > 0) return true;

            return false;
        }
    }
}