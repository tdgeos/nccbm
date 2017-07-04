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

namespace NCCBM.data.Report.jsyj
{
    public partial class jsyj_add : System.Web.UI.Page
    {
        private String tablename = "jianbaoluru";
        private String url = "jsyj_list.aspx?Type=list";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            String rq = txtRiqi.Text.Trim();
            if (rq == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('日期不能为空！'); </script>");
                return;
            }

            //判断名称是否相同
            bool bHave = VerifyExist(txtRiqi.Text);
            if (bHave)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该项目的基本情况已经存在，请重新选择！'); </script>");
            }
            else
            {
                string sqlStr = "INSERT INTO " + tablename + "(lururiqi,hancheng,linfen,xinzhou) Values(";
                if (txtRiqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + txtRiqi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + txt_report_hc.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + txt_report_lf.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + txt_report_xz.Text.Trim() + "')";

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

        protected bool VerifyExist(string date)
        {
            bool bHave = false;
            SqlDataReader dtr;
            SqlConnection conMy = DBCONN.GetDBConn();
            string szQuery;
            szQuery = "select * from " + tablename + " where lururiqi='" + date + "'";
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}