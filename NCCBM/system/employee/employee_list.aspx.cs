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
public partial class system_fun_System_EMPLOYEE : System.Web.UI.Page
{
    private string szType;

    protected void Page_Load(object sender, EventArgs e)
    {
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
                hidden_Select.Value = QueryName.Replace("<`>","'");
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
            if (szType == "NULL" || szType == ""||szType==null)
            {
                szType = "list";
            }
            GridView1.ShowFooter = false;
            GridView1.PageSize = common.GetPageSize();
            GridView1.Attributes.Add("BorderColor", "#0088cc");
            GridView1.Attributes.Add("BorderWidth", "1");
            switch (szType)
            {
                case "update":
                    divQuery.Visible = true;
                    GridView1.Columns[20].Visible = true;
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    break;
                case "add":
                    divQuery.Visible = false;
                    GridView1.ShowFooter = true;
                    break;
                case "delete":
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    GridView1.Columns[21].Visible = true;
                    divQuery.Visible = true;
                    break;
                case "list":
                    if (hidden_Select.Value != "") { InitDataSource(); }
                    divQuery.Visible = true;
                    GridView1.Columns[20].Visible = false;
                    GridView1.Columns[21].Visible = false;
                    break;
            }
        }
        //设置当前页
        try
        {
            int iPageIndex = Convert.ToInt32(HttpContext.Current.Request["PageIndex"].ToString());
            GridView1.PageIndex = iPageIndex;
        }
        catch
        {

        }

        zhengjianjiancha();

        SetDivePage();
    }

    private void SetDivePage()
    {
        if (GridView1.Visible == false || GridView1.PageCount == 1)
        {
            commPage1.Visible = false;
            return;
        }
        commPage1.Visible = true;
        WEE.MyPagerBar.BindDataDelegate f = new WEE.MyPagerBar.BindDataDelegate(BindData);
        commPage1.SetTarget(GridView1, f, GridView1.PageSize);
    }
    private void BindData()
    {
        InitDataSource();
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
            strQuery = "SELECT T_System_EMPLOYEE.Employee_ID, T_System_EMPLOYEE.Employee_Name, (CASE T_System_EMPLOYEE.Gender WHEN 0 THEN '男' WHEN 1 THEN '女' END) AS Gender, T_System_EMPLOYEE.Birthday, T_System_EMPLOYEE.Branch_ID, T_System_EMPLOYEE.Func_ID, T_System_EMPLOYEE.XMMCS, T_System_EMPLOYEE.HSE1, T_System_EMPLOYEE.HSE2, T_System_EMPLOYEE.LHQ1, T_System_EMPLOYEE.LHQ2,T_System_EMPLOYEE.JK1, T_System_EMPLOYEE.JK2, T_System_EMPLOYEE.JD1, T_System_EMPLOYEE.JD2,T_System_EMPLOYEE.Tel, T_System_EMPLOYEE.MobilTel, T_System_EMPLOYEE.JTel, T_System_EMPLOYEE.Email, T_System_BRANCH.Branch_Name, T_SYSTEM_FUNC.Func_Name, (CASE T_System_EMPLOYEE.state WHEN 0 THEN '在职' WHEN 1 THEN '离职' END) AS state, T_System_EMPLOYEE.ID FROM T_System_EMPLOYEE INNER JOIN T_System_BRANCH ON T_System_EMPLOYEE.Branch_ID = T_System_BRANCH.Branch_ID INNER JOIN T_SYSTEM_FUNC ON T_System_EMPLOYEE.Func_ID = T_SYSTEM_FUNC.Func_ID Where Employee_Name like'%" + strBlockName.Replace("'", "''") + "%' order by T_System_EMPLOYEE.Branch_ID,T_System_EMPLOYEE.Func_ID";
            sqlDS_DBMA.SelectCommand = strQuery;
            hidden_Select.Value = strBlockName;
            GridView1.DataSourceID = "sqlDS_DBMA";
            GridView1.DataBind();

            zhengjianjiancha();
        }
    }

    protected void InitDataSource()
    {
        //【功能】：重新为GridView绑定数据源
        string strQuery = "SELECT T_System_EMPLOYEE.Employee_ID, T_System_EMPLOYEE.Employee_Name, (CASE T_System_EMPLOYEE.Gender WHEN 0 THEN '男' WHEN 1 THEN '女' END) AS Gender, T_System_EMPLOYEE.Birthday, T_System_EMPLOYEE.Branch_ID, T_System_EMPLOYEE.Func_ID, T_System_EMPLOYEE.XMMCS, T_System_EMPLOYEE.HSE1, T_System_EMPLOYEE.HSE2, T_System_EMPLOYEE.LHQ1, T_System_EMPLOYEE.LHQ2,T_System_EMPLOYEE.JK1, T_System_EMPLOYEE.JK2, T_System_EMPLOYEE.JD1, T_System_EMPLOYEE.JD2,T_System_EMPLOYEE.Tel, T_System_EMPLOYEE.MobilTel, T_System_EMPLOYEE.JTel, T_System_EMPLOYEE.Email, T_System_BRANCH.Branch_Name, T_SYSTEM_FUNC.Func_Name, (CASE T_System_EMPLOYEE.state WHEN 0 THEN '在职' WHEN 1 THEN '离职' END) AS state, T_System_EMPLOYEE.ID FROM T_System_EMPLOYEE INNER JOIN T_System_BRANCH ON T_System_EMPLOYEE.Branch_ID = T_System_BRANCH.Branch_ID INNER JOIN T_SYSTEM_FUNC ON T_System_EMPLOYEE.Func_ID = T_SYSTEM_FUNC.Func_ID ";
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
            strQuery = strQuery + " Where T_System_EMPLOYEE.State='0' order by T_System_EMPLOYEE.Branch_ID,T_System_EMPLOYEE.Func_ID";
        }
        else
        {
            strQuery = strQuery + " Where T_System_EMPLOYEE.State='0'and  T_System_EMPLOYEE.Employee_Name like'%" + strBlockName.Replace("'", "''") + "%' order by T_System_EMPLOYEE.Branch_ID,T_System_EMPLOYEE.Func_ID";
        }
        sqlDS_DBMA.SelectCommand = strQuery;
        hidden_Select.Value = strBlockName;
        GridView1.DataSourceID = "sqlDS_DBMA";
        GridView1.DataBind();

        zhengjianjiancha();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (GridView1.Columns[11].Visible == true)
        {
            string strID;
            strID = GridView1.SelectedValue.ToString().Trim();
            Response.Redirect("employee_edit.aspx?ID=" + strID + "&PageIndex=" + GridView1.PageIndex + "&QueryName=" + hidden_Select.Value);
        }
        else if (GridView1.Columns[12].Visible == true)
        {
            string sqlStr = "";
            ArrayList strSQL = new ArrayList();
            sqlStr = "update T_System_EMPLOYEE set state= '1'  where ID = '" + GridView1.SelectedValue + "'";
            strSQL.Add(sqlStr);
            common.UpdateSQL(strSQL, "", 0);
            Response.Redirect("employee_list.aspx?&Type=delete&PageIndex=" + GridView1.PageIndex + "&QueryName=" + hidden_Select.Value);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
    }

    protected void GridView1_RowCreated(object sender, EventArgs e1)
    {
        GridViewRowEventArgs e = e1 as GridViewRowEventArgs;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            rowHeader.CssClass = "tableLineHeader";
            Literal newCells = new Literal();
            string style1 = "";
            if (szType == "list") style1 = "class=hidden";
            newCells.Text = "用户名</th>" +
                  "<th rowspan='2' nowrap>真实姓名</th>" +
                  "<th rowspan='2' nowrap>性别</th>" +
                  "<th rowspan='2' nowrap>出生日期</th>" +
                  "<th rowspan='2' nowrap>部门</th>" +
                  "<th rowspan='2' nowrap>职务</th>" +
                  "<th colspan='2' nowrap>HSE证书</th>" +
                  "<th colspan='2' nowrap>硫化氢证书</th>" +
                  "<th colspan='2' nowrap>井控证书</th>" +
                  "<th colspan='2' nowrap>监督证书</th>" +
                  "<th rowspan='2' nowrap>电话</th>" +
                  "<th rowspan='2' nowrap>手机</th>" +
                  "<th rowspan='2' nowrap>住宅电话</th>" +
                  "<th rowspan='2' nowrap>电子邮箱</th>" +
                  "<th rowspan='2' nowrap>状态</th>" +
                  "<th rowspan='2' nowrap " + style1 + ">操作</th>" +
                  "</tr><tr>";
            newCells.Text += @"                         
                      <th class=tableLinedown nowrap>起始日期</th>
                      <th class=tableLinedown nowrap>结束日期</th>
                      <th class=tableLinedown nowrap>起始日期</th>
                      <th class=tableLinedown nowrap>结束日期</th>
                      <th class=tableLinedown nowrap>起始日期</th>
                      <th class=tableLinedown nowrap>结束日期</th>
                      <th class=tableLinedown nowrap>起始日期</th>
                      <th class=tableLinedown nowrap>结束日期</th>
                      </tr>";


            TableCellCollection cells = e.Row.Cells;
            TableHeaderCell headerCell = new TableHeaderCell();
            headerCell.RowSpan = 2;
            headerCell.Controls.Add(newCells);
            rowHeader.Cells.Add(headerCell);
            rowHeader.Cells.Add(headerCell);
            rowHeader.Visible = true;
            GridView1.Controls[0].Controls.AddAt(0, rowHeader);
        }
    }

    private void zhengjianjiancha()
    {
        zjInfo.Text = "";
        int iGuoqi = 0;
        int iKuai = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            string hse = GridView1.Rows[i].Cells[8].Text.Trim();
            string lhq = GridView1.Rows[i].Cells[10].Text.Trim();
            string jk = GridView1.Rows[i].Cells[12].Text.Trim();
            string jd = GridView1.Rows[i].Cells[14].Text.Trim();
            bool b1 = false;
            bool b2 = false;
            if (hse != null && hse != "")
            {
                try
                {
                    int y = 1900;
                    int m = 1;
                    int d = 1;
                    string[] ss = hse.Split('.');
                    y = Int32.Parse(ss[0]);
                    if (ss.Length > 1) m = Int32.Parse(ss[1]);
                    if (ss.Length > 2) d = Int32.Parse(ss[2]);
                    DateTime dt = new DateTime(y, m, d);
                    if (dt <= System.DateTime.Now)
                    {
                        b1 = true;
                        GridView1.Rows[i].Cells[8].BackColor = System.Drawing.Color.Red;
                    }
                    else if (dt.Subtract(System.DateTime.Now).Days < 7)
                    {
                        b2 = true;
                        GridView1.Rows[i].Cells[8].BackColor = System.Drawing.Color.Yellow;
                    }
                }
                catch (System.Exception fe)
                {
                    
                }
            }
            if (lhq != null && lhq != "" && lhq.Split('-').Length == 3)
            {
                try
                {
                    int y = 1900;
                    int m = 1;
                    int d = 1;
                    string[] ss = lhq.Split('.');
                    y = Int32.Parse(ss[0]);
                    if (ss.Length > 1) m = Int32.Parse(ss[1]);
                    if (ss.Length > 2) d = Int32.Parse(ss[2]);
                    DateTime dt = new DateTime(y, m, d);
                    if (dt <= System.DateTime.Now)
                    {
                        b1 = true;
                        GridView1.Rows[i].Cells[10].BackColor = System.Drawing.Color.Red;
                    }
                    else if (dt.Subtract(System.DateTime.Now).Days < 7)
                    {
                        b2 = true;
                        GridView1.Rows[i].Cells[10].BackColor = System.Drawing.Color.Yellow;
                    }
                }
                catch (System.Exception fe)
                {

                }
            }
            if (jk != null && jk != "" && jk.Split('-').Length == 3)
            {
                try
                {
                    int y = 1900;
                    int m = 1;
                    int d = 1;
                    string[] ss = jk.Split('.');
                    y = Int32.Parse(ss[0]);
                    if (ss.Length > 1) m = Int32.Parse(ss[1]);
                    if (ss.Length > 2) d = Int32.Parse(ss[2]);
                    DateTime dt = new DateTime(y, m, d);
                    if (dt <= System.DateTime.Now)
                    {
                        b1 = true;
                        GridView1.Rows[i].Cells[12].BackColor = System.Drawing.Color.Red;
                    }
                    else if (dt.Subtract(System.DateTime.Now).Days < 7)
                    {
                        b2 = true;
                        GridView1.Rows[i].Cells[12].BackColor = System.Drawing.Color.Yellow;
                    }
                }
                catch (System.Exception fe)
                {

                }
            }
            if (jd != null && jd != "" && jd.Split('-').Length == 3)
            {
                try
                {
                    int y = 1900;
                    int m = 1;
                    int d = 1;
                    string[] ss = jd.Split('.');
                    y = Int32.Parse(ss[0]);
                    if (ss.Length > 1) m = Int32.Parse(ss[1]);
                    if (ss.Length > 2) d = Int32.Parse(ss[2]);
                    DateTime dt = new DateTime(y, m, d);
                    if (dt <= System.DateTime.Now)
                    {
                        b1 = true;
                        GridView1.Rows[i].Cells[14].BackColor = System.Drawing.Color.Red;
                    }
                    else if (dt.Subtract(System.DateTime.Now).Days < 7)
                    {
                        b2 = true;
                        GridView1.Rows[i].Cells[14].BackColor = System.Drawing.Color.Yellow;
                    }
                }
                catch (System.Exception fe)
                {

                }
            }
            if (b1) iGuoqi++;
            if (b2) iKuai++;
        }
        if (iGuoqi > 0 || iKuai > 0)
        {
            zjInfo.Text = "警告：有" + iGuoqi + "人证件已经过期，有" + iKuai + "人证件即将到期(小于7天)。";
        }
    }
}
