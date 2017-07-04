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

namespace NCCBM.data.zj.scjwjtjb
{
    public partial class scjwjtjb_add : System.Web.UI.Page
    {
        private String tablename = "Xls_Zj_Rbb_Tjb";
        private String url = "scjwjtjb_list.aspx?Type=list";
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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            String jd = TB_fuzejiandu.Text.Trim();
            String dh = TB_shigongdanwei.Text.Trim();
            String jh = TB_jinghao.Text.Trim();
            String qk = TB_qukuai.Text.Trim();
            if (jd == "" || dh == "" || jh == "" || qk == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('负责监督、队号、井号、区块不能为空！'); </script>");
                return;
            }

            bool bHave = VerifyExist(TB_jinghao.Text);
            if (bHave)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该项目的基本情况已经存在，请重新选择！'); </script>");
            }
            else
            {
                string sqlStr = "INSERT INTO " + tablename + "(" + 
                    "fuzejiandu," +
                    "shigongduiwu," +
                    "jinghao," +
                    "wanzuanjingshen," +
                    "kaizuanriqi," +
                    "wanzuanriqi," +
                    "wanjingshijian," +
                    "zuanjingzhouqi," +
                    "zuanjixinghao," +
                    "ykys_riqi," +
                    "ykys_cunzaiwenti," +
                    "ykys_shifoutongyikaizuan," +
                    "ykys_fuyanqingkuang," +
                    "ekys_riqi," +
                    "ekys_cunzaiwenti," +
                    "ekys_shifoutongyikaizuan," +
                    "ekys_fuyanqingkuang," +
                    "btsj_biaotaoxiashen," +
                    "sctgsj_changjiagangji," +
                    "sctgsj_waiguanjiancha," +
                    "sctgsj_bianhaohecha," +
                    "sctgsj_taoguanxiashen," +
                    "sctgsj_rujingchangtao," +
                    "sctgsj_shengyuchangtao," +
                    "sctgsj_rujingduantao," +
                    "sctgsj_shengyuduantao," +
                    "sctgsj_duantaoweizhi," +
                    "qxsj_quxinhuici," +
                    "qxsj_zongshouhuolv," +
                    "gjsgsj_gujingdui," +
                    "gjsgsj_qianzhiye," +
                    "gjsgsj_zhushuinijiangliang," +
                    "gjsgsj_tijingliang," +
                    "gjsgsj_pengyaqingkuang," +
                    "gjsgsj_shuinijiangmidu," +
                    "sy_riqi," +
                    "sy_yajiangqingkuang," +
                    "wjjc_jingkoushuiping," +
                    "wjjc_jingkougaodu," +
                    "wjjc_jingkouhanjie," +
                    "beizhu1," +//61

                    "bianhao," +
                    "xiaojieriqi," +
                    "jsjg_yikai," +
                    "jsjg_biaotao," +
                    "jsjg_erkai," +
                    "jsjg_chantao," +
                    "mcsj_3," +
                    "mcsj_5," +
                    "mcsj_11," +
                    "mcsj_qitameiceng," +
                    "jszlsj_baxinju," +
                    "jszlsj_zuidajingxie," +
                    "jszlsj_zuidaweiyi," +
                    "jszlsj_zuidaquanjiao," +
                    "jszlsj_zxd1_zuidalianxu," +
                    "jszlsj_zxd1_lianxusandian," +
                    "jszlsj_zxd2_zuidalianxu," +
                    "jszlsj_zxd2_lianxusandian," +
                    "jszlsj_quanjingduan," +
                    "jszlsj_meicengduan," +
                    "xtggjsj_dctg_duantaoshejiweizhi," +
                    "xtggjsj_dctg_duantaoshiceweizhi," +
                    "xtggjsj_dctg_zuliuhuanshendu," +
                    "xtggjsj_yuzushendu," +
                    "xtggjsj_dctg_meicengduanjieguweizhi," +
                    "xtggjsj_gjqk_shuinifanshen," +
                    "xtggjsj_diancepingjia," +
                    "beizhu2," +

                    "jbsj_jingbie," +
                    "jbsj_jingxing," +
                    "jbsj_diliweizhi," +
                    "jbsj_shejijingshen," +
                    "sjjsjg_yikai," +
                    "sjjsjg_taoguanxiashen," +
                    "sjjsjg_erkai," +
                    "sjjsjg_chantaoxiashen," +
                    "dxsj_jingkouzuobiao_x," +
                    "dxsj_jingkouzuobiao_y," +
                    "dxsj_jingkouhaiba," +
                    "dxsj_jingdichuishen," +
                    "dxsj_badianzuobiao_x," +
                    "dxsj_badianzuobiao_y," +
                    "dxsj_badianfangwei," +
                    "dxsj_badianchuishen," +
                    "dxsj_badianweiyi," +
                    "dxsj_cipianjiao," +
                    "dxsj_damenfangxiang," +
                    "dxsj_zaoxieduan," +
                    "dxsj_shejijingxie," +

                    "place" +
                    ") Values(";//92

                float fTemp;
                sqlStr = sqlStr + "'" + TB_fuzejiandu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_shigongdanwei.Text.Trim() + "',";
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

                if (TB_kaizuanriqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_kaizuanriqi.Text.Trim() + "',";

                if (TB_wanzuanriqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_wanzuanriqi.Text.Trim() + "',";

                if (TB_wanjingshijian.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_wanjingshijian.Text.Trim() + "',";

                if (TB_zuanjingzhouqi.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_zuanjingzhouqi.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_zuanjixinghao.Text.Trim() + "',";

                if (TB_ykys_riqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_ykys_riqi.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + TB_ykys_cunzaiwenti.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_ykys_shifoutongyi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_ykys_fuyanqingkuang.Text.Trim() + "',";

                if (TB_ekys_riqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_ekys_riqi.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + TB_ekys_cunzaiwenti.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_ekys_shifoutongyi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_ekys_fuyanqingkuang.Text.Trim() + "',";

                if (TB_btsj_biaotaoxiashen.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_btsj_biaotaoxiashen.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_sctgsj_changjia.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_sctgsj_waiguan.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_sctgsj_bianhao.Text.Trim() + "',";

                if (TB_sctgsj_taoguanxiashen.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_sctgsj_taoguanxiashen.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_sctgsj_rujingchangtao.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        int iTemp = Int32.Parse(TB_sctgsj_rujingchangtao.Text.Trim());
                        sqlStr = sqlStr + iTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_sctgsj_shengyuchangtao.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        int iTemp = Int32.Parse(TB_sctgsj_shengyuchangtao.Text.Trim());
                        sqlStr = sqlStr + iTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_sctgsj_rujingduantao.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        int iTemp = Int32.Parse(TB_sctgsj_rujingduantao.Text.Trim());
                        sqlStr = sqlStr + iTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_sctgsj_shengyuduantao.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        int iTemp = Int32.Parse(TB_sctgsj_shengyuduantao.Text.Trim());
                        sqlStr = sqlStr + iTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_sctgsj_duantaoweizhi.Text.Trim() + "',";

                if (TB_qxsj_quxinhuici.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        int iTemp = Int32.Parse(TB_qxsj_quxinhuici.Text.Trim());
                        sqlStr = sqlStr + iTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_qxsj_zongshouhuolv.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_gjsgsj_gujingdui.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_gjsgsj_qianzhiye.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_gjsgsj_zhushuinijiang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_gjsgsj_tijiangliang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_gjsgsj_pengya.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_gjsgsj_midu.Text.Trim() + "',";

                if (TB_sy_riqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_sy_riqi.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + TB_sy_yajiang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_wjjc_shuiping.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_wjjc_gaodu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_wjjc_hanjie.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_beizhu1.Text.Trim() + "',";


                sqlStr = sqlStr + "'" + TB_bianhao.Text.Trim() + "',";

                if (TB_xiaojieriqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + TB_xiaojieriqi.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + TB_jsjg_yikai.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jsjg_biaotao.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jsjg_erkai.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jsjg_chantao.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_mcsj_3.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_mcsj_5.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_mcsj_11.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_mcsj_qita.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jszlsj_baxinju.Text.Trim() + "',";

                if (TB_jszlsj_jingxie.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_jszlsj_jingxie.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_jszlsj_weiyi.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_jszlsj_weiyi.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_jszlsj_quanjiao.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_jszlsj_quanjiao.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_jszlsj_zxd_lianxu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jszlsj_zxd_pingjun.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jszlsj_wxd_lianxu.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jszlsj_wxd_pingjun.Text.Trim() + "',";

                if (TB_jszlsj_quanjingduan.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_jszlsj_quanjingduan.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_jszlsj_meicengduan.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_jszlsj_meicengduan.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_xtggjsj_dctgqk_shejiweizhi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_xtggjsj_dctgqk_shiceweizhi.Text.Trim() + "',";

                if (TB_xtggjsj_dctgqk_zuliuhuan.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_xtggjsj_dctgqk_zuliuhuan.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_xtggjsj_dctgqk_yuzushendu.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_xtggjsj_dctgqk_yuzushendu.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_xtggjsj_dctgqk_jieguweizhi.Text.Trim() + "',";

                if (TB_xtggjsj_gjqk_shuinifanshen.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_xtggjsj_gjqk_shuinifanshen.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_xtggjsj_diancepingjia.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_beizhu2.Text.Trim() + "',";


                sqlStr = sqlStr + "'" + TB_jbsj_jingbie.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jbsj_jingxing.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_jbsj_diliweizhi.Text.Trim() + "',";

                if (TB_jbsj_shejijingshen.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_jbsj_shejijingshen.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_sjjsjg_yikai.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_sjjsjg_biaotaoxiashen.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_sjjsjg_erkai.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_sjjsjg_chantaoxiashen.Text.Trim() + "',";

                if (TB_dxsj_jingkou_x.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_dxsj_jingkou_x.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_dxsj_jingkou_y.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_dxsj_jingkou_y.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_dxsj_jingkou_h.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_dxsj_jingkou_h.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_dxsj_jingdichuishen.Text.Trim() + "',";

                if (TB_dxsj_badian_x.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_dxsj_badian_x.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                if (TB_dxsj_badian_y.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_dxsj_badian_y.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_dxsj_badianfangwei.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_dxsj_badianchuishen.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_dxsj_badianweiyi.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_dxsj_cipianjiao.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_dxsj_damenfangxiang.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + TB_dxsj_zaoxieduan.Text.Trim() + "',";

                if (TB_dxsj_shejijingxie.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(TB_dxsj_shejijingxie.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + TB_qukuai.Text.Trim() + "')";

                try
                {
                    DataBaseHelper.execute(sqlStr);

                    String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                    string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','添加','完井数据统计表','" + jh + "','','" + qk + "','')";
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}
