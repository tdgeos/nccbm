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

namespace NCCBM.data.zj.wj
{

    public partial class wj_edit : System.Web.UI.Page
    {
        private string tablename = "Xls_Zj_Rbb_Wj";
        private string url = "wj_list.aspx?Type=update";
        private string id = "";
        private string szRoleID = null;
        private String userName = null;
        private String qk = null;

        protected void Page_Load(object sender, EventArgs e)
        {
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

        void setTextBox()
        {
            SqlDataReader dr;
            SqlConnection Myconn = DBCONN.GetDBConn();
            SqlCommand Mycomm = new SqlCommand("select * from " + tablename + " where id = " + id, Myconn);
            Mycomm.Connection.Open();
            dr = Mycomm.ExecuteReader();
            dr.Read();
            TB_fuzejiandu.Text = dr["fuzejiandu"].ToString();
            TB_duihao.Text = dr["duihao"].ToString();
            TB_jinghao.Text = dr["jinghao"].ToString();
            TB_biaozhiguan_sheji.Text = dr["biaozhiguan_sheji"].ToString();
            TB_biaozhiguan_shiji.Text = dr["biaozhiguan_shiji"].ToString();
            TB_rengongjingdi_sheji.Text = dr["rengongjingdi_sheji"].ToString();
            TB_rengongjingdi_shiji.Text = dr["rengongjingdi_shiji"].ToString();
            TB_gujingzhiliangCBL.Text = dr["gujingzhiliangCBL"].ToString();
            TB_gujingzhiliangVDL.Text = dr["gujingzhiliangVDL"].ToString();
            TB_shuipingpiancha_hor.Text = dr["shuipingpiancha_bzh"].ToString();
            TB_shuipingpiancha_hor.Text = dr["shuipingpiancha_hor"].ToString();
            TB_shuipingpiancha_ver.Text = dr["shuipingpiancha_ver"].ToString();
            TB_shiya_start.Text = dr["shiya_start"].ToString();
            TB_shiya_end.Text = dr["shiya_end"].ToString();
            TB_gangbanhanjie_up.Text = dr["gangbanhanjie_up"].ToString();
            TB_gangbanhanjie_down.Text = dr["gangbanhanjie_down"].ToString();
            TB_jingkougaodu.Text = dr["jingkougaodu"].ToString();
            TB_shikouwanhao.Text = dr["shikouwanhao"].ToString();
            TB_cunzaiwenti.Text = dr["cunzaiwenti"].ToString();
            TB_fuzaqingkuang.Text = dr["fuzaqingkuang"].ToString();
            TB_beizhu.Text = dr["beizhu"].ToString();
            TB_jiancefangshi.Text = dr["jiancefangshi"].ToString();

            qk = dr["place"].ToString();

            dr.Close();
            Mycomm.Connection.Close();
        }

        protected void addnew_Click(object sender, EventArgs e)
        {
            String jd = TB_fuzejiandu.Text.Trim();
            String dh = TB_duihao.Text.Trim();
            String jh = TB_jinghao.Text.Trim();
            if (jd == "" || dh == "" || jh == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('负责监督、队号、井号不能为空！'); </script>");
                return;
            }

            string sqlStr = "UPDATE " + tablename + " set ";

            float fTemp;
            sqlStr = sqlStr + "fuzejiandu='" + TB_fuzejiandu.Text + "',";
            sqlStr = sqlStr + "duihao='" + TB_duihao.Text + "',";
            sqlStr = sqlStr + "jinghao='" + TB_jinghao.Text + "',";
            sqlStr = sqlStr + "biaozhiguan_sheji='" + TB_biaozhiguan_sheji.Text + "',";
            sqlStr = sqlStr + "biaozhiguan_shiji='" + TB_biaozhiguan_shiji.Text + "',";
            sqlStr = sqlStr + "rengongjingdi_sheji='" + TB_rengongjingdi_sheji.Text + "',";
            sqlStr = sqlStr + "rengongjingdi_shiji='" + TB_rengongjingdi_shiji.Text + "',";
            sqlStr = sqlStr + "gujingzhiliangCBL='" + TB_gujingzhiliangCBL.Text + "',";
            sqlStr = sqlStr + "gujingzhiliangVDL='" + TB_gujingzhiliangVDL.Text + "',";
            sqlStr = sqlStr + "shuipingpiancha_bzh='" + TB_shuipingpiancha_bzh.Text + "',";
            sqlStr = sqlStr + "shuipingpiancha_hor='" + TB_shuipingpiancha_hor.Text + "',";
            sqlStr = sqlStr + "shuipingpiancha_ver='" + TB_shuipingpiancha_ver.Text + "',";
            sqlStr = sqlStr + "shiya_start='" + TB_shiya_start.Text + "',";
            sqlStr = sqlStr + "shiya_end='" + TB_shiya_end.Text + "',";
            sqlStr = sqlStr + "gangbanhanjie_up='" + TB_gangbanhanjie_up.Text + "',";
            sqlStr = sqlStr + "gangbanhanjie_down='" + TB_gangbanhanjie_down.Text + "',";

            if (TB_jingkougaodu.Text == "")
            {
                sqlStr = sqlStr + "jingkougaodu=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_jingkougaodu.Text.Trim());
                    sqlStr = sqlStr + "jingkougaodu=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "jingkougaodu=NULL,";
                }
            }

            sqlStr = sqlStr + "shikouwanhao='" + TB_shikouwanhao.Text + "',";
            sqlStr = sqlStr + "cunzaiwenti='" + TB_cunzaiwenti.Text + "',";
            sqlStr = sqlStr + "fuzaqingkuang='" + TB_fuzaqingkuang.Text + "',";
            sqlStr = sqlStr + "beizhu='" + TB_beizhu.Text + "',";
            sqlStr = sqlStr + "jiancefangshi='" + TB_jiancefangshi.Text + "' ";

            sqlStr = sqlStr + " where id = " + id;
            try
            {
                DataBaseHelper.execute(sqlStr);

                String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','修改','完井','" + jh + "','','" + qk + "','')";
                DataBaseHelper.execute(sqlLog);

                Response.Redirect(url);
            }
            catch (Exception e2)
            {
                return;
            }
        }

        protected void btnreturn_Click1(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}


