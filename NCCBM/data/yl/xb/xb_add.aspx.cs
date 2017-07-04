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
    public partial class xb_add : System.Web.UI.Page
    {
        private String tablename = "Xls_YL_Rbb_Xb";
        private String url = "xb_list.aspx";

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

            if (userPlaceId == 1)
            {
                TB_qukuai.Text = "韩城";
                TB_qukuai.Enabled = false;
            }
            if (userPlaceId == 2)
            {
                TB_qukuai.Text = "临汾";
                TB_qukuai.Enabled = false;
            }
            if (userPlaceId == 3)
            {
                TB_qukuai.Text = "忻州";
                TB_qukuai.Enabled = false;
            }
        }

        protected bool VerifyExist(string name, string date)
        {
            bool bHave = false;
            SqlDataReader dtr;
            SqlConnection conMy = DBCONN.GetDBConn();
            string szQuery;
            szQuery = "select * from " + tablename + " where jinghao='" + name + "' and riqi='" + date + "'";
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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            bool bHave = VerifyExist(TB_jinghao.Text.Trim(), TB_riqi.Text.Trim());
            if (bHave)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该项目的基本情况已经存在，请重新选择！'); </script>");
            }
            else
            {
                string sqlStr = "INSERT INTO " + tablename + "(xiabengriqi,jinghao,shigongneirong,wanjingqingkuang,yanshouqingkuang,"+
                    "jingtaiqingkuang,shigongdanwei,beizhu,place,riqi) Values(";

                if (TB_xiabengriqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_xiabengriqi.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + TB_jinghao.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shigongneirong.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_wanjingqingkuang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_yanshouqingkuang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jingtaiqingkuang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shigongdanwei.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_beizhu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_qukuai.Text.Trim() + "',";

                if (TB_riqi.Text == "") sqlStr = sqlStr + "NULL)";
                else sqlStr = sqlStr + "'" + TB_riqi.Text.Trim() + "')";

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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}
