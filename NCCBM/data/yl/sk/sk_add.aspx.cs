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
    public partial class sk_add : System.Web.UI.Page
    {
        private String tablename = "Xls_YL_Rbb_Sk";
        private String url = "sk_list.aspx?Type=list";
        private String userName = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            int userPlaceId = 0;
            try
            {
                userPlaceId = Int32.Parse(HttpContext.Current.Session["PlaceID"].ToString());
                userName = HttpContext.Current.Session["LoginUserID"].ToString();
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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            String ch = TB_cengwei.Text.Trim();
            String jh = TB_jinghao.Text.Trim();
            String qk = TB_qukuai.Text.Trim();
            if (ch == "" || jh == "" || qk == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('井号、层号和区块名称不能为空！'); </script>");
                return;
            }

            bool bHave = VerifyExist(TB_jinghao.Text.Trim());
            if (bHave)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该项目的基本情况已经存在，请重新选择！'); </script>");
            }
            else
            {
                string sqlStr = "INSERT INTO " + tablename + "(jinghao,cengwei,shekongriqi,shekongjingduan,shekaihoudu," +
                    "shejidanshu,shifadanshu,ruantanshamian,yingtanshamian,rengongjingdi,jianduren,beizhu,place) Values(";

                sqlStr = sqlStr + "'" + TB_jinghao.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_cengwei.Text.Trim() + "',";

                if (TB_shekongriqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_shekongriqi.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + TB_shekongjingduan.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shekaihoudu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shejidanshu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shifadanshu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_ruantanshamian.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_yingtanshamian.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_rengongjingdi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jianduren.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_beizhu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_qukuai.Text.Trim() + "')";

                try
                {
                    DataBaseHelper.execute(sqlStr);

                    String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                    string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','添加','射孔','" + jh + "','" + ch + "','" + qk + "','')";
                    DataBaseHelper.execute(sqlLog);

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

        protected bool VerifyExist(string name)
        {
            bool bHave = false;
            SqlDataReader dtr;
            SqlConnection conMy = DBCONN.GetDBConn();
            string szQuery;
            szQuery = "select * from " + tablename + " where jinghao='" + name + "'";
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