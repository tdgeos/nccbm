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

namespace NCCBM.data.yl.ylsg
{
    public partial class ylsg_edit : System.Web.UI.Page
    {
        private string tablename = "Xls_Yl_Rbb_Ylsg";
        private string url = "ylsg_list.aspx?Type=update";
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
            SqlCommand Mycomm = new SqlCommand("select * from " + tablename + " where id = '" + id + "'", Myconn);
            Mycomm.Connection.Open();
            dr = Mycomm.ExecuteReader();
            dr.Read();
            TB_jinghao.Text = dr["jinghao"].ToString();
            TB_cengwei.Text = dr["cengwei"].ToString();

            if (dr["riqi"] == null || dr["shigongriqi"].ToString().Trim() == "") TB_shigongriqi.Text = "";
            else TB_shigongriqi.Text = ((DateTime)dr["shigongriqi"]).ToString("yyyy-MM-dd");

            TB_meicengjingduan_dingjie.Text = dr["mcjd_dingjie"].ToString();
            TB_meicengjingduan_dijie.Text = dr["mcjd_dijie"].ToString();
            TB_meicenghoudu.Text = dr["meicenghoudu"].ToString();
            TB_shekongjingduan_dingjie.Text = dr["skjd_dingjie"].ToString();
            TB_shekongjingduan_dijie.Text = dr["skjd_dijie"].ToString();
            TB_shekaihoudu.Text = dr["shekaihoudu"].ToString();
            TB_yalieyeleixing.Text = dr["yalieyeleix"].ToString();
            TB_qianzhiye_sheji.Text = dr["qzy_sheji"].ToString();
            TB_qianzhiye_shiji.Text = dr["qzy_shiji"].ToString();
            TB_xieshaye_sheji.Text = dr["xsy_sheji"].ToString();
            TB_xieshaye_shiji.Text = dr["xsy_shiji"].ToString();
            TB_xieshaye_zdyl.Text = dr["xsy_zuidiyali"].ToString();
            TB_xieshaye_zgyl.Text = dr["xsy_zuigaoyali"].ToString();
            TB_xieshaye_pjyl.Text = dr["xsy_pingjunyali"].ToString();
            TB_xieshaye_pjpl.Text = dr["xsy_pingjunpailiang"].ToString();
            TB_dingtiye_sheji.Text = dr["dty_sheji"].ToString();
            TB_dingtiye_shiji.Text = dr["dty_shiji"].ToString();
            TB_zongyeliang_sheji.Text = dr["zyl_sheji"].ToString();
            TB_zongyeliang_shiji.Text = dr["zyl_shiji"].ToString();
            TB_jsl_shejizhongxisha.Text = dr["jsl_shejizhongxisha"].ToString();
            TB_jsl_shejizhongsha.Text = dr["jsl_shejizhongsha"].ToString();
            TB_jsl_shejicusha.Text = dr["jsl_shejicusha"].ToString();
            TB_jsl_shejizongshaliang.Text = dr["jsl_shejizongshaliang"].ToString();
            TB_jsl_shijizhongxisha.Text = dr["jsl_shijizhongxisha"].ToString();
            TB_jsl_shijizhongsha.Text = dr["jsl_shijizhongsha"].ToString();
            TB_jsl_shijicusha.Text = dr["jsl_shijicusha"].ToString();
            TB_jsl_shijizongshaliang.Text = dr["jsl_shijizongshaliang"].ToString();
            TB_pingjunshabi_sheji.Text = dr["pjsb_sheji"].ToString();
            TB_pingjunshabi_shiji.Text = dr["pjsb_shiji"].ToString();
            TB_polieyali.Text = dr["polieyali"].ToString();
            TB_tingbengyali.Text = dr["tingbengyali"].ToString();
            TB_30fenzhong.Text = dr["dang30miaohoujiangzhi"].ToString();
            TB_shifouhege.Text = dr["shifouhege"].ToString();
            TB_jianduren.Text = dr["jianduren"].ToString();
            TB_shigongduiwu.Text = dr["shigongduiwu"].ToString();
            TB_qukuai.Text = dr["qukuai"].ToString();
            TB_shifouyawan.Text = dr["shifouyawan"].ToString();
            TB_shigongleixing.Text = dr["shigongleixing"].ToString();
            TB_wanchengbaifenbi.Text = dr["wanchengbaifenbi"].ToString();
            TB_teshuqingkuang.Text = dr["teshuqingkshuoming"].ToString();
            TB_beizhu.Text = dr["beizhu"].ToString();

            qk = dr["place"].ToString();

            dr.Close();
            Mycomm.Connection.Close();
        }

        protected void addnew_Click(object sender, EventArgs e)
        {
            String ch = TB_cengwei.Text.Trim();
            String jh = TB_jinghao.Text.Trim();
            if (ch == "" || jh == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('井号和层号不能为空！'); </script>");
                return;
            }

            string sqlStr = "";
            sqlStr = "UPDATE " + tablename + " set ";
            sqlStr += "jinghao='" + TB_jinghao.Text + "',";
            sqlStr += "cengwei='" + TB_cengwei.Text + "',";

            if (TB_shigongriqi.Text == "") sqlStr = sqlStr + "shigongriqi=NULL,";
            else sqlStr = sqlStr + "shigongriqi='" + TB_shigongriqi.Text + "',";

            sqlStr += "mcjd_dingjie='" + TB_meicengjingduan_dingjie.Text + "',";
            sqlStr += "mcjd_dijie='" + TB_meicengjingduan_dijie.Text + "',";
            sqlStr += "meicenghoudu='" + TB_meicenghoudu.Text + "',";
            sqlStr += "skjd_dingjie='" + TB_shekongjingduan_dingjie.Text + "',";
            sqlStr += "skjd_dijie='" + TB_shekongjingduan_dijie.Text + "',";
            sqlStr += "shekaihoudu='" + TB_shekaihoudu.Text + "',";
            sqlStr += "yalieyeleix='" + TB_yalieyeleixing.Text + "',";
            sqlStr += "qzy_sheji='" + TB_qianzhiye_sheji.Text + "',";
            sqlStr += "qzy_shiji='" + TB_qianzhiye_shiji.Text + "',";
            sqlStr += "xsy_sheji='" + TB_xieshaye_sheji.Text + "',";
            sqlStr += "xsy_shiji='" + TB_xieshaye_shiji.Text + "',";
            sqlStr += "xsy_zuidiyali='" + TB_xieshaye_zdyl.Text + "',";
            sqlStr += "xsy_zuigaoyali='" + TB_xieshaye_zgyl.Text + "',";
            sqlStr += "xsy_pingjunyali='" + TB_xieshaye_pjyl.Text + "',";
            sqlStr += "xsy_pingjunpailiang='" + TB_xieshaye_pjpl.Text + "',";
            sqlStr += "dty_sheji='" + TB_dingtiye_sheji.Text + "',";
            sqlStr += "dty_shiji='" + TB_dingtiye_shiji.Text + "',";
            sqlStr += "zyl_sheji='" + TB_zongyeliang_sheji.Text + "',";
            sqlStr += "zyl_shiji='" + TB_zongyeliang_shiji.Text + "',";
            sqlStr += "jsl_shejizhongxisha='" + TB_jsl_shejizhongxisha.Text + "',";
            sqlStr += "jsl_shejizhongsha='" + TB_jsl_shejizhongsha.Text + "',";
            sqlStr += "jsl_shejicusha='" + TB_jsl_shejicusha.Text + "',";
            sqlStr += "jsl_shejizongshaliang='" + TB_jsl_shejizongshaliang.Text + "',";
            sqlStr += "jsl_shijizhongxisha='" + TB_jsl_shijizhongxisha.Text + "',";
            sqlStr += "jsl_shijizhongsha='" + TB_jsl_shijizhongsha.Text + "',";
            sqlStr += "jsl_shijicusha='" + TB_jsl_shijicusha.Text + "',";
            sqlStr += "jsl_shijizongshaliang='" + TB_jsl_shijizongshaliang.Text + "',";
            sqlStr += "pjsb_sheji='" + TB_pingjunshabi_sheji.Text + "',";
            sqlStr += "pjsb_shiji='" + TB_pingjunshabi_shiji.Text + "',";
            sqlStr += "polieyali='" + TB_polieyali.Text + "',";
            sqlStr += "tingbengyali='" + TB_tingbengyali.Text + "',";
            sqlStr += "dang30miaohoujiangzhi='" + TB_30fenzhong.Text + "',";
            sqlStr += "shifouhege='" + TB_shifouhege.Text + "',";
            sqlStr += "jianduren='" + TB_jianduren.Text + "',";
            sqlStr += "shigongduiwu='" + TB_shigongduiwu.Text + "',";
            sqlStr += "qukuai='" + TB_qukuai.Text + "',";
            sqlStr += "shifouyawan='" + TB_shifouyawan.Text + "',";
            sqlStr += "shigongleixing='" + TB_shigongleixing.Text + "',";
            sqlStr += "wanchengbaifenbi='" + TB_wanchengbaifenbi.Text + "',";
            sqlStr += "teshuqingkshuoming='" + TB_teshuqingkuang.Text + "',";
            sqlStr += "beizhu='" + TB_beizhu.Text + "' ";

            sqlStr = sqlStr + " where id = " + id;
            try
            {
                DataBaseHelper.execute(sqlStr);

                String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','修改','压裂施工','" + jh + "','" + ch + "','" + qk + "','')";
                DataBaseHelper.execute(sqlLog);

                Response.Redirect(url);
            }
            catch (Exception e2)
            {
                return;
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}
