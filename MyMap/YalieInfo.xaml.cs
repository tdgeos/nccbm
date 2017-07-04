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
    public partial class YalieInfo : ChildWindow
    {
        public YalieInfo()
        {
            this.OverlayOpacity = 0;
            InitializeComponent();
        }

        public void Init(String jinghao)
        {
            sqlSR.SqlServiceSoapClient client = new sqlSR.SqlServiceSoapClient();
            client.getYalieInfoCompleted += new EventHandler<sqlSR.getYalieInfoCompletedEventArgs>(client_getYalieInfoCompleted);
            client.getYalieInfoAsync(jinghao);
        }

        void client_getYalieInfoCompleted(object sender, sqlSR.getYalieInfoCompletedEventArgs e)
        {
            if (e == null || e.Result == null)
            {
                return;
            }
            char fgRow = '^';
            char fgCol = '`';
            String[] values = e.Result.Split('|');

            string[] sks = values[0].Split(fgRow)[0].Split(fgCol);
            string[] ylsgs = values[1].Split(fgRow)[0].Split(fgCol);
            string[] xbs = values[2].Split(fgRow)[0].Split(fgCol);
            string[] sbyys = values[3].Split(fgRow)[0].Split(fgCol);

            if (sks.Length > 15)
            {
                tbSk1.Text = sks[1];
                tbSk2.Text = sks[2];
                tbSk3.Text = sks[3].Split(' ')[0];
                tbSk4.Text = sks[4];
                tbSk5.Text = sks[5];
                tbSk6.Text = sks[6];
                tbSk7.Text = sks[7];
                tbSk8.Text = sks[8];
                tbSk9.Text = sks[9];
                tbSk10.Text = sks[10];
                tbSk11.Text = sks[11];
                tbSk12.Text = sks[12];
                tbSk13.Text = sks[13];
                tbSk14.Text = sks[15];
            }
            if (ylsgs.Length > 43)
            {
                tbYlsg1.Text = ylsgs[1];
                tbYlsg2.Text = ylsgs[2];
                tbYlsg3.Text = ylsgs[3].Split(' ')[0];
                tbYlsg4.Text = ylsgs[4];
                tbYlsg5.Text = ylsgs[5];
                tbYlsg6.Text = ylsgs[6];
                tbYlsg7.Text = ylsgs[7];
                tbYlsg8.Text = ylsgs[8];
                tbYlsg9.Text = ylsgs[9];
                tbYlsg10.Text = ylsgs[10];
                tbYlsg11.Text = ylsgs[11];
                tbYlsg12.Text = ylsgs[12];
                tbYlsg13.Text = ylsgs[13];
                tbYlsg14.Text = ylsgs[14];
                tbYlsg15.Text = ylsgs[15];
                tbYlsg16.Text = ylsgs[16];
                tbYlsg17.Text = ylsgs[17];
                tbYlsg18.Text = ylsgs[18];
                tbYlsg19.Text = ylsgs[19];
                tbYlsg20.Text = ylsgs[20];
                tbYlsg21.Text = ylsgs[21];
                tbYlsg22.Text = ylsgs[22];
                tbYlsg23.Text = ylsgs[23];
                tbYlsg24.Text = ylsgs[24];
                tbYlsg25.Text = ylsgs[25];
                tbYlsg26.Text = ylsgs[26];
                tbYlsg27.Text = ylsgs[27];
                tbYlsg28.Text = ylsgs[28];
                tbYlsg29.Text = ylsgs[29];
                tbYlsg30.Text = ylsgs[30];
                tbYlsg31.Text = ylsgs[31];
                tbYlsg32.Text = ylsgs[32];
                tbYlsg33.Text = ylsgs[33];
                tbYlsg34.Text = ylsgs[34];
                tbYlsg35.Text = ylsgs[35];
                tbYlsg36.Text = ylsgs[36];
                tbYlsg37.Text = ylsgs[37];
                tbYlsg38.Text = ylsgs[38];
                tbYlsg39.Text = ylsgs[39];
                tbYlsg40.Text = ylsgs[40];
                tbYlsg41.Text = ylsgs[41];
                tbYlsg42.Text = ylsgs[42];
                tbYlsg43.Text = ylsgs[43];
            }
            if (xbs.Length > 8)
            {
                tbXb1.Text = xbs[1].Split(' ')[0];
                tbXb2.Text = xbs[2];
                tbXb3.Text = xbs[3];
                tbXb4.Text = xbs[4];
                tbXb5.Text = xbs[5];
                tbXb6.Text = xbs[6];
                tbXb7.Text = xbs[7];
                tbXb8.Text = xbs[8];
            }
            if (sbyys.Length > 5)
            {
                tbSbyysm1.Text = sbyys[1];
                tbSbyysm2.Text = sbyys[2];
                tbSbyysm3.Text = sbyys[3].Split(' ')[0];
                tbSbyysm4.Text = sbyys[4];
                tbSbyysm5.Text = sbyys[5];
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

