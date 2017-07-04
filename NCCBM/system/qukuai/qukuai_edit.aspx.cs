using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace NCCBM.system.qukuai
{
    public partial class qukuai_edit : System.Web.UI.Page
    {
        private string tablename = "T_System_Qukuai";
        private string url = "qukuai_list.aspx?Type=list";
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

            TB_mingcheng.Text = dr["mingcheng"].ToString();
            TB_weizhi.Text = dr["weizhi"].ToString();
            TB_jianjie.Text = dr["jianjie"].ToString();
            TB_beizhu.Text = dr["beizhu"].ToString();

            dr.Close();
            Mycomm.Connection.Close();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string sqlStr = "update " + tablename + " set ";

            sqlStr = sqlStr + "mingcheng='" + TB_mingcheng.Text.Trim() + "',";
            sqlStr = sqlStr + "weizhi='" + TB_weizhi.Text.Trim() + "',";
            sqlStr = sqlStr + "jianjie='" + TB_jianjie.Text.Trim() + "',";
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