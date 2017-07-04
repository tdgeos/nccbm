using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// common 的摘要说明
/// </summary>
public class common
{
	public common()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	
    }
    //public static char[] c_split ={ '''','''''' };

    public static int GetPageSize()
    {
        int iPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("PageSize"));
        return iPageSize;
    }
    public static void returnparent(string strParentUrl)
    {
        string strOutPut = "";
        strOutPut = "<Script language='javascript'>";
        strOutPut = strOutPut + "alert('提交成功');location.href='" + strParentUrl + "';";
        strOutPut = strOutPut + "</Script>";
        HttpContext.Current.Response.Write(strOutPut);
    }
    public static void  alertMessage(string strMessage)
    {
        string strOutPut = "";
        strOutPut = "<Script language='javascript'>";
        strOutPut = strOutPut + "alert('" + strMessage + "');";
        strOutPut = strOutPut + "</Script>";
        HttpContext.Current.Response.Write(strOutPut);
    }
    public static void openSmallWindow(string strUrl,int iHeight,int iWidth)
    {
        string strOutPut = "";
        strOutPut = "<Script language='javascript'>";
        strOutPut = strOutPut + "self.close();window.open('" + strUrl + "','发送消息','height=" + iHeight + "px,width=" + iWidth + "px,center:yes;help:no;resizeble:no;scroll:yes;status:no;toolbar=no,menubar=no,top=220,left=300,location=no');";
        strOutPut = strOutPut + "</Script>";
        HttpContext.Current.Response.Write(strOutPut);
    }
    public static void openWindow(string strUrl)
    {
        string strOutPut = "";
        strOutPut = "<Script language='javascript'>";
        strOutPut = strOutPut + "window.open('" + strUrl + "','新窗口');";
        strOutPut = strOutPut + "</Script>";
        HttpContext.Current.Response.Write(strOutPut);
    }
    public static void closeWindow()
    {
        string strOutPut = "";
        strOutPut = "<Script language='javascript'>";
        strOutPut = strOutPut + "window.opener = null;window.close();";
        strOutPut = strOutPut + "</Script>";
        HttpContext.Current.Response.Write(strOutPut);
        HttpContext.Current.ClearError();
    }
    public static void closeLoginWindow()
    {
        alertMessage("三次登陆没有成功，请稍后再试！");
        closeWindow();
    }
    public static void CreateTestData(ArrayList strQuery)
    {
        SqlConnection con = DBCONN.GetDBConn();
        con.Open();
        
        SqlCommand com = con.CreateCommand();
        //com.Transaction = st;
        string strSQL="";
        string strDate = "";
        int jCount;
        
            for (int iLoop = 1; iLoop <=12; iLoop++)
            {
                if (iLoop == 2)
                {
                    jCount = 28;
                }else if(iLoop==1 || iLoop==3 || iLoop==5 ||iLoop==7 || iLoop==8 || iLoop==10||iLoop==12)
                {
                    jCount=31;
                }else
                {
                    jCount=30;
                }

                for (int jLoop = 1; jLoop <= jCount; jLoop++)
                {
                    SqlTransaction st = con.BeginTransaction();
                    com.Transaction = st;
                    try
                    {
                        for (int kLoop = 0; kLoop < strQuery.Count; kLoop++)
                        {
                            strDate = "2007-" + iLoop + "-" + jLoop;
                            strSQL = strQuery[kLoop].ToString();
                            strSQL = strSQL.Replace("@Date", strDate);
                            com.CommandText = strSQL;
                            com.ExecuteNonQuery();
                        }
                        st.Commit();              
                    }

                    catch (Exception ex)
                    {
                        alertMessage(ex.Message);
                        st.Rollback();
                    }
                    finally
                    {
                        //关闭
                        
                    }
                }
            }
            con.Close();
            alertMessage("数据生成成功！");
    }
    public static void UpdateSQL(ArrayList strQuery, string strParentUrl,int iCloseWin)
    {
        //【功能】:在插入或修改数据时实现事务处理:
        //【参数说明】:strQuery：插入或删除语句组成的数组，
        //             strParentUrl，成功后返回的父页面；如果不需要返回的话就设为""
        //             iCloseWin:是否关闭当前窗口（0为不挂你，1为关闭）,该参数只在strParentUrl为""时有效
        SqlConnection con = DBCONN.GetDBConn();
        con.Open();
        SqlTransaction st = con.BeginTransaction();
        SqlCommand com = con.CreateCommand();
        com.Transaction = st;
        try
        {
            for (int iLoop = 0; iLoop < strQuery.Count; iLoop++)
            {
                com.CommandText = strQuery[iLoop].ToString();
                com.ExecuteNonQuery();
            }
            st.Commit();
            if (strParentUrl.Length > 0)
            {
                returnparent(strParentUrl);
            }
            else
            {
                //alertMessage("提交成功");
                if (iCloseWin == 1)
                {
                    closeWindow();
                }
            }

        }
        catch(Exception ne) 
        {
            alertMessage("提交失败,错误原因：" + ne.Message);
            st.Rollback();
        }
        finally
        {
            //关闭
            con.Close();
        }
    }

    public static string  GetMinMaxWeek(string strTJSJ)
    {
        string strMinDay="", strMaxDay="";
        string strReturn = "";
        DateTime dt = DateTime.Parse(strTJSJ);
        int dWeek = (int)dt.DayOfWeek;
        switch (dWeek)
        {
            case 0:
                strMinDay = dt.ToString();
                strMaxDay = dt.AddDays(6).ToString();
                break;
            case 1:
                strMinDay = dt.AddDays(-1).ToString();
                strMaxDay = dt.AddDays(5).ToString();
                break;
            case 2:
                strMinDay = dt.AddDays(-2).ToString();
                strMaxDay = dt.AddDays(4).ToString();
                break;
            case 3:
                strMinDay = dt.AddDays(-3).ToString();
                strMaxDay = dt.AddDays(3).ToString();
                break;
            case 4:
                strMinDay = dt.AddDays(-4).ToString();
                strMaxDay = dt.AddDays(2).ToString();
                break;
            case 5:
                strMinDay = dt.AddDays(-5).ToString();
                strMaxDay = dt.AddDays(1).ToString();
                break;
            case 6:
                strMinDay = dt.AddDays(-6).ToString();
                strMaxDay = dt.ToString();
                break;
        }
        strMinDay = string.Format("{0:yyyy.MM.dd}", DateTime.Parse(strMinDay));
        strMaxDay = string.Format("{0:yyyy.MM.dd}", DateTime.Parse(strMaxDay));
        strReturn = strMinDay + "," + strMaxDay;
        return strReturn;

    }

    public static void gvExportExcel(GridView grdv_Export, SqlDataSource sqlDS_Export, string strSQLDS, string strQuery,string strFileName)
    {
        //[功能]把GridView数据输出到Excel
        //[参数说明]:grdv_Export列表内的GridView,sqlDS_Export列表内指定的数据源,strSQLDS列表内数据源的ID
        //           strQuery列表内指定的数据源的SelectCommand,strFileName导出的Excel文件名
        HttpContext.Current.Response.Clear();

        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(strFileName, System.Text.Encoding.GetEncoding("gb2312")) + ".xls");

        HttpContext.Current.Response.Charset = "gb2312";
        //HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentType = "application/vnd.xls";
        HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
        //HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");


        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdv_Export.AllowPaging = false;        
        sqlDS_Export.SelectCommand = strQuery;
        grdv_Export.DataSourceID = strSQLDS;
        
        grdv_Export.DataBind();
        grdv_Export.RenderControl(htmlWrite);

        HttpContext.Current.Response.Write(stringWrite.ToString());

        

        grdv_Export.AllowPaging = true;
        sqlDS_Export.SelectCommand = strQuery;
        grdv_Export.DataSourceID = strSQLDS;
        
        grdv_Export.DataBind();
        HttpContext.Current.Response.End();
    }

    public static void gvExportExcel(GridView grdv_Export, IList list, string strFileName)
    {
        //[功能]把GridView数据输出到Excel
        //[参数说明]:grdv_Export列表内的GridView,sqlDS_Export列表内指定的数据源,strSQLDS列表内数据源的ID
        //           strQuery列表内指定的数据源的SelectCommand,strFileName导出的Excel文件名
        HttpContext.Current.Response.Clear();

        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(strFileName, System.Text.Encoding.GetEncoding("gb2312")) + ".xls");

        HttpContext.Current.Response.Charset = "gb2312";
        //HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentType = "application/vnd.xls";
        HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
        //HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");


        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdv_Export.AllowPaging = false;
        
        grdv_Export.DataSource = list;

        grdv_Export.DataBind();
        grdv_Export.RenderControl(htmlWrite);

        HttpContext.Current.Response.Write(stringWrite.ToString());



        grdv_Export.AllowPaging = true;
        
        grdv_Export.DataSource = list;

        grdv_Export.DataBind();
        HttpContext.Current.Response.End();
    }

    public static void dlExportExcel(DataList[] dl_Export, string strFileName)
    {
        //【功能】把DataList数据输出到Excel
        //[参数说明]:DataList页面上所有的DataList
        //           strFileName导出的Excel文件名
        //HttpContext.Current.Response.Clear();

        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");

        HttpContext.Current.Response.Charset = "gb2312";
        HttpContext.Current.Response.ContentType = "application/vnd.xls";
        HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        for (int iLoop = 0; iLoop < dl_Export.Length; iLoop++)
        {
            dl_Export[iLoop].RenderControl(htmlWrite);
        }
        HttpContext.Current.Response.Write(stringWrite.ToString());

        HttpContext.Current.Response.End();

    }

    public static void ExportExcel(DataList[] dl_Export, GridView[] grdv_Export, SqlDataSource[] sqlDS_Export, string[] strSQLDS, string[] strQuery, string strFileName)
    {
        //[功能]把GridView数据输出到Excel
        //[参数说明]:grdv_Export列表内的GridView,sqlDS_Export列表内指定的数据源,strSQLDS列表内数据源的ID
        //           strQuery列表内指定的数据源的SelectCommand,strFileName导出的Excel文件名
        HttpContext.Current.Response.Clear();

        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");

        HttpContext.Current.Response.Charset = "gb2312";
        HttpContext.Current.Response.ContentType = "application/vnd.xls";
        HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        for (int iLoop = 0; iLoop < dl_Export.Length; iLoop++)
        {
            dl_Export[iLoop].RenderControl(htmlWrite);
        }
        for (int iLoop = 0; iLoop < grdv_Export.Length; iLoop++)
        {
            grdv_Export[iLoop].AllowPaging = false;
            sqlDS_Export[iLoop].SelectCommand = strQuery[iLoop];
            grdv_Export[iLoop].DataSourceID = strSQLDS[iLoop];
            grdv_Export[iLoop].DataBind();
            grdv_Export[iLoop].RenderControl(htmlWrite);
        }
        HttpContext.Current.Response.Write(stringWrite.ToString());

        for (int iLoop = 1; iLoop < grdv_Export.Length; iLoop++)
        {
            grdv_Export[iLoop].AllowPaging = true;
            sqlDS_Export[iLoop].SelectCommand = strQuery[iLoop];
            grdv_Export[iLoop].DataSourceID = strSQLDS[iLoop];
            grdv_Export[iLoop].DataBind();
        }
        HttpContext.Current.Response.End();

    }

    public static string Desc(string strComm,string strDes,int nType)
    {
        //    '[功能]：加密
        //'[创建人]：wfl
        //'[Create Time]：2002-1-2
        //'【参数】：strDes：待加密或解密的字符串
        //          strComm：加密使用的密钥，此处我们使用的是用户名
        //'         nType:  0为加密；1为解密
        int  iLoopCM,nComm,nPassWord,iLoopPW, jLoop, jCount;
		string  strTemp="" ;
		nComm = strComm.Length ;
        iLoopPW = -1;
        
        if(nType==0)  //加密
        {
           if(strDes != "")
            {
			   nPassWord = strDes.Length;
               int nMod;
               Math.DivRem(nPassWord,nComm,out nMod );
			   if( nMod != 0)
               {
                   double dTemp = nPassWord / nComm;
                   jCount = Convert.ToInt32(Math.Floor(dTemp)) + 1;
               }else{
					jCount = nPassWord / nComm;
               }
			   for(jLoop = 0;jLoop< jCount;jLoop++)
               {
						for(iLoopCM = 0;iLoopCM< nComm;iLoopCM++)
                        {
						    if((jLoop  * nComm + iLoopCM) <= (nPassWord-1))
                            {
							    if(iLoopPW < nPassWord-1){
								    iLoopPW = iLoopPW + 1;                                    
								    strTemp = strTemp + Convert.ToString((short)strComm.Substring(iLoopCM, 1).ToCharArray()[0] + (short)strDes.Substring(iLoopPW, 1).ToCharArray()[0]);
    							}	
							    else{
                                    strTemp = strTemp + Convert.ToString((short)strComm.Substring(iLoopCM, 1).ToCharArray()[0] + (short)strDes.Substring(iLoopPW, 1).ToCharArray()[0]);
								    iLoopPW = -1;
							    }
						    }else
                            {
							    break;
						    }
                        }
               }
            }
			else
            {
				for(iLoopCM = 0 ;iLoopCM< nComm;iLoopCM++){
                    strTemp = strTemp + Convert.ToString((short)strComm.Substring(iLoopCM, 1).ToCharArray()[0] + (short)strComm.Substring(iLoopCM, 1).ToCharArray()[0]);
				}
			}
        }else//解密
        {
            nPassWord = strDes.Length ;
			iLoopCM = 0;
			for(iLoopPW = 0 ;iLoopPW< (nPassWord-1);iLoopPW=iLoopPW +3)
            {
                if (nPassWord < 3)
                {
                    strTemp = strTemp + (char)(Convert.ToInt32(strDes.Substring(iLoopPW, nPassWord)) - (short)strComm.Substring(iLoopCM, 1).ToCharArray()[0]);
                }
                else
                {
                    strTemp = strTemp + (char)(Convert.ToInt32(strDes.Substring(iLoopPW, 3)) - (short)strComm.Substring(iLoopCM, 1).ToCharArray()[0]);
                }
				if(iLoopCM < nComm-1){
                    iLoopCM = iLoopCM + 1;
				}else{
					iLoopCM = 0;
				}
			}
        }
        return strTemp.Trim();
    }
    public static int  InitXMMCS(DropDownList dd)
    {
        int i = 0;
        string strXMMCS = HttpContext.Current.Session["XMMCS"].ToString();
        char[] arrTemp = new char[] { ';' };

        if (strXMMCS != "" && strXMMCS != "NULL")
        {
            string[] arrXMMCS = strXMMCS.Split(arrTemp[0]);
            dd.Items.Clear();
            for (int k = 0; k < arrXMMCS.Length; k++)
            {
                dd.Items.Add(arrXMMCS[k]);
            }
            i = 1;
        }
        return i;
    }

    public static void GroupRows(System.Web.UI.WebControls.GridView grd1,int cellNum)
    {
        //合并GridView中某列相同信息的行
        int i = 0, rowSpanNum=1;
        
        while (i < grd1.Rows.Count - 1)
        {
            GridViewRow gvr = grd1.Rows[i];
            for (++i; i < grd1.Rows.Count; i++)
            {
                GridViewRow gvrNext = grd1.Rows[i];
                if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)
                {
                    gvrNext.Cells[cellNum].Visible = false;
                    rowSpanNum++;
                }
                else
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    rowSpanNum = 1;
                    break;
                }
                if (i == grd1.Rows.Count-1)
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                }
            }
        }
    }   
    public static void GroupRowsRPC(System.Web.UI.WebControls.GridView grd1,int cellNum,int jS,int jE)
    {
        //合并GridView中某列相同信息的行,针对钻进日报
        
        int i = 0, rowSpanNum = 1;
       
        while (i < grd1.Rows.Count - 1)
        {
            GridViewRow gvr = grd1.Rows[i];
            
            for (++i; i < grd1.Rows.Count; i++)
            {
                GridViewRow gvrNext = grd1.Rows[i];

                if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text && gvr.Cells[cellNum + 1].Text == gvrNext.Cells[cellNum + 1].Text && gvr.Cells[cellNum + 3].Text == gvrNext.Cells[cellNum + 3].Text)
                {
                    for (int j = jS; j < jE; j++)
                    {
                        gvrNext.Cells[j].Visible = false;
                    }
                    rowSpanNum++;
                }
                else
                {
                    for (int j = jS; j < jE; j++)
                    {
                        gvr.Cells[j].RowSpan = rowSpanNum;
                    }                    
                    rowSpanNum = 1;
                    break;
                }
                if (i == grd1.Rows.Count-1)
                {
                    
                    for (int j = jS; j < jE; j++)
                    {
                        gvr.Cells[j].RowSpan = rowSpanNum;
                    }                    
                }
            }
        }
        
    }
    public static void GroupRowsRSC(System.Web.UI.WebControls.GridView grd1, int cellNum, int jS, int jE)
    {
        //合并GridView中某列相同信息的行,针对钻进日报

        int i = 0, rowSpanNum = 1;

        while (i < grd1.Rows.Count - 1)
        {
            GridViewRow gvr = grd1.Rows[i];

            for (++i; i < grd1.Rows.Count; i++)
            {
                GridViewRow gvrNext = grd1.Rows[i];

                if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text && gvr.Cells[cellNum + 1].Text == gvrNext.Cells[cellNum + 1].Text)
                {
                    for (int j = jS; j < jE; j++)
                    {
                        gvrNext.Cells[j].Visible = false;
                    }
                    rowSpanNum++;
                }
                else
                {
                    for (int j = jS; j < jE; j++)
                    {
                        gvr.Cells[j].RowSpan = rowSpanNum;
                    }
                    rowSpanNum = 1;
                    break;
                }
                if (i == grd1.Rows.Count - 1)
                {

                    for (int j = jS; j < jE; j++)
                    {
                        gvr.Cells[j].RowSpan = rowSpanNum;
                    }
                }
            }
        }

    }
    public static void GroupRowsZJ(System.Web.UI.WebControls.GridView grd1, int cellNum, int jS, int jE)
    {
        //合并GridView中某列相同信息的行,针对钻进日报

        int i = 0, rowSpanNum = 1;

        while (i < grd1.Rows.Count - 1)
        {
            GridViewRow gvr = grd1.Rows[i];

            for (++i; i < grd1.Rows.Count; i++)
            {
                GridViewRow gvrNext = grd1.Rows[i];

                if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)
                {
                    for (int j = jS; j < jE; j++)
                    {
                        gvrNext.Cells[j].Visible = false;
                    }
                    rowSpanNum++;
                }
                else
                {
                    for (int j = jS; j < jE; j++)
                    {
                        gvr.Cells[j].RowSpan = rowSpanNum;
                    }
                    rowSpanNum = 1;
                    break;
                }
                if (i == grd1.Rows.Count - 1)
                {

                    for (int j = jS; j < jE; j++)
                    {
                        gvr.Cells[j].RowSpan = rowSpanNum;
                    }
                }
            }
        }

    }
    public static string GetXQ(string strRQ)
    {
        string strXQ = "";
        DateTime dt = DateTime.Parse(strRQ);
        int dWeek = (int)dt.DayOfWeek;
        switch (dWeek)
        {
            case 0:
                strXQ = "日";
                break;
            case 1:
                strXQ = "一";
                break;
            case 2:
                strXQ = "二";
                break;
            case 3:
                strXQ = "三";
                break;
            case 4:
                strXQ = "四";
                break;
            case 5:
                strXQ = "五";
                break;
            case 6:
                strXQ = "六";
                break;
        }
        return strXQ;
    }
}
