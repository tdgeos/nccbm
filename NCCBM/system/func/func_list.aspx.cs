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

public partial class system_fun_fun : System.Web.UI.Page
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
            Gravfun.ShowFooter = false;
            Gravfun.PageSize = common.GetPageSize();
            Gravfun.Attributes.Add("BorderColor", "#0088cc");
            Gravfun.Attributes.Add("BorderWidth", "1");
            switch (szType)
            {
                case "update":
                    divQuery.Visible = true;
                    Gravfun.Columns[3].Visible = true;
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    break;
                case "add":
                    divQuery.Visible = false;
                    Gravfun.ShowFooter = true;
                    break;
                case "delete":
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    Gravfun.Columns[4].Visible = true;
                    divQuery.Visible = true;
                    break;
                case "list":
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    divQuery.Visible = true;
                    Gravfun.Columns[4].Visible = false;
                    Gravfun.Columns[3].Visible = false;
                    break;
            }
        }
        //设置当前页
        try
        {
            int iPageIndex = Convert.ToInt32(HttpContext.Current.Request["PageIndex"].ToString());
            Gravfun.PageIndex = iPageIndex;
        }
        catch
        {

        }
    }
    protected void cmdQuery_Click(object sender, EventArgs e)
    {
        //【功能】：数据查询事件
        string strBlockName;
        string strQuery = "SELECT Func_ID, Func_Name, (CASE T_SYSTEM_FUNC.state WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_SYSTEM_FUNC {0} ORDER BY Func_ID";
        string whereClause = "";
        strBlockName = txtName.Text.Trim();
        if (!string.IsNullOrEmpty(strBlockName))
        {
             whereClause = string.Format(" where Func_Name like '%{0}%' ", strBlockName);
        } 
        strQuery = string.Format(strQuery, whereClause);
        sqlDS_DBMA.SelectCommand = strQuery;
        hidden_Select.Value = strBlockName;
        Gravfun.DataSourceID = "sqlDS_DBMA";
        Gravfun.DataBind();
        Gravfun.PageIndex = 0;
    }

    protected void InitDataSource()
    {
        //【功能】：重新为GridView绑定数据源
        string strQuery = "SELECT Func_ID, Func_Name, (CASE T_SYSTEM_FUNC.state WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_SYSTEM_FUNC";
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
            strQuery = strQuery + " Where  Func_Name like'%" + strBlockName.Replace("'", "''") + "%' ORDER BY Func_ID";
        }
        sqlDS_DBMA.SelectCommand = strQuery;
        hidden_Select.Value = strBlockName;
        Gravfun.DataSourceID = "sqlDS_DBMA";
        Gravfun.DataBind();
    }
    protected void Gravfun_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Gravfun.Columns[3].Visible == true)
        {
            string Func_ID;
            Func_ID = Gravfun.SelectedValue.ToString().Trim();
            Response.Redirect("func_edit.aspx?Func_ID=" + Func_ID + "&PageIndex=" + Gravfun.PageIndex + "&QueryName=" + hidden_Select.Value);
        }
        else if (Gravfun.Columns[4].Visible == true)
        {
            string sqlStr = "";
            ArrayList strSQL = new ArrayList();
            sqlStr = "update T_SYSTEM_FUNC set state= '1'  where Func_ID = '" + Gravfun.SelectedValue + "'";
            strSQL.Add(sqlStr);
            common.UpdateSQL(strSQL, "", 0);
            Response.Redirect("func_list.aspx?&Type=delete&PageIndex=" + Gravfun.PageIndex + "&QueryName=" + hidden_Select.Value);
        }
    }
    protected void Gravfun_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gravfun.PageIndex = e.NewPageIndex;
    }
}
