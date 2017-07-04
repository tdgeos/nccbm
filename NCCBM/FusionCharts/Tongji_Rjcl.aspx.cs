using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.FusionCharts
{
    public partial class Tongji_Rjcl : System.Web.UI.Page
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
            dt.Columns.Add("f100");
            dt.Columns.Add("f90");
            dt.Columns.Add("f80");
            dt.Columns.Add("f70");
            dt.Columns.Add("f60");

            int[] naQk = new int[5];
            int[] naHc = new int[5];
            int[] naLf = new int[5];
            int[] naXz = new int[5];

            lblRiqi.Text = begin + " 至 " + end;

            if (ddl_zj_qukuai.Text != "全部")
            {
                string qk = ddl_zj_qukuai.SelectedItem.Text;
                string sqlTemp = "select jinghao,cengwei,wanchengbaifenbi from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place='" + qk + "' group by jinghao,cengwei,wanchengbaifenbi";
                DataTable dtTemp = DataBaseHelper.query(sqlTemp);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        string str = dtTemp.Rows[i][2].ToString();
                        string s1 = str.Split('％')[0];
                        s1 = s1.Split('%')[0];
                        try
                        {
                            float f = float.Parse(s1);
                            if (f > 100) naQk[0]++;
                            else if (f <= 100 && f > 90) naQk[1]++;
                            else if (f <= 90 && f > 80) naQk[2]++;
                            else if (f <= 80 && f > 70) naQk[3]++;
                            else if (f <= 70) naQk[4]++;
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
                dt.Rows.Add();

                dt.Rows[0][0] = qk;
                dt.Rows[0][1] = naQk[0];
                dt.Rows[0][2] = naQk[1];
                dt.Rows[0][3] = naQk[2];
                dt.Rows[0][4] = naQk[3];
                dt.Rows[0][5] = naQk[4];

                gv.DataSource = dt;
                gv.DataBind();
            }
            else
            {
                string sqlTemp = "select jinghao,cengwei,wanchengbaifenbi from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place='韩城' group by jinghao,cengwei,wanchengbaifenbi";
                DataTable dtTemp = DataBaseHelper.query(sqlTemp);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        string str = dtTemp.Rows[i][2].ToString();
                        string s1 = str.Split('％')[0];
                        s1 = s1.Split('%')[0];
                        try
                        {
                            float f = float.Parse(s1);
                            if (f > 100) naHc[0]++;
                            else if (f <= 100 && f > 90) naHc[1]++;
                            else if (f <= 90 && f > 80) naHc[2]++;
                            else if (f <= 80 && f > 70) naHc[3]++;
                            else if (f <= 70) naHc[4]++;
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
                sqlTemp = "select jinghao,cengwei,wanchengbaifenbi from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place='临汾' group by jinghao,cengwei,wanchengbaifenbi";
                dtTemp = DataBaseHelper.query(sqlTemp);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        string str = dtTemp.Rows[i][2].ToString();
                        string s1 = str.Split('％')[0];
                        s1 = s1.Split('%')[0];
                        try
                        {
                            float f = float.Parse(s1);
                            if (f > 100) naLf[0]++;
                            else if (f <= 100 && f > 90) naLf[1]++;
                            else if (f <= 90 && f > 80) naLf[2]++;
                            else if (f <= 80 && f > 70) naLf[3]++;
                            else if (f <= 70) naLf[4]++;
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
                sqlTemp = "select jinghao,cengwei,wanchengbaifenbi from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place='忻州' group by jinghao,cengwei,wanchengbaifenbi";
                dtTemp = DataBaseHelper.query(sqlTemp);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        string str = dtTemp.Rows[i][2].ToString();
                        string s1 = str.Split('％')[0];
                        s1 = s1.Split('%')[0];
                        try
                        {
                            float f = float.Parse(s1);
                            if (f > 100) naXz[0]++;
                            else if (f <= 100 && f > 90) naXz[1]++;
                            else if (f <= 90 && f > 80) naXz[2]++;
                            else if (f <= 80 && f > 70) naXz[3]++;
                            else if (f <= 70) naXz[4]++;
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }

                dt.Rows.Add();
                dt.Rows.Add();
                dt.Rows.Add();

                dt.Rows[0][0] = "韩城";
                dt.Rows[0][1] = naHc[0];
                dt.Rows[0][2] = naHc[1];
                dt.Rows[0][3] = naHc[2];
                dt.Rows[0][4] = naHc[3];
                dt.Rows[0][5] = naHc[4];
                dt.Rows[1][0] = "临汾";
                dt.Rows[1][1] = naLf[0];
                dt.Rows[1][2] = naLf[1];
                dt.Rows[1][3] = naLf[2];
                dt.Rows[1][4] = naLf[3];
                dt.Rows[1][5] = naLf[4];
                dt.Rows[2][0] = "忻州";
                dt.Rows[2][1] = naXz[0];
                dt.Rows[2][2] = naXz[1];
                dt.Rows[2][3] = naXz[2];
                dt.Rows[2][4] = naXz[3];
                dt.Rows[2][5] = naXz[4];

                gv.DataSource = dt;
                gv.DataBind();
            }

            string data = "";
            if (ddl_zj_qukuai.SelectedItem.Text == "全部")
            {
                data += naHc[0] + "," + naHc[1] + "," + naHc[2] + "," + naHc[3] + "," + naHc[4] + ";";
                data += naLf[0] + "," + naLf[1] + "," + naLf[2] + "," + naLf[3] + "," + naLf[4] + ";";
                data += naXz[0] + "," + naXz[1] + "," + naXz[2] + "," + naXz[3] + "," + naXz[4];
            }
            else
            {
                data += naQk[0] + "," + naQk[1] + "," + naQk[2] + "," + naQk[3] + "," + naQk[4];
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
                    data += "100以上;" + sshc[0] + ";" + sslf[0] + ";" + ssxz[0] + "$";
                    data += "90-100;" + sshc[1] + ";" + sslf[1] + ";" + ssxz[1] + "$";
                    data += "80-90;" + sshc[2] + ";" + sslf[2] + ";" + ssxz[2] + "$";
                    data += "70-80;" + sshc[3] + ";" + sslf[3] + ";" + ssxz[3] + "$";
                    data += "70以下;" + sshc[4] + ";" + sslf[4] + ";" + ssxz[4];
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
                    data += "100以上;" + ss[0] + "$";
                    data += "90-100;" + ss[1] + "$";
                    data += "80-90;" + ss[2] + "$";
                    data += "70-80;" + ss[3] + "$";
                    data += "70以下;" + ss[4];
                }
                img.Attributes["src"] = "chartRjcl.aspx?data=" + data;
            }
            else if(rbZhu.Checked)
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
                    data += "100以上;90-100;80-90;70-80;70以下,";
                    data += "韩城;" + sshc[0] + ";" + sshc[1] + ";" + sshc[2] + ";" + sshc[3] + ";" + sshc[4] + "$";
                    data += "临汾;" + sslf[0] + ";" + sslf[1] + ";" + sslf[2] + ";" + sslf[3] + ";" + sslf[4] + "$";
                    data += "忻州;" + ssxz[0] + ";" + ssxz[1] + ";" + ssxz[2] + ";" + ssxz[3] + ";" + ssxz[4];
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
                    data += "100以上;90-100;80-90;70-80;70以下,";
                    data += ddl_zj_qukuai.SelectedItem.Text + ";" + ss[0] + ";" + ss[1] + ";" + ss[2] + ";" + ss[3] + ";" + ss[4];
                }
                img.Attributes["src"] = "chartRjcl.aspx?data=" + data;
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
            data += "100以上,90-100,80-90,70-80,70以下;";
            data += ss[0] + "," + ss[1] + "," + ss[2] + "," + ss[3] + "," + ss[4];

            img.Attributes["src"] = "chart.aspx?data=" + data;
        }

        protected void rbBing_CheckedChanged(object sender, EventArgs e)
        {
            getImageData();
        }
    }
}