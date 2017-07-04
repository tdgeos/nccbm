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


namespace NCCBM.data.yl.sk
{

    public partial class sk_edit : System.Web.UI.Page
    {
        private string tablename = "Xls_yL_Rbb_Sk";
        private string url = "sk_list.aspx?Type=update";
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

            if (dr["shekongriqi"] == null || dr["shekongriqi"].ToString().Trim() == "") TB_shekongriqi.Text = "";
            else TB_shekongriqi.Text = ((DateTime)dr["shekongriqi"]).ToString("yyyy-MM-dd");

            TB_shekongjingduan.Text = dr["shekongjingduan"].ToString();
            TB_shekaihoudu.Text = dr["shekaihoudu"].ToString();
            TB_shejidanshu.Text = dr["shejidanshu"].ToString();
            TB_shifadanshu.Text = dr["shifadanshu"].ToString();
            TB_ruantanshamian.Text = dr["ruantanshamian"].ToString();
            TB_yingtanshamian.Text = dr["yingtanshamian"].ToString();
            TB_rengongjingdi.Text = dr["rengongjingdi"].ToString();
            TB_jianduren.Text = dr["jianduren"].ToString();
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

            string sqlStr = "update " + tablename + " set ";
            sqlStr = sqlStr + "jinghao='" + TB_jinghao.Text + "',";
            sqlStr = sqlStr + "cengwei='" + TB_cengwei.Text + "',";

            if (TB_shekongriqi.Text == "") sqlStr = sqlStr + "shekongriqi=NULL,";
            else sqlStr = sqlStr + "shekongriqi='" + TB_shekongriqi.Text.Trim() + "',";

            sqlStr = sqlStr + "shekongjingduan='" + TB_shekongjingduan.Text + "',";
            sqlStr = sqlStr + "shekaihoudu='" + TB_shekaihoudu.Text + "', ";
            sqlStr = sqlStr + "shejidanshu='" + TB_shejidanshu.Text + "',";
            sqlStr = sqlStr + "shifadanshu='" + TB_shifadanshu.Text + "',";
            sqlStr = sqlStr + "ruantanshamian='" + TB_ruantanshamian.Text + "',";
            sqlStr = sqlStr + "yingtanshamian='" + TB_yingtanshamian.Text + "',";
            sqlStr = sqlStr + "rengongjingdi='" + TB_rengongjingdi.Text + "',";
            sqlStr = sqlStr + "jianduren='" + TB_jianduren.Text + "',";
            sqlStr = sqlStr + "beizhu='" + TB_beizhu.Text + "' ";

            sqlStr = sqlStr + " where id = " + id;

            try
            {
                DataBaseHelper.execute(sqlStr);

                String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','修改','射孔','" + jh + "','" + ch + "','" + qk + "','')";
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
