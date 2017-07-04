using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using NCCBM.Menu;

public partial class rightmenu : System.Web.UI.Page
{
    private static string leftTitle = "当前页面名称";
    public string szDefaultURL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string szRoleID;
        try
        {
            szRoleID = HttpContext.Current.Session["RoleID"].ToString();
        }
        catch (NullReferenceException ne)
        {
            HttpContext.Current.Response.Redirect("NoLogin.aspx");
        }
        
        if (!IsPostBack)
        {            
           InitMenu();           
        }
    }

    protected void InitMenu()
    {
        SqlConnection myConn;
        SqlDataReader dtr;
        string strImage;
        string strTarget;
        string strType;
        string szTable = "<table style='margin-left:200px;'class='mefont' width='70%'  height='35' border='0' cellpadding='0' cellspacing='0'><tr  align='left' valign='middle'>";
        string szTDT = "<td style='width:8%' align='left' valign='middle'>";
        string szTDM = "";
        string szTDW = "</td>";
        string szQuery;
        string szRoleID;
        string szParentID;
        
        int i;
        strTarget = "rightmain";
        try
        {
            //获取数据连接语句，并创建数据库连接对象
            szRoleID = HttpContext.Current.Session["RoleID"].ToString();
            szParentID = Request.QueryString["Parent_ID"];
            myConn = DBCONN.GetDBConn();
            //增加权限控制
            szQuery = "SELECT t1.* FROM T_System_ROLE_APPLICATION t2,T_System_APPLICATION t1 where t2.RoleID = " + szRoleID + " and t2.ApplicationID = t1.T_ID and t1.Authority_Level=4 and t1.parent_id=" + szParentID + " order by t1.OrderLevel";
            SqlCommand cmdMy = new SqlCommand(szQuery, myConn);
            cmdMy.Connection.Open();
            dtr = cmdMy.ExecuteReader();
            strImage = "images/3.png";
            strType = "list";
            i = 0;
            int haspr = 0;
            while (dtr.Read())
            {
                haspr = 1;
                switch (dtr["Authority_Status"].ToString())
                {
                    case "0":
                        strImage = "css/res/3.png";
                        strType = "list";
                        break;
                    case "1":
                        strImage = "css/res/4.png";
                        strType = "add";
                        break;
                    case "2":
                        strImage = "css/res/5.png";
                        strType = "update";
                        break;
                    case "3":
                        strImage = "css/res/6.png";
                        strType = "delete";
                        break;
                }

                strTarget = dtr["Target"].ToString();
                                
                if (dtr["NavigateURL"].ToString().IndexOf("?") != -1)
                {
                    szTDM = "<div class=submit style='margin-top:5px;'><a style='margin-left:14px; color: #fff;' href=' " + dtr["NavigateURL"].ToString() + "&Type=" + strType + "&Parent_ID=" + dtr["T_ID"].ToString() + "' target='" + strTarget + "'><img src='" + strImage + "' />" + dtr["Title"].ToString() + "</a></div>";
                }
                else
                {
                    szTDM = "<div class=submit style='margin-top:5px;'><a style='margin-left:14px; color: #fff;' href='" + dtr["NavigateURL"].ToString() + "?Type=" + strType + "&Parent_ID=" + dtr["T_ID"].ToString() + "' target='" + strTarget + "'><img src='" + strImage + "' />" + dtr["Title"].ToString() + "</a></div>";
                }
                szTable = szTable + szTDT + szTDM + szTDW;
                if(i==0)
                {
                    if (dtr["NavigateURL"].ToString().IndexOf("?") != -1)
                    {
                        szDefaultURL = dtr["NavigateURL"].ToString() + "&Type=" + strType + "&Parent_ID=" + dtr["T_ID"].ToString();
                    }
                    else
                    {
                        szDefaultURL = dtr["NavigateURL"].ToString() + "?Type=" + strType + "&Parent_ID=" + dtr["T_ID"].ToString();
                    }
                }
                i = 1;
            }
            dtr.Close();
            cmdMy.Connection.Close();
            if (haspr == 0) {
                Response.Write("<script>alert('无此菜单权限！')</script>");
            }
            string strQuery_title = "select * from T_System_APPLICATION where T_ID=" + szParentID;
            SqlCommand cmdMy_title = new SqlCommand(strQuery_title, myConn);
            cmdMy_title.Connection.Open();
            SqlDataReader dtr_title = cmdMy_title.ExecuteReader();
            while (dtr_title.Read())
            {
                leftTitle = dtr_title["Title"].ToString();
            }
            szTable = szTable + "</tr></table>";
            Response.Write(szTable);
        }
        catch (Exception e)
        {
            throw (e);
        }

        this.lblTitle.Text = leftTitle;
    }
}
