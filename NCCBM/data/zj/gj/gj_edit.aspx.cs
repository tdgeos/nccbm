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


namespace NCCBM.data.zj.gj
{

    public partial class gj_edit : System.Web.UI.Page
    {
        private string tablename = "Xls_Zj_Rbb_Gj";
        private string url = "gj_list.aspx?Type=update";
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

            if (dr["shigongriqi"] == null || dr["shigongriqi"].ToString().Trim() == "") TB_shigongriqi.Text = "";
            else TB_shigongriqi.Text = ((DateTime)dr["shigongriqi"]).ToString("yyyy-MM-dd");

            TB_fangao_sheji.Text = dr["fangao_sheji"].ToString();
            TB_fangao_shiji.Text = dr["fangao_shiji"].ToString();
            TB_midu_sheji.Text = dr["midu_sheji"].ToString();
            TB_midu_shiji.Text = dr["midu_shiji"].ToString();
            TB_jiangyongliang_sheji.Text = dr["jiangyongliang_sheji"].ToString();
            TB_jiangyongliang_shiji.Text = dr["jiangyongliang_shiji"].ToString();
            TB_yongliang_sheji.Text = dr["yongliang_sheji"].ToString();
            TB_yongliang_shiji.Text = dr["yongliang_shiji"].ToString();
            TB_dingtiliang_sheji.Text = dr["dingtiliang_sheji"].ToString();
            TB_dingtiliang_shiji.Text = dr["dingtiliang_shiji"].ToString();
            TB_pengya_sheji.Text = dr["pengya_sheji"].ToString();
            TB_pengya_shiji.Text = dr["pengya_shiji"].ToString();
            TB_shigongwenti.Text = dr["shigongwenti"].ToString();
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

            string sqlStr = "update " + tablename + " set ";

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

            if (TB_shigongriqi.Text == "") sqlStr = sqlStr + "shigongriqi=NULL,";
            else sqlStr = sqlStr + "shigongriqi='" + TB_shigongriqi.Text + "',";

            sqlStr = sqlStr + "fangao_sheji='" + TB_fangao_sheji.Text + "',";
            sqlStr = sqlStr + "fangao_shiji='" + TB_fangao_shiji.Text + "',";
            sqlStr = sqlStr + "midu_sheji='" + TB_midu_sheji.Text + "',";
            sqlStr = sqlStr + "midu_shiji='" + TB_midu_shiji.Text + "',";
            sqlStr = sqlStr + "jiangyongliang_sheji='" + TB_jiangyongliang_sheji.Text + "',";
            sqlStr = sqlStr + "jiangyongliang_shiji='" + TB_jiangyongliang_shiji.Text + "',";
            sqlStr = sqlStr + "yongliang_sheji='" + TB_yongliang_sheji.Text + "',";
            sqlStr = sqlStr + "yongliang_shiji='" + TB_yongliang_shiji.Text + "',";
            sqlStr = sqlStr + "dingtiliang_sheji='" + TB_dingtiliang_sheji.Text + "',";
            sqlStr = sqlStr + "dingtiliang_shiji='" + TB_dingtiliang_shiji.Text + "',";
            sqlStr = sqlStr + "pengya_sheji='" + TB_pengya_sheji.Text + "',";
            sqlStr = sqlStr + "pengya_shiji='" + TB_pengya_shiji.Text + "',";
            sqlStr = sqlStr + "shigongwenti='" + TB_shigongwenti.Text + "',";
            sqlStr = sqlStr + "fuzaqingkuang='" + TB_fuzaqingkuang.Text + "',";
            sqlStr = sqlStr + "beizhu='" + TB_beizhu.Text + "',";
            sqlStr = sqlStr + "jiancefangshi='" + TB_jiancefangshi.Text + "' ";

            sqlStr = sqlStr + " where id = " + id;
            try
            {
                DataBaseHelper.execute(sqlStr);

                String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','修改','固井','" + jh + "','','" + qk + "','')";
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
