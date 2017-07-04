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
    public partial class EditWindow : ChildWindow
    {
        private String[] saLayers = null;
        private string qukuai = null;

        public String layer;
        public string[] values = null;

        public EditWindow()
        {
            InitializeComponent();
            this.OverlayOpacity = 0;

            this.cbDangqianzhuangtai.Items.Add("钻进");
            this.cbDangqianzhuangtai.Items.Add("下套管");
            this.cbDangqianzhuangtai.Items.Add("固井");
            this.cbDangqianzhuangtai.Items.Add("完井");
            this.cbDangqianzhuangtai.Items.Add("未压裂");
            this.cbDangqianzhuangtai.Items.Add("已压裂");
            this.cbDangqianzhuangtai.Items.Add("排彩");
        }

        public void Init(String layers, string qk)
        {
            if (qk == null || qk == "") return;
            if (layers == null || layers.Equals("")) return;
            qukuai = qk;
            saLayers = layers.Split(',');
            for(int i=0;i<saLayers.Length;i++)
            {
                this.cbLayer.Items.Add(saLayers[i]);
            }
            this.cbLayer.SelectedIndex = saLayers.Length-1;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.layer = cbLayer.SelectedItem.ToString();
            int index = this.cbLayer.SelectedIndex;
            String sJinghao = this.tbName.Text;
            if (sJinghao == null || sJinghao.Equals("")) return;
            String sx = this.tbX.Text;
            String sy = this.tbY.Text;
            String sh = this.tbH.Text;
            if (sx == null || sx.Equals("")) return;
            if (sy == null || sy.Equals("")) return;
   
            String sJingbie = tbJingbie.Text;
            String sJingxing = tbJingxing.Text;
            String sShejijingshen = tbShejijingshen.Text;
            String sShigongdanwei = tbShigongdanwei.Text;
            String sKaizuanriqi = tbKaizuanriqi.Text;
            int a = cbDangqianzhuangtai.SelectedIndex;
            String sDangqianzhuangtai = "";
            if (a >= 0) sDangqianzhuangtai = cbDangqianzhuangtai.SelectedItem.ToString();
            String sGengxinriqi = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
            String sBeizhu = tbBeizhu.Text;
   
            values = new string[15];
            values[0] = sJinghao;
            values[1] = sx;
            values[2] = sy;
            values[3] = sh;
            values[4] = sJingbie;
            values[5] = sJingxing;
            values[6] = sShejijingshen;
            values[7] = sShigongdanwei;
            values[8] = sKaizuanriqi;
            values[9] = sDangqianzhuangtai;
            values[10] = qukuai;
            values[11] = sGengxinriqi;
            values[12] = sBeizhu;
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

