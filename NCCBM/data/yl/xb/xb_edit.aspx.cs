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

namespace NCCBM.data.yl.xb
{

    public partial class xb_edit : System.Web.UI.Page
    {
        private string tablename = "Xls_yL_Rbb_Xb";
        private string url = "xb_list.aspx";
        private string id = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string szRoleID = null;
            try
            {
                szRoleID = HttpContext.Current.Session["RoleID"].ToString();
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

            if (dr["xiabengriqi"] == null || dr["xiabengriqi"].ToString().Trim() == "") TB_xiabengriqi.Text = "";
            else TB_xiabengriqi.Text = ((DateTime)dr["xiabengriqi"]).ToString("yyyy-MM-dd");

            TB_jinghao.Text = dr["jinghao"].ToString();
            TB_shigongneirong.Text = dr["shigongneirong"].ToString();
            TB_wanjingqingkuang.Text = dr["wanjingqingkuang"].ToString();
            TB_yanshouqingkuang.Text = dr["yanshouqingkuang"].ToString();
            TB_jingtaiqingkuang.Text = dr["jingtaiqingkuang"].ToString();
            TB_shigongdanwei.Text = dr["shigongdanwei"].ToString();
            TB_beizhu.Text = dr["beizhu"].ToString();

            dr.Close();
            Mycomm.Connection.Close();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string sqlStr = "update " + tablename + " set ";

            if (TB_xiabengriqi.Text == "") sqlStr = sqlStr + "xiabengriqi=NULL,";
            else sqlStr = sqlStr + "xiabengriqi='" + TB_xiabengriqi.Text.Trim() + "',";

            sqlStr = sqlStr + "jinghao='" + TB_jinghao.Text + "',";
            sqlStr = sqlStr + "shigongneirong='" + TB_shigongneirong.Text + "',";
            sqlStr = sqlStr + "wanjingqingkuang='" + TB_wanjingqingkuang.Text + "',";
            sqlStr = sqlStr + "yanshouqingkuang='" + TB_yanshouqingkuang.Text + "',";
            sqlStr = sqlStr + "jingtaiqingkuang='" + TB_jingtaiqingkuang.Text + "', ";
            sqlStr = sqlStr + "shigongdanwei='" + TB_shigongdanwei.Text + "',";
            sqlStr = sqlStr + "beizhu='" + TB_beizhu.Text + "' ";

            sqlStr = sqlStr + " where id = " + id;

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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}
