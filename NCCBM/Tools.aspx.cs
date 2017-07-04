using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace NCCBM
{
    public partial class Tools : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            zhengshu();
        }

        void zhengshu()
        {
            DataTable dt = NCCBM.Import.TableImport.ReadExcel("zhengshu.xls", "Sheet1");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string name = dt.Rows[i][0].ToString().Trim();
                string sql = "update T_System_EMPLOYEE set HSE1='2013.01.12', HSE2='2015.01.12', LHQ1='2013.01.13', LHQ2='2015.01.13', JK1='2013.01.10', JK2='2015.01.10' where Employee_Name='" + name + "'";
                DataBaseHelper.execute(sql);
            }
        }

        private void test()
        {
            string sql = null;
            DataTable dt = null;

            sql = "select id,jianchariqi from Xls_Yl_Rbb_Yqjc";
            dt = DataBaseHelper.query(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i][0].ToString().Trim();
                string s = dt.Rows[i][1].ToString().Trim();

                sql = "update Xls_Yl_Rbb_Yqjc set tmp_riqi='" + s + "' where id='" + id + "'";
                DataBaseHelper.execute(sql);
            }
        }

        //把'\n'转换成'/'，涉及：射孔、压裂施工、钻井数据统计表
        private void fenge()
        {
            string sql = null;
            DataTable dt = null;

            sql = "select id,shekongdingjie,shekongdijie,shekaihoudu,shejidanshu,shifadanshu from Xls_Yl_Rbb_Sk";
            dt = DataBaseHelper.query(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i][0].ToString().Trim();
                string s1 = dt.Rows[i][1].ToString().Trim();
                string s2 = dt.Rows[i][2].ToString().Trim();
                string s3 = dt.Rows[i][3].ToString().Trim();
                string s4 = dt.Rows[i][4].ToString().Trim();
                string s5 = dt.Rows[i][5].ToString().Trim();
                s1 = fix(s1);
                s2 = fix(s2);
                s3 = fix(s3);
                s4 = fix(s4);
                s5 = fix(s5);
                sql = "update Xls_Yl_Rbb_Sk set shekongdingjie='" + s1 + "', shekongdijie='" + s2 + "', shekaihoudu='" + s3 + "', shejidanshu='" + s4 + "', shifadanshu='" + s5 + "' where id='" + id + "'";
                DataBaseHelper.execute(sql);
            }

            sql = "select id,mcjd_dingjie,mcjd_dijie,meicenghoudu,skjd_dingjie,skjd_dijie,shekaihoudu,qzy_sheji,qzy_shiji,xsy_sheji,xsy_shiji,dty_sheji,dty_shiji from Xls_Yl_Rbb_Ylsg";
            dt = DataBaseHelper.query(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i][0].ToString().Trim();
                string s1 = dt.Rows[i][1].ToString().Trim();
                string s2 = dt.Rows[i][2].ToString().Trim();
                string s3 = dt.Rows[i][3].ToString().Trim();
                string s4 = dt.Rows[i][4].ToString().Trim();
                string s5 = dt.Rows[i][5].ToString().Trim();
                string s6 = dt.Rows[i][6].ToString().Trim();
                string s7 = dt.Rows[i][7].ToString().Trim();
                string s8 = dt.Rows[i][8].ToString().Trim();
                string s9 = dt.Rows[i][9].ToString().Trim();
                string s10 = dt.Rows[i][10].ToString().Trim();
                string s11 = dt.Rows[i][11].ToString().Trim();
                string s12 = dt.Rows[i][12].ToString().Trim();
                s1 = fix(s1);
                s2 = fix(s2);
                s3 = fix(s3);
                s4 = fix(s4);
                s5 = fix(s5);
                s6 = fix(s6);
                s7 = fix(s7);
                s8 = fix(s8);
                s9 = fix(s9);
                s10 = fix(s10);
                s11 = fix(s11);
                s12 = fix(s12);
                sql = "update Xls_Yl_Rbb_Ylsg set mcjd_dingjie='" + s1 + "', mcjd_dijie='" + s2 + "', meicenghoudu='" + s3 + "', skjd_dingjie='" + s4 + "', skjd_dijie='" + s5 + "', shekaihoudu='" + s6 + "', qzy_sheji='" + s7 + "', qzy_shiji='" + s8 + "', xsy_sheji='" + s9 + "', xsy_shiji='" + s10 + "', dty_sheji='" + s11 + "', dty_shiji='" + s12 + "' where id='" + id + "'";
                DataBaseHelper.execute(sql);
            }

            sql = "select id,mcsj_3,mcsj_5,mcsj_11,mcsj_qitameiceng,jszlsj_zxd1_zuidalianxu,jszlsj_zxd2_zuidalianxu,xtggjsj_dctg_meicengduanjieguweizhi from Xls_Zj_Rbb_Tjb";
            dt = DataBaseHelper.query(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i][0].ToString().Trim();
                string s1 = dt.Rows[i][1].ToString().Trim();
                string s2 = dt.Rows[i][2].ToString().Trim();
                string s3 = dt.Rows[i][3].ToString().Trim();
                string s4 = dt.Rows[i][4].ToString().Trim();
                string s5 = dt.Rows[i][5].ToString().Trim();
                string s6 = dt.Rows[i][6].ToString().Trim();
                string s7 = dt.Rows[i][7].ToString().Trim();
                s1 = fix(s1);
                s2 = fix(s2);
                s3 = fix(s3);
                s4 = fix(s4);
                s5 = fix(s5);
                s6 = fix(s6);
                s7 = fix(s7);
                sql = "update Xls_Zj_Rbb_Tjb set mcsj_3='" + s1 + "', mcsj_5='" + s2 + "', mcsj_11='" + s3 + "', mcsj_qitameiceng='" + s4 + "', jszlsj_zxd1_zuidalianxu='" + s5 + "', jszlsj_zxd2_zuidalianxu='" + s6 + "', xtggjsj_dctg_meicengduanjieguweizhi='" + s7 + "' where id='" + id + "'";
                DataBaseHelper.execute(sql);
            }
            lblInfo.Text = "处理完成。";
        }

        private string fix(string str)
        {
            string[] ss = str.Split('\n');
            if (ss.Length == 1) return ss[0].Trim();
            string s = "";
            for (int i = 0; i < ss.Length - 1; i++)
            {
                s += ss[i].Trim() + " / ";
            }
            s += ss[ss.Length - 1].Trim();
            return s;
        }

        //修正带回车符的井号
        private void fix_jinghao()
        {
            DataTable dt = DataBaseHelper.query("select * from Xls_Zj_Rbb_Zj where jinghao like '%保德%'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i][0].ToString();
                string jh = dt.Rows[i]["jinghao"].ToString();
                string[] ss = jh.Split('\n');
                jh = ss[0].Trim();
                ss = jh.Split('井');
                jh = ss[0].Trim();
                string sql = "update Xls_Zj_Rbb_Zj set jinghao='" + jh + "' where id='" + id + "'";
                DataBaseHelper.execute(sql);
            }
            lblInfo.Text = "完成。";
        }

        //更新井的状态
        private void updateJingZT()
        {
            string sql = "update jing_jichuxinxi set dangqianzhuangtai=NULL where 1=1";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='交井' where jinghao in (select jinghao from Xls_Yl_Rbb_Xb where yanshouqingkuang like '%已验收%')";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='下泵' where jinghao in (select jinghao from Xls_Yl_Rbb_Xb where shigongneirong like '%下泵%') and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='已压裂' where (jinghao in (select jinghao from Xls_Yl_Rbb_Ylsg where shifouyawan like '%是%') or jinghao in (select jinghao from Xls_Yl_Rbb_Yhpy)) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='未压裂' where (jinghao in (select jinghao from Xls_Yl_Rbb_Ylsg) or jinghao in (select jinghao from Xls_Yl_Rbb_Yqjc) or jinghao in (select jinghao from Xls_Yl_Rbb_Sk)) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='完井' where (jinghao in (select jinghao from Xls_Zj_Rbb_Wj) or jinghao in (select jinghao from Xls_Zj_Rbb_Tjb)) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='固井' where jinghao in (select jinghao from Xls_Zj_Rbb_Gj) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='下套管' where jinghao in (select jinghao from Xls_Zj_Rbb_Xtg) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='钻进' where jinghao in (select jinghao from Xls_Zj_Rbb_Zj) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);
        }

        private void setJichuxinxi()
        {
            string qk = "临汾";
            string sql = "select jinghao,dxsj_jingkouzuobiao_x,dxsj_jingkouzuobiao_y,dxsj_badianzuobiao_x,dxsj_badianzuobiao_y,dxsj_jingkouhaiba,jbsj_jingbie,jbsj_jingxing,jbsj_shejijingshen,shigongduiwu,kaizuanriqi from Xls_Zj_Rbb_Tjb where place='" + qk + "'";
            DataTable dt = DataBaseHelper.query(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jh = dt.Rows[i][0].ToString();

                string x_bd = dt.Rows[i][3].ToString();
                string y_bd = dt.Rows[i][4].ToString();
                if (x_bd == "") x_bd = dt.Rows[i][1].ToString();
                if (y_bd == "") y_bd = dt.Rows[i][2].ToString();

                string jchb = dt.Rows[i][5].ToString();
                string jb = dt.Rows[i][6].ToString();
                string jx = dt.Rows[i][7].ToString();
                string sjjs = dt.Rows[i][8].ToString();
                string dw = dt.Rows[i][9].ToString();
                string kzrq = dt.Rows[i][10].ToString();

                sql = "insert into jing_jichuxinxi values('" + jh + "', '" + x_bd + "', '" + y_bd + "', '" + jchb + "', '" + jb + "', '" + jx + "', '" + sjjs + "', '" + dw + "', '" + kzrq + "', '', '" + qk + "', '', '')";
                DataBaseHelper.execute(sql);
            }

            lblInfo.Text = "完成。";
        }

        private void qcJingzi()
        {
            string tablename = "";
            DataTable dt = DataBaseHelper.query("select id,jinghao from " + tablename);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i][0].ToString();
                string jh = dt.Rows[i]["jinghao"].ToString();
                string[] ss = jh.Split('\n');
                jh = ss[0].Trim();
                ss = jh.Split('井');
                jh = ss[0].Trim();
                string sql = "update " + tablename + " set jinghao='" + jh + "' where id='" + id + "'";
                DataBaseHelper.execute(sql);
            }
            lblInfo.Text = "完成。";
        }
    }
}