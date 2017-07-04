using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.FusionCharts
{
    public partial class Tongji_Sgzl : System.Web.UI.Page
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
            dt.Columns.Add("zongshu");
            dt.Columns.Add("jingshu");
            dt.Columns.Add("chenggonglv");

            lblRiqi.Text = begin + " 至 " + end;

            string data = "";

            if (ddl_zj_qukuai.Text != "全部")
            {
                string qk = ddl_zj_qukuai.SelectedItem.Text;
                string sqlTemp = null;
                DataTable dtTemp = null;
                String cgl = "0";
                sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place = '" + qk + "' group by jinghao,cengwei";
                dtTemp = DataBaseHelper.query(sqlTemp);
                int iZcs = dtTemp.Rows.Count;
                sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place = '" + qk + "' and shifouhege like '%否%' group by jinghao,cengwei";
                dtTemp = DataBaseHelper.query(sqlTemp);
                int iCgcs = iZcs - dtTemp.Rows.Count;

                if(iZcs > 0) cgl = String.Format("{0:N2}", iCgcs * 100.0f / iZcs);

                dt.Rows.Add();

                dt.Rows[0][0] = qk;
                dt.Rows[0][1] = iZcs;
                dt.Rows[0][2] = (iZcs-iCgcs);
                dt.Rows[0][3] = cgl == "0" ? "/" : cgl + "%";

                gv.DataSource = dt;
                gv.DataBind();

                data += iZcs + "," + (iZcs - iCgcs) + "," + cgl;
            }
            else
            {
                string sqlTemp = null;
                DataTable dtTemp = null;

                String cglHc = "0";
                sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place = '韩城' group by jinghao,cengwei";
                dtTemp = DataBaseHelper.query(sqlTemp);
                int iZcsHc = dtTemp.Rows.Count;
                sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place = '韩城' and shifouhege like '%否%' group by jinghao,cengwei";
                dtTemp = DataBaseHelper.query(sqlTemp);
                int iCgcsHc = iZcsHc - dtTemp.Rows.Count;
                if (iZcsHc > 0) cglHc = String.Format("{0:N2}", iCgcsHc * 100.0f / iZcsHc);

                String cglLf = "0";
                sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place = '临汾' group by jinghao,cengwei";
                dtTemp = DataBaseHelper.query(sqlTemp);
                int iZcsLf = dtTemp.Rows.Count;
                sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place = '临汾' and shifouhege like '%否%' group by jinghao,cengwei";
                dtTemp = DataBaseHelper.query(sqlTemp);
                int iCgcsLf = iZcsLf - dtTemp.Rows.Count;
                if (iZcsLf > 0) cglLf = String.Format("{0:N2}", iCgcsLf * 100.0f / iZcsLf);

                String cglXz = "0";
                sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place = '忻州' group by jinghao,cengwei";
                dtTemp = DataBaseHelper.query(sqlTemp);
                int iZcsXz = dtTemp.Rows.Count;
                sqlTemp = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and place = '忻州' and shifouhege like '%否%' group by jinghao,cengwei";
                dtTemp = DataBaseHelper.query(sqlTemp);
                int iCgcsXz = iZcsXz - dtTemp.Rows.Count;
                if (iZcsXz > 0) cglXz = String.Format("{0:N2}", iCgcsXz * 100.0f / iZcsXz);

                dt.Rows.Add();
                dt.Rows.Add();
                dt.Rows.Add();

                dt.Rows[0][0] = "韩城";
                dt.Rows[0][1] = iZcsHc;
                dt.Rows[0][2] = (iZcsHc-iCgcsHc);
                dt.Rows[0][3] = cglHc == "0" ? "/" : cglHc + "%";
                dt.Rows[1][0] = "临汾";
                dt.Rows[1][1] = iZcsLf;
                dt.Rows[1][2] = (iZcsLf - iCgcsLf);
                dt.Rows[1][3] = cglLf == "0" ? "/" : cglLf + "%";
                dt.Rows[2][0] = "忻州";
                dt.Rows[2][1] = iZcsXz;
                dt.Rows[2][2] = (iZcsXz - iCgcsXz);
                dt.Rows[2][3] = cglXz == "0" ? "/" : cglXz + "%";

                gv.DataSource = dt;
                gv.DataBind();

                data += iZcsHc + "," + (iZcsHc - iCgcsHc) + "," + cglHc + ";";
                data += iZcsLf + "," + (iZcsLf - iCgcsLf) + "," + cglLf + ";";
                data += iZcsXz + "," + (iZcsXz - iCgcsXz) + "," + cglXz;
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
                    data += "压裂次数;" + sshc[0] + ";" + sslf[0] + ";" + ssxz[0] + "$";
                    data += "失败次数;" + sshc[1] + ";" + sslf[1] + ";" + ssxz[1] + "$";
                    data += "一次成功率;" + sshc[2] + ";" + sslf[2] + ";" + ssxz[2];
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
                    data += "压裂次数;" + ss[0] + "$";
                    data += "失败次数;" + ss[1] + "$";
                    data += "一次成功率;" + ss[2];
                }
                img.Attributes["src"] = "chartSgzl.aspx?data=" + data;
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
                    data += "压裂次数;失败次数;一次成功率,";
                    data += "韩城;" + sshc[0] + ";" + sshc[1] + ";" + sshc[2] + "$";
                    data += "临汾;" + sslf[0] + ";" + sslf[1] + ";" + sslf[2] + "$";
                    data += "忻州;" + ssxz[0] + ";" + ssxz[1] + ";" + ssxz[2];
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
                    data += "压裂次数;失败次数;一次成功率,";
                    data += ddl_zj_qukuai.SelectedItem.Text + ";" + ss[0] + ";" + ss[1] + ";" + ss[2];
                }
                img.Attributes["src"] = "chartSgzl.aspx?data=" + data;
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
            data += "压裂次数,失败次数;";
            data += ss[0] + "," + ss[1];

            img.Attributes["src"] = "chart.aspx?data=" + data;
        }

        protected void rbBing_CheckedChanged(object sender, EventArgs e)
        {
            getImageData();
        }
    }
}