using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;  
using System.Web.Configuration;  
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;


namespace NCCBM
{
    public partial class mapgis : System.Web.UI.Page
    {
        private string _seperator = ","; 

        protected void Page_Load(object sender, EventArgs e)
        {
            string szRoleID;
            //判断用户是否登陆,如果没有登陆直接转向登陆页面
            try
            {
                szRoleID = HttpContext.Current.Session["RoleID"].ToString();
            }
            catch (NullReferenceException ne)
            {
                HttpContext.Current.Response.Redirect("NoLogin.aspx");
            }

            WriteInitParams();

        }

        private void WriteInitParams()  
        {  
            NameValueCollection appSettings = WebConfigurationManager.AppSettings;  
            StringBuilder stringBuilder = new StringBuilder();  
            stringBuilder.Append( "<param name=\"InitParams\" value=\"" );  
            string paramContent = string.Empty;  
            for( int i = 0 ; i < appSettings.Count ; i++ )  
            {  
                if( paramContent.Length > 0 )  
                {  
                    paramContent += _seperator;  
                }  
                paramContent += string.Format( "{0}={1}" , appSettings.GetKey( i ) , appSettings[ i ] );  
             }  
             stringBuilder.Append(paramContent );  
             stringBuilder.Append( "\" />" );  
             this.litInitParams.Text = stringBuilder.ToString();
        }

        private void getLayers()
        {
            XmlReader reader = XmlReader.Create("Layers.xml");
            XDocument document = XDocument.Load(reader);
            IEnumerable<XElement> childList =
            from el in document.Elements()  
            select el;
            string value = "";
            foreach (XElement e in childList)
            {
                value += e.Attribute("hc1").Value;
            }

        }
    }
}