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
public partial class system_user_user_add : System.Web.UI.Page
{
    protected const string returnurl = "user_list.aspx";
    public static int iPageIndex;
    public static string QueryName;
    int i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        string szRoleID;
        //判断用户是否登陆,如果没有登陆直接转向登陆页面
        try
        {
            szRoleID = HttpContext.Current.Session["RoleID"].ToString();
        }
        catch (NullReferenceException ne)
        {
            HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
        }

        if (!Page.IsPostBack)
        {
           
            setEmpData();
            setRoleData();
            
        }
    }
    void setEmpData()
    {
        //初始化页面文本框
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("select Employee_ID from t_system_employee where (Employee_ID not in(select EmpID from t_system_user))and state=0", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        if (dr.Read())
        {
            DataSet ds = new DataSet();
            SqlConnection MyConnDrop = DBCONN.GetDBConn();
            SqlCommand MycommDrop = new SqlCommand("select Employee_ID from t_system_employee where (Employee_ID not in(select EmpID from t_system_user))and state=0", MyConnDrop);
            MycommDrop.Connection.Open();
            SqlDataAdapter ad = new SqlDataAdapter("select Employee_ID from t_system_employee where (Employee_ID not in(select EmpID from t_system_user))and state=0", MyConnDrop);
            ad.Fill(ds, "T_System_EMPLOYEE");
            drp1EmpID.DataSource = ds;
            drp1EmpID.DataMember = "T_System_EMPLOYEE";
            drp1EmpID.DataTextField = "Employee_ID";
            drp1EmpID.DataValueField = "Employee_ID";
            drp1EmpID.DataBind();
            MycommDrop.Connection.Close();
        }
        else
        {
            string stralert = " 请先定义人员表";
            common.alertMessage(stralert);
        }
        dr.Close();
        Mycomm.Connection.Close();
    }
    void setRoleData()
    {
        //初始化页面文本框
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT Role_ID, Role_Name, State FROM T_System_Role WHERE State =0", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        if (dr.Read())
        {
            DataSet ds = new DataSet();
            SqlConnection MyConnDrop = DBCONN.GetDBConn();
            SqlCommand MycommDrop = new SqlCommand("SELECT Role_ID, Role_Name, State FROM T_System_Role WHERE State =0", MyConnDrop);
            MycommDrop.Connection.Open();
            SqlDataAdapter ad = new SqlDataAdapter("SELECT Role_ID, Role_Name, State FROM T_System_Role WHERE State =0", MyConnDrop);
            ad.Fill(ds, "T_System_Role");
            drop2RoleID.DataSource = ds;
            drop2RoleID.DataMember = "T_System_Role";
            drop2RoleID.DataTextField = "Role_Name";
            drop2RoleID.DataValueField = "Role_ID";
            drop2RoleID.DataBind();
            MycommDrop.Connection.Close();
        }
        else
        {
            string stralert = " 请先定义用户组";
            common.alertMessage(stralert);
        }
        dr.Close();
        Mycomm.Connection.Close();

    } 

    protected void addnew_Click(object sender, EventArgs e)
    {

        //判断用户名称是否相同
        bool bHave;
        bHave = VerifyUser(drp1EmpID.Text.ToString().Replace("'", "''"));

        if (bHave)
        {
            this.Response.Write("<script language='JavaScript'>window.alert('请注意：该用户已经存在，请重新选择！'); </script>");
        }
        else
        {
            //将信息插入数据库表
            string sqlStr = "";
            sqlStr = "INSERT INTO T_System_USER(EmpID, PassWord,RoleID,State,PlaceID) Values('" + drp1EmpID.SelectedValue.Replace("'", "''") 
                + "' , '" + common.Desc(drp1EmpID.SelectedValue, txtPassWord.Text.Trim(), 0) 
                + "' ,'" + drop2RoleID.SelectedValue.Replace("'", "''") + "',0," +  lstQukuai.SelectedIndex + ")";
            ArrayList strSQL = new ArrayList();
            strSQL.Add(sqlStr);
            if (strSQL.Count > 0)
            {
                common.UpdateSQL(strSQL, returnurl + "?PageIndex=" + iPageIndex, 1);
            }

        }
    }
    
    protected bool VerifyUser(string strNewUser)
    {
        bool bHave = false;
        SqlDataReader dtr;
        SqlConnection conMy = DBCONN.GetDBConn();
        string szQuery;
        szQuery = "select * from T_SYSTEM_USER where EmpID='" + strNewUser + "'";
        SqlCommand cmdMy = new SqlCommand(szQuery, conMy);
        cmdMy.Connection.Open();
        dtr = cmdMy.ExecuteReader();
        if (dtr.Read())
        {
            bHave = true;
        }
        cmdMy.Dispose();
        conMy.Close();
        return bHave;
    }
    protected void btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(returnurl + "?PageIndex=" + iPageIndex);
    }
  
}
