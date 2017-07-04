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

public partial class system_menumanage_right_four_edit : System.Web.UI.Page
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
            setTextBox();

            Set_Top_One();
            Set_Left_Two();
            Set_Left_Three();
            txtTitle.Focus();
        }
    }

    protected void top_one_list_SelectedIndexChanged(object sender, EventArgs e)
    {
        Set_Left_Two();
        Set_Left_Three();
    }
    protected void left_two_list_SelectedIndexChanged(object sender, EventArgs e)
    {
        Set_Left_Three();
    }
    //顶端一级菜单下拉框
    void Set_Top_One()
    {
        DataSet ds = new DataSet();
        SqlConnection MyConn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("Select * from T_System_APPLICATION where Authority_Level='1'", MyConn);
        Mycomm.Connection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("Select * from T_System_APPLICATION where Authority_Level='1'", MyConn);
        ad.Fill(ds, "T_System_APPLICATION");

        top_one_list.DataSource = ds;
        top_one_list.DataMember = "T_System_APPLICATION";
        top_one_list.DataTextField = "Title";
        top_one_list.DataValueField = "T_ID";
        top_one_list.DataBind();

        Mycomm.Connection.Close();
    }
    //左侧二级下拉框
    void Set_Left_Two()
    {
        DataSet ds = new DataSet();
        SqlConnection MyConn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("Select * from T_System_APPLICATION WHERE (Authority_Level='2'and Parent_ID = '" + top_one_list.SelectedValue + "')", MyConn);
        Mycomm.Connection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("Select * from T_System_APPLICATION WHERE (Authority_Level='2'and Parent_ID = '" + top_one_list.SelectedValue + "')", MyConn);
        ad.Fill(ds, "T_System_APPLICATION");

        left_two_list.DataSource = ds;
        left_two_list.DataMember = "T_System_APPLICATION";
        left_two_list.DataTextField = "Title";
        left_two_list.DataValueField = "T_ID";
        left_two_list.DataBind();

        Mycomm.Connection.Close();
    }
    //左侧三级菜单下拉框
    void Set_Left_Three()
    {
        DataSet ds = new DataSet();
        SqlConnection MyConn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("Select * from T_System_APPLICATION WHERE (Authority_Level='3'and Parent_ID = '" + left_two_list.SelectedValue + "')", MyConn);
        Mycomm.Connection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("Select * from T_System_APPLICATION WHERE (Authority_Level='3'and Parent_ID = '" + left_two_list.SelectedValue + "')", MyConn);
        ad.Fill(ds, "T_System_APPLICATION");

        left_three_list.DataSource = ds;
        left_three_list.DataMember = "T_System_APPLICATION";
        left_three_list.DataTextField = "Title";
        left_three_list.DataValueField = "T_ID";
        left_three_list.DataBind();

        Mycomm.Connection.Close();
    }
    //初始化数据
    void setTextBox()
    {
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT * FROM T_System_APPLICATION WHERE T_ID ='" + Request.QueryString["T_ID"] + "'", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        dr.Read();

        DropDownAuthority_Status.SelectedValue = dr["Authority_Status"].ToString().Trim();
        txtTitle.Text = dr["Title"].ToString().Trim();
        txtNavigateUrl.Text = dr["NavigateUrl"].ToString().Trim();
        txtOrderLevel.Text = dr["OrderLevel"].ToString().Trim();
        txtAuthority_Desc.Text = dr["Authority_Desc"].ToString().Trim();
        txtRemark.Text = dr["Remark"].ToString().Trim();

        Parent_ID.Text = dr["Parent_ID"].ToString().Trim();
        left_three_list.SelectedValue = dr["Parent_ID"].ToString().Trim();

        set_up_Parent_ID();
        
        dr.Close();
        Mycomm.Connection.Close();
    }
    //初始化左侧二级菜单下拉框数据
    void set_up_Parent_ID() 
    {
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT * FROM T_System_APPLICATION WHERE T_ID ='" + Parent_ID.Text + "'", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        dr.Read();

        upParent_ID.Text = dr["Parent_ID"].ToString().Trim();
        left_two_list.SelectedValue = dr["Parent_ID"].ToString().Trim();
        set_up_up_Parent_ID();

        dr.Close();
        Mycomm.Connection.Close();
    }
    //初始化顶端一级菜单下拉框数据
    void set_up_up_Parent_ID()  
    {
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT * FROM T_System_APPLICATION WHERE T_ID ='" + upParent_ID.Text + "'", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        dr.Read();
        top_one_list.SelectedValue = dr["Parent_ID"].ToString().Trim();

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
        string strone, strtwo, strthree, ListInfoUrl;
        strone = top_one_list.SelectedValue.ToString();
        strtwo = left_two_list.SelectedValue.ToString();
        strthree = left_three_list.SelectedValue.ToString();
        ListInfoUrl = "right_four_list.aspx?strone=" + strone + "&strtwo=" + strtwo + "&strthree=" + strthree;

        if (DropDownAuthority_Status.SelectedValue == "")
        {
            this.Response.Write("<script language='JavaScript'>window.alert('请注意：菜单属性不能为空，请选择！'); </script>");
        }
        else
        {
            if (intnumber == 0)
            {
                string sqlStr = "UPDATE T_System_APPLICATION SET Title='" + txtTitle.Text + "',Parent_ID='" + left_three_list.SelectedValue + "' ,NavigateUrl='" + txtNavigateUrl.Text + "',OrderLevel='" + txtOrderLevel.Text + "',";
                sqlStr += " Authority_Desc='" + txtAuthority_Desc.Text + "',Authority_Status='" + DropDownAuthority_Status.SelectedValue + "',Remark='" + txtRemark.Text + "'  WHERE T_ID = " + Int32.Parse(Request.QueryString["T_ID"]);
                DBModify(sqlStr);

                common.returnparent(ListInfoUrl);
            }
            else
            {
                common.alertMessage(StrMeg);
            }
        }
    }

    //判断菜单名称的唯一性
    public void checkname(Object src, EventArgs e)
    {
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT distinct Title FROM T_System_APPLICATION WHERE (Authority_Level='4' and Parent_ID='" + left_three_list.SelectedValue + "')", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        while (dr.Read())
        {
            if (dr["Title"].ToString().Trim() == txtTitle.Text)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该菜单名称已存在，请重新录入！'); </script>");
                intnumber = intnumber + 1;
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

}
