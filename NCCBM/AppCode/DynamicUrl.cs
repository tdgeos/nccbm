using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace NCCBM
{
    public class DynamicUrl
    {
        private string path="";
        private Dictionary<string, string> parameterList = new Dictionary<string, string>();
        public DynamicUrl()
        {
        }
        public DynamicUrl(string strUrl)
        {
            this.Parse(strUrl);   
        }
        protected void Parse(string strUrl)
        {
            int index = strUrl.IndexOf("?");
            if (index > 0)
            {
                this.path = strUrl.Substring(0, index);
                string queryString = strUrl.Substring(index + 1);
                string[] kvList = queryString.Split("&".ToCharArray());
                if (kvList.Length > 0)//Any()
                {
                    foreach (string kvPair in kvList)
                    {
                        int equalSignIndex = kvPair.IndexOf("=");
                        if (equalSignIndex > 0)
                        {
                            this.parameterList[kvPair.Substring(0, equalSignIndex)]
                                = kvPair.Substring(equalSignIndex + 1);
                        }
                    }
                }
            }
            else this.path = strUrl;
        }
        public string this[string key]
        {
            get
            {
                return this.parameterList[key];
            }
            set
            {
                this.parameterList[key] = value;
            }
        }
        public void Remove(string key)
        {
            this.parameterList.Remove(key);
        }
        public bool ContainsKey(string key)
        {
            return this.parameterList.ContainsKey(key);
        }
        public IEnumerable<string> Keys
        {
            get
            {
                return this.parameterList.Keys;
            }
        }
        public void RemoveAll()
        {
            this.parameterList.Clear();
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(path);
            if (this.parameterList.Count > 0)//.Any()
            {                
                bool isFirst = true;
                foreach (string key in this.parameterList.Keys)
                {
                    if (isFirst)
                        builder.Append("?");
                    else
                        builder.Append("&");
                     
                    builder.AppendFormat("{0}={1}", key, parameterList[key]);
                    isFirst = false;
                }
            }
            return builder.ToString();
        }

    }
}