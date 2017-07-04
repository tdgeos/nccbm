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
using System.Data.SqlClient;

namespace NCCBM.data.Report.jszl
{
    public partial class jszl_add : System.Web.UI.Page
    {
        private String tablename = "Report_jingshenzhiliang";
        private String url = "jszl_list.aspx?Type=list";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void addnew_Click(object sender, EventArgs e)
        {
            String rq = TB_lururiqi.Text.Trim();
            String jh = TB_jinghao.Text.Trim();
            String qk = TB_qukuai.Text.Trim();
            if (rq == "" || jh == "" || qk == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('井号、日期和区块名称不能为空！'); </script>");
                return;
            }

            //判断名称是否相同
            bool bHave = VerifyExist(TB_jinghao.Text, TB_lururiqi.Text);
            if (bHave)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该项目的基本情况已经存在，请重新选择！'); </script>");
            }
            else
            {
                string sqlStr = "INSERT INTO " + tablename + "(lururiqi,qukuai,jinghao,shigongduiwu,jiandu,yinsu," +
                    "cuoshi,beizhu) Values(";
                if (TB_lururiqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_lururiqi.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + TB_qukuai.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jinghao.Text.Trim() + "',";

                if (TB_shigongduiwu.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_shigongduiwu.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + TB_jiandu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_yinsu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_cuoshi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_beizhu.Text.Trim() + "')";
                try
                {
                    DataBaseHelper.execute(sqlStr);
                    Response.Redirect(url);
                }
                catch (Exception e2)
                {
                    return;
                }
            }
        }

        protected bool VerifyExist(string name, string date)
        {
            bool bHave = false;
            SqlDataReader dtr;
            SqlConnection conMy = DBCONN.GetDBConn();
            string szQuery;
            szQuery = "select * from " + tablename + " where jinghao='" + name + "' and lururiqi='" + date + "'";
            SqlCommand cmdMy = new SqlCommand(szQuery, conMy);
            cmdMy.Connection.Open();
            dtr = cmdMy.ExecuteReader();
            if (dtr.Read())
            {
                bHave = true;
            }
            cmdMy.Dispose();
            conMy.Close();
            return bHave;
        }

        protected void btnreturn_Click1(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}