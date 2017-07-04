using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.FusionCharts
{
    public partial class Tongji_Gj : System.Web.UI.Page
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
            dt.Columns.Add("gj");
            dt.Columns.Add("yichang");

            int[] naQk = new int[2];
            int[] naHc = new int[2];
            int[] naLf = new int[2];
            int[] naXz = new int[2];

            lblRiqi.Text = begin + " 至 " + end;

            if (ddl_zj_qukuai.Text != "全部")
            {
                string qk = ddl_zj_qukuai.SelectedItem.Text;

                List<string[]> lstTemp = new List<string[]>();
                string sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Gj where riqi between '" + begin + "' and '" + end + "' and place='" + qk + "'";
                DataTable dtTemp = DataBaseHelper.query(sqlTemp);
                if (dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        string[] recTemp = new string[2];
                        recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                        recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                        bool b = true;
                        for (int j = 0; j < lstTemp.Count; j++)
                        {
                            if (recTemp[0] == lstTemp[j][0] && recTemp[1] == lstTemp[j][1])
                            {
                                b = false;
                                break;
                            }
                        }
                        if (b) lstTemp.Add(recTemp);
                    }
                }
                naQk[0] = lstTemp.Count;
                lstTemp.Clear();

                sqlTemp = "select qukuai,jinghao from Report_gjzuoye where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "'";
                dtTemp = DataBaseHelper.query(sqlTemp);
                if (dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        string[] recTemp = new string[2];
                        recTemp[0] = dtTemp.Rows[i]["qukuai"].ToString();
                        recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                        bool b = true;
                        for (int j = 0; j < lstTemp.Count; j++)
                        {
                            if (recTemp[0] == lstTemp[j][0] && recTemp[1] == lstTemp[j][1])
                            {
                                b = false;
                                break;
                            }
                        }
                        if (b) lstTemp.Add(recTemp);
                    }
                }
                naQk[1] = lstTemp.Count;
                lstTemp.Clear();

                dt.Rows.Add();

                dt.Rows[0][0] = qk;
                dt.Rows[0][1] = naQk[0];
                dt.Rows[0][2] = naQk[1];

                gv.DataSource = dt;
                gv.DataBind();
            }
            else
            {
                List<string[]> lstTemp = new List<string[]>();
                string sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Gj where riqi between '" + begin + "' and '" + end + "'";
                DataTable dtTemp = DataBaseHelper.query(sqlTemp);
                if (dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        string[] recTemp = new string[2];
                        recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                        recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                        bool b = true;
                        for (int j = 0; j < lstTemp.Count; j++)
                        {
                            if (recTemp[0] == lstTemp[j][0] && recTemp[1] == lstTemp[j][1])
                            {
                                b = false;
                                break;
                            }
                        }
                        if (b) lstTemp.Add(recTemp);
                    }
                }

                for (int i = 0; i < lstTemp.Count; i++)
                {
                    if (lstTemp[i][0] == "韩城") naHc[0]++;
                    if (lstTemp[i][0] == "临汾") naLf[0]++;
                    if (lstTemp[i][0] == "忻州") naXz[0]++;
                }
                lstTemp.Clear();

                sqlTemp = "select qukuai,jinghao from Report_gjzuoye where lururiqi between '" + begin + "' and '" + end + "'";
                dtTemp = DataBaseHelper.query(sqlTemp);
                if (dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        string[] recTemp = new string[2];
                        recTemp[0] = dtTemp.Rows[i]["qukuai"].ToString();
                        recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                        bool b = true;
                        for (int j = 0; j < lstTemp.Count; j++)
                        {
                            if (recTemp[0] == lstTemp[j][0] && recTemp[1] == lstTemp[j][1])
                            {
                                b = false;
                                break;
                            }
                        }
                        if (b) lstTemp.Add(recTemp);
                    }
                }

                for (int i = 0; i < lstTemp.Count; i++)
                {
                    if (lstTemp[i][0] == "韩城") naHc[1]++;
                    if (lstTemp[i][0] == "临汾") naLf[1]++;
                    if (lstTemp[i][0] == "忻州") naXz[1]++;
                }
                lstTemp.Clear();

                dt.Rows.Add();
                dt.Rows.Add();
                dt.Rows.Add();

                dt.Rows[0][0] = "韩城";
                dt.Rows[0][1] = naHc[0];
                dt.Rows[0][2] = naHc[1];
                dt.Rows[1][0] = "临汾";
                dt.Rows[1][1] = naLf[0];
                dt.Rows[1][2] = naLf[1];
                dt.Rows[2][0] = "忻州";
                dt.Rows[2][1] = naXz[0];
                dt.Rows[2][2] = naXz[1];

                gv.DataSource = dt;
                gv.DataBind();
            }

            string data = "";
            if (ddl_zj_qukuai.SelectedItem.Text == "全部")
            {
                data += naHc[0] + "," + naHc[1] + ";";
                data += naLf[0] + "," + naLf[1] + ";";
                data += naXz[0] + "," + naXz[1];
            }
            else
            {
                data += naQk[0] + "," + naQk[1];
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
                    data += "固井监督井数;" + sshc[0] + ";" + sslf[0] + ";" + ssxz[0] + "$";
                    data += "异常施工井数;" + sshc[1] + ";" + sslf[1] + ";" + ssxz[1];
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
                    data += "固井监督井数;" + ss[0] + "$";
                    data += "异常施工井数;" + ss[1];
                }
                img.Attributes["src"] = "chartGjzy.aspx?data=" + data;
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
                    data += "固井监督井数;异常施工井数,";
                    data += "韩城;" + sshc[0] + ";" + sshc[1] + "$";
                    data += "临汾;" + sslf[0] + ";" + sslf[1] + "$";
                    data += "忻州;" + ssxz[0] + ";" + ssxz[1];
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
                    data += "固井监督井数;异常施工井数,";
                    data += ddl_zj_qukuai.SelectedItem.Text + ";" + ss[0] + ";" + ss[1];
                }
                img.Attributes["src"] = "chartGjzy.aspx?data=" + data;
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
            data += "固井监督井数,异常施工井数;";
            data += ss[0] + "," + ss[1];

            img.Attributes["src"] = "chart.aspx?data=" + data;
        }

        protected void rbBing_CheckedChanged(object sender, EventArgs e)
        {
            getImageData();
        }
    }
}