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

namespace NCCBM.system.employee
{
    public partial class system_employee_EMPLOYEE_add : System.Web.UI.Page
    {
        private String tablename = "T_System_EMPLOYEE";
        private String url = "employee_list.aspx?Type=list";

        protected void Page_Load(object sender, EventArgs e)
        {
            int userPlaceId = 0;
            try
            {
                userPlaceId = Int32.Parse(HttpContext.Current.Session["PlaceID"].ToString());
            }
            catch (NullReferenceException ne)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            if (!Page.IsPostBack)
            {
                setbranchData();
                setFunData();
            }
        }
        //部门下拉框
        void setbranchData()
        {
            //初始化页面文本框
            SqlDataReader dr;
            SqlConnection Myconn = DBCONN.GetDBConn();
            SqlCommand Mycomm = new SqlCommand("SELECT DISTINCT  Branch_ID,Replace(Branch_Name,'<#>','`')as Branch_Name, State FROM T_System_BRANCH WHERE (State = 0) ", Myconn);
            Mycomm.Connection.Open();
            dr = Mycomm.ExecuteReader();
            if (dr.Read())
            {
                DataSet ds = new DataSet();
                SqlConnection MyConnDrop = DBCONN.GetDBConn();
                SqlCommand MycommDrop = new SqlCommand("SELECT DISTINCT  Branch_ID,Replace(Branch_Name,'<#>','`')as Branch_Name, State FROM T_System_BRANCH WHERE (State = 0)", MyConnDrop);
                MycommDrop.Connection.Open();
                SqlDataAdapter ad = new SqlDataAdapter("SELECT DISTINCT  Branch_ID,Replace(Branch_Name,'<#>','`')as Branch_Name, State FROM T_System_BRANCH WHERE (State = 0)", MyConnDrop);
                ad.Fill(ds, "T_System_BRANCH");
                dropBranch_ID.DataSource = ds;
                dropBranch_ID.DataMember = "T_System_BRANCH";
                dropBranch_ID.DataTextField = "Branch_Name";
                dropBranch_ID.DataValueField = "Branch_ID";
                dropBranch_ID.DataBind();
                MycommDrop.Connection.Close();
            }
            else
            {
                string stralert = " 请先定义部门";
                common.alertMessage(stralert);
            }
            dr.Close();
            Mycomm.Connection.Close();
        }
        //职务下拉框
        void setFunData()
        {
            //初始化页面文本框
            SqlDataReader dr;
            SqlConnection Myconn = DBCONN.GetDBConn();
            SqlCommand Mycomm = new SqlCommand("SELECT DISTINCT Func_ID, Replace(Func_Name,'<#>','`')as Func_Name, State FROM T_SYSTEM_FUNC WHERE (State = 0)", Myconn);
            Mycomm.Connection.Open();
            dr = Mycomm.ExecuteReader();
            if (dr.Read())
            {
                DataSet ds = new DataSet();
                SqlConnection MyConnDrop = DBCONN.GetDBConn();
                SqlCommand MycommDrop = new SqlCommand("SELECT DISTINCT Func_ID, Replace(Func_Name,'<#>','`')as Func_Name, State FROM T_SYSTEM_FUNC WHERE (State = 0)", MyConnDrop);
                MycommDrop.Connection.Open();
                SqlDataAdapter ad = new SqlDataAdapter("SELECT DISTINCT Func_ID, Replace(Func_Name,'<#>','`')as Func_Name, State FROM T_SYSTEM_FUNC WHERE (State = 0)", MyConnDrop);
                ad.Fill(ds, "T_SYSTEM_FUNC");
                dropFunc_ID.DataSource = ds;
                dropFunc_ID.DataMember = "T_SYSTEM_FUNC";
                dropFunc_ID.DataTextField = "Func_Name";
                dropFunc_ID.DataValueField = "Func_ID";
                dropFunc_ID.DataBind();
                MycommDrop.Connection.Close();
            }
            else
            {
                string stralert = " 请先定义职务";
                common.alertMessage(stralert);
            }
            dr.Close();
            Mycomm.Connection.Close();

        }
        protected void addnew_Click(object sender, EventArgs e)
        {

            //判断用户名称是否相同
            bool bHave;
            bHave = VerifyUser(txtEmployee_ID.Text.ToString().Replace("'", "''"));

            if (bHave)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('该用户名已经存在，请重新输入！'); </script>");
                return;
            }

            string sqlStr = "INSERT INTO T_System_EMPLOYEE(Employee_ID, Employee_Name,Gender,Birthday,Branch_ID,Func_ID,HSE1,HSE2,LHQ1,LHQ2,JK1,JK2,JD1,JD2,Tel,MobilTel,JTel,Email,State) Values('"
                    + txtEmployee_ID.Text.Replace("'", "''") + "' , '"
                    + txtEmployee_Name.Text.Replace("'", "''") + "','"
                    + radlGender.SelectedItem.Value + "' ,'"
                    + txtBirthday.Text.Replace("'", "''") + "' ,'"
                    + dropBranch_ID.SelectedValue + "' ,'"
                    + dropFunc_ID.SelectedValue + "' ,'"
                    + tbHSE1.Text.Replace("'", "''") + "','"
                    + tbHSE2.Text.Replace("'", "''") + "','"
                    + tbLHQ1.Text.Replace("'", "''") + "','"
                    + tbLHQ2.Text.Replace("'", "''") + "','"
                    + tbJK1.Text.Replace("'", "''") + "','"
                    + tbJK2.Text.Replace("'", "''") + "','"
                    + tbJD1.Text.Replace("'", "''") + "','"
                    + tbJD2.Text.Replace("'", "''") + "','"
                    + txtTel.Text.Replace("'", "''") + "' ,'"
                    + txtMobilTel.Text.Replace("'", "''") + "' ,'"
                    + txtJTel.Text.Replace("'", "''") + "' ,'"
                    + txtEmail.Text.Replace("'", "''") + "','0')";
            try
            {
                DataBaseHelper.execute(sqlStr);
                Response.Redirect(url);
            }
            catch (Exception e2)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('" + e2.Message + "'); </script>");
                return;
            }
        }

        protected bool VerifyUser(string strNewUser)
        {
            bool bHave = false;
            SqlDataReader dtr;
            SqlConnection conMy = DBCONN.GetDBConn();
            string szQuery;
            szQuery = "select * from T_System_EMPLOYEE where  Employee_ID='" + strNewUser + "'";
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
            Response.Redirect(url);
        }
    }
}
