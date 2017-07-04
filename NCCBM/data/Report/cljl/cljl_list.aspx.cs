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
using MR.Cdecl;

namespace NCCBM.data.Report.cljl
{
    public partial class cljl_list : System.Web.UI.Page
    {
        private string szType;
        private string tablename = "Report_jinglou";
        private DataTable dt = null;
        private int nPageSize = common.GetPageSize();
        private int nCols = 11;
        private string strQk = "";
        private string strOrderBy = " order by lururiqi desc";
        private int userPlaceId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    userPlaceId = Int32.Parse(HttpContext.Current.Session["PlaceID"].ToString());
                }
                catch (NullReferenceException ne)
                {
                    HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
                }

                if (userPlaceId == 1)
                {
                    strQk = "and qukuai='韩城'";
                }
                if (userPlaceId == 2)
                {
                    strQk = "and qukuai='临汾'";
                }
                if (userPlaceId == 3)
                {
                    strQk = "and qukuai='忻州'";
                }

                GridView2.Attributes.Add("BorderColor", "Black");
                GridView2.Attributes.Add("BorderWidth", "1");

                dt = DataBaseHelper.query("select * from " + tablename + " where 1=1 " + strQk + strOrderBy);
                GridView2.DataSource = dt;

                szType = Request.QueryString["Type"];
                if (szType == "NULL" || szType == "" || szType == null)
                {
                    szType = "list";
                }
                GridView2.ShowFooter = false;
                GridView2.PageSize = nPageSize;
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
                dt = DataBaseHelper.query("select * from " + tablename + " where 1=1 " + strQk + strOrderBy);
                GridView2.DataSource = dt;
            }

            GridView2.DataBind();

            for (int i = 1; i <= GridView2.Rows.Count; i++)
            {
                int n = GridView2.PageIndex * nPageSize + i - 1;
                GridView2.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
            }

            szType = Request.QueryString["Type"];
            if (szType == "NULL" || szType == "" || szType == null)
            {
                szType = "list";
            }
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
            int index = gvrow.RowIndex + nPageSize * GridView2.PageIndex;
            string id = dt.Rows[index]["id"].ToString();
            if (e.CommandName == "xiugai")
            {
                Response.Redirect("cljl_edit.aspx?ID=" + id);
            }
            if (e.CommandName == "tianjia")
            {
                Response.Redirect("cljl_add.aspx?ID=" + id);
            }
            if (e.CommandName == "shanchu")
            {
                string sql = "delete from " + tablename + " where id = " + id;

                try
                {
                    DataBaseHelper.execute(sql);
                    dt = DataBaseHelper.query("select * from " + tablename + " where 1=1 " + strQk + strOrderBy);
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    for (int i = 1; i <= GridView2.Rows.Count; i++)
                    {
                        int n = GridView2.PageIndex * nPageSize + i - 1;
                        GridView2.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
                    }
                }
                catch (Exception e2)
                {

                }
            }
        }

        protected void Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            for (int i = 1; i <= GridView2.Rows.Count; i++)
            {
                int n = GridView2.PageIndex * nPageSize + i - 1;
                GridView2.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
            }
        }
    }
}