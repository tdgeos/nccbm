using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for DBCONN
/// </summary>
/// 
public class DBCONN
{
    public static SqlConnection GetDBConn()
    {
        string strConn = ConfigurationManager.ConnectionStrings["_NCCBM"].ToString();
        return new SqlConnection(strConn);
    }
}
