using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Net;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;


namespace NCCBM.Import
{
    public partial class ZJ_Import : System.Web.UI.Page
    {
        private string[] dbTables = new string[5] { "Xls_Zj_Rbb_Zj", "Xls_Zj_Rbb_Xtg", "Xls_Zj_Rbb_Gj", "Xls_Zj_Rbb_Wj", "Xls_Zj_Rbb_Tjb" };
        private string[] dbTablesCN = new string[5] { "钻进", "下套管", "固井", "完井", "钻井数据统计表" };
        private int[] xlsCols = new int[2] { 23, 91 };

        private DataTable dtLishi = null;
        private string sqlLishi = "select * from T_ImportLog where tag=0";
        private int nPageSize = 10;

        private string userName = null;
        private int userPlaceId = 0;

        private static bool isRight = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userName = HttpContext.Current.Session["LoginUserID"].ToString();
                userPlaceId = Int32.Parse(HttpContext.Current.Session["PlaceID"].ToString());
            }
            catch (System.Exception ee)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            this.lbData.Click += new EventHandler(lbData_Click);

            if (!IsPostBack)
            {
                if (userPlaceId == 0)
                {
                    lstQukuai.Items.Add("韩城");
                    lstQukuai.Items.Add("临汾");
                    lstQukuai.Items.Add("忻州");
                    ddlFJQukuai.Items.Add("韩城");
                    ddlFJQukuai.Items.Add("临汾");
                    ddlFJQukuai.Items.Add("忻州");
                    lstLishiQukuai.Items.Add("全部");
                    lstLishiQukuai.Items.Add("韩城");
                    lstLishiQukuai.Items.Add("临汾");
                    lstLishiQukuai.Items.Add("忻州");
                }
                if (userPlaceId == 1)
                {
                    lstQukuai.Items.Add("韩城");
                    ddlFJQukuai.Items.Add("韩城");
                    lstLishiQukuai.Items.Add("韩城");
                }
                if (userPlaceId == 2)
                {
                    lstQukuai.Items.Add("临汾");
                    ddlFJQukuai.Items.Add("临汾");
                    lstLishiQukuai.Items.Add("临汾");
                }
                if (userPlaceId == 3)
                {
                    lstQukuai.Items.Add("忻州");
                    ddlFJQukuai.Items.Add("忻州");
                    lstLishiQukuai.Items.Add("忻州");
                }

                GridView1.Attributes.Add("BorderColor", "Black");
                GridView1.Attributes.Add("BorderWidth", "1");

                string strTemp = "";
                if (userPlaceId != 0)
                {
                    strTemp = " and qukuai='" + lstLishiQukuai.SelectedItem.Text + "'";
                }
                dtLishi = DataBaseHelper.query(sqlLishi + strTemp + " order by riqi desc");
                GridView1.PageSize = nPageSize;
                GridView1.DataSource = dtLishi;
                GridView1.DataBind();
                for (int i = 1; i <= GridView1.Rows.Count; i++)
                {
                    int n = GridView1.PageIndex * nPageSize + i - 1;
                    GridView1.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
                }
            }
            else
            {
                string sqlRiqi = "";
                string sqlQukuai = "";
                string sqlName = "";
                string sqlUser = "";
                string sqlNumber = "";
                lblLishi.Text = "";

                string strRiqiBegin = this.tbKaishiRiqi.Text.Trim();
                string strRiqiEnd = this.tbJieshuRiqi.Text.Trim();
                if (strRiqiBegin == "" && strRiqiEnd != "")
                {
                    lblLishi.ForeColor = System.Drawing.Color.Red;
                    lblLishi.Text = "条件选择错误：请输入开始日期。";
                    return;
                }
                if (strRiqiBegin != "" && strRiqiEnd == "")
                {
                    lblLishi.ForeColor = System.Drawing.Color.Red;
                    lblLishi.Text = "条件选择错误：请输入结束日期。";
                    return;
                }
                if (strRiqiBegin != "" && strRiqiEnd != "")
                {
                    try
                    {
                        DateTime dateBegin = Convert.ToDateTime(strRiqiBegin);
                        DateTime dateEnd = Convert.ToDateTime(strRiqiEnd);
                        sqlRiqi = " and cast(riqi as datetime) >= '" + dateBegin.ToString() + "' and cast(riqi as datetime) <= '" + dateEnd.ToString() + "'";
                    }
                    catch (System.FormatException fe)
                    {
                        lblLishi.ForeColor = System.Drawing.Color.Red;
                        lblLishi.Text = "条件选择错误：无法识别所输入的日期格式。";
                        return;
                    }
                }

                if (lstLishiQukuai.SelectedItem.Text.Trim() != "全部")
                {
                    sqlQukuai = " and qukuai='" + lstLishiQukuai.SelectedItem.Text.Trim() + "'";
                }

                if (lstLishiTable.SelectedIndex != 0)
                {
                    sqlName = " and name='" + lstLishiTable.SelectedItem.Text.Trim() + "'";
                }

                if (tbUser.Text.Trim() != "")
                {
                    sqlUser = " and usr='" + tbUser.Text.Trim() + "'";
                }

                string strNumBegin = tbNumBegin.Text.Trim();
                string strNumEnd = tbNumEnd.Text.Trim();
                if (strNumBegin == "" && strNumEnd != "")
                {
                    lblLishi.ForeColor = System.Drawing.Color.Red;
                    lblLishi.Text = "条件选择错误：请输入最小导入数量。";
                    return;
                }
                if (strNumBegin != "" && strNumEnd == "")
                {
                    lblLishi.ForeColor = System.Drawing.Color.Red;
                    lblLishi.Text = "条件选择错误：请输入最大导入数量。";
                    return;
                }
                if (strNumBegin != "" && strNumEnd != "")
                {
                    sqlNumber = " and number >= '" + strNumBegin + "' and number <= '" + strNumEnd + "'";
                }

                string sql = sqlLishi + sqlRiqi + sqlQukuai + sqlName + sqlUser + sqlNumber + " order by riqi desc";
                dtLishi = DataBaseHelper.query(sql);
                int count = dtLishi.Rows.Count;
                if (count <= 0)
                {
                    lblLishi.ForeColor = System.Drawing.Color.Red;
                    lblLishi.Text = "没有查询到符合条件的记录。";
                    return;
                }
                else
                {
                    lblLishi.ForeColor = System.Drawing.Color.Black;
                    lblLishi.Text = "查询到 " + count + " 条符合条件的记录。";
                    GridView1.DataSource = dtLishi;
                    GridView1.DataBind();
                    for (int i = 1; i <= GridView1.Rows.Count; i++)
                    {
                        int n = GridView1.PageIndex * nPageSize + i - 1;
                        GridView1.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
                    }
                }
            }
        }

        void lbData_Click(object sender, EventArgs e)
        {
            string tempFilePath = lblTempPath.Text;
            if (tempFilePath != null && tempFilePath != "")
            {
                MyTools.DeleteWebFile(tempFilePath);
            }

            if (fuData.HasFile)
            {
                lblData.Text = "";
                lblIsCover.Visible = false;
                lblIsCover.Text = "";
                btnImport.Text = "导入";
                btnReset.Visible = false;

                if (fuData.PostedFile.ContentLength < MyTools.maxLoadSize * 1024)
                {
                    try
                    {
                        fuData.PostedFile.SaveAs(Server.MapPath(MyTools.tmpRibaoDir) + fuData.FileName);
                        lblTempPath.Text = Server.MapPath(MyTools.tmpRibaoDir) + fuData.FileName;
                    }
                    catch (Exception ex)
                    {
                        lblData.ForeColor = System.Drawing.Color.Red;
                        lblData.Text = "文件上传出错：" + ex.Message;
                        return;
                    }
                }
                else
                {
                    lblData.ForeColor = System.Drawing.Color.Red;
                    lblData.Text = "上传文件不能大于4MB!";
                    return;
                }
            }
            else
            {
                lblData.Text = "";
                return;
            }

            tbRiqi.Text = "";
            lstQukuai.SelectedIndex = 0;

            string str = this.fuData.FileName;
            if (str == null || str == "") return;
            this.tbDataFile.Text = str;
            string info = "";
            string strQukuai = TableImport.getPlace(str);
            string strRiqi = TableImport.getDate(str);
            if (strQukuai == null)
            {
                info += "无法解析出区块名称,请手动指定.";
                lstQukuai.SelectedIndex = 0;
            }
            else if (strQukuai == "韩城") lstQukuai.SelectedIndex = 0;
            else if (strQukuai == "临汾") lstQukuai.SelectedIndex = 1;
            else if (strQukuai == "忻州") lstQukuai.SelectedIndex = 2;
            if (strRiqi != null) tbRiqi.Text = strRiqi;
            else info += "无法解析出日期,请手动输入.";

            lst1.Items.Clear();
            lst2.Items.Clear();
            lst1.Items.Add(" ");
            lst2.Items.Add(" ");
            List<string> lstSheets = new List<string>();
            string[] sheets = TableImport.GetExcelSheets(str);
            if (sheets != null && sheets.Length > 0)
            {
                int count = sheets.Length;
                for (int i = 0; i < count; i++)
                {
                    string[] ss = sheets[i].Split('\'');
                    if (ss.Length == 1)
                    {
                        string[] sss = ss[0].Split('$');
                        if (sss.Length >= 2 && sss[1].Trim() == "")
                        {
                            lst1.Items.Add(sss[0]);
                            lst2.Items.Add(sss[0]);
                        }
                    }
                    else
                    {
                        if (ss[2].Trim() == "")
                        {
                            string[] sss = ss[1].Split('$');
                            if (sss.Length >= 2 && sss[1].Trim() == "")
                            {
                                lst1.Items.Add(sss[0]);
                                lst2.Items.Add(sss[0]);
                            }
                        }
                    }
                }

                lst1.SelectedIndex = 0;
                lst2.SelectedIndex = 0;

                for (int i = 0; i < lst1.Items.Count; i++)
                {
                    if (lst1.Items[i].Text.Trim() == "钻井监督日报" || lst1.Items[i].Text.Trim() == "钻井监督日报表")
                    {
                        lst1.SelectedIndex = i;
                    }
                    if (lst2.Items[i].Text.Trim() == "钻井数据统计表" || lst2.Items[i].Text.Trim() == "数据统计表")
                    {
                        lst2.SelectedIndex = i;
                    }
                }
            }
            else
            {
                info += "无法获取电子表格的工作表。";
                string filePath = HttpContext.Current.Server.MapPath(MyTools.tmpRibaoDir) + str;
                MyTools.DeleteWebFile(filePath);
            }
            this.lblData.ForeColor = System.Drawing.Color.Red;
            this.lblData.Text = info;
        }

        protected void check_click(object sender, EventArgs e)
        {
            this.lblData.Text = "正在验证，请稍后......";

            String fileName = tbDataFile.Text.Trim();
            if (fileName == null || fileName == "")
            {
                this.lblData.ForeColor = System.Drawing.Color.Red;
                this.lblData.Text = "验证失败：没有选择文件.<br/>";
                isRight = false;
                return;
            }

            String strQukuai = lstQukuai.SelectedItem.Text.Trim();
            if (strQukuai == null || strQukuai == "")
            {
                this.lblData.ForeColor = System.Drawing.Color.Red;
                this.lblData.Text = "验证失败：请选择对应的区块.<br/>";
                isRight = false;
                return;
            }
            String strRiqi = tbRiqi.Text.Trim();
            if (strRiqi == null || strRiqi == "")
            {
                this.lblData.ForeColor = System.Drawing.Color.Red;
                this.lblData.Text = "验证失败：请输入一个有效的日期.<br/>";
                isRight = false;
                return;
            }
            if (lst1.Items.Count == 0)
            {
                this.lblData.ForeColor = System.Drawing.Color.Red;
                this.lblData.Text = "验证失败：没有获取到有效的工作表.<br/>";
                isRight = false;
                return;
            }

            String sheet1 = lst1.SelectedItem.Text;
            if (sheet1.Trim() != "")
            {
                DataTable dt = TableImport.ReadExcel(fileName, sheet1);
                if (dt == null || dt.Rows.Count == 0)
                {
                    this.lblData.ForeColor = System.Drawing.Color.Red;
                    this.lblData.Text = "验证失败：没有读取到工作表：\"" + sheet1 + "\"中的数据.<br/>";
                    isRight = false;
                    return;
                }
                else
                {
                    for (int i = dt.Columns.Count; i >= 0; i--)
                    {
                        string str5 = dt.Rows[5][i - 1].ToString().Trim();
                        string str6 = dt.Rows[6][i - 1].ToString().Trim();
                        if (str5 == "" && str6 == "") dt.Columns.RemoveAt(i - 1);
                        else break;
                    }

                    if (dt.Columns.Count != xlsCols[0])
                    {
                        this.lblData.ForeColor = System.Drawing.Color.Red;
                        this.lblData.Text = "验证失败：工作表：\"" + sheet1 + "\"中的列数数据库中不一致.<br/>";
                        isRight = false;
                        return;
                    }
                    else
                    {
                        DataTable[] dts = TableImport.SplitTable(dt);
                        if (dts == null || dts[0] == null || dts[1] == null || dts[2] == null || dts[3] == null)
                        {
                            this.lblData.ForeColor = System.Drawing.Color.Red;
                            this.lblData.Text = "验证失败：无法将该工作表数据拆分成‘钻进’、‘下套管’、‘固井’、‘完井’等4个子表.<br/>";
                            isRight = false;
                            return;
                        }
                        else
                        {
                            TableImport.Edit_Zjjdrbb_Zj(dts[0], strQukuai, strRiqi);
                            TableImport.Edit_Zjjdrbb_Xtg(dts[1], strQukuai, strRiqi);
                            TableImport.Edit_Zjjdrbb_Gj(dts[2], strQukuai, strRiqi);
                            TableImport.Edit_Zjjdrbb_Wj(dts[3], strQukuai, strRiqi);
                        }

                    }
                }
            }

            String sheet2 = lst2.SelectedItem.Text;
            if (sheet2.Trim() != "")
            {
                DataTable dt = TableImport.ReadExcel(fileName, sheet2);
                if (dt == null || dt.Rows.Count == 0)
                {
                    this.lblData.ForeColor = System.Drawing.Color.Red;
                    this.lblData.Text = "验证失败：没有读取到该工作表：\"" + sheet2 + "\"中的数据.<br/>";
                    isRight = false;
                    return;
                }
                else
                {
                    for (int i = dt.Columns.Count; i > 0; i--)
                    {
                        string str1 = dt.Rows[0][i - 1].ToString().Trim();
                        string str2 = dt.Rows[1][i - 1].ToString().Trim();
                        string str3 = dt.Rows[2][i - 1].ToString().Trim();
                        string str4 = dt.Rows[3][i - 1].ToString().Trim();
                        if (str1 == "" && str2 == "" && str3 == "" && str4 == "") dt.Columns.RemoveAt(i - 1);
                        else break;
                    }
                    if (dt.Columns.Count != xlsCols[1])
                    {
                        this.lblData.ForeColor = System.Drawing.Color.Red;
                        this.lblData.Text = "验证失败：工作表：\"" + sheet2 + "\"中的列数与数据库中不一致.<br/>";
                        isRight = false;
                        return;
                    }
                    else
                    {
                        TableImport.Edit_Zjjdrbb_Scjwjtjb(dt, strQukuai, strRiqi);
                    }
                }
            }

            bool b1 = false;
            string info = "该日期的";
            if (lst1.SelectedIndex != 0)
            {
                if (TableImport.existLog(strRiqi, strQukuai, dbTablesCN[0]))
                {
                    b1 = true;
                    info += dbTablesCN[0] + ",";
                }
                if (TableImport.existLog(strRiqi, strQukuai, dbTablesCN[1]))
                {
                    b1 = true;
                    info += dbTablesCN[1] + ",";
                }
                if (TableImport.existLog(strRiqi, strQukuai, dbTablesCN[2]))
                {
                    b1 = true;
                    info += dbTablesCN[2] + ",";
                }
                if (TableImport.existLog(strRiqi, strQukuai, dbTablesCN[3]))
                {
                    b1 = true;
                    info += dbTablesCN[3] + ",";
                }
            }

            info += "已经导入，是否覆盖？";
            if (b1)
            {
                lblIsCover.Visible = true;
                lblIsCover.Text = info;
                btnImport.Text = "覆盖";
                btnReset.Visible = true;
            }

            this.lblData.ForeColor = System.Drawing.Color.Black;
            this.lblData.Text = "验证成功，可以进行导入。";
            isRight = true;
        }

        protected void import_click(object sender, EventArgs e)
        {
            if (!isRight)
            {
                this.lblData.ForeColor = System.Drawing.Color.Black;
                this.lblData.Text = "尚未验证成功，无法导入。";
                return;
            }

            this.lblData.Text = "正在导入，请稍后......";

            String fileName = tbDataFile.Text.Trim();
            String strQukuai = lstQukuai.SelectedItem.Text.Trim();
            String strRiqi = tbRiqi.Text.Trim();

            List<string> sqls = null;
            int count = 0;
            string info = "导入结果：<br/>";

            String sheet1 = lst1.SelectedItem.Text;
            if (sheet1.Trim() != "")
            {
                DataTable dt = TableImport.ReadExcel(fileName, sheet1);
                for (int i = dt.Columns.Count; i >= xlsCols[0]; i--)
                {
                    string str5 = dt.Rows[5][i - 1].ToString().Trim();
                    string str6 = dt.Rows[6][i - 1].ToString().Trim();
                    if (str5 == "" && str6 == "") dt.Columns.RemoveAt(i - 1);
                    else break;
                }
                DataTable[] dts = TableImport.SplitTable(dt);
                TableImport.Edit_Zjjdrbb_Zj(dts[0], strQukuai, strRiqi);
                TableImport.Edit_Zjjdrbb_Xtg(dts[1], strQukuai, strRiqi);
                TableImport.Edit_Zjjdrbb_Gj(dts[2], strQukuai, strRiqi);
                TableImport.Edit_Zjjdrbb_Wj(dts[3], strQukuai, strRiqi);

                if (TableImport.existLog(strRiqi, strQukuai, dbTablesCN[0]))
                {
                    if (btnImport.Text == "覆盖")
                    {
                        string sql = "delete from " + dbTables[0] + " where riqi = '" + strRiqi + "'";
                        DataBaseHelper.execute(sql);
                        sql = "delete from T_ImportLog where name = '" + dbTablesCN[0] + "' and riqi = '" + strRiqi + "'";
                        DataBaseHelper.execute(sql);

                        sqls = TableImport.CreateSqls(dts[0], dbTables[0]);
                        count = TableImport.Import(sqls);
                        TableImport.addLog(strRiqi, strQukuai, dbTablesCN[0], userName, count, 0);
                        info += "　　" + dbTablesCN[0] + "表共导入\"" + count + "\"条有效记录；<br/>";
                    }
                }
                else
                {
                    sqls = TableImport.CreateSqls(dts[0], dbTables[0]);
                    count = TableImport.Import(sqls);
                    TableImport.addLog(strRiqi, strQukuai, dbTablesCN[0], userName, count, 0);
                    info += "　　" + dbTablesCN[0] + "表共导入\"" + count + "\"条有效记录；<br/>";
                }
                if (TableImport.existLog(strRiqi, strQukuai, dbTablesCN[1]))
                {
                    if (btnImport.Text == "覆盖")
                    {
                        string sql = "delete from " + dbTables[1] + " where riqi = '" + strRiqi + "'";
                        DataBaseHelper.execute(sql);
                        sql = "delete from T_ImportLog where name = '" + dbTablesCN[1] + "' and riqi = '" + strRiqi + "'";
                        DataBaseHelper.execute(sql);

                        sqls = TableImport.CreateSqls(dts[1], dbTables[1]);
                        count = TableImport.Import(sqls);
                        TableImport.addLog(strRiqi, strQukuai, dbTablesCN[1], userName, count, 0);
                        info += "　　" + dbTablesCN[1] + "表共导入\"" + count + "\"条有效记录；<br/>";
                    }
                }
                else
                {
                    sqls = TableImport.CreateSqls(dts[1], dbTables[1]);
                    count = TableImport.Import(sqls);
                    TableImport.addLog(strRiqi, strQukuai, dbTablesCN[1], userName, count, 0);
                    info += "　　" + dbTablesCN[1] + "表共导入\"" + count + "\"条有效记录；<br/>";
                }
                if (TableImport.existLog(strRiqi, strQukuai, dbTablesCN[2]))
                {
                    if (btnImport.Text == "覆盖")
                    {
                        string sql = "delete from " + dbTables[2] + " where riqi = '" + strRiqi + "'";
                        DataBaseHelper.execute(sql);
                        sql = "delete from T_ImportLog where name = '" + dbTablesCN[2] + "' and riqi = '" + strRiqi + "'";
                        DataBaseHelper.execute(sql);

                        sqls = TableImport.CreateSqls(dts[2], dbTables[2]);
                        count = TableImport.Import(sqls);
                        TableImport.addLog(strRiqi, strQukuai, dbTablesCN[2], userName, count, 0);
                        info += "　　" + dbTablesCN[2] + "表共导入\"" + count + "\"条有效记录；<br/>";
                    }
                }
                else
                {
                    sqls = TableImport.CreateSqls(dts[2], dbTables[2]);
                    count = TableImport.Import(sqls);
                    TableImport.addLog(strRiqi, strQukuai, dbTablesCN[2], userName, count, 0);
                    info += "　　" + dbTablesCN[2] + "表共导入\"" + count + "\"条有效记录；<br/>";
                }
                if (TableImport.existLog(strRiqi, strQukuai, dbTablesCN[3]))
                {
                    if (btnImport.Text == "覆盖")
                    {
                        string sql = "delete from " + dbTables[3] + " where riqi = '" + strRiqi + "'";
                        DataBaseHelper.execute(sql);
                        sql = "delete from T_ImportLog where name = '" + dbTablesCN[3] + "' and riqi = '" + strRiqi + "'";
                        DataBaseHelper.execute(sql);

                        sqls = TableImport.CreateSqls(dts[3], dbTables[3]);
                        count = TableImport.Import(sqls);
                        TableImport.addLog(strRiqi, strQukuai, dbTablesCN[3], userName, count, 0);
                        info += "　　" + dbTablesCN[3] + "表共导入\"" + count + "\"条有效记录；<br/>";
                    }
                }
                else
                {
                    sqls = TableImport.CreateSqls(dts[3], dbTables[3]);
                    count = TableImport.Import(sqls);
                    TableImport.addLog(strRiqi, strQukuai, dbTablesCN[3], userName, count, 0);
                    info += "　　" + dbTablesCN[3] + "表共导入\"" + count + "\"条有效记录。<br/>";
                }
            }

            String sheet2 = lst2.SelectedItem.Text;
            if (sheet2.Trim() != "")
            {
                DataTable dt = TableImport.ReadExcel(fileName, sheet2);
                for (int i = dt.Columns.Count; i > 0; i--)
                {
                    string str1 = dt.Rows[0][i - 1].ToString().Trim();
                    string str2 = dt.Rows[1][i - 1].ToString().Trim();
                    string str3 = dt.Rows[2][i - 1].ToString().Trim();
                    string str4 = dt.Rows[3][i - 1].ToString().Trim();
                    if (str1 == "" && str2 == "" && str3 == "" && str4 == "") dt.Columns.RemoveAt(i - 1);
                    else break;
                }
                TableImport.Edit_Zjjdrbb_Scjwjtjb(dt, strQukuai, strRiqi);
                sqls = TableImport.CreateSqls(dt, dbTables[4]);
                count = TableImport.Import(sqls);
                TableImport.addLog(strRiqi, strQukuai, dbTablesCN[4], userName, count, 0);
                info += "　　" + dbTablesCN[4] + "共导入\"" + count + "\"条有效记录。<br/>";
            }

            //删除文件
            string tempFilePath = lblTempPath.Text;
            MyTools.DeleteWebFile(tempFilePath);

            this.lblData.ForeColor = System.Drawing.Color.Black;
            this.lblData.Text = info;

            tbDataFile.Text = "";
            lstQukuai.SelectedIndex = -1;
            tbRiqi.Text = "";

            lst1.Items.Clear();
            lst2.Items.Clear();
            lst1.Items.Add(" ");
            lst2.Items.Add(" ");

            lblIsCover.Visible = false;
            lblIsCover.Text = "";
            btnImport.Text = "导入";
            btnReset.Visible = false;

            //TableImport.updateJichuxinxi();
        }

        protected void reset_click(object sender, EventArgs e)
        {
            tbDataFile.Text = "";
            lstQukuai.SelectedIndex = -1;
            tbRiqi.Text = "";

            lst1.Items.Clear();
            lst2.Items.Clear();
            lst1.Items.Add(" ");
            lst2.Items.Add(" ");

            lblData.Text = "";
            lblIsCover.Visible = false;
            lblIsCover.Text = "";
            btnImport.Text = "导入";
            btnReset.Visible = false;
        }

        protected void Page_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = dtLishi;
            GridView1.DataBind();
            for (int i = 1; i <= GridView1.Rows.Count; i++)
            {
                int n = GridView1.PageIndex * nPageSize + i - 1;
                GridView1.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string sqlRiqi = "";
            string sqlQukuai = "";
            string sqlName = "";
            string sqlUser = "";
            string sqlNumber = "";
            lblLishi.Text = "";

            string strRiqiBegin = this.tbKaishiRiqi.Text.Trim();
            string strRiqiEnd = this.tbJieshuRiqi.Text.Trim();
            if (strRiqiBegin == "" && strRiqiEnd != "")
            {
                lblLishi.ForeColor = System.Drawing.Color.Red;
                lblLishi.Text = "条件选择错误：请输入开始日期。";
                return;
            }
            if (strRiqiBegin != "" && strRiqiEnd == "")
            {
                lblLishi.ForeColor = System.Drawing.Color.Red;
                lblLishi.Text = "条件选择错误：请输入结束日期。";
                return;
            }
            if (strRiqiBegin != "" && strRiqiEnd != "")
            {
                try
                {
                    DateTime dateBegin = Convert.ToDateTime(strRiqiBegin);
                    DateTime dateEnd = Convert.ToDateTime(strRiqiEnd);
                    sqlRiqi = " and cast(riqi as datetime) >= '" + dateBegin.ToString() + "' and cast(riqi as datetime) <= '" + dateEnd.ToString() + "'";
                }
                catch (System.FormatException fe)
                {
                    lblLishi.ForeColor = System.Drawing.Color.Red;
                    lblLishi.Text = "条件选择错误：无法识别所输入的日期格式。";
                    return;
                }
            }

            if (lstLishiQukuai.SelectedItem.Text.Trim() != "全部")
            {
                sqlQukuai = " and qukuai='" + lstLishiQukuai.SelectedItem.Text.Trim() + "'";
            }

            if (lstLishiTable.SelectedIndex != 0)
            {
                sqlName = " and name='" + lstLishiTable.SelectedItem.Text.Trim() + "'";
            }

            if (tbUser.Text.Trim() != "")
            {
                sqlUser = " and usr='" + tbUser.Text.Trim() + "'";
            }

            string strNumBegin = tbNumBegin.Text.Trim();
            string strNumEnd = tbNumEnd.Text.Trim();
            if (strNumBegin == "" && strNumEnd != "")
            {
                lblLishi.ForeColor = System.Drawing.Color.Red;
                lblLishi.Text = "条件选择错误：请输入最小导入数量。";
                return;
            }
            if (strNumBegin != "" && strNumEnd == "")
            {
                lblLishi.ForeColor = System.Drawing.Color.Red;
                lblLishi.Text = "条件选择错误：请输入最大导入数量。";
                return;
            }
            if (strNumBegin != "" && strNumEnd != "")
            {
                sqlNumber = " and number >= '" + strNumBegin + "' and number <= '" + strNumEnd + "'";
            }

            string sql = sqlLishi + sqlRiqi + sqlQukuai + sqlName + sqlUser + sqlNumber + " order by riqi desc";
            dtLishi = DataBaseHelper.query(sql);
            int count = dtLishi.Rows.Count;
            if (count <= 0)
            {
                lblLishi.ForeColor = System.Drawing.Color.Red;
                lblLishi.Text = "没有查询到符合条件的记录。";
                return;
            }
            else
            {
                lblLishi.ForeColor = System.Drawing.Color.Black;
                lblLishi.Text = "查询到 " + count + " 条符合条件的记录。";
                GridView1.DataSource = dtLishi;
                GridView1.DataBind();
                for (int i = 1; i <= GridView1.Rows.Count; i++)
                {
                    int n = GridView1.PageIndex * nPageSize + i - 1;
                    GridView1.Rows[i - 1].Cells[0].Text = (n + 1).ToString();
                }
            }
        }

    }
}
