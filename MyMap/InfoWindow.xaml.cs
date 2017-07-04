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
using System.Windows.Browser;

namespace MyMap
{
    public partial class InfoWindow : ChildWindow
    {
        public InfoWindow()
        {
            this.OverlayOpacity = 0;
            InitializeComponent();
        }

        public void SetValues(string record)
        {
            string[] values = record.Split(',');
            tbName.Text = values[1];
            tbX.Text = values[2];
            tbY.Text = values[3];
            tbH.Text = values[4];
            tbJingbie.Text = values[5];
            tbJingxing.Text = values[6];
            tbShejijingshen.Text = values[7];
            tbShigongdanwei.Text = values[8];
            tbKaizuanriqi.Text = values[9].Split(' ')[0];
            tbDangqianzhuangtai.Text = values[10];
            tbQkuai.Text = values[11];
            tbGengxinriqi.Text = values[12].Split(' ')[0];
            tbBeizhu.Text = values[13];
        }

        private void btn_zj_Click(object sender, RoutedEventArgs e)
        {
            string jh = tbName.Text;
            //HtmlWindow html = HtmlPage.Window;
            //html.Navigate(new Uri("/Query/zj/ZJ_Query_jinghao_xx.aspx?JH=" + jh, UriKind.Relative));
            ZuanjingInfo zjInfo = new ZuanjingInfo();
            zjInfo.Init(jh);
            zjInfo.Show();
        }

        private void btn_yl_Click(object sender, RoutedEventArgs e)
        {
            string jh = tbName.Text;
            //HtmlWindow html = HtmlPage.Window;
            //html.Navigate(new Uri("/Query/yl/YL_Query_jinghao_xx.aspx?JH=" + jh, UriKind.Relative));
            YalieInfo ylInfo = new YalieInfo();
            ylInfo.Init(jh);
            ylInfo.Show();
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

