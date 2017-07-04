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
using NCCBM;

public partial class system_user_System_user : System.Web.UI.Page
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
        catch (NullReferenceException ne)
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
            Gravuser.ShowFooter = false;
            Gravuser.PageSize = common.GetPageSize();
            Gravuser.Attributes.Add("BorderColor", "#0088cc");
            Gravuser.Attributes.Add("BorderWidth", "1");
            switch (szType)
            {
                case "update":
                    divQuery.Visible = true;
                    Gravuser.Columns[5].Visible = true;
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    break;
                case "add":
                    divQuery.Visible = false;
                    Gravuser.ShowFooter = true;
                    break;
                case "delete":
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    Gravuser.Columns[6].Visible = true;
                    divQuery.Visible = true;
                    break;
                case "list":
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    divQuery.Visible = true;
                    Gravuser.Columns[5].Visible = false;
                    Gravuser.Columns[6].Visible = false;
                    break;
            }
        }
        //设置当前页
        try
        {
            int iPageIndex = Convert.ToInt32(HttpContext.Current.Request["PageIndex"].ToString());
            Gravuser.PageIndex = iPageIndex;
        }
        catch
        {

        }
    }
    protected void cmdQuery_Click(object sender, EventArgs e)
    {
        //【功能】：数据查询事件

        string strBlockName;
        string strQuery;
        if (txtName.Text == "")
        {
            string stralert = "请输入查询条件，然后进行查询！";
            common.alertMessage(stralert);
        }
        else
        {
            strBlockName = txtName.Text.Trim();
            strQuery = "SELECT DISTINCT T_System_USER.PassWord, T_System_USER.RoleID, T_System_Role.Role_Name, T_System_USER.EmpID, (CASE T_System_USER.PlaceID WHEN 0 THEN '全部' WHEN 1 THEN '韩城' WHEN 2 THEN '临汾' WHEN 3 THEN '忻州' END) AS PlaceID, (CASE T_System_USER.State WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_System_USER INNER JOIN T_System_Role ON T_System_USER.RoleID = T_System_Role.Role_ID Where EmpID like'%" + strBlockName.Replace("'", "''") + "%'";
            sqlDS_DBMA.SelectCommand = strQuery;
            hidden_Select.Value = strBlockName;
            Gravuser.DataSourceID = "sqlDS_DBMA";
            Gravuser.DataBind();
            Gravuser.PageIndex = 0;
        }
    }

    protected void InitDataSource()
    {
        //【功能】：重新为GridView绑定数据源
        string strQuery = "SELECT DISTINCT T_System_USER.PassWord, T_System_USER.RoleID, T_System_Role.Role_Name, T_System_USER.EmpID, (CASE T_System_USER.PlaceID WHEN 0 THEN '全部' WHEN 1 THEN '韩城' WHEN 2 THEN '临汾' WHEN 3 THEN '忻州' END) AS PlaceID,  (CASE T_System_USER.State WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_System_USER INNER JOIN T_System_Role ON T_System_USER.RoleID = T_System_Role.Role_ID";
        string strBlockName;
        strBlockName = hidden_Select.Value;
        if (txtName.Text == "")
        {
            txtName.Text = strBlockName;
        }
        else
        {
            strBlockName = txtName.Text.Trim();
        }
        if (strBlockName == "")
        {

        }
        else
        {
            strQuery = strQuery + "  Where EmpID like'%" + strBlockName.Replace("'","''") + "%'";
        }
        hidden_Select.Value = strBlockName;

        //sqlDS_DBMA.SelectCommand = strQuery;
        //Gravuser.DataSourceID = "sqlDS_DBMA";
        DataTable dt = DataBaseHelper.query(strQuery);
        Gravuser.DataSource = dt;
        Gravuser.DataBind();
    }
    protected void Gravuser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Gravuser.Columns[4].Visible == true)
        {
            string EmpID;
            EmpID = Gravuser.SelectedValue.ToString().Trim();
            Response.Redirect("user_edit.aspx?EmpID=" + EmpID + "&PageIndex=" + Gravuser.PageIndex + "&QueryName=" + hidden_Select.Value);
        }
        else if (Gravuser.Columns[5].Visible == true)
        {
            string sqlStr = "";
            ArrayList strSQL = new ArrayList();
            sqlStr = "update T_System_USER set state= '1'  where EmpID = '" + Gravuser.SelectedValue + "'";
            strSQL.Add(sqlStr);
            common.UpdateSQL(strSQL, "", 0);
            Response.Redirect("user_list.aspx?&Type=delete&PageIndex=" + Gravuser.PageIndex + "&QueryName=" + hidden_Select.Value);
        }
    }
    protected void Gravuser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gravuser.PageIndex = e.NewPageIndex;
    }

}
