using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data.OleDb;
using System.Xml;
using System.Xml.Linq;


namespace MyMap.Web
{
    /// <summary>
    /// SqlService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SqlService : System.Web.Services.WebService
    {
        public static string tempDir = ConfigurationSettings.AppSettings["TempOtherDir"].ToString();
        public static string connStr = ConfigurationManager.ConnectionStrings["_NCCBM"].ToString();
        public static string dbTable = "jing_jichuxinxi";

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool sqlExecute(String sql)
        {
            if (sql == null || sql.Equals("")) return false;
            int r = 0;
            try
            {
                SqlConnection _sqlConnection = new SqlConnection();
                _sqlConnection.ConnectionString = connStr;
                _sqlConnection.Open();

                SqlCommand comm = new SqlCommand(sql, _sqlConnection);
                r = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            if (r > 0) return true;
            return false;
        }

        [WebMethod]
        public void InsertJing(String sql, String jh)
        {
            if (sql == null || sql.Equals("") || jh == null || jh.Equals("")) return;

            SqlConnection _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = connStr;

            try
            {
                _sqlConnection.Open();
            }
            catch (Exception ex)
            {
                return;
            }
            String str = "select * from " + dbTable + " where jinghao='" + jh + "'";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(str, _sqlConnection);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                return;
            }
            int nRows = dt.Rows.Count;
            if (nRows <= 0)
            {
                SqlCommand comm = new SqlCommand(sql, _sqlConnection);
                comm.ExecuteNonQuery();
                _sqlConnection.Close();
            }
        }

        [WebMethod] //执行一条sql语句，返回多条记录，以";"分隔
        public String getJingInfo(String jinghao)
        {
            if (jinghao == null || jinghao.Equals("")) return null;

            //连接数据库
            SqlConnection _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = connStr;

            try
            {
                _sqlConnection.Open();
            }
            catch (Exception ex)
            {
                return null;
            }

            string rlt = "";
            string sql = "select wanzuanjingshen,wanzuanriqi,wanjingshijian,zuanjingzhouqi,btsj_biaotaoxiashen,qxsj_quxinhuici,jszlsj_zuidajingxie,jszlsj_zuidaweiyi,xtggjsj_gjqk_shuinifanshen from Xls_Zj_Rbb_Tjb where jinghao='" + jinghao + "' order by riqi desc";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(sql, _sqlConnection);
            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                rlt += ";";
            }
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j < (dt.Columns.Count - 1)) rlt += dt.Rows[0][j].ToString() + ",";
                    else rlt += dt.Rows[0][j].ToString() + ";";
                }
            }
            else
            {
                rlt += ";";
            }

            sql = "select shekongriqi,cengwei,jianduren from Xls_Yl_Rbb_Sk where jinghao='" + jinghao + "'";
            da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(sql, _sqlConnection);
            dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                rlt += ",";
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rlt += "射孔日期：" + dt.Rows[0][0].ToString().Split(' ')[0] + "，层位：" + dt.Rows[0][1].ToString() + "，监督人：" + dt.Rows[0][2].ToString() + "；";
                }
                rlt += ",";
            }
            else
            {
                rlt += ",";
            }

            sql = "select shigongriqi,cengwei,shifouhege,shigongduiwu,jianduren from Xls_Yl_Rbb_Ylsg where jinghao='" + jinghao + "'";
            da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(sql, _sqlConnection);
            dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                rlt += ",";
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rlt += "施工日期：" + dt.Rows[0][0].ToString().Split(' ')[0] + "，层位：";
                    rlt += dt.Rows[0][1].ToString() + "，是否合格：";
                    rlt += dt.Rows[0][2].ToString() + "，施工队伍：";
                    rlt += dt.Rows[0][3].ToString() + "，监督人：";
                    rlt += dt.Rows[0][4].ToString() + "；";
                }
                rlt += ",";
                
            }
            else
            {
                rlt += ",";
            }

            sql = "select xiabengriqi from Xls_Yl_Rbb_Xb where jinghao='" + jinghao + "'";
            da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(sql, _sqlConnection);
            dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                rlt += "";
            }
            if (dt.Rows.Count > 0)
            {
                rlt += dt.Rows[0][0].ToString().Split(' ')[0];
            }
            else
            {
                rlt += "";
            }

            _sqlConnection.Close();
            return rlt;
        }

        [WebMethod] //执行一条sql语句，返回多条记录，以";"分隔
        public String getRecord2(String sql)
        {
            if (sql == null || sql.Equals("")) return null;

            //连接数据库
            SqlConnection _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = connStr;

            try
            {
                _sqlConnection.Open();
            }
            catch (Exception ex)
            {
                return null;
            }

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(sql, _sqlConnection);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                return null;
            }
            int nCols = dt.Columns.Count;
            int nRows = dt.Rows.Count;
            if (nCols == 0 || nRows == 0)
            {
                _sqlConnection.Close();
                return null;
            }
            String[] cols = new String[nCols];
            for (int i = 0; i < nCols; i++)
            {
                cols[i] = dt.Columns[i].ColumnName;
            }

            String record = "";
            for (int i = 0; i < nRows; i++)
            {
                DataRow dr = dt.Rows[i];
                for (int j = 0; j < nCols; j++)
                {
                    if (j < (nCols - 1)) record += dr[cols[j]].ToString() + ",";
                    else record += dr[cols[j]].ToString();
                }
                if (i < nRows - 1) record += ";";
            }
            _sqlConnection.Close();
            return record;
        }
        

        [WebMethod] //查询井的状态，用于更新地图符号
        public String getRecord4(String beginDate, string endDate, string qk)
        {
            string sql = null;
            string where = null;
            if (beginDate != null && endDate != null && beginDate != "" && endDate != "")
            {
                where = " where qukuai = '" + qk + "'";
            }
            else
            {
                where = " where qukuai = '" + qk + "'";
            }
            sql = "select jinghao, dangqianzhuangtai from " + dbTable + where;

            SqlConnection _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = connStr;

            try
            {
                _sqlConnection.Open();
            }
            catch (Exception ex)
            {
                return null;
            }

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(sql, _sqlConnection);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                return null;
            }
            int nCols = dt.Columns.Count;
            int nRows = dt.Rows.Count;
            if (nCols == 0 || nRows == 0)
            {
                return null;
            }
            String[] cols = new String[nCols];
            for (int i = 0; i < nCols; i++)
            {
                cols[i] = dt.Columns[i].ColumnName;
            }

            String records = "";
            for (int k = 0; k < nRows; k++)
            {
                string record = "";
                DataRow dr = dt.Rows[k];
                for (int j = 0; j < nCols; j++)
                {
                    if (j < (nCols - 1)) record += dr[cols[j]].ToString() + ",";
                    else record += dr[cols[j]].ToString();
                }
                if (k < nRows - 1) records += record + ";";
                else records += record;
            }
            _sqlConnection.Close();
            return records;
        }

        [WebMethod] //读取Excel中指定的Sheet，返回多条记录，以";"分隔
        public String readExcel(String xls, String sheet)
        {
            string username = "admin";

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

            string uploadPath = HttpContext.Current.Server.MapPath("temp\\") + username + "\\" + xls;
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
                return null;
            }

            DataTable dt = new DataTable(sheet);
            string sql = "SELECT * FROM [" + sheet + "$]";
            OleDbDataAdapter oleAdMaster = new OleDbDataAdapter(sql, oleDBConn);
            try
            {
                oleAdMaster.Fill(dt);
            }
            catch (OleDbException ex)
            {
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();
                return null;
            }
            oleAdMaster.Dispose();
            oleDBConn.Close();
            oleDBConn.Dispose();

            int nCols = dt.Columns.Count;
            int nRows = dt.Rows.Count;
            if (nCols < 13 || nRows == 0)
            {
                return null;
            }
            String[] cols = new String[nCols];
            for (int i = 0; i < nCols; i++)
            {
                cols[i] = dt.Columns[i].ColumnName;
            }

            String record = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                for (int j = 0; j < nCols; j++)
                {
                    if (j < (nCols - 1)) record += dr[cols[j]].ToString() + ",";
                    else record += dr[cols[j]].ToString();
                }
                if (i < dt.Rows.Count - 1) record += ";";
            }
            return record;
        }

        [WebMethod] //按表名数组和字段值数组构造sql语句，并将查询结果按表名分类
        public String[] getRecord(String table, String value, String qk)
        {
            if (table == null || value == null || table.Equals("") || value.Equals(""))
            {
                return null;
            }

            String[] tables = table.Split(',');
            String[] values = value.Split(',');

            //连接数据库
            SqlConnection _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = connStr;
            try
            {
                _sqlConnection.Open();
            }
            catch (Exception ex)
            {
                return null;
            }

            //按表名分类,分成若干个MyTable对象，即：从相同表中查询到的数据保存在同一个MyTable对象中
            List<string> list = new List<string>();
            for (int i = 0; i < tables.Length; i++)
            {
                if (list.IndexOf(tables[i]) == -1)
                    list.Add(tables[i]);
            }
            String[] tmps = list.ToArray();
            MyTable[] myTables = new MyTable[tmps.Length];
            for (int i = 0; i < tmps.Length; i++)
            {
                myTables[i] = new MyTable(tmps[i]);
            }

            //循环查询
            for (int n = 0; n < tables.Length; n++)
            {
                //获取该表名对应的MyTable对象
                int myTableIndex = -1;
                for (int i = 0; i < myTables.Length; i++)
                {
                    if (tables[n].Equals(myTables[i].name))
                    {
                        myTableIndex = i;
                        break;
                    }
                }
                if (myTableIndex == -1) continue;

                //查询
                String sql = "SELECT * FROM " + dbTable + " WHERE qukuai='" + qk + "' and jinghao='" + values[n] + "'";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sql, _sqlConnection);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    continue;
                }
                int nCols = dt.Columns.Count;
                int nRows = dt.Rows.Count;
                if (nCols == 0 || nRows == 0)
                {
                    continue;
                }
                String record = "";
                DataRow dr = dt.Rows[0];
                for (int j = 0; j < nCols; j++)
                {
                    if (j < (nCols - 1)) record += dr[j].ToString() + ",";
                    else record += dr[j].ToString();
                }
                myTables[myTableIndex].records.Add(record);
            }
            _sqlConnection.Close();

            String[] result = new String[myTables.Length];
            for (int i = 0; i < myTables.Length; i++)
            {
                result[i] = myTables[i].GetTable();
            }
            return result;
        }

        [WebMethod]
        public String getLayers()
        {
            //FileInfo fileLog = new FileInfo(Server.MapPath("log.txt"));
            //fileLog.Create();
            //FileStream fs = fileLog.OpenWrite();
            XmlReader reader = XmlReader.Create(Server.MapPath("Layers.xml"));
            try
            {
                string value = "";
                XDocument document = XDocument.Load(reader);
                IEnumerable<XElement> childList = from el in document.Descendants("hancheng") select el;
                foreach (XElement xe in childList)
                {
                    value += xe.Value + ",";
                }
                childList = from el in document.Descendants("linfen") select el;
                foreach (XElement xe in childList)
                {
                    value += xe.Value + ",";
                }
                childList = from el in document.Descendants("xinzhou") select el;
                foreach (XElement xe in childList)
                {
                    value += xe.Value;
                }
                reader.Close();
                //byte[] bts = Encoding.UTF8.GetBytes(value);
                //fs.Write(bts, 0, bts.Length);
                return value;
            }
            catch (System.Exception ex)
            {
                reader.Close();
                return null;
            }
            finally
            {
                //fs.Close();
            }
        }


        [WebMethod]
        public String getZuanjingInfo(String jinghao)
        {
            String fgRow = "^";
            String fgCol = "`";
            string sql = "select * from Xls_Zj_Rbb_Zj where jinghao='" + jinghao + "' order by riqi desc";
            String zj = getData(sql, fgRow, fgCol);
            if (zj == null) zj = "";

            sql = "select * from Xls_Zj_Rbb_Xtg where jinghao='" + jinghao + "' order by riqi desc";
            String xtg = getData(sql, fgRow, fgCol);
            if (xtg == null) xtg = "";

            sql = "select * from Xls_Zj_Rbb_Gj where jinghao='" + jinghao + "' order by riqi desc";
            String gj = getData(sql, fgRow, fgCol);
            if (gj == null) gj = "";

            sql = "select * from Xls_Zj_Rbb_Wj where jinghao='" + jinghao + "' order by riqi desc";
            String wj = getData(sql, fgRow, fgCol);
            if (wj == null) wj = "";

            return zj + "|" + xtg + "|" + gj + "|" + wj;
        }

        [WebMethod]
        public String getYalieInfo(String jh)
        {
            String fgRow = "^";
            String fgCol = "`";
            string sql = "select * from Xls_Yl_Rbb_Sk where jinghao='" + jh + "'";
            string sk = getData(sql, fgRow, fgCol);
            if (sk == null) sk = "";

            sql = "select * from Xls_Yl_Rbb_Ylsg where jinghao='" + jh + "'";
            string ylsg = getData(sql, fgRow, fgCol);
            if (ylsg == null) ylsg = "";

            sql = "select * from Xls_Yl_Rbb_Xb where jinghao='" + jh + "'";
            string xb = getData(sql, fgRow, fgCol);
            if (xb == null) xb = "";

            sql = "select * from Xls_Yl_Rbb_Sbyysm where jinghao='" + jh + "'";
            string sbyy = getData(sql, fgRow, fgCol);
            if (sbyy == null) sbyy = "";

            return sk + "|" + ylsg + "|" + xb + "|" + sbyy;
        }

        private String getData(String sql, String fgRow, String fgCol)
        {
            if (sql == null || sql.Equals("")) return null;

            SqlConnection _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = connStr;

            try
            {
                _sqlConnection.Open();
            }
            catch (Exception ex)
            {
                return null;
            }

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(sql, _sqlConnection);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                return null;
            }
            int nCols = dt.Columns.Count;
            int nRows = dt.Rows.Count;
            if (nCols == 0 || nRows == 0)
            {
                _sqlConnection.Close();
                return null;
            }
            String[] cols = new String[nCols];
            for (int i = 0; i < nCols; i++)
            {
                cols[i] = dt.Columns[i].ColumnName;
            }

            String record = "";
            for (int i = 0; i < nRows; i++)
            {
                DataRow dr = dt.Rows[i];
                for (int j = 0; j < nCols; j++)
                {
                    if (j < (nCols - 1)) record += dr[cols[j]].ToString() + fgCol;
                    else record += dr[cols[j]].ToString();
                }
                if (i < nRows - 1) record += fgRow;
            }
            _sqlConnection.Close();
            return record;
        }
    }

    class MyTable
    {
        public String name;
        public List<String> records;//','分隔

        public MyTable(String name)
        {
            this.name = name;
            records = new List<string>();
        }

        public String GetTable()
        {
            if (records.Count == 0) return null;
            String str = name;
            for (int i = 0; i < records.Count; i++)
            {
                str += ";" + records[i];
            }
            return str;
        }
    }
}
