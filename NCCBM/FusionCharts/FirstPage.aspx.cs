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
using System.Text;
using System.Data.SqlClient;
using System.Timers;
using System.Windows.Forms;
using System.Collections.Generic;

namespace NCCBM.FusionCharts
{
    public partial class FirstPage : System.Web.UI.Page
    {
        private int userPlaceId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userPlaceId = Int32.Parse(HttpContext.Current.Session["PlaceID"].ToString());
            }
            catch (NullReferenceException ne)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            if (!IsPostBack)
            {
                if (userPlaceId == 0)
                {
                    ddl_zj_qukuai.Items.Add("全部");
                    ddl_zj_qukuai.Items.Add("韩城");
                    ddl_zj_qukuai.Items.Add("临汾");
                    ddl_zj_qukuai.Items.Add("忻州");
                }
                if (userPlaceId == 1)
                {
                    ddl_zj_qukuai.Items.Add("韩城");
                }
                if (userPlaceId == 2)
                {
                    ddl_zj_qukuai.Items.Add("临汾");
                }
                if (userPlaceId == 3)
                {
                    ddl_zj_qukuai.Items.Add("忻州");
                }

                gv_zj.Attributes.Add("BorderColor", "Black");
                gv_zj.Attributes.Add("BorderWidth", "1");

                gv_yl.Attributes.Add("BorderColor", "Black");
                gv_yl.Attributes.Add("BorderWidth", "1");

                tbRiqi.Text = DateTime.Now.ToString("yyyy-MM-dd");

                Ztqj_Zj("zhou");
                Ztqj_Yl("zhou");
            }
        }

        protected void Btn_Zhou_Click(object sender, EventArgs e)
        {
            Ztqj_Zj("zhou");
            Ztqj_Yl("zhou");
        }

        protected void Btn_Yue_Click(object sender, EventArgs e)
        {
            Ztqj_Zj("yue");
            Ztqj_Yl("yue");
        }

        protected void Btn_Ji_Click(object sender, EventArgs e)
        {
            Ztqj_Zj("ji");
            Ztqj_Yl("ji");
        }

        protected void Btn_Nian_Click(object sender, EventArgs e)
        {
            Ztqj_Zj("nian");
            Ztqj_Yl("nian");
        }

        private void Ztqj_Zj(string type)
        {
            string strRiqi = tbRiqi.Text;
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);
            string begin = "";
            string end = "";

            if (type == "zhou")
            {
                string strWeek = MyTools.GetWeek(strRiqi);
                begin = strWeek.Split(',')[0];
                end = strWeek.Split(',')[1];
            }
            if (type == "yue")
            {
                begin = y + "-" + m + "-01";
                DateTime dtEnd = new DateTime(y, m, MyTools.GetMonthDays(strRiqi));
                if (dtEnd > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
                else end = y + "-" + m + "-" + MyTools.GetMonthDays(strRiqi);
            }
            if (type == "ji")
            {
                begin = MyTools.GetQuarterBegin(strRiqi).ToString("yyyy-MM-dd");
                DateTime dtEnd = MyTools.GetQuarterEnd(strRiqi);
                if (dtEnd > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
                else end = dtEnd.ToString("yyyy-MM-dd");
            }
            if (type == "nian")
            {
                begin = y + "-01-01";
                end = y + "-12-31";
                DateTime riqi = new DateTime(y, 12, 31);
                if (riqi > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
            }

            lblZj.Text = begin + " - " + end;

            string[] field_zhou = new string[7] { "区块", "周开钻", "周开钻验收", "周完钻", "周完井", "动用钻机数", "周进尺" };
            string[] field_yue = new string[7] { "区块", "月开钻", "月开钻验收", "月完钻", "月完井", "动用钻机数", "月进尺" };
            string[] field_ji = new string[7] { "区块", "季开钻", "季开钻验收", "季完钻", "季完井", "动用钻机数", "季进尺" };
            string[] field_nian = new string[7] { "区块", "年开钻", "年开钻验收", "年完钻", "年完井", "动用钻机数", "年进尺" };

            string[] fields = null;
            if (type == "zhou")
            {
                fields = field_zhou;
            }
            if (type == "yue")
            {
                fields = field_yue;
            }
            if (type == "ji")
            {
                fields = field_ji;
            }
            if (type == "nian")
            {
                fields = field_nian;
            }

            DataTable dt = new DataTable();
            for (int i = 0; i < fields.Length; i++)
            {
                DataColumn dc = new DataColumn(fields[i]);
                dt.Columns.Add(dc);
            }

            if (ddl_zj_qukuai.Text != "全部")
            {
                string qk = ddl_zj_qukuai.SelectedItem.Text;
                int cKaizuan = getKauzuan(begin, end, qk, false);
                int cYanshou = getKauzuanYanshou(begin, end, qk, false);
                int cWanzuan = getWanzuan(begin, end, qk, false);
                int cWanjing = getWanjing(begin, end, qk, false);
                int cZuanji = getZuanji(begin, end, qk, false);
                float cJinchi = getJinchi(begin, end, qk, false);

                DataRow dr;
                dr = dt.NewRow();
                dr[0] = qk;
                dr[1] = cKaizuan;
                dr[2] = cYanshou;
                dr[3] = cWanzuan;
                dr[4] = cWanjing;
                dr[5] = cZuanji;
                dr[6] = cJinchi;
                dt.Rows.Add(dr);
            }
            else
            {
                string qk = "韩城";
                int cKaizuan = getKauzuan(begin, end, qk, false);
                int cYanshou = getKauzuanYanshou(begin, end, qk, false);
                int cWanzuan = getWanzuan(begin, end, qk, false);
                int cWanjing = getWanjing(begin, end, qk, false);
                int cZuanji = getZuanji(begin, end, qk, false);
                float cJinchi = getJinchi(begin, end, qk, false);

                DataRow dr;
                dr = dt.NewRow();
                dr[0] = qk;
                dr[1] = cKaizuan;
                dr[2] = cYanshou;
                dr[3] = cWanzuan;
                dr[4] = cWanjing;
                dr[5] = cZuanji;
                dr[6] = cJinchi;
                dt.Rows.Add(dr);

                qk = "临汾";
                cKaizuan = getKauzuan(begin, end, qk, false);
                cYanshou = getKauzuanYanshou(begin, end, qk, false);
                cWanzuan = getWanzuan(begin, end, qk, false);
                cWanjing = getWanjing(begin, end, qk, false);
                cZuanji = getZuanji(begin, end, qk, false);
                cJinchi = getJinchi(begin, end, qk, false);

                dr = dt.NewRow();
                dr[0] = qk;
                dr[1] = cKaizuan;
                dr[2] = cYanshou;
                dr[3] = cWanzuan;
                dr[4] = cWanjing;
                dr[5] = cZuanji;
                dr[6] = cJinchi;
                dt.Rows.Add(dr);

                qk = "忻州";
                cKaizuan = getKauzuan(begin, end, qk, false);
                cYanshou = getKauzuanYanshou(begin, end, qk, false);
                cWanzuan = getWanzuan(begin, end, qk, false);
                cWanjing = getWanjing(begin, end, qk, false);
                cZuanji = getZuanji(begin, end, qk, false);
                cJinchi = getJinchi(begin, end, qk, false);

                dr = dt.NewRow();
                dr[0] = qk;
                dr[1] = cKaizuan;
                dr[2] = cYanshou;
                dr[3] = cWanzuan;
                dr[4] = cWanjing;
                dr[5] = cZuanji;
                dr[6] = cJinchi;
                dt.Rows.Add(dr);
            }

            gv_zj.DataSource = dt;
            gv_zj.DataBind();
        }

        private void Ztqj_Yl(string type)
        {
            string strRiqi = tbRiqi.Text;
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);
            string begin = "";
            string end = "";

            if (type == "zhou")
            {
                string strWeek = MyTools.GetWeek(strRiqi);
                begin = strWeek.Split(',')[0];
                end = strWeek.Split(',')[1];
            }
            if (type == "yue")
            {
                begin = y + "-" + m + "-01";
                DateTime dtEnd = new DateTime(y, m, MyTools.GetMonthDays(strRiqi));
                if (dtEnd > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
                else end = y + "-" + m + "-" + MyTools.GetMonthDays(strRiqi);
            }
            if (type == "ji")
            {
                begin = MyTools.GetQuarterBegin(strRiqi).ToString("yyyy-MM-dd");
                DateTime dtEnd = MyTools.GetQuarterEnd(strRiqi);
                if (dtEnd > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
                else end = dtEnd.ToString("yyyy-MM-dd");
            }
            if (type == "nian")
            {
                begin = y + "-01-01";
                end = y + "-12-31";
                DateTime riqi1 = new DateTime(y, 12, 31);
                if (riqi1 > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
            }

            string nianBegin = y + "-01-01";
            string nianEnd = y + "-12-31";
            DateTime riqi2 = new DateTime(y, 12, 31);
            if (riqi2 > DateTime.Now) nianEnd = DateTime.Now.ToString("yyyy-MM-dd");

            lblYl.Text = begin + " - " + end;

            string[] field_zhou = new string[8] { "区块", "周压裂完层数", "周压裂施工井数", "周压裂完井数", "年压裂施工次数", "年压裂层数", "年压裂施工井数", "全年成功率" };
            string[] field_yue = new string[8] { "区块", "月压裂完层数", "月压裂施工井数", "月压裂完井数", "年压裂施工次数", "年压裂层数", "年压裂施工井数", "全年成功率" };
            string[] field_ji = new string[8] { "区块", "季压裂完层数", "季压裂施工井数", "季压裂完井数", "年压裂施工次数", "年压裂层数", "年压裂施工井数", "全年成功率" };
            string[] field_nian = new string[7] { "区块", "年压裂层数", "年压裂完层数", "年压裂井数", "年压裂完井数", "年压裂施工次数", "全年成功率" };

            string[] fields = null;
            if (type == "zhou")
            {
                fields = field_zhou;
            }
            if (type == "yue")
            {
                fields = field_yue;
            }
            if (type == "ji")
            {
                fields = field_ji;
            }
            if (type == "nian")
            {
                fields = field_nian;
            }

            DataTable dt = new DataTable();
            for (int i = 0; i < fields.Length; i++)
            {
                DataColumn dc = new DataColumn(fields[i]);
                dt.Columns.Add(dc);
            }

            if (ddl_zj_qukuai.Text != "全部")
            {
                string qk = ddl_zj_qukuai.SelectedItem.Text;
                int cYaliewanCengshu = getYaliewanCengshu(begin, end, qk, false);
                int cYalieJingshu = getYalieJingshu(begin, end, qk, false);
                int cYaliewanJingshu = getYaliewanJingshu(begin, end, qk, false);
                int cNYalieCishu = getYalieCishu(nianBegin, nianEnd, qk, false);
                int cNYalieCengshu = getYalieCengshu(nianBegin, nianEnd, qk, false);
                int cNYalieJingshu = getYalieJingshu(nianBegin, nianEnd, qk, false);
                string cgl = "/";
                if (cNYalieCishu > 0) cgl = String.Format("{0:N2}", cNYalieCengshu * 100.0f / cNYalieCishu) + "%";

                DataRow dr;
                dr = dt.NewRow();
                if (type != "nian")
                {
                    dr[0] = qk;
                    dr[1] = cYaliewanCengshu;
                    dr[2] = cYaliewanJingshu;
                    dr[3] = cYalieJingshu;
                    dr[4] = cNYalieCishu;
                    dr[5] = cNYalieCengshu;
                    dr[6] = cNYalieJingshu;
                    dr[7] = cgl;
                }
                else
                {
                    dr[0] = qk;
                    dr[1] = cNYalieCengshu;
                    dr[2] = cYaliewanCengshu;
                    dr[3] = cYalieJingshu;
                    dr[4] = cYaliewanJingshu;
                    dr[5] = cNYalieCishu;
                    dr[6] = cgl;
                }
                dt.Rows.Add(dr);
            }
            else
            {
                string qk = "韩城";
                int cYaliewanCengshu = getYaliewanCengshu(begin, end, qk, false);
                int cYalieJingshu = getYalieJingshu(begin, end, qk, false);
                int cYaliewanJingshu = getYaliewanJingshu(begin, end, qk, false);
                int cNYalieCishu = getYalieCishu(nianBegin, nianEnd, qk, false);
                int cNYalieCengshu = getYalieCengshu(nianBegin, nianEnd, qk, false);
                int cNYalieJingshu = getYalieJingshu(nianBegin, nianEnd, qk, false);
                string cgl = "/";
                if (cNYalieCishu > 0) cgl = String.Format("{0:N2}", cNYalieCengshu * 100.0f / cNYalieCishu) + "%";

                DataRow dr;
                dr = dt.NewRow();
                if (type != "nian")
                {
                    dr[0] = qk;
                    dr[1] = cYaliewanCengshu;
                    dr[2] = cYaliewanJingshu;
                    dr[3] = cYalieJingshu;
                    dr[4] = cNYalieCishu;
                    dr[5] = cNYalieCengshu;
                    dr[6] = cNYalieJingshu;
                    dr[7] = cgl;
                }
                else
                {
                    dr[0] = qk;
                    dr[1] = cNYalieCengshu;
                    dr[2] = cYaliewanCengshu;
                    dr[3] = cYalieJingshu;
                    dr[4] = cYaliewanJingshu;
                    dr[5] = cNYalieCishu;
                    dr[6] = cgl;
                }
                dt.Rows.Add(dr);

                qk = "临汾";
                cYaliewanCengshu = getYaliewanCengshu(begin, end, qk, false);
                cYalieJingshu = getYalieJingshu(begin, end, qk, false);
                cYaliewanJingshu = getYaliewanJingshu(begin, end, qk, false);
                cNYalieCishu = getYalieCishu(nianBegin, nianEnd, qk, false);
                cNYalieCengshu = getYalieCengshu(nianBegin, nianEnd, qk, false);
                cNYalieJingshu = getYalieJingshu(nianBegin, nianEnd, qk, false);
                cgl = "/";
                if (cNYalieCishu > 0) cgl = String.Format("{0:N2}", cNYalieCengshu * 100.0f / cNYalieCishu) + "%";

                dr = dt.NewRow();
                if (type != "nian")
                {
                    dr[0] = qk;
                    dr[1] = cYaliewanCengshu;
                    dr[2] = cYaliewanJingshu;
                    dr[3] = cYalieJingshu;
                    dr[4] = cNYalieCishu;
                    dr[5] = cNYalieCengshu;
                    dr[6] = cNYalieJingshu;
                    dr[7] = cgl;
                }
                else
                {
                    dr[0] = qk;
                    dr[1] = cNYalieCengshu;
                    dr[2] = cYaliewanCengshu;
                    dr[3] = cYalieJingshu;
                    dr[4] = cYaliewanJingshu;
                    dr[5] = cNYalieCishu;
                    dr[6] = cgl;
                }
                dt.Rows.Add(dr);

                qk = "忻州";
                cYaliewanCengshu = getYaliewanCengshu(begin, end, qk, false);
                cYalieJingshu = getYalieJingshu(begin, end, qk, false);
                cYaliewanJingshu = getYaliewanJingshu(begin, end, qk, false);
                cNYalieCishu = getYalieCishu(nianBegin, nianEnd, qk, false);
                cNYalieCengshu = getYalieCengshu(nianBegin, nianEnd, qk, false);
                cNYalieJingshu = getYalieJingshu(nianBegin, nianEnd, qk, false);
                cgl = "/";
                if (cNYalieCishu > 0) cgl = String.Format("{0:N2}", cNYalieCengshu * 100.0f / cNYalieCishu) + "%";

                dr = dt.NewRow();
                if (type != "nian")
                {
                    dr[0] = qk;
                    dr[1] = cYaliewanCengshu;
                    dr[2] = cYaliewanJingshu;
                    dr[3] = cYalieJingshu;
                    dr[4] = cNYalieCishu;
                    dr[5] = cNYalieCengshu;
                    dr[6] = cNYalieJingshu;
                    dr[7] = cgl;
                }
                else
                {
                    dr[0] = qk;
                    dr[1] = cNYalieCengshu;
                    dr[2] = cYaliewanCengshu;
                    dr[3] = cYalieJingshu;
                    dr[4] = cYaliewanJingshu;
                    dr[5] = cNYalieCishu;
                    dr[6] = cgl;
                }
                dt.Rows.Add(dr);
            }

            gv_yl.DataSource = dt;
            gv_yl.DataBind();
        }

        private int getKauzuan(string begin, string end, string place, bool jingbie)
        {
            List<string[]> lstRecords = new List<string[]>();
            List<string[]> lstOldTemp = new List<string[]>();
            List<string[]> lstNowTemp = new List<string[]>();
            string sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Tjb where place='" + place + "' and kaizuanriqi between '" + begin + "' and '" + end + "'";
            DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstNowTemp.Count; j++)
                    {
                        if (recTemp[0] == lstNowTemp[j][0] && recTemp[1] == lstNowTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstNowTemp.Add(recTemp);
                }
            }
            sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Zj where place='" + place + "' and kaizuanriqi between '" + begin + "' and '" + end + "'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstOldTemp.Count; j++)
                    {
                        if (recTemp[0] == lstOldTemp[j][0] && recTemp[1] == lstOldTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstOldTemp.Add(recTemp);
                }
            }
            for (int i = 0; i < lstNowTemp.Count; i++)
            {
                bool b = true;
                for (int j = 0; j < lstOldTemp.Count; j++)
                {
                    if (lstOldTemp[j][0] == lstNowTemp[i][0] &&
                        lstOldTemp[j][1] == lstNowTemp[i][1])
                    {
                        b = false;
                        break;
                    }
                }
                if (b) lstRecords.Add(lstNowTemp[i]);
            }
            for (int j = 0; j < lstOldTemp.Count; j++)
            {
                lstRecords.Add(lstOldTemp[j]);
            }
            int cKaizuan = lstRecords.Count;
            return cKaizuan;
        }

        private int getKauzuanYanshou(string begin, string end, string place, bool jingbie)
        {
            string sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Tjb where place='" + place + "' and (ykys_riqi between '" + begin + "' and '" + end + "' or ekys_riqi between '" + begin + "' and '" + end + "')";
            DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            int cYanshou = dtTemp.Rows.Count;
            return cYanshou;
        }

        private int getWanzuan(string begin, string end, string place, bool jingbie)
        {
            List<string[]> lstRecords = new List<string[]>();
            List<string[]> lstOldTemp = new List<string[]>();
            List<string[]> lstNowTemp = new List<string[]>();
            string sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Tjb where place='" + place + "' and wanzuanriqi between '" + begin + "' and '" + end + "'";
            DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstNowTemp.Count; j++)
                    {
                        if (recTemp[0] == lstNowTemp[j][0] && recTemp[1] == lstNowTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstNowTemp.Add(recTemp);
                }
            }
            sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Xtg where place='" + place + "' and wanzuanriqi between '" + begin + "' and '" + end + "'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstOldTemp.Count; j++)
                    {
                        if (recTemp[0] == lstOldTemp[j][0] && recTemp[1] == lstOldTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstOldTemp.Add(recTemp);
                }
            }
            for (int i = 0; i < lstNowTemp.Count; i++)
            {
                bool b = true;
                for (int j = 0; j < lstOldTemp.Count; j++)
                {
                    if (lstOldTemp[j][0] == lstNowTemp[i][0] &&
                        lstOldTemp[j][1] == lstNowTemp[i][1])
                    {
                        b = false;
                        break;
                    }
                }
                if (b) lstRecords.Add(lstNowTemp[i]);
            }
            for (int j = 0; j < lstOldTemp.Count; j++)
            {
                lstRecords.Add(lstOldTemp[j]);
            }
            int cWanzuan = lstRecords.Count;
            return cWanzuan;
        }

        private int getWanjing(string begin, string end, string place, bool jingbie)
        {
            List<string[]> lstRecords = new List<string[]>();
            List<string[]> lstOldTemp = new List<string[]>();
            List<string[]> lstNowTemp = new List<string[]>();
            string sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Tjb where place='" + place + "' and wanjingshijian between '" + begin + "' and '" + end + "'";
            DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstNowTemp.Count; j++)
                    {
                        if (recTemp[0] == lstNowTemp[j][0] && recTemp[1] == lstNowTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstNowTemp.Add(recTemp);
                }
            }
            sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Wj where place='" + place + "' and riqi between '" + begin + "' and '" + end + "'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstOldTemp.Count; j++)
                    {
                        if (recTemp[0] == lstOldTemp[j][0] && recTemp[1] == lstOldTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstOldTemp.Add(recTemp);
                }
            }
            for (int i = 0; i < lstNowTemp.Count; i++)
            {
                bool b = true;
                for (int j = 0; j < lstOldTemp.Count; j++)
                {
                    if (lstOldTemp[j][0] == lstNowTemp[i][0] &&
                        lstOldTemp[j][1] == lstNowTemp[i][1])
                    {
                        b = false;
                        break;
                    }
                }
                if (b) lstRecords.Add(lstNowTemp[i]);
            }
            for (int j = 0; j < lstOldTemp.Count; j++)
            {
                lstRecords.Add(lstOldTemp[j]);
            }
            int cWanjing = lstRecords.Count;
            return cWanjing;
        }

        private int getYalieCishu(string begin, string end, string place, bool jingbie)
        {
            string sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where place='" + place + "' and shigongriqi between '" + begin + "' and '" + end + "'";
            DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            int cCishu = dtTemp.Rows.Count;
            return cCishu;
        }

        private int getYalieCengshu(string begin, string end, string place, bool jingbie)
        {
            List<string[]> lstRecords = new List<string[]>();
            string sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where place='" + place + "' and shigongriqi between '" + begin + "' and '" + end + "'";
            DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["jinghao"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["cengwei"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstRecords.Count; j++)
                    {
                        if (recTemp[0] == lstRecords[j][0] && recTemp[1] == lstRecords[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstRecords.Add(recTemp);
                }
            }
            int cCengshu = lstRecords.Count;
            return cCengshu;
        }

        private int getYaliewanCengshu(string begin, string end, string place, bool jingbie)
        {
            List<string[]> lstRecords = new List<string[]>();
            string sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shifouhege like '%是%' and place='" + place + "' and shigongriqi between '" + begin + "' and '" + end + "'";
            DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["jinghao"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["cengwei"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstRecords.Count; j++)
                    {
                        if (recTemp[0] == lstRecords[j][0] && recTemp[1] == lstRecords[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstRecords.Add(recTemp);
                }
            }
            int cCengshu = lstRecords.Count;
            return cCengshu;
        }

        private int getYalieJingshu(string begin, string end, string place, bool jingbie)
        {
            List<string> lstRecords = new List<string>();
            string sqlTemp = "select jinghao from Xls_Yl_Rbb_Ylsg where place='" + place + "' and  shigongriqi between '" + begin + "' and '" + end + "' order by shigongriqi desc";
            DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string str = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstRecords.Count; j++)
                    {
                        if (str == lstRecords[j])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstRecords.Add(str);
                }
            }
            int cJingshu = lstRecords.Count;
            return cJingshu;
        }

        private int getYaliewanJingshu(string begin, string end, string place, bool jingbie)
        {
            List<string> lstRecords = new List<string>();
            string sqlTemp = "select jinghao from Xls_Yl_Rbb_Ylsg where place='" + place + "' and  shigongriqi between '" + begin + "' and '" + end + "' and shifouyawan like '%是%' order by shigongriqi desc";
            DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string recTemp = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstRecords.Count; j++)
                    {
                        if (recTemp == lstRecords[j])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstRecords.Add(recTemp);
                }
            }
            int cYawan = lstRecords.Count;
            return cYawan;
        }

        private int getZuanji(string begin, string end, string place, bool jingbie)
        {
            string sqlTemp = null;
            DataTable dtTemp = null;

            sqlTemp = "select duihao from Xls_Zj_Rbb_Zj where place='" + place + "' and riqi >= '" + begin + "' and riqi <= '" + end + "' group by duihao";
            dtTemp = DataBaseHelper.query(sqlTemp);
            return dtTemp.Rows.Count;
        }

        private float getJinchi(string begin, string end, string place, bool jingbie)
        {
            string sqlTemp = "select SUM(dangrijinchi) as 'jinchi' from Xls_Zj_Rbb_Zj where place='" + place + "' and riqi between '" + begin + "' and '" + end + "'";
            DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            float cJinchi = 0;
            if (dtTemp.Rows.Count > 0)
            {
                string values = dtTemp.Rows[0]["jinchi"].ToString();
                try
                {
                    cJinchi = float.Parse(values);
                }
                catch (System.Exception e)
                {

                }
            }
            return cJinchi;
        }
    }
}