using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.FusionCharts
{
    public partial class Tongji_Gkfx : System.Web.UI.Page
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
            dt.Columns.Add("zugongyinsu");
            dt.Columns.Add("hancheng");
            dt.Columns.Add("linfen");
            dt.Columns.Add("xinzhou");

            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();

            dt.Rows[0][0] = "套管质量";
            dt.Rows[1][0] = "下雨";
            dt.Rows[2][0] = "工农关系";
            dt.Rows[3][0] = "车辆维修";
            dt.Rows[4][0] = "井场搬迁";
            dt.Rows[5][0] = "井台搬迁";
            dt.Rows[6][0] = "备水配液";

            gv.DataSource = dt;
            gv.DataBind();

            int[] nZgys = new int[7];
            int[] hcZgys = new int[7];
            int[] lfZgys = new int[7];
            int[] xzZgys = new int[7];

            lblRiqi.Text = begin + " 至 " + end;

            if (ddl_zj_qukuai.Text != "全部")
            {
                string qk = ddl_zj_qukuai.SelectedItem.Text;

                string sql = "select * from Report_zugongyinsu where lururiqi between '" + begin + "' and '" + end + "' and qukuai='" + qk + "'";
                DataTable dtTemp = DataBaseHelper.query(sql);
                int nRows = dtTemp.Rows.Count;
                if (nRows > 0)
                {
                    for (int i = 0; i < nRows; i++)
                    {
                        try
                        {
                            nZgys[0] += Int32.Parse(dtTemp.Rows[i]["taoguanzhiliangwenti"].ToString());
                        }
                        catch (System.Exception fe)
                        {
                            nZgys[0] += 0;
                        }
                        try
                        {
                            nZgys[1] += Int32.Parse(dtTemp.Rows[i]["xiayu"].ToString());
                        }
                        catch (System.Exception fe)
                        {
                            nZgys[1] += 0;
                        }
                        try
                        {
                            nZgys[2] += Int32.Parse(dtTemp.Rows[i]["gongnongguanxi"].ToString());
                        }
                        catch (System.Exception fe)
                        {
                            nZgys[2] += 0;
                        }
                        try
                        {
                            nZgys[3] += Int32.Parse(dtTemp.Rows[i]["cheliangweixiu"].ToString());
                        }
                        catch (System.Exception fe)
                        {
                            nZgys[3] += 0;
                        }
                        try
                        {
                            nZgys[4] += Int32.Parse(dtTemp.Rows[i]["jingchangbanqian"].ToString());
                        }
                        catch (System.Exception fe)
                        {
                            nZgys[4] += 0;
                        }
                        try
                        {
                            nZgys[5] += Int32.Parse(dtTemp.Rows[i]["dengdaijingtaibanqian"].ToString());
                        }
                        catch (System.Exception fe)
                        {
                            nZgys[5] += 0;
                        }
                        try
                        {
                            nZgys[6] += Int32.Parse(dtTemp.Rows[i]["beishuipeiye"].ToString());
                        }
                        catch (System.Exception fe)
                        {
                            nZgys[6] += 0;
                        }
                    }
                }

                if (qk == "韩城")
                {
                    gv.Columns[1].Visible = true;
                    gv.Columns[2].Visible = false;
                    gv.Columns[3].Visible = false;
                    gv.Rows[0].Cells[1].Text = nZgys[0].ToString();
                    gv.Rows[1].Cells[1].Text = nZgys[1].ToString();
                    gv.Rows[2].Cells[1].Text = nZgys[2].ToString();
                    gv.Rows[3].Cells[1].Text = nZgys[3].ToString();
                    gv.Rows[4].Cells[1].Text = nZgys[4].ToString();
                    gv.Rows[5].Cells[1].Text = nZgys[5].ToString();
                    gv.Rows[6].Cells[1].Text = nZgys[6].ToString();
                }
                if (qk == "临汾")
                {
                    gv.Columns[1].Visible = false;
                    gv.Columns[2].Visible = true;
                    gv.Columns[3].Visible = false;
                    gv.Rows[0].Cells[2].Text = nZgys[0].ToString();
                    gv.Rows[1].Cells[2].Text = nZgys[1].ToString();
                    gv.Rows[2].Cells[2].Text = nZgys[2].ToString();
                    gv.Rows[3].Cells[2].Text = nZgys[3].ToString();
                    gv.Rows[4].Cells[2].Text = nZgys[4].ToString();
                    gv.Rows[5].Cells[2].Text = nZgys[5].ToString();
                    gv.Rows[6].Cells[2].Text = nZgys[6].ToString();
                }
                if (qk == "忻州")
                {
                    gv.Columns[1].Visible = false;
                    gv.Columns[2].Visible = false;
                    gv.Columns[3].Visible = true;
                    gv.Rows[0].Cells[3].Text = nZgys[0].ToString();
                    gv.Rows[1].Cells[3].Text = nZgys[1].ToString();
                    gv.Rows[2].Cells[3].Text = nZgys[2].ToString();
                    gv.Rows[3].Cells[3].Text = nZgys[3].ToString();
                    gv.Rows[4].Cells[3].Text = nZgys[4].ToString();
                    gv.Rows[5].Cells[3].Text = nZgys[5].ToString();
                    gv.Rows[6].Cells[3].Text = nZgys[6].ToString();
                }
            }
            else
            {
                string sql = "select * from Report_zugongyinsu where lururiqi between '" + begin + "' and '" + end + "'";
                DataTable dtTemp = DataBaseHelper.query(sql);
                int nRows = dtTemp.Rows.Count;
                if (nRows > 0)
                {
                    for (int i = 0; i < nRows; i++)
                    {
                        if (dtTemp.Rows[i]["qukuai"].ToString() == "韩城")
                        {
                            try
                            {
                                hcZgys[0] += Int32.Parse(dtTemp.Rows[i]["taoguanzhiliangwenti"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                hcZgys[0] += 0;
                            }
                            try
                            {
                                hcZgys[1] += Int32.Parse(dtTemp.Rows[i]["xiayu"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                hcZgys[1] += 0;
                            }
                            try
                            {
                                hcZgys[2] += Int32.Parse(dtTemp.Rows[i]["gongnongguanxi"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                hcZgys[2] += 0;
                            }
                            try
                            {
                                hcZgys[3] += Int32.Parse(dtTemp.Rows[i]["cheliangweixiu"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                hcZgys[3] += 0;
                            }
                            try
                            {
                                hcZgys[4] += Int32.Parse(dtTemp.Rows[i]["jingchangbanqian"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                hcZgys[4] += 0;
                            }
                            try
                            {
                                hcZgys[5] += Int32.Parse(dtTemp.Rows[i]["dengdaijingtaibanqian"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                hcZgys[5] += 0;
                            }
                            try
                            {
                                hcZgys[6] += Int32.Parse(dtTemp.Rows[i]["beishuipeiye"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                hcZgys[6] += 0;
                            }
                        }
                        if (dtTemp.Rows[i]["qukuai"].ToString() == "临汾")
                        {
                            try
                            {
                                lfZgys[0] += Int32.Parse(dtTemp.Rows[i]["taoguanzhiliangwenti"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                lfZgys[0] += 0;
                            }
                            try
                            {
                                lfZgys[1] += Int32.Parse(dtTemp.Rows[i]["xiayu"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                lfZgys[1] += 0;
                            }
                            try
                            {
                                lfZgys[2] += Int32.Parse(dtTemp.Rows[i]["gongnongguanxi"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                lfZgys[2] += 0;
                            }
                            try
                            {
                                lfZgys[3] += Int32.Parse(dtTemp.Rows[i]["cheliangweixiu"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                lfZgys[3] += 0;
                            }
                            try
                            {
                                lfZgys[4] += Int32.Parse(dtTemp.Rows[i]["jingchangbanqian"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                lfZgys[4] += 0;
                            }
                            try
                            {
                                lfZgys[5] += Int32.Parse(dtTemp.Rows[i]["dengdaijingtaibanqian"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                lfZgys[5] += 0;
                            }
                            try
                            {
                                lfZgys[6] += Int32.Parse(dtTemp.Rows[i]["beishuipeiye"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                lfZgys[6] += 0;
                            }
                        }
                        if (dtTemp.Rows[i]["qukuai"].ToString() == "忻州")
                        {
                            try
                            {
                                xzZgys[0] += Int32.Parse(dtTemp.Rows[i]["taoguanzhiliangwenti"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                xzZgys[0] += 0;
                            }
                            try
                            {
                                xzZgys[1] += Int32.Parse(dtTemp.Rows[i]["xiayu"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                xzZgys[1] += 0;
                            }
                            try
                            {
                                xzZgys[2] += Int32.Parse(dtTemp.Rows[i]["gongnongguanxi"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                xzZgys[2] += 0;
                            }
                            try
                            {
                                xzZgys[3] += Int32.Parse(dtTemp.Rows[i]["cheliangweixiu"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                xzZgys[3] += 0;
                            }
                            try
                            {
                                xzZgys[4] += Int32.Parse(dtTemp.Rows[i]["jingchangbanqian"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                xzZgys[4] += 0;
                            }
                            try
                            {
                                xzZgys[5] += Int32.Parse(dtTemp.Rows[i]["dengdaijingtaibanqian"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                xzZgys[5] += 0;
                            }
                            try
                            {
                                xzZgys[6] += Int32.Parse(dtTemp.Rows[i]["beishuipeiye"].ToString());
                            }
                            catch (System.Exception fe)
                            {
                                xzZgys[6] += 0;
                            }
                        }
                    }
                }

                gv.Rows[0].Cells[1].Text = hcZgys[0].ToString();
                gv.Rows[1].Cells[1].Text = hcZgys[1].ToString();
                gv.Rows[2].Cells[1].Text = hcZgys[2].ToString();
                gv.Rows[3].Cells[1].Text = hcZgys[3].ToString();
                gv.Rows[4].Cells[1].Text = hcZgys[4].ToString();
                gv.Rows[5].Cells[1].Text = hcZgys[5].ToString();
                gv.Rows[6].Cells[1].Text = hcZgys[6].ToString();

                gv.Rows[0].Cells[2].Text = lfZgys[0].ToString();
                gv.Rows[1].Cells[2].Text = lfZgys[1].ToString();
                gv.Rows[2].Cells[2].Text = lfZgys[2].ToString();
                gv.Rows[3].Cells[2].Text = lfZgys[3].ToString();
                gv.Rows[4].Cells[2].Text = lfZgys[4].ToString();
                gv.Rows[5].Cells[2].Text = lfZgys[5].ToString();
                gv.Rows[6].Cells[2].Text = lfZgys[6].ToString();

                gv.Rows[0].Cells[3].Text = xzZgys[0].ToString();
                gv.Rows[1].Cells[3].Text = xzZgys[1].ToString();
                gv.Rows[2].Cells[3].Text = xzZgys[2].ToString();
                gv.Rows[3].Cells[3].Text = xzZgys[3].ToString();
                gv.Rows[4].Cells[3].Text = xzZgys[4].ToString();
                gv.Rows[5].Cells[3].Text = xzZgys[5].ToString();
                gv.Rows[6].Cells[3].Text = xzZgys[6].ToString();
            }

            string data = "";
            if (ddl_zj_qukuai.SelectedItem.Text == "全部")
            {
                data += hcZgys[0] + "," + hcZgys[1] + "," + hcZgys[2] + "," + hcZgys[3] + "," + hcZgys[4] + "," + hcZgys[5] + "," + hcZgys[6] + ";";
                data += lfZgys[0] + "," + lfZgys[1] + "," + lfZgys[2] + "," + lfZgys[3] + "," + lfZgys[4] + "," + lfZgys[5] + "," + lfZgys[6] + ";";
                data += xzZgys[0] + "," + xzZgys[1] + "," + xzZgys[2] + "," + xzZgys[3] + "," + xzZgys[4] + "," + xzZgys[5] + "," + xzZgys[6];
            }
            else
            {
                data += nZgys[0] + "," + nZgys[1] + "," + nZgys[2] + "," + nZgys[3] + "," + nZgys[4] + "," + nZgys[5] + "," + nZgys[6];
            }
            lblData.Text = data;

            getImageData();
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

        private void getImageData()
        {
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

                    data += "qb,(" + lblRiqi.Text + "),1,";
                    data += "韩城;临汾;忻州,";
                    data += "套管质量;" + sshc[0] + ";" + sslf[0] + ";" + ssxz[0] + "$";
                    data += "下雨;" + sshc[1] + ";" + sslf[1] + ";" + ssxz[1] + "$";
                    data += "工农关系;" + sshc[2] + ";" + sslf[2] + ";" + ssxz[2] + "$";
                    data += "车辆维修;" + sshc[3] + ";" + sslf[3] + ";" + ssxz[3] + "$";
                    data += "井场搬迁;" + sshc[4] + ";" + sslf[4] + ";" + ssxz[4] + "$";
                    data += "井台搬迁;" + sshc[5] + ";" + sslf[5] + ";" + ssxz[5] + "$";
                    data += "备水配液;" + sshc[6] + ";" + sslf[6] + ";" + ssxz[6];

                }
                else
                {
                    string s = lblData.Text;
                    string[] ss = s.Split(',');

                    string sqk = "";
                    if (ddl_zj_qukuai.SelectedItem.Text == "韩城") sqk = "hc";
                    if (ddl_zj_qukuai.SelectedItem.Text == "临汾") sqk = "lf";
                    if (ddl_zj_qukuai.SelectedItem.Text == "忻州") sqk = "xz";

                    data += sqk + ",(" + lblRiqi.Text + "),1,";
                    data += ddl_zj_qukuai.SelectedItem.Text + ",";
                    data += "套管质量;" + ss[0] + "$";
                    data += "下雨;" + ss[1] + "$";
                    data += "工农关系;" + ss[2] + "$";
                    data += "车辆维修;" + ss[3] + "$";
                    data += "井场搬迁;" + ss[4] + "$";
                    data += "井台搬迁;" + ss[5] + "$";
                    data += "备水配液;" + ss[6];
                }
                img.Attributes["src"] = "chartGkfx.aspx?data=" + data;
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

                    data += "qb,(" + lblRiqi.Text + "),0,";
                    data += "套管质量;下雨;工农关系;车辆维修;井场搬迁;井台搬迁;备水配液,";
                    data += "韩城;" + sshc[0] + ";" + sshc[1] + ";" + sshc[2] + ";" + sshc[3] + ";" + sshc[4] + ";" + sshc[5] + ";" + sshc[6] + "$";
                    data += "临汾;" + sslf[0] + ";" + sslf[1] + ";" + sslf[2] + ";" + sslf[3] + ";" + sslf[4] + ";" + sslf[5] + ";" + sslf[6] + "$";
                    data += "忻州;" + ssxz[0] + ";" + ssxz[1] + ";" + ssxz[2] + ";" + ssxz[3] + ";" + ssxz[4] + ";" + ssxz[5] + ";" + ssxz[6];
                }
                else
                {
                    string s = lblData.Text;
                    string[] ss = s.Split(',');

                    string sqk = "";
                    if (ddl_zj_qukuai.SelectedItem.Text == "韩城") sqk = "hc";
                    if (ddl_zj_qukuai.SelectedItem.Text == "临汾") sqk = "lf";
                    if (ddl_zj_qukuai.SelectedItem.Text == "忻州") sqk = "xz";

                    data += sqk + ",(" + lblRiqi.Text + "),0,";
                    data += "套管质量;下雨;工农关系;车辆维修;井场搬迁;井台搬迁;备水配液,";
                    data += ddl_zj_qukuai.SelectedItem.Text + ";" + ss[0] + ";" + ss[1] + ";" + ss[2] + ";" + ss[3] + ";" + ss[4] + ";" + ss[5] + ";" + ss[6];
                }
                img.Attributes["src"] = "chartGkfx.aspx?data=" + data;
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
            data += "套管质量,下雨,工农关系,车辆维修,井场搬迁,井台搬迁,备水配液;";
            data += ss[0] + "," + ss[1] + "," + ss[2] + "," + ss[3] + "," + ss[4] + "," + ss[5] + "," + ss[6];

            img.Attributes["src"] = "chart.aspx?data=" + data;
        }

        protected void rbBing_CheckedChanged(object sender, EventArgs e)
        {
            getImageData();
        }
    }
}