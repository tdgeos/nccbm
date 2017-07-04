using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using Microsoft.Office.Core;
using System.Reflection;
using System.Net;
//using Excel = Microsoft.Office.Interop.Excel;


namespace NCCBM.Import
{
    public class TableImport
    {
        public static string getDate(string filename)
        {
            if (filename == null || filename == "") return null;
            string[] tmps = filename.Split('.');
            if (tmps == null || tmps.Length < 2) return null;
            if (tmps[0] == null || tmps[0].Trim() == "") return null;
            tmps = tmps[0].Split('-');
            if (tmps == null || tmps.Length < 4) return null;
            int c = tmps.Length;
            string date = tmps[c - 3] + "-" + tmps[c - 2] + "-" + tmps[c - 1];
            try
            {
                DateTime dt = DateTime.Parse(date);
                return date;
            }
            catch (System.FormatException fe)
            {
                return null;
            }
        }

        public static string getPlace(string filename)
        {
            if (filename.IndexOf("韩城") != -1)
            {
                return "韩城";
            }
            if (filename.IndexOf("临汾") != -1)
            {
                return "临汾";
            }
            if (filename.IndexOf("忻州") != -1 || filename.IndexOf("保德") != -1)
            {
                return "忻州";
            }
            return null;
        }

        public static void PrintTable(DataTable dt)
        {
            int cols = dt.Columns.Count;
            int rows = dt.Rows.Count;
            FileInfo f = new FileInfo("E:/work/wei/log.txt");
            StreamWriter w = f.CreateText();
            for (int i = 0; i < cols; i++)
            {
                if (i < cols - 1) w.Write(dt.Columns[i].ColumnName + ",");
                else w.WriteLine(dt.Columns[i].ColumnName);
                w.Flush();
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (j < cols - 1) w.Write(dt.Rows[i][dt.Columns[j]] + ",");
                    else w.WriteLine(dt.Rows[i][dt.Columns[j]]);
                    w.Flush();
                }
            }
            w.Close();
        }

        public static DataTable ReadExcel(string fileName, string strSheet)
        {
            if (fileName == null || fileName == "") return null;
            if (strSheet == null || strSheet == "") return null;

            string path = HttpContext.Current.Server.MapPath(MyTools.tmpRibaoDir);
            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                             "Data Source=" + path + fileName +
                             ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
            OleDbConnection oleDBConn = new OleDbConnection(connStr);
            try
            {
                oleDBConn.Open();
            }
            catch (System.Data.OleDb.OleDbException oe)
            {
                return null;
            }

            DataTable dt = new DataTable();
            string sql = "SELECT * FROM [" + strSheet + "$]";
            OleDbDataAdapter oleAdMaster = new OleDbDataAdapter(sql, oleDBConn);
            try
            {
                oleAdMaster.Fill(dt);
            }
            catch (OleDbException ex)
            {
                return null;
            }
            finally
            {
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();
            }
            return dt;
        }

        public static string[] GetExcelSheets(string fileName)
        {
            if (fileName == null || fileName == "") return null;

            string path = HttpContext.Current.Server.MapPath(MyTools.tmpRibaoDir);
            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                             "Data Source=" + path + fileName +
                             ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
            OleDbConnection oleDBConn = new OleDbConnection(connStr);
            try
            {
                oleDBConn.Open();
            }
            catch (System.Data.OleDb.OleDbException oe)
            {
                return null;
            }

            DataTable dt = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            if (dt != null && dt.Rows.Count > 0)
            {
                int count = dt.Rows.Count;
                string[] sheets = new string[count];
                for(int i=0;i<count;i++)
                {
                    sheets[i] = dt.Rows[i]["TABLE_NAME"].ToString();
                }
                oleDBConn.Close();
                oleDBConn.Dispose();
                return sheets;
            }
            oleDBConn.Close();
            oleDBConn.Dispose();
            return null;
        }

        public static DataTable[] SplitTable(DataTable dt)
        {
            DataTable[] dt_col = new DataTable[4];
            DataTable temp = null;
            DataRow dr;
            DataRow copy_temp = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                if ("钻进".Equals(dr[0].ToString().Trim()))
                {
                    temp = dt.Clone();
                }
                if ("下套管".Equals(dr[0].ToString().Trim()))
                {
                    dt_col[0] = temp;
                    temp = dt.Clone();
                }
                if ("固井".Equals(dr[0].ToString().Trim()))
                {
                    dt_col[1] = temp;
                    temp = dt.Clone();
                }
                if ("完井".Equals(dr[0].ToString().Trim()))
                {
                    dt_col[2] = temp;
                    temp = dt.Clone();
                }
                if ("工况总结".Equals(dr[0].ToString().Trim()))
                {
                    break;
                }
                if (temp != null)
                {
                    copy_temp = temp.NewRow();
                    copy_temp.ItemArray = dr.ItemArray;
                    temp.Rows.Add(copy_temp);
                }
            }
            dt_col[3] = temp;
            return dt_col;
        }

        public static void Edit_Zjjdrbb_Zj(DataTable dt, string place, string date)
        {
            int cols = dt.Columns.Count;
            for (int i = 0; i < cols; i++)
            {
                dt.Columns[i].ColumnName = "F" + i;
            }

            DataColumn dc3 = new DataColumn("fujian");
            dt.Columns.Add(dc3);

            DataColumn dc1 = new DataColumn("place");
            DataColumn dc2 = new DataColumn("date");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jh = dt.Rows[i]["F3"].ToString().Trim();
                if (jh == "") dt.Rows.RemoveAt(i--);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jh = dt.Rows[i]["F3"].ToString().Trim();
                dt.Rows[i]["F3"] = jh.Split('井')[0];

                correctDate(dt, "F4", i);
                correctNumber(dt, "F5", i);
                correctNumber(dt, "F6", i);
                correctNumber(dt, "F7", i);

                dt.Rows[i]["place"] = place;
                dt.Rows[i]["date"] = date;
                dt.Rows[i]["fujian"] = "";
            }
            dt.Columns.Remove("F0");
        }

        public static void Edit_Zjjdrbb_Xtg(DataTable dt, string place, string date)
        {
            int cols = dt.Columns.Count;
            for (int i = 0; i < cols; i++)
            {
                dt.Columns[i].ColumnName = "F" + i;
            }

            DataColumn dc3 = new DataColumn("fujian");
            dt.Columns.Add(dc3);

            DataColumn dc1 = new DataColumn("place");
            DataColumn dc2 = new DataColumn("date");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jh = dt.Rows[i]["F3"].ToString().Trim();
                dt.Rows[i]["F3"] = jh.Split('井')[0];

                correctDate(dt, "F5", i);
                correctNumber(dt, "F4", i);
                correctNumber(dt, "F6", i);

                dt.Rows[i]["place"] = place;
                dt.Rows[i]["date"] = date;
                dt.Rows[i]["fujian"] = "";
            }
            dt.Columns.Remove("F0");
        }

        public static void Edit_Zjjdrbb_Gj(DataTable dt, string place, string date)
        {
            int cols = dt.Columns.Count;
            for (int i = 0; i < cols; i++)
            {
                dt.Columns[i].ColumnName = "F" + i;
            }

            DataColumn dc3 = new DataColumn("fujian");
            dt.Columns.Add(dc3);

            DataColumn dc1 = new DataColumn("place");
            DataColumn dc2 = new DataColumn("date");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jh = dt.Rows[i]["F3"].ToString().Trim();
                dt.Rows[i]["F3"] = jh.Split('井')[0];

                correctDate(dt, "F5", i);

                correctNumber(dt, "F4", i);

                dt.Rows[i]["place"] = place;
                dt.Rows[i]["date"] = date;
                dt.Rows[i]["fujian"] = "";
            }
            dt.Columns.Remove("F0");
            dt.Columns.Remove("F11");
        }

        public static void Edit_Zjjdrbb_Wj(DataTable dt, string place, string date)
        {
            int cols = dt.Columns.Count;
            for (int i = 0; i < cols; i++)
            {
                dt.Columns[i].ColumnName = "F" + i;
            }

            DataColumn dc3 = new DataColumn("fujian");
            dt.Columns.Add(dc3);

            DataColumn dc1 = new DataColumn("place");
            DataColumn dc2 = new DataColumn("date");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jh = dt.Rows[i]["F3"].ToString().Trim();
                dt.Rows[i]["F3"] = jh.Split('井')[0];

                correctNumber(dt, "F16", i);

                dt.Rows[i]["place"] = place;
                dt.Rows[i]["date"] = date;
                dt.Rows[i]["fujian"] = "";
            }
            dt.Columns.Remove("F0");
        }

        public static void Edit_Zjjdrbb_Scjwjtjb(DataTable dt, string place, string date)
        {
            int cols = dt.Columns.Count;
            for (int i = 0; i < cols; i++)
            {
                dt.Columns[i].ColumnName = "F" + i;
            }

            DataColumn dc1 = new DataColumn("place");
            DataColumn dc2 = new DataColumn("date");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jh = dt.Rows[i]["F3"].ToString().Trim();
                dt.Rows[i]["F3"] = jh.Split('井')[0];

                correctNumber(dt, "F4", i);
                correctNumber(dt, "F8", i);
                correctNumber(dt, "F17", i);
                correctNumber(dt, "F21", i);
                correctInt(dt, "F23", i);
                correctInt(dt, "F24", i);
                correctInt(dt, "F25", i);
                correctInt(dt, "F26", i);
                correctInt(dt, "F28", i);
                correctNumber(dt, "F52", i);
                correctNumber(dt, "F53", i);
                correctNumber(dt, "F54", i);
                correctNumber(dt, "F59", i);
                correctNumber(dt, "F60", i);
                correctNumber(dt, "F63", i);
                correctNumber(dt, "F64", i);
                correctNumber(dt, "F66", i);
                correctNumber(dt, "F72", i);
                correctNumber(dt, "F77", i);
                correctNumber(dt, "F78", i);
                correctNumber(dt, "F79", i);
                correctNumber(dt, "F81", i);
                correctNumber(dt, "F82", i);
                correctNumber(dt, "F89", i);

                correctDate(dt, "F5", i);
                correctDate(dt, "F6", i);
                correctDate(dt, "F7", i);
                correctDate(dt, "F9", i);
                correctDate(dt, "F13", i);
                correctDate(dt, "F35", i);
                correctDate(dt, "F42", i);

                correctHuanhang(dt, "F46", i);
                correctHuanhang(dt, "F47", i);
                correctHuanhang(dt, "F48", i);
                correctHuanhang(dt, "F49", i);
                correctHuanhang(dt, "F54", i);
                correctHuanhang(dt, "F56", i);
                correctHuanhang(dt, "F64", i);

                dt.Rows[i]["place"] = place;
                dt.Rows[i]["date"] = date;
            }

            dt.Columns.Add("zuanji");
            dt.Columns["zuanji"].SetOrdinal(9);

            dt.Columns.Remove("F0");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sql = "select id from Xls_Zj_Rbb_Tjb where jinghao='" + dt.Rows[i]["F3"] + "'";
                DataTable dtTemp = DataBaseHelper.query(sql);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    string id = dtTemp.Rows[0][0].ToString();
                    sql = "delete from Xls_Zj_Rbb_Tjb where id='" + id + "'";
                    DataBaseHelper.execute(sql);
                }
            }
        }

        public static void Edit_Ylsgrbb_Sk(DataTable dt, string place, string date)
        {
            for (int j = dt.Columns.Count; j > 0; j--)
            {
                string str1 = dt.Rows[0][j - 1].ToString().Trim();
                if (str1 == "") dt.Columns.RemoveAt(j - 1);
                else break;
            }

            int cols = dt.Columns.Count;
            for (int i = 0; i < cols; i++)
            {
                Type type1 = dt.Columns[i].DataType;
                dt.Columns[i].ColumnName = "F" + i;
            }

            DataColumn dc1 = new DataColumn("place");
            DataColumn dc2 = new DataColumn("date");
            DataColumn fj = new DataColumn("fujian");
            dt.Columns.Add(fj);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            dt.Rows.RemoveAt(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jh = dt.Rows[i]["F0"].ToString().Trim();
                dt.Rows[i]["F0"] = jh.Split('井')[0];

                correctDate(dt, "F2", i);

                correctHuanhang(dt, "F3", i);
                correctHuanhang(dt, "F4", i);
                correctHuanhang(dt, "F5", i);
                correctHuanhang(dt, "F6", i);

                dt.Rows[i]["place"] = place;
                dt.Rows[i]["date"] = date;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sql = "select id from Xls_Yl_Rbb_Sk where jinghao='" + dt.Rows[i]["F0"] + "' and cengwei='" + dt.Rows[i]["F1"] + "' and shekongriqi='" + dt.Rows[i]["F2"] + "'";
                DataTable dtTemp = DataBaseHelper.query(sql);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void Edit_Ylsgrbb_Yqjc(DataTable dt, string place, string date)
        {
            for (int j = dt.Columns.Count; j > 0; j--)
            {
                string str1 = dt.Rows[0][j - 1].ToString().Trim();
                if (str1 == "") dt.Columns.RemoveAt(j - 1);
                else break;
            }

            int cols = dt.Columns.Count;
            for (int i = 0; i < cols; i++)
            {
                dt.Columns[i].ColumnName = "F" + i;
            }

            DataColumn fj = new DataColumn("fujian");
            DataColumn dc1 = new DataColumn("place");
            DataColumn dc2 = new DataColumn("date");
            dt.Columns.Add(fj);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jh = dt.Rows[i]["F0"].ToString().Trim();
                string[] ss = jh.Split('井');
                dt.Rows[i]["F0"] = ss[0];

                correctNumber(dt, "F2", i);
                correctNumber(dt, "F3", i);
                correctNumber(dt, "F4", i);
                correctNumber(dt, "F5", i);
                correctNumber(dt, "F6", i);
                correctNumber(dt, "F7", i);
                correctNumber(dt, "F8", i);
                correctNumber(dt, "F9", i);

                correctDate(dt, "F17", i);

                dt.Rows[i]["place"] = place;
                dt.Rows[i]["date"] = date;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sql = "select id from Xls_Yl_Rbb_Yqjc where jinghao='" + dt.Rows[i]["F0"] + "' and cengwei='" + dt.Rows[i]["F1"] + "'";
                DataTable dtTemp = DataBaseHelper.query(sql);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void Edit_Ylsgrbb_Ylsg(DataTable dt, string place, string date)
        {
            for (int j = dt.Columns.Count; j > 0; j--)
            {
                string str1 = dt.Rows[0][j - 1].ToString().Trim();
                if (str1 == "") dt.Columns.RemoveAt(j - 1);
                else break;
            }

            int cols = dt.Columns.Count;
            for (int i = 0; i < cols; i++)
            {
                dt.Columns[i].ColumnName = "F" + i;
            }

            DataColumn dc1 = new DataColumn("place");
            DataColumn dc2 = new DataColumn("date");
            DataColumn fj = new DataColumn("fujian");
            dt.Columns.Add(fj);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            dt.Rows.RemoveAt(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jh = dt.Rows[i]["F0"].ToString().Trim();
                dt.Rows[i]["F0"] = jh.Split('井')[0];

                correctDate(dt, "F2", i);

                correctHuanhang(dt, "F3", i);
                correctHuanhang(dt, "F4", i);
                correctHuanhang(dt, "F5", i);
                correctHuanhang(dt, "F6", i);
                correctHuanhang(dt, "F7", i);
                correctHuanhang(dt, "F8", i);
                correctHuanhang(dt, "F10", i);
                correctHuanhang(dt, "F11", i);
                correctHuanhang(dt, "F12", i);
                correctHuanhang(dt, "F13", i);
                correctHuanhang(dt, "F18", i);
                correctHuanhang(dt, "F19", i);

                dt.Rows[i]["place"] = place;
                dt.Rows[i]["date"] = date;
            }
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sql = "select id from Xls_Yl_Rbb_Ylsg where jinghao='" + dt.Rows[i]["F0"] + "' and cengwei='" + dt.Rows[i]["F1"] + "' and shigongriqi='" + dt.Rows[i]["F2"] + "' and qzy_shiji='" + dt.Rows[i]["F11"] + "'";
                DataTable dtTemp = DataBaseHelper.query(sql);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void Edit_Ylsgrbb_Sbyysm(DataTable dt, string place, string date)
        {
            int cols = dt.Columns.Count;
            for (int i = 0; i < cols; i++)
            {
                dt.Columns[i].ColumnName = "F" + i;
            }

            DataColumn dc1 = new DataColumn("place");
            DataColumn dc2 = new DataColumn("date");
            DataColumn fj = new DataColumn("fujian");
            dt.Columns.Add(fj);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                correctDate(dt, "F3", i);

                dt.Rows[i]["place"] = place;
                dt.Rows[i]["date"] = date;
            }
            dt.Columns.Remove("F0");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sql = "select id from Xls_Yl_Rbb_Sbyysm where jinghao='" + dt.Rows[i]["F1"] + "' and cenghao='" + dt.Rows[i]["F2"] + "'";
                DataTable dtTemp = DataBaseHelper.query(sql);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(i);
                    i--;
                }
            }
        }

        public static bool isRowNull(DataRow dr, int cols)
        {
            bool b = true;
            for (int i = 0; i < cols; i++)
            {
                if (dr[i].ToString().Trim() != "")
                {
                    b = false;
                    break;
                }
            }
            return b;
        }

        public static List<string> CreateSqls(DataTable dt, string tablename)
        {
            int cols = dt.Columns.Count;
            int rows = dt.Rows.Count;
            List<string> sqls = new List<string>();
            DataRow dr;
            for (int i = 0; i < rows; i++)
            {
                dr = dt.Rows[i];
                if (isRowNull(dr, cols - 3)) continue;
                string sql = "insert into " + tablename + " values(";
                for (int j = 0; j < cols; j++)
                {
                    String value = null;
                    Type type = dt.Columns[j].DataType;
                    if (type.Name == "DateTime")
                    {
                        if (dr[j].ToString() != "")
                        {
                            DateTime datetime = (DateTime)dr[j];
                            value = "'" + datetime.ToString("yyyy-MM-dd") + "'";
                        }
                        else
                        {
                            value = "NULL";
                        }
                    }
                    else
                    {
                        value = dr[j].ToString();
                        if (value == null || value == "") value = "NULL";
                        else value = "'" + value + "'";
                    }
                    if (j < cols - 1) sql += value + ",";
                    else sql += value + ")";
                }
                sqls.Add(sql);
            }
            return sqls;
        }

        public static int Import(List<string> sqls)
        {
            return DataBaseHelper.execute2(sqls);
        }

        public static void addLog(string date, string place, string table, string usr, int num, int tag)
        {
            string sql = "insert into T_ImportLog values ('" + date + "','" + place + "','" + table + "','" + usr + "','" + num + "','" + tag + "')";
            DataBaseHelper.execute(sql);
        }

        public static bool existLog(string date, string place, string table)
        {
            string sql = "select COUNT(*) from T_ImportLog where riqi='" + date + "' and name='" + table + "' and qukuai='" + place + "'";
            int count = (int)DataBaseHelper.count(sql);
            if (count == 0) return false;
            else return true;
        }

        public static String GetUnitedDate(String str)
        {
            if (str == null || str == "") return null;
            str = str.Trim();
            string[] sa1 = str.Split('.');
            if (sa1.Length == 3)
            {
                try
                {
                    int y = Int32.Parse(sa1[0]);
                    int m = Int32.Parse(sa1[1]);
                    int d = Int32.Parse(sa1[2]);
                    return y + "-" + m + "-" + d;
                }
                catch (System.Exception ex)
                {
                    return null;
                }
            }
            sa1 = str.Split('/');
            if (sa1.Length == 3)
            {
                try
                {
                    int y = Int32.Parse(sa1[0]);
                    int m = Int32.Parse(sa1[1]);
                    int d = Int32.Parse(sa1[2]);
                    return y + "-" + m + "-" + d;
                }
                catch (System.Exception ex)
                {
                    return null;
                }
            }
            sa1 = str.Split('-');
            if (sa1.Length == 3)
            {
                try
                {
                    int y = Int32.Parse(sa1[0]);
                    int m = Int32.Parse(sa1[1]);
                    int d = Int32.Parse(sa1[2]);
                    return y + "-" + m + "-" + d;
                }
                catch (System.Exception ex)
                {
                    return null;
                }
            }
            if (sa1.Length == 2)
            {
                if (sa1[0].IndexOf("—") != -1)
                {
                    string[] ss = sa1[0].Split('—');
                    if (ss.Length == 2)
                    {
                        int y = Int32.Parse(ss[0]);
                        int m = Int32.Parse(ss[1]);
                        int d = Int32.Parse(sa1[1]);
                        return y + "-" + m + "-" + d;
                    }
                }
                if (sa1[1].IndexOf("—") != -1)
                {
                    string[] ss = sa1[1].Split('—');
                    if (ss.Length == 2)
                    {
                        int y = Int32.Parse(sa1[0]);
                        int m = Int32.Parse(ss[0]);
                        int d = Int32.Parse(ss[1]);
                        return y + "-" + m + "-" + d;
                    }
                }
            }
            sa1 = str.Split('—');
            if (sa1.Length == 3)
            {
                try
                {
                    int y = Int32.Parse(sa1[0]);
                    int m = Int32.Parse(sa1[1]);
                    int d = Int32.Parse(sa1[2]);
                    return y + "-" + m + "-" + d;
                }
                catch (System.Exception ex)
                {
                    return null;
                }
            }
            sa1 = str.Split('、');
            if (sa1.Length == 3)
            {
                try
                {
                    int y = Int32.Parse(sa1[0]);
                    int m = Int32.Parse(sa1[1]);
                    int d = Int32.Parse(sa1[2]);
                    return y + "-" + m + "-" + d;
                }
                catch (System.Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public static void correctDate(DataTable dt, string field, int rows)
        {
            String strDate = dt.Rows[rows][field].ToString().Trim();
            Type type = dt.Columns[field].DataType;
            if (type.Name != "DateTime")
            {
                if (strDate.Length > 4 && strDate[4] != '/' && strDate[4] != '.' && strDate[4] != '-' && strDate[4] != '—' 
                    && strDate[4] != '、')
                {
                    try
                    {
                        int tmp = Int32.Parse(strDate);
                        strDate = DateTime.FromOADate(tmp).ToString("d");
                        String str = TableImport.GetUnitedDate(strDate);
                        if (str != null && str != "") dt.Rows[rows][field] = str;
                        else dt.Rows[rows][field] = "";
                    }
                    catch (System.FormatException fe)
                    {
                        String str = TableImport.GetUnitedDate(strDate);
                        if (str != null && str != "") dt.Rows[rows][field] = str;
                        else dt.Rows[rows][field] = "";
                    }

                }
                else
                {
                    if (strDate != null && strDate != "")
                    {
                        String str = TableImport.GetUnitedDate(strDate);
                        if (str != null && str != "") dt.Rows[rows][field] = str;
                        else dt.Rows[rows][field] = "";
                    }
                }
            }
        }

        public static void correctNumber(DataTable dt, string field, int rows)
        {
            string str = dt.Rows[rows][field].ToString();
            if (str != null && str != "")
            {
                double d = 0.0;
                try
                {
                    d = Double.Parse(str);
                    Math.Round(d, 2);
                    str = d.ToString("f2");
                    dt.Rows[rows][field] = str;
                }
                catch (System.FormatException fe)
                {
                    dt.Rows[rows][field] = null;
                }
            }
        }

        public static void correctInt(DataTable dt, string field, int rows)
        {
            string str = dt.Rows[rows][field].ToString();
            if (str != null && str != "")
            {
                int d = 0;
                try
                {
                    d = Int32.Parse(str);
                    str = d.ToString();
                    dt.Rows[rows][field] = str;
                }
                catch (System.FormatException fe)
                {
                    dt.Rows[rows][field] = null;
                }
            }
        }

        public static void correctHuanhang(DataTable dt, string field, int rows)
        {
            string str = dt.Rows[rows][field].ToString();
            if (str != null && str != "")
            {
                string[] tmps = str.Split('\n');
                string value = "";
                if (tmps.Length > 1)
                {
                    for (int j = 0; j < tmps.Length - 1; j++)
                    {
                        value += tmps[j].Trim() + ',';
                    }
                    value += tmps[tmps.Length - 1].Trim();
                    dt.Rows[rows][field] = value;
                }
            }
        }

        /*
        public static void updateJichuxinxi()
        {
            string sql = "update jing_jichuxinxi set dangqianzhuangtai=NULL where 1=1";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='交井' where jinghao in (select jinghao from Xls_Yl_Rbb_Xb where yanshouqingkuang like '%已验收%')";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='下泵' where jinghao in (select jinghao from Xls_Yl_Rbb_Xb where shigongneirong like '%下泵%') and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='已压裂' where (jinghao in (select jinghao from Xls_Yl_Rbb_Ylsg where shifouyawan like '%是%') or jinghao in (select jinghao from Xls_Yl_Rbb_Yhpy)) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='未压裂' where (jinghao in (select jinghao from Xls_Yl_Rbb_Ylsg) or jinghao in (select jinghao from Xls_Yl_Rbb_Yqjc) or jinghao in (select jinghao from Xls_Yl_Rbb_Sk)) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='完井' where (jinghao in (select jinghao from Xls_Zj_Rbb_Wj) or jinghao in (select jinghao from Xls_Zj_Rbb_Tjb)) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='固井' where jinghao in (select jinghao from Xls_Zj_Rbb_Gj) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='下套管' where jinghao in (select jinghao from Xls_Zj_Rbb_Xtg) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);

            sql = "update jing_jichuxinxi set dangqianzhuangtai='钻进' where jinghao in (select jinghao from Xls_Zj_Rbb_Zj) and dangqianzhuangtai is NULL";
            DataBaseHelper.execute(sql);
        }
        */
    }
}