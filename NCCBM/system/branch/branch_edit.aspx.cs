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

public partial class system_branch_edit_page : System.Web.UI.Page
{
    protected const string ListInfoUrl = "branch_list.aspx";
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
            setbox();
            setdpdFatherName();
        }
        iPageIndex = Convert.ToInt32(HttpContext.Current.Request["PageIndex"].ToString());
        QueryName = HttpContext.Current.Request["QueryName"].ToString();
    }

    void DBModify(string SQLModifyStr)
    {
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand(SQLModifyStr, Myconn);
        Mycomm.Connection.Open();
        Mycomm.ExecuteReader();
        Mycomm.Connection.Close();
    }

    void setbox()
    {
        //初始化页面文本框
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("select Branch_ID,Branch_Name,Father_ID,State from T_System_BRANCH Where Branch_ID = '" + Request.QueryString["Branch_ID"] + "'", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        dr.Read();
        radlstate.SelectedValue = dr["State"].ToString().Trim();
        txtBranchName.Text = dr["Branch_Name"].ToString().Trim().Replace("'", "'");
        if (dr["Father_ID"].ToString().Trim() == "0")
        {

        }
        else
        {
            txtFAtherName.SelectedValue = dr["Father_ID"].ToString().Trim();
        }

        dr.Close();
        Mycomm.Connection.Close();
    }

    void setdpdFatherName()
    {
        //初始化页面dropdownlist
        DataSet ds = new DataSet();
        SqlConnection MyConn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("Select distinct Branch_ID,Branch_Name,State from T_System_BRANCH", MyConn);
        Mycomm.Connection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("Select distinct Branch_ID,Branch_Name,State from T_System_BRANCH", MyConn);
        ad.Fill(ds, "T_System_BRANCH");
        txtFAtherName.DataSource = ds;
        txtFAtherName.DataMember = "T_System_BRANCH";
        txtFAtherName.DataTextField = "Branch_Name";
        txtFAtherName.DataValueField = "Branch_ID";
        txtFAtherName.DataBind();
        txtFAtherName.Items.Insert(0, new ListItem("无", "0"));
       
        Mycomm.Connection.Close();
    }

    protected void makesure_Click(object sender, EventArgs e)
    {
        //判断子部门与父级部门名称是否相同
        if (txtBranchName.Text.ToString() == txtFAtherName.SelectedItem.ToString())
        {
            this.Response.Write("<script language='JavaScript'>window.alert('请注意：该子部门名称与上级部门相同，请重新录入！'); </script>");
        }
        else
        {
            //更新数据库表信息
            string sqlStr = "";
            sqlStr = "update T_System_BRANCH set Branch_Name = '" + txtBranchName.Text.Replace("'", "''") + "' , Father_ID= '" + txtFAtherName.SelectedValue + "', state='" + radlstate.SelectedItem.Value + "' where Branch_ID = '" + Request.QueryString["Branch_ID"] + "'";

            DBModify(sqlStr);

            common.returnparent(ListInfoUrl + "?PageIndex=" + iPageIndex + "&Type=update&QueryName=" + QueryName.Replace("'","<`>"));
        }        
    }
    protected void btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListInfoUrl + "?PageIndex=" + iPageIndex + "&Type=update&QueryName=" + QueryName);
    }
}