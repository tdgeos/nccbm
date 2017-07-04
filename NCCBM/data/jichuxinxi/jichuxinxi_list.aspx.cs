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

namespace NCCBM.data.jichuxinxi
{
    public partial class jichuxinxi_list : System.Web.UI.Page
    {
        private string szType = null;
        private string tablename = "jing_jichuxinxi";
        private DataTable dt = null;
        private int nPageSize = common.GetPageSize();
        private int nCols = 14;
        private string strOrderBy = " order by qukuai";
        //private string strQukuaiWhere = "";

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

            string szPage = Request.QueryString["iPage"];
            if (szPage != null && szPage != "")
            {
                tbPageIndex.Text = szPage;
            }

            string szQukuai = Request.QueryString["Qukuai"];
            if (szQukuai != null && szQukuai != "")
            {
                tbQk.Text = szQukuai;
            }

            string szZt = Request.QueryString["Zhuangtai"];
            if (szZt != null && szZt != "")
            {
                tbZt.Text = szZt;
            }

            string szJinghao = Request.QueryString["Jinghao"];
            if (szJinghao != null && szJinghao != "")
            {
                tbJinghao.Text = szJinghao;
            }

            if (!Page.IsPostBack)
            {
                if (userPlaceId == 0)
                {
                    lstQukuai.Items.Add("全部");
                    lstQukuai.Items.Add("韩城");
                    lstQukuai.Items.Add("临汾");
                    lstQukuai.Items.Add("忻州");
                }
                if (userPlaceId == 1)
                {
                    lstQukuai.Items.Add("韩城");
                }
                if (userPlaceId == 2)
                {
                    lstQukuai.Items.Add("临汾");
                }
                if (userPlaceId == 3)
                {
                    lstQukuai.Items.Add("忻州");
                }

                if (tbQk.Text.Trim() != "")
                {
                    lstQukuai.SelectedIndex = Int32.Parse(tbQk.Text.Trim());
                }

                if (tbZt.Text.Trim() != "")
                {
                    lstZhuangtai.SelectedIndex = Int32.Parse(tbZt.Text.Trim());
                }

                GridView2.Attributes.Add("BorderColor", "Black");
                GridView2.Attributes.Add("BorderWidth", "1");

                GridView2.ShowFooter = false;
                GridView2.PageSize = nPageSize;
                if (tbPageIndex.Text != "") GridView2.PageIndex = Int32.Parse(tbPageIndex.Text);

                string qk = lstQukuai.SelectedItem.Text;
                string zt = lstZhuangtai.SelectedItem.Text;
                string strQukuai = "";
                string strJinghao = "";
                string strZt = "";
                if (qk != "全部") strQukuai = " and qukuai='" + qk + "'";
                if (tbJinghao.Text.Trim() != "") strJinghao = " and jinghao like '%" + tbJinghao.Text.Trim() + "%'";
                if (zt != "全部") strZt = " and dangqianzhuangtai='" + zt + "'";
                string sql = "select * from " + tablename + " where 1=1 " + strQukuai + strJinghao + strZt;

                dt = DataBaseHelper.query(sql + strOrderBy);
                GridView2.DataSource = dt;
                GridView2.DataBind();
                if (dt.Rows.Count <= 0)
                {
                    lblInfo.ForeColor = System.Drawing.Color.Red;
                    lblInfo.Text = "没有符合条件的记录。";
                }
                else
                {
                    lblInfo.ForeColor = System.Drawing.Color.Black;
                    lblInfo.Text = "查询到 " + dt.Rows.Count + " 条记录。";
                }

                for (int i = 1; i <= GridView2.Rows.Count; i++)
                {
                    int n = GridView2.PageIndex * nPageSize + i - 1;
                    GridView2.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
                }

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
                lblInfo.Text = "";
                if (tbPageIndex.Text != "") GridView2.PageIndex = Int32.Parse(tbPageIndex.Text);

                string qk = lstQukuai.SelectedItem.Text;
                string zt = lstZhuangtai.SelectedItem.Text;
                string strQukuai = "";
                string strJinghao = "";
                string strZt = "";
                if (qk != "全部") strQukuai = " and qukuai='" + qk + "'";
                if (tbJinghao.Text.Trim() != "") strJinghao = " and jinghao like '%" + tbJinghao.Text.Trim() + "%'";
                if (zt != "全部") strZt = " and dangqianzhuangtai='" + zt + "'";
                string sql = "select * from " + tablename + " where 1=1 " + strQukuai + strJinghao + strZt;
                dt = DataBaseHelper.query(sql + strOrderBy);
                GridView2.DataSource = dt;
                GridView2.DataBind();
                if (dt.Rows.Count <= 0)
                {
                    lblInfo.ForeColor = System.Drawing.Color.Red;
                    lblInfo.Text = "没有符合条件的记录。";
                }
                else
                {
                    lblInfo.ForeColor = System.Drawing.Color.Black;
                    lblInfo.Text = "查询到 " + dt.Rows.Count + " 条记录。";
                }
                for (int i = 1; i <= GridView2.Rows.Count; i++)
                {
                    int n = GridView2.PageIndex * nPageSize + i - 1;
                    GridView2.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
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
                Response.Redirect("jichuxinxi_edit.aspx?ID=" + id + "&iPage=" + iPage + "&Qukuai=" + lstQukuai.SelectedIndex + "&Zhuangtai=" + lstZhuangtai.SelectedIndex + "&Jinghao=" + tbJinghao.Text);
            }

            if (e.CommandName == "shanchu")
            {
                string sql = "delete from " + tablename + " where id = " + id;
                try
                {
                    DataBaseHelper.execute(sql);
                    string qk = lstQukuai.SelectedItem.Text;
                    string zt = lstZhuangtai.SelectedItem.Text;
                    string strQukuai = "";
                    string strJinghao = "";
                    string strZt = "";
                    if (qk != "全部") strQukuai = " and qukuai='" + qk + "'";
                    if (tbJinghao.Text.Trim() != "") strJinghao = " and jinghao like '%" + tbJinghao.Text.Trim() + "%'";
                    if (zt != "全部") strZt = " and dangqianzhuangtai='" + zt + "'";
                    sql = "select * from " + tablename + " where 1=1 " + strQukuai + strJinghao + strZt;
                    dt = DataBaseHelper.query(sql + strOrderBy);
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    for (int i = 1; i <= GridView2.Rows.Count; i++)
                    {
                        int n = GridView2.PageIndex * nPageSize + i - 1;
                        GridView2.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
                    }
                }
                catch (Exception e2)
                {

                }
            }
        }

        protected void Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            tbPageIndex.Text = e.NewPageIndex.ToString();
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            for (int i = 1; i <= GridView2.Rows.Count; i++)
            {
                int n = GridView2.PageIndex * nPageSize + i - 1;
                GridView2.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
            }
        }

        protected void btnShaixuan_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "";
            if (tbPageIndex.Text != "") GridView2.PageIndex = Int32.Parse(tbPageIndex.Text);

            string qk = lstQukuai.SelectedItem.Text;
            string zt = lstZhuangtai.SelectedItem.Text;
            string strQukuai = "";
            string strJinghao = "";
            string strZt = "";
            if (qk != "全部") strQukuai = " and qukuai='" + qk + "'";
            if (tbJinghao.Text.Trim() != "") strJinghao = " and jinghao like '%" + tbJinghao.Text.Trim() + "%'";
            if (zt != "全部") strZt = " and dangqianzhuangtai='" + zt + "'";
            string sql = "select * from " + tablename + " where 1=1 " + strQukuai + strJinghao + strZt;
            dt = DataBaseHelper.query(sql + strOrderBy);
            GridView2.DataSource = dt;
            GridView2.DataBind();
            if (dt.Rows.Count <= 0)
            {
                lblInfo.ForeColor = System.Drawing.Color.Red;
                lblInfo.Text = "没有符合条件的记录。";
            }
            else
            {
                lblInfo.ForeColor = System.Drawing.Color.Black;
                lblInfo.Text = "查询到 " + dt.Rows.Count + " 条记录。";
            }
            for (int i = 1; i <= GridView2.Rows.Count; i++)
            {
                int n = GridView2.PageIndex * nPageSize + i - 1;
                GridView2.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
            }
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/NoLogin.aspx");

        }

    }
}