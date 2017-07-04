using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace NCCBM
{
    public class DataBaseHelper
    {
        private static string conStr = ConfigurationManager.ConnectionStrings["_NCCBM"].ConnectionString;

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable query(string sql)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand com = con.CreateCommand();
            SqlDataAdapter ada = new SqlDataAdapter(com);
            com.CommandText = sql;
            con.Open();
            try
            {
                ada.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        /// <summary>
        /// 使用事务执行sql
        /// </summary>
        /// <param name="sql"></param>
        public static void executeWithTrans(string sql)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand com = con.CreateCommand();
            com.CommandText = sql;
            con.Open();
            com.Transaction = con.BeginTransaction();
            try
            {
                com.ExecuteNonQuery();
                com.Transaction.Commit();
            }
            catch (Exception e)
            {                
                com.Transaction.Rollback();
                throw e;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// 尝试sql执行是否正确
        /// </summary>
        /// <param name="sql"></param>
        public static void tryToExecute(string sql)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand com = con.CreateCommand();
            com.CommandText = sql;
            con.Open();
            com.Transaction = con.BeginTransaction();
            try
            {
                com.ExecuteNonQuery();
                com.Transaction.Rollback();
            }
            catch (Exception e)
            {
                com.Transaction.Rollback();
                throw e;
            }
            finally
            {
                con.Close();
            }
        }
            

        /// <summary>
        /// 直接执行sql
        /// </summary>
        /// <param name="sql"></param>
        public static void execute(string sql)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand com = con.CreateCommand();
            com.CommandText = sql;
            con.Open();
            try
            {
                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }


        /// <summary>
        /// 直接执行sql
        /// </summary>
        /// <param name="sql"></param>
        public static object count(string sql)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand com = con.CreateCommand();
            com.CommandText = sql;
            con.Open();
            object result = null;
            try
            {
                result = com.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public static int tryExecute2(List<string> sqls)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            SqlCommand com = con.CreateCommand();
            com.Transaction = con.BeginTransaction();
            int count = 0;
            for (int i = 0; i < sqls.Count; i++)
            {
                com.CommandText = sqls[i];
                try
                {
                    com.ExecuteNonQuery();
                    count++;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            com.Transaction.Rollback();
            con.Close();
            return count;
        }


        public static int execute2(List<string> sqls)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            SqlCommand com = con.CreateCommand();
            com.Transaction = con.BeginTransaction();
            int count = 0;
            for (int i = 0; i < sqls.Count; i++)
            {
                com.CommandText = sqls[i];
                try
                {
                    com.ExecuteNonQuery();
                    count++;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            if (com.Transaction != null) com.Transaction.Commit();
            con.Close();
            return count;
        }

        public static int ExecuteNonQuery(string sql, Dictionary<string, object> dic)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand com = con.CreateCommand();
            com.CommandText = sql;
            con.Open();
            int result;
            foreach (string s in dic.Keys)
            {
                com.Parameters.Add(new SqlParameter(s, dic[s]));
            }
            try
            {
                result = com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
    }
}