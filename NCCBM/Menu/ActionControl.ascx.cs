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
using NCCBM;
namespace NCCBM.Menu
{
    public partial class ActionControl : System.Web.UI.UserControl
    {
        string addUrl = "wj_list.aspx";
        IActioner page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = Parent as IActioner;

            //get privelege in database
            AddBtn.Visible = true;
            ModifyBtn.Visible = true;
            DeleteBtn.Visible = true;
            QueryBtn.Visible = true;
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            page.AddAction();
        }
      

        protected void ModifyBtn_Click(object sender, EventArgs e)
        {
            page.ModifyAction();
        }

        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            page.DeleteAction();

        }

        protected void QueryBtn_Click(object sender, EventArgs e)
        {
            page.DeleteAction();
        }
    }
}