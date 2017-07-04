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
    public partial class gj_add : System.Web.UI.Page
    {
        private String tablename = "Xls_Zj_Rbb_Gj";
        private String url = "gj_list.aspx?Type=list";
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
        protected void addnew_Click(object sender, EventArgs e)
        {
            String jd = TB_fuzejiandu.Text.Trim();
            String dh = TB_duihao.Text.Trim();
            String jh = TB_jinghao.Text.Trim();
            String qk = TB_qukuai.Text.Trim();
            String rq = TB_riqi.Text.Trim();
            if (jd == "" || dh == "" || jh == "" || qk == "" || rq == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('负责监督、队号、井号、区块和日期不能为空！'); </script>");
                return;
            }


            bool bHave = VerifyExist(TB_jinghao.Text.Trim(), TB_riqi.Text.Trim());
            if (bHave)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该项目的基本情况已经存在，请重新选择！'); </script>");
            }
            else
            {
                string sqlStr = "INSERT INTO " + tablename + "(fuzejiandu,duihao,jinghao,wanzuanjingshen,shigongriqi,fangao_sheji,"+
                    "fangao_shiji,midu_sheji,midu_shiji,jiangyongliang_sheji,jiangyongliang_shiji,yongliang_sheji,"+
                    "yongliang_shiji,dingtiliang_sheji,dingtiliang_shiji,pengya_sheji,pengya_shiji,shigongwenti,"+
                    "fuzaqingkuang,beizhu,jiancefangshi,place,riqi) Values(";

                float fTemp;
                sqlStr = sqlStr + "'" + TB_fuzejiandu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_duihao.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jinghao.Text.Trim() + "',";      

                if (TB_wanzuanjingshen.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_wanzuanjingshen.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_shigongriqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_shigongriqi.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + TB_fangao_sheji.Text.Trim() + "',";   
                sqlStr = sqlStr + "'" + TB_fangao_shiji.Text.Trim() + "',";   
                sqlStr = sqlStr + "'" + TB_midu_sheji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_midu_shiji.Text.Trim() + "',";   
                sqlStr = sqlStr + "'" + TB_jiangyongliang_sheji.Text.Trim() + "',";   
                sqlStr = sqlStr + "'" + TB_jiangyongliang_shiji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_yongliang_sheji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_yongliang_shiji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_dingtiliang_sheji.Text.Trim() + "',";  
                sqlStr = sqlStr + "'" + TB_dingtiliang_shiji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_pengya_sheji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_pengya_shiji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shigongwenti.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_fuzaqingkuang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_beizhu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jiancefangshi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_qukuai.Text.Trim() + "',";

                if (TB_riqi.Text == "") sqlStr = sqlStr + "NULL)";
                else sqlStr = sqlStr + "'" + TB_riqi.Text.Trim() + "')";

                try
                {
                    DataBaseHelper.execute(sqlStr);

                    String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                    string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','添加','固井','" + jh + "','','" + qk + "','')";
                    DataBaseHelper.execute(sqlLog);

                    Response.Redirect(url);
                }
                catch (Exception e2)
                {
                    return;
                }
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

        protected void btnreturn_Click1(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    
    }
}
         