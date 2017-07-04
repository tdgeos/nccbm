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


namespace NCCBM.data.yl.sbyysm
{

    public partial class sbyysm_edit : System.Web.UI.Page
    {
        private string tablename = "Xls_yL_Rbb_Sbyysm";
        private string url = "sbyysm_list.aspx?Type=update";
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
            TB_cenghao.Text = dr["cenghao"].ToString();

            if (dr["yalieriqi"] == null || dr["yalieriqi"].ToString().Trim() == "") TB_yalieriqi.Text = "";
            else TB_yalieriqi.Text = ((DateTime)dr["yalieriqi"]).ToString("yyyy-MM-dd");

            TB_shigongmiaoshu.Text = dr["shigongmiaoshu"].ToString();
            TB_shibaiyuanyinfenxi.Text = dr["shibaiyuanyinfenxi"].ToString();
            TB_jiashabaifenbi.Text = dr["jiashabaifenbi"].ToString();
            TB_shigongduiwu.Text = dr["shigongduiwu"].ToString();

            qk = dr["place"].ToString();

            dr.Close();
            Mycomm.Connection.Close();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            String ch = TB_cenghao.Text.Trim();
            String jh = TB_jinghao.Text.Trim();
            if (ch == "" || jh == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('井号和层号不能为空！'); </script>");
                return;
            }

            string sqlStr = "update " + tablename + " set ";

            sqlStr = sqlStr + "jinghao='" + TB_jinghao.Text.Trim() + "',";
            sqlStr = sqlStr + "cenghao='" + TB_cenghao.Text.Trim() + "',";

            if (TB_yalieriqi.Text == "") sqlStr = sqlStr + "yalieriqi=NULL,";
            else sqlStr = sqlStr + "yalieriqi='" + TB_yalieriqi.Text.Trim() + "',";

            sqlStr = sqlStr + "shigongmiaoshu='" + TB_shigongmiaoshu.Text.Trim() + "',";
            sqlStr = sqlStr + "shibaiyuanyinfenxi='" + TB_shibaiyuanyinfenxi.Text.Trim() + "',";
            sqlStr = sqlStr + "jiashabaifenbi='" + TB_jiashabaifenbi.Text.Trim() + "',";
            sqlStr = sqlStr + "shigongduiwu='" + TB_shigongduiwu.Text.Trim() + "' ";

            sqlStr = sqlStr + " where id = " + id;

            try
            {
                DataBaseHelper.execute(sqlStr);

                String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','修改','失败原因说明','" + jh + "','" + ch + "','" + qk + "','')";
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
