using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.system.qukuai
{
    public partial class qukuai_list : System.Web.UI.Page
    {
        private string szType = null;
        private string tablename = "T_System_Qukuai";
        private DataTable dt = null;
        private int nPageSize = common.GetPageSize();
        private int nCols = 4;

        private string userName = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userName = HttpContext.Current.Session["LoginUserID"].ToString();
            }
            catch (NullReferenceException ne)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            szType = Request.QueryString["Type"];
            if (szType == "NULL" || szType == "" || szType == null)
            {
                szType = "list";
            }

            string szPage = Request.QueryString["iPage"];
            if (szPage != null && szPage != "")
            {
                tbPageIndex.Text = szPage;
            }

            string szName = Request.QueryString["strName"];
            if (szName != null && szName != "")
            {
                tbName.Text = szName;
            }

            if (!Page.IsPostBack)
            {
                GridView2.ShowFooter = false;
                GridView2.PageSize = nPageSize;
                GridView2.Attributes.Add("BorderColor", "#0088cc");
                GridView2.Attributes.Add("BorderWidth", "1");
                if (tbPageIndex.Text != "") GridView2.PageIndex = Int32.Parse(tbPageIndex.Text);

                String strSQL = "select * from " + tablename + " where 1=1 ";
                if (tbName.Text.Trim() != "")
                {
                    strSQL += "and mingcheng like '%" + tbName.Text.Trim() + "%'";
                }

                dt = DataBaseHelper.query(strSQL);
                GridView2.DataSource = dt;
                GridView2.DataBind();
                if (dt.Rows.Count <= 0)
                {
                    //
                }
                else
                {
                    //
                }

                switch (szType)
                {
                    case "update":
                        GridView2.Columns[nCols].Visible = true;
                        break;
                    case "add":
                        GridView2.ShowFooter = true;
                        break;
                    case "delete":
                        GridView2.Columns[nCols + 1].Visible = true;
                        break;
                    case "list":
                        GridView2.Columns[nCols].Visible = false;
                        GridView2.Columns[nCols + 1].Visible = false;
                        break;
                }
            }
            else
            {
                if (tbPageIndex.Text != "") GridView2.PageIndex = Int32.Parse(tbPageIndex.Text);
                String strSQL = "select * from " + tablename + " where 1=1 ";
                if (tbName.Text.Trim() != "")
                {
                    strSQL += "and mingcheng like '%" + tbName.Text.Trim() + "%'";
                }
                dt = DataBaseHelper.query(strSQL);
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
        }

        protected void Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            tbPageIndex.Text = e.NewPageIndex.ToString();
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvrow = null;
            try
            {
                gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            }
            catch (System.InvalidCastException ice)
            {
                return;
            }
            int iPage = 0;
            if (tbPageIndex.Text != "")
            {
                iPage = Int32.Parse(tbPageIndex.Text);
            }

            int index = gvrow.RowIndex + nPageSize * iPage;
            string id = dt.Rows[index]["id"].ToString();
            if (e.CommandName == "xiugai")
            {
                Response.Redirect("qukuai_edit.aspx?ID=" + id + "&iPage=" + iPage + "&strName=" + tbName.Text);
            }
            if (e.CommandName == "shanchu")
            {
                string sql = "delete from " + tablename + " where id = " + id;
                try
                {
                    DataBaseHelper.execute(sql);
                    string strSQL = "select * from " + tablename + " where 1=1 ";
                    dt = DataBaseHelper.query(strSQL);
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
                catch (Exception e2)
                {

                }
            }
        }

        protected void cmdQuery_Click(object sender, EventArgs e)
        {
            String strSQL = "select * from " + tablename + " where 1=1 ";
            string strName = this.tbName.Text.Trim();
            strSQL += "and mingcheng like '%" + strName + "%'";
            dt = DataBaseHelper.query(strSQL);
            GridView2.DataSource = dt;
            GridView2.DataBind();

            if (dt.Rows.Count <= 0)
            {
                //
            }
            else
            {
                //
            }
        }
    }
}