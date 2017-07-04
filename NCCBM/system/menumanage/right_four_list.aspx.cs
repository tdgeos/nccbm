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

public partial class system_menumanage_right_four_list : System.Web.UI.Page
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
            left_two_list.DataBind();
            left_two_list.SelectedValue = Request.QueryString["strtwo"];
            left_three_list.DataBind();
            left_three_list.SelectedValue = Request.QueryString["strthree"];
        }
        GVRightFourList.Attributes.Add("BorderColor", "#0088cc");
        GVRightFourList.Attributes.Add("BorderWidth", "1");
    }
    protected void btnMainMenuAdd_Click(object sender, EventArgs e)
    {
        string strone, strtwo,strthree;
        strone = top_one_list.SelectedValue.ToString();
        strtwo = left_two_list.SelectedValue.ToString();
        strthree = left_three_list.SelectedValue.ToString();
        Response.Redirect("right_four_add.aspx?strone=" + strone + "&strtwo=" + strtwo+ "&strthree="+strthree);
    }
    protected void GVRightFourList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GVRightFourList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("right_four_edit.aspx?T_ID=" + GVRightFourList.SelectedValue);
    }
    protected void top_one_list_SelectedIndexChanged(object sender, EventArgs e)
    {
        left_two_list.DataBind();
        left_three_list.DataBind();
    }
    protected void left_two_list_SelectedIndexChanged(object sender, EventArgs e)
    {
        left_three_list.DataBind();
    }
    protected void left_three_list_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVRightFourList.DataBind();
    }
}
