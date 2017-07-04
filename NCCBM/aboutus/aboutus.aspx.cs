using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NCCBM.aboutus
{
    public partial class aboutus : System.Web.UI.Page
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
                HttpContext.Current.Response.Redirect("NoLogin.aspx");
            }

        }
    }
}