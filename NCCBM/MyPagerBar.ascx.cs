using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WEE
{
    public partial class MyPagerBar : System.Web.UI.UserControl
    {
        public delegate void BindDataDelegate();//事件委托
        //事件委托绑定数据
        private BindDataDelegate BindData;
        private GridView _gv;//要操作的GridView控件
        private int pagesize = 1;//默认的分页数

        private void Page_Load(object sender, System.EventArgs e)
        {
        }
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="adg">GridView控件名</param>
        /// <param name="NewBind">数据绑定事件委托</param>
        /// <param name="iPageSize">每页显示数</param>
        public void SetTarget(GridView agv, BindDataDelegate NewBind, int iPageSize)
        {
            _gv = agv;
            BindData = new BindDataDelegate(NewBind);

            this.First.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            this.Previous.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            this.Next.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            this.Lastly.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            _gv.DataBinding += new System.EventHandler(this.BeforeDataBinding);

            _gv.AllowPaging = true;
            _gv.PagerSettings.Visible = false;
            pagesize = iPageSize;
            _gv.PageSize = pagesize;
            _gv.PagerStyle.HorizontalAlign = HorizontalAlign.Right;
        }

        public void SetTarget(GridView agv, BindDataDelegate NewBind, int iPageSize, int recCount)
        {
            if (recCount <= iPageSize)
            {
                this.Visible = false;
                return;
            }
            
            _gv = agv;
            BindData = new BindDataDelegate(NewBind);

            this.First.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            this.Previous.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            this.Next.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            this.Lastly.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            _gv.DataBinding += new System.EventHandler(this.BeforeDataBinding);

            _gv.AllowPaging = true;
            _gv.PagerSettings.Visible = false;
            pagesize = iPageSize;
            _gv.PageSize = pagesize;
            _gv.PagerStyle.HorizontalAlign = HorizontalAlign.Right;

            int PageCount = (int)((recCount - 1) / pagesize + 1);
            PageNumber.Text = (_gv.PageIndex + 1).ToString() + "/" + PageCount.ToString();
            PageBulk.Text = pagesize.ToString();
            RecordNumber.Text = recCount.ToString();
        }


        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="adg">GridView控件名</param>
        /// <param name="NewBind">数据绑定事件委托</param>
        public void SetTarget(GridView agv, BindDataDelegate NewBind)
        {
            _gv = agv;
            BindData = new BindDataDelegate(NewBind);

            this.First.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            this.Previous.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            this.Next.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            this.Lastly.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            _gv.DataBinding += new System.EventHandler(this.BeforeDataBinding);

            _gv.AllowPaging = true;
            _gv.PagerSettings.Visible = false;
            _gv.PageSize = pagesize;
            _gv.PagerStyle.HorizontalAlign = HorizontalAlign.Right;
        }



        //分页按钮事件
        private void NavigationButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string strCommandName = ((ImageButton)sender).ID;//取得事件的对象名

            switch (strCommandName)
            {
                case "First":
                    _gv.PageIndex = 0;//跳转到首页
                    PageOperate();
                    break;
                case "Previous":
                    _gv.PageIndex = Math.Max(_gv.PageIndex - 1, 0);//跳转到上一页
                    PageOperate();
                    break;
                case "Next":
                    _gv.PageIndex = Math.Min(_gv.PageIndex + 1, _gv.PageCount - 1);//跳转到下一页
                    PageOperate();
                    break;
                case "Lastly":
                    _gv.PageIndex = _gv.PageCount - 1;//跳转到最后一页
                    PageOperate();
                    break;
            }
            BindData();//绑定数据
        }
        //页次显示改变
        private void PageOperate()
        {
            PageNumber.Text = (_gv.PageIndex + 1).ToString() + "/" + _gv.PageCount.ToString();//页次
            PageBulk.Text = pagesize.ToString();
        }

        //页面跳转
        public void Go_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (SelectPage.Text.Trim() != "")
            {
                int iPage = int.Parse(SelectPage.Text.Trim()) - 1;
                if (iPage >= 0 && iPage <= _gv.PageCount - 1)
                {
                    _gv.PageIndex = iPage;
                    PageOperate();
                    BindData();
                }
                else if (iPage > _gv.PageCount - 1)
                {
                    _gv.PageIndex = _gv.PageCount - 1;
                    BindData();
                }
                else
                    _gv.PageIndex = 0;
                BindData();
            }
            else
                ImageButton1.EnableViewState = true;

        }

        /// <summary>
        /// GridView数据邦定前，设置导航条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeDataBinding(object sender, System.EventArgs e)
        {
            int newCount = 0;
            int PageCount = 0;
            if (_gv.DataSource != null)
            {
                if (_gv.DataSource.GetType().ToString().ToLower() == "system.data.datatable")
                {
                    newCount = ((DataTable)_gv.DataSource).Rows.Count;
                }
                else if (_gv.DataSource.GetType().ToString().ToLower() == "system.data.dataview")
                {
                    newCount = ((DataView)_gv.DataSource).Table.Rows.Count;
                }
                else if (_gv.DataSource.GetType().ToString().ToLower() == "system.data.dataset")
                {
                    newCount = ((DataSet)_gv.DataSource).Tables[0].Rows.Count;
                }
            }
            else
            {
                SqlDataSource sds = (SqlDataSource)_gv.DataSourceObject;
                newCount = ((DataView)sds.Select(DataSourceSelectArguments.Empty)).Count;
            }

            PageCount = (int)((newCount - 1) / pagesize + 1);
            PageNumber.Text = (_gv.PageIndex + 1).ToString() + "/" + PageCount.ToString();
            PageBulk.Text = pagesize.ToString();
            RecordNumber.Text = newCount.ToString();
        }
    }
}
