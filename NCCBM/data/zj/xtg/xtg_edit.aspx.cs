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

namespace NCCBM.data.zj.xtg
{

    public partial class xtg_edit : System.Web.UI.Page
    {
        private string tablename = "Xls_Zj_Rbb_Xtg";
        private String url = "xtg_list.aspx?Type=update";
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
            TB_wanzuanjingshen.Text = dr["wanzuanjingshen"].ToString();

            if (dr["wanzuanriqi"] == null || dr["wanzuanriqi"].ToString().Trim() == "") TB_wanzuanriqi.Text = "";
            else TB_wanzuanriqi.Text = ((DateTime)dr["wanzuanriqi"]).ToString("yyyy-MM-dd");

            TB_taoguanxiashen.Text = dr["taoguanxiashen"].ToString();
            TB_gangji.Text = dr["gangji"].ToString();
            TB_chicun.Text = dr["chicun"].ToString();
            TB_sikou.Text = dr["sikou"].ToString();
            TB_waiguan.Text = dr["waiguan"].ToString();
            TB_pingjunbihou.Text = dr["pingjunbihou"].ToString();
            TB_pingjunwaijing.Text = dr["pingjunwaijing"].ToString();
            TB_mifengzhi.Text = dr["mifengzhi"].ToString();
            TB_jingkou.Text = dr["jingkou"].ToString();
            TB_fugu.Text = dr["fukuang"].ToString();
            TB_taoguanshuju.Text = dr["taoguanshuju"].ToString();
            TB_taoguanpici.Text = dr["taoguanpici"].ToString();
            TB_cunzaiwenti.Text = dr["cunzaiwenti"].ToString();
            TB_zhenggaicuoshi.Text = dr["zhenggaicuoshi"].ToString();
            TB_beizhu.Text = dr["beizhu"].ToString();
            TB_jiancefangshi.Text = dr["jiancefangshi"].ToString();
            TB_taoguanchangjia.Text = dr["taoguanchangjia"].ToString();

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

            if (TB_wanzuanjingshen.Text == "")
            {
                sqlStr = sqlStr + "wanzuanjingshen=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_wanzuanjingshen.Text.Trim());
                    sqlStr = sqlStr + "wanzuanjingshen=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "wanzuanjingshen=NULL,";
                }
            }

            if (TB_wanzuanriqi.Text == "") sqlStr = sqlStr + "wanzuanriqi=NULL,";
            else sqlStr = sqlStr + "wanzuanriqi='" + TB_wanzuanriqi.Text + "',";

            if (TB_taoguanxiashen.Text == "")
            {
                sqlStr = sqlStr + "taoguanxiashen=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_taoguanxiashen.Text.Trim());
                    sqlStr = sqlStr + "taoguanxiashen=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "taoguanxiashen=NULL,";
                }
            }

            sqlStr = sqlStr + "gangji='" + TB_gangji.Text + "',";
            sqlStr = sqlStr + "chicun='" + TB_chicun.Text + "',";
            sqlStr = sqlStr + "sikou='" + TB_gangji.Text + "',";
            sqlStr = sqlStr + "waiguan='" + TB_waiguan.Text + "',";
            sqlStr = sqlStr + "pingjunbihou='" + TB_pingjunbihou.Text + "',";
            sqlStr = sqlStr + "pingjunwaijing='" + TB_pingjunwaijing.Text + "',";
            sqlStr = sqlStr + "mifengzhi='" + TB_mifengzhi.Text + "',";
            sqlStr = sqlStr + "jingkou='" + TB_jingkou.Text + "',";
            sqlStr = sqlStr + "fukuang='" + TB_fugu.Text + "',";
            sqlStr = sqlStr + "taoguanshuju='" + TB_taoguanshuju.Text + "',";
            sqlStr = sqlStr + "taoguanpici='" + TB_taoguanpici.Text + "',";
            sqlStr = sqlStr + "cunzaiwenti='" + TB_cunzaiwenti.Text + "',";
            sqlStr = sqlStr + "zhenggaicuoshi='" + TB_zhenggaicuoshi.Text + "',";
            sqlStr = sqlStr + "beizhu='" + TB_beizhu.Text + "',";
            sqlStr = sqlStr + "jiancefangshi='" + TB_jiancefangshi.Text + "', ";
            sqlStr = sqlStr + "taoguanchangjia='" + TB_taoguanchangjia.Text + "' ";

            sqlStr = sqlStr + " where id = " + id;
            try
            {
                DataBaseHelper.execute(sqlStr);

                String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','修改','下套管','" + jh + "','','" + qk + "','')";
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