using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Services;
using System.Web.SessionState;

namespace NCCBM.Import
{
    /// <summary>
    /// FujianHandler 的摘要说明
    /// </summary>
    /// IRequiresSessionState(读写), IReadOnlySessionState(只读),仅IE支持
    public class FujianHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            string dir = "";
            HttpCookie cookie = HttpContext.Current.Request.Cookies["fjdir"];
            if (cookie != null)
            {
                String str = cookie.Value.ToString();
                str = HttpContext.Current.Server.UrlDecode(str);
                dir = HttpUtility.UrlDecode(str, System.Text.Encoding.GetEncoding("UTF-8"));
            }

            //string uploadPath = HttpContext.Current.Server.MapPath("~/temp/fujian/");
            string uploadPath = "D:/NCCBM/fujian/" + dir;

            HttpPostedFile file = context.Request.Files["Filedata"];
            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                String name = file.FileName;
                name = name.Replace('#', '^');
                name = name.Replace('+', '~');
                file.SaveAs(uploadPath + "/" + name);
                //FileInfo fInfo = new FileInfo(uploadPath + file.FileName);
                //MyTools.Upload(fInfo, dir);
                //fInfo.Delete();
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("0");
            }
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