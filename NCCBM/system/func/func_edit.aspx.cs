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
public partial class system_fun_fun_edit : System.Web.UI.Page
{
    protected const string returnurl = "func_list.aspx";
    public static int iPageIndex;
    public static string QueryName;
    public static string szOldFuncName;

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
        SqlCommand Mycomm = new SqlCommand("select * from T_SYSTEM_FUNC where Func_ID = '" + Request.QueryString["Func_ID"] + "'", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        dr.Read();
        radlstate.SelectedValue = dr["state"].ToString().Trim();
        txtFunc_Name.Text = dr["Func_Name"].ToString().Trim();
        szOldFuncName = dr["Func_Name"].ToString().Trim();
        dr.Close();
        Mycomm.Connection.Close();
    }
   
    protected void addnew_Click(object sender, EventArgs e)
    {
        //判断职务名称是否相同
        bool bHave;
        bHave = VerifyUser(szOldFuncName.Replace("'", "''"), txtFunc_Name.Text.ToString().Replace("'", "''"));
        if (bHave)
        {
            this.Response.Write("<script language='JavaScript'>window.alert('请注意：该职务已经存在，请重新输入！'); </script>");
        }
        else
        {
            string sqlStr = "";
            sqlStr = "update T_SYSTEM_FUNC set  Func_Name=  '" + txtFunc_Name.Text.Replace("'", "''") + "' ,state='" + radlstate.SelectedValue + "'  where Func_ID = '" + Request.QueryString["Func_ID"] + "'";
            ArrayList strSQL = new ArrayList();
            strSQL.Add(sqlStr);
            if (strSQL.Count > 0)
            {
                common.UpdateSQL(strSQL, returnurl + "?PageIndex=" + iPageIndex + "&Type=update&QueryName=" + QueryName.Replace("'", "<`>"), 1);
            }
        }
    }

    protected bool VerifyUser(string strCurUser, string strNewUser)
    {
        bool bHave = false;
        SqlDataReader dtr;
        SqlConnection conMy = DBCONN.GetDBConn();
        string szQuery;
        szQuery = "select * from T_SYSTEM_FUNC where Func_Name='" + strNewUser + "' and Func_Name<>'" + strCurUser + "'";
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
