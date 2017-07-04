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

namespace NCCBM.data.Report.zgys
{
    public partial class zgys_edit : System.Web.UI.Page
    {
        private string tablename = "Report_zugongyinsu";
        private String url = "zgys_list.aspx?Type=list";
        private string id = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["ID"];
            if (!Page.IsPostBack)
            {
                setTextBox();
            }
        }
        void setTextBox()
        {
            SqlDataReader dr;
            SqlConnection Myconn = DBCONN.GetDBConn();
            SqlCommand Mycomm = new SqlCommand("select * from " + tablename + " where id = " + id, Myconn);
            Mycomm.Connection.Open();
            dr = Mycomm.ExecuteReader();
            dr.Read();
            if (dr["lururiqi"] == null || dr["lururiqi"].ToString().Trim() == "") TB_lururiqi.Text = "";
            else TB_lururiqi.Text = ((DateTime)dr["lururiqi"]).ToString("yyyy-MM-dd");
            TB_qukuai.Text = dr["qukuai"].ToString();
            TB_taoguan.Text = dr["taoguanzhiliangwenti"].ToString();



            TB_xiayu.Text = dr["xiayu"].ToString();
            TB_gongnong.Text = dr["gongnongguanxi"].ToString();

            if (dr["cheliangweixiu"] == null || dr["cheliangweixiu"].ToString().Trim() == "") TB_cheliang.Text = "";
            else TB_cheliang.Text = (dr["cheliangweixiu"]).ToString();

            if (dr["jingchangbanqian"] == null || dr["jingchangbanqian"].ToString().Trim() == "") TB_jingchang.Text = "";
            else TB_jingchang.Text = (dr["jingchangbanqian"]).ToString();

            TB_dengdaijingtai.Text = dr["dengdaijingtaibanqian"].ToString();
            TB_beishui.Text = dr["beishuipeiye"].ToString();
            TB_beizhu.Text = dr["beizhu"].ToString();

            dr.Close();
            Mycomm.Connection.Close();
        }

        protected void addnew_Click(object sender, EventArgs e)
        {
            String rq = TB_lururiqi.Text.Trim();
            String qk = TB_qukuai.Text.Trim();
            if (rq == "" || qk == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('日期和区块名称不能为空！'); </script>");
                return;
            }

            string sqlStr = "UPDATE " + tablename + " set ";

            if (TB_lururiqi.Text == "") sqlStr = sqlStr + "lururiqi=NULL,";
            else sqlStr = sqlStr + "lururiqi='" + TB_lururiqi.Text + "',";

            sqlStr = sqlStr + "taoguanzhiliangwenti='" + TB_taoguan.Text + "',";

            if (TB_cheliang.Text == "") sqlStr = sqlStr + "cheliangweixiu=NULL,";
            else sqlStr = sqlStr + "cheliangweixiu='" + TB_cheliang.Text + "',";

            if (TB_jingchang.Text == "") sqlStr = sqlStr + "jingchangbanqian=NULL,";
            else sqlStr = sqlStr + "jingchangbanqian='" + TB_jingchang.Text + "',";

            sqlStr = sqlStr + "qukuai='" + TB_qukuai.Text + "',";
            sqlStr = sqlStr + "xiayu='" + TB_xiayu.Text + "',";
            sqlStr = sqlStr + "gongnongguanxi='" + TB_gongnong.Text + "',";
            sqlStr = sqlStr + "dengdaijingtaibanqian='" + TB_dengdaijingtai.Text + "',";
            sqlStr = sqlStr + "beishuipeiye='" + TB_beishui.Text + "',";
            sqlStr = sqlStr + "beizhu='" + TB_beizhu.Text + "'";

            sqlStr = sqlStr + " where id = " + id;
            try
            {
                DataBaseHelper.execute(sqlStr);
                Response.Redirect(url);
            }
            catch (Exception e2)
            {
                return;
            }
        }

        protected void btnreturn_Click1(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}