using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Configuration;

namespace NCCBM
{
    public class MyTools
    {
        public static string tmpRibaoDir = ConfigurationSettings.AppSettings["TempRibaoDir"].ToString();
        public static string tmpFujianDir = ConfigurationSettings.AppSettings["TempFujianDir"].ToString();
        public static int maxLoadSize = Int32.Parse(ConfigurationSettings.AppSettings["uploadMaxSize"].ToString());

        public static string ftpServerIp = ConfigurationSettings.AppSettings["ftpServerIp"].ToString();
        public static string ftpUserName = ConfigurationSettings.AppSettings["ftpUserName"].ToString();
        public static string ftpUserPwd = ConfigurationSettings.AppSettings["ftpUserPwd"].ToString();

        public static string GetWeek(String strRiqi)
        {
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);
            DateTime riqi = new DateTime(y, m, d);
            DateTime begin = new DateTime();
            DateTime end = new DateTime();
            if (riqi.DayOfWeek == DayOfWeek.Saturday)
            {
                begin = riqi;
                end = riqi.AddDays(6);
            }
            if (riqi.DayOfWeek == DayOfWeek.Sunday)
            {
                begin = riqi.AddDays(-1);
                end = riqi.AddDays(5);
            }
            if (riqi.DayOfWeek == DayOfWeek.Monday)
            {
                begin = riqi.AddDays(-2);
                end = riqi.AddDays(4);
            }
            if (riqi.DayOfWeek == DayOfWeek.Tuesday)
            {
                begin = riqi.AddDays(-3);
                end = riqi.AddDays(3);
            }
            if (riqi.DayOfWeek == DayOfWeek.Wednesday)
            {
                begin = riqi.AddDays(-4);
                end = riqi.AddDays(2);
            }
            if (riqi.DayOfWeek == DayOfWeek.Thursday)
            {
                begin = riqi.AddDays(-5);
                end = riqi.AddDays(1);
            }
            if (riqi.DayOfWeek == DayOfWeek.Friday)
            {
                begin = riqi.AddDays(-6);
                end = riqi;
            }
            if (end > DateTime.Now) end = DateTime.Now;
            return begin.ToString("yyyy-MM-dd") + "," + end.ToString("yyyy-MM-dd");
        }

        public static int GetMonthDays(String strRiqi)
        {
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);

            if (m == 1 || m == 3 || m == 5 || m == 7 || m == 8 || m == 10 || m == 12) return 31;
            if (m == 2)
            {
                if (y % 4 == 0 || y % 100 == 0)
                {
                    return 29;
                }
                else
                {
                    return 28;
                }
            }
            if (m == 4 || m == 6 || m == 9 || m == 11) return 30;
            return 0;
        }

        public static DateTime GetQuarterBegin(String strRiqi)
        {
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);

            if (m == 1 || m == 2 || m == 3) return new DateTime(y, 1, 1);
            if (m == 4 || m == 5 || m == 6) return new DateTime(y, 4, 1);
            if (m == 7 || m == 8 || m == 9) return new DateTime(y, 7, 1);
            if (m == 10 || m == 11 || m == 12) return new DateTime(y, 10, 1);
            return new DateTime(y, 1, 1);
        }

        public static DateTime GetQuarterEnd(String strRiqi)
        {
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);

            if (m == 1 || m == 2 || m == 3) return new DateTime(y, 3, 31);
            if (m == 4 || m == 5 || m == 6) return new DateTime(y, 6, 30);
            if (m == 7 || m == 8 || m == 9) return new DateTime(y, 9, 30);
            if (m == 10 || m == 11 || m == 12) return new DateTime(y, 12, 31);
            return new DateTime(y, 12, 31);
        }

        public static void MessageBox(System.Web.UI.Page page, string msg)
        {
            page.Response.Write("<script language='JavaScript'>window.alert('" + msg + "'); </script>");
        }

        public static string GetNumberDate(string d)
        {
            string[] ss = d.Split('-');
            if (ss.Length != 3) return null;
            string sy = ss[0];
            string sm = ss[1];
            string sd = ss[2];
            if (sm.Length == 1) sm = "0" + sm;
            if (sd.Length == 1) sd = "0" + sd;
            return sy + sm + sd;
        }

        public static string GetParentPath(string path)
        {
            if (path == null || path == "") return null;
            if (path == "/") return null;
            string[] ss = path.Split('/');
            if (ss.Length == 1) return null;
            string str = "";
            for (int i = 0; i < ss.Length - 1; i++)
            {
                if (i == 0) str = ss[0];
                else str += "/" + ss[i];
            }
            return str;
        }

        public static void DeleteWebFile(string filePath)
        {
            try
            {
                //if (File.Exists(filePath))
                //{
                //    File.Delete(filePath);
                //}
                FileInfo fi = new FileInfo(filePath);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void FujianPaixu(List<string> fjs)
        {
            if (fjs == null) return;
            if (fjs.Count == 0) return;
            for (int i = 0; i < fjs.Count; i++)
            {
                string tmp = fjs[i];
                int index = -1;
                for (int j = i+1; j < fjs.Count; j++)
                {
                    DateTime dtTmp = GetFujianDate(tmp);
                    string fj = fjs[j];
                    DateTime dt = GetFujianDate(fj);
                    if (dtTmp < dt)
                    {
                        tmp = fj;
                        index = j;
                    }
                }
                if (index != -1)
                {
                    fjs[index] = fjs[i];
                    fjs[i] = tmp;
                }
            }
        }

        public static DateTime GetFujianDate(string fj)
        {
            string[] ss = fj.Split('_');
            if (ss.Length == 1) return new DateTime(1900, 1, 1);

            string s = ss[1];
            string[] dd = s.Split('-');
            if (dd.Length != 3) return new DateTime(1900, 1, 1);
            int y = 0;
            int m = 0;
            int d = 0;
            try
            {
                y = Int32.Parse(dd[0]);
                m = Int32.Parse(dd[1]);
                d = Int32.Parse(dd[2].Split('.')[0]);
            }
            catch(Exception e)
            {

            }
            if (y == 0 || m == 0 || d == 0) return new DateTime(1900, 1, 1);
            else return new DateTime(y, m, d);
        }

        public static bool IsFtpdirExists(string path)
        {
            string parent = MyTools.GetParentPath(path);
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(parent));
            reqFTP.Credentials = new NetworkCredential(ftpUserName, ftpUserPwd);
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
            reqFTP.UseBinary = true;
            reqFTP.UsePassive = true;
            reqFTP.Proxy = null;
            FtpWebResponse response = null;
            try
            {
                response = (FtpWebResponse)reqFTP.GetResponse();
            }
            catch (SystemException e)
            {
            }
            if (response == null) return false;
            StreamReader ftpStream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("GB2312"));
            //StreamReader ftpStream = new StreamReader(response.GetResponseStream());

            List<string> files = new List<string>();
            string line = ftpStream.ReadLine();
            while (line != null)
            {
                files.Add(line);
                line = ftpStream.ReadLine();
            }

            ftpStream.Close();
            response.Close();
            string[] ss = path.Split('/');
            string s = ss[ss.Length - 1];
            return files.Contains(s);
        }

        public static bool IsHaveFujian(string qukuai, string table, string jinghao)
        {
            string dir = "ftp://" + ftpServerIp + "/" + qukuai + "/" + table + "/" + jinghao;
            if (IsFtpdirExists(dir))
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(dir));
                reqFTP.Credentials = new NetworkCredential(ftpUserName, ftpUserPwd);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = true;
                reqFTP.Proxy = null;
                FtpWebResponse response = null;
                try
                {
                    response = (FtpWebResponse)reqFTP.GetResponse();
                }
                catch (SystemException e)
                {
                }
                if (response == null) return false;
                StreamReader ftpStream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("GB2312"));
                //StreamReader ftpStream = new StreamReader(response.GetResponseStream());

                List<string> files = new List<string>();
                string line = ftpStream.ReadLine();
                while (line != null)
                {
                    files.Add(line);
                    line = ftpStream.ReadLine();
                }

                ftpStream.Close();
                response.Close();
                return files.Count > 0;
            }
            else
            {
                return false;
            }
        }

        public static List<string> GetFujian(string qukuai, string table, string jinghao)
        {
            string dir = "ftp://" + ftpServerIp + "/" + qukuai + "/" + table + "/" + jinghao;
            if (IsFtpdirExists(dir))
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(dir));
                reqFTP.Credentials = new NetworkCredential(ftpUserName, ftpUserPwd);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = true;
                reqFTP.Proxy = null;
                FtpWebResponse response = null;
                try
                {
                    response = (FtpWebResponse)reqFTP.GetResponse();
                }
                catch (SystemException e)
                {
                }
                if (response == null) return null;
                StreamReader ftpStream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("GB2312"));
                //StreamReader ftpStream = new StreamReader(response.GetResponseStream());

                List<string> files = new List<string>();
                string line = ftpStream.ReadLine();
                while (line != null)
                {
                    files.Add(line);
                    line = ftpStream.ReadLine();
                }

                ftpStream.Close();
                response.Close();
                return files;
            }
            else
            {
                return null;
            }
        }

        //ftp的上传功能
        public static bool Upload(FileInfo fileInf, string dir)
        {
            if (fileInf == null || !fileInf.Exists) return false;
            if (dir == null || dir == "") return false;

            String fjName = fileInf.Name;
            fjName = fjName.Replace('#', '^');
            fjName = fjName.Replace('+', '~');

            string str = "ftp://" + ftpServerIp + "/" + dir;
            if (!MyTools.IsFtpdirExists(str))
            {
                try
                {
                    FtpWebRequest reqFTP = null;
                    FtpWebResponse response = null;
                    reqFTP = (FtpWebRequest)WebRequest.Create(str);
                    reqFTP.Credentials = new NetworkCredential(ftpUserName, ftpUserPwd);
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.KeepAlive = true;
                    reqFTP.UseBinary = true;
                    reqFTP.UsePassive = true;
                    //reqFTP.Proxy = new WebProxy(proxyHost, proxyPort);
                    reqFTP.EnableSsl = true;
                    response = (FtpWebResponse)reqFTP.GetResponse();
                    response.Close();
                }
                catch (System.Exception e)
                {
                    return false;
                }
            }
            string uri = str + "/" + fjName;

            FileStream fs = null;
            Stream strm = null;
            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                reqFTP.Credentials = new NetworkCredential(ftpUserName, ftpUserPwd);
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.ContentLength = fileInf.Length;
                reqFTP.KeepAlive = true;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = true;
                //reqFTP.Proxy = new WebProxy(proxyHost, proxyPort);
                reqFTP.EnableSsl = true;

                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                fs = fileInf.OpenRead();
                strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();

                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
            finally
            {
                if (strm != null) strm.Close();
                if (fs != null) fs.Close();
            }
        }

        //从ftp服务器上下载文件的功能
        //dstFilePath: 本地存放位置，包含文件名
        //srcFilePath: 服务器文件的相对路径，即从ftp根目录起算的路径
        public static bool Download(string dstFilePath, string ftpUrl)
        {
            FtpWebRequest reqFTP;
            FileStream outputStream = null;
            FtpWebResponse response = null;
            Stream ftpStream = null;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(ftpUrl);
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserName, ftpUserPwd);

                outputStream = new FileStream(dstFilePath, FileMode.Create);

                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();

                long len = response.ContentLength;
                int bufferSize = 2048;
                byte[] buffer = new byte[bufferSize];
                int readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (ftpStream != null) ftpStream.Close();
                if (outputStream != null) outputStream.Close();
                if (response != null) response.Close();
            }
        }
    }
}