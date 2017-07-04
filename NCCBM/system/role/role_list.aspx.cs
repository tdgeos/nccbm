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
public partial class system_role : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szType;
        string szParentID;
        string szRoleID;
        //判断用户是否登陆,如果没有登陆直接转向登陆页面
        try
        {
            szRoleID = HttpContext.Current.Session["RoleID"].ToString();
        }
        catch
        {
            HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
        }

        szType = Request.QueryString["Type"];
        szParentID = Request.QueryString["Parent_ID"];
       
        if (!Page.IsPostBack)
        {
            try
            {
                //从修改页面返回来的
                string QueryName = HttpContext.Current.Request["QueryName"].ToString();
                hidden_Select.Value = QueryName.Replace("<`>", "'");
                txtName.Text = hidden_Select.Value;
            }
            catch
            {

                if (txtName.Text != "")
                {
                    hidden_Select.Value = txtName.Text;
                }
                else
                {
                    txtName.Text = hidden_Select.Value;
                }
            }
            txtName.Focus();
            if (szType == "NULL" || szType == "" || szType == null)
            {
                szType = "list";
            }
            Gravrole.ShowFooter = false;
            Gravrole.PageSize = common.GetPageSize();
            Gravrole.Attributes.Add("BorderColor", "#0088cc");
            Gravrole.Attributes.Add("BorderWidth", "1");
            switch (szType)
            {
                case "update":
                    divQuery.Visible = true;
                    Gravrole.Columns[3].Visible = true;
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    break;
                case "add":
                    divQuery.Visible = false;
                    Gravrole.ShowFooter = true;
                    break;
                case "delete":
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    Gravrole.Columns[4].Visible = true;
                    divQuery.Visible = true;
                    break;
                case "list":
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    divQuery.Visible = true;
                    Gravrole.Columns[4].Visible = false;
                    Gravrole.Columns[3].Visible = false;
                    break;
            }
        }
        //设置当前页
        try
        {
            int iPageIndex = Convert.ToInt32(HttpContext.Current.Request["PageIndex"].ToString());
            Gravrole.PageIndex = iPageIndex;
        }
        catch
        {

        }
    }
    protected void cmdQuery_Click(object sender, EventArgs e)
    {
        //【功能】：数据查询事件
        string strRoleName;
        string strQuery;
        if (txtName.Text == "")
        {
            string stralert = "请输入查询条件，然后进行查询！";
            common.alertMessage(stralert);
        }
        else
        {
            strRoleName = txtName.Text.Trim().Replace("'","''");
            strQuery = "SELECT Role_ID, Role_Name, (CASE T_System_Role.state WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_System_Role  Where Role_Name like'%" + strRoleName + "%' ORDER BY Role_ID";
            sqlDS_DBMA.SelectCommand = strQuery;
            hidden_Select.Value = strRoleName;
            Gravrole.DataSourceID = "sqlDS_DBMA";
            Gravrole.DataBind();
            Gravrole.PageIndex = 0;
        }

    }
 
    protected void InitDataSource()
    {
        //【功能】：重新为GridView绑定数据源
        string strQuery = "SELECT Role_ID, Role_Name, (CASE T_System_Role.state WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_System_Role";
        string strRoleName;
        strRoleName = hidden_Select.Value;
        if (txtName.Text == "")
        {
            txtName.Text = strRoleName;
        }
        else
        {
            strRoleName = txtName.Text.Trim();
        }
        if (strRoleName == "")
        {

        }
        else
        {
            strQuery = strQuery + " Where Role_Name like'%"+ strRoleName.Replace("'","''") +  "%' ORDER BY Role_ID";
        }
        sqlDS_DBMA.SelectCommand = strQuery;
        hidden_Select.Value = strRoleName;
        Gravrole.DataSourceID = "sqlDS_DBMA";
        Gravrole.DataBind();
    }
    protected void Gravrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (Gravrole.Columns[3].Visible == true)
        {
            string Role_ID;
            Role_ID = Gravrole.SelectedValue.ToString().Trim();
            Response.Redirect("role_edit.aspx?Role_ID=" + Role_ID + "&PageIndex=" + Gravrole.PageIndex + "&QueryName=" + hidden_Select.Value);
        }
        else if (Gravrole.Columns[4].Visible == true)
        {
            string sqlStr = "";
            ArrayList strSQL = new ArrayList();
            sqlStr = "update T_System_Role set state= '1'  where Role_ID = '" + Gravrole.SelectedValue + "'";
            strSQL.Add(sqlStr);
            common.UpdateSQL(strSQL, "", 0);
            Response.Redirect("role_list.aspx?&Type=delete&PageIndex=" + Gravrole.PageIndex + "&QueryName=" + hidden_Select.Value);
        }
    }
    protected void Gravrole_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gravrole.PageIndex = e.NewPageIndex;
    }
    
}
