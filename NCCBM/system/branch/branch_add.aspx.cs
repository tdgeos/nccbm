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

public partial class system_branch_add_page : System.Web.UI.Page
{
    protected const string ListInfoUrl = "branch_list.aspx";
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
            setdpdFatherName();
        }
    }

    void DBModify(string SQLModifyStr)
    {
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand(SQLModifyStr, Myconn);
        Mycomm.Connection.Open();
        Mycomm.ExecuteReader();
        Mycomm.Connection.Close();
    }
    
    void setdpdFatherName()
    {
        //初始化页面dropdownlist
        DataSet ds = new DataSet();
        SqlConnection MyConn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("Select distinct Branch_ID,Branch_Name from T_System_BRANCH", MyConn);
        Mycomm.Connection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("Select distinct Branch_ID,Branch_Name from T_System_BRANCH", MyConn);
        ad.Fill(ds, "T_System_BRANCH");
        txtFAtherName.DataSource = ds;
        txtFAtherName.DataMember = "T_System_BRANCH";
        txtFAtherName.DataTextField = "Branch_Name";
        txtFAtherName.DataValueField = "Branch_ID";
        txtFAtherName.DataBind();
        txtFAtherName.Items.Insert(0, new ListItem("无", "0"));

        Mycomm.Connection.Close();
    }

    protected void addnew_Click(object sender, EventArgs e)
    {
        check_BranchName();
        if (i != 0)
        {
        }
        else
        {
            //将信息插入数据库表
            string sqlStr = "";
            sqlStr = "INSERT INTO T_System_BRANCH(Branch_Name, Father_ID,State) Values('"
                + txtBranchName.Text.Replace("'", "''") + "' , '"
                + txtFAtherName.SelectedValue + "','"
                + "0)";

            DBModify(sqlStr);
            common.returnparent(ListInfoUrl);
        }
    }
    protected void check_BranchName()
    {
        //检查该部门名称是否存在
        
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT distinct Branch_Name FROM T_System_BRANCH", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        while (dr.Read())
        {
            if (dr["Branch_Name"].ToString().Trim() == txtBranchName.Text)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该部门名称已存在，请重新录入！'); </script>");
                i = i + 1;
                break;
            }
        }
        dr.Close();
        Mycomm.Connection.Close();
    }
    protected void btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListInfoUrl + "?PageIndex=" + iPageIndex);
    }
}