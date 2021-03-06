﻿using System;
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

namespace NCCBM.data.Report.ykys
{
    public partial class ykys_edit : System.Web.UI.Page
    {
        private string tablename = "Report_yikaiyanshou";
        private String url = "ykys_list.aspx?Type=list";
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
            TB_jinghao.Text = dr["jinghao"].ToString();

            

            TB_shigongduiwu.Text = dr["shigongduiwu"].ToString();
            TB_jiandu.Text = dr["jiandu"].ToString();

            if (dr["yikaishijian"] == null || dr["yikaishijian"].ToString().Trim() == "") TB_yikaishijian.Text = "";
            else TB_yikaishijian.Text = ((DateTime)dr["yikaishijian"]).ToString("yyyy-MM-dd");

            if (dr["erkaishijian"] == null || dr["erkaishijian"].ToString().Trim() == "") TB_erkaishijian.Text = "";
            else TB_erkaishijian.Text = ((DateTime)dr["erkaishijian"]).ToString("yyyy-MM-dd");

            TB_yinsu.Text = dr["yinsu"].ToString();
            TB_cuoshi.Text = dr["cuoshi"].ToString();
            TB_beizhu.Text = dr["beizhu"].ToString();

            dr.Close();
            Mycomm.Connection.Close();
        }

        protected void addnew_Click(object sender, EventArgs e)
        {
            String rq = TB_lururiqi.Text.Trim();
            String jh = TB_jinghao.Text.Trim();
            String qk = TB_qukuai.Text.Trim();
            if (rq == "" || jh == "" || qk == "")
            {
                this.Response.Write("<script language='JavaScript'>window.alert('井号、日期和区块名称不能为空！'); </script>");
                return;
            }

            string sqlStr = "UPDATE " + tablename + " set ";

            if (TB_lururiqi.Text == "") sqlStr = sqlStr + "lururiqi=NULL,";
            else sqlStr = sqlStr + "lururiqi='" + TB_lururiqi.Text + "',";

            sqlStr = sqlStr + "jinghao='" + TB_jinghao.Text + "',";

            if (TB_yikaishijian.Text == "") sqlStr = sqlStr + "yikaishijian=NULL,";
            else sqlStr = sqlStr + "yikaishijian='" + TB_yikaishijian.Text + "',";

            if (TB_erkaishijian.Text == "") sqlStr = sqlStr + "erkaishijian=NULL,";
            else sqlStr = sqlStr + "erkaishijian='" + TB_erkaishijian.Text + "',";

            sqlStr = sqlStr + "qukuai='" + TB_qukuai.Text + "',";
            sqlStr = sqlStr + "shigongduiwu='" + TB_shigongduiwu.Text + "',";
            sqlStr = sqlStr + "jiandu='" + TB_jiandu.Text + "',";
            sqlStr = sqlStr + "yinsu='" + TB_yinsu.Text + "',";
            sqlStr = sqlStr + "cuoshi='" + TB_cuoshi.Text + "',";
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