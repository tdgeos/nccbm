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

public partial class system_menumanage_left_three_list : System.Web.UI.Page
{
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
            top_one_list.SelectedValue = Request.QueryString["strone"];
            left_two_list.SelectedValue = Request.QueryString["strtwo"];
        }
        GVLeftThreeList.Attributes.Add("BorderColor", "#0088cc");
        GVLeftThreeList.Attributes.Add("BorderWidth", "1");
    }
    protected void btnMainMenuAdd_Click(object sender, EventArgs e)
    {
        string strone,strtwo;
        strone = top_one_list.SelectedValue.ToString();
        strtwo = left_two_list.SelectedValue.ToString();
        Response.Redirect("left_three_add.aspx?strone=" + strone + "&strtwo=" + strtwo);
    }
    protected void GVLeftThreeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("left_three_edit.aspx?T_ID=" + GVLeftThreeList.SelectedValue);
    }
    protected void GVLeftThreeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }    
    protected void top_one_list_SelectedIndexChanged(object sender, EventArgs e)
    {
        left_two_list.DataBind();
    }

    protected void left_two_list_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVLeftThreeList.DataBind();
    }


    protected void GVLeftThreeList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int t_delete_id = Convert.ToInt16(e.Keys[0]);
        
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT distinct Parent_ID FROM T_System_APPLICATION WHERE Parent_ID =" + t_delete_id, Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        if (dr.Read())
        {
            e.Cancel = true;
            string stralert = "请将此菜单的下一级菜单全部删除后再删除此级菜单！";
            common.alertMessage(stralert);
        }
        else
        {
            e.Cancel = false;
            string stralert = "确定要删除吗？删除后数据将不可恢复！";
            common.alertMessage(stralert);
        }
        dr.Close();
        Mycomm.Connection.Close();
    }
}
