using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;

/// <summary>
/// Summary description for USER_CHECK
/// </summary>
public class USER_CHECK : System.Web.UI.Page
{
    public USER_CHECK()
    {
    }

    public static void USER_CHECKER(string strLogin_Name, string strPassWord)
    {
        SqlDataReader dtr;
        SqlConnection conMy = DBCONN.GetDBConn();
        string szQuery;
        szQuery = "SELECT t1.Employee_Name, t1.Employee_ID,t2.EmpID, t2.PassWord, t2.RoleID ,t2.PlaceID FROM T_System_Role t3,T_System_USER t2,T_System_EMPLOYEE t1 where t2.EmpID = t1.Employee_ID";
        szQuery = szQuery + " and t2.EmpID='" + strLogin_Name.Replace("'","''") + "' and t2.state=0 and t3.Role_ID=t2.RoleID and t3.state=0";
        //SqlCommand cmdMy = new SqlCommand("SELECT * FROM T_System_USER WHERE Login_Name = '" + strLogin_Name + "'", conMy);
        SqlCommand cmdMy = new SqlCommand(szQuery, conMy);
        try
        {
            cmdMy.Connection.Open();
        }
        catch
        {
            //无法打开连接
            HttpContext.Current.Response.Redirect("ErrorPage.aspx");
        }
        dtr = cmdMy.ExecuteReader();
        if (dtr.Read())  //如果用户名存在,验证密码
        {
            if (strLogin_Name == dtr["EmpID"].ToString() && strPassWord == dtr["PassWord"].ToString()) //如果用户名和密码正确
            {
                //HttpCookie aCookie = new HttpCookie("MyCookies");
                //aCookie.Values["UserName"] = strLogin_Name;
                //aCookie.Values["PlaceID"] = dtr["PlaceID"].ToString();
                //HttpContext.Current.Response.Cookies.Add(aCookie); 
                HttpContext.Current.Response.Cookies["UserName"].Value = strLogin_Name;
                HttpContext.Current.Response.Cookies["PlaceID"].Value = dtr["PlaceID"].ToString();

                HttpContext.Current.Session["LoginUserID"] = strLogin_Name;
                HttpContext.Current.Session["Employee_ID"] = dtr["EmpID"].ToString();
                HttpContext.Current.Session["LoginUserName"] = dtr["Employee_Name"].ToString();
                HttpContext.Current.Session["RoleID"] = dtr["RoleID"].ToString();
                HttpContext.Current.Session["PlaceID"] = dtr["PlaceID"].ToString();
                //HttpContext.Current.Session["PageSize"] = System.Configuration.ConfigurationManager.AppSettings.Get("PageSize").ToString();
                dtr.Close();
                cmdMy.Connection.Close();

                dtr.Close();
                cmdMy.Connection.Close();
                System.Web.HttpContext.Current.Response.Redirect("Default.aspx");
            }
            else                                                            //如果密码不正确
            {
                dtr.Close();
                cmdMy.Connection.Close();
                System.Web.HttpContext.Current.Response.Write("<script>alert('密码错误！');</script>");
                System.Web.HttpContext.Current.Response.Write("<script>window.location.href=window.location.href;</script>");
            }
        }
        else            //如果用户名不存在
        {
            dtr.Close();
            cmdMy.Connection.Close();
            HttpContext.Current.ClearError();
            System.Web.HttpContext.Current.Response.Write("<script>alert('用户名不存在！');</script>");
            System.Web.HttpContext.Current.Response.Write("<script>window.location.href=window.location.href;</script>");
        }

    }

    public static bool USER_CHECKER2(string strLogin_Name, string strPassWord)
    {
        SqlDataReader dtr;
        SqlConnection conMy = DBCONN.GetDBConn();
        string szQuery;
        szQuery = "SELECT t1.Employee_Name, t1.Employee_ID,t2.EmpID, t2.PassWord, t2.RoleID ,t2.PlaceID FROM T_System_Role t3,T_System_USER t2,T_System_EMPLOYEE t1 where t2.EmpID = t1.Employee_ID";
        szQuery = szQuery + " and t2.EmpID='" + strLogin_Name.Replace("'", "''") + "' and t2.state=0 and t3.Role_ID=t2.RoleID and t3.state=0";
        //SqlCommand cmdMy = new SqlCommand("SELECT * FROM T_System_USER WHERE Login_Name = '" + strLogin_Name + "'", conMy);
        SqlCommand cmdMy = new SqlCommand(szQuery, conMy);
        try
        {
            cmdMy.Connection.Open();
        }
        catch
        {
            //无法打开连接
            System.Web.HttpContext.Current.Response.Write("<script>alert('无法验证用户名和用户密码！');</script>");
            System.Web.HttpContext.Current.Response.Write("<script>window.location.href=window.location.href;</script>");
        }
        dtr = cmdMy.ExecuteReader();
        if (dtr.Read())  //如果用户名存在,验证密码
        {
            if (strLogin_Name == dtr["EmpID"].ToString() && strPassWord == dtr["PassWord"].ToString()) //如果用户名和密码正确
            {
                //HttpCookie aCookie = new HttpCookie("MyCookies");
                //aCookie.Values["UserName"] = strLogin_Name;
                //aCookie.Values["PlaceID"] = dtr["PlaceID"].ToString();
                //HttpContext.Current.Response.Cookies.Add(aCookie); 
                HttpContext.Current.Response.Cookies["UserName"].Value = strLogin_Name;
                HttpContext.Current.Response.Cookies["PlaceID"].Value = dtr["PlaceID"].ToString();

                HttpContext.Current.Session["LoginUserID"] = strLogin_Name;
                HttpContext.Current.Session["Employee_ID"] = dtr["EmpID"].ToString();
                HttpContext.Current.Session["LoginUserName"] = dtr["Employee_Name"].ToString();
                HttpContext.Current.Session["RoleID"] = dtr["RoleID"].ToString();
                HttpContext.Current.Session["PlaceID"] = dtr["PlaceID"].ToString();
                //HttpContext.Current.Session["PageSize"] = System.Configuration.ConfigurationManager.AppSettings.Get("PageSize").ToString();
                dtr.Close();
                cmdMy.Connection.Close();

                dtr.Close();
                cmdMy.Connection.Close();
                return true;
            }
            else                                                            //如果密码不正确
            {
                dtr.Close();
                cmdMy.Connection.Close();
                System.Web.HttpContext.Current.Response.Write("<script>alert('密码错误！');</script>");
                System.Web.HttpContext.Current.Response.Write("<script>window.location.href=window.location.href;</script>");
            }
        }
        else            //如果用户名不存在
        {
            dtr.Close();
            cmdMy.Connection.Close();
            HttpContext.Current.ClearError();
            System.Web.HttpContext.Current.Response.Write("<script>alert('用户名不存在！');</script>");
            System.Web.HttpContext.Current.Response.Write("<script>window.location.href=window.location.href;</script>");
        }

        return false;
    }

    public static bool IsLogin
    {
        get {
            bool hasRoleID = HttpContext.Current.Session["RoleID"] != null;
            return hasRoleID;
        }

    }
    public static void RedirectIfNotLogin()
    {
        if (!USER_CHECK.IsLogin)
        {
            HttpContext.Current.Response.Redirect("login.aspx");
        }
    }
}
