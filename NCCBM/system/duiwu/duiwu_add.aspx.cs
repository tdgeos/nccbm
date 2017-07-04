using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace NCCBM.system.duiwu
{
    public partial class duiwu_add : System.Web.UI.Page
    {
        private String tablename = "T_System_Duiwu";
        private String url = "duiwu_list.aspx?Type=list";

        protected void Page_Load(object sender, EventArgs e)
        {
            int userPlaceId = 0;
            try
            {
                userPlaceId = Int32.Parse(HttpContext.Current.Session["PlaceID"].ToString());
            }
            catch (NullReferenceException ne)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            bool bHave = VerifyExist(TB_name.Text.Trim());
            if (bHave)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该项目的基本情况已经存在，请重新选择！'); </script>");
            }
            else
            {
                string sqlStr = "INSERT INTO  " + tablename + "(name,jiancheng,zizhi,guimo,dianhua,youxiang,bangongdidian,qukuai,zjoryl,beizhu) Values(";
                sqlStr = sqlStr + "'" + TB_name.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jiancheng.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_zizhi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_guimo.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_dianhua.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_youxiang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_didian.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_qukuai.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_zjoryl.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_beizhu.Text.Trim() + "')";

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
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }

        protected bool VerifyExist(string name)
        {
            bool bHave = false;
            SqlDataReader dtr;
            SqlConnection conMy = DBCONN.GetDBConn();
            string szQuery;
            szQuery = "select * from " + tablename + " where name='" + name + "'";
            SqlCommand cmdMy = new SqlCommand(szQuery, conMy);
            cmdMy.Connection.Open();
            dtr = cmdMy.ExecuteReader();
            if (dtr.Read())
            {
                bHave = true;
            }
            cmdMy.Dispose();
            conMy.Close();
            return bHave;
        }
    }
}