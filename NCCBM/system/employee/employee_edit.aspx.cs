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

public partial class system_employee_EMPLOYEE_edit : System.Web.UI.Page
{
    protected const string returnurl = "employee_list.aspx";
    public static int iPageIndex;
    public static string QueryName;
    public static string szOldEmployee_ID;
    DateTime Datetoday = System.DateTime.Now;
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
            HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
        }

        if (!Page.IsPostBack)
        {
            setTextBox();
        }
        iPageIndex = Convert.ToInt32(HttpContext.Current.Request["PageIndex"].ToString());
        QueryName = HttpContext.Current.Request["QueryName"].ToString();
        // txtBirthday.Attributes.Add("onclick", "Fcalendar(this,'../../ChooseTime.aspx');");

    }
    //初始化人员表
    void setTextBox()
    {
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT T_System_EMPLOYEE.*, T_SYSTEM_FUNC.Func_Name AS Func_Name,T_System_BRANCH.Branch_Name AS Branch_Name FROM T_System_EMPLOYEE INNER JOIN T_SYSTEM_FUNC ON  T_System_EMPLOYEE.Func_ID = T_SYSTEM_FUNC.Func_ID INNER JOIN  T_System_BRANCH ON  T_System_EMPLOYEE.Branch_ID = T_System_BRANCH.Branch_ID WHERE ID ='" + Request.QueryString["ID"] + "'", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        dr.Read();

        txtEmployee_Name.Text = dr["Employee_Name"].ToString().Trim();
        txtEmployee_ID.Text = dr["Employee_ID"].ToString().Trim();
        szOldEmployee_ID = dr["Employee_ID"].ToString().Trim();
        radlGender.Text = dr["Gender"].ToString().Trim();
        txtMobilTel.Text = dr["MobilTel"].ToString().Trim();
        dropBranch_ID.Text = dr["Branch_ID"].ToString().Trim();
        dropFunc_ID.Text = dr["Func_ID"].ToString().Trim();
        radlstate.SelectedValue = dr["state"].ToString().Trim();
        txtTel.Text = dr["Tel"].ToString().Trim();
        if (dr["Birthday"].ToString() == "")
        {
            txtBirthday.Text = "";
        }
        else
        {
            txtBirthday.Text = dr["Birthday"].ToString().Trim();
        }
        if (dr["HSE1"].ToString() == "")
        {
            tbHSE1.Text = "";
        }
        else
        {
            tbHSE1.Text = dr["HSE1"].ToString().Trim();
        }
        if (dr["HSE2"].ToString() == "")
        {
            tbHSE2.Text = "";
        }
        else
        {
            tbHSE2.Text = dr["HSE2"].ToString().Trim();
        }
        if (dr["LHQ1"].ToString() == "")
        {
            tbLHQ1.Text = "";
        }
        else
        {
            tbLHQ1.Text = dr["LHQ1"].ToString().Trim();
        }
        if (dr["LHQ2"].ToString() == "")
        {
            tbLHQ2.Text = "";
        }
        else
        {
            tbLHQ2.Text = dr["LHQ2"].ToString().Trim();
        }
        if (dr["JK1"].ToString() == "")
        {
            tbJK1.Text = "";
        }
        else
        {
            tbJK1.Text = dr["JK1"].ToString().Trim();
        }
        if (dr["JK2"].ToString() == "")
        {
            tbJK2.Text = "";
        }
        else
        {
            tbJK2.Text = dr["JK2"].ToString().Trim();
        }
        if (dr["JD1"].ToString() == "")
        {
            tbJD1.Text = "";
        }
        else
        {
            tbJD1.Text = dr["JD1"].ToString().Trim();
        }
        if (dr["JD2"].ToString() == "")
        {
            tbJD2.Text = "";
        }
        else
        {
            tbJD2.Text = dr["JD2"].ToString().Trim();
        }

        txtJTel.Text = dr["JTel"].ToString().Trim();
        txtEmail.Text = dr["Email"].ToString().Trim();
        string strXMMCS = dr["XMMCS"].ToString().Trim();
        char[] arrTemp = new char[] { ';' };

        if (strXMMCS != "" && strXMMCS != "NULL")
        {
            string[] arrXMMCS = strXMMCS.Split(arrTemp[0]);
            for (int k = 0; k < arrXMMCS.Length; k++)
            {
                if (arrXMMCS[k] == "全部")
                    cbAll.Checked = true;
                if (arrXMMCS[k] == "紫金山")
                    cbZJS.Checked = true;
                if (arrXMMCS[k] == "三交北")
                    cbSJB.Checked = true;
                if (arrXMMCS[k] == "三交")
                    cbSJ.Checked = true;
                if (arrXMMCS[k] == "石楼北")
                    cbSLB.Checked = true;
                if (arrXMMCS[k] == "石楼南")
                    cbSLN.Checked = true;
                if (arrXMMCS[k] == "韩城")
                    cbHC.Checked = true;
                if (arrXMMCS[k] == "保田青山")
                    cbQS.Checked = true;
                if (arrXMMCS[k] == "硫磺沟")
                    cbLHG.Checked = true;
                if (arrXMMCS[k] == "大宁")
                    cbDN.Checked = true;
                if (arrXMMCS[k] == "永利")
                    cbYL.Checked = true;
                if (arrXMMCS[k] == "石楼西")
                    cbSLX.Checked = true;
                if (arrXMMCS[k] == "格日敖勒1")
                    cbGRAL1.Checked = true;
                if (arrXMMCS[k] == "格日敖勒2")
                    cbGRAL2.Checked = true;
            }
        }
        dr.Close();
        Mycomm.Connection.Close();
    }

    protected void addnew_Click(object sender, EventArgs e)
    {

        //判断用户名称是否相同
        bool bHave;
        bHave = VerifyExist(szOldEmployee_ID.Replace("'", "''"), txtEmployee_ID.Text.ToString().Replace("'", "''"));
        if (bHave)
        {
            this.Response.Write("<script language='JavaScript'>window.alert('该用户名已经存在，请重新输入！'); </script>");
        }
        else
        {
            //更新数据库表
            radlGender.Text = radlGender.SelectedItem.Value;
            string sqlStr = "";
            string strXMMCS = "";
            if (cbAll.Checked)
            {
                strXMMCS = "全部;";
            }
            if (cbZJS.Checked)
            {
                strXMMCS = strXMMCS + "紫金山;";
            }
            if (cbSJB.Checked)
            {
                strXMMCS = strXMMCS + "三交北;";
            }
            if (cbSJ.Checked)
            {
                strXMMCS = strXMMCS + "三交;";
            }
            if (cbSLB.Checked)
            {
                strXMMCS = strXMMCS + "石楼北;";
            }
            if (cbSLN.Checked)
            {
                strXMMCS = strXMMCS + "石楼南;";
            }
            if (cbHC.Checked)
            {
                strXMMCS = strXMMCS + "韩城;";
            }
            if (cbQS.Checked)
            {
                strXMMCS = strXMMCS + "保田青山;";
            }
            if (cbLHG.Checked)
            {
                strXMMCS = strXMMCS + "硫磺沟;";
            }
            if (cbDN.Checked)
            {
                strXMMCS = strXMMCS + "大宁;";
            }
            if (cbYL.Checked)
            {
                strXMMCS = strXMMCS + "永利;";
            }
            if (cbSLX.Checked)
            {
                strXMMCS = strXMMCS + "石楼西;";
            }
            if (cbGRAL1.Checked)
            {
                strXMMCS = strXMMCS + "格日敖勒1;";
            }
            if (cbGRAL2.Checked)
            {
                strXMMCS = strXMMCS + "格日敖勒2;";
            }
            if (strXMMCS == "")
            {
                //common.alertMessage("没有选择管辖的项目");
                //return;
            }
            else
            {
                strXMMCS = strXMMCS.Substring(0, strXMMCS.Length - 1);
            }
            if (txtBirthday.Text == "")
            {
                sqlStr = "update T_System_EMPLOYEE set Employee_ID = '" + txtEmployee_ID.Text.Replace("'", "''") + "' , Employee_Name= '" + txtEmployee_Name.Text.Replace("'", "''") + "', Tel= '" + txtTel.Text.Replace("'", "''") + "', Gender= '" + radlGender.SelectedItem.Value + "',Branch_ID = '" + dropBranch_ID.SelectedValue.Replace("'", "''") + "',XMMCS='" + strXMMCS + "',MobilTel = '" + txtMobilTel.Text.Replace("'", "''") + "', Func_ID= '" + dropFunc_ID.SelectedValue.Replace("'", "''") + "',  Birthday=null , JTel = '" + txtJTel.Text.Replace("'", "''") + "' ,Email = '" + txtEmail.Text.Replace("'", "''") + "',state='" + radlstate.SelectedItem.Value + "', HSE1='" + tbHSE1.Text + "', HSE2='" + tbHSE2.Text + "', LHQ1='" + tbLHQ1.Text + "', LHQ2='" + tbLHQ2.Text + "', JK1='" + tbJK1.Text + "', JK2='" + tbJK2.Text + "', JD1='" + tbJD1.Text + "', JD2='" + tbJD2.Text + "'  where ID = '" + Request.QueryString["ID"] + "'";

            }
            else if (Convert.ToDateTime(txtBirthday.Text) > Convert.ToDateTime(Datetoday.Date.ToString("yyyy-MM-dd")))
            {
                string stralert = "日期越界请重新输入！";
                common.alertMessage(stralert);
            }
            else
            {
                sqlStr = "update T_System_EMPLOYEE set Employee_ID = '" + txtEmployee_ID.Text.Replace("'", "''") + "' , Employee_Name= '" + txtEmployee_Name.Text.Replace("'", "''") + "', Tel= '" + txtTel.Text.Replace("'", "''") + "', Gender= '" + radlGender.SelectedItem.Value + "',Branch_ID = '" + dropBranch_ID.SelectedValue.Replace("'", "''") + "',XMMCS='" + strXMMCS + "',MobilTel = '" + txtMobilTel.Text.Replace("'", "''") + "', Func_ID= '" + dropFunc_ID.SelectedValue.Replace("'", "''") + "',  Birthday= '" + txtBirthday.Text + "',JTel = '" + txtJTel.Text.Replace("'", "''") + "' ,Email = '" + txtEmail.Text.Replace("'", "''") + "',state='" + radlstate.SelectedItem.Value + "', HSE1='" + tbHSE1.Text + "', HSE2='" + tbHSE2.Text + "', LHQ1='" + tbLHQ1.Text + "', LHQ2='" + tbLHQ2.Text + "', JK1='" + tbJK1.Text + "', JK2='" + tbJK2.Text + "', JD1='" + tbJD1.Text + "', JD2='" + tbJD2.Text + "'  where ID = '" + Request.QueryString["ID"] + "'";
            }
            ArrayList strSQL = new ArrayList();
            strSQL.Add(sqlStr);
            if (szOldEmployee_ID != txtEmployee_ID.Text)
            {
                sqlStr = "update T_System_USER set empid = '" + txtEmployee_ID.Text.Replace("'", "''") + "' where empid='" + szOldEmployee_ID.Replace("'", "''") + "'";
                strSQL.Add(sqlStr);
            }

            if (strSQL.Count > 0)
            {
                common.UpdateSQL(strSQL, returnurl + "?PageIndex=" + iPageIndex + "&Type=update&QueryName=" + QueryName.Replace("'", "<`>"), 1);
            }

        }
    }

    protected bool VerifyExist(string strCurName, string strNewName)
    {
        bool bHave = false;
        SqlDataReader dtr;
        SqlConnection conMy = DBCONN.GetDBConn();
        string szQuery;
        szQuery = "select * from T_System_EMPLOYEE where Employee_ID='" + strNewName + "' and Employee_ID<>'" + strCurName + "'";
        SqlCommand cmdMy = new SqlCommand(szQuery, conMy);
        cmdMy.Connection.Open();
        dtr = cmdMy.ExecuteReader();
        if (dtr.Read())
        {
            bHave = true;
        }
        cmdMy.Dispose();
        conMy.Close();
        return bHave;

    }
    protected void btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(returnurl + "?PageIndex=" + iPageIndex + "&Type=update&QueryName=" + QueryName);
    }
    protected void cbAll_CheckedChanged(object sender, EventArgs e)
    {
        if (cbAll.Checked)
        {
            cbAll.Checked = true;
            cbZJS.Checked = true;
            cbSJB.Checked = true;
            cbSJ.Checked = true;
            cbSLB.Checked = true;
            cbSLN.Checked = true;
            cbHC.Checked = true;
            cbQS.Checked = true;
            cbLHG.Checked = true;
            cbYL.Checked = true;
            cbSLX.Checked = true;
            cbGRAL1.Checked = true;
            cbGRAL2.Checked = true;
        }
        else
        {
            cbAll.Checked = false;
            cbZJS.Checked = false;
            cbSJB.Checked = false;
            cbSJ.Checked = false;
            cbSLB.Checked = false;
            cbSLN.Checked = false;
            cbHC.Checked = false;
            cbQS.Checked = false;
            cbLHG.Checked = false;
            cbYL.Checked = false;
            cbSLX.Checked = false;
            cbGRAL1.Checked = false;
            cbGRAL2.Checked = false;
        }
    }
}
