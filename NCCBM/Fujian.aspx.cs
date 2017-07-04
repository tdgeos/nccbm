using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Net;

namespace NCCBM
{
    public partial class Fujian : System.Web.UI.Page
    {
        private string tmpDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\temp\\";

        protected void Page_Load(object sender, EventArgs e)
        {
            String srid = null;
            int rid = 0;
            try
            {
                string strUserName = HttpContext.Current.Session["LoginUserID"].ToString();
                srid = HttpContext.Current.Session["RoleID"].ToString();
                rid = Int32.Parse(srid);
            }
            catch (System.Exception ee)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            lbDownLoad.Command += new CommandEventHandler(lbtnDownLoad_Command);
            lbReturn.Command += new CommandEventHandler(lbReturn_Command);
            lbDelete.Command += new CommandEventHandler(lbDelete_Command);

            if (rid == 1)
            {
                lbDelete.Visible = true;
            }

            if (!Page.IsPostBack)
            {
                string dir = "";
                string fjs = "";
                string table = "";
                string page = "";

                string s = Request.QueryString["FJ"];
                string[] ss = s.Split(',');
                if (ss.Length == 3)
                {
                    dir = "/" + ss[0] + "/" + ss[1] + "/" + ss[2] + "/";
                    List<string> tmp = MyTools.GetFujian(ss[0], ss[1], ss[2]);
                    MyTools.FujianPaixu(tmp);
                    fjs = "";
                    if (tmp.Count > 0)
                    {
                        for (int i = 0; i < tmp.Count; i++)
                        {
                            if (i == 0) fjs = tmp[i];
                            else fjs += ";" + tmp[i];
                        }
                    }
                    fjs = fjs.Replace('^', '#');
                    fjs = fjs.Replace('~', '+');
                    table = ss[1];
                    lblDir.Text = dir;
                    lblFjs.Text = fjs;
                }

                string f0 = fjs.Split(';')[0];
                string filename = f0;
                filename = filename.Replace('#', '^');
                filename = filename.Replace('+', '~');
                string url = "ftp://" + MyTools.ftpUserName + ":" + MyTools.ftpUserPwd + "@" + MyTools.ftpServerIp + dir + filename;
                url = BM(url);
                Image1.ImageUrl = url;
                Label1.Text = f0;

                s = Request.QueryString["Page"];
                ss = s.Split(',');
                if (ss.Length == 5 && ss[0] == "m")
                {
                    string qk = "&Qukuai=" + ss[1];
                    string rqb = "&dateBegin=" + ss[2];
                    string rqe = "&dateEnd=" + ss[3];
                    string pageindex = "&iPage=" + ss[4];
                    if (table == "钻进")
                    {
                        page = "~/data/zj/zj/zj_list.aspx?Type=list" + qk + rqb + rqe + pageindex;
                    }
                    if (table == "下套管")
                    {
                        page = "~/data/zj/xtg/xtg_list.aspx?Type=list" + qk + rqb + rqe + pageindex;
                    }
                    if (table == "固井")
                    {
                        page = "~/data/zj/gj/gj_list.aspx?Type=list" + qk + rqb + rqe + pageindex;
                    }
                    if (table == "完井")
                    {
                        page = "~/data/zj/wj/wj_list.aspx?Type=list" + qk + rqb + rqe + pageindex;
                    }
                    if (table == "完井统计表")
                    {
                        page = "~/data/zj/scjwjtjb/scjwjtjb_list.aspx?Type=list" + qk + rqb + rqe + pageindex;
                    }
                    if (table == "压裂检查")
                    {
                        page = "~/data/yl/yqjc/yqjc_list.aspx?Type=list" + qk + rqb + rqe + pageindex;
                    }
                    if (table == "射孔")
                    {
                        page = "~/data/yl/sk/sk_list.aspx?Type=list" + qk + rqb + rqe + pageindex;
                    }
                    if (table == "压裂施工")
                    {
                        page = "~/data/yl/ylsg/ylsg_list.aspx?Type=list" + qk + rqb + rqe + pageindex;
                    }
                    if (table == "失败原因说明")
                    {
                        page = "~/data/yl/sbyysm/sbyysm_list.aspx?Type=list" + qk + rqb + rqe + pageindex;
                    }
                }
                if (ss.Length == 7 && ss[0] == "q")
                {
                    string cd = ss[1];
                    string qk = ss[2];
                    string rqb = ss[3];
                    string rqe = ss[4];
                    string pageindex = ss[5];
                    string info = ss[6];
                    if(cd == "0") page = "~/Query/yl/ylzdycx.aspx?CD=" + cd + "," + qk + "," + rqb + "," + rqe + "," + info + "," + pageindex;
                    else page = "~/Query/yl/ylcx.aspx?CD=" + cd + "," + qk + "," + rqb + "," + rqe + "," + info + "," + pageindex;
                }
                if (ss.Length == 7 && ss[0] == "f")
                {
                    string cd = ss[1];
                    string qk = ss[2];
                    string rqb = ss[3];
                    string rqe = ss[4];
                    string pageindex = ss[5];
                    string info = ss[6];
                    page = "~/Query/rb/rbcx.aspx?CD=" + cd + "," + qk + "," + rqb + "," + rqe + "," + info + "," + pageindex;
                }
                if (ss.Length == 8 && ss[0] == "q")
                {
                    page = "~/Query/rb/xxxx.aspx?JH=" + ss[1] + "," + ss[2] + "," + ss[3] + "," + ss[4] + "," + ss[5] + "," + ss[6] + "," + ss[7];
                }
                if (ss.Length == 5 && ss[0] == "q")
                {
                    string cd = ss[1];
                    if(cd == "0") page = "~/Query/yl/zdycx.aspx?CD=" + cd;
                    else page = "~/Query/yl/wjcx.aspx?CD=" + cd;
                }
                lblPage.Text = page;
            }

            string str = lblFjs.Text;
            if (str == "") return;
            string[] files = str.Split(';');
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i] == "") continue;
                LinkButton lbtn = new LinkButton();
                lbtn.CommandArgument = files[i];
                lbtn.Text = files[i];
                lbtn.Command += new CommandEventHandler(lbtn_Command);
                lbtn.Style["style"] = "margin-top:5px;";
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Controls.Add(lbtn);
                this.Panel1.Controls.Add(div);
            }
        }

        void lbDelete_Command(object sender, CommandEventArgs e)
        {
            String filename = Label1.Text.ToString().Trim();
            String tmp = filename;
            filename = filename.Replace('#', '^');
            filename = filename.Replace('+', '~');

            FtpWebRequest reqFTP;
            try
            {
                String path = "ftp://" + MyTools.ftpServerIp + lblDir.Text + filename;
                Uri uri = new Uri(path);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.Credentials = new NetworkCredential(MyTools.ftpUserName, MyTools.ftpUserPwd);

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                response.Close();
            }
            catch (Exception ex)
            {
            }

            this.Panel1.Controls.Clear();
            Label1.Text = "";
            Image1.ImageUrl = null;

            string str = lblFjs.Text;
            string ss = "";
            if (str == "") return;
            string[] files = str.Split(';');
            int j = 0;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i] == tmp) continue;
                if (j == 0) ss += files[i];
                else ss += ";" + files[i];
                j++;

                LinkButton lbtn = new LinkButton();
                lbtn.CommandArgument = files[i];
                lbtn.Text = files[i];
                lbtn.Command += new CommandEventHandler(lbtn_Command);
                lbtn.Style["style"] = "margin-top:5px;";
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Controls.Add(lbtn);
                this.Panel1.Controls.Add(div);
            }
            lblFjs.Text = ss;

            files = ss.Split(';');
            if (files.Length > 0)
            {
                filename = files[0];
                Label1.Text = filename;
                filename = filename.Replace('#', '^');
                filename = filename.Replace('+', '~');
                string url = "ftp://" + MyTools.ftpUserName + ":" + MyTools.ftpUserPwd + "@" + MyTools.ftpServerIp + lblDir.Text + filename;
                url = BM(url);
                Image1.ImageUrl = url;
            }
        }

        void lbReturn_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(lblPage.Text);
        }

        void lbtn_Command(object sender, CommandEventArgs e)
        {
            String filename = e.CommandArgument.ToString();
            Label1.Text = filename;
            filename = filename.Replace('#', '^');
            filename = filename.Replace('+', '~');
            string url = "ftp://" + MyTools.ftpUserName + ":" + MyTools.ftpUserPwd + "@" + MyTools.ftpServerIp + lblDir.Text + filename;
            url = BM(url);
            Image1.ImageUrl = url;  
        }

        void lbtnDownLoad_Command(object sender, CommandEventArgs e)
        {
            String filename = Label1.Text.ToString().Trim();
            filename = filename.Replace('#', '^');
            filename = filename.Replace('+', '~');
            HttpContext.Current.Response.ContentType = "application/ms-download";
            string s_path = tmpDir + filename;
            Download(s_path, filename);
            System.IO.FileInfo file = new System.IO.FileInfo(s_path);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.End();
            if (file.Exists) file.Delete();
        }

        //从ftp服务器上下载文件的功能
        //dstFilePath: 本地存放位置，包含文件名
        //srcFilePath: 服务器文件的相对路径，即从ftp根目录起算的路径
        private bool Download(string dstFilePath, string srcFileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                String path = "ftp://" + MyTools.ftpServerIp + lblDir.Text + srcFileName;
                Uri uri = new Uri(path);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(MyTools.ftpUserName, MyTools.ftpUserPwd);

                FileStream outputStream = new FileStream(dstFilePath, FileMode.Create);

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

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
        }

        private string BM(string hz)
        {
            string tmp = "";

            foreach (char c in hz)
            {
                if (c != ':' && c != '.' && c != '/' && c != '\\' && c != '?' && c != '=' && c != '&' && c != '@' && c != '!' && c != '#' && c != '$' && c != '%' && c != '^' && c != '(' && c != ')' && c != '"' && c != '<' && c != '>' && c != ',' && c != ' ' && c != '[' && c != ']' && c != '{' && c != '}' && c != '_' && c != '-' && c != '+' && c != ';' && c != '`' && c != '~' && c != '|')
                {
                    tmp += System.Web.HttpUtility.UrlEncode(c.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
                }
                else
                {
                    tmp += c.ToString();
                }
            }

            return tmp;
        }
    }
}