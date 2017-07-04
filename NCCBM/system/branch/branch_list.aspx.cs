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

public partial class system_branch_rightmain : System.Web.UI.Page
{
    SqlConnection sqlcon;
    SqlCommand sqlcom;

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
        if (!IsPostBack)
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
            if (szType == "NULL" || szType == "")
            {
                szType = "list";
            }
            grdvBranch.ShowFooter = false;
            grdvBranch.Columns[4].Visible = false;
            grdvBranch.Columns[5].Visible = false;
            grdvBranch.PageSize = common.GetPageSize();
            grdvBranch.Attributes.Add("BorderColor", "#0088cc");
            grdvBranch.Attributes.Add("BorderWidth", "1");

            switch (szType)
            {
                case "update":
                    grdvBranch.Columns[4].Visible = true;
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    break;
                case "add":
                    grdvBranch.FooterRow.Visible = true;
                    break;
                case "delete":
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    grdvBranch.Columns[5].Visible = true;
                    break;
                case "list":
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    break;
            }
        }
        
       
       
        //设置当前页
        try
        {
            int iPageIndex = Convert.ToInt32(HttpContext.Current.Request["PageIndex"].ToString());
            grdvBranch.PageIndex = iPageIndex;
        }
        catch
        {

        }
    }

    protected void lbtnMakesure_Click(object sender, EventArgs e)
    {
        //【功能】：添加数据时的确认事件
        TextBox Branch_Name = grdvBranch.FooterRow.FindControl("TxtaddBranchName") as TextBox;
        DropDownList Father_Name = grdvBranch.FooterRow.FindControl("DpdFatherName") as DropDownList;

        sqlDS_DBMA.InsertParameters["Branch_Name"].DefaultValue = Branch_Name.Text;
        sqlDS_DBMA.InsertParameters["Father_ID"].DefaultValue = Father_Name.SelectedValue;
        sqlDS_DBMA.Insert();

        grdvBranch.ShowFooter = false;
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        //【功能】：添加数据时的取消事件
        grdvBranch.FooterRow.Visible = false;
    }

    protected void cmdQuery_Click(object sender, EventArgs e)
    {
        //【功能】：数据查询事件
        string strBranchName;
        string strQuery;
        if (txtName.Text == "")
        {
            string stralert = "请输入查询条件，然后进行查询！";
            common.alertMessage(stralert);
        }
        else
        {
            strBranchName = txtName.Text.Trim();
            strQuery = "SELECT Branch_ID, Branch_Name, (SELECT Branch_Name FROM T_System_BRANCH WHERE (Branch_ID = t.Father_ID)) AS Father_Name, (CASE t.state WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_System_BRANCH t Where Branch_Name like'%" + strBranchName.Replace("'", "''") + "%' ORDER BY Branch_ID";
            sqlDS_DBMA.SelectCommand = strQuery;
            hidden_Select.Value = strBranchName;
            grdvBranch.DataSourceID = "sqlDS_DBMA";
            grdvBranch.DataBind();
        }
    }

    protected void InitDataSource()
    {
        //【功能】：重新为GridView绑定数据源
        string strQuery = "SELECT Branch_ID, Branch_Name, (SELECT Branch_Name FROM T_System_BRANCH WHERE (Branch_ID = t.Father_ID)) AS Father_Name, (CASE t.state WHEN 0 THEN '可用' WHEN 1 THEN '停用' END) AS State FROM T_System_BRANCH t ";
        string strBranchName;
        strBranchName = hidden_Select.Value;
        if (txtName.Text == "")
        {
            txtName.Text = strBranchName;
        }
        else
        {
            strBranchName = txtName.Text.Trim();
        }
        if (strBranchName == "")
        {

        }
        else
        {
            strQuery =strQuery+  " Where  Branch_Name like '%" + strBranchName.Replace("'", "''") + "%' ORDER BY Branch_ID";
        }
        sqlDS_DBMA.SelectCommand = strQuery;
        hidden_Select.Value = strBranchName;
        grdvBranch.DataSourceID = "sqlDS_DBMA";
        grdvBranch.DataBind();
    }

    protected void grdv_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //【功能】：重定向页面,并实现页面的传值
        if (grdvBranch.Columns[4].Visible == true)
        {
            string Branch_ID;
            Branch_ID = grdvBranch.SelectedValue.ToString().Trim();
            Response.Redirect("branch_edit.aspx?Branch_ID=" + Branch_ID + "&PageIndex=" + grdvBranch.PageIndex + "&QueryName=" + hidden_Select.Value);
        }
        else if (grdvBranch.Columns[5].Visible == true)
        {
            string sqlStr = "";
            ArrayList strSQL = new ArrayList();
            sqlStr = "update T_System_BRANCH set state= '1'  where Branch_ID = '" + grdvBranch.SelectedValue + "'";
            strSQL.Add(sqlStr);
            common.UpdateSQL(strSQL, "", 0);
            Response.Redirect("branch_list.aspx?&Type=delete&PageIndex=" + grdvBranch.PageIndex + "&QueryName=" + hidden_Select.Value);
        }
    }

    void DBModify(string SQLModifyStr)
    {
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand(SQLModifyStr, Myconn);
        Mycomm.Connection.Open();
        Mycomm.ExecuteReader();
        Mycomm.Connection.Close();
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string strBranch = "";
        string strChildBrand = "";
        strBranch = "delete from T_System_BRANCH where Branch_ID = '" + grdvBranch.SelectedValue + "'";
        strChildBrand = "delete from T_System_BRANCH where Father_ID = '" + grdvBranch.SelectedValue + "'";
        DBModify(strBranch);
    }

    protected void grdv_Branch_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        string sqlstr2 = "delete from T_System_BRANCH where Father_ID='" + grdvBranch.DataKeys[e.RowIndex].Value.ToString() + "'";
        sqlcon = DBCONN.GetDBConn();
        sqlcom = new SqlCommand(sqlstr2, sqlcon);
        sqlcon.Open();
        sqlcom.ExecuteNonQuery();
        sqlcon.Close();
    }
}
