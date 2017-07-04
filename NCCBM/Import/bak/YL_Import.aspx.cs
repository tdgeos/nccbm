using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Configuration;
using System.Net;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace NCCBM.Import
{
    public partial class YL_Import : System.Web.UI.Page
    {
        private string[] dbTablesCN = new string[4] { "压裂检查", "射孔", "压裂施工", "失败原因说明" };
        private string[] dbTables = new string[4] { "Xls_Yl_Rbb_Yqjc", "Xls_Yl_Rbb_Sk", "Xls_Yl_Rbb_Ylsg", "Xls_Yl_Rbb_Sbyysm" };
        private int[] xlsCols = new int[4] { 20, 15,  46, 9 };

        private DataTable dtLishi = null;
        private string sqlLishi = "select * from T_ImportLog where tag=1";
        private int nPageSize = 10;

        private string userName = "";
        private int userPlaceId = 0;

        private static bool isRight = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userName = HttpContext.Current.Session["LoginUserID"].ToString();
                Response.Cookies["username"].Value = userName;
                userPlaceId = Int32.Parse(HttpContext.Current.Session["PlaceID"].ToString());
            }
            catch (System.Exception ee)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            this.lbData.Click += new EventHandler(lbData_Click);
            this.lbYlsgzl.Click += new EventHandler(lbYlsgzl_Click);
            lbFujian.Click += new EventHandler(lbFujian_Click);

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
                dtLishi = DataBaseHelper.query(sqlLishi + strTemp);
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
            addFujianList();
        }

        void lbtn_Command(object sender, CommandEventArgs e)
        {
            string item = (string)e.CommandArgument;
            string list = lblFujianList.Text;
            string temp = "";
            if (list != "")
            {
                string[] ss = list.Split(';');
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i] == item)
                    {
                        string strDir = Server.MapPath(TableImport.tmpFujianDir);
                        string fj = ss[i];
                        string path = strDir + fj;
                        FileInfo file = new FileInfo(path);
                        file.Delete();
                        continue;
                    }
                    if (temp == "") temp = ss[i];
                    else temp += ";" + ss[i];
                }
                lblFujianList.Text = temp;
                addFujianList();
            }
        }

        void addFujianList()
        {
            this.Panel1.Controls.Clear();
            string list = lblFujianList.Text;
            if (list != "")
            {
                string[] ss = list.Split(';');
                for (int i = 0; i < ss.Length; i++)
                {
                    string value = ss[i];
                    Label lbl = new Label();
                    lbl.Text = value;
                    lbl.Width = 350;
                    LinkButton lbtn = new LinkButton();
                    lbtn.CommandArgument = ss[i];
                    lbtn.Text = "删除";
                    lbtn.Command += new CommandEventHandler(lbtn_Command);
                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Style["style"] = "border-width:1px; border-color:#d0d0f0; border-style:solid;margin-top:2px;";
                    div.Controls.Add(lbl);
                    div.Controls.Add(lbtn);
                    this.Panel1.Controls.Add(div);
                }
            }
        }

        void lbFujian_Click(object sender, EventArgs e)
        {
            lblUpload.Text = "";
            if (fuFujian.HasFile)
            {
                if (fuFujian.PostedFile.ContentLength < TableImport.maxLoadSize * 1024)
                {
                    try
                    {
                        fuFujian.PostedFile.SaveAs(Server.MapPath(TableImport.tmpFujianDir) + fuFujian.FileName);
                        tbFujian.Text = fuFujian.FileName;
                    }
                    catch (Exception ex)
                    {
                        lblUpload.ForeColor = System.Drawing.Color.Red;
                        lblUpload.Text = "文件上传出错：" + ex.Message;
                        return;
                    }
                }
                else
                {
                    lblUpload.ForeColor = System.Drawing.Color.Red;
                    lblUpload.Text = "文件过大!";
                    return;
                }
            }
            else
            {
                return;
            }
        }

        protected void addfj_click(object sender, EventArgs e)
        {
            lblUpload.Text = "";

            string filename = tbFujian.Text;

            string list = lblFujianList.Text;
            string[] ss = list.Split(';');
            if (filename != "")
            {
                string item = filename;
                bool b = true;
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i] == item)
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    if (list != "") list = list + ";" + item;
                    else list = item;
                    lblFujianList.Text = list;
                }
                tbFujian.Text = "";
                addFujianList();  
            }
            else
            {
                lblUpload.ForeColor = System.Drawing.Color.Red;
                lblUpload.Text = "没有选择附件！";
            }         
        }

        protected void uploadfj_click(object sender, EventArgs e)
        {
            string dbTable = null;
            string fjDir = null;
            int index = ddlFJTable.SelectedIndex;
            if (index == 0) dbTable = dbTables[0];
            if (index == 1) dbTable = dbTables[1];
            if (index == 2) dbTable = dbTables[2];
            if (index == 3) dbTable = dbTables[3];
            if (index == 3)
            {
                fjDir = "失败原因说明";
            }
            else
            {
                fjDir = dbTablesCN[index];
            }
            string jinghao = tbJinghao.Text;
            string cenghao = tbCenghao.Text;
            string qukuai = ddlFJQukuai.SelectedItem.Text;

            if (jinghao == "")
            {
                lblUpload.ForeColor = System.Drawing.Color.Red;
                lblUpload.Text = "没有指定井号！";
                return;
            }
            if (cenghao == "")
            {
                lblUpload.ForeColor = System.Drawing.Color.Red;
                lblUpload.Text = "没有指定层号！";
                return;
            }
            cenghao = cenghao.Replace('#', '^');
            cenghao = cenghao.Replace('+', '~');

            string strDir = Server.MapPath(TableImport.tmpFujianDir);
            string list = lblFujianList.Text;
            if (list != "")
            {
                string[] ss = list.Split(';');
                for (int i = 0; i < ss.Length; i++)
                {
                    string fj = ss[i];
                    string path = strDir + fj;
                    FileInfo file = new FileInfo(path);
                    TableImport.Upload(file, qukuai + "/" + fjDir + "/" + jinghao + "_" + cenghao);
                    file.Delete();
                }
                lblUpload.ForeColor = System.Drawing.Color.Black;
                lblUpload.Text = "上传完成。";
                lblFujianList.Text = "";
                addFujianList();
                tbJinghao.Text = "";
                tbCenghao.Text = "";
                ddlFJQukuai.SelectedIndex = 0;
                ddlFJTable.SelectedIndex = 0;
            }
            else
            {
                lblUpload.ForeColor = System.Drawing.Color.Red;
                lblUpload.Text = "没有附件！";
            }
        }

        void lbYlsgzl_Click(object sender, EventArgs e)
        {
            string tempFilePath = lblZhiliang.Text;
            if (tempFilePath != null && tempFilePath != "")
            {
                TableImport.DeleteTempFile(tempFilePath);
                tempFilePath = null;
            }

            if (fuYlsgzl.HasFile)
            {
                lblData.Text = "";

                if (fuYlsgzl.PostedFile.ContentLength < TableImport.maxLoadSize * 1024)
                {
                    try
                    {
                        fuYlsgzl.PostedFile.SaveAs(Server.MapPath(TableImport.tmpRibaoDir) + fuYlsgzl.FileName);
                        lblZhiliang.Text = Server.MapPath(TableImport.tmpRibaoDir) + fuYlsgzl.FileName;
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
                    lblData.Text = "文件过大!";
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

            string str = this.fuYlsgzl.FileName;
            if (str == null || str == "") return;
            this.tbYlsgzl.Text = str;
            string info = "";
            string strQukuai = TableImport.getPlace(str);
            string strRiqi = TableImport.getDate(str);
            if (strQukuai == null)
            {
            }
            else if (strQukuai == "韩城") lstQukuai.SelectedIndex = 0;
            else if (strQukuai == "临汾") lstQukuai.SelectedIndex = 1;
            else if (strQukuai == "忻州") lstQukuai.SelectedIndex = 2;
            if (strRiqi != null) tbRiqi.Text = strRiqi;

            ddlYljc.Items.Clear();
            ddlYljc.Items.Add(" ");
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
                            ddlYljc.Items.Add(sss[0]);
                        }
                    }
                    else
                    {
                        if (ss[2].Trim() == "")
                        {
                            string[] sss = ss[1].Split('$');
                            if (sss.Length >= 2 && sss[1].Trim() == "")
                            {
                                ddlYljc.Items.Add(sss[0]);
                            }
                        }
                    }
                }

                ddlYljc.SelectedIndex = 0;

                for (int i = 0; i < ddlYljc.Items.Count; i++)
                {
                    if (ddlYljc.Items[i].Text.Trim() == "压裂检查" || ddlYljc.Items[i].Text.Trim() == "压裂检查（含HSE）")
                    {
                        ddlYljc.SelectedIndex = i;
                    }
                }
            }
            else
            {
                info += "无法获取电子表格的工作表。";
                string filePath = HttpContext.Current.Server.MapPath(TableImport.tmpRibaoDir) + str;
                TableImport.DeleteTempFile(filePath);
            }
            this.lblData.ForeColor = System.Drawing.Color.Red;
            this.lblData.Text = info;
        }

        void lbData_Click(object sender, EventArgs e)
        {
            string tempFilePath = lblGongcheng.Text;
            if (tempFilePath != null && tempFilePath != "")
            {
                TableImport.DeleteTempFile(tempFilePath);
                tempFilePath = null;
            }

            if (fuData.HasFile)
            {
                if (fuData.PostedFile.ContentLength < TableImport.maxLoadSize * 1024)
                {
                    try
                    {
                        fuData.PostedFile.SaveAs(Server.MapPath(TableImport.tmpRibaoDir) + fuData.FileName);
                        lblGongcheng.Text = Server.MapPath(TableImport.tmpRibaoDir) + fuData.FileName;
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
                return;
            }

            string str = this.fuData.FileName;
            if (str == null || str == "") return;
            this.tbDataFile.Text = str;
            string info = "";

            ddlSk.Items.Clear();
            ddlYlsg.Items.Clear();
            ddlYc.Items.Clear();
            ddlSk.Items.Add(" ");
            ddlYlsg.Items.Add(" ");
            ddlYc.Items.Add(" ");
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
                            ddlSk.Items.Add(sss[0]);
                            ddlYlsg.Items.Add(sss[0]);
                            ddlYc.Items.Add(sss[0]);
                        }
                    }
                    else
                    {
                        if (ss[2].Trim() == "")
                        {
                            string[] sss = ss[1].Split('$');
                            if (sss.Length >= 2 && sss[1].Trim() == "")
                            {
                                ddlSk.Items.Add(sss[0]);
                                ddlYlsg.Items.Add(sss[0]);
                                ddlYc.Items.Add(sss[0]);
                            }
                        }
                    }
                }

                ddlSk.SelectedIndex = 0;
                ddlYlsg.SelectedIndex = 0;
                ddlYc.SelectedIndex = 0;

                for (int i = 0; i < ddlSk.Items.Count; i++)
                {
                    if (ddlSk.Items[i].Text.Trim() == "射孔")
                    {
                        ddlSk.SelectedIndex = i;
                    }
                    if (ddlYlsg.Items[i].Text.Trim() == "压裂施工")
                    {
                        ddlYlsg.SelectedIndex = i;
                    }
                    if (ddlYc.Items[i].Text.Trim() == "失败原因说明" 
                        || ddlYc.Items[i].Text.Trim() == "加砂量未达到100%原因"
                        || ddlYc.Items[i].Text.Trim() == "加砂量未达到设计要求100%原因")
                    {
                        ddlYc.SelectedIndex = i;
                    }
                }
            }
            else
            {
                info += "无法获取电子表格的工作表。";
                string filePath = HttpContext.Current.Server.MapPath(TableImport.tmpRibaoDir) + str;
                TableImport.DeleteTempFile(filePath);
            }
            this.lblData.ForeColor = System.Drawing.Color.Red;
            this.lblData.Text = info;
        }

        protected void check_click(object sender, EventArgs e)
        {
            this.lblData.Text = "正在验证，请稍后......";

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

            String fileName1 = tbYlsgzl.Text.Trim();
            if (fileName1 != "")
            {
                String sheet = ddlYljc.SelectedItem.Text;
                if (sheet.Trim() != "")
                {
                    DataTable dt = TableImport.ReadExcel(fileName1, sheet);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        this.lblData.ForeColor = System.Drawing.Color.Red;
                        this.lblData.Text = "验证失败：无法读取压裂施工质量日报中的数据.<br/>";
                        isRight = false;
                        return;
                    }
                    else
                    {
                        TableImport.Edit_Ylsgrbb_Yqjc(dt, strQukuai, strRiqi);
                        if (dt.Columns.Count != xlsCols[0])
                        {
                            this.lblData.ForeColor = System.Drawing.Color.Red;
                            this.lblData.Text = "验证失败：工作表：\"" + sheet + "\"中的列数与数据库中不一致.<br/>";
                            isRight = false;
                            return;
                        }
                    }
                }
            }

            String fileName = tbDataFile.Text.Trim();
            if (fileName != "")
            {
                for (int i = 0; i < 3; i++)
                {
                    String sheet = null;
                    if (i == 0) sheet = ddlSk.SelectedItem.Text;
                    if (i == 1) sheet = ddlYlsg.SelectedItem.Text;
                    if (i == 2) sheet = ddlYc.SelectedItem.Text;
                    if (sheet.Trim() != "")
                    {
                        DataTable dt = TableImport.ReadExcel(fileName, sheet);
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            this.lblData.ForeColor = System.Drawing.Color.Red;
                            this.lblData.Text = "验证失败：没有读取到工作表：\"" + sheet + "\"中的数据.<br/>";
                            isRight = false;
                            return;
                        }
                        else
                        {
                            if (i == 0) TableImport.Edit_Ylsgrbb_Sk(dt, strQukuai, strRiqi);
                            if (i == 1) TableImport.Edit_Ylsgrbb_Ylsg(dt, strQukuai, strRiqi);
                            if (i == 2) TableImport.Edit_Ylsgrbb_Sbyysm(dt, strQukuai, strRiqi);

                            if (dt.Columns.Count != xlsCols[i+1])
                            {
                                this.lblData.ForeColor = System.Drawing.Color.Red;
                                this.lblData.Text = "验证失败：工作表：\"" + sheet + "\"中的列数与数据库中不一致.<br/>";
                                isRight = false;
                                return;
                            }
                        }
                    }
                }
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

            String strQukuai = lstQukuai.SelectedItem.Text.Trim();
            String strRiqi = tbRiqi.Text.Trim();

            List<string> sqls = null;
            int count = 0;
            string info = "导入结果：<br/>";

            String fileName1 = tbYlsgzl.Text.Trim();
            String sheet = ddlYljc.SelectedItem.Text;
            if (sheet.Trim() != "")
            {
                DataTable dt = TableImport.ReadExcel(fileName1, sheet);
                TableImport.Edit_Ylsgrbb_Yqjc(dt, strQukuai, strRiqi);

                sqls = TableImport.CreateSqls(dt, dbTables[0]);
                count = TableImport.Import(sqls);
                TableImport.addLog(strRiqi, strQukuai, dbTablesCN[0], userName, count, 1);
                info += "　　" + dbTablesCN[0] + "表共导入\"" + count + "\"条有效记录；<br/>";
            }

            String fileName = tbDataFile.Text.Trim();

            for (int i = 0; i < 3; i++)
            {
                sheet = null;
                if (i == 0) sheet = ddlSk.SelectedItem.Text;
                if (i == 1) sheet = ddlYlsg.SelectedItem.Text;
                if (i == 2) sheet = ddlYc.SelectedItem.Text;

                if (sheet.Trim() != "")
                {
                    DataTable dt = TableImport.ReadExcel(fileName, sheet);
                    if (i == 0) TableImport.Edit_Ylsgrbb_Sk(dt, strQukuai, strRiqi);
                    if (i == 1) TableImport.Edit_Ylsgrbb_Ylsg(dt, strQukuai, strRiqi);
                    if (i == 2) TableImport.Edit_Ylsgrbb_Sbyysm(dt, strQukuai, strRiqi);

                    sqls = TableImport.CreateSqls(dt, dbTables[i+1]);
                    count = TableImport.Import(sqls);
                    TableImport.addLog(strRiqi, strQukuai, dbTablesCN[i+1], userName, count, 1);
                    info += "　　" + dbTablesCN[i+1] + "表共导入\"" + count + "\"条有效记录；<br/>";
                }
            }

            lblData.Text = info;

            //删除文件
            TableImport.DeleteTempFile(lblGongcheng.Text);
            lblGongcheng.Text = null;
            TableImport.DeleteTempFile(lblZhiliang.Text);
            lblZhiliang.Text = null;

            tbDataFile.Text = "";
            tbYlsgzl.Text = "";
            lstQukuai.SelectedIndex = -1;
            tbRiqi.Text = "";

            ddlYljc.Items.Clear();
            ddlSk.Items.Clear();
            ddlYlsg.Items.Clear();
            ddlYc.Items.Clear();
            ddlYljc.Items.Add(" ");
            ddlSk.Items.Add(" ");
            ddlYlsg.Items.Add(" ");
            ddlYc.Items.Add(" ");

            TableImport.updateJichuxinxi();
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

            if (lstLishiQukuai.SelectedIndex != 0)
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