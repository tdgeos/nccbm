using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.Query.yl
{
    public partial class ylzdycx : System.Web.UI.Page
    {
        private int nPageSize = common.GetPageSize();
        private int nFujianIndex = 12;

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
            string[] tjs = cd.Split(',');

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

                if (tjs.Length == 6)
                {
                    int iQk = Int32.Parse(tjs[1]);
                    ddlQukuai.SelectedIndex = iQk;
                    resetJianduren();
                    resetDuiwu();
                    string rqBegin = tjs[2];
                    string rqEnd = tjs[3];
                    
                    int iPageIndex = Int32.Parse(tjs[5]);
                    GridView2.PageIndex = iPageIndex;

                    string[] ss = tjs[4].Split('a');
                    int index = Int32.Parse(ss[0]);
                    ddlJianduren.SelectedIndex = index;

                    index = Int32.Parse(ss[1]);
                    ddlDuiwu.SelectedIndex = index;

                    index = Int32.Parse(ss[2]);
                    ddlHege.SelectedIndex = index;

                    index = Int32.Parse(ss[3]);
                    ddlXiabeng.SelectedIndex = index;

                    ddlQukuai.SelectedIndex = iQk;
                    tbBegin.Text = rqBegin;
                    tbEnd.Text = rqEnd;

                    string qk = ddlQukuai.SelectedItem.Text;
                    string jdr = ddlJianduren.SelectedItem.Text;
                    string dw = ddlDuiwu.SelectedItem.Text;
                    int xb = ddlXiabeng.SelectedIndex;
                    int hg = ddlHege.SelectedIndex;

                    string where_qk = "";
                    string where_rq = "";
                    string where_jd = "";
                    string where_dw = "";
                    string where_hg = "";
                    string where_xb = "";
                    string strWhere = " where 1=1";

                    if (qk != "全部") where_qk = " and place='" + qk + "'";
                    if (rqBegin != "" && rqEnd != "") where_rq = " and shigongriqi<='" + rqEnd + "' and shigongriqi>='" + rqBegin + "'";
                    if (jdr != "全部") where_jd = " and jianduren like '%" + jdr + "%'";
                    if (dw != "全部") where_dw = " and shigongduiwu like '%" + dw + "%'";
                    if (hg == 1) where_hg = " and shifouhege like '%是%'";
                    if (hg == 2) where_hg = " and shifouhege like '%否%'";

                    if (xb == 1) where_xb = " and jinghao not in (select jinghao from Xls_Yl_Rbb_Xb where yanshouqingkuang like '%已验收%')";
                    if (xb == 2) where_xb = " and jinghao in (select jinghao from Xls_Yl_Rbb_Xb where yanshouqingkuang like '%已验收%')";

                    strWhere += where_qk + where_rq;
                    strWhere += where_jd;
                    strWhere += where_dw;
                    strWhere += where_hg;
                    strWhere += where_xb;

                    string sql = "select * from Xls_Yl_Rbb_Ylsg " + strWhere;
                    lblWhere.Text = strWhere;
                }
                else
                {
                    if (tbBegin.Text == "" && tbEnd.Text == "")
                    {
                        //DateTime dtime = System.DateTime.Now;
                        //tbBegin.Text = dtime.Year + "-" + dtime.Month + "-01";
                        //tbEnd.Text = dtime.Year + "-" + dtime.Month + "-" + dtime.Day;
                    }

                    string sql = "";
                    if (ddlQukuai.SelectedItem.Text == "全部")
                    {
                        sql = "select * from Xls_Yl_Rbb_Ylsg where 1=1 ";
                        lblWhere.Text = " where 1=1 ";
                    }
                    else
                    {
                        sql = "select * from Xls_Yl_Rbb_Ylsg where 1=1 and place='" + ddlQukuai.SelectedItem.Text + "' ";
                        lblWhere.Text = "where 1=1 and place='" + ddlQukuai.SelectedItem.Text + "' ";
                    }
                }
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

                String sqlTemp = "select jinghao from Xls_Yl_Rbb_Ylsg " + lblWhere.Text + " group by jinghao";
                DataTable dtTemp = DataBaseHelper.query(sqlTemp);
                int iJingshu = dtTemp.Rows.Count;
                sqlTemp = "select jinghao from Xls_Yl_Rbb_Ylsg " + lblWhere.Text + " and shifouyawan like '%是%' group by jinghao";
                dtTemp = DataBaseHelper.query(sqlTemp);
                int iYawan = dtTemp.Rows.Count;

                //sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg " + lblWhere.Text + " group by jinghao,cengwei";
                //dtTemp = DataBaseHelper.query(sqlTemp);
                //int iCengshu = dtTemp.Rows.Count;
                //sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg " + lblWhere.Text + " and shifouhege like '%是%' group by jinghao,cengwei";
                //dtTemp = DataBaseHelper.query(sqlTemp);
                //int iWanchengCengshu = dtTemp.Rows.Count;

                sqlTemp = "select cengwei from Xls_Yl_Rbb_Ylsg " + lblWhere.Text;
                dtTemp = DataBaseHelper.query(sqlTemp);
                int iZcs = dtTemp.Rows.Count;
                sqlTemp = "select cengwei from Xls_Yl_Rbb_Ylsg " + lblWhere.Text + " and shifouhege like '%是%'";
                dtTemp = DataBaseHelper.query(sqlTemp);
                int iCgcs = dtTemp.Rows.Count;

                string cgl = String.Format("{0:N2}", iCgcs * 100.0f / iZcs) + "%";

                result_number.ForeColor = System.Drawing.Color.Black;
                result_number.Text = "共查询到" + iJingshu + "口井，其中共压裂完成" + iYawan + "口井，共压裂" + iZcs + "次，成功率" + cgl + "。";
                
                GridView2.DataSource = dt;
                GridView2.DataBind();

                for (int i = 1; i <= GridView2.Rows.Count; i++)
                {
                    int n = GridView2.PageIndex * nPageSize + i - 1;
                    GridView2.Rows[i - 1].Cells[0].Text = (n + 1).ToString();

                    String jh = dt.Rows[n]["jinghao"].ToString().Trim();
                    String ch = dt.Rows[n]["cengwei"].ToString().Trim();
                    ch = ch.Replace('#', '^');
                    ch = ch.Replace('+', '~');
                    String qk = dt.Rows[n]["place"].ToString().Trim();
                    int gv_qk = ddlQukuai.SelectedIndex;
                    String rqBegin = tbBegin.Text;
                    String rqEnd = tbEnd.Text;
                    int gv_index = GridView2.PageIndex;
                    if (MyTools.IsHaveFujian(qk, "压裂施工", jh + "_" + ch))
                    {
                        LinkButton lbtn = new LinkButton();
                        String str = qk + ",压裂施工," + jh + "_" + ch;
                        String str2 = "q," + 0 + "," + gv_qk + "," + rqBegin + "," + rqEnd + "," + gv_index + ",";
                        lbtn.PostBackUrl = "~/Fujian.aspx?FJ=" + str + "&Page=" + str2;
                        lbtn.Text = "查看";
                        lbtn.CausesValidation = false;
                        GridView2.Rows[i - 1].Cells[nFujianIndex].Controls.Add(lbtn);
                    }

                    string sfhg = dt.Rows[n]["shifouhege"].ToString().Trim();
                    string sjsl = dt.Rows[n]["wanchengbaifenbi"].ToString().Trim();
                    float fjsl = 0;
                    string s1 = sjsl.Split('％')[0];
                    s1 = s1.Split('%')[0];
                    try
                    {
                        fjsl = float.Parse(s1);
                    }
                    catch (Exception e)
                    {
                    }
                    if (sfhg == "否")
                    {
                        GridView2.Rows[i - 1].BackColor = System.Drawing.Color.FromArgb(255, 0x40, 0x40);//System.Drawing.Color.Red
                    }
                    else if (fjsl < 80)
                    {
                        GridView2.Rows[i - 1].BackColor = System.Drawing.Color.FromArgb(0xff, 0x80, 0x80);
                    }
                    else if (fjsl >= 80 && fjsl < 100)
                    {
                        GridView2.Rows[i - 1].BackColor = System.Drawing.Color.FromArgb(0xff, 0xf0, 0x80);
                    }
                }
            }
            SetDivePage();
        }

        private DataTable getData()
        {
            string sql = "select * from Xls_Yl_Rbb_Ylsg " + lblWhere.Text;
            return DataBaseHelper.query(sql);
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
            string jh = GridView2.Rows[gvrow.RowIndex].Cells[1].Text;
            string cw = GridView2.Rows[gvrow.RowIndex].Cells[2].Text;
            cw = cw.Replace('+', '_');
            cw = cw.Replace('#', '^');
            string str = ddlJianduren.SelectedIndex + "a" + ddlDuiwu.SelectedIndex + "a" + ddlHege.SelectedIndex + "a" + ddlXiabeng.SelectedIndex;
            Response.Redirect("ylmx.aspx?JH=" + jh + "," + cw + "," + 0 + "," + ddlQukuai.SelectedIndex + "," + tbBegin.Text + "," + tbEnd.Text + "," + str + "," + GridView2.PageIndex);
        }

        protected void Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
        }

        protected void ddlQukuai_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetJianduren();
            resetDuiwu();
        }

        protected void Query_Click(object sender, EventArgs e)
        {
            result_number.Text = "";

            string qk = ddlQukuai.SelectedItem.Text;
            string rqBegin = tbBegin.Text;
            string rqEnd = tbEnd.Text;
            string jdr = ddlJianduren.SelectedItem.Text;
            string dw = ddlDuiwu.SelectedItem.Text;
            int xb = ddlXiabeng.SelectedIndex;
            int hg = ddlHege.SelectedIndex;

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
            string where_jd = "";
            string where_dw = "";
            string where_hg = "";
            string where_xb = "";
            string strWhere = " where 1=1";

            if (qk != "全部") where_qk = " and place='" + qk + "'";
            if (rqBegin != "" && rqEnd != "") where_rq = " and shigongriqi<='" + rqEnd + "' and shigongriqi>='" + rqBegin + "'";
            if (jdr != "全部") where_jd = " and jianduren like '%" + jdr + "%'";
            if (dw != "全部") where_dw = " and shigongduiwu like '%" + dw + "%'";
            if (hg == 1) where_hg = " and shifouhege like '%是%'";
            if (hg == 2) where_hg = " and shifouhege like '%否%'";

            if (xb == 1) where_xb = " and jinghao not in (select jinghao from Xls_Yl_Rbb_Xb where yanshouqingkuang like '%已验收%')";
            if (xb == 2) where_xb = " and jinghao in (select jinghao from Xls_Yl_Rbb_Xb where yanshouqingkuang like '%已验收%')";

            strWhere += where_qk + where_rq;
            strWhere += where_jd;
            strWhere += where_dw;
            strWhere += where_hg;
            strWhere += where_xb;

            string sql = "select * from Xls_Yl_Rbb_Ylsg " + strWhere;
            lblWhere.Text = strWhere;
            GridView2_DataBind();
        }

        private void resetJianduren()
        {
            string qk = ddlQukuai.SelectedItem.Text;
            string sql = "";
            if (qk == "全部") sql = "select Employee_Name from T_System_EMPLOYEE where State='0' and Zjoryl='压裂' and Branch_ID not in (select Branch_ID from T_System_BRANCH where Father_ID='0') group by Employee_Name";
            else
            {
                string id = getBranchId(qk);
                sql = "select Employee_Name from T_System_EMPLOYEE where State='0' and Zjoryl='压裂' and Branch_ID='" + id + "' group by Employee_Name";
            }
            DataTable dt = DataBaseHelper.query(sql);
            ListItem[] lstItems = toListItem(dt, "Employee_Name", "Employee_Name");
            ddlJianduren.Items.Clear();
            ddlJianduren.Items.Add("全部");
            ddlJianduren.Items.AddRange(lstItems);
        }

        private void resetDuiwu()
        {
            string qk = ddlQukuai.SelectedItem.Text;
            string sql = "";
            if (qk == "全部") sql = "select jiancheng from T_System_Duiwu where jiancheng<>' ' and jiancheng is not null and zjoryl='压裂' group by jiancheng";
            else
            {
                sql = "select jiancheng from T_System_Duiwu where jiancheng<>' ' and jiancheng is not null and zjoryl='压裂' and qukuai='" + qk + "' group by jiancheng";
            }
            DataTable dt = DataBaseHelper.query(sql);
            ListItem[] lstItems = toListItem(dt, "jiancheng", "jiancheng");
            ddlDuiwu.Items.Clear();
            ddlDuiwu.Items.Add("全部");
            ddlDuiwu.Items.AddRange(lstItems);
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