using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using NCCBM.AppCode;
using System.Web.SessionState;
using System.Data;

namespace NCCBM.Menu
{
    [DefaultProperty("Menu")]
    [ToolboxData("<{0}:MenuComposite runat=server></{0}:MenuComposite>")]
    public class MenuComposite : WebControl, IRequiresSessionState
    {
        private int _direction;

        public int Direction
        {
            //myVar  
            get { return _direction; }
            set { _direction = value; }
        }  

        /// <summary> 
        /// 设置获取选择的菜单 
        /// </summary> 
        [Bindable(true)]
        [DefaultValue("")]
        [Localizable(true)]
        public string SelectedMenuText
        {
            get
            {
                String s = (String)ViewState["SelectedMenuText"];
                return ((s == null) ? String.Empty : s);
            }
            set
            {
                ViewState["SelectedMenuText"] = value;
            }
        }
        /// <summary> 
        /// 获取和设置菜单项从ViewState 
        /// </summary> 
        [Bindable(true)]
        [DefaultValue(null)]
        [Localizable(true)]
        public MenuCompositeItem MenuItems
        {
            get
            {
                return ViewState["MenuItems"] as MenuCompositeItem;
            }
            set
            {
                ViewState["MenuItems"] = value;
            }
        }
        /// <summary> 
        /// 呈现菜单结构 
        /// </summary> 
        /// <param name="output">HTML输出流</param> 
        protected override void RenderContents(HtmlTextWriter output)
        {
            string ugid = null;
            if (HttpContext.Current.Session["RoleID"] != null)
            {
                ugid = HttpContext.Current.Session["RoleID"].ToString();
            }

            MenuCompositeItem root = this.MenuItems;
            if (null == root) return;
            switch (this._direction)
            {
                case 1:
                    output.Write(@"<div class='navmenu_top'>");
                    break;
                case 2:
                    output.Write(@"<div class='ti' ></div>");
                    output.Write(@"<div>");
                    break;
            }
            for (int i = 0; i < root.Children.Count; i++)
            {
                if (!isHaveMenu(ugid, root.Children[i].Id))
                {
                    continue;
                }
                if (root.Children[i].Level == "2" && root.Children[i].Text == "主要信息")
                {
                    output.Write(@"<div class='arrowlistmenu'>");
                    RecursiveRender2(output, root.Children[i]);
                    output.Write(@" </div>");
                }
                else if (root.Children[i].Level == "2")
                {
                    output.Write(@"<div class='arrowlistmenu'>");
                    RecursiveRender(output, root.Children[i]);
                    output.Write(@" </div>");
                }
                else
                {
                    output.Write(@"<div class='content'> <ul> <li>");
                    RecursiveRender(output, root.Children[i]);
                    output.Write(@" </li> </ul> </div>");
                }
            }
            output.Write(@"</div>");
        }
        /// <summary> 
        /// 递归输出菜单项 
        /// </summary> 
        /// <param name="output">HTML输出流</param> 
        /// <param name="item">菜单项.</param> 
        /// <param name="depth">Indentation depth.</param> 
        private void RecursiveRender(HtmlTextWriter output, MenuCompositeItem item)
        {
            string ugid = null;
            if (HttpContext.Current.Session["RoleID"] != null)
            {
                ugid = HttpContext.Current.Session["RoleID"].ToString();
            }
            if (!isHaveMenu(ugid, item.Id))
            {
                return;
            }

            if (string.IsNullOrEmpty(item.Target))//为空不设置跳转目标 
            {
                if (item.Level == "2")
                {
                    output.Write(@"<h3 class='menuheader expandable'>");
                    output.Write("<span class='accordprefix'></span>"); 
                }
                else
                {
                    string html = string.Format("<a href=\"../{0}?id={1}\">", MySettings.DefaultPageUrl, item.Id);
                    output.Write(html);
                }
            }
            else
            {
                if (item.Level == "2")
                {
                    output.Write(@"<h3 class='menuheader expandable'>");
                }
                else
                {
                    string html = string.Format("<a href=\"../{0}?id={1}\" target=\"{2}\">", MySettings.DefaultPageUrl, item.Id, item.Target);
                    output.Write(html);
                }
            }
                
            if (item.Id == SelectedMenuText) //选中的菜单 
            {
                if (item.Level == "3")
                {
                    string str = "<span style=\"color:#ff00ff;\"> " + item.Text + "</span>";
                    output.WriteLine(str);
                }
                else
                {
                    output.WriteLine(item.Text);
                }
            }
            else
            {
                if (item.Level == "3")
                {
                    string str = "<span style=\"color:Blue;\"> " + item.Text + "</span>";
                    output.Write(str);
                }
                else
                {
                    output.Write(item.Text);
                }
            }

            if (item.Level == "2")
            {
                output.Write("<span class='accordsuffix'></span>");
                output.Write("</h3>");
            }
            else
            {
                output.Write("</a>");
            }

            if (item.Children.Count > 0)
            {
                output.WriteLine();
                output.Write("<ul class='categoryitems'>");
                for (int i = 0; i < item.Children.Count; i++)
                {
                    output.Write("<li>");
                    RecursiveRender(output, item.Children[i]);
                    output.Write("</li>");
                }
                output.Write("</ul>");
            }
        }

        private void RecursiveRender2(HtmlTextWriter output, MenuCompositeItem item)
        {
            if (item.Children.Count > 0)
            {
                //output.Write(@"<h3 class='menuheader expandable'>");
                //output.Write("<span class='accordprefix'></span>"); 

                //output.WriteLine();
                output.Write("<ul >");
                for (int i = 0; i < item.Children.Count; i++)
                {
                    output.Write("<li>");

                    string html = string.Format("<a href=\"../{0}?id={1}\" target=\"{2}\">", MySettings.DefaultPageUrl, item.Children[i].Id, item.Children[i].Target);
                    output.Write(html);
                    if (item.Children[i].Id == SelectedMenuText) //选中的菜单 
                    {
                        string str = "<span style=\"color:#ff00ff;\"> " + item.Children[i].Text + "</span>";
                        output.Write(str);
                    }
                    else
                    {
                        string str = "<span style=\"color:Blue;\"> " + item.Children[i].Text + "</span>";
                        output.Write(str);
                    }
                    output.Write("</a>");
                    output.Write("</li>");
                }
                output.Write("</ul>");

                //output.Write("<span class='accordsuffix'></span>");
                //output.Write("</h3>");
            }
        }

        private bool isHaveMenu(string ugid, string menuid)
        {
            string sql = "select * from T_System_ROLE_APPLICATION where RoleID='" + ugid + "' and ApplicationID='" + menuid + "'";
            DataTable dt = DataBaseHelper.query(sql);
            if (dt.Rows.Count > 0) return true;
            return false;
        }
    }
}
