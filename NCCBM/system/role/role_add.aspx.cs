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
public partial class system_role_role_add : System.Web.UI.Page
{
    protected const string returnurl = "role_list.aspx";
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

        }

    }
  
    protected void addnew_Click(object sender, EventArgs e)
    {

        //判断用户组名称是否相同
        bool bHave;
        bHave = VerifyUser(txtRole_Name.Text.ToString().Replace("'","''"));

        if (bHave)
        {
            this.Response.Write("<script language='JavaScript'>window.alert('请注意：该用户组已经存在，请重新输入！'); </script>");
        }
        else
        {
            //将信息插入数据库表
            string sqlStr = "";
            sqlStr = "INSERT INTO T_System_Role (Role_Name,State) Values( '" + txtRole_Name.Text.Replace("'","''") + "',0 )";
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
        szQuery = "select * from T_System_Role where Role_Name='" + strNewUser + "'";
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
        Response.Redirect(returnurl + "?PageIndex=" + iPageIndex );

    }
}
