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
    public partial class SearchWindow : ChildWindow
    {
        private string[] guanxis = new string[7] { "相似", "等于", "大于", "大于等于", "小于", "小于等于", "不等于" };
        private string[] cols = new string[7] { "井号", "井别", "设计井深", "完钻井深", "开钻日期", "完钻日期", "当前状态" };
        private string[] cols_en = new string[7] { "jinghao", "jingbie", "shejijingshen", "wanzuanjingshen", "kaizuanriqi", "wanzuanriqi", "dangqianzhuangtai"};
        private string dbTable = "jing_jichuxinxi";
        private String qukuai;

        private sqlSR.SqlServiceSoapClient client = null;
        public String[] records = null;

        public SearchWindow()
        {
            InitializeComponent();
            this.OverlayOpacity = 0;

            client = new sqlSR.SqlServiceSoapClient();
            client.getRecord2Completed += new EventHandler<sqlSR.getRecord2CompletedEventArgs>(client_getRecord2Completed);
            
            for (int i = 0; i < 7; i++)
            {
                this.cbField1.Items.Add(cols[i]);
                this.cbField2.Items.Add(cols[i]);
                this.cbField3.Items.Add(cols[i]);

                this.cbGuanxi1.Items.Add(guanxis[i]);
                this.cbGuanxi2.Items.Add(guanxis[i]);
                this.cbGuanxi3.Items.Add(guanxis[i]);
            }

            this.cbField1.SelectedIndex = 0;
            this.cbField2.SelectedIndex = 0;
            this.cbField3.SelectedIndex = 0;

            this.cbGuanxi1.SelectedIndex = 0;
            this.cbGuanxi2.SelectedIndex = 0;
            this.cbGuanxi3.SelectedIndex = 0;
        }

        public void Init(String qk)
        {
            qukuai = qk;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            String where1 = null;
            String where2 = null;
            String where3 = null;

            String field1 = cols_en[this.cbField1.SelectedIndex];

            String guanxi1 = null;
            int g1 = this.cbGuanxi1.SelectedIndex;
            if (g1 == 0) guanxi1 = "like";
            if (g1 == 1) guanxi1 = "=";
            if (g1 == 2) guanxi1 = ">";
            if (g1 == 3) guanxi1 = ">=";
            if (g1 == 4) guanxi1 = "<";
            if (g1 == 5) guanxi1 = "<=";
            if (g1 == 6) guanxi1 = "<>";

            String value1 = this.tbValue1.Text;
            if (value1 == null || value1.Equals(""))
            {
                MessageBox.Show("第二个查询条件不完整！");
                return;
            }
            if (this.cbGuanxi1.SelectedIndex == 0) value1 = "'%" + value1 + "%'";
            else value1 = "'" + value1 + "'";
            where1 = field1 + " " + guanxi1 + " " + value1;
            

            if ((bool)this.chbWhere2.IsChecked)
            {
                String field2 = cols_en[this.cbField2.SelectedIndex];
                String guanxi2 = null;
                int g2 = this.cbGuanxi2.SelectedIndex;
                if (g2 == 0) guanxi2 = "like";
                if (g2 == 1) guanxi2 = "=";
                if (g2 == 2) guanxi2 = ">";
                if (g2 == 3) guanxi2 = ">=";
                if (g2 == 4) guanxi2 = "<";
                if (g2 == 5) guanxi2 = "<=";
                if (g2 == 6) guanxi2 = "<>";
                String value2 = this.tbValue2.Text;
                if (value2 == null || value2.Equals(""))
                {
                    MessageBox.Show("第二个查询条件不完整！");
                    return;
                }
                if (this.cbGuanxi2.SelectedIndex == 0) value2 = "'%" + value2 + "%'";
                else value2 = "'" + value2 + "'";
                where2 = field2 + " " + guanxi2 + " " + value2;
            }
            if ((bool)this.chbWhere3.IsChecked)
            {
                String field3 = cols_en[this.cbField3.SelectedIndex];
                String guanxi3 = null;
                int g3 = this.cbGuanxi3.SelectedIndex;
                if (g3 == 0) guanxi3 = "like";
                if (g3 == 1) guanxi3 = "=";
                if (g3 == 2) guanxi3 = ">";
                if (g3 == 3) guanxi3 = ">=";
                if (g3 == 4) guanxi3 = "<";
                if (g3 == 5) guanxi3 = "<=";
                if (g3 == 6) guanxi3 = "<>";
                String value3 = this.tbValue3.Text;
                if (value3 == null || value3.Equals(""))
                {
                    MessageBox.Show("第三个查询条件不完整！");
                    return;
                }
                if (this.cbGuanxi3.SelectedIndex == 0) value3 = "'%" + value3 + "%'";
                else value3 = "'" + value3 + "'";
                where3 = field3 + " " + guanxi3 + " " + value3;
            }

            String sql2 = "SELECT * FROM " + dbTable + " WHERE qukuai='" + qukuai + "'";
            if (where1 != null)
            {
                sql2 += " AND " + where1;
            }
            if (where2 != null)
            {
                sql2 += " AND " + where2;
            }
            if (where3 != null)
            {
                sql2 += " AND " + where3;
            }
            client.getRecord2Async(sql2);
        }

        void client_getRecord2Completed(object sender, sqlSR.getRecord2CompletedEventArgs e)
        {
            if (e == null || e.Result == null)
            {
                MessageBox.Show("没有查询到记录.");
                return;
            }
            String rlt = e.Result;
            String[] rows = rlt.Split(';');
            records = rlt.Split(';');
            this.DialogResult = true;
            this.Close();
        }

        private void chbWhere2_Checked(object sender, RoutedEventArgs e)
        {
            this.chbWhere3.IsEnabled = true;
            this.cbField2.IsEnabled = true;
            this.cbGuanxi2.IsEnabled = true;
            this.tbValue2.IsEnabled = true;
        }

        private void chbWhere2_Unchecked(object sender, RoutedEventArgs e)
        {
            this.chbWhere3.IsEnabled = false;
            this.cbField2.IsEnabled = false;
            this.cbGuanxi2.IsEnabled = false;
            this.tbValue2.IsEnabled = false;
            this.tbValue2.Text = "";
            this.tbValue3.Text = "";
        }

        private void chbWhere3_Checked(object sender, RoutedEventArgs e)
        {
            this.cbField3.IsEnabled = true;
            this.cbGuanxi3.IsEnabled = true;
            this.tbValue3.IsEnabled = true;
        }

        private void chbWhere3_Unchecked(object sender, RoutedEventArgs e)
        {
            this.cbField3.IsEnabled = false;
            this.cbGuanxi3.IsEnabled = false;
            this.tbValue3.IsEnabled = false;
            this.tbValue3.Text = "";
        }
    }
}

