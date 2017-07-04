using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NCCBM.Menu;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace NCCBM
{
    public partial class Default : System.Web.UI.Page
    {
        public MuneItemHelper menu_items_cache;
        public MenuCompositeItem menu_item_left_cache;

        private string CurrentMenuId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            USER_CHECK.RedirectIfNotLogin();

            this.CurrentMenuId =  Request.QueryString["id"] == null ? "1" : Request.QueryString["id"];
            InitializeMenuItemCache(this.CurrentMenuId);
            int menu_Id = Convert.ToInt32(CurrentMenuId);

            SqlDataReader dtr;
            SqlConnection myConnection = DBCONN.GetDBConn();
            SqlCommand cmdMy = new SqlCommand("select * from T_System_APPLICATION where T_Id=" + menu_Id, myConnection);
            cmdMy.Connection.Open();
            dtr = cmdMy.ExecuteReader();
            if (dtr.Read())
            {
                int lv = Int32.Parse(dtr["Authority_Level"].ToString());
                if (lv == 1)
                {
                    HttpContext.Current.Session["leftTitle"] = dtr["Title"].ToString();
                }
                if (menu_Id == 1)
                {
                    HttpContext.Current.Session["leftTitle"] = "<img src='css/res/ico.png' style='margin-top:-3px;'>";
                }
                menuComposite_left.SelectedMenuText = dtr["T_ID"].ToString();
                if (menuComposite_left.SelectedMenuText == "1") menuComposite_left.SelectedMenuText = "111";
                if (menuComposite_left.SelectedMenuText == "2") menuComposite_left.SelectedMenuText = "211";
                if (menuComposite_left.SelectedMenuText == "3") menuComposite_left.SelectedMenuText = "311";
                if (menuComposite_left.SelectedMenuText == "4") menuComposite_left.SelectedMenuText = "411";
                if (menuComposite_left.SelectedMenuText == "6") menuComposite_left.SelectedMenuText = "611";
            }
            Label1.Text = HttpContext.Current.Session["leftTitle"].ToString();;
        }

        private void InitializeMenuItemCache(string menu_id)
        {
            menu_items_cache = new MuneItemHelper("");

            MenuCompositeItem root = (this.Master as SiteMaster).InitializeTopMenu();
            menu_items_cache.getMenuItem(root, false, "0");

            menuComposite_left.Direction = 2;
            root = new MenuCompositeItem("", "root", null, "");
            menuComposite_left.MenuItems = root;
            switch (menu_items_cache.getLevel(menu_id))
            {
            case "1":
                menu_items_cache.getMenuItem(root, true, menu_id);
                menu_item_left_cache = root;
                break;
            default:
                String str = "" + menu_id[0];
                menu_items_cache.getMenuItem(root, true, str);
                menu_item_left_cache = root;
                menuComposite_left.MenuItems = menu_item_left_cache;
                break;
            }
        }

        protected void NavigateByMenuId(string menuId)
        {
            int nodeId = Convert.ToInt32(menuId);
            string strUrl;
            string strTarget = "";
            string tid = "";

            string ugid = null;
            if (HttpContext.Current.Session["RoleID"] != null)
            {
                ugid = HttpContext.Current.Session["RoleID"].ToString();
            }

            SqlDataReader dtr;
            SqlConnection myConnection = DBCONN.GetDBConn();
            SqlCommand cmdMy = new SqlCommand("SELECT * FROM T_System_APPLICATION WHERE T_Id = " + nodeId, myConnection);
            cmdMy.Connection.Open();
            dtr = cmdMy.ExecuteReader();
            if (dtr.Read())
            {
                strUrl = dtr["NavigateUrl"].ToString();
                strTarget = dtr["Target"].ToString();
                tid = dtr["T_ID"].ToString();

                if (tid.Trim() == "2")//数据管理
                {
                    if (ugid == "3") strUrl = "Import/ZJ_Import.aspx";//钻井
                    if (ugid == "4") strUrl = "Import/YL_Import.aspx";//压裂
                }
                if (tid.Trim() == "3")//数据查询
                {
                    if (ugid == "3") strUrl = "Query/rb/rbcx.aspx?CD=1";
                    if (ugid == "4") strUrl = "Query/yl/ylcx.aspx?CD=1";
                }
                if (tid.Trim() == "4")//统计分析
                {
                    if (ugid == "3") strUrl = "FusionCharts/Tongji_Kzys.aspx";
                    if (ugid == "4") strUrl = "FusionCharts/Tongji_Gkfx.aspx";
                }
            }
            else
            {
                strUrl = "";
            }
            dtr.Close();
            cmdMy.Connection.Close();

            if (strUrl == "NULL" || strUrl == "" || strUrl == "#")
            {
                RefreshRight(0, "");
            }
            else
            {
                if (strTarget == "rightmain" || strTarget == "")
                {

                    if (strUrl.IndexOf("?") == -1)
                    {
                        RefreshRight(2, strUrl + "?Parent_ID=" + nodeId);
                    }
                    else
                    {
                        RefreshRight(2, strUrl + "&Parent_ID=" + nodeId);
                    }
                }                
                else
                {
                    if (strUrl.IndexOf("?") == -1)
                    {
                        RefreshRight(1, strUrl + "?Parent_ID=" + nodeId);
                    }
                    else
                    {
                        RefreshRight(1, strUrl + "&Parent_ID=" + nodeId);
                    }
                }
            }
        }

        protected void RefreshRight(int iType, string strUrl)
        {
            string strOutPut = "<script type=\"text/javascript\" >";
            
            switch (iType)
            {
                case 0: //strUrl为空，为根目录
                    strOutPut = strOutPut + this.HideRightMenu()+SetRightMenuUrl("null.htm");
                    strOutPut = strOutPut + this.HideRightMain()+SetRightMainUrl("null.htm");
                    break;
                case 1: //strUrl非空，为叶目录，它的Target为RightMenu
                    strOutPut = strOutPut + this.ShowRightMenu()+SetRightMenuUrl(strUrl);
                    break;
                case 2://strUrl非空，为叶目录，它的Target为RightMain                
                    strOutPut = strOutPut +this.HideRightMenu()+ SetRightMainUrl(strUrl)+this.ShowRightMain();
                    break;
            }
            strOutPut += this.DecideLeftMenuVisibility(strUrl);
            strOutPut += "</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), strOutPut,false);
        }

        protected string HideRightMenu()
        {
            return " $('#rightmenu').hide();";
        }
        protected string ShowRightMenu()
        {
            return " $('#rightmenu').show();";
        }
        protected string HideRightMain()
        {
            return " $('#rightmain').hide();";
        }
        protected string ShowRightMain()
        {
            return " $('#rightmain').show();";
        }
        protected string SetRightMenuUrl(string url)
        {
            return SetFrameUrl("rightmenu", url);
        }
        protected string SetRightMainUrl(string url)
        {
            return SetFrameUrl("rightmain", url);
        }
        protected string SetFrameUrl(string frameid, string url)
        {
            string script = string.Format("$('#{0}').attr('src','{1}');", frameid, url);
            return script;
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            NavigateByMenuId(this.CurrentMenuId);
        }

        protected string DecideLeftMenuVisibility(string strUrl)
        {
            DynamicUrl dynUrl = new DynamicUrl(strUrl);
            if (dynUrl.ContainsKey("noleftmenu")) return " $('div.arrowlistmenu').hide();";
            else return " $('div.arrowlistmenu').show();";
        }

        public static string GetRootURI()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            HttpRequest Req;
            if (HttpCurrent != null)
            {
                Req = HttpCurrent.Request;
                string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
                if (Req.ApplicationPath == null || Req.ApplicationPath == "/") 
                    AppPath = UrlAuthority;
                else 
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }
        
    }
}