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

namespace NCCBM.data.yl.yqjc
{

    public partial class yqjc_edit : System.Web.UI.Page
    {
        private string tablename = "Xls_yL_Rbb_Yqjc";
        private string url = "yqjc_list.aspx?Type=update";
        private string id = "";
        private String userName = null;
        private String qk = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string szRoleID = null;
            try
            {
                szRoleID = HttpContext.Current.Session["RoleID"].ToString();
                userName = HttpContext.Current.Session["LoginUserID"].ToString();
            }
            catch (NullReferenceException ne)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            string szText = Request.QueryString["iPage"];
            if (szText != null && szText != "")
            {
                url += "&iPage=" + szText;
            }
            szText = Request.QueryString["dtBegin"];
            if (szText != null && szText != "")
            {
                url += "&dateBegin=" + szText;
            }
            szText = Request.QueryString["dtEnd"];
            if (szText != null && szText != "")
            {
                url += "&dateEnd=" + szText;
            }
            szText = Request.QueryString["Qukuai"];
            if (szText != null && szText != "")
            {
                url += "&Qukuai=" + szText;
            }

            id = Request.QueryString["ID"];
            if (!Page.IsPostBack)
            {
                setTextBox();
            }
        }

        private void setTextBox()
        {
            SqlDataReader dr;
            SqlConnection Myconn = DBCONN.GetDBConn();
            SqlCommand Mycomm = new SqlCommand("select * from " + tablename + " where id = " + id, Myconn);
            Mycomm.Connection.Open();
            dr = Mycomm.ExecuteReader();
            dr.Read();

            TB_jinghao.Text = dr["jinghao"].ToString();
            TB_cengwei.Text = dr["cengwei"].ToString();
            TB_jianchariqi.Text = ((DateTime)dr["jianchariqi"]).ToString("yyyy-MM-dd");
            TB_ytjc_qzy_zd_guanding.Text = dr["ytjc_qzy_zd_guanding"].ToString();
            TB_ytjc_qzy_zd_guandi.Text = dr["ytjc_qzy_zd_guandi"].ToString();
            TB_ytjc_qzy_yd_guanding.Text = dr["ytjc_qzy_yd_guanding"].ToString();
            TB_ytjc_qzy_yd_guandi.Text = dr["ytjc_qzy_yd_guandi"].ToString();
            TB_ytjc_xsy_zd_guanding.Text = dr["ytjc_xsy_zd_guanding"].ToString();
            TB_ytjc_xsy_zd_guandi.Text = dr["ytjc_xsy_zd_guandi"].ToString();
            TB_ytjc_xsy_yd_guanding.Text = dr["ytjc_xsy_yd_guanding"].ToString();
            TB_ytjc_xsy_yd_guandi.Text = dr["ytjc_xsy_yd_guandi"].ToString();
            TB_zcjzdjc_zhongxisha.Text = dr["zcjzdjc_zhongxisha"].ToString();
            TB_zcjzdjc_zhongsha.Text = dr["zcjzdjc_zhongsha"].ToString();
            TB_zcjzdjc_cusha.Text = dr["zcjzdjc_cusha"].ToString();
            TB_hse.Text = dr["hse"].ToString();
            TB_jianduren.Text = dr["jianduren"].ToString();
            TB_shigongduiwu.Text = dr["shigongduiwu"].ToString();
            TB_beizhu.Text = dr["beizhu"].ToString();

            qk = dr["place"].ToString();

            dr.Close();
            Mycomm.Connection.Close();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            String ch = TB_cengwei.Text.Trim();
            String jh = TB_jinghao.Text.Trim();
            if (ch == "" || jh == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('井号和层号不能为空！'); </script>");
                return;
            }

            float fTemp = 0;
            string sqlStr = "update " + tablename + " set ";
            sqlStr = sqlStr + "jinghao='" + TB_jinghao.Text + "',";
            sqlStr = sqlStr + "cengwei='" + TB_cengwei.Text + "',";

            if (TB_jianchariqi.Text == "") sqlStr = sqlStr + "jianchariqi=NULL,";
            else sqlStr = sqlStr + "jianchariqi='" + TB_jianchariqi.Text.Trim() + "',";

            if (TB_ytjc_qzy_zd_guanding.Text == "")
            {
                sqlStr = sqlStr + "ytjc_qzy_zd_guanding=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_ytjc_qzy_zd_guanding.Text.Trim());
                    sqlStr = sqlStr + "ytjc_qzy_zd_guanding=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "ytjc_qzy_zd_guanding=NULL,";
                }
            }

            if (TB_ytjc_qzy_zd_guandi.Text == "")
            {
                sqlStr = sqlStr + "ytjc_qzy_zd_guandi=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_ytjc_qzy_zd_guandi.Text.Trim());
                    sqlStr = sqlStr + "ytjc_qzy_zd_guandi=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "ytjc_qzy_zd_guandi=NULL,";
                }
            }

            if (TB_ytjc_qzy_yd_guanding.Text == "")
            {
                sqlStr = sqlStr + "ytjc_qzy_yd_guanding=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_ytjc_qzy_yd_guanding.Text.Trim());
                    sqlStr = sqlStr + "ytjc_qzy_yd_guanding=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "ytjc_qzy_yd_guanding=NULL,";
                }
            }

            if (TB_ytjc_qzy_yd_guandi.Text == "")
            {
                sqlStr = sqlStr + "ytjc_qzy_yd_guandi=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_ytjc_qzy_yd_guandi.Text.Trim());
                    sqlStr = sqlStr + "ytjc_qzy_yd_guandi=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "ytjc_qzy_yd_guandi=NULL,";
                }
            }

            if (TB_ytjc_xsy_zd_guanding.Text == "")
            {
                sqlStr = sqlStr + "ytjc_xsy_zd_guanding=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_ytjc_xsy_zd_guanding.Text.Trim());
                    sqlStr = sqlStr + "ytjc_xsy_zd_guanding=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "ytjc_xsy_zd_guanding=NULL,";
                }
            }

            if (TB_ytjc_xsy_zd_guandi.Text == "")
            {
                sqlStr = sqlStr + "ytjc_xsy_zd_guandi=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_ytjc_xsy_zd_guandi.Text.Trim());
                    sqlStr = sqlStr + "ytjc_xsy_zd_guandi=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "ytjc_xsy_zd_guandi=NULL,";
                }
            }

            if (TB_ytjc_xsy_yd_guanding.Text == "")
            {
                sqlStr = sqlStr + "ytjc_xsy_yd_guanding=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_ytjc_xsy_yd_guanding.Text.Trim());
                    sqlStr = sqlStr + "ytjc_xsy_yd_guanding=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "ytjc_xsy_yd_guanding=NULL,";
                }
            }

            if (TB_ytjc_xsy_yd_guandi.Text == "")
            {
                sqlStr = sqlStr + "ytjc_xsy_yd_guandi=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_ytjc_xsy_yd_guandi.Text.Trim());
                    sqlStr = sqlStr + "ytjc_xsy_yd_guandi=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "ytjc_xsy_yd_guandi=NULL,";
                }
            }

            sqlStr = sqlStr + "zcjzdjc_zhongxisha='" + TB_zcjzdjc_zhongxisha.Text + "',";
            sqlStr = sqlStr + "zcjzdjc_zhongsha='" + TB_zcjzdjc_zhongsha.Text + "',";
            sqlStr = sqlStr + "zcjzdjc_cusha='" + TB_zcjzdjc_cusha.Text + "',";
            sqlStr = sqlStr + "hse='" + TB_hse.Text + "',";

            sqlStr = sqlStr + "jianduren='" + TB_jianduren.Text + "',";
            sqlStr = sqlStr + "shigongduiwu='" + TB_shigongduiwu.Text + "',";
            sqlStr = sqlStr + "beizhu='" + TB_beizhu.Text + "' ";

            sqlStr = sqlStr + " where id = " + id;

            try
            {
                DataBaseHelper.execute(sqlStr);

                String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','修改','压裂检查','" + jh + "','" + ch + "','" + qk + "','')";
                DataBaseHelper.execute(sqlLog);

                Response.Redirect(url);
            }
            catch (Exception e2)
            {
                return;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}
