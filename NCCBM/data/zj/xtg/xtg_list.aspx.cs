using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MR.Cdecl;

namespace NCCBM.data.zj.xtg
{
    public partial class xtg_list : System.Web.UI.Page
    {
        private string szType = null;
        private string tablename = "Xls_Zj_Rbb_Xtg";
        private DataTable dt = null;
        private int nPageSize = common.GetPageSize();
        private int nCols = 26;
        private string strOrderBy = " order by riqi desc";
        private string strQukuaiWhere = "";

        private string userName = null;
        private int userPlaceId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userName = HttpContext.Current.Session["LoginUserID"].ToString();
                userPlaceId = Int32.Parse(HttpContext.Current.Session["PlaceID"].ToString());
            }
            catch (NullReferenceException ne)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            szType = Request.QueryString["Type"];
            if (szType == "NULL" || szType == "" || szType == null)
            {
                szType = "list";
            }

            string szQk = Request.QueryString["Qukuai"];
            if (szQk != null && szQk != "")
            {
                tbQukuai.Text = szQk;
            }

            string szPage = Request.QueryString["iPage"];
            if (szPage != null && szPage != "")
            {
                tbPageIndex.Text = szPage;
            }
            string szDate1 = Request.QueryString["dateBegin"];
            string szDate2 = Request.QueryString["dateEnd"];
            if (szDate1 == null || szDate1 == "" || szDate2 == null || szDate2 == "")
            {
                if (tbKaishiRiqi.Text == "" && tbJieshuRiqi.Text == "")
                {
                    //DateTime dtime = System.DateTime.Now;
                    //tbKaishiRiqi.Text = dtime.Year + "-" + dtime.Month + "-01";
                    //tbJieshuRiqi.Text = dtime.Year + "-" + dtime.Month + "-" + dtime.Day;
                    string sql = "select top 1 riqi from Xls_Zj_Rbb_Xtg order by riqi desc";
                    DataTable dtTemp = DataBaseHelper.query(sql);
                    DateTime riqi = (DateTime)dtTemp.Rows[0][0];

                    string rqBegin = riqi.AddDays(-1).ToString("yyyy-MM-dd");
                    string rqEnd = riqi.ToString("yyyy-MM-dd");

                    tbKaishiRiqi.Text = rqBegin;
                    tbJieshuRiqi.Text = rqEnd;
                }
            }
            else
            {
                tbKaishiRiqi.Text = szDate1;
                tbJieshuRiqi.Text = szDate2;
            }

            if (!Page.IsPostBack)
            {
                if (userPlaceId == 0)
                {
                    ddlPlace.Items.Add("全部");
                    ddlPlace.Items.Add("韩城");
                    ddlPlace.Items.Add("临汾");
                    ddlPlace.Items.Add("忻州");
                }
                if (userPlaceId == 1)
                {
                    ddlPlace.Items.Add("韩城");
                }
                if (userPlaceId == 2)
                {
                    ddlPlace.Items.Add("临汾");
                }
                if (userPlaceId == 3)
                {
                    ddlPlace.Items.Add("忻州");
                }

                if (tbQukuai.Text.Trim() != "")
                {
                    ddlPlace.SelectedIndex = Int32.Parse(tbQukuai.Text.Trim());
                }

                GridView2.ShowFooter = false;
                GridView2.PageSize = nPageSize;
                if (tbPageIndex.Text != "") GridView2.PageIndex = Int32.Parse(tbPageIndex.Text);

                lblRiqi.Text = "";

                String strSQL = "select * from " + tablename + " where 1=1 ";
                string sqlDate = toSqldate(tbKaishiRiqi.Text, tbJieshuRiqi.Text);
                strSQL += sqlDate;

                strQukuaiWhere = "";
                if (ddlPlace.SelectedItem.Text != "全部") strQukuaiWhere = " and place='" + ddlPlace.SelectedItem.Text + "'";

                dt = DataBaseHelper.query(strSQL + strQukuaiWhere + strOrderBy);
                BindData();

                switch (szType)
                {
                    case "update":
                        GridView2.Columns[nCols].Visible = true;
                        break;
                    case "add":
                        GridView2.ShowFooter = true;
                        break;
                    case "delete":
                        GridView2.Columns[nCols + 1].Visible = true;
                        break;
                    case "list":
                        GridView2.Columns[nCols].Visible = false;
                        GridView2.Columns[nCols + 1].Visible = false;
                        break;
                }
            }
            else
            {
                lblRiqi.Text = "";
                if (tbPageIndex.Text != "") GridView2.PageIndex = Int32.Parse(tbPageIndex.Text);
                String strSQL = "select * from " + tablename + " where 1=1 ";
                DateTime dtime = System.DateTime.Now;
                string sqlDate = toSqldate(tbKaishiRiqi.Text, tbJieshuRiqi.Text);
                strSQL += sqlDate;
                strQukuaiWhere = "";
                if (ddlPlace.SelectedItem.Text != "全部") strQukuaiWhere = " and place='" + ddlPlace.SelectedItem.Text + "'";
                dt = DataBaseHelper.query(strSQL + strQukuaiWhere + strOrderBy);
                BindData();
            }
            SetDivePage();
        }

        private void SetDivePage()
        {
            if (dt.Rows.Count == 0 || GridView2.Visible == false || GridView2.PageCount == 1)
            {
                commPage1.Visible = false;
                return;
            }
            commPage1.Visible = true;
            WEE.MyPagerBar.BindDataDelegate f = new WEE.MyPagerBar.BindDataDelegate(BindData);
            commPage1.SetTarget(GridView2, f, GridView2.PageSize);
        }
        private void BindData()
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
            if (dt.Rows.Count <= 0)
            {
                lblRiqi.ForeColor = System.Drawing.Color.Red;
                lblRiqi.Text = "没有符合条件的记录。";
            }
            else
            {
                lblRiqi.ForeColor = System.Drawing.Color.Black;
                lblRiqi.Text = "查询到 " + dt.Rows.Count + " 条记录。";
            }
            for (int i = 1; i <= GridView2.Rows.Count; i++)
            {
                int n = GridView2.PageIndex * nPageSize + i - 1;
                GridView2.Rows[i - 1].Cells[0].Text = (n + 1).ToString();

                String jh = dt.Rows[n]["jinghao"].ToString().Trim();
                String qk = dt.Rows[n]["place"].ToString().Trim();
                int gv_qk = ddlPlace.SelectedIndex;
                String rqBegin = tbKaishiRiqi.Text;
                String rqEnd = tbJieshuRiqi.Text;
                int gv_index = GridView2.PageIndex;
                if (MyTools.IsHaveFujian(qk, "下套管", jh))
                {
                    LinkButton lbtn = new LinkButton();
                    String str = qk + ",下套管," + jh;
                    String str2 = "m," + gv_qk + "," + rqBegin + "," + rqEnd + "," + gv_index;
                    lbtn.PostBackUrl = "~/Fujian.aspx?FJ=" + str + "&Page=" + str2;
                    lbtn.Text = "查看";
                    lbtn.CausesValidation = false;
                    GridView2.Rows[i - 1].Cells[nCols - 1].Controls.Add(lbtn);
                }
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
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
            int iPage = 0;
            if (tbPageIndex.Text != "")
            {
                iPage = Int32.Parse(tbPageIndex.Text);
            }

            int index = gvrow.RowIndex + nPageSize * iPage;
            string id = dt.Rows[index]["id"].ToString();
            if (e.CommandName == "xiugai")
            {
                Response.Redirect("xtg_edit.aspx?ID=" + id + "&iPage=" + iPage + "&dtBegin=" + tbKaishiRiqi.Text + "&dtEnd=" + tbJieshuRiqi.Text + "&Qukuai=" + ddlPlace.SelectedIndex);
            }
            if (e.CommandName == "shanchu")
            {
                string sql = "delete from " + tablename + " where id = " + id;
                try
                {
                    DataBaseHelper.execute(sql);

                    string jh = dt.Rows[index]["jinghao"].ToString();
                    string qk = dt.Rows[index]["place"].ToString();
                    String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                    string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','删除','下套管','" + jh + "','','" + qk + "','')";
                    DataBaseHelper.execute(sqlLog);

                    string strSQL = "select * from " + tablename + " where 1=1 ";
                    string sqlDate = toSqldate(tbKaishiRiqi.Text, tbJieshuRiqi.Text);
                    strSQL += sqlDate;
                    strQukuaiWhere = "";
                    if (ddlPlace.SelectedItem.Text != "全部") strQukuaiWhere = " and place='" + ddlPlace.SelectedItem.Text + "'";
                    dt = DataBaseHelper.query(strSQL + strQukuaiWhere + strOrderBy);
                    BindData();
                }
                catch (Exception e2)
                {

                }
            }
        }

        protected void GridView2_RowCreated(object sender, EventArgs e1)
        {
            GridViewRowEventArgs e = e1 as GridViewRowEventArgs;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                rowHeader.CssClass = "tableLineHeader";
                Literal newCells = new Literal(); 
                string style1 = "";
                if (szType == "list") style1 = "class=hidden";
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
                      "<th rowspan='1' nowrap>附件</th>" +
                      "<th rowspan='1' nowrap " + style1 + ">操作</th>";

                TableCellCollection cells = e.Row.Cells;
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.RowSpan = 1;
                headerCell.Controls.Add(newCells);
                rowHeader.Cells.Add(headerCell);
                rowHeader.Cells.Add(headerCell);
                rowHeader.Visible = true;
                GridView2.Controls[0].Controls.AddAt(0, rowHeader);
            }
        }
        protected void Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            tbPageIndex.Text = e.NewPageIndex.ToString();
            GridView2.PageIndex = e.NewPageIndex;
            //BindData();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            String strSQL = "select * from " + tablename + " where 1=1 ";
            string strRiqiBegin = this.tbKaishiRiqi.Text.Trim();
            string strRiqiEnd = this.tbJieshuRiqi.Text.Trim();
            string sqlDate = toSqldate(strRiqiBegin, strRiqiEnd);
            if (sqlDate == null) return;

            lblRiqi.Text = "";

            strSQL += sqlDate;
            strQukuaiWhere = "";
            if (ddlPlace.SelectedItem.Text != "全部") strQukuaiWhere = " and place='" + ddlPlace.SelectedItem.Text + "'";
            dt = DataBaseHelper.query(strSQL + strQukuaiWhere + strOrderBy);
            BindData();
        }

        private string toSqldate(string strRiqiBegin, string strRiqiEnd)
        {
            string sqlRiqi = "";
            if (strRiqiBegin == "" && strRiqiEnd != "")
            {
                lblRiqi.ForeColor = System.Drawing.Color.Red;
                lblRiqi.Text = "条件选择错误：请输入开始日期。";
                return null;
            }
            if (strRiqiBegin != "" && strRiqiEnd == "")
            {
                lblRiqi.ForeColor = System.Drawing.Color.Red;
                lblRiqi.Text = "条件选择错误：请输入结束日期。";
                return null;
            }
            if (strRiqiBegin != "" && strRiqiEnd != "")
            {
                try
                {

                    DateTime dateBegin = Convert.ToDateTime(strRiqiBegin);
                    DateTime dateEnd = Convert.ToDateTime(strRiqiEnd);
                    sqlRiqi = " and cast(riqi as datetime) >= '" + dateBegin.ToString() + "' and cast(riqi as datetime) <= '" + dateEnd.ToString() + "'";
                }
                catch (System.FormatException fe)
                {
                    lblRiqi.ForeColor = System.Drawing.Color.Red;
                    lblRiqi.Text = "条件选择错误：无法识别所输入的日期格式。";
                    return null;
                }
            }
            return sqlRiqi;
        }
    }
}

