using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Text;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MyMap.sqlSR;


namespace MyMap
{
    public partial class AttrWindow : ChildWindow
    {
        public delegate void MyEventHandler(object sender, MyEventArgs e);
        public event MyEventHandler RecordChecked;
        public event MyEventHandler RecordDeleted;

        private string[] cols = new string[16] { "序号", "井号", "横坐标", "纵坐标", "海拔", "井别", "井型", "设计井深", "完钻井深", "施工单位", "开钻日期", "完钻日期", "当前状态", "区块", "更新日期", "备注" };

        public AttrWindow()
        {
            InitializeComponent();
            this.OverlayOpacity = 0;
        }

        public void SetValues(String[] records)
        {
            this.tabControl1.Items.Clear();
            int nTables = records.Length;
            for (int i = 0; i < nTables; i++)
            {
                if (records[i] == null || records[i] == "") continue;
                String[] value = records[i].Split(';');
                if (value.Length < 1) continue;
                String tablename = value[0];
                int nRows = value.Length - 1;
                BindableDataGrid.Data.DataTable table = new BindableDataGrid.Data.DataTable(tablename);
                /*
                for (int j = 0; j < cols.Length; j++)
                {
                    BindableDataGrid.Data.DataColumn col = new BindableDataGrid.Data.DataColumn(cols[j]);
                    table.Columns.Add(col);
                }
                */
                for (int j = 0; j < nRows; j++)
                {
                    String row = value[j+1];
                    String[] values = row.Split(',');
                    BindableDataGrid.Data.DataRow dr = new BindableDataGrid.Data.DataRow();
                    for (int a = 0; a < values.Length; a++)
                    {
                        if (a == 10 || a == 11 || a == 14)
                        {
                            dr[cols[a]] = values[a].Split(' ')[0];
                        }
                        else
                        {
                            dr[cols[a]] = values[a];
                        }
                    }
                    table.Rows.Add(dr);
                }

                

                BindableDataGrid.BindableDataGrid dg = new BindableDataGrid.BindableDataGrid();
                dg.SelectionChanged += new SelectionChangedEventHandler(bindableDataGrid_SelectionChanged);
                dg.DataSource = table;
                dg.DataBind();
                //for (int j = 0; j < nRows; j++)
                //{
                    
                //}

                

                TabItem tabItem = new TabItem();
                tabItem.Header = tablename;
                tabItem.Content = dg;
                this.tabControl1.Items.Add(tabItem);
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

        private void bindableDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem item = (TabItem)this.tabControl1.SelectedItem;
            String layername = (String)item.Header;
            BindableDataGrid.BindableDataGrid dg = (BindableDataGrid.BindableDataGrid)item.Content;
            String featruename = dg.SelectedItem.Items[cols[1]].ToString();
            MyEventArgs mye = new MyEventArgs(layername, featruename);
            OnRecordChecked(mye);
        }

        //事件触发方法
        protected virtual void OnRecordChecked(MyEventArgs e)
        {
            if (RecordChecked != null)
                RecordChecked(this, e);
        }

        protected virtual void OnRecordDeleted(MyEventArgs e)
        {
            if (RecordDeleted != null)
                RecordDeleted(this, e);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            TabItem item = (TabItem)this.tabControl1.SelectedItem;
            String layername = (String)item.Header;
            BindableDataGrid.BindableDataGrid dg = (BindableDataGrid.BindableDataGrid)item.Content;
            int index = dg.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("没有选中要素！");
                return;
            }
            MessageBoxResult r = MessageBox.Show("删除后将不可恢复，确定删除吗？", "删除要素", MessageBoxButton.OKCancel);
            if (r == MessageBoxResult.Cancel) return;
            if (r == MessageBoxResult.OK)
            {
                String featruename = dg.SelectedItem.Items[cols[1]].ToString();
                BindableDataGrid.Data.DataTable table = (BindableDataGrid.Data.DataTable)dg.DataSource;
                table.Rows.Remove(dg.SelectedItem);
                MyEventArgs mye = new MyEventArgs(layername, featruename);
                OnRecordDeleted(mye);
            }
        }
    }
}

