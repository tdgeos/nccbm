using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NCCBM.Menu;

namespace NCCBM
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string szRoleID = HttpContext.Current.Session["RoleID"].ToString();
            }
            catch (NullReferenceException ne)
            {
                HttpContext.Current.Response.Redirect("NoLogin.aspx");
            }

        }

        public MenuComposite TopMenu
        {
            get { return this.menuComposite_top; }
        }

        public MenuCompositeItem InitializeTopMenu()
        {
            MenuCompositeItem root = new MenuCompositeItem("", "root", null, "");
           
            this.TopMenu.Direction = 1;
            this.TopMenu.MenuItems = root;
            return root;
        }

    }
}