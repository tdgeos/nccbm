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
                              

namespace NCCBM.data.zj.zj
{ 

    public partial class zj_edit : System.Web.UI.Page
    {

        private string tablename = "Xls_Zj_Rbb_Zj";
        private String url = "zj_list.aspx?Type=update";
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

            if (dr["kaizuanriqi"] == null || dr["kaizuanriqi"].ToString().Trim() == "") TB_kaizuanriqi.Text = "";
            else TB_kaizuanriqi.Text = ((DateTime)dr["kaizuanriqi"]).ToString("yyyy-MM-dd");

            TB_shejijingshen.Text = dr["shejijingshen"].ToString();
            TB_muqianjingshen.Text = dr["muqianjingshen"].ToString();
            TB_dangrijinchi.Text = dr["dangrijinchi"].ToString();
            TB_gongkuang.Text = dr["gongkuang"].ToString();
            TB_midu.Text = dr["midu"].ToString();
            TB_niandu.Text = dr["niandu"].ToString();
            TB_jingduan.Text = dr["jingduan"].ToString();
            TB_jingxie_sheji.Text = dr["jingxie_sheji"].ToString();
            TB_jingxie_shiji.Text = dr["jingxie_shiji"].ToString();
            TB_fangwei_sheji.Text = dr["fangwei_sheji"].ToString();
            TB_fangwei_shiji.Text = dr["fangwei_shiji"].ToString();
            TB_goutui_sheji.Text = dr["goutui_sheji"].ToString();
            TB_goutui_shiji.Text = dr["goutui_shiji"].ToString();
            TB_HSEqingkuang.Text = dr["HSEqingkuang"].ToString();
            TB_fuzaqingkuang.Text = dr["fuzaqingkuang"].ToString();
            TB_zhenggaineirong.Text = dr["zhenggaineirong"].ToString();
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
            sqlStr = sqlStr + "jinghao='" + TB_jinghao.Text + "',";

            if (TB_kaizuanriqi.Text == "") sqlStr = sqlStr + "kaizuanriqi=NULL,";
            else sqlStr = sqlStr + "kaizuanriqi='" + TB_kaizuanriqi.Text + "',";

            if (TB_shejijingshen.Text == "")
            {
                sqlStr = sqlStr + "shejijingshen=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_shejijingshen.Text.Trim());
                    sqlStr = sqlStr + "shejijingshen=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "shejijingshen=NULL,";
                }
            }

            if (TB_muqianjingshen.Text == "")
            {
                sqlStr = sqlStr + "muqianjingshen=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_muqianjingshen.Text.Trim());
                    sqlStr = sqlStr + "muqianjingshen=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "muqianjingshen=NULL,";
                }
            }

            if (TB_dangrijinchi.Text == "")
            {
                sqlStr = sqlStr + "dangrijinchi=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_dangrijinchi.Text.Trim());
                    sqlStr = sqlStr + "dangrijinchi=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "dangrijinchi=NULL,";
                }
            }

            sqlStr = sqlStr + "gongkuang='" + TB_gongkuang.Text + "',";
            sqlStr = sqlStr + "midu='" + TB_midu.Text + "',";
            sqlStr = sqlStr + "niandu='" + TB_niandu.Text + "',";
            sqlStr = sqlStr + "jingduan='" + TB_jingduan.Text + "',";
            sqlStr = sqlStr + "jingxie_sheji='" + TB_jingxie_sheji.Text + "',";
            sqlStr = sqlStr + "jingxie_shiji='" + TB_jingxie_shiji.Text + "',";
            sqlStr = sqlStr + "fangwei_sheji='" + TB_fangwei_sheji.Text + "',";
            sqlStr = sqlStr + "fangwei_shiji='" + TB_fangwei_shiji.Text + "',";
            sqlStr = sqlStr + "goutui_sheji='" + TB_goutui_sheji.Text + "',";
            sqlStr = sqlStr + "goutui_shiji='" + TB_goutui_shiji.Text + "',";
            sqlStr = sqlStr + "HSEqingkuang='" + TB_HSEqingkuang.Text + "',";
            sqlStr = sqlStr + "fuzaqingkuang='" + TB_fuzaqingkuang.Text + "',";
            sqlStr = sqlStr + "zhenggaineirong='" + TB_zhenggaineirong.Text + "',";
            sqlStr = sqlStr + "beizhu='" + TB_beizhu.Text + "',";
            sqlStr = sqlStr + "jiancefangshi='" + TB_jiancefangshi.Text + "' ";

            sqlStr = sqlStr + " where id = " + id;
            try
            {
                DataBaseHelper.execute(sqlStr);

                String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','修改','钻进','" + jh + "','','" + qk + "','')";
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