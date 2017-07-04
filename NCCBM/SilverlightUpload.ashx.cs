using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace NCCBM
{
    /// <summary>
    /// SilverlightUpload 的摘要说明
    /// </summary>
    public class SilverlightUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            string username = "temp";

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

            //获取上传的数据流
            string filename = context.Request.QueryString["fileName"];
            Stream sr = context.Request.InputStream;
            try
            {
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                string dir = context.Server.MapPath("temp/" + username + "/");
                string targetPath = dir + "/" + filename;
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (FileStream fs = File.Create(targetPath, 4096))
                {
                    while ((bytesRead = sr.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, bytesRead);
                    }
                }

                context.Response.ContentType = "text/plain";
                context.Response.Write("1");
            }
            catch (Exception e)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("上传失败, 错误信息:" + e.Message);
            }
            finally
            { sr.Dispose(); }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}