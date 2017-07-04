using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace NCCBM.data.jichuxinxi
{
    public partial class jichuxinxi_add : System.Web.UI.Page
    {
        private String tablename = "jing_jichuxinxi";
        private String url = "jichuxinxi_list.aspx";
        private string username = "temp";

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

            if (HttpContext.Current.Request.Cookies["UserName"] != null)
            {
                username = HttpContext.Current.Request.Cookies["UserName"].Value;
            }
            else
            {
                if (HttpContext.Current.Session["LoginUserID"] != null)
                {
                    username = HttpContext.Current.Session["LoginUserID"].ToString();
                }
            }

            if (userPlaceId == 1)
            {
                tbQukuai.Text = "韩城";
                tbQukuai.Enabled = false;
            }
            if (userPlaceId == 2)
            {
                tbQukuai.Text = "临汾";
                tbQukuai.Enabled = false;
            }
            if (userPlaceId == 3)
            {
                tbQukuai.Text = "忻州";
                tbQukuai.Enabled = false;
            }
        }



        protected void btnOk_Click(object sender, EventArgs e)
        {
            String jh = tbJinghao.Text.Trim();
            String qk = tbQukuai.Text.Trim();
            if (jh == "" || qk == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('井号和区块名称不能为空！'); </script>");
                return;
            }

            bool bHave = VerifyExist(tbJinghao.Text.Trim(), tbQukuai.Text.Trim());
            if (bHave)
            {
                this.Response.Write("<script language='JavaScript'>window.alert('请注意：该项目的基本情况已经存在，请重新选择！'); </script>");
            }
            else
            {
                float fTemp = 0;
                string sqlStr = "INSERT INTO " + tablename + "(jinghao,x,y,h," +
                    "jingbie,jingxing,shejijingshen," +
                    "shigongdanwei,kaizuanriqi,dangqianzhuangtai," +
                    "qukuai,shengchanriqi,beizhu) Values(";

                sqlStr = sqlStr + "'" + tbJinghao.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + tbX.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + tbY.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + tbH.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + tbJingbie.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + tbJingxing.Text.Trim() + "',";

                if (tbShejijingshen.Text == "")
                {
                    sqlStr = sqlStr + "NULL,";
                }
                else
                {
                    try
                    {
                        fTemp = float.Parse(tbShejijingshen.Text.Trim());
                        sqlStr = sqlStr + fTemp.ToString() + ",";
                    }
                    catch (System.FormatException fe)
                    {
                        sqlStr = sqlStr + "NULL,";
                    }
                }

                sqlStr = sqlStr + "'" + tbShigongdanwei.Text.Trim() + "',";
               
                if (tbKaizuanriqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + tbKaizuanriqi.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + tbDangqianzhuangtai.Text.Trim() + "',";
                sqlStr = sqlStr + "'" + tbQukuai.Text.Trim() + "',";

                if (tbGengxinriqi.Text == "") sqlStr = sqlStr + "NULL,";
                else sqlStr = sqlStr + "'" + tbGengxinriqi.Text.Trim() + "',";

                sqlStr = sqlStr + "'" + tbBeizhu.Text.Trim() + "')";

                try
                {
                    DataBaseHelper.execute(sqlStr);
                }
                catch (Exception e2)
                {
                    this.Response.Write("<script language='JavaScript'>window.alert('错误：无法导入数据库，请检查数据是否有效！'); </script>");
                    return;
                }
                Response.Redirect(url);
            }
        }

        protected bool VerifyExist(string name, string qukuai)
        {
            bool bHave = false;
            SqlDataReader dtr;
            SqlConnection conMy = DBCONN.GetDBConn();
            string szQuery;
            szQuery = "select * from " + tablename + " where jinghao='" + name + "' and qukuai='" + qukuai + "'";
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

        protected void btnBack_Click1(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }



        void lbData_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            if (fuData.HasFile)
            {
                if (fuData.PostedFile.ContentLength < 4096 * 1024)
                {
                    try
                    {
                        fuData.PostedFile.SaveAs(Server.MapPath("../../upload/data/" + username + "/") + fuData.FileName);
                    }
                    catch (Exception ex)
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "文件上传出错：" + ex.Message;
                        return;
                    }
                }
                else
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "上传文件不能大于4MB!";
                    return;
                }
            }
            else
            {
                Label1.Text = "没有选择文件！";
                return;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string uploadPath = Server.MapPath("../../temp/" + username + "/") + tbDataFile.Text;
            string connExcel = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                             "Data Source=" + uploadPath +
                             ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1'";
            OleDbConnection oleDBConn = new OleDbConnection(connExcel);
            try
            {
                oleDBConn.Open();
            }
            catch (System.Data.OleDb.OleDbException oe)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "读取Excel文件出错：" + oe.Message;
                return;
            }

            DataTable dt = new DataTable();
            string sql = "SELECT * FROM [Sheet1$]";
            OleDbDataAdapter oleAdMaster = new OleDbDataAdapter(sql, oleDBConn);
            try
            {
                oleAdMaster.Fill(dt);
            }
            catch (OleDbException ex)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "读取Excel文件出错：" + ex.Message;
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();
                return;
            }
            oleAdMaster.Dispose();
            oleDBConn.Close();
            oleDBConn.Dispose();

            if (dt.Rows.Count > 0 && dt.Columns.Count >= 13)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = "insert into jing_jichuxinxi values('" 
                        + dt.Rows[i][0] + "', '" 
                        + dt.Rows[i][1] + "', '" 
                        + dt.Rows[i][2] + "', '" 
                        + dt.Rows[i][3] + "', '" 
                        + dt.Rows[i][4] + "', '" 
                        + dt.Rows[i][5] + "', '" 
                        + dt.Rows[i][6] + "', '" 
                        + dt.Rows[i][7] + "', '" 
                        + dt.Rows[i][8] + "', '" 
                        + dt.Rows[i][9] + "', '" 
                        + dt.Rows[i][10] + "', '" 
                        + dt.Rows[i][11] + "', '" 
                        + dt.Rows[i][12] + "')";
                    DataBaseHelper.execute(sql);
                }
            }
        }
    }
}