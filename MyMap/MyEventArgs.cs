using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MyMap
{
    //定义事件参数类
    public class MyEventArgs : EventArgs
    {
        public String strLayerName;
        public String strFeatrueName;

        public MyEventArgs(String p1, String p2)
        {
            strLayerName = p1;
            strFeatrueName = p2;
        }
    }
}
