using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using ZDIMS.Drawing;
using ZDIMS.BaseLib;
using ZDIMS.Interface;
using ZDIMSDemo.Controls;
using ZDIMSDemo.Controls.MapDoc;
using ZDIMS.Map;
using ZDIMS.Util;
using ZDIMSDemo.Controls.Layer;

using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MyMap
{
    public partial class MainPage : UserControl
    {
        private GraphicsLayer m_gpLayer = null;
        private SFeatureGeometry m_featureGeo = null;
        private GraphicsBase m_graphics = null;
        private EditWindow editWindow = null;
        private AddsWindow addsWindow = null;
        private SearchWindow sw = null;

        private VectorMapDoc m_vectorCur = null;

        private List<String> lstMap1Layers = null;
        private List<TreeViewItem> lstMap1TreeItems = null;
        private List<String> lstMap2Layers = null;
        private List<TreeViewItem> lstMap2TreeItems = null;
        private List<String> lstMap3Layers = null;
        private List<TreeViewItem> lstMap3TreeItems = null;
        private List<String> lstCurLayers = null;

        private int nDelLayer = -1;

        private string[] saUpdateRecords;

        private string[] saStatus = new string[8] { "钻进", "下套管", "固井", "完井", "未压裂", "已压裂", "下泵", "交井" };
        private int[] naSymIds = new int[8] { 37, 41, 42, 43, 44, 48, 136, 130 };
        private int[] naSymbolSize = new int[3] { 100, 300, 100};

        private IDictionary<string, string> _configurations; //从web.config中读取的参数
        private static string address = "";
        private static string map_hancheng = "";
        private static string map_linfen = "";
        private static string map_xinzhou = "";

        private string strBaseLayer = "基础图层";

        private List<string> lstSelectResult = new List<string>();
        public static string dbTable = "jing_jichuxinxi";
        public string curQukuai = "";

        public MainPage()
        {
            InitializeComponent();

            _configurations = (Application.Current as App).Configurations; 
            address = _configurations["map_addr"];
            map_hancheng = _configurations["map_hancheng"];
            map_linfen = _configurations["map_linfen"];
            map_xinzhou = _configurations["map_xinzhou"];

            comboBox1.Items.Clear();
            string qkid = MyCookies.GetCookie("PlaceID");
            if (qkid != null && qkid != "")
            {
                if (qkid == "0")
                {
                    comboBox1.Items.Add("韩城");
                    comboBox1.Items.Add("临汾");
                    comboBox1.Items.Add("忻州");
                    curQukuai = "韩城";
                }
                if (qkid == "1")
                {
                    comboBox1.Items.Add("韩城");
                    curQukuai = "韩城";
                }
                if (qkid == "2")
                {
                    comboBox1.Items.Add("临汾");
                    curQukuai = "临汾";
                }
                if (qkid == "3")
                {
                    comboBox1.Items.Add("忻州");
                    curQukuai = "忻州";
                }
            }
            else
            {
                comboBox1.Items.Add("韩城");
                comboBox1.Items.Add("临汾");
                comboBox1.Items.Add("忻州");
                curQukuai = "韩城";
            }

            sqlSR.SqlServiceSoapClient client = new sqlSR.SqlServiceSoapClient();
            client.getLayersCompleted += new EventHandler<sqlSR.getLayersCompletedEventArgs>(client_getLayersCompleted);
            client.getLayersAsync();
        }

        void client_getLayersCompleted(object sender, sqlSR.getLayersCompletedEventArgs e)
        {
            
            string value = null;
            string[] mapLayers = null;
            string[] hcLayers = null;
            string[] lfLayers = null;
            string[] xzLayers = null;
            if (e != null && e.Result != null)
            {
                value = e.Result;
                mapLayers = value.Split(',');
                hcLayers = mapLayers[0].Split(';');
                lfLayers = mapLayers[1].Split(';');
                xzLayers = mapLayers[2].Split(';');
            }
            
            /*
            string[] hcLayers = new string[] { "井位-2011", "井位-2012", "井位-2013" };
            string[] lfLayers = new string[] { "井位-2012", "井位-2013" };
            string[] xzLayers = new string[] { "井位-2012", "井位-2013" };
            */
            initLayers(hcLayers, lfLayers, xzLayers);
        }

        //初始化需要编辑的图层列表
        private void initLayers(string[] hcLayers, string[] lfLayers, string[] xzLayers)
        {
            //Map1
            lstMap1Layers = new List<string>();
            lstMap1TreeItems = new List<TreeViewItem>();
            for (int i = 0; i < hcLayers.Length; i++)
            {
                lstMap1Layers.Add(hcLayers[i]);
            }

            TreeViewItem item0 = new TreeViewItem();
            item0.Name = "item0";
            StackPanel sp0 = new StackPanel();
            CheckBox cb0 = new CheckBox();
            cb0.Checked += new RoutedEventHandler(cb_Checked);
            cb0.Unchecked += new RoutedEventHandler(cb_Unchecked);
            cb0.Content = strBaseLayer;
            cb0.IsChecked = true;
            sp0.Children.Add(cb0);
            item0.Header = sp0;
            lstMap1TreeItems.Add(item0);

            for (int i = 0; i < lstMap1Layers.Count; i++)
            {
                TreeViewItem item = new TreeViewItem();
                item.Name = "item" + (i+1);
                StackPanel sp = new StackPanel();
                CheckBox cb = new CheckBox();
                cb.Checked += new RoutedEventHandler(cb_Checked);
                cb.Unchecked += new RoutedEventHandler(cb_Unchecked);
                cb.Content = lstMap1Layers[i];
                cb.IsChecked = true;
                sp.Children.Add(cb);
                item.Header = sp;
                item.IsExpanded = false;//是否展开
                lstMap1TreeItems.Add(item);
            }

            
            //Map2
            lstMap2Layers = new List<string>();
            lstMap2TreeItems = new List<TreeViewItem>();
            //lstMap2Layers.Add("2009年完钻井");
            //lstMap2Layers.Add("2010年完钻探井");
            //lstMap2Layers.Add("2011年探井");
            //lstMap2Layers.Add("2012年探井");
            for (int i = 0; i < lfLayers.Length; i++)
            {
                lstMap2Layers.Add(lfLayers[i]);
            }

            TreeViewItem item10 = new TreeViewItem();
            item10.Name = "item10";
            StackPanel sp10 = new StackPanel();
            CheckBox cb10 = new CheckBox();
            cb10.Checked += new RoutedEventHandler(cb_Checked);
            cb10.Unchecked += new RoutedEventHandler(cb_Unchecked);
            cb10.Content = strBaseLayer;
            cb10.IsChecked = true;
            sp10.Children.Add(cb10);
            item10.Header = sp10;
            lstMap2TreeItems.Add(item10);

            for (int i = 0; i < lstMap2Layers.Count; i++)
            {
                TreeViewItem item = new TreeViewItem();
                item.Name = "item1" + (i + 1);
                StackPanel sp = new StackPanel();
                CheckBox cb = new CheckBox();
                cb.Checked += new RoutedEventHandler(cb_Checked);
                cb.Unchecked += new RoutedEventHandler(cb_Unchecked);
                cb.Content = lstMap2Layers[i];
                cb.IsChecked = true;
                sp.Children.Add(cb);
                item.Header = sp;
                item.IsExpanded = false;
                lstMap2TreeItems.Add(item);
            }

            //Map3
            lstMap3Layers = new List<string>();
            lstMap3TreeItems = new List<TreeViewItem>();
            //lstMap3Layers.Add("保德煤层气井");
            //lstMap3Layers.Add("保德煤层气探井");
            //lstMap3Layers.Add("所有部署井");
            for (int i = 0; i < xzLayers.Length; i++)
            {
                lstMap3Layers.Add(xzLayers[i]);
            }

            TreeViewItem item20 = new TreeViewItem();
            item20.Name = "item20";
            StackPanel sp20 = new StackPanel();
            CheckBox cb20 = new CheckBox();
            cb20.Checked += new RoutedEventHandler(cb_Checked);
            cb20.Unchecked += new RoutedEventHandler(cb_Unchecked);
            cb20.Content = strBaseLayer;
            cb20.IsChecked = true;
            sp20.Children.Add(cb20);
            item20.Header = sp20;
            lstMap3TreeItems.Add(item20);

            for (int i = 0; i < lstMap3Layers.Count; i++)
            {
                TreeViewItem item = new TreeViewItem();
                item.Name = "item2" + (i + 1);
                StackPanel sp = new StackPanel();
                CheckBox cb = new CheckBox();
                cb.Checked += new RoutedEventHandler(cb_Checked);
                cb.Unchecked += new RoutedEventHandler(cb_Unchecked);
                cb.Content = lstMap3Layers[i];
                cb.IsChecked = true;
                sp.Children.Add(cb);
                item.Header = sp;
                item.IsExpanded = false;
                lstMap3TreeItems.Add(item);
            }

            //
            lstCurLayers = new List<string>();
            for (int i = 0; i < lstMap1Layers.Count; i++)
            {
                lstCurLayers.Add(lstMap1Layers[i]);
            }
            this.comboBox1.SelectedIndex = 0;
            this.iMSMap1.MapReady += new ZDIMS.Event.IMSMapEventHandler(iMSMap_MapReady);
        }

        //目录树中图层对应的CheckBox被取消选中时触发此方法，用来隐藏图层
        void cb_Unchecked(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)this.treeView1.SelectedItem;
            if (item == null) return;
            StackPanel sp = (StackPanel)item.Header;
            CheckBox cb = (CheckBox)sp.Children[0];
            String layer = (String)cb.Content;
            if (string.Compare(layer, strBaseLayer, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                int nVectorLayerCount = m_vectorCur.LayerCount;
                for (int i = 0; i < nVectorLayerCount; i++)
                {
                    bool b = false;
                    String str2 = m_vectorCur.GetLayerInfo(i).LayerAliasName;
                    for (int j = 0; j < lstCurLayers.Count; j++)
                    {
                        String str1 = lstCurLayers[j] + ".wt";
                        b = (0 == string.Compare(str1, str2, StringComparison.CurrentCultureIgnoreCase));
                        if (b) break;
                    }
                    if (!b)
                    {
                        m_vectorCur.GetMapLayerInfo(i).LayerStatus = EnumLayerStatus.Invisiable;
                    }
                }
            }
            else
            {
                int nVectorLayerCount = m_vectorCur.LayerCount;
                for (int i = 0; i < nVectorLayerCount; i++)
                {
                    String str1 = layer + ".wt";
                    String str2 = m_vectorCur.GetLayerInfo(i).LayerAliasName;
                    if (string.Compare(str1, str2, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        m_vectorCur.GetMapLayerInfo(i).LayerStatus = EnumLayerStatus.Invisiable;
                    }
                }
            }
            m_vectorCur.UpdateAllLayerInfo(ue);
        }

        //目录树中图层对应的CheckBox被选中时触发此方法，用来显示图层
        void cb_Checked(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)this.treeView1.SelectedItem;
            if (item == null) return;
            StackPanel sp = (StackPanel)item.Header;
            CheckBox cb = (CheckBox)sp.Children[0];
            String layer = (String)cb.Content;
            if (string.Compare(layer, strBaseLayer, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                int nVectorLayerCount = m_vectorCur.LayerCount;
                for (int i = 0; i < nVectorLayerCount; i++)
                {
                    bool b = false;
                    for (int j = 0; j < lstCurLayers.Count; j++)
                    {
                        String str1 = lstCurLayers[j] + ".wt";
                        String str2 = m_vectorCur.GetLayerInfo(i).LayerAliasName;
                        b = (0 == string.Compare(str1, str2, StringComparison.CurrentCultureIgnoreCase));
                        if (b) break;
                    }
                    if (!b)
                    {
                        m_vectorCur.GetMapLayerInfo(i).LayerStatus = EnumLayerStatus.Visiable;
                    }
                }
            }
            else
            {
                int nVectorLayerCount = m_vectorCur.LayerCount;
                for (int i = 0; i < nVectorLayerCount; i++)
                {
                    String str1 = layer + ".wt";
                    String str2 = m_vectorCur.GetLayerInfo(i).LayerAliasName;
                    if (string.Compare(str1, str2, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        m_vectorCur.GetMapLayerInfo(i).LayerStatus = EnumLayerStatus.Visiable;
                        m_vectorCur.GetMapLayerInfo(i).LayerStatus = EnumLayerStatus.Selectable;
                        m_vectorCur.GetMapLayerInfo(i).LayerStatus = EnumLayerStatus.Editable;
                    }
                }
            }
            m_vectorCur.UpdateAllLayerInfo(ue);
        }

        //地图加载完成后触发
        private void iMSMap_MapReady(ZDIMS.Event.IMSMapEvent e)
        {
            if (!DesignerProperties.IsInDesignTool)
            {
                m_gpLayer = new GraphicsLayer() { EnableGPUMode = true };
                this.iMSMap1.AddChild(m_gpLayer);
                int nVectorLayerCount = m_vectorCur.LayerCount;
                for (int i = 0; i < nVectorLayerCount; i++)
                {
                    bool b = false;
                    for (int j = 0; j < lstCurLayers.Count; j++)
                    {
                        String str1 = lstCurLayers[j] + ".wt";
                        String str2 = m_vectorCur.GetLayerInfo(i).LayerAliasName;
                        b = (0 == string.Compare(str1, str2, StringComparison.CurrentCultureIgnoreCase));
                        if (b) break;
                    }
                    if (b)
                    {
                        m_vectorCur.GetMapLayerInfo(i).LayerStatus = EnumLayerStatus.Selectable;
                        m_vectorCur.GetMapLayerInfo(i).LayerStatus = EnumLayerStatus.Editable;
                    }
                }
                m_vectorCur.SetPageSize(1000, new UploadStringCompletedEventHandler(setPageSize));
                m_vectorCur.UpdateAllLayerInfo(new UploadStringCompletedEventHandler(ue));
            }
        }

        //mapgis设置查询结果的每页显示数量的方法的结果回调
        private void setPageSize(object sender, UploadStringCompletedEventArgs e)
        {
            COperResult rlt = m_vectorCur.OnSetPageSize(e);
            if (rlt.OperResult == true)
            {
                //设置查询结果中每一页的记录数量
            }
        }

        //刷新地图的回调
        private void ue(object sender, UploadStringCompletedEventArgs e)
        {
            this.iMSMap1.Refresh();
        }

        //更换地图
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string qk = comboBox1.SelectedItem.ToString();
            if (qk.Trim() == "韩城")
            {
                this.iMSMap1.RemoveChild(m_vectorCur);
                VectorMapDoc m_vectorMymap1 = null;
                m_vectorMymap1 = new VectorMapDoc
                {
                    AutoGetMapInfo = true,
                    MapDocName = map_hancheng,
                    ServerAddress = address
                };
                
                this.iMSMap1.AddChild(m_vectorMymap1);
                m_vectorCur = m_vectorMymap1;

                lstCurLayers.Clear();
                for (int i = 0; i < lstMap1Layers.Count; i++)
                {
                    lstCurLayers.Add(lstMap1Layers[i]);
                }
                this.treeView1.ItemsSource = lstMap1TreeItems;
                curQukuai = "韩城";
            }
            else if (qk.Trim() == "临汾")
            {
                this.iMSMap1.RemoveChild(m_vectorCur);
                VectorMapDoc m_vectorMymap2 = null;
                m_vectorMymap2 = new VectorMapDoc
                {
                    AutoGetMapInfo = true,
                    MapDocName = map_linfen,
                    ServerAddress = address
                };
                this.iMSMap1.AddChild(m_vectorMymap2);
                m_vectorCur = m_vectorMymap2;

                lstCurLayers.Clear();
                for (int i = 0; i < lstMap2Layers.Count; i++)
                {
                    lstCurLayers.Add(lstMap2Layers[i]);
                }
                this.treeView1.ItemsSource = lstMap2TreeItems;
                curQukuai = "临汾";
            }
            else if (qk.Trim() == "忻州")
            {
                this.iMSMap1.RemoveChild(m_vectorCur);
                VectorMapDoc m_vectorMymap3 = null;
                m_vectorMymap3 = new VectorMapDoc
                {
                    AutoGetMapInfo = true,
                    MapDocName = map_xinzhou,
                    ServerAddress = address
                };
                this.iMSMap1.AddChild(m_vectorMymap3);
                m_vectorCur = m_vectorMymap3;

                lstCurLayers.Clear();
                for (int i = 0; i < lstMap3Layers.Count; i++)
                {
                    lstCurLayers.Add(lstMap3Layers[i]);
                }
                this.treeView1.ItemsSource = lstMap3TreeItems;
                curQukuai = "忻州";
            }
        }

        //从地图查询结果集中获取每个记录对应的图层名称，返回值为集合，以","分隔
        private string getLayerName(CMapSelectAndGetAtt result)
        {
            if (result.AttDS == null || result.AttDS.Length == 0) return null;
            CAttDataSet rltDS = result.AttDS[0];
            if (rltDS == null || rltDS.attTables == null) return null;
 
            int nAttrTables = rltDS.attTables.Length;
            List<String> names = new List<string>();
            for (int j = 0; j < nAttrTables; j++)
            {
                CAttDataTable table = rltDS.attTables[j];
                if (table == null) continue;
                CAttStruct cols = rltDS.attTables[j].Columns;
                if (cols.FldName.Length == 0) continue;
                CAttDataRow[] rows = rltDS.attTables[j].Rows;
                if (rows == null || rows.Length == 0) continue;

                for (int i = 0; i < rows.Length; i++)
                {
                    for (int n = 0; n < cols.FldNumber; n++)
                    {
                        if (cols.FldName[n].Equals("name"))
                        {
                            String name = m_vectorCur.GetLayerInfo(j).LayerAliasName;
                            char[] tmp = name.ToCharArray();
                            char[] tmp2 = new char[tmp.Length - 3];
                            for (int a = 0; a < tmp2.Length; a++)
                            {
                                tmp2[a] = tmp[a];
                            }
                            name = new String(tmp2);
                            names.Add(name);
                        }
                    }
                }
            }

            if (names.Count == 0) return null;
            String str = names[0];
            for (int i = 1; i < names.Count; i++)
            {
                str += "," + names[i];
            }
            return str;
        }

        //从地图查询结果集中获取每个记录对应的对象名称，返回值为集合，以","分隔
        private string getFeatureName(CMapSelectAndGetAtt result)
        {
            if (result.AttDS == null || result.AttDS.Length == 0) return null;
            CAttDataSet rltDS = result.AttDS[0];
            if (rltDS == null || rltDS.attTables == null) return null;
            int nAttrTables = rltDS.attTables.Length;
            List<String> names = new List<string>();
            for (int j = 0; j < nAttrTables; j++)
            {
                CAttDataTable table = rltDS.attTables[j];
                if (table == null) continue;
                CAttStruct cols = rltDS.attTables[j].Columns;
                if (cols.FldName.Length == 0) continue;
                CAttDataRow[] rows = rltDS.attTables[j].Rows;
                if (rows == null || rows.Length == 0) continue;
                for (int i = 0; i < rows.Length; i++)
                {
                    for (int n = 0; n < cols.FldNumber; n++)
                    {
                        if (cols.FldName[n].Equals("name"))
                        {
                            String name = rows[i].Values[n];
                            names.Add(name);
                        }
                    }
                }
            }
            if (names.Count == 0) return null;
            String str = names[0];
            for (int i = 1; i < names.Count; i++)
            {
                str += "," + names[i];
            }
            return str;
        }


/********************************************** 点击查询和拉框查询 ***********************************************/
        //点击查询按钮事件
        private void hitSelect_Click(object sender, RoutedEventArgs e)
        {
            if (m_gpLayer != null)
            {
                m_gpLayer.DrawingType = DrawingType.Point;
                m_gpLayer.DrawingOverCallback = new DrawingEventHandler(DotSelect);
            }
        }

        //拉框查询按钮事件
        private void rectSelect_Click(object sender, RoutedEventArgs e)
        {
            if (m_gpLayer != null)
            {
                m_gpLayer.DrawingType = DrawingType.Rectangle;
                m_gpLayer.DrawingOverCallback = new DrawingEventHandler(RectSelect);
            }
        }

        //点击查询的查询方法
        private void DotSelect(GraphicsLayer gLayer, IGraphics graphics, List<Point> logPntArr)
        {
            if (logPntArr.Count > 0)
            {
                Dot_2D dot = new Dot_2D();
                dot.x = logPntArr[0].X;
                dot.y = logPntArr[0].Y;

                CMapSelectParam mapsel = new CMapSelectParam();
                CWebSelectParam websel = new CWebSelectParam();

                websel.CompareRectOnly = m_vectorCur.CompareRectOnly;
                websel.Geometry = dot;

                if (dot != null) websel.GeomType = (dot as IWebGeometry).GetGeomType();

                websel.MustInside = m_vectorCur.MustInside;
                //websel.NearDistance = m_vectorCur.NearDistanse;
                int i = comboBox1.SelectedIndex;
                int r = 0;
                if (i == 0) r = naSymbolSize[0] / 3;
                if (i == 1) r = naSymbolSize[1] / 3;
                if (i == 2) r = naSymbolSize[2] / 3;
                websel.NearDistance = r;
                websel.SelectionType = SelectionType.SpatialRange;
                mapsel.SelectParam = websel;
                m_vectorCur.Select(mapsel, new UploadStringCompletedEventHandler(onPointSelectResult));
            }
        }

        //点击查询的查询结果处理方法，该查询结果是指从地图数据中查询的结果，再按此结果到数据库查询
        private void onPointSelectResult(object sender, UploadStringCompletedEventArgs e)
        {
            lstSelectResult.Clear();
            treeChaxun.ItemsSource = lstSelectResult;
            CMapSelectAndGetAtt result = m_vectorCur.OnSelect(e);
            if (result == null) return;
            String layerNames = getLayerName(result);
            String featureNames = getFeatureName(result);
            if (featureNames == null || layerNames == null)
            {
                MessageBox.Show("没有从地图中查询到记录.");
                return;
            }

            sqlSR.SqlServiceSoapClient client = new sqlSR.SqlServiceSoapClient();
            client.getRecordCompleted += new EventHandler<sqlSR.getRecordCompletedEventArgs>(client_getRecordCompleted_Point);
            client.getRecordAsync(layerNames, featureNames, curQukuai);
        }

        //点击查询后，从数据库查询的结果处理方法，该结果包含多个表的数据
        private void client_getRecordCompleted_Point(object sender, sqlSR.getRecordCompletedEventArgs e)
        {
            if (e == null || e.Result == null)
            {
                MessageBox.Show("没有从数据库中查询到记录.");
                return;
            }

            String[] records = e.Result.ToArray<String>();
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] == null) continue;
                string[] temp = records[i].Split(';');
                for (int j = 1; j < temp.Length; j++)
                {
                    lstSelectResult.Add(records[i].Split(';')[j]);
                }
            }
            if (lstSelectResult.Count == 0)
            {
                MessageBox.Show("查询到记录数量为0.");
                return; 
            }
            List<string> lstNames = new List<string>();
            for (int i = 0; i < lstSelectResult.Count; i++)
            {
                lstNames.Add(lstSelectResult[i].Split(',')[1]);
            }
            treeChaxun.ItemsSource = lstNames;
            tabControl1.SelectedIndex = 1;
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.SetValues(lstSelectResult[0]);
            infoWindow.Show();
        }

        //拉框查询的查询方法
        private void RectSelect(GraphicsLayer gLayer, IGraphics graphics, List<Point> logPntArr)
        {
            if (logPntArr.Count > 1)
            {
                ZDIMS.BaseLib.Rect rect = new ZDIMS.BaseLib.Rect();
                rect.xmin = Math.Min(logPntArr[0].X, logPntArr[1].X);
                rect.xmax = Math.Max(logPntArr[0].X, logPntArr[1].X);
                rect.ymin = Math.Min(logPntArr[0].Y, logPntArr[1].Y);
                rect.ymax = Math.Max(logPntArr[0].Y, logPntArr[1].Y);
                CMapSelectParam mapsel = new CMapSelectParam();
                CWebSelectParam websel = new CWebSelectParam();

                websel.CompareRectOnly = m_vectorCur.CompareRectOnly;
                websel.Geometry = rect;

                if (rect != null) websel.GeomType = (rect as IWebGeometry).GetGeomType();

                websel.MustInside = m_vectorCur.MustInside;
                websel.NearDistance = m_vectorCur.NearDistanse;
                websel.SelectionType = SelectionType.SpatialRange;
                mapsel.SelectParam = websel;
                m_vectorCur.Select(mapsel, new UploadStringCompletedEventHandler(onRectSelectResult));
            }
        }

        //拉框查询的查询结果处理方法，该查询结果是指从地图数据中查询的结果，再按此结果到数据库查询
        private void onRectSelectResult(object sender, UploadStringCompletedEventArgs e)
        {
            lstSelectResult.Clear();
            treeChaxun.ItemsSource = lstSelectResult;
            CMapSelectAndGetAtt result = m_vectorCur.OnSelect(e);
            if (result == null) return;
            String layerNames = getLayerName(result);
            String featureNames = getFeatureName(result);
            if (featureNames == null || layerNames == null)
            {
                MessageBox.Show("没有查询到记录.");
                return;
            }
            sqlSR.SqlServiceSoapClient client = new sqlSR.SqlServiceSoapClient();
            client.getRecordCompleted += new EventHandler<sqlSR.getRecordCompletedEventArgs>(client_getRecordCompleted_Rect);
            client.getRecordAsync(layerNames, featureNames, curQukuai);
        }

        //拉框查询后，从数据库查询的结果处理方法，该结果包含多个表的数据
        private void client_getRecordCompleted_Rect(object sender, sqlSR.getRecordCompletedEventArgs e)
        {
            if (e == null || e.Result == null)
            {
                MessageBox.Show("没有查询到记录.");
                return;
            }
            String[] records = e.Result.ToArray<String>();
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] == null) continue;
                string[] temp = records[i].Split(';');
                for (int j = 1; j < temp.Length; j++)
                {
                    lstSelectResult.Add(records[i].Split(';')[j]);
                }
            }

            if (lstSelectResult.Count == 0)
            {
                MessageBox.Show("没有查询到记录.");
                return;
            }

            List<string> lstNames = new List<string>();
            for (int i = 0; i < lstSelectResult.Count; i++)
            {
                lstNames.Add(lstSelectResult[i].Split(',')[1]);
            }
            treeChaxun.ItemsSource = lstNames;
            tabControl1.SelectedIndex = 1;
        }
/********************************************** end ***********************************************/





/********************************************** 闪烁对象 ***********************************************/
        //按对象名称到地图数据中查询该对象的FID等信息
        private void selectFeature(String featureName)
        {
            CMapSelectParam mapsel = new CMapSelectParam();
            CWebSelectParam websel = new CWebSelectParam();
            websel.CompareRectOnly = m_vectorCur.CompareRectOnly;
            websel.Geometry = null;
            websel.MustInside = m_vectorCur.MustInside;
            websel.NearDistance = m_vectorCur.NearDistanse; 
            websel.SelectionType = SelectionType.Condition;
            websel.WhereClause = "name = '" + featureName + "'";
            mapsel.SelectParam = websel;
            m_vectorCur.Select(mapsel, new UploadStringCompletedEventHandler(onAttrSelectResult));
        }

        //按属性查询的结果处理方法，从结果集中获取对象的FID和LayerIndex，然后闪烁该对象
        private void onAttrSelectResult(object sender, UploadStringCompletedEventArgs e)
        {
            CMapSelectAndGetAtt result = m_vectorCur.OnSelect(e);
            if (result == null) return;
            CAttDataSet rltDS = result.AttDS[0];
            if (rltDS == null) return;
            int nLayerIndex = -1;
            long fid = -1;
            for (int j = 0; j < rltDS.attTables.Length; j++)
            {
                CAttDataTable table = rltDS.attTables[j];
                if (table == null) continue;
                CAttStruct cols = rltDS.attTables[j].Columns;
                if (cols.FldName.Length == 0) continue;
                CAttDataRow[] rows = rltDS.attTables[j].Rows;
                if (rows == null || rows.Length == 0) continue;

                nLayerIndex = j;
                fid = rows[0].FID;
                break;
            }
            if (nLayerIndex == -1 || fid == -1)
            {
                MessageBox.Show("没有查询到数据!");
                return;
            }

            CGetObjByID getGeo = new CGetObjByID();
            getGeo.LayerIndex = nLayerIndex;
            getGeo.FeatureID = fid;
            COpenMap openmap = new COpenMap();
            openmap.MapName = new string[1] { m_vectorCur.MapDocName };
            getGeo.MapName = openmap;
            m_vectorCur.GetGeomByID(getGeo, new UploadStringCompletedEventHandler(FlashFeature));
        }

        //闪烁对象
        private void FlashFeature(object sender, UploadStringCompletedEventArgs e)
        {
            try
            {
                m_featureGeo = m_vectorCur.OnGetGeomByID(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取该要素空间信息失败！" + ex.Message);
                return;
            }
            bool flg = false;
            if (m_featureGeo.LinGeom != null && m_featureGeo.LinGeom.Length > 0)
            {
                List<Point> pntArr = new List<Point>();
                for (int i = 0; i < m_featureGeo.LinGeom.Length; i++)
                {
                    for (int j = 0; j < m_featureGeo.LinGeom[i].Line.Arcs.Length; j++)
                        for (int k = 0; k < m_featureGeo.LinGeom[i].Line.Arcs[j].Dots.Length; k++)
                            pntArr.Add(new Point(m_featureGeo.LinGeom[i].Line.Arcs[j].Dots[k].x, m_featureGeo.LinGeom[i].Line.Arcs[j].Dots[k].y));
                }
                if (pntArr[0].X < m_vectorCur.MapContainer.WinViewBound.XMin || pntArr[0].X > m_vectorCur.MapContainer.WinViewBound.XMax ||
                    pntArr[0].Y < m_vectorCur.MapContainer.WinViewBound.YMin || pntArr[0].Y > m_vectorCur.MapContainer.WinViewBound.YMax)
                    m_vectorCur.MapContainer.PanTo(pntArr[0].X, pntArr[0].Y);
                m_graphics = new IMSPolyline(CoordinateType.Logic)
                {
                    Points = pntArr,
                    StrokeThickness = 4
                };
                flg = true;
            }
            if (m_featureGeo.PntGeom != null && m_featureGeo.PntGeom.Length > 0)
            {
                if (m_featureGeo.PntGeom[0].Dot.x < m_vectorCur.MapContainer.WinViewBound.XMin || m_featureGeo.PntGeom[0].Dot.x > m_vectorCur.MapContainer.WinViewBound.XMax ||
                    m_featureGeo.PntGeom[0].Dot.y < m_vectorCur.MapContainer.WinViewBound.YMin || m_featureGeo.PntGeom[0].Dot.y > m_vectorCur.MapContainer.WinViewBound.YMax)
                    m_vectorCur.MapContainer.PanTo(m_featureGeo.PntGeom[0].Dot.x, m_featureGeo.PntGeom[0].Dot.y);
                m_graphics = new IMSCircle(CoordinateType.Logic)
                {
                    CenX = m_featureGeo.PntGeom[0].Dot.x,
                    CenY = m_featureGeo.PntGeom[0].Dot.y,
                    RadiusEx = 6
                };
                flg = true;
            }
            if (m_featureGeo.RegGeom != null && m_featureGeo.RegGeom.Length > 0)
            {
                List<Point> pntArr = new List<Point>();
                for (int i = 0; i < m_featureGeo.RegGeom[0].Rings.Length; i++)
                    for (int j = 0; j < m_featureGeo.RegGeom[0].Rings[i].Arcs.Length; j++)
                        for (int k = 0; k < m_featureGeo.RegGeom[0].Rings[i].Arcs[j].Dots.Length; k++)
                            pntArr.Add(new Point(m_featureGeo.RegGeom[0].Rings[i].Arcs[j].Dots[k].x, m_featureGeo.RegGeom[0].Rings[i].Arcs[j].Dots[k].y));
                if (pntArr[0].X < m_vectorCur.MapContainer.WinViewBound.XMin || pntArr[0].X > m_vectorCur.MapContainer.WinViewBound.XMax ||
                    pntArr[0].Y < m_vectorCur.MapContainer.WinViewBound.YMin || pntArr[0].Y > m_vectorCur.MapContainer.WinViewBound.YMax)
                    m_vectorCur.MapContainer.PanTo(pntArr[0].X, pntArr[0].Y);
                m_graphics = new IMSPolygon(CoordinateType.Logic)
                {
                    Points = pntArr
                };
                flg = true;
            }
            if (flg)
            {
                m_gpLayer.AddGraphics(m_graphics);
                m_graphics.FlickerOverCallback = new GraphicsFlickerOverDelegate(FlickerOverCallback);
                m_graphics.Draw();
                m_graphics.Flicker();
            }
        }

        //闪烁结束的回调方法
        private void FlickerOverCallback(GraphicsBase g)
        {
            if (m_gpLayer != null && m_graphics != null)
            {
                m_gpLayer.RemoveGraphics(m_graphics);
                m_graphics = null;
            }
        }
/********************************************** end ***********************************************/


/********************************************** 操作切换 ***********************************************/
        //放大按钮
        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            this.iMSMap1.OperType = IMSOperType.ZoomIn;
        }

        //缩小按钮
        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            this.iMSMap1.OperType = IMSOperType.ZoomOut;
        }

        //复位按钮
        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            this.iMSMap1.OperType = IMSOperType.Restore;
        }

        //平移按钮
        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            this.iMSMap1.OperType = IMSOperType.Drag;
        }

        //刷新按钮
        private void btnFlash_Click(object sender, RoutedEventArgs e)
        {
            this.iMSMap1.OperType = IMSOperType.Refresh;
        }
/********************************************** end ***********************************************/



/********************************************** 条件查询 ***********************************************/
        //按条件查询按钮事件
        private void attrSearch_Click(object sender, RoutedEventArgs e)
        {
            sw = new SearchWindow();
            sw.Closed += new EventHandler(sw_Closed);
            sw.Init(curQukuai);
            sw.Show();
        }

        void sw_Closed(object sender, EventArgs e)
        {
            if (sw.DialogResult == true)
            {
                lstSelectResult.Clear();
                List<string> lstNames = new List<string>();
                treeChaxun.ItemsSource = lstNames;
                if (sw.records != null)
                {
                    for (int i = 0; i < sw.records.Length; i++)
                    {
                        lstSelectResult.Add(sw.records[i]);
                        lstNames.Add(sw.records[i].Split(',')[1]);
                    }
                    treeChaxun.ItemsSource = lstNames;
                    tabControl1.SelectedIndex = 1;
                }
            }
        }
/********************************************** end ***********************************************/


        



/********************************************** 添加单个对象 ***********************************************/
        //添加对象按钮事件
        private void addFeature_Click(object sender, RoutedEventArgs e)
        {
            String str = "";
            for (int i = 0; i < lstCurLayers.Count; i++)
            {
                if (i < lstCurLayers.Count - 1) str += lstCurLayers[i] + ",";
                else str += lstCurLayers[i];
            }
            editWindow = new EditWindow();
            editWindow.Closed += new EventHandler(ew_Closed);
            editWindow.Init(str, curQukuai);
            editWindow.Show();
        }

        //添加对象的子窗口关闭事件
        void ew_Closed(object sender, EventArgs e)
        {
            if (editWindow.DialogResult == true)
            {
                String layerName = editWindow.layer;
                String[] values = editWindow.values;
                if (layerName != null && layerName != "" && values != null)
                {
                    int index = 0;
                    int nVectorLayerCount = m_vectorCur.LayerCount;
                    for (int i = 0; i < nVectorLayerCount; i++)
                    {
                        String str1 = layerName + ".wt";
                        String str2 = m_vectorCur.GetLayerInfo(i).LayerAliasName;
                        if (string.Compare(str1, str2, StringComparison.CurrentCultureIgnoreCase) == 0)
                        {
                            m_vectorCur.ActiveLayerIndex = i;
                            index = i;
                            break;
                        }
                    }

                    int symId = 73;
                    for (int m = 0; m < 8; m++)
                    {
                        if (0 == string.Compare(saStatus[m], values[9].Trim(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            symId = naSymIds[m];
                            break;
                        }
                    }

                    CPointInfo pntInfo = new CPointInfo();
                    pntInfo.SymID = symId;
                    int iMap = comboBox1.SelectedIndex;
                    int r = 0;
                    if (iMap == 0) r = naSymbolSize[0];
                    if (iMap == 1) r = naSymbolSize[1];
                    if (iMap == 2) r = naSymbolSize[2];
                    pntInfo.SymHeight = r;
                    pntInfo.SymWidth = r;
                    pntInfo.Color = 13;

                    WebGraphicsInfo ginfo = new WebGraphicsInfo();
                    ginfo.InfoType = GInfoType.PntInfo;
                    ginfo.PntInfo = pntInfo;

                    double x = Double.Parse(values[1]);
                    double y = Double.Parse(values[2]);
                    Dot_2D pt = new Dot_2D(x, y);

                    SFeatureGeometry fgeom = new SFeatureGeometry();
                    fgeom.PntGeom = new GPoint[1];
                    fgeom.PntGeom[0] = new GPoint(pt);

                    ZDIMS.BaseLib.Rect bound = new ZDIMS.BaseLib.Rect(pt.x, pt.y, pt.x, pt.y);

                    string jh = values[0].Split('井')[0];
                    SFeature fset = new SFeature();
                    fset.AttValue = new String[] { "0", jh, "0" };
                    fset.ftype = SFclsGeomType.Pnt;
                    fset.bound = bound;
                    fset.fGeom = fgeom;

                    CMapFeatureInfo feature = new CMapFeatureInfo();
                    feature.LayerIndex = index;
                    feature.GInfo = ginfo;
                    feature.FSet = fset;
                    m_vectorCur.AddFeature(feature, new UploadStringCompletedEventHandler(onAddOperResult));
                }
            }
        }

        void client_InsertJingCompleted_add(object sender, AsyncCompletedEventArgs e)
        {
            //
        }

        //添加对象的结果处理回调
        private void onAddOperResult(object sender, UploadStringCompletedEventArgs e)
        {
            COperResult rlt = m_vectorCur.OnAddFeature(e);
            if (rlt.OperResult == true)
            {
                MessageBox.Show("添加成功", "提示", MessageBoxButton.OK);
                m_vectorCur.MapContainer.OperType = IMSOperType.Refresh;
            }
            else
            {
                MessageBox.Show("添加失败，错误信息：" + rlt.ErrorInfo, "提示", MessageBoxButton.OK);
            }
        }

        private void client_sqlExecuteCompleted_add(object sender, sqlSR.sqlExecuteCompletedEventArgs e)
        {
            //
        }
/********************************************** end ***********************************************/




/********************************************** 更新地图符号 ***********************************************/
        //更新地图符号
        private void updateSmbl_Click(object sender, RoutedEventArgs e)
        {
            sqlSR.SqlServiceSoapClient clt = null;
            clt = new sqlSR.SqlServiceSoapClient();
            clt.getRecord4Completed += new EventHandler<sqlSR.getRecord4CompletedEventArgs>(client_getUpdateDataCompleted);
            string beginDate = "";
            string endDate = "";
            clt.getRecord4Async(beginDate, endDate, curQukuai);
        }

        private void client_getUpdateDataCompleted(object sender, sqlSR.getRecord4CompletedEventArgs e)
        {
            String str = e.Result;
            if (str == null || str == "") return;
            saUpdateRecords = str.Split(';');
            if (saUpdateRecords == null || saUpdateRecords.Length < 1) return;
            CMapSelectParam mapsel = new CMapSelectParam();
            CWebSelectParam websel = new CWebSelectParam();
            websel.CompareRectOnly = m_vectorCur.CompareRectOnly;
            websel.Geometry = null;
            websel.MustInside = m_vectorCur.MustInside;
            websel.NearDistance = m_vectorCur.NearDistanse;
            websel.SelectionType = SelectionType.Condition;
            websel.WhereClause = "ID>-1";
            mapsel.SelectParam = websel;
            m_vectorCur.Select(mapsel, new UploadStringCompletedEventHandler(onAllSelectResult));
        }

        private void onAllSelectResult(object sender, UploadStringCompletedEventArgs e)
        {
            CMapSelectAndGetAtt result = m_vectorCur.OnSelect(e);
            if (result == null) return;
            CAttDataSet rltDS = result.AttDS[0];
            if (rltDS == null) return;
            for (int j = 0; j < rltDS.attTables.Length; j++)
            {
                CAttDataTable table = rltDS.attTables[j];
                if (table == null) continue;
                CAttStruct cols = rltDS.attTables[j].Columns;
                if (cols.FldName.Length == 0) continue;
                CAttDataRow[] rows = rltDS.attTables[j].Rows;
                if (rows == null || rows.Length == 0) continue;
                string strLayerName = m_vectorCur.GetLayerInfo(j).LayerAliasName;
                //MessageBox.Show("strLayerName = " + strLayerName);
                int a = -1;
                for (int i = 0; i < lstCurLayers.Count; i++)
                {
                    String str1 = lstCurLayers[i] + ".wt";
                    bool b = (0 == string.Compare(str1, strLayerName, StringComparison.CurrentCultureIgnoreCase));
                    if (b)
                    {
                        a = i;
                        break;
                    }
                }
                //MessageBox.Show("a = " + a);
                if (a == -1) continue;
                
                String[] records = saUpdateRecords;
                for (int i = 0; i < rows.Length; i++)
                {
                    String objName = rows[i].Values[1];
                    int symId = 73;
                    for (int n = 0; n < records.Length; n++)
                    {
                        string str1 = records[n].Split(',')[0];
                        if (0 == string.Compare(str1.Trim(), objName.Trim(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            string status = records[n].Split(',')[1];
                            for (int m = 0; m < 8; m++)
                            {
                                if (0 == string.Compare(saStatus[m], status.Trim(), StringComparison.CurrentCultureIgnoreCase))
                                {
                                    symId = naSymIds[m];
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    CPointInfo pntInfo = new CPointInfo();
                    pntInfo.SymID = symId;
                    if (symId == 73) pntInfo.Color = 13;
                    int iLayer = comboBox1.SelectedIndex;
                    int r = 0;
                    if (iLayer == 0) r = naSymbolSize[0];
                    if (iLayer == 1) r = naSymbolSize[1];
                    if (iLayer == 2) r = naSymbolSize[2];
                    pntInfo.SymHeight = r;
                    pntInfo.SymWidth = r;
                    WebGraphicsInfo gInfo = new WebGraphicsInfo();
                    gInfo.InfoType = GInfoType.PntInfo;
                    gInfo.PntInfo = pntInfo;
                    SFeature fset = new SFeature();
                    fset.ftype = SFclsGeomType.Pnt;
                    fset.FID = rows[i].FID;
                    CMapFeatureInfo feature = new CMapFeatureInfo();
                    feature.LayerIndex = j;
                    feature.GInfo = gInfo;
                    feature.FSet = fset;
                    m_vectorCur.UpdateFeature(feature, new UploadStringCompletedEventHandler(updateResult));
                }
            }
        }

        private void updateResult(object sender, UploadStringCompletedEventArgs e)
        {
            //COperResult rlt = m_vectorCur.OnUpdateFeature(e);
            //if (rlt.OperResult == false)
            //{
            //    MessageBox.Show("更新失败，错误信息：" + rlt.ErrorInfo, "提示", MessageBoxButton.OK);
            //}
        }
/********************************************** end ***********************************************/




/********************************************** 批量添加 ***********************************************/
        //批量添加对象按钮事件
        private void addFeatures_Click(object sender, RoutedEventArgs e)
        {
            String str = "";
            for (int i = 0; i < lstCurLayers.Count; i++)
            {
                if (i < lstCurLayers.Count - 1) str += lstCurLayers[i] + ",";
                else str += lstCurLayers[i];
            }
            addsWindow = new AddsWindow();
            addsWindow.Closed += new EventHandler(addsWindow_Closed);
            addsWindow.Init(str);
            addsWindow.Show();
        }

        void addsWindow_Closed(object sender, EventArgs e)
        {
            if (addsWindow.DialogResult == true)
            {
                String layerName = addsWindow.layer;
                String[] records = addsWindow.records;
                if (layerName == null || layerName == "") return;
                if (records == null)
                {
                    MessageBox.Show("Excel表中的列数与数据库中的不一致，无法添加！");
                    return;
                }

                int index = 0;
                int nVectorLayerCount = m_vectorCur.LayerCount;
                for (int i = 0; i < nVectorLayerCount; i++)
                {
                    String str1 = layerName + ".wt";
                    String str2 = m_vectorCur.GetLayerInfo(i).LayerAliasName;
                    if (string.Compare(str1, str2, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        m_vectorCur.ActiveLayerIndex = i;
                        index = i;
                        break;
                    }
                }

                for (int i = 0; i < records.Length; i++)
                {
                    String[] values = records[i].Split(',');

                    int symId = 73;
                    for (int m = 0; m < 8; m++)
                    {
                        if (0 == string.Compare(saStatus[m], values[9].Trim(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            symId = naSymIds[m];
                            break;
                        }
                    }

                    CPointInfo pntInfo = new CPointInfo();
                    pntInfo.SymID = symId;
                    int r = 0;
                    int iMap = comboBox1.SelectedIndex;
                    if (iMap == 0) r = naSymbolSize[0];
                    if (iMap == 1) r = naSymbolSize[1];
                    if (iMap == 2) r = naSymbolSize[2];
                    pntInfo.SymHeight = r;
                    pntInfo.SymWidth = r;
                    pntInfo.Color = 13;

                    WebGraphicsInfo ginfo = new WebGraphicsInfo();
                    ginfo.InfoType = GInfoType.PntInfo;
                    ginfo.PntInfo = pntInfo;

                    double x = Double.Parse(values[1]);
                    double y = Double.Parse(values[2]);
                    Dot_2D pt = new Dot_2D(x, y);

                    SFeatureGeometry fgeom = new SFeatureGeometry();
                    fgeom.PntGeom = new GPoint[1];
                    fgeom.PntGeom[0] = new GPoint(pt);

                    ZDIMS.BaseLib.Rect bound = new ZDIMS.BaseLib.Rect(pt.x, pt.y, pt.x, pt.y);

                    string jh = values[0].Split('井')[0];
                    SFeature fset = new SFeature();
                    fset.AttValue = new String[] { "0", jh, "0" };
                    fset.ftype = SFclsGeomType.Pnt;
                    fset.bound = bound;
                    fset.fGeom = fgeom;

                    CMapFeatureInfo feature = new CMapFeatureInfo();
                    feature.LayerIndex = index;
                    feature.GInfo = ginfo;
                    feature.FSet = fset;
                    m_vectorCur.AddFeature(feature, new UploadStringCompletedEventHandler(onAddsOperResult));
                }
            }
        }

        private void onAddsOperResult(object sender, UploadStringCompletedEventArgs e)
        {
            //COperResult rlt = m_vectorCur.OnAddFeature(e);
            //if (rlt.OperResult == false)
            //{
            //    MessageBox.Show("添加失败，错误信息：" + rlt.ErrorInfo, "提示", MessageBoxButton.OK);
            //}
        }

        private void client_sqlExecuteCompleted_adds(object sender, sqlSR.sqlExecuteCompletedEventArgs e)
        {
            //
        }
/********************************************** end ***********************************************/

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            if (treeChaxun.Items.Count > 0)
            {
                string name = treeChaxun.SelectedItem.ToString();
                if (name != null)
                {
                    for (int i = 0; i < lstSelectResult.Count; i++)
                    {
                        if (name == lstSelectResult[i].Split(',')[1])
                        {
                            InfoWindow infoWindow = new InfoWindow();
                            infoWindow.SetValues(lstSelectResult[i]);
                            infoWindow.Show();
                            break;
                        }
                    }
                }
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("删除后将不可恢复，确定删除吗？", "删除要素", MessageBoxButton.OKCancel);
            if (r == MessageBoxResult.Cancel) return;
            if (r == MessageBoxResult.OK)
            {
                if (treeChaxun.Items.Count > 0)
                {
                    string name = treeChaxun.SelectedItem.ToString();
                    CMapSelectParam mapsel = new CMapSelectParam();
                    CWebSelectParam websel = new CWebSelectParam();
                    websel.CompareRectOnly = m_vectorCur.CompareRectOnly;
                    websel.Geometry = null;
                    websel.MustInside = m_vectorCur.MustInside;
                    websel.NearDistance = m_vectorCur.NearDistanse;
                    websel.SelectionType = SelectionType.Condition;
                    websel.WhereClause = "name = '" + name + "'";
                    mapsel.SelectParam = websel;
                    m_vectorCur.Select(mapsel, new UploadStringCompletedEventHandler(onDelFeature));
                }
            }
        }

        //按属性查询的结果处理方法，用来删除对象
        private void onDelFeature(object sender, UploadStringCompletedEventArgs e)
        {
            CMapSelectAndGetAtt result = m_vectorCur.OnSelect(e);
            if (result == null) return;
            CAttDataSet rltDS = result.AttDS[0];
            if (rltDS == null) return;
            int nLayerIndex = -1;
            long fid = -1;
            for (int j = 0; j < rltDS.attTables.Length; j++)
            {
                CAttDataTable table = rltDS.attTables[j];
                if (table == null) continue;
                CAttStruct cols = rltDS.attTables[j].Columns;
                if (cols.FldName.Length == 0) continue;
                CAttDataRow[] rows = rltDS.attTables[j].Rows;
                if (rows == null || rows.Length == 0) continue;

                nLayerIndex = j;
                fid = rows[0].FID;
                break;
            }
            if (nLayerIndex == -1 || fid == -1)
            {
                MessageBox.Show("没有查询到数据!");
                return;
            }

            CGetObjByID getGeo = new CGetObjByID();
            getGeo.LayerIndex = nLayerIndex;
            getGeo.FeatureID = fid;
            m_vectorCur.DeleteFeature(getGeo, new UploadStringCompletedEventHandler(delFeature));
        }

        //删除对象的结果处理回调
        private void delFeature(object sender, UploadStringCompletedEventArgs e)
        {
            COperResult r = m_vectorCur.OnDeleteFeature(e);
            if (r.OperResult == true)
            {
                this.iMSMap1.Refresh();
                string name = treeChaxun.SelectedItem.ToString();
                List<string> lstNames = new List<string>();
                for (int i = 0; i < lstSelectResult.Count; i++)
                {
                    if (name == lstSelectResult[i].Split(',')[1])
                    {
                        lstSelectResult.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < lstSelectResult.Count; i++)
                {
                    if (lstSelectResult[i] != null)
                    {
                        string[] temp = lstSelectResult[i].Split(',');
                        if (temp.Length > 2 && temp[1] != "")
                        {
                            lstNames.Add(temp[1]);
                        }
                    }
                }

                string sql = "delete from " + dbTable + " where jinghao = '" + name + "' and qukuai = '" + curQukuai + "'";
                sqlSR.SqlServiceSoapClient client = new sqlSR.SqlServiceSoapClient();
                client.sqlExecuteCompleted += new EventHandler<sqlSR.sqlExecuteCompletedEventArgs>(client_sqlExecuteCompleted_del);
                client.sqlExecuteAsync(sql);

                treeChaxun.ItemsSource = lstNames;
            }
            else
            {
                MessageBox.Show("删除失败！" + r.ErrorInfo);
            }
        }

        private void client_sqlExecuteCompleted_del(object sender, sqlSR.sqlExecuteCompletedEventArgs e)
        {
            MessageBox.Show("删除成功！");
        }

        private void treeChaxun_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeChaxun.Items.Count > 0)
            {
                string name = treeChaxun.SelectedItem.ToString();
                selectFeature(name);
            }
        }
    }
}

