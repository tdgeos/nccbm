using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MyMap
{
    public partial class ZuanjingInfo : ChildWindow
    {
        public ZuanjingInfo()
        {
            InitializeComponent();
            //dgZj.RowStyle = Style.
        }

        public void Init(String jinghao)
        {
            sqlSR.SqlServiceSoapClient client = new sqlSR.SqlServiceSoapClient();
            client.getZuanjingInfoCompleted += new EventHandler<sqlSR.getZuanjingInfoCompletedEventArgs>(client_getZuanjingInfoCompleted);
            client.getZuanjingInfoAsync(jinghao);
        }

        void client_getZuanjingInfoCompleted(object sender, sqlSR.getZuanjingInfoCompletedEventArgs e)
        {
            if (e == null || e.Result == null)
            {
                return;
            }

            char fgRow = '^';
            char fgCol = '`';
            String[] values = e.Result.Split('|');
            //
            string[] zj_rows = values[0].Split(fgRow);
            string[] xtg_rows = values[1].Split(fgRow);
            string[] gj_rows = values[2].Split(fgRow);
            string[] wj_rows = values[3].Split(fgRow);

            List<RowInfo> lst = new List<RowInfo>();
            for (int i = 0; i < zj_rows.Length; i++)
            {
                if (zj_rows[i] != "")
                {
                    string[] row = zj_rows[i].Split(fgCol);
                    if (row.Length > 24)
                    {
                        RowInfo rowInfo = new RowInfo();
                        rowInfo.id = i + 1;
                        rowInfo.f1 = row[1];
                        rowInfo.f2 = row[2];
                        rowInfo.f3 = row[3];
                        rowInfo.f4 = row[4].Split(' ')[0];
                        rowInfo.f5 = row[5];
                        rowInfo.f6 = row[6];
                        rowInfo.f7 = row[7];
                        rowInfo.f8 = row[8];
                        rowInfo.f9 = row[9];
                        rowInfo.f10 = row[10];
                        rowInfo.f11 = row[11];
                        rowInfo.f12 = row[12];
                        rowInfo.f13 = row[13];
                        rowInfo.f14 = row[14];
                        rowInfo.f15 = row[15];
                        rowInfo.f16 = row[16];
                        rowInfo.f17 = row[17];
                        rowInfo.f18 = row[18];
                        rowInfo.f19 = row[19];
                        rowInfo.f20 = row[20];
                        rowInfo.f21 = row[21];
                        rowInfo.qukuai = row[23];
                        rowInfo.riqi = row[24].Split(' ')[0];
                        lst.Add(rowInfo);
                    }
                }
            }
            dgZj.ItemsSource = lst;
            //lst.Clear();

            List<RowInfo> lst2 = new List<RowInfo>();
            for (int i = 0; i < xtg_rows.Length; i++)
            {
                if (xtg_rows[i] != "")
                {
                    string[] row = xtg_rows[i].Split(fgCol);
                    if (row.Length > 24)
                    {
                        RowInfo rowInfo = new RowInfo();
                        rowInfo.id = i + 1;
                        rowInfo.f1 = row[1];
                        rowInfo.f2 = row[2];
                        rowInfo.f3 = row[3];
                        rowInfo.f4 = row[4];
                        rowInfo.f5 = row[5].Split(' ')[0];
                        rowInfo.f6 = row[6];
                        rowInfo.f7 = row[7];
                        rowInfo.f8 = row[8];
                        rowInfo.f9 = row[9];
                        rowInfo.f10 = row[10];
                        rowInfo.f11 = row[11];
                        rowInfo.f12 = row[12];
                        rowInfo.f13 = row[13];
                        rowInfo.f14 = row[14];
                        rowInfo.f15 = row[15];
                        rowInfo.f16 = row[16];
                        rowInfo.f17 = row[17];
                        rowInfo.f18 = row[18];
                        rowInfo.f19 = row[19];
                        rowInfo.f20 = row[20];
                        rowInfo.f21 = row[21];
                        rowInfo.qukuai = row[23];
                        rowInfo.riqi = row[24].Split(' ')[0];
                        lst2.Add(rowInfo);
                    }
                }
            }
            dgXtg.ItemsSource = lst2;
            //lst.Clear();

            List<RowInfo> lst3 = new List<RowInfo>();
            for (int i = 0; i < gj_rows.Length; i++)
            {
                if (gj_rows[i] != "")
                {
                    string[] row = gj_rows[i].Split(fgCol);
                    if (row.Length > 24)
                    {
                        RowInfo rowInfo = new RowInfo();
                        rowInfo.id = i + 1;
                        rowInfo.f1 = row[1];
                        rowInfo.f2 = row[2];
                        rowInfo.f3 = row[3];
                        rowInfo.f4 = row[4];
                        rowInfo.f5 = row[5].Split(' ')[0];
                        rowInfo.f6 = row[6];
                        rowInfo.f7 = row[7];
                        rowInfo.f8 = row[8];
                        rowInfo.f9 = row[9];
                        rowInfo.f10 = row[10];
                        rowInfo.f11 = row[11];
                        rowInfo.f12 = row[12];
                        rowInfo.f13 = row[13];
                        rowInfo.f14 = row[14];
                        rowInfo.f15 = row[15];
                        rowInfo.f16 = row[16];
                        rowInfo.f17 = row[17];
                        rowInfo.f18 = row[18];
                        rowInfo.f19 = row[19];
                        rowInfo.f20 = row[20];
                        rowInfo.f21 = row[21];
                        rowInfo.qukuai = row[23];
                        rowInfo.riqi = row[24].Split(' ')[0];
                        lst3.Add(rowInfo);
                    }
                }
            }
            dgGj.ItemsSource = lst3;
            //lst.Clear();

            List<RowInfo> lst4 = new List<RowInfo>();
            for (int i = 0; i < wj_rows.Length; i++)
            {
                if (wj_rows[i] != "")
                {
                    string[] row = wj_rows[i].Split(fgCol);
                    if (row.Length > 24)
                    {
                        RowInfo rowInfo = new RowInfo();
                        rowInfo.id = i + 1;
                        rowInfo.f1 = row[1];
                        rowInfo.f2 = row[2];
                        rowInfo.f3 = row[3];
                        rowInfo.f4 = row[4];
                        rowInfo.f5 = row[5];
                        rowInfo.f6 = row[6];
                        rowInfo.f7 = row[7];
                        rowInfo.f8 = row[8];
                        rowInfo.f9 = row[9];
                        rowInfo.f10 = row[10];
                        rowInfo.f11 = row[11];
                        rowInfo.f12 = row[12];
                        rowInfo.f13 = row[13];
                        rowInfo.f14 = row[14];
                        rowInfo.f15 = row[15];
                        rowInfo.f16 = row[16];
                        rowInfo.f17 = row[17];
                        rowInfo.f18 = row[18];
                        rowInfo.f19 = row[19];
                        rowInfo.f20 = row[20];
                        rowInfo.f21 = row[21];
                        rowInfo.qukuai = row[23];
                        rowInfo.riqi = row[24].Split(' ')[0];
                        lst4.Add(rowInfo);
                    }
                }
            }
            dgWj.ItemsSource = lst4;
            //lst.Clear();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public class RowInfo
        {
            public int id { get; set; }
            public string riqi { get; set; }
            public string f1 { get; set; }
            public string f2 { get; set; }
            public string f3 { get; set; }
            public string f4 { get; set; }
            public string f5 { get; set; }
            public string f6 { get; set; }
            public string f7 { get; set; }
            public string f8 { get; set; }
            public string f9 { get; set; }
            public string f10 { get; set; }
            public string f11 { get; set; }
            public string f12 { get; set; }
            public string f13 { get; set; }
            public string f14 { get; set; }
            public string f15 { get; set; }
            public string f16 { get; set; }
            public string f17 { get; set; }
            public string f18 { get; set; }
            public string f19 { get; set; }
            public string f20 { get; set; }
            public string f21 { get; set; }
            public string qukuai { get; set; }
        }
    }
}

