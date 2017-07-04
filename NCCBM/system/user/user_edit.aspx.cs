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
public partial class system_user_user_edit : System.Web.UI.Page
{
    protected const string returnurl = "user_list.aspx";
    public static int iPageIndex;
    public static string QueryName;

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
            setTextBox();
            setRoleData();
        }
        iPageIndex = Convert.ToInt32(HttpContext.Current.Request["PageIndex"].ToString());
        QueryName = HttpContext.Current.Request["QueryName"].ToString();
    }
    void setTextBox()
    {
        string strPassword;
        string strSQL;
        strSQL = "select Employee_ID from t_system_employee where (Employee_ID not in(select EmpID from t_system_user where EmpID<>'" + Request.QueryString["EmpID"].Replace("'","''") + "'))";
        SqlDataSource1.SelectCommand = strSQL;
        drp1EmpID.DataSourceID = "SqlDataSource1";
        drp1EmpID.DataBind();
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT * FROM T_System_USER WHERE EmpID ='" + Request.QueryString["EmpID"].Replace("'", "''") + "'", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        dr.Read();
        strPassword = common.Desc(Request.QueryString["EmpID"], dr["PassWord"].ToString().Trim(), 1);
        
        txtPassWord.Text = strPassword;
        
        drop2RoleID.Text = dr["RoleID"].ToString().Trim();
        radlstate.SelectedValue = dr["state"].ToString().Trim();
        drp1EmpID.SelectedValue = dr["EmpID"].ToString().Trim();
        lstQukuai.SelectedIndex = Int32.Parse(dr["PlaceID"].ToString().Trim());
        
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
        bHave = VerifyUser(Request.QueryString["EmpID"].Replace("'", "''"), drp1EmpID.Text.ToString().Replace("'", "''"));
        if (bHave)
        {
            this.Response.Write("<script language='JavaScript'>window.alert('请注意：该用户已经存在，请重新选择！'); </script>");
        }
        else
        {
            string sqlStr = "";
            sqlStr = "update T_System_USER set EmpID = '" + drp1EmpID.SelectedValue.Replace("'", "''") + "' , PassWord= '" + common.Desc(drp1EmpID.SelectedValue, txtPassWord.Text.Trim(), 0) + "',  RoleID= '" + drop2RoleID.SelectedValue.Replace("'", "''") + "', state = '" + radlstate.SelectedValue + "', PlaceID = '" + lstQukuai.SelectedIndex +  "'  where EmpID = '" + Request.QueryString["EmpID"].Replace("'", "''") + "'";

            ArrayList strSQL = new ArrayList();
            strSQL.Add(sqlStr);
            if (strSQL.Count > 0)
            {
                common.UpdateSQL(strSQL, returnurl + "?PageIndex=" + iPageIndex + "&Type=update&QueryName=" + QueryName.Replace("'", "<`>"), 1);
            }
        }
    }

    protected bool VerifyUser(string strCurUser,string strNewUser)
    {
        bool bHave = false;
        SqlDataReader dtr;
        SqlConnection conMy = DBCONN.GetDBConn();
        string szQuery;
        szQuery = "select * from T_SYSTEM_USER where EmpID='"+strNewUser+"' and EmpID<>'"+ strCurUser+"'" ;
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
        Response.Redirect(returnurl + "?PageIndex=" + iPageIndex + "&Type=update&QueryName=" + QueryName);
    }
}
