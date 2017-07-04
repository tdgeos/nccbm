using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.FusionCharts
{
    public partial class Tongji_dbfx : System.Web.UI.Page
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

            rbBing.Visible = false;

            lbtnHc.Command += new CommandEventHandler(lbtnHc_Command);
            lbtnLf.Command += new CommandEventHandler(lbtnLf_Command);
            lbtnXz.Command += new CommandEventHandler(lbtnXz_Command);

            if (!IsPostBack)
            {
                if (userPlaceId == 0)
                {
                    ddlQukuai.Items.Add("韩城");
                    ddlQukuai.Items.Add("临汾");
                    ddlQukuai.Items.Add("忻州");
                }
                if (userPlaceId == 1)
                {
                    ddlQukuai.Items.Add("韩城");
                }
                if (userPlaceId == 2)
                {
                    ddlQukuai.Items.Add("临汾");
                }
                if (userPlaceId == 3)
                {
                    ddlQukuai.Items.Add("忻州");
                }

                int nian = System.DateTime.Now.Year;
                for (int i = 2012; i <=nian; i++)
                {
                    ddlBegin.Items.Add(i.ToString());
                    ddlEnd.Items.Add(i.ToString());
                }
                ddlEnd.SelectedIndex = ddlEnd.Items.Count - 1;

                gv.Attributes.Add("BorderColor", "Black");
                gv.Attributes.Add("BorderWidth", "1");

                string begin = ddlBegin.SelectedItem.Text;
                string end = ddlEnd.SelectedItem.Text;
                int a = Int32.Parse(begin);
                int b = Int32.Parse(end);

                getData(a, b);
            }
        }

        private void getData2(int a, int b)
        {
            if (b <= a) return;
            DataTable dt = new DataTable();
            dt.Columns.Add("niandu");
            dt.Columns.Add("kzys");
            dt.Columns.Add("jszl");
            dt.Columns.Add("xtgzy");
            dt.Columns.Add("gjzy");
            dt.Columns.Add("cljl");
            dt.Columns.Add("clys");

            lblRiqi.Text = a + " 至 " + b;

            int iRows = b - a + 1;

            int[,] datass = new int[iRows, 7];
            string data = "";

            for (int i = 0; i < iRows; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = a + i;
                data += dr[0].ToString() + ",";

                string begin = (a + i) + "-01-01";
                string end = (a + i) + "-12-31";
                string qk = ddlQukuai.SelectedItem.Text;
                string sqlTemp = "select jinghao from Report_yikaiyanshou where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "'";
                DataTable dtTemp = DataBaseHelper.query(sqlTemp);
                dr[1] = dtTemp.Rows.Count;
                data += dr[1].ToString() + ",";

                sqlTemp = "select jinghao from Report_jingshenzhiliang where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "'";
                dtTemp = DataBaseHelper.query(sqlTemp);
                dr[2] = dtTemp.Rows.Count;
                data += dr[2].ToString() + ",";

                sqlTemp = "select jinghao from Report_xtgzuoye where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "'";
                dtTemp = DataBaseHelper.query(sqlTemp);
                dr[3] = dtTemp.Rows.Count;
                data += dr[3].ToString() + ",";

                sqlTemp = "select jinghao from Report_gjzuoye where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "'";
                dtTemp = DataBaseHelper.query(sqlTemp);
                dr[4] = dtTemp.Rows.Count;
                data += dr[4].ToString() + ",";

                sqlTemp = "select jinghao from Report_jinglou where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "'";
                dtTemp = DataBaseHelper.query(sqlTemp);
                dr[5] = dtTemp.Rows.Count;
                data += dr[5].ToString() + ",";

                sqlTemp = "select jinghao from Report_yongshui where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "'";
                dtTemp = DataBaseHelper.query(sqlTemp);
                dr[6] = dtTemp.Rows.Count;
                if (i == iRows - 1) data += dr[6].ToString();
                else data += dr[6].ToString() + ";";

                dt.Rows.Add(dr);
            }

            gv.DataSource = dt;
            gv.DataBind();

            lblData.Text = data;

            getImageData();
        }

        private void getData(int a, int b)
        {
            if (b <= a) return;
            DataTable dt = new DataTable();
            dt.Columns.Add("niandu");
            dt.Columns.Add("zong");
            dt.Columns.Add("kzys");
            dt.Columns.Add("jszl");
            dt.Columns.Add("xtgzy");
            dt.Columns.Add("gjzy");
            dt.Columns.Add("cljl");
            dt.Columns.Add("clys");

            lblRiqi.Text = a + " 至 " + b;

            int iRows = b - a + 1;

            int[,] datass = new int[iRows, 7];
            string data = "";

            for (int i = 0; i < iRows; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = a + i;
                data += dr[0].ToString() + ",";

                string begin = (a + i) + "-01-01";
                string end = (a + i) + "-12-31";
                string qk = ddlQukuai.SelectedItem.Text;

                string sqlTemp = null;
                DataTable dtTemp = null;

                List<string> lstRecords = new List<string>();
                List<string> lstOldTemp = new List<string>();
                sqlTemp = "select jinghao from Xls_Zj_Rbb_Tjb where wanzuanriqi >= '" + begin + "' and kaizuanriqi <= '" + end + "' and place='" + qk + "' group by jinghao";
                dtTemp = DataBaseHelper.query(sqlTemp);
                for (int j = 0; j < dtTemp.Rows.Count; j++)
                {
                    lstRecords.Add(dtTemp.Rows[j][0].ToString());
                }
                sqlTemp = "select jinghao from Xls_Zj_Rbb_Tjb where wanjingshijian between '" + begin + "' and '" + end + "' and place='" + qk + "' group by jinghao";
                dtTemp = DataBaseHelper.query(sqlTemp);
                for (int j = 0; j < dtTemp.Rows.Count; j++)
                {
                    string s = dtTemp.Rows[j][0].ToString();
                    bool k = true;
                    for (int n = 0; n < lstRecords.Count; n++)
                    {
                        if (s == lstRecords[n])
                        {
                            k = false;
                            break;
                        }
                    }
                    if (k) lstRecords.Add(s);
                }
                sqlTemp = "select jinghao from Xls_Zj_Rbb_Wj where riqi between '" + begin + "' and '" + end + "' and place='" + qk + "' group by jinghao";
                dtTemp = DataBaseHelper.query(sqlTemp);
                for (int j = 0; j < dtTemp.Rows.Count; j++)
                {
                    string s = dtTemp.Rows[j][0].ToString();
                    bool k = true;
                    for (int n = 0; n < lstRecords.Count; n++)
                    {
                        if (s == lstRecords[n])
                        {
                            k = false;
                            break;
                        }
                    }
                    if (k) lstRecords.Add(s);
                }
                dr[1] = lstRecords.Count;
                data += dr[1].ToString() + ",";

                sqlTemp = "select jinghao from Report_yikaiyanshou where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "' group by jinghao";
                dtTemp = DataBaseHelper.query(sqlTemp);
                dr[2] = dtTemp.Rows.Count;
                data += dr[2].ToString() + ",";

                sqlTemp = "select jinghao from Report_jingshenzhiliang where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "' group by jinghao";
                dtTemp = DataBaseHelper.query(sqlTemp);
                dr[3] = dtTemp.Rows.Count;
                data += dr[3].ToString() + ",";

                sqlTemp = "select jinghao from Report_xtgzuoye where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "' group by jinghao";
                dtTemp = DataBaseHelper.query(sqlTemp);
                dr[4] = dtTemp.Rows.Count;
                data += dr[4].ToString() + ",";

                sqlTemp = "select jinghao from Report_gjzuoye where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "' group by jinghao";
                dtTemp = DataBaseHelper.query(sqlTemp);
                dr[5] = dtTemp.Rows.Count;
                data += dr[5].ToString() + ",";

                sqlTemp = "select jinghao from Report_jinglou where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "' group by jinghao";
                dtTemp = DataBaseHelper.query(sqlTemp);
                dr[6] = dtTemp.Rows.Count;
                data += dr[6].ToString() + ",";

                sqlTemp = "select jinghao from Report_yongshui where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "' group by jinghao";
                dtTemp = DataBaseHelper.query(sqlTemp);
                dr[7] = dtTemp.Rows.Count;
                if(i == iRows-1) data += dr[7].ToString();
                else data += dr[7].ToString() + ";";

                dt.Rows.Add(dr);
            }

            gv.DataSource = dt;
            gv.DataBind();

            lblData.Text = data;

            getImageData();
        }

        private void getImageData()
        {
            string riqi = lblRiqi.Text;
            string data = "";
            int iQk = ddlQukuai.SelectedIndex;
            string title = "";
            if (iQk == 0) title = "韩城区块对比分析";
            if (iQk == 1) title = "临汾区块对比分析";
            if (iQk == 2) title = "忻州区块对比分析";

            string[] cols = new string[7] { "完成钻井总数", "开钻验收不合格数量", "井身质量超标数量", "下套管作业施工异常数量", "固井作业施工异常", "处理井漏数量", "处理涌水数量" };

            if (rbXian.Checked)
            {
                lbtnHc.Visible = false;
                lbtnLf.Visible = false;
                lbtnXz.Visible = false;

                string s = lblData.Text;
                string[] ss = s.Split(';');
                int iRows = ss.Length;
                data += title + ",(" + riqi + "),1,";
                for (int i = 0; i < iRows; i++)
                {
                    string tmp = ss[i].Split(',')[0];
                    if(i == iRows-1) data += tmp + ",";
                    else data += tmp + ";";
                }

                for (int n = 0; n < 7; n++)
                {
                    string str = "";
                    for (int i = 0; i < iRows; i++)
                    {
                        string[] tmp = ss[i].Split(',');
                        if (i == iRows-1) str += tmp[n+1];
                        else str += tmp[n+1] + ";";
                    }
                    if (n == 6) data += cols[n] + ";" + str;
                    else data += cols[n] + ";" + str + "$";
                }
                img.Attributes["src"] = "chartdbfx.aspx?data=" + data;
            }
            else if(rbZhu.Checked)
            {
                lbtnHc.Visible = false;
                lbtnLf.Visible = false;
                lbtnXz.Visible = false;

                string s = lblData.Text;
                string[] ss = s.Split(';');
                int iRows = ss.Length;
                data += title + ",(" + riqi + "),0,";
                for (int i = 0; i < iRows; i++)
                {
                    string tmp = ss[i].Split(',')[0];
                    if (i == iRows - 1) data += tmp + ",";
                    else data += tmp + ";";
                }

                for (int n = 0; n < 7; n++)
                {
                    string str = "";
                    for (int i = 0; i < iRows; i++)
                    {
                        string[] tmp = ss[i].Split(',');
                        if (i == iRows - 1) str += tmp[n + 1];
                        else str += tmp[n + 1] + ";";
                    }
                    if (n == 6) data += cols[n] + ";" + str;
                    else data += cols[n] + ";" + str + "$";
                }
                img.Attributes["src"] = "chartdbfx.aspx?data=" + data;
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

        protected void rbZhu_CheckedChanged(object sender, EventArgs e)
        {
            getImageData();
        }

        protected void rbXian_CheckedChanged(object sender, EventArgs e)
        {
            getImageData();
        }

        protected void Btn_Click(object sender, EventArgs e)
        {
            string begin = ddlBegin.SelectedItem.Text;
            string end = ddlEnd.SelectedItem.Text;
            int a = Int32.Parse(begin);
            int b = Int32.Parse(end);
            getData(a, b);
        }

        protected void ddlQukuai_SelectedIndexChanged(object sender, EventArgs e)
        {
            string begin = ddlBegin.SelectedItem.Text;
            string end = ddlEnd.SelectedItem.Text;
            int a = Int32.Parse(begin);
            int b = Int32.Parse(end);
            getData(a, b);
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
            
        }

        protected void rbBing_CheckedChanged(object sender, EventArgs e)
        {
            getImageData();
        }
    }
}