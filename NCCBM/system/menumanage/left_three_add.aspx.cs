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

public partial class system_menumanage_left_three_add : System.Web.UI.Page
{
    public static int intnumber = 0;
    protected const string StrMeg = "请注意：该菜单名称已存在，请重新录入！";
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
            top_one_list.DataBind();
            top_one_list.Focus();
            top_one_list.SelectedValue = Request.QueryString["strone"];
            left_two_list.DataBind(); 
            left_two_list.SelectedValue = Request.QueryString["strtwo"];
        }
    }
    protected void top_one_list_SelectedIndexChanged(object sender, EventArgs e)
    {
        left_two_list.DataBind();
    }
    public void checkname(Object src, EventArgs e)
    {
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT distinct Title FROM T_System_APPLICATION WHERE (Authority_Level='3' and Parent_ID='"+ left_two_list.SelectedValue +"')", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        while (dr.Read())
        {
            if (dr["Title"].ToString().Trim() == txtTitle.Text)
            {
                intnumber = intnumber + 1;
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该菜单名称已存在，请重新录入！'); </script>");
                break;
            }
            else
            {
                intnumber = 0;
            }
        }
        dr.Close();
        Mycomm.Connection.Close();
    }
    void DBModify(string SQLModifyStr)
    {
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand(SQLModifyStr, Myconn);
        Mycomm.Connection.Open();
        Mycomm.ExecuteReader();
        Mycomm.Connection.Close();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string strone, strtwo,  ListInfoUrl;
        strone = top_one_list.SelectedValue.ToString();
        strtwo = left_two_list.SelectedValue.ToString();
        ListInfoUrl = "left_three_list.aspx?strone=" + strone + "&strtwo=" + strtwo;
        if (intnumber == 0)
        {
            string sqlStr = "";
            sqlStr = "INSERT INTO T_System_APPLICATION(Title,Parent_ID,Authority_Level,NavigateUrl,OrderLevel,Target,Authority_Desc,Authority_Status,Remark) Values('" + txtTitle.Text + "' , '" + left_two_list.SelectedValue + "' , '3','" + txtNavigateUrl.Text + "','" + txtOrderLevel.Text + "' , '" + selected_position.SelectedValue + "','" + txtAuthority_Desc.Text + "' ,'0' , '" + txtRemark.Text + "')";

            DBModify(sqlStr);
            common.returnparent(ListInfoUrl);
        }
        else
        {
            common.alertMessage(StrMeg);
        }
    }
}
