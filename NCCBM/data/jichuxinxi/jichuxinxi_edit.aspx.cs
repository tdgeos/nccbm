using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NCCBM.data.jichuxinxi
{
    public partial class jichuxinxi_edit : System.Web.UI.Page
    {
        private string tablename = "jing_jichuxinxi";
        private string url = "jichuxinxi_list.aspx?Type=list";
        private string id = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string szRoleID = null;
            try
            {
                szRoleID = HttpContext.Current.Session["RoleID"].ToString();
            }
            catch (NullReferenceException ne)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            string szText = Request.QueryString["iPage"];
            if (szText != null && szText != "")
            {
                url += "&iPage=" + szText;
            }
            szText = Request.QueryString["Qukuai"];
            if (szText != null && szText != "")
            {
                url += "&Qukuai=" + szText;
            }
            szText = Request.QueryString["Zhuangtai"];
            if (szText != null && szText != "")
            {
                url += "&Zhuangtai=" + szText;
            }
            szText = Request.QueryString["Jinghao"];
            if (szText != null && szText != "")
            {
                url += "&Jinghao=" + szText;
            }

            id = Request.QueryString["ID"];
            if (!Page.IsPostBack)
            {
                setTextBox();
            }
        }

        private void setTextBox()
        {
            SqlDataReader dr;
            SqlConnection Myconn = DBCONN.GetDBConn();
            SqlCommand Mycomm = new SqlCommand("select * from " + tablename + " where id = " + id, Myconn);
            Mycomm.Connection.Open();
            dr = Mycomm.ExecuteReader();
            dr.Read();

            tbJinghao.Text = dr["jinghao"].ToString();
            tbX.Text = dr["x"].ToString();
            tbY.Text = dr["y"].ToString();
            tbH.Text = dr["h"].ToString();
            tbJingbie.Text = dr["jingbie"].ToString();
            tbJingxing.Text = dr["jingxing"].ToString();
            tbShejijingshen.Text = dr["shejijingshen"].ToString();
            tbShigongdanwei.Text = dr["shigongdanwei"].ToString();

            if (dr["kaizuanriqi"] == null || dr["kaizuanriqi"].ToString().Trim() == "") tbKaizuanriqi.Text = "";
            else tbKaizuanriqi.Text = ((DateTime)dr["kaizuanriqi"]).ToString("yyyy-MM-dd");

            tbDangqianzhuangtai.Text = dr["dangqianzhuangtai"].ToString();
            tbQukuai.Text = dr["qukuai"].ToString();

            if (dr["shengchanriqi"] == null || dr["shengchanriqi"].ToString().Trim() == "") tbGengxinriqi.Text = "";
            else tbGengxinriqi.Text = ((DateTime)dr["shengchanriqi"]).ToString("yyyy-MM-dd");

            tbBeizhu.Text = dr["beizhu"].ToString();

            dr.Close();
            Mycomm.Connection.Close();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }

        protected void btnOk_Click1(object sender, EventArgs e)
        {
            String jh = tbJinghao.Text.Trim();
            String qk = tbQukuai.Text.Trim();
            if (jh == "" || qk == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('井号和区块名称不能为空！'); </script>");
                return;
            }

            string sqlStr = "update " + tablename + " set ";

            sqlStr = sqlStr + "jinghao='" + tbJinghao.Text.Trim() + "',";
            sqlStr = sqlStr + "x='" + tbX.Text.Trim() + "',";
            sqlStr = sqlStr + "y='" + tbY.Text.Trim() + "',";
            sqlStr = sqlStr + "h='" + tbH.Text.Trim() + "',";
            sqlStr = sqlStr + "jingbie='" + tbJingbie.Text.Trim() + "',";
            sqlStr = sqlStr + "jingxing='" + tbJingxing.Text.Trim() + "',";

            float fTemp = 0;
            if (tbShejijingshen.Text == "")
            {
                sqlStr = sqlStr + "shejijingshen=NULL,";
            }
            else
            {
                try
                {
                    fTemp = float.Parse(tbShejijingshen.Text.Trim());
                    sqlStr = sqlStr + "shejijingshen=" + fTemp.ToString() + ",";
                }
                catch (System.FormatException fe)
                {
                    sqlStr = sqlStr + "shejijingshen=NULL,";
                }
            }

            sqlStr = sqlStr + "shigongdanwei='" + tbShigongdanwei.Text.Trim() + "',";

            if (tbKaizuanriqi.Text == "") sqlStr = sqlStr + "kaizuanriqi=NULL,";
            else sqlStr = sqlStr + "kaizuanriqi='" + tbKaizuanriqi.Text.Trim() + "',";

            sqlStr = sqlStr + "dangqianzhuangtai='" + tbDangqianzhuangtai.Text.Trim() + "',";
            sqlStr = sqlStr + "qukuai='" + tbQukuai.Text.Trim() + "',";

            if (tbGengxinriqi.Text == "") sqlStr = sqlStr + "shengchanriqi=NULL,";
            else sqlStr = sqlStr + "shengchanriqi='" + tbGengxinriqi.Text.Trim() + "',";

            sqlStr = sqlStr + "beizhu='" + tbBeizhu.Text.Trim() + "' ";

            sqlStr = sqlStr + " where id = " + id;

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
}