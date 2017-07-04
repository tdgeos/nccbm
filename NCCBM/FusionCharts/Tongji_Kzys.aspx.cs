using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.FusionCharts
{
    public partial class Tongji_Kzys : System.Web.UI.Page
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

            lbtnHc.Command += new CommandEventHandler(lbtnHc_Command);
            lbtnLf.Command += new CommandEventHandler(lbtnLf_Command);
            lbtnXz.Command += new CommandEventHandler(lbtnXz_Command);

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

                gv.Attributes.Add("BorderColor", "Black");
                gv.Attributes.Add("BorderWidth", "1");

                tbRiqi.Text = DateTime.Now.ToString("yyyy-MM-dd");
                string strRiqi = tbRiqi.Text;
                string strWeek = MyTools.GetWeek(strRiqi);
                string begin = strWeek.Split(',')[0];
                string end = strWeek.Split(',')[1];

                getData(begin, end);
            }
        }

        private void getData(string begin, string end)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("qukuai");
            dt.Columns.Add("yikai");
            dt.Columns.Add("yikaihege");
            dt.Columns.Add("yikaibu");
            dt.Columns.Add("erkai");
            dt.Columns.Add("erkaihege");
            dt.Columns.Add("erkaibu");

            int[] naQk = new int[4];
            int[] naHc = new int[4];
            int[] naLf = new int[4];
            int[] naXz = new int[4];

            lblRiqi.Text = begin + " 至 " + end;

            if (ddl_zj_qukuai.Text != "全部")
            {
                string qk = ddl_zj_qukuai.SelectedItem.Text;

                System.Data.DataTable dtTemp = null;
                string sql = "select qukuai,jinghao,erkaishijian from Report_yikaiyanshou where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "'";
                dtTemp = DataBaseHelper.query(sql);

                int iYikai = dtTemp.Rows.Count;
                int iErkai = 0;
                for (int i = 0; i < iYikai; i++)
                {
                    if (dtTemp.Rows[i][2].ToString().Trim() != "") iErkai++;
                }

                List<string> lstJing = new List<string>();
                sql = "select jinghao from Xls_Zj_Rbb_Tjb where ykys_riqi between '" + begin + "' and '" + end + "' and place='" + qk + "'";
                dtTemp = DataBaseHelper.query(sql);
                int iYikaiYanshou = dtTemp.Rows.Count;
                for (int i = 0; i < iYikaiYanshou; i++)
                {
                    string jh = dtTemp.Rows[i]["jinghao"].ToString().Trim();
                    bool b = true;
                    for (int j = 0; j < lstJing.Count; j++)
                    {
                        if (jh == lstJing[j])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstJing.Add(jh);
                }
                iYikaiYanshou = lstJing.Count;
                lstJing.Clear();

                sql = "select jinghao from Xls_Zj_Rbb_Tjb where ekys_riqi between '" + begin + "' and '" + end + "' and place='" + qk + "'";
                dtTemp = DataBaseHelper.query(sql);
                int iErkaiYanshou = dtTemp.Rows.Count;
                for (int i = 0; i < iErkaiYanshou; i++)
                {
                    string jh = dtTemp.Rows[i]["jinghao"].ToString().Trim();
                    bool b = true;
                    for (int j = 0; j < lstJing.Count; j++)
                    {
                        if (jh == lstJing[j])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstJing.Add(jh);
                }
                iErkaiYanshou = lstJing.Count;
                lstJing.Clear();

                naQk[0] = iYikaiYanshou;
                naQk[1] = iYikai;
                naQk[2] = iErkaiYanshou;
                naQk[3] = iErkai;

                if (naQk[0] < naQk[1]) naQk[0] = naQk[1];
                if (naQk[2] < naQk[3]) naQk[2] = naQk[3];

                dt.Rows.Add();

                dt.Rows[0][0] = qk;
                dt.Rows[0][1] = naQk[0];
                dt.Rows[0][2] = naQk[0] - naQk[1];
                dt.Rows[0][3] = naQk[1];
                dt.Rows[0][4] = naQk[2];
                dt.Rows[0][5] = naQk[2] - naQk[3];
                dt.Rows[0][6] = naQk[3];

                gv.DataSource = dt;
                gv.DataBind();
            }
            else
            {
                System.Data.DataTable dtTemp = null;
                string sql = "select qukuai,jinghao,erkaishijian from Report_yikaiyanshou where lururiqi between '" + begin + "' and '" + end + "'";
                dtTemp = DataBaseHelper.query(sql);

                int count = dtTemp.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dtTemp.Rows[i][0].ToString().Trim() == "韩城")
                    {
                        if (dtTemp.Rows[i][2].ToString().Trim() != "") naHc[3]++;
                        else naHc[1]++;
                    }
                    if (dtTemp.Rows[i][0].ToString().Trim() == "临汾")
                    {
                        if (dtTemp.Rows[i][2].ToString().Trim() != "") naLf[3]++;
                        else naLf[1]++;
                    }
                    if (dtTemp.Rows[i][0].ToString().Trim() == "忻州")
                    {
                        if (dtTemp.Rows[i][2].ToString().Trim() != "") naXz[3]++;
                        else naXz[1]++;
                    }
                }

                List<string> lstJingHc = new List<string>();
                List<string> lstJingLf = new List<string>();
                List<string> lstJingXz = new List<string>();
                sql = "select jinghao,place from Xls_Zj_Rbb_Tjb where ykys_riqi between '" + begin + "' and '" + end + "'";
                dtTemp = DataBaseHelper.query(sql);
                int iYikaiYanshou = dtTemp.Rows.Count;
                for (int i = 0; i < iYikaiYanshou; i++)
                {
                    if (dtTemp.Rows[i]["place"].ToString().Trim() == "韩城")
                    {
                        string jh = dtTemp.Rows[i]["jinghao"].ToString().Trim();
                        bool b = true;
                        for (int j = 0; j < lstJingHc.Count; j++)
                        {
                            if (jh == lstJingHc[j])
                            {
                                b = false;
                                break;
                            }
                        }
                        if (b) lstJingHc.Add(jh);
                    }
                    if (dtTemp.Rows[i]["place"].ToString().Trim() == "临汾")
                    {
                        string jh = dtTemp.Rows[i]["jinghao"].ToString().Trim();
                        bool b = true;
                        for (int j = 0; j < lstJingLf.Count; j++)
                        {
                            if (jh == lstJingLf[j])
                            {
                                b = false;
                                break;
                            }
                        }
                        if (b) lstJingLf.Add(jh);
                    }
                    if (dtTemp.Rows[i]["place"].ToString().Trim() == "忻州")
                    {
                        string jh = dtTemp.Rows[i]["jinghao"].ToString().Trim();
                        bool b = true;
                        for (int j = 0; j < lstJingXz.Count; j++)
                        {
                            if (jh == lstJingXz[j])
                            {
                                b = false;
                                break;
                            }
                        }
                        if (b) lstJingXz.Add(jh);
                    }
                }
                naHc[0] = lstJingHc.Count;
                naLf[0] = lstJingLf.Count;
                naXz[0] = lstJingXz.Count;
                lstJingHc.Clear();
                lstJingLf.Clear();
                lstJingXz.Clear();

                sql = "select jinghao,place from Xls_Zj_Rbb_Tjb where ekys_riqi between '" + begin + "' and '" + end + "'";
                dtTemp = DataBaseHelper.query(sql);
                int iErkaiYanshou = dtTemp.Rows.Count;
                for (int i = 0; i < iErkaiYanshou; i++)
                {
                    if (dtTemp.Rows[i]["place"].ToString().Trim() == "韩城")
                    {
                        string jh = dtTemp.Rows[i]["jinghao"].ToString().Trim();
                        bool b = true;
                        for (int j = 0; j < lstJingHc.Count; j++)
                        {
                            if (jh == lstJingHc[j])
                            {
                                b = false;
                                break;
                            }
                        }
                        if (b) lstJingHc.Add(jh);
                    }
                    if (dtTemp.Rows[i]["place"].ToString().Trim() == "临汾")
                    {
                        string jh = dtTemp.Rows[i]["jinghao"].ToString().Trim();
                        bool b = true;
                        for (int j = 0; j < lstJingLf.Count; j++)
                        {
                            if (jh == lstJingLf[j])
                            {
                                b = false;
                                break;
                            }
                        }
                        if (b) lstJingLf.Add(jh);
                    }
                    if (dtTemp.Rows[i]["place"].ToString().Trim() == "忻州")
                    {
                        string jh = dtTemp.Rows[i]["jinghao"].ToString().Trim();
                        bool b = true;
                        for (int j = 0; j < lstJingXz.Count; j++)
                        {
                            if (jh == lstJingXz[j])
                            {
                                b = false;
                                break;
                            }
                        }
                        if (b) lstJingXz.Add(jh);
                    }
                }
                naHc[2] = lstJingHc.Count;
                naLf[2] = lstJingLf.Count;
                naXz[2] = lstJingXz.Count;
                lstJingHc.Clear();
                lstJingLf.Clear();
                lstJingXz.Clear();

                if (naHc[0] < naHc[1]) naHc[0] = naHc[1];
                if (naHc[2] < naHc[3]) naHc[2] = naHc[3];
                if (naLf[0] < naLf[1]) naLf[0] = naLf[1];
                if (naLf[2] < naLf[3]) naLf[2] = naLf[3];
                if (naXz[0] < naXz[1]) naXz[0] = naXz[1];
                if (naXz[2] < naXz[3]) naXz[2] = naXz[3];

                dt.Rows.Add();
                dt.Rows.Add();
                dt.Rows.Add();

                dt.Rows[0][0] = "韩城";
                dt.Rows[0][1] = naHc[0];
                dt.Rows[0][2] = naHc[0] - naHc[1];
                dt.Rows[0][3] = naHc[1];
                dt.Rows[0][4] = naHc[2];
                dt.Rows[0][5] = naHc[2] - naHc[3];
                dt.Rows[0][6] = naHc[3];
                dt.Rows[1][0] = "临汾";
                dt.Rows[1][1] = naLf[0];
                dt.Rows[1][2] = naLf[0] - naLf[1];
                dt.Rows[1][3] = naLf[1];
                dt.Rows[1][4] = naLf[2];
                dt.Rows[1][5] = naLf[2] - naLf[3];
                dt.Rows[1][6] = naLf[3];
                dt.Rows[2][0] = "忻州";
                dt.Rows[2][1] = naXz[0];
                dt.Rows[2][2] = naXz[0] - naXz[1];
                dt.Rows[2][3] = naXz[1];
                dt.Rows[2][4] = naXz[2];
                dt.Rows[2][5] = naXz[2] - naXz[3];
                dt.Rows[2][6] = naXz[3];

                gv.DataSource = dt;
                gv.DataBind();
            }

            string data = "";
            if (ddl_zj_qukuai.SelectedItem.Text == "全部")
            {
                data += naHc[0] + "," + (naHc[0]-naHc[1]) + "," + naHc[1] + "," + naHc[2] + "," + (naHc[2]-naHc[3]) + "," + naHc[3] + ";";
                data += naLf[0] + "," + (naLf[0] - naLf[1]) + "," + naLf[1] + "," + naLf[2] + "," + (naLf[2] - naLf[3]) + "," + naLf[3] + ";";
                data += naXz[0] + "," + (naXz[0] - naXz[1]) + "," + naXz[1] + "," + naXz[2] + "," + (naXz[2] - naXz[3]) + "," + naXz[3];
            }
            else
            {
                data += naQk[0] + "," + naQk[1] + "," + (naQk[0] - naHc[1]) + "," + naQk[2] + "," + (naQk[2] - naQk[3]) + "," + naQk[3];
            }
            lblData.Text = data;

            getImageData();
        }

        private void getImageData()
        {
            string riqi = lblRiqi.Text;
            string data = "";
            if (rbXian.Checked)
            {
                lbtnHc.Visible = false;
                lbtnLf.Visible = false;
                lbtnXz.Visible = false;

                if (ddl_zj_qukuai.SelectedItem.Text == "全部")
                {
                    string s = lblData.Text;
                    string[] ss = s.Split(';');
                    string[] sshc = ss[0].Split(',');
                    string[] sslf = ss[1].Split(',');
                    string[] ssxz = ss[2].Split(',');

                    data += "qb,(" + riqi + "),1,";
                    data += "韩城;临汾;忻州,";
                    data += "一开验收总数;" + sshc[0] + ";" + sslf[0] + ";" + ssxz[0] + "$";
                    data += "一开合格数量;" + sshc[1] + ";" + sslf[1] + ";" + ssxz[1] + "$";
                    data += "一开不合格数量;" + sshc[2] + ";" + sslf[2] + ";" + ssxz[2] + "$";
                    data += "二开验收总数;" + sshc[3] + ";" + sslf[3] + ";" + ssxz[3] + "$";
                    data += "二开合格数量;" + sshc[4] + ";" + sslf[4] + ";" + ssxz[4] + "$";
                    data += "二开不合格数量;" + sshc[5] + ";" + sslf[5] + ";" + ssxz[5];
                }
                else
                {
                    string s = lblData.Text;
                    string[] ss = s.Split(',');

                    string sqk = "";
                    if (ddl_zj_qukuai.SelectedItem.Text == "韩城") sqk = "hc";
                    if (ddl_zj_qukuai.SelectedItem.Text == "临汾") sqk = "lf";
                    if (ddl_zj_qukuai.SelectedItem.Text == "忻州") sqk = "xz";
                    data += sqk + ",(" + riqi + "),1,";
                    data += ddl_zj_qukuai.SelectedItem.Text + ",";
                    data += "一开验收总数;" + ss[0] + "$";
                    data += "一开合格数量;" + ss[1] + "$";
                    data += "一开不合格数量;" + ss[2] + "$";
                    data += "二开验收总数;" + ss[3] + "$";
                    data += "二开合格数量;" + ss[4] + "$";
                    data += "二开不合格数量;" + ss[5];
                }
                img.Attributes["src"] = "chartKzys.aspx?data=" + data;
            }
            else if (rbZhu.Checked)
            {
                lbtnHc.Visible = false;
                lbtnLf.Visible = false;
                lbtnXz.Visible = false;

                if (ddl_zj_qukuai.SelectedItem.Text == "全部")
                {
                    string s = lblData.Text;
                    string[] ss = s.Split(';');
                    string[] sshc = ss[0].Split(',');
                    string[] sslf = ss[1].Split(',');
                    string[] ssxz = ss[2].Split(',');

                    data += "qb,(" + riqi + "),0,";
                    data += "一开验收总数;一开合格数量;一开不合格数量;二开验收总数;二开合格数量;二开不合格数量,";
                    data += "韩城;" + sshc[0] + ";" + sshc[1] + ";" + sshc[2] + ";" + sshc[3] + ";" + sshc[4] + ";" + sshc[5] + "$";
                    data += "临汾;" + sslf[0] + ";" + sslf[1] + ";" + sslf[2] + ";" + sslf[3] + ";" + sslf[4] + ";" + sslf[5] + "$";
                    data += "忻州;" + ssxz[0] + ";" + ssxz[1] + ";" + ssxz[2] + ";" + ssxz[3] + ";" + ssxz[4] + ";" + ssxz[5];
                }
                else
                {
                    string s = lblData.Text;
                    string[] ss = s.Split(',');

                    string sqk = "";
                    if (ddl_zj_qukuai.SelectedItem.Text == "韩城") sqk = "hc";
                    if (ddl_zj_qukuai.SelectedItem.Text == "临汾") sqk = "lf";
                    if (ddl_zj_qukuai.SelectedItem.Text == "忻州") sqk = "xz";

                    data += sqk + ",(" + riqi + "),0,";
                    data += "一开验收总数;一开合格数量;一开不合格数量;二开验收总数;二开合格数量;二开不合格数量,";
                    data += ddl_zj_qukuai.SelectedItem.Text + ";" + ss[0] + ";" + ss[1] + ";" + ss[2] + ";" + ss[3] + ";" + ss[4] + ";" + ss[5];
                }
                img.Attributes["src"] = "chartKzys.aspx?data=" + data;
            }
            else
            {
                lbtnHc.Visible = true;
                lbtnLf.Visible = true;
                lbtnXz.Visible = true;
                lbtnHc.ForeColor = System.Drawing.Color.Purple;
                lbtnLf.ForeColor = System.Drawing.Color.Blue;
                lbtnXz.ForeColor = System.Drawing.Color.Blue;
                setBingtu("韩城");
            }
        }

        void lbtnHc_Command(object sender, CommandEventArgs e)
        {
            lbtnHc.ForeColor = System.Drawing.Color.Purple;
            lbtnLf.ForeColor = System.Drawing.Color.Blue;
            lbtnXz.ForeColor = System.Drawing.Color.Blue;
            setBingtu("韩城");
        }

        void lbtnLf_Command(object sender, CommandEventArgs e)
        {
            lbtnHc.ForeColor = System.Drawing.Color.Blue;
            lbtnLf.ForeColor = System.Drawing.Color.Purple;
            lbtnXz.ForeColor = System.Drawing.Color.Blue;
            setBingtu("临汾");
        }

        void lbtnXz_Command(object sender, CommandEventArgs e)
        {
            lbtnHc.ForeColor = System.Drawing.Color.Blue;
            lbtnLf.ForeColor = System.Drawing.Color.Blue;
            lbtnXz.ForeColor = System.Drawing.Color.Purple;
            setBingtu("忻州");
        }

        void setBingtu(string qk)
        {
            string riqi = lblRiqi.Text;
            string data = "";
            string s = lblData.Text;
            string[] ss = null;
            if (ddl_zj_qukuai.SelectedItem.Text == "全部")
            {
                string[] tmp = s.Split(';');
                if (qk == "韩城") ss = tmp[0].Split(',');
                if (qk == "临汾") ss = tmp[1].Split(',');
                if (qk == "忻州") ss = tmp[2].Split(',');
            }
            else
            {
                ss = s.Split(',');
            }
            data += "一开验收总数,一开合格数量,一开不合格数量,二开验收总数,二开合格数量,二开不合格数量;";
            data += ss[0] + "," + ss[1] + "," + ss[2] + "," + ss[3] + "," + ss[4] + "," + ss[5];

            img.Attributes["src"] = "chart.aspx?data=" + data;
        }

        protected void Btn_Zhou_Click(object sender, EventArgs e)
        {
            string strRiqi = tbRiqi.Text;
            string strWeek = MyTools.GetWeek(strRiqi);
            string begin = strWeek.Split(',')[0];
            string end = strWeek.Split(',')[1];

            getData(begin, end);
        }

        protected void Btn_Yue_Click(object sender, EventArgs e)
        {
            string strRiqi = tbRiqi.Text;
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);
            string begin = y + "-" + m + "-01";
            DateTime dtEnd = new DateTime(y, m, MyTools.GetMonthDays(strRiqi));
            string end = null;
            if (dtEnd > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
            else end = y + "-" + m + "-" + MyTools.GetMonthDays(strRiqi);

            getData(begin, end);
        }

        protected void Btn_Ji_Click(object sender, EventArgs e)
        {
            string strRiqi = tbRiqi.Text;
            string begin = MyTools.GetQuarterBegin(strRiqi).ToString("yyyy-MM-dd");
            DateTime dtEnd = MyTools.GetQuarterEnd(strRiqi);
            string end = null;
            if (dtEnd > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
            else end = dtEnd.ToString("yyyy-MM-dd");
            getData(begin, end);
        }

        protected void Btn_Nian_Click(object sender, EventArgs e)
        {
            string strRiqi = tbRiqi.Text;
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);
            string begin = y + "-01-01";
            DateTime dtEnd = new DateTime(y, 12, 31);
            string end = null;
            if (dtEnd > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
            else end = y + "-12-31";

            getData(begin, end);
        }

        protected void rbZhu_CheckedChanged(object sender, EventArgs e)
        {
            getImageData();
        }

        protected void rbXian_CheckedChanged(object sender, EventArgs e)
        {
            getImageData();
        }

        protected void rbBing_CheckedChanged(object sender, EventArgs e)
        {
            getImageData();
        }
    }
}