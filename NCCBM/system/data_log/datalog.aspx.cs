using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.system.data_log
{
    public partial class datalog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string szRoleID = HttpContext.Current.Session["RoleID"].ToString();
            }
            catch (NullReferenceException ne)
            {
                //HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            if (!IsPostBack)
            {
                GridView1.PageSize = 10;
                getData();
            }
        }

        protected void select_Click(object sender, EventArgs e)
        {
            getData();
        }

        protected void Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                int n = GridView1.PageIndex * 10 + i + 1;
                GridView1.Rows[i].Cells[0].Text = (n).ToString();
            }
        }

        void getData()
        {
            string sql = "select * from data_log where 1=1 ";

            string strRiqiBegin = this.tbKaishiRiqi.Text.Trim();
            string strRiqiEnd = this.tbJieshuRiqi.Text.Trim();
            if (strRiqiBegin == "" && strRiqiEnd != "")
            {
                lblInfo.ForeColor = System.Drawing.Color.Red;
                lblInfo.Text = "条件选择错误：请输入开始日期。";
                return;
            }
            if (strRiqiBegin != "" && strRiqiEnd == "")
            {
                lblInfo.ForeColor = System.Drawing.Color.Red;
                lblInfo.Text = "条件选择错误：请输入结束日期。";
                return;
            }
            if (strRiqiBegin != "" && strRiqiEnd != "")
            {
                sql += " and riqi>'" + strRiqiBegin + "' and riqi<'" + strRiqiEnd + "'";
            }

            string qk = ddlQukuai.SelectedItem.Value;
            if (qk != "全部")
            {
                sql += "and qukuai='" + qk + "'";
            }

            string tablename = ddlTable.SelectedItem.Value;
            if (tablename != "全部")
            {
                sql += " and biaoming='" + tablename + "'";
            }

            string yhm = tbUser.Text;
            if (yhm != "")
            {
                sql += " and yonghuming='" + yhm + "'";
            }

            DataTable dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblInfo.ForeColor = System.Drawing.Color.Black;
                lblInfo.Text = "查询到 " + dt.Rows.Count + " 条符合条件的记录。";
                GridView1.DataSource = dt;
                GridView1.DataBind();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    int n = GridView1.PageIndex * 10 + i + 1;
                    GridView1.Rows[i].Cells[0].Text = (n).ToString();
                }
            }
            else
            {
                lblInfo.ForeColor = System.Drawing.Color.Black;
                lblInfo.Text = "没有查询到符合条件的记录。";
            }
        }
    }
}