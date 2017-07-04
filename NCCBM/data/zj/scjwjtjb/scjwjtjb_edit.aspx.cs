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

    public partial class scjwjtjb_edit : System.Web.UI.Page
    {
        private String tablename = "Xls_Zj_Rbb_Tjb";
        private String url = "scjwjtjb_list.aspx?Type=list";
        private string id = "";
        private string szRoleID = null;
        private String userName = null;
        private String qk = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                szRoleID = HttpContext.Current.Session["RoleID"].ToString();
                userName = HttpContext.Current.Session["LoginUserID"].ToString();
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

        void setTextBox()
        {
            
            SqlDataReader dr;
            SqlConnection Myconn = DBCONN.GetDBConn();
            SqlCommand Mycomm = new SqlCommand("select * from " + tablename + " where id = " + id, Myconn);
            Mycomm.Connection.Open();
            dr = Mycomm.ExecuteReader();
            dr.Read();

            TB_fuzejiandu.Text = dr["fuzejiandu"].ToString();
            TB_shigongdanwei.Text = dr["shigongduiwu"].ToString();
            TB_jinghao.Text = dr["shigongduiwu"].ToString();
            TB_wanzuanjingshen.Text = dr["wanzuanjingshen"].ToString();

            if (dr["kaizuanriqi"] == null || dr["kaizuanriqi"].ToString().Trim() == "") TB_kaizuanriqi.Text = "";
            else TB_kaizuanriqi.Text = ((DateTime)dr["kaizuanriqi"]).ToString("yyyy-MM-dd");

            if (dr["wanzuanriqi"] == null || dr["wanzuanriqi"].ToString().Trim() == "") TB_wanzuanriqi.Text = "";
            else TB_wanzuanriqi.Text = ((DateTime)dr["wanzuanriqi"]).ToString("yyyy-MM-dd");

            if (dr["wanjingshijian"] == null || dr["wanjingshijian"].ToString().Trim() == "") TB_wanjingshijian.Text = "";
            else TB_wanjingshijian.Text = ((DateTime)dr["wanjingshijian"]).ToString("yyyy-MM-dd");

            TB_zuanjingzhouqi.Text = dr["zuanjingzhouqi"].ToString();
            TB_zuanjixinghao.Text = dr["zuanjixinghao"].ToString();

            if (dr["ykys_riqi"] == null || dr["ykys_riqi"].ToString().Trim() == "") TB_ykys_riqi.Text = "";
            else TB_ykys_riqi.Text = ((DateTime)dr["ykys_riqi"]).ToString("yyyy-MM-dd");

            TB_ykys_cunzaiwenti.Text = dr["ykys_cunzaiwenti"].ToString();
            TB_ykys_shifoutongyi.Text = dr["ykys_shifoutongyikaizuan"].ToString();
            TB_ykys_fuyanqingkuang.Text = dr["ykys_fuyanqingkuang"].ToString();

            if (dr["ekys_riqi"] == null || dr["ekys_riqi"].ToString().Trim() == "") TB_ekys_riqi.Text = "";
            else TB_ekys_riqi.Text = ((DateTime)dr["ekys_riqi"]).ToString("yyyy-MM-dd");

            TB_ekys_cunzaiwenti.Text = dr["ekys_cunzaiwenti"].ToString();
            TB_ekys_shifoutongyi.Text = dr["ekys_shifoutongyikaizuan"].ToString();
            TB_ekys_fuyanqingkuang.Text = dr["ekys_fuyanqingkuang"].ToString();
            TB_btsj_biaotaoxiashen.Text = dr["btsj_biaotaoxiashen"].ToString();
            TB_sctgsj_changjia.Text = dr["sctgsj_changjiagangji"].ToString();
            TB_sctgsj_waiguan.Text = dr["sctgsj_waiguanjiancha"].ToString();
            TB_sctgsj_bianhao.Text = dr["sctgsj_bianhaohecha"].ToString();
            TB_sctgsj_taoguanxiashen.Text = dr["sctgsj_taoguanxiashen"].ToString();
            TB_sctgsj_rujingchangtao.Text = dr["sctgsj_rujingchangtao"].ToString();
            TB_sctgsj_shengyuchangtao.Text = dr["sctgsj_shengyuchangtao"].ToString();
            TB_sctgsj_rujingduantao.Text = dr["sctgsj_rujingduantao"].ToString();
            TB_sctgsj_shengyuduantao.Text = dr["sctgsj_shengyuduantao"].ToString();
            TB_sctgsj_duantaoweizhi.Text = dr["sctgsj_duantaoweizhi"].ToString();
            TB_qxsj_quxinhuici.Text = dr["qxsj_quxinhuici"].ToString();
            TB_qxsj_zongshouhuolv.Text = dr["qxsj_zongshouhuolv"].ToString();
            TB_gjsgsj_gujingdui.Text = dr["gjsgsj_gujingdui"].ToString();
            TB_gjsgsj_qianzhiye.Text = dr["gjsgsj_qianzhiye"].ToString();
            TB_gjsgsj_zhushuinijiang.Text = dr["gjsgsj_zhushuinijiangliang"].ToString();
            TB_gjsgsj_tijiangliang.Text = dr["gjsgsj_tijingliang"].ToString();
            TB_gjsgsj_pengya.Text = dr["gjsgsj_pengyaqingkuang"].ToString();
            TB_gjsgsj_midu.Text = dr["gjsgsj_shuinijiangmidu"].ToString();

            if (dr["sy_riqi"] == null || dr["sy_riqi"].ToString().Trim() == "") TB_sy_riqi.Text = "";
            else TB_sy_riqi.Text = ((DateTime)dr["sy_riqi"]).ToString("yyyy-MM-dd");

            TB_sy_yajiang.Text = dr["sy_yajiangqingkuang"].ToString();
            TB_wjjc_shuiping.Text = dr["wjjc_jingkoushuiping"].ToString();
            TB_wjjc_gaodu.Text = dr["wjjc_jingkougaodu"].ToString();
            TB_wjjc_hanjie.Text = dr["wjjc_jingkouhanjie"].ToString();
            TB_beizhu1.Text = dr["beizhu1"].ToString();


            TB_bianhao.Text = dr["bianhao"].ToString();

            if (dr["xiaojieriqi"] == null || dr["xiaojieriqi"].ToString().Trim() == "") TB_xiaojieriqi.Text = "";
            else TB_xiaojieriqi.Text = ((DateTime)dr["xiaojieriqi"]).ToString("yyyy-MM-dd");

            TB_jsjg_yikai.Text = dr["jsjg_yikai"].ToString();
            TB_jsjg_biaotao.Text = dr["jsjg_biaotao"].ToString();
            TB_jsjg_erkai.Text = dr["jsjg_erkai"].ToString();
            TB_jsjg_chantao.Text = dr["jsjg_chantao"].ToString();
            TB_mcsj_3.Text = dr["mcsj_3"].ToString();
            TB_mcsj_5.Text = dr["mcsj_5"].ToString();
            TB_mcsj_11.Text = dr["mcsj_11"].ToString();
            TB_mcsj_qita.Text = dr["mcsj_qitameiceng"].ToString();
            TB_jszlsj_baxinju.Text = dr["jszlsj_baxinju"].ToString();
            TB_jszlsj_jingxie.Text = dr["jszlsj_zuidajingxie"].ToString();
            TB_jszlsj_weiyi.Text = dr["jszlsj_zuidaweiyi"].ToString();
            TB_jszlsj_quanjiao.Text = dr["jszlsj_zuidaquanjiao"].ToString();
            TB_jszlsj_zxd_lianxu.Text = dr["jszlsj_zxd1_zuidalianxu"].ToString();
            TB_jszlsj_zxd_pingjun.Text = dr["jszlsj_zxd1_lianxusandian"].ToString();
            TB_jszlsj_wxd_lianxu.Text = dr["jszlsj_zxd2_zuidalianxu"].ToString();
            TB_jszlsj_wxd_pingjun.Text = dr["jszlsj_zxd2_lianxusandian"].ToString();
            TB_jszlsj_quanjingduan.Text = dr["jszlsj_quanjingduan"].ToString();
            TB_jszlsj_meicengduan.Text = dr["jszlsj_meicengduan"].ToString();
            TB_xtggjsj_dctgqk_shejiweizhi.Text = dr["xtggjsj_dctg_duantaoshejiweizhi"].ToString();
            TB_xtggjsj_dctgqk_shiceweizhi.Text = dr["xtggjsj_dctg_duantaoshiceweizhi"].ToString();
            TB_xtggjsj_dctgqk_zuliuhuan.Text = dr["xtggjsj_dctg_zuliuhuanshendu"].ToString();
            TB_xtggjsj_dctgqk_yuzushendu.Text = dr["xtggjsj_yuzushendu"].ToString();
            TB_xtggjsj_dctgqk_jieguweizhi.Text = dr["xtggjsj_dctg_meicengduanjieguweizhi"].ToString();
            TB_xtggjsj_gjqk_shuinifanshen.Text = dr["xtggjsj_gjqk_shuinifanshen"].ToString();
            TB_xtggjsj_diancepingjia.Text = dr["xtggjsj_diancepingjia"].ToString();
            TB_beizhu2.Text = dr["beizhu2"].ToString();


            TB_jbsj_jingbie.Text = dr["jbsj_jingbie"].ToString();
            TB_jbsj_jingxing.Text = dr["jbsj_jingxing"].ToString();
            TB_jbsj_diliweizhi.Text = dr["jbsj_diliweizhi"].ToString();
            TB_jbsj_shejijingshen.Text = dr["jbsj_shejijingshen"].ToString();
            TB_sjjsjg_yikai.Text = dr["sjjsjg_yikai"].ToString();
            TB_sjjsjg_biaotaoxiashen.Text = dr["sjjsjg_taoguanxiashen"].ToString();
            TB_sjjsjg_erkai.Text = dr["sjjsjg_erkai"].ToString();
            TB_sjjsjg_chantaoxiashen.Text = dr["sjjsjg_chantaoxiashen"].ToString();
            TB_dxsj_jingkou_x.Text = dr["dxsj_jingkouzuobiao_x"].ToString();
            TB_dxsj_jingkou_y.Text = dr["dxsj_jingkouzuobiao_y"].ToString();
            TB_dxsj_jingkou_h.Text = dr["dxsj_jingkouhaiba"].ToString();
            TB_dxsj_jingdichuishen.Text = dr["dxsj_jingdichuishen"].ToString();
            TB_dxsj_badian_x.Text = dr["dxsj_badianzuobiao_x"].ToString();
            TB_dxsj_badian_y.Text = dr["dxsj_badianzuobiao_y"].ToString();
            TB_dxsj_badianfangwei.Text = dr["dxsj_badianfangwei"].ToString();
            TB_dxsj_badianchuishen.Text = dr["dxsj_badianchuishen"].ToString();
            TB_dxsj_badianweiyi.Text = dr["dxsj_badianweiyi"].ToString();
            TB_dxsj_cipianjiao.Text = dr["dxsj_cipianjiao"].ToString();
            TB_dxsj_damenfangxiang.Text = dr["dxsj_damenfangxiang"].ToString();
            TB_dxsj_zaoxieduan.Text = dr["dxsj_zaoxieduan"].ToString();
            TB_dxsj_shejijingxie.Text = dr["dxsj_shejijingxie"].ToString();

            qk = dr["place"].ToString();

            dr.Close();
            Mycomm.Connection.Close();
            
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            String jd = TB_fuzejiandu.Text.Trim();
            String dh = TB_shigongdanwei.Text.Trim();
            String jh = TB_jinghao.Text.Trim();
            if (jd == "" || dh == "" || jh == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('负责监督、队号、井号不能为空！'); </script>");
                return;
            }

            string sqlStr = "update " + tablename + " set ";

            float fTemp;
            sqlStr = sqlStr + "fuzejiandu='" + TB_fuzejiandu.Text + "',";
            sqlStr = sqlStr + "shigongduiwu='" + TB_shigongdanwei.Text + "',";
            sqlStr = sqlStr + "jinghao='" + TB_jinghao.Text + "',";

            if (TB_wanzuanjingshen.Text == "")
            {
                sqlStr = sqlStr + "wanzuanjingshen=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_wanzuanjingshen.Text.Trim());
                    sqlStr = sqlStr + "wanzuanjingshen=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "wanzuanjingshen=NULL,";
                }
            }

            if (TB_kaizuanriqi.Text == "") sqlStr = sqlStr + "kaizuanriqi=NULL,";
            else sqlStr = sqlStr + "kaizuanriqi='" + TB_kaizuanriqi.Text + "',";

            if (TB_wanzuanriqi.Text == "") sqlStr = sqlStr + "wanzuanriqi=NULL,";
            else sqlStr = sqlStr + "wanzuanriqi='" + TB_wanzuanriqi.Text + "',";

            if (TB_wanjingshijian.Text == "") sqlStr = sqlStr + "wanjingshijian=NULL,";
            else sqlStr = sqlStr + "wanjingshijian='" + TB_wanjingshijian.Text + "',";

            if (TB_zuanjingzhouqi.Text == "")
            {
                sqlStr = sqlStr + "zuanjingzhouqi=NULL,";
            }
            else
            {
                try
                {
                    int iTemp = Int32.Parse(TB_zuanjingzhouqi.Text.Trim());
                    sqlStr = sqlStr + "zuanjingzhouqi=" + iTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "zuanjingzhouqi=NULL,";
                }
            }

            sqlStr = sqlStr + "zuanjixinghao='" + TB_zuanjixinghao.Text + "',";

            if (TB_ykys_riqi.Text == "") sqlStr = sqlStr + "ykys_riqi=NULL,";
            else sqlStr = sqlStr + "ykys_riqi='" + TB_ykys_riqi.Text + "',";

            sqlStr = sqlStr + "ykys_cunzaiwenti='" + TB_ykys_cunzaiwenti.Text + "',";
            sqlStr = sqlStr + "ykys_shifoutongyikaizuan='" + TB_ykys_shifoutongyi.Text + "',";
            sqlStr = sqlStr + "ykys_fuyanqingkuang='" + TB_ykys_fuyanqingkuang.Text + "',";

            if (TB_ekys_riqi.Text == "") sqlStr = sqlStr + "ekys_riqi=NULL,";
            else sqlStr = sqlStr + "ekys_riqi='" + TB_ekys_riqi.Text + "',";

            sqlStr = sqlStr + "ekys_cunzaiwenti='" + TB_ekys_cunzaiwenti.Text + "',";
            sqlStr = sqlStr + "ekys_shifoutongyikaizuan='" + TB_ekys_shifoutongyi.Text + "',";
            sqlStr = sqlStr + "ekys_fuyanqingkuang='" + TB_ekys_fuyanqingkuang.Text + "',";

            if (TB_btsj_biaotaoxiashen.Text == "")
            {
                sqlStr = sqlStr + "btsj_biaotaoxiashen=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_btsj_biaotaoxiashen.Text.Trim());
                    sqlStr = sqlStr + "btsj_biaotaoxiashen=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "btsj_biaotaoxiashen=NULL,";
                }
            }

            sqlStr = sqlStr + "sctgsj_changjiagangji='" + TB_sctgsj_changjia.Text + "',";
            sqlStr = sqlStr + "sctgsj_waiguanjiancha='" + TB_sctgsj_waiguan.Text + "',";
            sqlStr = sqlStr + "sctgsj_bianhaohecha='" + TB_sctgsj_bianhao.Text + "',";

            if (TB_sctgsj_taoguanxiashen.Text == "")
            {
                sqlStr = sqlStr + "sctgsj_taoguanxiashen=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_sctgsj_taoguanxiashen.Text.Trim());
                    sqlStr = sqlStr + "sctgsj_taoguanxiashen=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "sctgsj_taoguanxiashen=NULL,";
                }
            }

            if (TB_sctgsj_rujingchangtao.Text == "")
            {
                sqlStr = sqlStr + "sctgsj_rujingchangtao=NULL,";
            }
            else
            {
                try
                {
                    int iTemp = Int32.Parse(TB_sctgsj_rujingchangtao.Text.Trim());
                    sqlStr = sqlStr + "sctgsj_rujingchangtao=" + iTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "sctgsj_rujingchangtao=NULL,";
                }
            }

            if (TB_sctgsj_shengyuchangtao.Text == "")
            {
                sqlStr = sqlStr + "sctgsj_shengyuchangtao=NULL,";
            }
            else
            {
                try
                {
                    int iTemp = Int32.Parse(TB_sctgsj_shengyuchangtao.Text.Trim());
                    sqlStr = sqlStr + "sctgsj_shengyuchangtao=" + iTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "sctgsj_shengyuchangtao=NULL,";
                }
            }

            if (TB_sctgsj_rujingduantao.Text == "")
            {
                sqlStr = sqlStr + "sctgsj_rujingduantao=NULL,";
            }
            else
            {
                try
                {
                    int iTemp = Int32.Parse(TB_sctgsj_rujingduantao.Text.Trim());
                    sqlStr = sqlStr + "sctgsj_rujingduantao=" + iTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "sctgsj_rujingduantao=NULL,";
                }
            }

            if (TB_sctgsj_shengyuduantao.Text == "")
            {
                sqlStr = sqlStr + "sctgsj_shengyuduantao=NULL,";
            }
            else
            {
                try
                {
                    int iTemp = Int32.Parse(TB_sctgsj_shengyuduantao.Text.Trim());
                    sqlStr = sqlStr + "sctgsj_shengyuduantao=" + iTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "sctgsj_shengyuduantao=NULL,";
                }
            }

            sqlStr = sqlStr + "sctgsj_duantaoweizhi='" + TB_sctgsj_duantaoweizhi.Text + "',";

            if (TB_qxsj_quxinhuici.Text == "")
            {
                sqlStr = sqlStr + "qxsj_quxinhuici=NULL,";
            }
            else
            {
                try
                {
                    int iTemp = Int32.Parse(TB_qxsj_quxinhuici.Text.Trim());
                    sqlStr = sqlStr + "qxsj_quxinhuici=" + iTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "qxsj_quxinhuici=NULL,";
                }
            }

            sqlStr = sqlStr + "qxsj_zongshouhuolv='" + TB_qxsj_zongshouhuolv.Text + "',";
            sqlStr = sqlStr + "gjsgsj_gujingdui='" + TB_gjsgsj_gujingdui.Text + "',";
            sqlStr = sqlStr + "gjsgsj_qianzhiye='" + TB_gjsgsj_qianzhiye.Text + "',";
            sqlStr = sqlStr + "gjsgsj_zhushuinijiangliang='" + TB_gjsgsj_zhushuinijiang.Text + "',";
            sqlStr = sqlStr + "gjsgsj_tijingliang='" + TB_gjsgsj_tijiangliang.Text + "',";
            sqlStr = sqlStr + "gjsgsj_pengyaqingkuang='" + TB_gjsgsj_pengya.Text + "',";
            sqlStr = sqlStr + "gjsgsj_shuinijiangmidu='" + TB_gjsgsj_midu.Text + "',";

            if (TB_sy_riqi.Text == "") sqlStr = sqlStr + "sy_riqi=NULL,";
            else sqlStr = sqlStr + "sy_riqi='" + TB_sy_riqi.Text + "',";

            sqlStr = sqlStr + "sy_yajiangqingkuang='" + TB_sy_yajiang.Text + "',";
            sqlStr = sqlStr + "wjjc_jingkoushuiping='" + TB_wjjc_shuiping.Text + "',";
            sqlStr = sqlStr + "wjjc_jingkougaodu='" + TB_wjjc_gaodu.Text + "',";
            sqlStr = sqlStr + "wjjc_jingkouhanjie='" + TB_wjjc_hanjie.Text + "',";
            sqlStr = sqlStr + "beizhu1='" + TB_beizhu1.Text + "',";


            sqlStr = sqlStr + "bianhao='" + TB_bianhao.Text + "',";
            
            if (TB_xiaojieriqi.Text == "") sqlStr = sqlStr + "xiaojieriqi=NULL,";
            else sqlStr = sqlStr + "xiaojieriqi='" + TB_xiaojieriqi.Text + "',";

            sqlStr = sqlStr + "jsjg_yikai='" + TB_jsjg_yikai.Text + "',";
            sqlStr = sqlStr + "jsjg_biaotao='" + TB_jsjg_biaotao.Text + "',";
            sqlStr = sqlStr + "jsjg_erkai='" + TB_jsjg_erkai.Text + "',";
            sqlStr = sqlStr + "jsjg_chantao='" + TB_jsjg_chantao.Text + "',";
            sqlStr = sqlStr + "mcsj_3='" + TB_mcsj_3.Text + "',";
            sqlStr = sqlStr + "mcsj_5='" + TB_mcsj_5.Text + "',";
            sqlStr = sqlStr + "mcsj_11='" + TB_mcsj_11.Text + "',";
            sqlStr = sqlStr + "mcsj_qitameiceng='" + TB_mcsj_qita.Text + "',";
            sqlStr = sqlStr + "jszlsj_baxinju='" + TB_jszlsj_baxinju.Text + "',";

            if (TB_jszlsj_jingxie.Text == "")
            {
                sqlStr = sqlStr + "jszlsj_zuidajingxie=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_jszlsj_jingxie.Text.Trim());
                    sqlStr = sqlStr + "jszlsj_zuidajingxie=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "jszlsj_zuidajingxie=NULL,";
                }
            }

            if (TB_jszlsj_weiyi.Text == "")
            {
                sqlStr = sqlStr + "jszlsj_zuidaweiyi=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_jszlsj_weiyi.Text.Trim());
                    sqlStr = sqlStr + "jszlsj_zuidaweiyi=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "jszlsj_zuidaweiyi=NULL,";
                }
            }

            if (TB_jszlsj_quanjiao.Text == "")
            {
                sqlStr = sqlStr + "jszlsj_zuidaquanjiao=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_jszlsj_quanjiao.Text.Trim());
                    sqlStr = sqlStr + "jszlsj_zuidaquanjiao=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "jszlsj_zuidaquanjiao=NULL,";
                }
            }

            sqlStr = sqlStr + "jszlsj_zxd1_zuidalianxu='" + TB_jszlsj_zxd_lianxu.Text + "',";
            sqlStr = sqlStr + "jszlsj_zxd1_lianxusandian='" + TB_jszlsj_zxd_pingjun.Text + "',";
            sqlStr = sqlStr + "jszlsj_zxd2_zuidalianxu='" + TB_jszlsj_wxd_lianxu.Text + "',";
            sqlStr = sqlStr + "jszlsj_zxd2_lianxusandian='" + TB_jszlsj_wxd_pingjun.Text + "',";

            if (TB_jszlsj_quanjingduan.Text == "")
            {
                sqlStr = sqlStr + "jszlsj_quanjingduan=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_jszlsj_quanjingduan.Text.Trim());
                    sqlStr = sqlStr + "jszlsj_quanjingduan=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "jszlsj_quanjingduan=NULL,";
                }
            }
            
            if (TB_jszlsj_meicengduan.Text == "")
            {
                sqlStr = sqlStr + "jszlsj_meicengduan=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_jszlsj_meicengduan.Text.Trim());
                    sqlStr = sqlStr + "jszlsj_meicengduan=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "jszlsj_meicengduan=NULL,";
                }
            }

            sqlStr = sqlStr + "xtggjsj_dctg_duantaoshejiweizhi='" + TB_xtggjsj_dctgqk_shejiweizhi.Text + "',";
            sqlStr = sqlStr + "xtggjsj_dctg_duantaoshiceweizhi='" + TB_xtggjsj_dctgqk_shiceweizhi.Text + "',";

            if (TB_xtggjsj_dctgqk_zuliuhuan.Text == "")
            {
                sqlStr = sqlStr + "xtggjsj_dctg_zuliuhuanshendu=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_xtggjsj_dctgqk_zuliuhuan.Text.Trim());
                    sqlStr = sqlStr + "xtggjsj_dctg_zuliuhuanshendu=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "xtggjsj_dctg_zuliuhuanshendu=NULL,";
                }
            }

            if (TB_xtggjsj_dctgqk_yuzushendu.Text == "")
            {
                sqlStr = sqlStr + "xtggjsj_yuzushendu=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_xtggjsj_dctgqk_yuzushendu.Text.Trim());
                    sqlStr = sqlStr + "xtggjsj_yuzushendu=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "xtggjsj_yuzushendu=NULL,";
                }
            }

            sqlStr = sqlStr + "xtggjsj_dctg_meicengduanjieguweizhi='" + TB_xtggjsj_dctgqk_jieguweizhi.Text + "',";
            
            if (TB_xtggjsj_gjqk_shuinifanshen.Text == "")
            {
                sqlStr = sqlStr + "xtggjsj_gjqk_shuinifanshen=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_xtggjsj_gjqk_shuinifanshen.Text.Trim());
                    sqlStr = sqlStr + "xtggjsj_gjqk_shuinifanshen=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "xtggjsj_gjqk_shuinifanshen=NULL,";
                }
            }

            sqlStr = sqlStr + "xtggjsj_diancepingjia='" + TB_xtggjsj_diancepingjia.Text + "',";
            sqlStr = sqlStr + "beizhu2='" + TB_beizhu2.Text + "',";


            sqlStr = sqlStr + "jbsj_jingbie='" + TB_jbsj_jingbie.Text + "',";
            sqlStr = sqlStr + "jbsj_jingxing='" + TB_jbsj_jingxing.Text + "',";
            sqlStr = sqlStr + "jbsj_diliweizhi='" + TB_jbsj_diliweizhi.Text + "',";
            
            if (TB_jbsj_shejijingshen.Text == "")
            {
                sqlStr = sqlStr + "jbsj_shejijingshen=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_jbsj_shejijingshen.Text.Trim());
                    sqlStr = sqlStr + "jbsj_shejijingshen=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "jbsj_shejijingshen=NULL,";
                }
            }

            sqlStr = sqlStr + "sjjsjg_yikai='" + TB_sjjsjg_yikai.Text + "',";
            sqlStr = sqlStr + "sjjsjg_taoguanxiashen='" + TB_sjjsjg_biaotaoxiashen.Text + "',";
            sqlStr = sqlStr + "sjjsjg_erkai='" + TB_sjjsjg_erkai.Text + "', ";
            sqlStr = sqlStr + "sjjsjg_chantaoxiashen='" + TB_sjjsjg_chantaoxiashen.Text + "',";

            if (TB_dxsj_jingkou_x.Text == "")
            {
                sqlStr = sqlStr + "dxsj_jingkouzuobiao_x=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_dxsj_jingkou_x.Text.Trim());
                    sqlStr = sqlStr + "dxsj_jingkouzuobiao_x=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "dxsj_jingkouzuobiao_x=NULL,";
                }
            }

            if (TB_dxsj_jingkou_y.Text == "")
            {
                sqlStr = sqlStr + "dxsj_jingkouzuobiao_y=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_dxsj_jingkou_y.Text.Trim());
                    sqlStr = sqlStr + "dxsj_jingkouzuobiao_y=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "dxsj_jingkouzuobiao_y=NULL,";
                }
            }

            if (TB_dxsj_jingkou_h.Text == "")
            {
                sqlStr = sqlStr + "dxsj_jingkouhaiba=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_dxsj_jingkou_h.Text.Trim());
                    sqlStr = sqlStr + "dxsj_jingkouhaiba=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "dxsj_jingkouhaiba=NULL,";
                }
            }

            sqlStr = sqlStr + "dxsj_jingdichuishen='" + TB_dxsj_jingdichuishen.Text + "',";

            if (TB_dxsj_badian_x.Text == "")
            {
                sqlStr = sqlStr + "dxsj_badianzuobiao_x=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_dxsj_badian_x.Text.Trim());
                    sqlStr = sqlStr + "dxsj_badianzuobiao_x=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "dxsj_badianzuobiao_x=NULL,";
                }
            }

            if (TB_dxsj_badian_y.Text == "")
            {
                sqlStr = sqlStr + "dxsj_badianzuobiao_y=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_dxsj_badian_y.Text.Trim());
                    sqlStr = sqlStr + "dxsj_badianzuobiao_y=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "dxsj_badianzuobiao_y=NULL,";
                }
            }

            sqlStr = sqlStr + "dxsj_badianfangwei='" + TB_dxsj_badianfangwei.Text + "',";
            sqlStr = sqlStr + "dxsj_badianchuishen='" + TB_dxsj_badianchuishen.Text + "',";
            sqlStr = sqlStr + "dxsj_badianweiyi='" + TB_dxsj_badianweiyi.Text + "',";
            sqlStr = sqlStr + "dxsj_cipianjiao='" + TB_dxsj_cipianjiao.Text + "',";
            sqlStr = sqlStr + "dxsj_damenfangxiang='" + TB_dxsj_damenfangxiang.Text + "',";
            sqlStr = sqlStr + "dxsj_zaoxieduan='" + TB_dxsj_zaoxieduan.Text + "',";

            if (TB_dxsj_shejijingxie.Text == "")
            {
                sqlStr = sqlStr + "dxsj_shejijingxie=NULL ";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(TB_dxsj_shejijingxie.Text.Trim());
                    sqlStr = sqlStr + "dxsj_shejijingxie=" + fTemp.ToString() + " ";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "dxsj_shejijingxie=NULL ";
                }
            }

            sqlStr = sqlStr + " where id = " + id;
            try
            {
                DataBaseHelper.execute(sqlStr);

                String riqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
                string sqlLog = "insert into data_log values('" + userName + "','" + riqi + "','修改','完井数据统计表','" + jh + "','','" + qk + "','')";
                DataBaseHelper.execute(sqlLog);

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