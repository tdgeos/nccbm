using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace NCCBM
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void Login_In(object sender, EventArgs e)
        {
            string name = this.username.Value.Trim();
            string password = this.password.Value.Trim();
            string encryptedPWD = common.Desc(name, password, 0);
            USER_CHECK.USER_CHECKER(name, encryptedPWD);
            //Response.Redirect(ViewState["UrlReferrer"].ToString());
        }
    }
}