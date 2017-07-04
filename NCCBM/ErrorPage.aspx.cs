using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NCCBM
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage em = (ErrorMessage)Session["error_message"];
            if (em != null)
            {
                message.Text = em.message;
                if (em.jump_url != null)
                {
                    this.url.HRef = em.jump_url;
                    this.url.InnerText = em.jump_text;
                }
            }
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

    public class ErrorMessage
    {
        public string message = null;
        public string jump_url = null;
        public string jump_text = null;

        public ErrorMessage(string message, string jump_url, string jump_text)
        {
            this.message = message;
            this.jump_url = jump_url;
            this.jump_text = jump_text;
        }
    }
}