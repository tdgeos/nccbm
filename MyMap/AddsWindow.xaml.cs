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
using System.IO;

namespace MyMap
{
    public partial class AddsWindow : ChildWindow
    {
        private String[] saLayers = null;

        public String layer;
        public String[] records;

        public AddsWindow()
        {
            InitializeComponent();
            this.OverlayOpacity = 0;
        }

        public void Init(String layers)
        {
            if (layers == null || layers.Equals("")) return;
            saLayers = layers.Split(',');
            for (int i = 0; i < saLayers.Length; i++)
            {
                this.cbLayer.Items.Add(saLayers[i]);
            }
            this.cbLayer.SelectedIndex = saLayers.Length-1;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            layer = this.cbLayer.SelectedItem.ToString();
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog pfDialog = new OpenFileDialog();
            pfDialog.Filter = "Excel (*.xls, *.xlsx)|*.xls;*.xlsx";
            if ((bool)pfDialog.ShowDialog())
            {
                String fileName = pfDialog.File.Name;
                this.tbName.Text = fileName;

                FileInfo fileinfo = pfDialog.File;

                if (fileinfo != null)
                {
                    WebClient webclient = new WebClient();

                    string uploadFileName = fileinfo.Name.ToString(); //获取所选文件的名字

                    Uri upTargetUri = new Uri(String.Format("http://11.128.1.15/SilverlightUpload.ashx?fileName={0}", uploadFileName), UriKind.Absolute); //指定上传地址

                    webclient.OpenWriteCompleted += new OpenWriteCompletedEventHandler(webclient_OpenWriteCompleted);
                    webclient.Headers["Content-Type"] = "multipart/form-data";

                    webclient.OpenWriteAsync(upTargetUri, "POST", fileinfo.OpenRead());
                    webclient.WriteStreamClosed += new WriteStreamClosedEventHandler(webclient_WriteStreamClosed);
                }
            }
        }

        void webclient_OpenWriteCompleted(object sender, OpenWriteCompletedEventArgs e)
        {
            Stream clientStream = e.UserState as Stream;
            Stream serverStream = e.Result;
            byte[] buffer = new byte[4096];
            int readcount = 0;
            while ((readcount = clientStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                serverStream.Write(buffer, 0, readcount);
            }
            serverStream.Close();
            clientStream.Close();
        }

        void webclient_WriteStreamClosed(object sender, WriteStreamClosedEventArgs e)
        {
            //判断写入是否有异常
            if (e.Error != null)
            {
                //System.Windows.Browser.HtmlPage.Window.Alert(e.Error.Message.ToString());
                MessageBox.Show(e.Error.Message.ToString());
            }
            else
            {
                sqlSR.SqlServiceSoapClient clt = null;
                clt = new sqlSR.SqlServiceSoapClient();
                clt.readExcelCompleted += new EventHandler<sqlSR.readExcelCompletedEventArgs>(clt_readExcelCompleted);
                string fileName = this.tbName.Text;
                clt.readExcelAsync(fileName, "Sheet1");
            }
        }

        void clt_readExcelCompleted(object sender, sqlSR.readExcelCompletedEventArgs e)
        {
            String rlt = e.Result;
            if (rlt == null || rlt == "") return;
            records = rlt.Split(';');
        }
    }
}

