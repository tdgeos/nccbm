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

namespace NCCBM.data.zj.zj
{
    public partial class zj_add : System.Web.UI.Page
    {
        private String tablename = "Xls_Zj_Rbb_Zj";
        private String url = "zj_list.aspx?Type=list";
        private int userPlaceId = 0;
        private String userName = null;

        protected void Page_Load(object sender, EventArgs e)
        {
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

            //判断名称是否相同
            bool bHave = VerifyExist(TB_jinghao.Text, TB_riqi.Text);
            if (bHave)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该项目的基本情况已经存在，请重新选择！'); </script>");
            }
            else
            {
                string sqlStr = "INSERT INTO " + tablename + "(fuzejiandu,duihao,jinghao,kaizuanriqi,shejijingshen,muqianjingshen,"+
                    "dangrijinchi,gongkuang,midu,niandu,jingduan,jingxie_sheji,jingxie_shiji,fangwei_sheji,fangwei_shiji,goutui_sheji,"+
                    "goutui_shiji,HSEqingkuang,fuzaqingkuang,zhenggaineirong,beizhu,jiancefangshi,place,riqi) Values(";
                float fTemp;
                sqlStr = sqlStr + "'" + TB_fuzejiandu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_duihao.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jinghao.Text.Trim() + "',";

                if (TB_kaizuanriqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_kaizuanriqi.Text.Trim() + "',";

                if (TB_shejijingshen.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_shejijingshen.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_muqianjingshen.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_muqianjingshen.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_dangrijinchi.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_dangrijinchi.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_gongkuang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_midu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_niandu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jingduan.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jingxie_sheji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jingxie_shiji.Text.Trim() + "',";    
                sqlStr = sqlStr + "'" + TB_fangwei_sheji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_fangwei_shiji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_goutui_sheji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_goutui_shiji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_HSEqingkuang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_fuzaqingkuang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_zhenggaicuoshi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_beizhu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jiancefangshi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_qukuai.Text.Trim() + "',";

                if (TB_riqi.Text == "") sqlStr = sqlStr + "NULL)";
                else sqlStr = sqlStr + "'" + TB_riqi.Text.Trim() + "')";  

                try
                {
                    DataBaseHelper.execute(sqlStr);

                    String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                    string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','添加','钻进','" + jh + "','','" + qk + "','')";
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