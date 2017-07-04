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

namespace NCCBM.data.Report.jsyj
{
    public partial class jsyj_edit : System.Web.UI.Page
    {
        private string tablename = "jianbaoluru";
        private String url = "jsyj_list.aspx?Type=list";
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
            if (dr["lururiqi"] == null || dr["lururiqi"].ToString().Trim() == "") txtRiqi.Text = "";
            else txtRiqi.Text = ((DateTime)dr["lururiqi"]).ToString("yyyy-MM-dd");
            txt_report_hc.Text = dr["hancheng"].ToString();
            txt_report_lf.Text = dr["linfen"].ToString();
            txt_report_xz.Text = dr["xinzhou"].ToString();
            dr.Close();
            Mycomm.Connection.Close();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            String rq = txtRiqi.Text.Trim();
            if (rq == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('日期不能为空！'); </script>");
                return;
            }

            string sqlStr = "UPDATE " + tablename + " set ";
            if (txtRiqi.Text == "") sqlStr = sqlStr + "lururiqi=NULL,";
            else sqlStr = sqlStr + "lururiqi='" + txtRiqi.Text + "',";
            sqlStr = sqlStr + "hancheng='" + txt_report_hc.Text + "',";
            sqlStr = sqlStr + "linfen='" + txt_report_lf.Text + "',";
            sqlStr = sqlStr + "xinzhou='" + txt_report_xz.Text + "'";
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

        protected void btnBack_Click1(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}