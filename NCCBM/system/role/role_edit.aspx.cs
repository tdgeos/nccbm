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

public partial class system_role_role_edit : System.Web.UI.Page
{
    protected const string returnurl = "role_list.aspx";
    public static int iPageIndex;
    public static string QueryName;
    public static string szOldRoleName;

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
        }
        iPageIndex = Convert.ToInt32(HttpContext.Current.Request["PageIndex"].ToString());
        QueryName = HttpContext.Current.Request["QueryName"].ToString();

    }
    void setTextBox()
    {
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT * FROM T_System_Role WHERE Role_ID ='" + Request.QueryString["Role_ID"] + "'", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        dr.Read();
        txtRole_Name.Text = dr["Role_Name"].ToString().Trim();
        szOldRoleName = dr["Role_Name"].ToString().Trim();
        radlstate.SelectedValue = dr["state"].ToString().Trim();

        dr.Close();
        Mycomm.Connection.Close();
    }
  
    protected void addnew_Click(object sender, EventArgs e)
    {
        //判断用户组名称是否相同
        bool bHave;
        bHave = VerifyExist(szOldRoleName.Replace("'", "''"), txtRole_Name.Text.ToString().Replace("'", "''"));
        if (bHave)
        {
            this.Response.Write("<script language='JavaScript'>window.alert('请注意：该用户组已经存在，请重新输入！'); </script>");
        }
        else
        {
            string sqlStr = "";
            sqlStr = "update T_System_Role set  Role_Name='" + txtRole_Name.Text.Replace("'", "''") + "',state='" + radlstate.SelectedValue + "' where Role_ID = '" + Request.QueryString["Role_ID"] + "'";

            ArrayList strSQL = new ArrayList();
            strSQL.Add(sqlStr);
            if (strSQL.Count > 0)
            {
                common.UpdateSQL(strSQL, returnurl + "?PageIndex=" + iPageIndex + "&Type=update&QueryName=" + QueryName.Replace("'", "<`>"), 1);
            }

        }
    }

    protected bool VerifyExist(string strCurName, string strNewName)
    {
        bool bHave = false;
        SqlDataReader dtr;
        SqlConnection conMy = DBCONN.GetDBConn();
        string szQuery;
        szQuery = "select * from T_System_Role where Role_Name='" + strNewName + "' and Role_Name<>'" + strCurName + "'";
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
