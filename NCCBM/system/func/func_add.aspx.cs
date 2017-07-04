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
public partial class system_fun_fun_add : System.Web.UI.Page
{
    protected const string returnurl = "func_list.aspx";
    public static int iPageIndex;

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

        //判断职务名称是否相同
        bool bHave;
        string funcName=txtFunc_Name.Text.ToString().Replace("'", "''");
        bHave = VerifyUser(funcName);

        if (bHave)
        {
            this.Response.Write("<script language='JavaScript'>window.alert('请注意：该职务已经存在，请重新选择！'); </script>");
        }
        else
        {
            //将信息插入数据库表
            string sqlStr = "";
            sqlStr = string.Format("INSERT INTO T_SYSTEM_FUNC (Func_Name,State) Values('{0}',0);",funcName);
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
        szQuery = "select * from T_SYSTEM_FUNC where Func_Name='" + strNewUser + "'";
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
