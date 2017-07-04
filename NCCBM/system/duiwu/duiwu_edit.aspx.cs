using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace NCCBM.system.duiwu
{
    public partial class duiwu_edit : System.Web.UI.Page
    {
        private string tablename = "T_System_Duiwu";
        private string url = "duiwu_list.aspx?Type=list";
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
            szText = Request.QueryString["strName"];
            if (szText != null && szText != "")
            {
                url += "&strName=" + szText;
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

            TB_name.Text = dr["name"].ToString();
            TB_jiancheng.Text = dr["jiancheng"].ToString();
            TB_zizhi.Text = dr["zizhi"].ToString();
            TB_guimo.Text = dr["guimo"].ToString();
            TB_dianhua.Text = dr["dianhua"].ToString();
            TB_youxiang.Text = dr["youxiang"].ToString();
            TB_didian.Text = dr["bangongdidian"].ToString();
            TB_qukuai.Text = dr["qukuai"].ToString();
            TB_zjoryl.Text = dr["zjoryl"].ToString();
            TB_beizhu.Text = dr["beizhu"].ToString();

            dr.Close();
            Mycomm.Connection.Close();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string sqlStr = "update " + tablename + " set ";

            sqlStr = sqlStr + "name='" + TB_name.Text.Trim() + "',";
            sqlStr = sqlStr + "jiancheng='" + TB_jiancheng.Text.Trim() + "',";
            sqlStr = sqlStr + "zizhi='" + TB_zizhi.Text.Trim() + "',";
            sqlStr = sqlStr + "guimo='" + TB_guimo.Text.Trim() + "',";
            sqlStr = sqlStr + "dianhua='" + TB_dianhua.Text.Trim() + "',";
            sqlStr = sqlStr + "youxiang='" + TB_youxiang.Text.Trim() + "',";
            sqlStr = sqlStr + "bangongdidian='" + TB_didian.Text.Trim() + "',";
            sqlStr = sqlStr + "qukuai='" + TB_qukuai.Text.Trim() + "',";
            sqlStr = sqlStr + "zjoryl='" + TB_zjoryl.Text.Trim() + "',";
            sqlStr = sqlStr + "beizhu='" + TB_beizhu.Text.Trim() + "'";

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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}