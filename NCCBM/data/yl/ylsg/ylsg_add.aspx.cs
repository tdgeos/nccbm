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

namespace NCCBM.data.yl.ylsg
{
    public partial class ylsg_add : System.Web.UI.Page
    {
        private String tablename = "Xls_Yl_Rbb_Ylsg";
        private String url = "ylsg_list.aspx?Type=list";
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
            String ch = TB_cengwei.Text.Trim();
            String jh = TB_jinghao.Text.Trim();
            String qk = TB_qukuai.Text.Trim();
            if (ch == "" || jh == "" || qk == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('井号、层号和区块名称不能为空！'); </script>");
                return;
            }

            bool bHave = VerifyExist(TB_jinghao.Text);
            if (bHave)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该项目的基本情况已经存在，请重新选择！'); </script>");
            }
            else
            {
                string sqlStr = "INSERT INTO " + tablename + "(jinghao,cengwei,shigongriqi,mcjd_dingjie,mcjd_dijie,meicenghoudu,"+
                    "skjd_dingjie,skjd_dijie,shekaihoudu,yalieyeleix,qzy_sheji,qzy_shiji,xsy_sheji,xsy_shiji,xsy_zuidiyali,"+
                    "xsy_zuigaoyali,xsy_pingjunyali,xsy_pingjunpailiang,dty_sheji,dty_shiji,zyl_sheji,zyl_shiji,"+
                    "jsl_shejizhongxisha,jsl_shejizhongsha,jsl_shejicusha,jsl_shejizongshaliang,jsl_shijizhongxisha,"+
                    "jsl_shijizhongsha,jsl_shijicusha,jsl_shijizongshaliang,pjsb_sheji,pjsb_shiji,polieyali,tingbengyali,"+
                    "dang30miaohoujiangzhi,shifouhege,jianduren,shigongduiwu,qukuai,shifouyawan,shigongleixing,"+
                    "wanchengbaifenbi,teshuqingkshuoming,beizhu,place) Values(";
                sqlStr = sqlStr + "'" + TB_jinghao.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_cengwei.Text.Trim() + "',";

                if (TB_shigongriqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_shigongriqi.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + TB_meicengjingduan_dingjie.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_meicengjingduan_dijie.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_meicenghoudu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shekongjingduan_dingjie.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shekongjingduan_dijie.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shekaihoudu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_yalieyeleixing.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_qianzhiye_sheji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_qianzhiye_shiji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_xieshaye_sheji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_xieshaye_shiji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_xieshaye_zdyl.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_xieshaye_zgyl.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_xieshaye_pjyl.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_xieshaye_pjpl.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_dingtiye_sheji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_dingtiye_shiji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_zongyeliang_sheji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_zongyeliang_shiji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jsl_shejizhongxisha.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jsl_shejizhongsha.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jsl_shejicusha.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jsl_shejizongshaliang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jsl_shijizhongxisha.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jsl_shijizhongsha.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jsl_shijicusha.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jsl_shijizongshaliang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_pingjunshabi_sheji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_pingjunshabi_shiji.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_polieyali.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_tingbengyali.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_30fenzhong.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shifouhege.Text.Trim() + "',";  
                sqlStr = sqlStr + "'" + TB_jianduren.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shigongduiwu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_qukuai.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shifouyawan.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shigongleixing.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_wanchengbaifenbi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_teshuqingkuang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_beizhu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_qukuai.Text.Trim() + "')";

                try
                {
                    DataBaseHelper.execute(sqlStr);

                    String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                    string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','添加','压裂施工','" + jh + "','" + ch + "','" + qk + "','')";
                    DataBaseHelper.execute(sqlLog);

                    Response.Redirect(url);
                }
                catch (Exception e2)
                {
                    return;
                }
            }
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

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}