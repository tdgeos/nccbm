using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Graph;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;


namespace NCCBM.data
{
    public partial class jianbao : System.Web.UI.Page
    {
        private string userName = "";

        private string fg_field = "&";
        private string fg_record = "|";
        private char fgc_field = '&';
        private char fgc_record = '|';
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Cookies["UserName"] != null)
            {
                userName = HttpContext.Current.Request.Cookies["UserName"].Value;
            }
            else
            {
                if (HttpContext.Current.Session["LoginUserID"] != null)
                {
                    userName = HttpContext.Current.Session["LoginUserID"].ToString();
                }
            }

            try
            {
                string userPlaceId = HttpContext.Current.Session["PlaceID"].ToString();
            }
            catch (System.Exception ee)
            {
                HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
            }

            if (!IsPostBack)
            {
                grd_zj.Attributes.Add("BorderColor", "Black");
                grd_zj.Attributes.Add("BorderWidth", "1");

                grd_yl.Attributes.Add("BorderColor", "Black");
                grd_yl.Attributes.Add("BorderWidth", "1");

                grd_yierkaiyanshou.Attributes.Add("BorderColor", "Black");
                grd_yierkaiyanshou.Attributes.Add("BorderWidth", "1");

                grd_jingshenzhiliang.Attributes.Add("BorderColor", "Black");
                grd_jingshenzhiliang.Attributes.Add("BorderWidth", "1");

                grd_xiataoguan.Attributes.Add("BorderColor", "Black");
                grd_xiataoguan.Attributes.Add("BorderWidth", "1");

                grd_gujingzuoye.Attributes.Add("BorderColor", "Black");
                grd_gujingzuoye.Attributes.Add("BorderWidth", "1");

                grd_chulijinglou.Attributes.Add("BorderColor", "Black");
                grd_chulijinglou.Attributes.Add("BorderWidth", "1");

                grd_chuliyongshui.Attributes.Add("BorderColor", "Black");
                grd_chuliyongshui.Attributes.Add("BorderWidth", "1");

                grd_zugongyinsu.Attributes.Add("BorderColor", "Black");
                grd_zugongyinsu.Attributes.Add("BorderWidth", "1");

                grd_rujingcailiao.Attributes.Add("BorderColor", "Black");
                grd_rujingcailiao.Attributes.Add("BorderWidth", "1");

                grd_shigongzhiliang.Attributes.Add("BorderColor", "Black");
                grd_shigongzhiliang.Attributes.Add("BorderWidth", "1");

                tbRiqi.Text = DateTime.Now.ToString("yyyy-MM-dd");
                string strRiqi = tbRiqi.Text;
                string strWeek = MyTools.GetWeek(strRiqi);
                string begin = strWeek.Split(',')[0];
                string end = strWeek.Split(',')[1];

                lblRiqi.Text = begin + "," + end;
                lblType.Text = "zhou";

                lblZuanjingRiqi.Text = begin + " 至 " + end;
                lblYalieRiqi.Text = begin + " 至 " + end;
                lblJishuyanjiuRiqi.Text = begin + " 至 " + end;

                getZtqkZj(begin, end);
                getZtqkYl(begin, end);

                getGJKzys(begin, end);
                getGJJszl(begin, end);
                getGJXtgzy(begin, end);
                getGJGjzy(begin, end);
                getGJCljl(begin, end);
                getGJClys(begin, end);

                getGJZgys(begin, end);
                getGJRjcl(begin, end);
                getGJSgzl(begin, end);

                getJishu(begin, end);
            }
        }

        protected void grd_yl_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string str = "";
                string type = lblType.Text;
                if (type == "zhou") str = "周";
                if (type == "yue") str = "月";
                if (type == "ji") str = "季";

                if (type != "nian")
                {
                    GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    Literal newCells = new Literal();
                    newCells.Text = "类别</th>" +
                          "<th rowspan='2' nowrap>区块</th>" +
                          "<th colspan='3' nowrap>" + str + "压裂</th>" +
                          "<th colspan='3' nowrap>年压裂</th>" +
                          "<th rowspan='2' nowrap>备注</th>" +
                          "</tr><tr>";
                    newCells.Text += @"                         
                      <th class=tableLinedown nowrap>口</th>
                      <th class=tableLinedown nowrap>次</th>
                      <th class=tableLinedown nowrap>层</th>
                      <th class=tableLinedown nowrap>口</th>
                      <th class=tableLinedown nowrap>次</th>
                      <th class=tableLinedown nowrap>层</th>
                      </tr>";

                    TableCellCollection cells = e.Row.Cells;
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.RowSpan = 2;
                    headerCell.Controls.Add(newCells);
                    rowHeader.Cells.Add(headerCell);
                    rowHeader.Cells.Add(headerCell);
                    rowHeader.Visible = true;
                    grd_yl.Controls[0].Controls.AddAt(0, rowHeader);
                }
                else
                {
                    GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    Literal newCells = new Literal();
                    newCells.Text = "类别</th>" +
                          "<th rowspan='2' nowrap>区块</th>" +
                          "<th colspan='3' nowrap>年压裂</th>" +
                          "<th rowspan='2' nowrap>备注</th>" +
                          "</tr><tr>";
                    newCells.Text += @"                         
                      <th class=tableLinedown nowrap>口</th>
                      <th class=tableLinedown nowrap>次</th>
                      <th class=tableLinedown nowrap>层</th>
                      </tr>";

                    TableCellCollection cells = e.Row.Cells;
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.RowSpan = 2;
                    headerCell.Controls.Add(newCells);
                    rowHeader.Cells.Add(headerCell);
                    rowHeader.Cells.Add(headerCell);
                    rowHeader.Visible = true;
                    grd_yl.Controls[0].Controls.AddAt(0, rowHeader);
                }
            }
        }

        //合并GridView列中相同的行
        public void GroupRows(GridView gridView, int cellNum)
        {
            int i = 0, rowSpanNum = 1;
            while (i < gridView.Rows.Count - 1)
            {
                GridViewRow gvr = gridView.Rows[i];
                for (++i; i < gridView.Rows.Count; i++)
                {
                    GridViewRow gvrNext = gridView.Rows[i];
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
                    if (i == gridView.Rows.Count - 1)
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    }
                }
            }
        }

        private string getDateCN(string date)
        {
            if (date == null || date == "") return date;
            if (date.Split('-').Length < 3) return date;
            return date.Split('-')[0] + "年" + date.Split('-')[1] + "月" + date.Split('-')[2] + "日";
        }

        private string getDateCNB(string date)
        {
            if (date == null || date == "") return date;
            if (date.Split('-').Length != 3) return date;
            string[] numCN = new string[11]{"〇","一","二","三","四","五","六","七","八","九","十"};
            string y = date.Split('-')[0];
            string m = date.Split('-')[1];
            string d = date.Split('-')[2];
            string yCN = numCN[y[0]-48] + numCN[y[1]-48] + numCN[y[2]-48] + numCN[y[3]-48];
            string mCN = "";
            string dCN = "";
            if (m.Length == 1)
            {
                mCN = numCN[m[0]-48];
            }
            else if (m[0]-48 == 0)
            {
                mCN = numCN[m[1]-48];
            }
            else
            {
                if (m[1]-48 == 0)
                {
                    mCN = numCN[10];
                }
                else
                {
                    mCN = numCN[10] + numCN[m[1]-48];
                }
            }
            if (d.Length == 1)
            {
                dCN = numCN[d[0]-48];
            }
            else if (d[0]-48 == 0)
            {
                dCN = numCN[d[1]-48];
            }
            else
            {
                if (d[0]-48 == 1)
                {
                    if (d[1]-48 == 0)
                    {
                        dCN = numCN[10];
                    }
                    else
                    {
                        dCN = numCN[10] + numCN[d[1]-48];
                    }
                }
                else
                {
                    if (d[1]-48 == 0)
                    {
                        dCN = numCN[d[0]-48] + numCN[10];
                    }
                    else
                    {
                        dCN = numCN[d[0]-48] + numCN[10] + numCN[d[1]-48];
                    }
                }
            }

            return yCN + "年" + mCN + "月" + dCN + "日";
        }


        protected void Button_Out_Click(object sender, EventArgs e)
        {
            string strWanjing = "";
            string strJinchi = "";
            string strYalie = "";
            string type = lblType.Text;
            if (type == "zhou")
            {
                strWanjing = "周完井（口）";
                strJinchi = "周进尺（米）";
                strYalie = "周压裂";
            }
            if (type == "yue")
            {
                strWanjing = "月完井（口）";
                strJinchi = "月进尺（米）";
                strYalie = "月压裂";
            }
            if (type == "ji")
            {
                strWanjing = "季完井（口）";
                strJinchi = "季进尺（米）";
                strYalie = "季压裂";
            }

            try
            {
                if (type == "zhou" || type == "yue" || type == "ji")
                {
                    string[] ss = lblRiqi.Text.Split(',');
                    string begin = ss[0];
                    string end = ss[1];

                    string date = "（" + getDateCN(begin) + "～" + getDateCN(end) + "）";

                    string end_dt_w = end;
                    string start_dt = begin;
                    string start_dt_y = DateTime.Parse(end_dt_w).AddYears(-1).ToString();

                    object oMissing = System.Reflection.Missing.Value;
                    object oEndOfDoc = "\\endofdoc";
                    Word._Application oWord = new Word.Application();
                    oWord.DisplayAlerts = WdAlertLevel.wdAlertsNone;
                    Word._Document oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                    oWord.Visible = false;


                    Word.Paragraph otopleft, otopleft_second, otopleft_third, otopleft_fourth;

                    otopleft = oDoc.Content.Paragraphs.Add(ref oMissing);
                    otopleft.Range.Text = "内部刊物";
                    otopleft.Range.Font.Size = 12;
                    otopleft.Range.Font.Bold = 1;
                    otopleft.Range.Font.ColorIndex = WdColorIndex.wdRed;
                    otopleft.Format.SpaceAfter = 0;    //0 pt spacing after paragraph.
                    otopleft.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    otopleft.Range.InsertParagraphAfter();

                    otopleft_second = oDoc.Content.Paragraphs.Add(ref oMissing);
                    otopleft_second.Range.Text = "注意保存";
                    otopleft_second.Range.Font.Size = 12;
                    otopleft_second.Range.Font.Bold = 1;
                    otopleft_second.Range.Font.ColorIndex = WdColorIndex.wdRed;
                    otopleft_second.Format.SpaceAfter = 0;    //0 pt spacing after paragraph.
                    otopleft_second.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    otopleft_second.Range.InsertParagraphAfter();

                    otopleft_third = oDoc.Content.Paragraphs.Add(ref oMissing);
                    otopleft_fourth = oDoc.Content.Paragraphs.Add(ref oMissing);


                    Word.Paragraph oParaTitle, oversion, oParaSubTitle, obelow, obelow_second, odate, oLine;
                    oParaTitle = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaTitle.Range.Text = "工 程 监 理 简 报";
                    oParaTitle.Range.Font.Size = 42;
                    oParaTitle.Range.Font.Bold = 0;
                    oParaTitle.Range.Font.Name = "华文新魏";
                    oParaTitle.Range.Font.ColorIndex = WdColorIndex.wdRed;
                    oParaTitle.Format.SpaceAfter = 5;
                    oParaTitle.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oParaTitle.Range.InsertParagraphAfter();

                    oversion = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oversion.Range.Text = "（第  期）";
                    oversion.Range.Font.Size = 14f;
                    oversion.Range.Font.Bold = 0;
                    oversion.Range.Font.Name = "宋体";
                    oversion.Range.Font.ColorIndex = WdColorIndex.wdBlack;
                    oversion.Format.SpaceAfter = 0;
                    oversion.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oversion.Range.InsertParagraphAfter();

                    oParaSubTitle = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaSubTitle.Range.Text = date;
                    oParaSubTitle.Range.Font.Size = 10.5f;
                    oParaSubTitle.Range.Font.Bold = 1;
                    oParaSubTitle.Range.Font.ColorIndex = WdColorIndex.wdBlack;
                    oParaSubTitle.Format.SpaceAfter = 0;
                    oParaSubTitle.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oParaSubTitle.Range.InsertParagraphAfter();

                    obelow = oDoc.Content.Paragraphs.Add(ref oMissing);
                    obelow.Range.Text = "中联煤层气国家工程研究中心";
                    obelow.Range.Font.Size = 14f;
                    obelow.Range.Font.Bold = 1;
                    obelow.Range.Font.Name = "楷体_GB2312";
                    obelow.Range.Font.ColorIndex = WdColorIndex.wdRed;
                    obelow.Format.SpaceAfter = 0;
                    obelow.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    obelow.Range.InsertParagraphAfter();

                    obelow_second = oDoc.Content.Paragraphs.Add(ref oMissing);
                    obelow_second.Range.Text = "中石油煤层气有限责任公司工程监督中心  " + getDateCNB(end);
                    obelow_second.Range.Font.Size = 14f;
                    obelow_second.Range.Font.Bold = 1;
                    obelow_second.Range.Font.Name = "楷体_GB2312";
                    obelow_second.Range.Font.ColorIndex = WdColorIndex.wdRed;
                    obelow_second.Format.SpaceAfter = 0;
                    obelow_second.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    obelow_second.Range.InsertParagraphAfter();

                    //odate = oDoc.Content.Paragraphs.Add(ref oMissing);
                    //odate.Range.Text = getDateCNB(end);
                    //odate.Range.Font.Size = 14f;
                    //odate.Range.Font.Bold = 0;
                    //odate.Range.Font.Name = "宋体";
                    //odate.Range.Font.ColorIndex = WdColorIndex.wdBlack;
                    //odate.Format.SpaceAfter = 0;
                    //odate.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    //odate.Range.InsertParagraphAfter();


                    oLine = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oLine.Range.Text = "                                                                                                                                                                                                   ";//"日期：2012-3-3~2012-3-9";
                    oLine.Range.Font.Size = 5f;
                    oLine.Range.Font.Bold = 0;
                    oLine.Range.Font.Underline = WdUnderline.wdUnderlineThick;
                    oLine.Range.Font.UnderlineColor = WdColor.wdColorRed;
                    oLine.Range.Font.Name = "宋体";
                    oLine.Range.Font.ColorIndex = WdColorIndex.wdBlack;
                    oLine.Format.SpaceAfter = 0;
                    oLine.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    oLine.Range.InsertParagraphAfter();

                    Word.Paragraph oPara1;

                    oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oPara1.LineSpacing = 15f;//设置文档的行间距
                    oPara1.SpaceBefore = float.Parse("0");
                    oPara1.SpaceAfter = float.Parse("0");
                    oPara1.Range.Text = "一、工程总体情况";
                    oPara1.Range.Font.Size = 16f;
                    oPara1.Range.Font.Name = "宋体";
                    oPara1.Range.Font.Bold = 1;
                    oPara1.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                    oPara1.Range.Font.ColorIndex = WdColorIndex.wdBlack;
                    oPara1.Format.SpaceAfter = 0;
                    oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    oPara1.Range.InsertParagraphAfter();

                    Word.Paragraph oPara2;
                    object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara2 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara2.LineSpacing = 15f;
                    oPara2.SpaceBefore = float.Parse("0");
                    oPara2.SpaceAfter = float.Parse("0");
                    oPara2.Range.Font.Size = 14f;
                    oPara2.Range.Font.Name = "宋体";
                    oPara2.Range.Text = "1、钻井";
                    oPara2.Format.SpaceAfter = 0;
                    oPara2.Range.InsertParagraphAfter();

                    //Insert a 7 x 9table, fill it with data, and make the first row
                    //bold and italic.
                    Word.Table oTable_zj;
                    Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_zj = oDoc.Tables.Add(wrdRng, 7, 9, ref oMissing, ref oMissing);
                    oTable_zj.Range.ParagraphFormat.SpaceAfter = 0;

                    //加粗外边框
                    //oTable_zj.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleThickThinLargeGap;
                    oTable_zj.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                    oTable_zj.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_zj.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    oTable_zj.Columns[1].Width = 45f;
                    oTable_zj.Columns[2].Width = 45f;
                    oTable_zj.Columns[3].Width = 40f;
                    oTable_zj.Columns[4].Width = 45f;
                    oTable_zj.Columns[5].Width = 45f;
                    oTable_zj.Columns[6].Width = 45f;
                    oTable_zj.Columns[7].Width = 55f;
                    oTable_zj.Columns[8].Width = 35f;
                    oTable_zj.Columns[9].Width = 55f;

                    oTable_zj.Rows[1].Range.Font.Bold = 1;
                    oTable_zj.Rows[2].Range.Font.Bold = 0;
                    oTable_zj.Rows[3].Range.Font.Bold = 0;
                    oTable_zj.Rows[4].Range.Font.Bold = 0;
                    oTable_zj.Rows[5].Range.Font.Bold = 0;
                    oTable_zj.Rows[6].Range.Font.Bold = 0;
                    oTable_zj.Rows[7].Range.Font.Bold = 0;

                    oTable_zj.Rows[1].Height = 22f;
                    oTable_zj.Rows[2].Height = 22f;
                    oTable_zj.Rows[3].Height = 22f;
                    oTable_zj.Rows[4].Height = 22f;
                    oTable_zj.Rows[5].Height = 22f;
                    oTable_zj.Rows[6].Height = 22f;
                    oTable_zj.Rows[7].Height = 22f;

                    oTable_zj.Range.Font.Size = 9f;

                    //oTable1.Rows[1].Range.Font.Italic = 1;

                    oTable_zj.Cell(1, 1).Range.Text = "类别";

                    oTable_zj.Cell(1, 2).Range.Text = "区块";
                    oTable_zj.Columns[3].SetWidth(50, WdRulerStyle.wdAdjustNone);
                    oTable_zj.Columns[4].SetWidth(50, WdRulerStyle.wdAdjustNone);
                    oTable_zj.Columns[5].SetWidth(50, WdRulerStyle.wdAdjustNone);
                    oTable_zj.Columns[6].SetWidth(50, WdRulerStyle.wdAdjustNone);
                    oTable_zj.Columns[7].SetWidth(50, WdRulerStyle.wdAdjustNone);
                    oTable_zj.Cell(1, 3).Range.Text = "钻 机（台）";
                    oTable_zj.Cell(1, 4).Range.Text = strWanjing;
                    oTable_zj.Cell(1, 5).Range.Text = "年完井（口）";
                    oTable_zj.Cell(1, 6).Range.Text = strJinchi;
                    oTable_zj.Cell(1, 7).Range.Text = "年进尺（米）";
                    oTable_zj.Cell(1, 8).Range.Text = "正 钻（口）";
                    oTable_zj.Cell(1, 9).Range.Text = "备注";

                    //单元格合并
                    Word.Cell cell_zj_kt = oTable_zj.Cell(2, 1);
                    cell_zj_kt.Merge(oTable_zj.Cell(3, 1));
                    cell_zj_kt.Merge(oTable_zj.Cell(4, 1));


                    Word.Cell cell_zj_kf = oTable_zj.Cell(5, 1);
                    cell_zj_kf.Merge(oTable_zj.Cell(6, 1));
                    cell_zj_kf.Merge(oTable_zj.Cell(7, 1));

                    oTable_zj.Cell(2, 1).Range.Text = "勘探";
                    oTable_zj.Cell(5, 1).Range.Text = "开发";


                    string strData = dataZuanjing.Text;
                    string[] saRecs = strData.Split(fgc_record);

                    string[] values = saRecs[0].Split(fgc_field);
                    oTable_zj.Cell(2, 2).Range.Text = "韩城";
                    oTable_zj.Cell(2, 3).Range.Text = values[0];
                    oTable_zj.Cell(2, 4).Range.Text = values[1];
                    oTable_zj.Cell(2, 5).Range.Text = values[2];
                    oTable_zj.Cell(2, 6).Range.Text = values[3];
                    oTable_zj.Cell(2, 7).Range.Text = values[4];
                    oTable_zj.Cell(2, 8).Range.Text = values[5];
                    oTable_zj.Cell(2, 9).Range.Text = values[6];

                    values = saRecs[1].Split(fgc_field);
                    oTable_zj.Cell(3, 2).Range.Text = "临汾";
                    oTable_zj.Cell(3, 3).Range.Text = values[0];
                    oTable_zj.Cell(3, 4).Range.Text = values[1];
                    oTable_zj.Cell(3, 5).Range.Text = values[2];
                    oTable_zj.Cell(3, 6).Range.Text = values[3];
                    oTable_zj.Cell(3, 7).Range.Text = values[4];
                    oTable_zj.Cell(3, 8).Range.Text = values[5];
                    oTable_zj.Cell(3, 9).Range.Text = values[6];

                    values = saRecs[2].Split(fgc_field);
                    oTable_zj.Cell(4, 2).Range.Text = "忻州";
                    oTable_zj.Cell(4, 3).Range.Text = values[0];
                    oTable_zj.Cell(4, 4).Range.Text = values[1];
                    oTable_zj.Cell(4, 5).Range.Text = values[2];
                    oTable_zj.Cell(4, 6).Range.Text = values[3];
                    oTable_zj.Cell(4, 7).Range.Text = values[4];
                    oTable_zj.Cell(4, 8).Range.Text = values[5];
                    oTable_zj.Cell(4, 9).Range.Text = values[6];

                    values = saRecs[3].Split(fgc_field);
                    oTable_zj.Cell(5, 2).Range.Text = "韩城";
                    oTable_zj.Cell(5, 3).Range.Text = values[0];
                    oTable_zj.Cell(5, 4).Range.Text = values[1];
                    oTable_zj.Cell(5, 5).Range.Text = values[2];
                    oTable_zj.Cell(5, 6).Range.Text = values[3];
                    oTable_zj.Cell(5, 7).Range.Text = values[4];
                    oTable_zj.Cell(5, 8).Range.Text = values[5];
                    oTable_zj.Cell(5, 9).Range.Text = values[6];

                    values = saRecs[4].Split(fgc_field);
                    oTable_zj.Cell(6, 2).Range.Text = "临汾";
                    oTable_zj.Cell(6, 3).Range.Text = values[0];
                    oTable_zj.Cell(6, 4).Range.Text = values[1];
                    oTable_zj.Cell(6, 5).Range.Text = values[2];
                    oTable_zj.Cell(6, 6).Range.Text = values[3];
                    oTable_zj.Cell(6, 7).Range.Text = values[4];
                    oTable_zj.Cell(6, 8).Range.Text = values[5];
                    oTable_zj.Cell(6, 9).Range.Text = values[6];

                    values = saRecs[5].Split(fgc_field);
                    oTable_zj.Cell(7, 2).Range.Text = "忻州";
                    oTable_zj.Cell(7, 3).Range.Text = values[0];
                    oTable_zj.Cell(7, 4).Range.Text = values[1];
                    oTable_zj.Cell(7, 5).Range.Text = values[2];
                    oTable_zj.Cell(7, 6).Range.Text = values[3];
                    oTable_zj.Cell(7, 7).Range.Text = values[4];
                    oTable_zj.Cell(7, 8).Range.Text = values[5];
                    oTable_zj.Cell(7, 9).Range.Text = values[6];

                    oTable_zj.Borders.Enable = 1;

                    Word.Paragraph oPara_yl;
                    oPara_yl = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oPara_yl.LineSpacing = 15f;//设置文档的行间距
                    oPara_yl.SpaceBefore = float.Parse("0");
                    oPara_yl.SpaceAfter = float.Parse("0");
                    oPara_yl.Range.Font.Size = 14f;
                    oPara_yl.Range.Font.Name = "宋体";
                    oPara_yl.Range.Text = "2、压裂";
                    //oPara_yl.Format.SpaceAfter = 10;
                    oPara_yl.Range.InsertParagraphAfter();

                    Word.Table oTable_yl;
                    Word.Range wrdRng_yl = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_yl = oDoc.Tables.Add(wrdRng_yl, 8, 9, ref oMissing, ref oMissing);
                    oTable_yl.Range.ParagraphFormat.SpaceAfter = 0;
                    oTable_yl.Range.Font.Size = 9f;

                    oTable_yl.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    oTable_yl.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_yl.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    oTable_yl.Columns[1].Width = 45f;
                    oTable_yl.Columns[2].Width = 45f;
                    oTable_yl.Columns[3].Width = 45f;
                    oTable_yl.Columns[4].Width = 50f;
                    oTable_yl.Columns[5].Width = 50f;
                    oTable_yl.Columns[6].Width = 50f;
                    oTable_yl.Columns[7].Width = 50f;
                    oTable_yl.Columns[8].Width = 40f;
                    oTable_yl.Columns[9].Width = 55f;

                    oTable_yl.Rows[1].Range.Font.Bold = 1;
                    oTable_yl.Rows[2].Range.Font.Bold = 1;
                    oTable_yl.Rows[3].Range.Font.Bold = 0;
                    oTable_yl.Rows[4].Range.Font.Bold = 0;
                    oTable_yl.Rows[5].Range.Font.Bold = 0;
                    oTable_yl.Rows[6].Range.Font.Bold = 0;
                    oTable_yl.Rows[7].Range.Font.Bold = 0;
                    oTable_yl.Rows[8].Range.Font.Bold = 0;

                    oTable_yl.Rows[1].Height = 22f;
                    oTable_yl.Rows[2].Height = 22f;
                    oTable_yl.Rows[3].Height = 22f;
                    oTable_yl.Rows[4].Height = 22f;
                    oTable_yl.Rows[5].Height = 22f;
                    oTable_yl.Rows[6].Height = 22f;
                    oTable_yl.Rows[7].Height = 22f;

                    Word.Cell cell_yl_cls = oTable_yl.Cell(1, 1);
                    cell_yl_cls.Merge(oTable_yl.Cell(2, 1));

                    Word.Cell cell_yl_dis = oTable_yl.Cell(1, 2);
                    cell_yl_dis.Merge(oTable_yl.Cell(2, 2));
                    oTable_yl.Cell(1, 1).Range.Text = "类别";
                    oTable_yl.Cell(1, 2).Range.Text = "区块";

                    Word.Cell cell_yl_w = oTable_yl.Cell(1, 3);
                    cell_yl_w.Merge(oTable_yl.Cell(1, 5));
                    cell_yl_w.Range.Text = strYalie;
                    oTable_yl.Cell(2, 3).Range.Text = "口";
                    oTable_yl.Cell(2, 4).Range.Text = "次";
                    oTable_yl.Cell(2, 5).Range.Text = "层";


                    Word.Cell cell_yl_y = oTable_yl.Cell(1, 4);
                    cell_yl_y.Merge(oTable_yl.Cell(1, 6));
                    cell_yl_y.Range.Text = "年压裂";
                    oTable_yl.Cell(2, 6).Range.Text = "口";
                    oTable_yl.Cell(2, 7).Range.Text = "次";
                    oTable_yl.Cell(2, 8).Range.Text = "层";

                    Word.Cell cell_yl_remark = oTable_yl.Cell(1, 5);
                    cell_yl_remark.Merge(oTable_yl.Cell(2, 9));
                    cell_yl_remark.Range.Text = "备注";

                    Word.Cell cell_yl_kt = oTable_yl.Cell(3, 1);
                    cell_yl_kt.Merge(oTable_yl.Cell(4, 1));
                    cell_yl_kt.Merge(oTable_yl.Cell(5, 1));
                    oTable_yl.Cell(3, 1).Range.Text = "勘探";
                    oTable_yl.Cell(3, 2).Range.Text = "韩城";
                    oTable_yl.Cell(4, 2).Range.Text = "临汾";
                    oTable_yl.Cell(5, 2).Range.Text = "忻州";

                    Word.Cell cell_yl_kf = oTable_yl.Cell(6, 1);
                    cell_yl_kf.Merge(oTable_yl.Cell(7, 1));
                    cell_yl_kf.Merge(oTable_yl.Cell(8, 1));
                    oTable_yl.Cell(6, 1).Range.Text = "开发";
                    oTable_yl.Cell(6, 2).Range.Text = "韩城";
                    oTable_yl.Cell(7, 2).Range.Text = "临汾";
                    oTable_yl.Cell(8, 2).Range.Text = "忻州";

                    strData = dataYalie.Text;
                    saRecs = strData.Split(fgc_record);

                    values = saRecs[0].Split(fgc_field);
                    oTable_yl.Cell(3, 2).Range.Text = "韩城";
                    oTable_yl.Cell(3, 3).Range.Text = values[0];
                    oTable_yl.Cell(3, 4).Range.Text = values[1];
                    oTable_yl.Cell(3, 5).Range.Text = values[2];
                    oTable_yl.Cell(3, 6).Range.Text = values[3];
                    oTable_yl.Cell(3, 7).Range.Text = values[4];
                    oTable_yl.Cell(3, 8).Range.Text = values[5];
                    oTable_yl.Cell(3, 9).Range.Text = values[6];

                    values = saRecs[1].Split(fgc_field);
                    oTable_yl.Cell(4, 2).Range.Text = "临汾";
                    oTable_yl.Cell(4, 3).Range.Text = values[0];
                    oTable_yl.Cell(4, 4).Range.Text = values[1];
                    oTable_yl.Cell(4, 5).Range.Text = values[2];
                    oTable_yl.Cell(4, 6).Range.Text = values[3];
                    oTable_yl.Cell(4, 7).Range.Text = values[4];
                    oTable_yl.Cell(4, 8).Range.Text = values[5];
                    oTable_yl.Cell(4, 9).Range.Text = values[6];

                    values = saRecs[2].Split(fgc_field);
                    oTable_yl.Cell(5, 2).Range.Text = "忻州";
                    oTable_yl.Cell(5, 3).Range.Text = values[0];
                    oTable_yl.Cell(5, 4).Range.Text = values[1];
                    oTable_yl.Cell(5, 5).Range.Text = values[2];
                    oTable_yl.Cell(5, 6).Range.Text = values[3];
                    oTable_yl.Cell(5, 7).Range.Text = values[4];
                    oTable_yl.Cell(5, 8).Range.Text = values[5];
                    oTable_yl.Cell(5, 9).Range.Text = values[6];

                    values = saRecs[3].Split(fgc_field);
                    oTable_yl.Cell(6, 2).Range.Text = "韩城";
                    oTable_yl.Cell(6, 3).Range.Text = values[0];
                    oTable_yl.Cell(6, 4).Range.Text = values[1];
                    oTable_yl.Cell(6, 5).Range.Text = values[2];
                    oTable_yl.Cell(6, 6).Range.Text = values[3];
                    oTable_yl.Cell(6, 7).Range.Text = values[4];
                    oTable_yl.Cell(6, 8).Range.Text = values[5];
                    oTable_yl.Cell(6, 9).Range.Text = values[6];

                    values = saRecs[4].Split(fgc_field);
                    oTable_yl.Cell(7, 2).Range.Text = "临汾";
                    oTable_yl.Cell(7, 3).Range.Text = values[0];
                    oTable_yl.Cell(7, 4).Range.Text = values[1];
                    oTable_yl.Cell(7, 5).Range.Text = values[2];
                    oTable_yl.Cell(7, 6).Range.Text = values[3];
                    oTable_yl.Cell(7, 7).Range.Text = values[4];
                    oTable_yl.Cell(7, 8).Range.Text = values[5];
                    oTable_yl.Cell(7, 9).Range.Text = values[6];

                    values = saRecs[5].Split(fgc_field);
                    oTable_yl.Cell(8, 2).Range.Text = "忻州";
                    oTable_yl.Cell(8, 3).Range.Text = values[0];
                    oTable_yl.Cell(8, 4).Range.Text = values[1];
                    oTable_yl.Cell(8, 5).Range.Text = values[2];
                    oTable_yl.Cell(8, 6).Range.Text = values[3];
                    oTable_yl.Cell(8, 7).Range.Text = values[4];
                    oTable_yl.Cell(8, 8).Range.Text = values[5];
                    oTable_yl.Cell(8, 9).Range.Text = values[6];

                    oTable_yl.Borders.Enable = 1;

                    Word.Paragraph oPara3;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara3 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara3.LineSpacing = 15f;//设置文档的行间距
                    oPara3.SpaceBefore = float.Parse("0");
                    oPara3.SpaceAfter = float.Parse("0");
                    oPara3.Range.Text = "二、关键环节监督及质量分析";
                    oPara3.Range.Font.Bold = 1;
                    oPara3.Range.Font.Name = "宋体";
                    oPara3.Range.Font.Size = 16f;
                    //oPara3.Format.SpaceAfter = 2;
                    oPara3.Range.InsertParagraphAfter();

                    Word.Paragraph oPara4;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara4 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara4.LineSpacing = 15f;//设置文档的行间距
                    oPara4.SpaceBefore = float.Parse("0");
                    oPara4.SpaceAfter = float.Parse("0");
                    oPara4.Range.Font.Size = 14f;
                    oPara4.Range.Font.Bold = 1;
                    oPara4.Range.Text = "1、钻井";
                    //oPara4.Format.SpaceAfter = 2;
                    oPara4.Range.InsertParagraphAfter();



                    Word.Paragraph oPara11;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara11 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara11.LineSpacing = 10f;//设置文档的行间距
                    oPara11.SpaceBefore = 0f;
                    oPara11.SpaceAfter = 0f;
                    oPara11.Range.Font.Size = 12f;
                    oPara11.Range.Text = "(1)开钻验收";
                    //oPara11.Format.SpaceAfter = 0;
                    oPara11.Range.Font.Bold = 0;
                    oPara11.Range.InsertParagraphAfter();

                    Word.Paragraph oPara12;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara12 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara12.LineSpacing = 10f;//设置文档的行间距
                    oPara12.SpaceBefore = 0f;
                    oPara12.SpaceAfter = float.Parse("0");
                    oPara12.Range.Font.Size = 12f;

                    strData = dataKzys.Text;
                    saRecs = strData.Split(fgc_record);
                    int iDataCount = saRecs.Length - 1;
                    oPara12.Range.Text = saRecs[iDataCount] + "不合格情况详见下表。";
                    oPara12.Format.SpaceAfter = 0;
                    oPara12.Range.Font.Bold = 0;
                    //oPara12.Range.InsertParagraphAfter();

                    Word.Table oTable_yierkai;
                    Word.Range wrdRng_yierkai = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_yierkai = oDoc.Tables.Add(wrdRng_yierkai, iDataCount + 1, 10, ref oMissing, ref oMissing);
                    //oTable_yierkai.Range.ParagraphFormat.SpaceAfter = 6;
                    oTable_yierkai.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;//表格居中
                    oTable_yierkai.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_yierkai.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    oTable_yierkai.Rows[1].Range.Font.Bold = 1;
                    oTable_yierkai.Range.Font.Size = 9f;

                    oTable_yierkai.Columns[1].Width = 30f;
                    oTable_yierkai.Columns[2].Width = 30f;
                    oTable_yierkai.Columns[3].Width = 40f;
                    oTable_yierkai.Columns[4].Width = 55f;
                    oTable_yierkai.Columns[5].Width = 35f;
                    oTable_yierkai.Columns[6].Width = 50f;
                    oTable_yierkai.Columns[7].Width = 50f;
                    oTable_yierkai.Columns[8].Width = 120f;
                    oTable_yierkai.Columns[9].Width = 55f;
                    oTable_yierkai.Columns[10].Width = 30f;

                    oTable_yierkai.Cell(1, 1).Range.Text = "序号";
                    oTable_yierkai.Cell(1, 2).Range.Text = "区块";
                    oTable_yierkai.Cell(1, 3).Range.Text = "井号";
                    oTable_yierkai.Cell(1, 4).Range.Text = "施工队伍";
                    oTable_yierkai.Cell(1, 5).Range.Text = "监督";
                    oTable_yierkai.Cell(1, 6).Range.Text = "一开验收时间";
                    oTable_yierkai.Cell(1, 7).Range.Text = "二开验收时间";
                    oTable_yierkai.Cell(1, 8).Range.Text = "不合格因素（人员、证件、设备等）";
                    oTable_yierkai.Cell(1, 9).Range.Text = "处理措施";
                    oTable_yierkai.Cell(1, 10).Range.Text = "备注";

                    oTable_yierkai.Borders.Enable = 1;

                    for (int i = 0; i < iDataCount; i++)
                    {
                        oTable_yierkai.Rows[i + 2].Range.Font.Bold = 0;
                        values = saRecs[i].Split(fgc_field);
                        oTable_yierkai.Cell(i + 2, 1).Range.Text = (i + 1).ToString();
                        oTable_yierkai.Cell(i + 2, 2).Range.Text = values[1];
                        oTable_yierkai.Cell(i + 2, 3).Range.Text = values[2];
                        oTable_yierkai.Cell(i + 2, 4).Range.Text = values[3];
                        oTable_yierkai.Cell(i + 2, 5).Range.Text = values[4];
                        oTable_yierkai.Cell(i + 2, 6).Range.Text = values[5].Split(' ')[0];
                        oTable_yierkai.Cell(i + 2, 7).Range.Text = values[6].Split(' ')[0];
                        oTable_yierkai.Cell(i + 2, 8).Range.Text = values[7];
                        oTable_yierkai.Cell(i + 2, 9).Range.Text = values[8];
                        oTable_yierkai.Cell(i + 2, 10).Range.Text = values[9];
                    }

                    Word.Paragraph oPara13;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara13 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara13.LineSpacing = 10f;//设置文档的行间距
                    oPara13.SpaceBefore = float.Parse("0");
                    oPara13.SpaceAfter = float.Parse("0");
                    oPara13.Range.Font.Size = 12f;
                    oPara13.Range.Text = "(2)井身质量";
                    oPara13.Format.SpaceAfter = 2;
                    oPara13.Range.InsertParagraphAfter();


                    Word.Paragraph oPara14;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara14 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara14.LineSpacing = 10f;//设置文档的行间距
                    oPara14.SpaceBefore = float.Parse("0");
                    oPara14.SpaceAfter = float.Parse("0");
                    oPara14.Range.Font.Size = 12f;

                    strData = dataJszl.Text;
                    saRecs = strData.Split(fgc_record);
                    iDataCount = saRecs.Length - 1;
                    oPara14.Range.Text = saRecs[iDataCount] + "超标情况详见下表。";
                    //oPara14.Format.SpaceAfter = 2;

                    Word.Table oTable_jszl;
                    Word.Range wrdRng_jszl = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_jszl = oDoc.Tables.Add(wrdRng_jszl, iDataCount + 1, 8, ref oMissing, ref oMissing);
                    //oTable_jszl.Range.ParagraphFormat.SpaceAfter = 6;
                    oTable_jszl.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
                    oTable_jszl.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_jszl.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    oTable_jszl.Rows[1].Range.Font.Bold = 1;
                    oTable_jszl.Range.Font.Size = 9f;

                    oTable_jszl.Columns[1].Width = 40f;
                    oTable_jszl.Columns[2].Width = 40f;
                    oTable_jszl.Columns[3].Width = 50f;
                    oTable_jszl.Columns[4].Width = 60f;
                    oTable_jszl.Columns[5].Width = 40f;
                    oTable_jszl.Columns[6].Width = 140f;
                    oTable_jszl.Columns[7].Width = 75f;
                    oTable_jszl.Columns[8].Width = 50f;


                    oTable_jszl.Cell(1, 1).Range.Text = "序号";
                    oTable_jszl.Cell(1, 2).Range.Text = "区块";
                    oTable_jszl.Cell(1, 3).Range.Text = "井号";
                    oTable_jszl.Cell(1, 4).Range.Text = "施工队伍";
                    oTable_jszl.Cell(1, 5).Range.Text = "监督";
                    oTable_jszl.Cell(1, 6).Range.Text = "超标因素（井斜、狗腿度、方位、位移等）";
                    oTable_jszl.Cell(1, 7).Range.Text = "处理措施";
                    oTable_jszl.Cell(1, 8).Range.Text = "备注";

                    for (int i = 0; i < iDataCount; i++)
                    {
                        oTable_jszl.Rows[i + 2].Range.Font.Bold = 0;
                        values = saRecs[i].Split(fgc_field);
                        oTable_jszl.Cell(i + 2, 1).Range.Text = (i + 1).ToString();
                        oTable_jszl.Cell(i + 2, 2).Range.Text = values[1];
                        oTable_jszl.Cell(i + 2, 3).Range.Text = values[2];
                        oTable_jszl.Cell(i + 2, 4).Range.Text = values[3];
                        oTable_jszl.Cell(i + 2, 5).Range.Text = values[4];
                        oTable_jszl.Cell(i + 2, 6).Range.Text = values[5];
                        oTable_jszl.Cell(i + 2, 7).Range.Text = values[6];
                        oTable_jszl.Cell(i + 2, 8).Range.Text = values[7];
                    }

                    oTable_jszl.Borders.Enable = 1;

                    Word.Paragraph oPara15;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara15 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara15.LineSpacing = 10f;//设置文档的行间距
                    oPara15.SpaceBefore = float.Parse("0");
                    oPara15.SpaceAfter = float.Parse("0");
                    oPara15.Range.Font.Size = 12f;
                    oPara15.Range.Text = "(3)下套管作业";
                    //oPara15.Format.SpaceAfter = 2;
                    oPara15.Range.InsertParagraphAfter();


                    Word.Paragraph oPara16;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara16 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara16.LineSpacing = 10f;//设置文档的行间距
                    oPara16.SpaceBefore = float.Parse("0");
                    oPara16.SpaceAfter = float.Parse("0");
                    oPara16.Range.Font.Size = 12f;

                    strData = dataXtgzy.Text;
                    saRecs = strData.Split(fgc_record);
                    iDataCount = saRecs.Length - 1;
                    oPara16.Range.Text = saRecs[iDataCount] + "异常情况详见下表。";
                    //oPara16.Format.SpaceAfter = 2;

                    Word.Table oTable_xtgzy;
                    Word.Range wrdRng_xtgzy = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_xtgzy = oDoc.Tables.Add(wrdRng_xtgzy, iDataCount + 1, 8, ref oMissing, ref oMissing);
                    //oTable_xtgzy.Range.ParagraphFormat.SpaceAfter = 6;
                    oTable_xtgzy.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
                    oTable_xtgzy.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_xtgzy.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    oTable_xtgzy.Rows[1].Range.Font.Bold = 1;
                    oTable_xtgzy.Range.Font.Size = 9f;

                    oTable_xtgzy.Columns[1].Width = 30f;
                    oTable_xtgzy.Columns[2].Width = 30f;
                    oTable_xtgzy.Columns[3].Width = 60f;
                    oTable_xtgzy.Columns[4].Width = 55f;
                    oTable_xtgzy.Columns[5].Width = 50f;
                    oTable_xtgzy.Columns[6].Width = 145f;
                    oTable_xtgzy.Columns[7].Width = 75f;
                    oTable_xtgzy.Columns[8].Width = 50f;

                    oTable_xtgzy.Cell(1, 1).Range.Text = "序号";
                    oTable_xtgzy.Cell(1, 2).Range.Text = "区块";
                    oTable_xtgzy.Cell(1, 3).Range.Text = "井号";
                    oTable_xtgzy.Cell(1, 4).Range.Text = "施工队伍";
                    oTable_xtgzy.Cell(1, 5).Range.Text = "监督";
                    oTable_xtgzy.Cell(1, 6).Range.Text = "异常情况（套管单、现场目测等）";
                    oTable_xtgzy.Cell(1, 7).Range.Text = "处理措施";
                    oTable_xtgzy.Cell(1, 8).Range.Text = "备注";

                    for (int i = 0; i < iDataCount; i++)
                    {
                        oTable_xtgzy.Rows[i + 2].Range.Font.Bold = 0;
                        values = saRecs[i].Split(fgc_field);
                        oTable_xtgzy.Cell(i + 2, 1).Range.Text = (i + 1).ToString();
                        oTable_xtgzy.Cell(i + 2, 2).Range.Text = values[1];
                        oTable_xtgzy.Cell(i + 2, 3).Range.Text = values[2];
                        oTable_xtgzy.Cell(i + 2, 4).Range.Text = values[3];
                        oTable_xtgzy.Cell(i + 2, 5).Range.Text = values[4];
                        oTable_xtgzy.Cell(i + 2, 6).Range.Text = values[5];
                        oTable_xtgzy.Cell(i + 2, 7).Range.Text = values[6];
                        oTable_xtgzy.Cell(i + 2, 8).Range.Text = values[7];

                    }

                    oTable_xtgzy.Borders.Enable = 1;

                    Word.Paragraph oPara17;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara17 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara17.LineSpacing = 10f;//设置文档的行间距
                    oPara17.SpaceBefore = float.Parse("0");
                    oPara17.SpaceAfter = float.Parse("0");
                    oPara17.Range.Font.Size = 12f;
                    oPara17.Range.Text = "(4)固井作业";
                    //oPara17.Format.SpaceAfter = 2;
                    oPara17.Range.InsertParagraphAfter();


                    Word.Paragraph oPara18;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara18 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara18.LineSpacing = 10f;//设置文档的行间距
                    oPara18.SpaceBefore = float.Parse("0");
                    oPara18.SpaceAfter = float.Parse("0");
                    oPara18.Range.Font.Size = 12f;

                    strData = dataGjzy.Text;
                    saRecs = strData.Split(fgc_record);
                    iDataCount = saRecs.Length - 1;
                    oPara18.Range.Text = saRecs[iDataCount] + "异常情况详见下表。";
                    //oPara18.Format.SpaceAfter = 2;

                    Word.Table oTable_gjzy;
                    Word.Range wrdRng_gjzy = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_gjzy = oDoc.Tables.Add(wrdRng_gjzy, iDataCount + 1, 8, ref oMissing, ref oMissing);
                    //oTable_gjzy.Range.ParagraphFormat.SpaceAfter = 6;
                    oTable_gjzy.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
                    oTable_gjzy.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_gjzy.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    oTable_gjzy.Rows[1].Range.Font.Bold = 1;
                    oTable_gjzy.Range.Font.Size = 9f;

                    oTable_gjzy.Columns[1].Width = 30f;
                    oTable_gjzy.Columns[2].Width = 30f;
                    oTable_gjzy.Columns[3].Width = 60f;
                    oTable_gjzy.Columns[4].Width = 55f;
                    oTable_gjzy.Columns[5].Width = 50f;
                    oTable_gjzy.Columns[6].Width = 145f;
                    oTable_gjzy.Columns[7].Width = 75f;
                    oTable_gjzy.Columns[8].Width = 50f;

                    oTable_gjzy.Cell(1, 1).Range.Text = "序号";
                    oTable_gjzy.Cell(1, 2).Range.Text = "区块";
                    oTable_gjzy.Cell(1, 3).Range.Text = "井号";
                    oTable_gjzy.Cell(1, 4).Range.Text = "施工队伍";
                    oTable_gjzy.Cell(1, 5).Range.Text = "监督";
                    oTable_gjzy.Cell(1, 6).Range.Text = "异常情况（蹩压、失返、诸将量不足等）";
                    oTable_gjzy.Cell(1, 7).Range.Text = "处理措施";
                    oTable_gjzy.Cell(1, 8).Range.Text = "备注";

                    for (int i = 0; i < iDataCount; i++)
                    {
                        oTable_gjzy.Rows[i + 2].Range.Font.Bold = 0;
                        values = saRecs[i].Split(fgc_field);
                        oTable_gjzy.Cell(i + 2, 1).Range.Text = (i + 1).ToString();
                        oTable_gjzy.Cell(i + 2, 2).Range.Text = values[1];
                        oTable_gjzy.Cell(i + 2, 3).Range.Text = values[2];
                        oTable_gjzy.Cell(i + 2, 4).Range.Text = values[3];
                        oTable_gjzy.Cell(i + 2, 5).Range.Text = values[4];
                        oTable_gjzy.Cell(i + 2, 6).Range.Text = values[5];
                        oTable_gjzy.Cell(i + 2, 7).Range.Text = values[6];
                        oTable_gjzy.Cell(i + 2, 8).Range.Text = values[7];
                    }

                    oTable_gjzy.Borders.Enable = 1;

                    Word.Paragraph oPara20;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara20 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara20.LineSpacing = 10f;//设置文档的行间距
                    oPara20.SpaceBefore = float.Parse("0");
                    oPara20.SpaceAfter = float.Parse("0");
                    oPara20.Range.Font.Size = 12f;
                    oPara20.Range.Text = "（5）处理井漏情况";

                    //oPara20.Format.SpaceAfter = 2;

                    strData = dataCljl.Text;
                    saRecs = strData.Split(fgc_record);
                    iDataCount = saRecs.Length - 1;

                    Word.Table oTable_cljl;
                    Word.Range wrdRng_cljl = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_cljl = oDoc.Tables.Add(wrdRng_cljl, iDataCount + 1, 10, ref oMissing, ref oMissing);
                    //oTable_cljl.Range.ParagraphFormat.SpaceAfter = 6;
                    oTable_cljl.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
                    oTable_cljl.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_cljl.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    oTable_cljl.Rows[1].Range.Font.Bold = 1;
                    oTable_cljl.Range.Font.Size = 9f;

                    oTable_cljl.Columns[1].Width = 30f;
                    oTable_cljl.Columns[2].Width = 30f;
                    oTable_cljl.Columns[3].Width = 45f;
                    oTable_cljl.Columns[4].Width = 55f;
                    oTable_cljl.Columns[5].Width = 45f;
                    oTable_cljl.Columns[6].Width = 65f;
                    oTable_cljl.Columns[7].Width = 65f;
                    oTable_cljl.Columns[8].Width = 65f;
                    oTable_cljl.Columns[9].Width = 65f;
                    oTable_cljl.Columns[10].Width = 30f;


                    oTable_cljl.Cell(1, 1).Range.Text = "序号";
                    oTable_cljl.Cell(1, 2).Range.Text = "区块";
                    oTable_cljl.Cell(1, 3).Range.Text = "井号";
                    oTable_cljl.Cell(1, 4).Range.Text = "施工队伍";
                    oTable_cljl.Cell(1, 5).Range.Text = "监督";
                    oTable_cljl.Cell(1, 6).Range.Text = "漏失层位";
                    oTable_cljl.Cell(1, 7).Range.Text = "漏失量";
                    oTable_cljl.Cell(1, 8).Range.Text = "处理措施";
                    oTable_cljl.Cell(1, 9).Range.Text = "处理结果";
                    oTable_cljl.Cell(1, 10).Range.Text = "备注";

                    for (int i = 0; i < iDataCount; i++)
                    {
                        oTable_cljl.Rows[i + 2].Range.Font.Bold = 0;
                        values = saRecs[i].Split(fgc_field);
                        oTable_cljl.Cell(i + 2, 1).Range.Text = (i + 1).ToString();
                        oTable_cljl.Cell(i + 2, 2).Range.Text = values[1];
                        oTable_cljl.Cell(i + 2, 3).Range.Text = values[2];
                        oTable_cljl.Cell(i + 2, 4).Range.Text = values[3];
                        oTable_cljl.Cell(i + 2, 5).Range.Text = values[4];
                        oTable_cljl.Cell(i + 2, 6).Range.Text = values[5];
                        oTable_cljl.Cell(i + 2, 7).Range.Text = values[6];
                        oTable_cljl.Cell(i + 2, 8).Range.Text = values[7];
                    }

                    oTable_cljl.Borders.Enable = 1;

                    Word.Paragraph oPara21;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara21 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara21.LineSpacing = 10f;//设置文档的行间距
                    oPara21.SpaceBefore = float.Parse("0");
                    oPara21.SpaceAfter = float.Parse("0");
                    oPara21.Range.Font.Size = 12f;
                    oPara21.Range.Text = "（6）处理涌水情况";
                    //oPara21.Format.SpaceAfter = 2;

                    strData = dataClys.Text;
                    saRecs = strData.Split(fgc_record);
                    iDataCount = saRecs.Length - 1;

                    Word.Table oTable_clys;
                    Word.Range wrdRng_clys = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_clys = oDoc.Tables.Add(wrdRng_clys, iDataCount + 1, 10, ref oMissing, ref oMissing);
                    //oTable_clys.Range.ParagraphFormat.SpaceAfter = 6;
                    oTable_clys.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
                    oTable_clys.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_clys.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    oTable_clys.Rows[1].Range.Font.Bold = 1;
                    oTable_clys.Range.Font.Size = 9f;

                    oTable_clys.Columns[1].Width = 30f;
                    oTable_clys.Columns[2].Width = 30f;
                    oTable_clys.Columns[3].Width = 45f;
                    oTable_clys.Columns[4].Width = 55f;
                    oTable_clys.Columns[5].Width = 45f;
                    oTable_clys.Columns[6].Width = 65f;
                    oTable_clys.Columns[7].Width = 65f;
                    oTable_clys.Columns[8].Width = 65f;
                    oTable_clys.Columns[9].Width = 65f;
                    oTable_clys.Columns[10].Width = 30f;


                    oTable_clys.Cell(1, 1).Range.Text = "序号";
                    oTable_clys.Cell(1, 2).Range.Text = "区块";
                    oTable_clys.Cell(1, 3).Range.Text = "井号";
                    oTable_clys.Cell(1, 4).Range.Text = "施工队伍";
                    oTable_clys.Cell(1, 5).Range.Text = "监督";
                    oTable_clys.Cell(1, 6).Range.Text = "涌水层位";
                    oTable_clys.Cell(1, 7).Range.Text = "涌水量";
                    oTable_clys.Cell(1, 8).Range.Text = "处理措施";
                    oTable_clys.Cell(1, 9).Range.Text = "处理结果";
                    oTable_clys.Cell(1, 10).Range.Text = "备注";

                    for (int i = 0; i < iDataCount; i++)
                    {
                        oTable_clys.Rows[i + 2].Range.Font.Bold = 0;
                        values = saRecs[i].Split(fgc_field);
                        oTable_clys.Cell(i + 2, 1).Range.Text = (i + 1).ToString();
                        oTable_clys.Cell(i + 2, 2).Range.Text = values[1];
                        oTable_clys.Cell(i + 2, 3).Range.Text = values[2];
                        oTable_clys.Cell(i + 2, 4).Range.Text = values[3];
                        oTable_clys.Cell(i + 2, 5).Range.Text = values[4];
                        oTable_clys.Cell(i + 2, 6).Range.Text = values[5];
                        oTable_clys.Cell(i + 2, 7).Range.Text = values[6];
                        oTable_clys.Cell(i + 2, 8).Range.Text = values[7];
                        oTable_clys.Cell(i + 2, 9).Range.Text = values[8];
                        oTable_clys.Cell(i + 2, 10).Range.Text = values[9];
                    }

                    oTable_clys.Borders.Enable = 1;

                    Word.Paragraph oPara22;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara22 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara22.LineSpacing = 5f;//设置文档的行间距
                    oPara22.SpaceBefore = float.Parse("0");
                    oPara22.SpaceAfter = float.Parse("0");
                    oPara22.Range.Font.Size = 14f;
                    oPara22.Range.Font.Name = "宋体";
                    oPara22.Range.Font.Bold = 1;
                    oPara22.Range.Text = "2、压裂进度及质量影响因素分析";
                    //oPara22.Format.SpaceAfter = 2;
                    oPara22.Range.InsertParagraphAfter();

                    Word.Paragraph oPara23;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara23 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara23.LineSpacing = 5f;//设置文档的行间距
                    oPara23.SpaceBefore = float.Parse("0");
                    oPara23.SpaceAfter = float.Parse("0");
                    oPara23.Range.Font.Size = 12f;
                    oPara23.Range.Font.Name = "宋体";
                    oPara23.Range.Font.Bold = 0;
                    oPara23.Range.Text = "（1）阻工因素";
                    //oPara22.Format.SpaceAfter = 2;
                    oPara23.Range.InsertParagraphAfter();

                    strData = dataZgys.Text;
                    saRecs = strData.Split(fgc_record);
                    iDataCount = saRecs.Length - 1;

                    Word.Table oTable_zugongyinsu;
                    Word.Range wrgRng_zugongyinsu = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_zugongyinsu = oDoc.Tables.Add(wrgRng_zugongyinsu, iDataCount + 1, 9, ref oMissing, ref oMissing);
                    oTable_zugongyinsu.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_zugongyinsu.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    oTable_zugongyinsu.Rows[1].Range.Font.Bold = 1;

                    for (int i = 0; i < iDataCount; i++)
                    {
                        oTable_zugongyinsu.Rows[i + 2].Range.Font.Bold = 0;
                    }

                    oTable_zugongyinsu.Range.Font.Size = 9f;

                    oTable_zugongyinsu.Cell(1, 1).Range.Text = "区块";
                    oTable_zugongyinsu.Cell(1, 2).Range.Text = "套管质量问题";
                    oTable_zugongyinsu.Cell(1, 3).Range.Text = "下雨及其影响天数";
                    oTable_zugongyinsu.Cell(1, 4).Range.Text = "工农关系影响天数";
                    oTable_zugongyinsu.Cell(1, 5).Range.Text = "车辆维修天数";
                    oTable_zugongyinsu.Cell(1, 6).Range.Text = "井场搬迁";
                    oTable_zugongyinsu.Cell(1, 7).Range.Text = "等待井台搬迁";
                    oTable_zugongyinsu.Cell(1, 8).Range.Text = "备水配液";
                    oTable_zugongyinsu.Cell(1, 9).Range.Text = "备注";

                    for (int i = 0; i < iDataCount; i++)
                    {
                        values = saRecs[i].Split(fgc_field);
                        //oTable_zugongyinsu.Cell(i+2, 1).Range.Text = "忻州";
                        oTable_zugongyinsu.Cell(i + 2, 1).Range.Text = values[1];
                        oTable_zugongyinsu.Cell(i + 2, 2).Range.Text = values[2];
                        oTable_zugongyinsu.Cell(i + 2, 3).Range.Text = values[3];
                        oTable_zugongyinsu.Cell(i + 2, 4).Range.Text = values[4];
                        oTable_zugongyinsu.Cell(i + 2, 5).Range.Text = values[5];
                        oTable_zugongyinsu.Cell(i + 2, 6).Range.Text = values[6];
                        oTable_zugongyinsu.Cell(i + 2, 7).Range.Text = values[7];
                    }

                    oTable_zugongyinsu.Borders.Enable = 1;

                    Word.Paragraph oPara24;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara24 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara24.LineSpacing = 5f;//设置文档的行间距
                    oPara24.Range.InsertParagraphBefore();
                    oPara24.SpaceBefore = float.Parse("0");
                    oPara24.SpaceAfter = float.Parse("0");
                    oPara24.Range.Font.Size = 12f;
                    oPara24.Range.Font.Name = "宋体";
                    oPara24.Range.Font.Bold = 0;
                    oPara24.Range.Text = "（2）入井材料";
                    //oPara24.Format.SpaceAfter = 2;
                    oPara24.Range.InsertParagraphAfter();

                    Word.Paragraph oPara25;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara25 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara25.LineSpacing = 5f;//设置文档的行间距
                    //oPara25.Range.InsertParagraphBefore();
                    oPara25.SpaceBefore = float.Parse("0");
                    oPara25.SpaceAfter = float.Parse("0");
                    oPara25.Range.Font.Size = 12f;
                    oPara25.Range.Font.Name = "宋体";
                    oPara25.Range.Font.Bold = 0;
                    oPara25.Range.Text = "通过各分公司现场监督检测，本周KCL、水浊度均符合施工要求。各压裂队加砂量如下表：";
                    //oPara25.Format.SpaceAfter = 2;
                    oPara25.Range.InsertParagraphAfter();

                    strData = dataRjcl.Text;
                    saRecs = strData.Split(fgc_record);
                    iDataCount = saRecs.Length - 1;

                    Word.Table oTable_rujingcailiao;
                    Word.Range wrgRng_rujingcailiao = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_rujingcailiao = oDoc.Tables.Add(wrgRng_rujingcailiao, iDataCount + 1, 6, ref oMissing, ref oMissing);
                    oTable_rujingcailiao.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_rujingcailiao.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    oTable_rujingcailiao.Rows[1].Range.Font.Bold = 1;

                    for (int i = 0; i < iDataCount; i++)
                    {
                        oTable_rujingcailiao.Rows[i + 2].Range.Font.Bold = 0;
                    }

                    oTable_rujingcailiao.Range.Font.Size = 9f;

                    oTable_rujingcailiao.Cell(1, 1).Range.Text = "区块";
                    oTable_rujingcailiao.Cell(1, 2).Range.Text = "压裂队";
                    oTable_rujingcailiao.Cell(1, 3).Range.Text = "100.00%";
                    oTable_rujingcailiao.Cell(1, 4).Range.Text = "90%-100%";
                    oTable_rujingcailiao.Cell(1, 5).Range.Text = "80%-90%";
                    oTable_rujingcailiao.Cell(1, 6).Range.Text = "80%以下";


                    for (int i = 0; i < iDataCount; i++)
                    {
                        values = saRecs[i].Split(fgc_field);
                        oTable_rujingcailiao.Cell(i + 2, 1).Range.Text = values[0];
                        oTable_rujingcailiao.Cell(i + 2, 2).Range.Text = values[1];
                        oTable_rujingcailiao.Cell(i + 2, 3).Range.Text = values[2];
                        oTable_rujingcailiao.Cell(i + 2, 4).Range.Text = values[3];
                        oTable_rujingcailiao.Cell(i + 2, 5).Range.Text = values[4];
                        oTable_rujingcailiao.Cell(i + 2, 6).Range.Text = values[5];
                    }

                    oTable_rujingcailiao.Borders.Enable = 1;

                    Word.Paragraph oPara26;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara26 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara26.LineSpacing = 5f;//设置文档的行间距
                    oPara26.Range.InsertParagraphBefore();
                    oPara26.SpaceBefore = float.Parse("0");
                    oPara26.SpaceAfter = float.Parse("0");
                    oPara26.Range.Font.Size = 12f;
                    oPara26.Range.Font.Name = "宋体";
                    oPara26.Range.Font.Bold = 0;
                    oPara26.Range.Text = "（3）施工质量";
                    //oPara26.Format.SpaceAfter = 2;
                    oPara26.Range.InsertParagraphAfter();

                    strData = dataSgzl.Text;
                    saRecs = strData.Split(fgc_record);
                    iDataCount = saRecs.Length - 1;

                    Word.Paragraph oPara27;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara27 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara27.LineSpacing = 5f;//设置文档的行间距
                    //oPara27.Range.InsertParagraphBefore();
                    oPara27.SpaceBefore = float.Parse("0");
                    oPara27.SpaceAfter = float.Parse("0");
                    oPara27.Range.Font.Size = 12f;
                    oPara27.Range.Font.Name = "宋体";
                    oPara27.Range.Font.Bold = 0;
                    oPara27.Range.Text = saRecs[iDataCount];
                    //oPara27.Format.SpaceAfter = 2;
                    oPara27.Range.InsertParagraphAfter();

                    Word.Table oTable_shigongzhiliang;

                    Word.Range wrgRng_shigongzhiliang = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_shigongzhiliang = oDoc.Tables.Add(wrgRng_shigongzhiliang, iDataCount + 1, 5, ref oMissing, ref oMissing);
                    oTable_shigongzhiliang.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_shigongzhiliang.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    oTable_shigongzhiliang.Rows[1].Range.Font.Bold = 1;

                    oTable_shigongzhiliang.Columns[1].Width = 80f;
                    oTable_shigongzhiliang.Columns[2].Width = 80f;
                    oTable_shigongzhiliang.Columns[3].Width = 80f;
                    oTable_shigongzhiliang.Columns[4].Width = 130f;
                    oTable_shigongzhiliang.Columns[5].Width = 80f;

                    for (int i = 0; i < iDataCount; i++)
                    {
                        oTable_shigongzhiliang.Rows[i + 2].Range.Font.Bold = 0;
                    }

                    oTable_shigongzhiliang.Range.Font.Size = 9f;
                    oTable_shigongzhiliang.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthAuto;

                    oTable_shigongzhiliang.Cell(1, 1).Range.Text = "区块";
                    oTable_shigongzhiliang.Cell(1, 2).Range.Text = "井号";
                    oTable_shigongzhiliang.Cell(1, 3).Range.Text = "层位";
                    oTable_shigongzhiliang.Cell(1, 4).Range.Text = "失败原因简述";
                    oTable_shigongzhiliang.Cell(1, 5).Range.Text = "备注";

                    for (int i = 0; i < iDataCount; i++)
                    {
                        values = saRecs[i].Split(fgc_field);
                        oTable_shigongzhiliang.Cell(i + 2, 1).Range.Text = values[0];
                        oTable_shigongzhiliang.Cell(i + 2, 2).Range.Text = values[1];
                        oTable_shigongzhiliang.Cell(i + 2, 3).Range.Text = values[2];
                        oTable_shigongzhiliang.Cell(i + 2, 4).Range.Text = values[3];
                        oTable_shigongzhiliang.Cell(i + 2, 5).Range.Text = values[4];
                    }

                    oTable_shigongzhiliang.Borders.Enable = 1;

                    Word.Paragraph oPara28;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara28 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara28.LineSpacing = 15f;//设置文档的行间距
                    oPara28.Range.InsertParagraphBefore();
                    oPara28.SpaceBefore = float.Parse("0");
                    oPara28.SpaceAfter = float.Parse("0");
                    oPara28.Range.Font.Size = 16f;
                    oPara28.Range.Font.Name = "宋体";
                    oPara28.Range.Font.Bold = 1;
                    oPara28.Range.Text = "三、技术研究及其他工作";
                    //oPara28.Format.SpaceAfter = 2;
                    oPara28.Range.InsertParagraphAfter();

                    Word.Paragraph oPara29;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara29 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara29.LineSpacing = 15f;//设置文档的行间距
                    //oPara29.Range.InsertParagraphBefore();
                    oPara29.SpaceBefore = float.Parse("0");
                    oPara29.SpaceAfter = float.Parse("0");
                    oPara29.Range.Font.Size = 14f;
                    oPara29.Range.Font.Name = "宋体";
                    oPara29.Range.Font.Bold = 1;
                    oPara29.Range.Text = "（一）韩城";
                    //oPara29.Format.SpaceAfter = 2;
                    oPara29.Range.InsertParagraphAfter();

                    Word.Paragraph oPara32;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara32 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara32.LineSpacing = 15f;//设置文档的行间距
                    //oPara32.Range.InsertParagraphBefore();
                    oPara32.SpaceBefore = float.Parse("0");
                    oPara32.SpaceAfter = float.Parse("0");
                    oPara32.Range.Font.Size = 12f;
                    oPara32.Range.Font.Name = "宋体";
                    oPara32.Range.Font.Bold = 0;
                    oPara32.Range.Text = txt_report_hc.Text;

                    //oPara32.Format.SpaceAfter = 2;
                    oPara32.Range.InsertParagraphAfter();

                    Word.Paragraph oPara30;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara30 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara30.LineSpacing = 15f;//设置文档的行间距
                    oPara30.SpaceBefore = float.Parse("0");
                    oPara30.SpaceAfter = float.Parse("0");
                    oPara30.Range.Font.Size = 14f;
                    oPara30.Range.Font.Name = "宋体";
                    oPara30.Range.Font.Bold = 1;
                    oPara30.Range.Text = "（二）临汾";
                    //oPara30.Format.SpaceAfter = 2;
                    oPara30.Range.InsertParagraphAfter();

                    Word.Paragraph oPara33;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara33 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara33.LineSpacing = 12f;//设置文档的行间距
                    oPara33.SpaceBefore = float.Parse("0");
                    oPara33.SpaceAfter = float.Parse("0");
                    oPara33.Range.Font.Size = 12f;
                    oPara33.Range.Font.Name = "宋体";
                    oPara33.Range.Font.Bold = 0;
                    oPara33.Range.Text = txt_report_lf.Text;

                    //oPara33.Format.SpaceAfter = 2;
                    oPara33.Range.InsertParagraphAfter();

                    Word.Paragraph oPara31;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara31 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara31.LineSpacing = 15f;//设置文档的行间距
                    oPara31.SpaceBefore = float.Parse("0");
                    oPara31.SpaceAfter = float.Parse("0");
                    oPara31.Range.Font.Size = 14f;
                    oPara31.Range.Font.Name = "宋体";
                    oPara31.Range.Font.Bold = 1;
                    oPara31.Range.Text = "（三）忻州";
                    //oPara28.Format.SpaceAfter = 2;
                    oPara31.Range.InsertParagraphAfter();

                    Word.Paragraph oPara34;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara34 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara34.LineSpacing = 15f;//设置文档的行间距
                    oPara34.SpaceBefore = float.Parse("0");
                    oPara34.SpaceAfter = float.Parse("0");
                    oPara34.Range.Font.Size = 12f;
                    oPara34.Range.Font.Name = "宋体";
                    oPara34.Range.Font.Bold = 0;
                    oPara34.Range.Text = txt_report_xz.Text;

                    string dstDir = Server.MapPath("../temp/" + userName + "/");

                    if (Directory.Exists(dstDir) == false)
                    {
                        try
                        {
                            Directory.CreateDirectory(dstDir);
                        }
                        catch (System.Exception e1)
                        {
                            this.Response.Write("<script language='JavaScript'>window.alert('服务器端IO操作异常'); </script>");
                            return;
                        }
                    }
                    if (File.Exists(dstDir + "质量简报.doc"))
                    {
                        try
                        {
                            File.Delete(dstDir + "质量简报.doc");
                        }
                        catch (System.Exception e2)
                        {

                        }
                    }

                    oDoc.SaveAs(dstDir + "/质量简报.doc");
                    oWord.Quit();
                    oDoc = null;
                    oWord = null;

                    Response.Redirect("../temp/" + userName + "/质量简报.doc");
                }
                else if (type == "nian")
                {
                    string[] ss = lblRiqi.Text.Split(',');
                    string begin = ss[0];
                    string end = ss[1];

                    string date = "（" + getDateCN(begin) + "～" + getDateCN(end) + "）";

                    string end_dt_w = end;
                    string start_dt = begin;
                    string start_dt_y = DateTime.Parse(end_dt_w).AddYears(-1).ToString();

                    object oMissing = System.Reflection.Missing.Value;
                    object oEndOfDoc = "\\endofdoc";
                    Word._Application oWord = new Word.Application();
                    oWord.DisplayAlerts = WdAlertLevel.wdAlertsNone;
                    Word._Document oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                    oWord.Visible = false;


                    Word.Paragraph otopleft, otopleft_second, otopleft_third, otopleft_fourth;

                    otopleft = oDoc.Content.Paragraphs.Add(ref oMissing);
                    otopleft.Range.Text = "内部刊物";
                    otopleft.Range.Font.Size = 12;
                    otopleft.Range.Font.Bold = 1;
                    otopleft.Range.Font.ColorIndex = WdColorIndex.wdRed;
                    otopleft.Format.SpaceAfter = 0; 
                    otopleft.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    otopleft.Range.InsertParagraphAfter();

                    otopleft_second = oDoc.Content.Paragraphs.Add(ref oMissing);
                    otopleft_second.Range.Text = "注意保存";
                    otopleft_second.Range.Font.Size = 12;
                    otopleft_second.Range.Font.Bold = 1;
                    otopleft_second.Range.Font.ColorIndex = WdColorIndex.wdRed;
                    otopleft_second.Format.SpaceAfter = 0; 
                    otopleft_second.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    otopleft_second.Range.InsertParagraphAfter();

                    otopleft_third = oDoc.Content.Paragraphs.Add(ref oMissing);
                    otopleft_fourth = oDoc.Content.Paragraphs.Add(ref oMissing);


                    Word.Paragraph oParaTitle, oversion, oParaSubTitle, obelow, obelow_second, odate, oLine;
                    oParaTitle = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaTitle.Range.Text = "工 程 监 理 简 报";
                    oParaTitle.Range.Font.Size = 42;
                    oParaTitle.Range.Font.Bold = 0;
                    oParaTitle.Range.Font.Name = "华文新魏";
                    oParaTitle.Range.Font.ColorIndex = WdColorIndex.wdRed;
                    oParaTitle.Format.SpaceAfter = 5;
                    oParaTitle.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oParaTitle.Range.InsertParagraphAfter();

                    oversion = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oversion.Range.Text = "（第  期）";
                    oversion.Range.Font.Size = 14f;
                    oversion.Range.Font.Bold = 0;
                    oversion.Range.Font.Name = "宋体";
                    oversion.Range.Font.ColorIndex = WdColorIndex.wdBlack;
                    oversion.Format.SpaceAfter = 0;
                    oversion.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oversion.Range.InsertParagraphAfter();

                    oParaSubTitle = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaSubTitle.Range.Text = date;
                    oParaSubTitle.Range.Font.Size = 10.5f;
                    oParaSubTitle.Range.Font.Bold = 1;
                    oParaSubTitle.Range.Font.ColorIndex = WdColorIndex.wdBlack;
                    oParaSubTitle.Format.SpaceAfter = 0;
                    oParaSubTitle.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oParaSubTitle.Range.InsertParagraphAfter();

                    obelow = oDoc.Content.Paragraphs.Add(ref oMissing);
                    obelow.Range.Text = "中联煤层气国家工程研究中心";
                    obelow.Range.Font.Size = 14f;
                    obelow.Range.Font.Bold = 1;
                    obelow.Range.Font.Name = "楷体_GB2312";
                    obelow.Range.Font.ColorIndex = WdColorIndex.wdRed;
                    obelow.Format.SpaceAfter = 0;
                    obelow.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    obelow.Range.InsertParagraphAfter();

                    obelow_second = oDoc.Content.Paragraphs.Add(ref oMissing);
                    obelow_second.Range.Text = "中石油煤层气有限责任公司工程监督中心  " + getDateCNB(end);
                    obelow_second.Range.Font.Size = 14f;
                    obelow_second.Range.Font.Bold = 1;
                    obelow_second.Range.Font.Name = "楷体_GB2312";
                    obelow_second.Range.Font.ColorIndex = WdColorIndex.wdRed;
                    obelow_second.Format.SpaceAfter = 0;
                    obelow_second.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    obelow_second.Range.InsertParagraphAfter();

                    oLine = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oLine.Range.Text = "                                                                                                                                                                                                   ";//"日期：2012-3-3~2012-3-9";
                    oLine.Range.Font.Size = 5f;
                    oLine.Range.Font.Bold = 0;
                    oLine.Range.Font.Underline = WdUnderline.wdUnderlineThick;
                    oLine.Range.Font.UnderlineColor = WdColor.wdColorRed;
                    oLine.Range.Font.Name = "宋体";
                    oLine.Range.Font.ColorIndex = WdColorIndex.wdBlack;
                    oLine.Format.SpaceAfter = 0;
                    oLine.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    oLine.Range.InsertParagraphAfter();

                    Word.Paragraph oPara1;

                    oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oPara1.LineSpacing = 15f;//设置文档的行间距
                    oPara1.SpaceBefore = float.Parse("0");
                    oPara1.SpaceAfter = float.Parse("0");
                    oPara1.Range.Text = "一、工程总体情况";
                    oPara1.Range.Font.Size = 16f;
                    oPara1.Range.Font.Name = "宋体";
                    oPara1.Range.Font.Bold = 1;
                    oPara1.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                    oPara1.Range.Font.ColorIndex = WdColorIndex.wdBlack;
                    oPara1.Format.SpaceAfter = 0;
                    oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    oPara1.Range.InsertParagraphAfter();

                    Word.Paragraph oPara2;
                    object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara2 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara2.LineSpacing = 15f;
                    oPara2.SpaceBefore = float.Parse("0");
                    oPara2.SpaceAfter = float.Parse("0");
                    oPara2.Range.Font.Size = 14f;
                    oPara2.Range.Font.Name = "宋体";
                    oPara2.Range.Text = "1、钻井";
                    oPara2.Format.SpaceAfter = 0;
                    oPara2.Range.InsertParagraphAfter();

                    Word.Table oTable_zj;
                    Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_zj = oDoc.Tables.Add(wrdRng, 7, 7, ref oMissing, ref oMissing);
                    oTable_zj.Range.ParagraphFormat.SpaceAfter = 0;

                    //加粗外边框
                    //oTable_zj.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleThickThinLargeGap;
                    oTable_zj.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                    oTable_zj.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_zj.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    oTable_zj.Columns[1].Width = 87f;
                    oTable_zj.Columns[2].Width = 87f;
                    oTable_zj.Columns[3].Width = 87f;
                    oTable_zj.Columns[4].Width = 87f;
                    oTable_zj.Columns[5].Width = 87f;
                    oTable_zj.Columns[6].Width = 87f;
                    oTable_zj.Columns[7].Width = 87f;

                    oTable_zj.Rows[1].Range.Font.Bold = 1;
                    oTable_zj.Rows[2].Range.Font.Bold = 0;
                    oTable_zj.Rows[3].Range.Font.Bold = 0;
                    oTable_zj.Rows[4].Range.Font.Bold = 0;
                    oTable_zj.Rows[5].Range.Font.Bold = 0;
                    oTable_zj.Rows[6].Range.Font.Bold = 0;
                    oTable_zj.Rows[7].Range.Font.Bold = 0;

                    oTable_zj.Rows[1].Height = 22f;
                    oTable_zj.Rows[2].Height = 22f;
                    oTable_zj.Rows[3].Height = 22f;
                    oTable_zj.Rows[4].Height = 22f;
                    oTable_zj.Rows[5].Height = 22f;
                    oTable_zj.Rows[6].Height = 22f;
                    oTable_zj.Rows[7].Height = 22f;

                    oTable_zj.Range.Font.Size = 9f;

                    //oTable1.Rows[1].Range.Font.Italic = 1;

                    oTable_zj.Cell(1, 1).Range.Text = "类别";

                    oTable_zj.Cell(1, 2).Range.Text = "区块";
                    oTable_zj.Columns[3].SetWidth(50, WdRulerStyle.wdAdjustNone);
                    oTable_zj.Columns[4].SetWidth(50, WdRulerStyle.wdAdjustNone);
                    oTable_zj.Columns[5].SetWidth(50, WdRulerStyle.wdAdjustNone);
                    oTable_zj.Columns[6].SetWidth(50, WdRulerStyle.wdAdjustNone);
                    oTable_zj.Columns[7].SetWidth(50, WdRulerStyle.wdAdjustNone);
                    oTable_zj.Cell(1, 3).Range.Text = "钻 机（台）";
                    oTable_zj.Cell(1, 4).Range.Text = "年完井（口）";
                    oTable_zj.Cell(1, 5).Range.Text = "年进尺（米）";
                    oTable_zj.Cell(1, 6).Range.Text = "正 钻（口）";
                    oTable_zj.Cell(1, 7).Range.Text = "备注";

                    //单元格合并
                    Word.Cell cell_zj_kt = oTable_zj.Cell(2, 1);
                    cell_zj_kt.Merge(oTable_zj.Cell(3, 1));
                    cell_zj_kt.Merge(oTable_zj.Cell(4, 1));


                    Word.Cell cell_zj_kf = oTable_zj.Cell(5, 1);
                    cell_zj_kf.Merge(oTable_zj.Cell(6, 1));
                    cell_zj_kf.Merge(oTable_zj.Cell(7, 1));

                    oTable_zj.Cell(2, 1).Range.Text = "勘探";
                    oTable_zj.Cell(5, 1).Range.Text = "开发";


                    string strData = dataZuanjing.Text;
                    string[] saRecs = strData.Split(fgc_record);

                    string[] values = saRecs[0].Split(fgc_field);
                    oTable_zj.Cell(2, 2).Range.Text = "韩城";
                    oTable_zj.Cell(2, 3).Range.Text = values[0];
                    oTable_zj.Cell(2, 4).Range.Text = values[2];
                    oTable_zj.Cell(2, 5).Range.Text = values[4];
                    oTable_zj.Cell(2, 6).Range.Text = values[5];
                    oTable_zj.Cell(2, 7).Range.Text = values[6];

                    values = saRecs[1].Split(fgc_field);
                    oTable_zj.Cell(3, 2).Range.Text = "临汾";
                    oTable_zj.Cell(3, 3).Range.Text = values[0];
                    oTable_zj.Cell(3, 4).Range.Text = values[2];
                    oTable_zj.Cell(3, 5).Range.Text = values[4];
                    oTable_zj.Cell(3, 6).Range.Text = values[5];
                    oTable_zj.Cell(3, 7).Range.Text = values[6];

                    values = saRecs[2].Split(fgc_field);
                    oTable_zj.Cell(4, 2).Range.Text = "忻州";
                    oTable_zj.Cell(4, 3).Range.Text = values[0];
                    oTable_zj.Cell(4, 4).Range.Text = values[2];
                    oTable_zj.Cell(4, 5).Range.Text = values[4];
                    oTable_zj.Cell(4, 6).Range.Text = values[5];
                    oTable_zj.Cell(4, 7).Range.Text = values[6];

                    values = saRecs[3].Split(fgc_field);
                    oTable_zj.Cell(5, 2).Range.Text = "韩城";
                    oTable_zj.Cell(5, 3).Range.Text = values[0];
                    oTable_zj.Cell(5, 4).Range.Text = values[2];
                    oTable_zj.Cell(5, 5).Range.Text = values[4];
                    oTable_zj.Cell(5, 6).Range.Text = values[5];
                    oTable_zj.Cell(5, 7).Range.Text = values[6];

                    values = saRecs[4].Split(fgc_field);
                    oTable_zj.Cell(6, 2).Range.Text = "临汾";
                    oTable_zj.Cell(6, 3).Range.Text = values[0];
                    oTable_zj.Cell(6, 4).Range.Text = values[2];
                    oTable_zj.Cell(6, 5).Range.Text = values[4];
                    oTable_zj.Cell(6, 6).Range.Text = values[5];
                    oTable_zj.Cell(6, 7).Range.Text = values[6];

                    values = saRecs[5].Split(fgc_field);
                    oTable_zj.Cell(7, 2).Range.Text = "忻州";
                    oTable_zj.Cell(7, 3).Range.Text = values[0];
                    oTable_zj.Cell(7, 4).Range.Text = values[2];
                    oTable_zj.Cell(7, 5).Range.Text = values[4];
                    oTable_zj.Cell(7, 6).Range.Text = values[5];
                    oTable_zj.Cell(7, 7).Range.Text = values[6];

                    oTable_zj.Borders.Enable = 1;

                    Word.Paragraph oPara_yl;
                    oPara_yl = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oPara_yl.LineSpacing = 15f;
                    oPara_yl.SpaceBefore = float.Parse("0");
                    oPara_yl.SpaceAfter = float.Parse("0");
                    oPara_yl.Range.Font.Size = 14f;
                    oPara_yl.Range.Font.Name = "宋体";
                    oPara_yl.Range.Text = "2、压裂";
                    oPara_yl.Range.InsertParagraphAfter();

                    Word.Table oTable_yl;
                    Word.Range wrdRng_yl = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable_yl = oDoc.Tables.Add(wrdRng_yl, 8, 6, ref oMissing, ref oMissing);
                    oTable_yl.Range.ParagraphFormat.SpaceAfter = 0;
                    oTable_yl.Range.Font.Size = 9f;

                    oTable_yl.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    oTable_yl.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable_yl.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    oTable_yl.Columns[1].Width = 70f;
                    oTable_yl.Columns[2].Width = 70f;
                    oTable_yl.Columns[3].Width = 70f;
                    oTable_yl.Columns[4].Width = 70f;
                    oTable_yl.Columns[5].Width = 70f;
                    oTable_yl.Columns[6].Width = 70f;

                    oTable_yl.Rows[1].Range.Font.Bold = 1;
                    oTable_yl.Rows[2].Range.Font.Bold = 1;
                    oTable_yl.Rows[3].Range.Font.Bold = 0;
                    oTable_yl.Rows[4].Range.Font.Bold = 0;
                    oTable_yl.Rows[5].Range.Font.Bold = 0;
                    oTable_yl.Rows[6].Range.Font.Bold = 0;
                    oTable_yl.Rows[7].Range.Font.Bold = 0;
                    oTable_yl.Rows[8].Range.Font.Bold = 0;

                    oTable_yl.Rows[1].Height = 22f;
                    oTable_yl.Rows[2].Height = 22f;
                    oTable_yl.Rows[3].Height = 22f;
                    oTable_yl.Rows[4].Height = 22f;
                    oTable_yl.Rows[5].Height = 22f;
                    oTable_yl.Rows[6].Height = 22f;
                    oTable_yl.Rows[7].Height = 22f;

                    Word.Cell cell_yl_cls = oTable_yl.Cell(1, 1);
                    cell_yl_cls.Merge(oTable_yl.Cell(2, 1));

                    Word.Cell cell_yl_dis = oTable_yl.Cell(1, 2);
                    cell_yl_dis.Merge(oTable_yl.Cell(2, 2));
                    oTable_yl.Cell(1, 1).Range.Text = "类别";
                    oTable_yl.Cell(1, 2).Range.Text = "区块";

                    Word.Cell cell_yl_y = oTable_yl.Cell(1, 3);
                    cell_yl_y.Merge(oTable_yl.Cell(1, 5));
                    cell_yl_y.Range.Text = "年压裂";
                    oTable_yl.Cell(2, 3).Range.Text = "口";
                    oTable_yl.Cell(2, 4).Range.Text = "次";
                    oTable_yl.Cell(2, 5).Range.Text = "层";

                    Word.Cell cell_yl_remark = oTable_yl.Cell(1, 4);
                    cell_yl_remark.Merge(oTable_yl.Cell(2, 6));
                    cell_yl_remark.Range.Text = "备注";

                    Word.Cell cell_yl_kt = oTable_yl.Cell(3, 1);
                    cell_yl_kt.Merge(oTable_yl.Cell(4, 1));
                    cell_yl_kt.Merge(oTable_yl.Cell(5, 1));
                    oTable_yl.Cell(3, 1).Range.Text = "勘探";
                    oTable_yl.Cell(3, 2).Range.Text = "韩城";
                    oTable_yl.Cell(4, 2).Range.Text = "临汾";
                    oTable_yl.Cell(5, 2).Range.Text = "忻州";

                    Word.Cell cell_yl_kf = oTable_yl.Cell(6, 1);
                    cell_yl_kf.Merge(oTable_yl.Cell(7, 1));
                    cell_yl_kf.Merge(oTable_yl.Cell(8, 1));
                    oTable_yl.Cell(6, 1).Range.Text = "开发";
                    oTable_yl.Cell(6, 2).Range.Text = "韩城";
                    oTable_yl.Cell(7, 2).Range.Text = "临汾";
                    oTable_yl.Cell(8, 2).Range.Text = "忻州";

                    strData = dataYalie.Text;
                    saRecs = strData.Split(fgc_record);

                    values = saRecs[0].Split(fgc_field);
                    oTable_yl.Cell(3, 2).Range.Text = "韩城";
                    oTable_yl.Cell(3, 3).Range.Text = values[3];
                    oTable_yl.Cell(3, 4).Range.Text = values[4];
                    oTable_yl.Cell(3, 5).Range.Text = values[5];
                    oTable_yl.Cell(3, 6).Range.Text = values[6];

                    values = saRecs[1].Split(fgc_field);
                    oTable_yl.Cell(4, 2).Range.Text = "临汾";
                    oTable_yl.Cell(4, 3).Range.Text = values[3];
                    oTable_yl.Cell(4, 4).Range.Text = values[4];
                    oTable_yl.Cell(4, 5).Range.Text = values[5];
                    oTable_yl.Cell(4, 6).Range.Text = values[6];

                    values = saRecs[2].Split(fgc_field);
                    oTable_yl.Cell(5, 2).Range.Text = "忻州";
                    oTable_yl.Cell(5, 3).Range.Text = values[3];
                    oTable_yl.Cell(5, 4).Range.Text = values[4];
                    oTable_yl.Cell(5, 5).Range.Text = values[5];
                    oTable_yl.Cell(5, 6).Range.Text = values[6];

                    values = saRecs[3].Split(fgc_field);
                    oTable_yl.Cell(6, 2).Range.Text = "韩城";
                    oTable_yl.Cell(6, 3).Range.Text = values[3];
                    oTable_yl.Cell(6, 4).Range.Text = values[4];
                    oTable_yl.Cell(6, 5).Range.Text = values[5];
                    oTable_yl.Cell(6, 6).Range.Text = values[6];

                    values = saRecs[4].Split(fgc_field);
                    oTable_yl.Cell(7, 2).Range.Text = "临汾";
                    oTable_yl.Cell(7, 3).Range.Text = values[3];
                    oTable_yl.Cell(7, 4).Range.Text = values[4];
                    oTable_yl.Cell(7, 5).Range.Text = values[5];
                    oTable_yl.Cell(7, 6).Range.Text = values[6];

                    values = saRecs[5].Split(fgc_field);
                    oTable_yl.Cell(8, 2).Range.Text = "忻州";
                    oTable_yl.Cell(8, 3).Range.Text = values[3];
                    oTable_yl.Cell(8, 4).Range.Text = values[4];
                    oTable_yl.Cell(8, 5).Range.Text = values[5];
                    oTable_yl.Cell(8, 6).Range.Text = values[6];

                    oTable_yl.Borders.Enable = 1;


                    string strChartData = getNianData(begin, end);
                    string[] strChartDatas = strChartData.Split(';');

                    Word.Paragraph oPara3;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara3 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara3.LineSpacing = 15f;
                    oPara3.SpaceBefore = float.Parse("0");
                    oPara3.SpaceAfter = float.Parse("0");
                    oPara3.Range.Text = "二、关键环节监督及质量分析";
                    oPara3.Range.Font.Bold = 1;
                    oPara3.Range.Font.Name = "宋体";
                    oPara3.Range.Font.Size = 16f;
                    oPara3.Range.InsertParagraphAfter();

                    Word.Paragraph oPara4;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara4 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara4.LineSpacing = 15f;
                    oPara4.SpaceBefore = float.Parse("0");
                    oPara4.SpaceAfter = float.Parse("0");
                    oPara4.Range.Font.Size = 14f;
                    oPara4.Range.Font.Bold = 1;
                    oPara4.Range.Text = "1、钻井";
                    oPara4.Range.InsertParagraphAfter();

                    Word.Paragraph oPara11;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara11 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara11.LineSpacing = 10f;
                    oPara11.SpaceBefore = 0f;
                    oPara11.SpaceAfter = 0f;
                    oPara11.Range.Font.Size = 12f;
                    oPara11.Range.Text = "(1)开钻验收";
                    oPara11.Range.Font.Bold = 0;
                    oPara11.Range.InsertParagraphAfter();

                    Word.Paragraph oPara12;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara12 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara12.LineSpacing = 10f;
                    oPara12.SpaceBefore = 0f;
                    oPara12.SpaceAfter = float.Parse("0");
                    oPara12.Range.Font.Size = 12f;

                    string[] strKzysData = strChartDatas[0].Split(',');
                    int iKzys_Hc_Yikai = Int32.Parse(strKzysData[0]);
                    int iKzys_Lf_Yikai = Int32.Parse(strKzysData[1]);
                    int iKzys_Xz_Yikai = Int32.Parse(strKzysData[2]);
                    int iKzys_Hc_Yikai_Shibai = Int32.Parse(strKzysData[3]);
                    int iKzys_Lf_Yikai_Shibai = Int32.Parse(strKzysData[4]);
                    int iKzys_Xz_Yikai_Shibai = Int32.Parse(strKzysData[5]);
                    int iKzys_Hc_Erkai = Int32.Parse(strKzysData[6]);
                    int iKzys_Lf_Erkai = Int32.Parse(strKzysData[7]);
                    int iKzys_Xz_Erkai = Int32.Parse(strKzysData[8]);
                    int iKzys_Hc_Erkai_Shibai = Int32.Parse(strKzysData[9]);
                    int iKzys_Lf_Erkai_Shibai = Int32.Parse(strKzysData[10]);
                    int iKzys_Xz_Erkai_Shibai = Int32.Parse(strKzysData[11]);
                    int yikai = iKzys_Hc_Yikai + iKzys_Lf_Yikai + iKzys_Xz_Yikai;
                    int yikai_sb = iKzys_Hc_Yikai_Shibai + iKzys_Lf_Yikai_Shibai + iKzys_Xz_Yikai_Shibai;
                    int erkai = iKzys_Hc_Erkai + iKzys_Lf_Erkai + iKzys_Xz_Erkai;
                    int erkai_sb = iKzys_Hc_Erkai_Shibai + iKzys_Lf_Erkai_Shibai + iKzys_Xz_Erkai_Shibai;

                    oPara12.Range.Text = "本年一开验收" + yikai + "口井，不合格数量" + yikai_sb + "口。二开验收" + erkai + "口井，不合格数量" + erkai_sb + "口。";

                    object oClassType = "MSGraph.Chart";
                    Microsoft.Office.Interop.Word.InlineShape oShape;
                    object oChart;
                    object oChartApp;
                    object[] Parameters;
                    Microsoft.Office.Interop.Graph.Chart objChart;
                    Microsoft.Office.Interop.Graph.DataSheet dataSheet;

                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oShape = ((Word.Range)oRng).InlineShapes.AddOLEObject(ref oClassType, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                    oChart = oShape.OLEFormat.Object;
                    oChartApp = oChart.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oChart, null);

                    Parameters = new Object[1];
                    Parameters[0] = 4; //xlLine = 4
                    oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty, null, oChart, Parameters);

                    objChart = (Microsoft.Office.Interop.Graph.Chart)oShape.OLEFormat.Object;
                    objChart.ChartType = Microsoft.Office.Interop.Graph.XlChartType.xlColumnClustered;

                    dataSheet = objChart.Application.DataSheet;
                    dataSheet.Cells[1, 2] = "一开验收";
                    dataSheet.Cells[1, 3] = "一开不合格";
                    dataSheet.Cells[1, 4] = "二开验收";
                    dataSheet.Cells[1, 5] = "二开不合格";
                    dataSheet.Cells[2, 1] = "韩城";
                    dataSheet.Cells[2, 2] = iKzys_Hc_Yikai;
                    dataSheet.Cells[2, 3] = iKzys_Hc_Yikai_Shibai;
                    dataSheet.Cells[2, 4] = iKzys_Hc_Erkai;
                    dataSheet.Cells[2, 5] = iKzys_Hc_Erkai_Shibai;
                    dataSheet.Cells[3, 1] = "临汾";
                    dataSheet.Cells[3, 2] = iKzys_Lf_Yikai;
                    dataSheet.Cells[3, 3] = iKzys_Lf_Yikai_Shibai;
                    dataSheet.Cells[3, 4] = iKzys_Lf_Erkai;
                    dataSheet.Cells[3, 5] = iKzys_Lf_Erkai_Shibai;
                    dataSheet.Cells[4, 1] = "忻州";
                    dataSheet.Cells[4, 2] = iKzys_Xz_Yikai;
                    dataSheet.Cells[4, 3] = iKzys_Xz_Yikai_Shibai;
                    dataSheet.Cells[4, 4] = iKzys_Xz_Erkai;
                    dataSheet.Cells[4, 5] = iKzys_Xz_Erkai_Shibai;
                    objChart.Application.Update();

                    oChartApp.GetType().InvokeMember("Update", BindingFlags.InvokeMethod, null, oChartApp, null);
                    oChartApp.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, oChartApp, null);

                    oShape.Width = oWord.InchesToPoints(5.75f);
                    oShape.Height = oWord.InchesToPoints(3.0f);

                    Word.Paragraph oParaTemp = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaTemp.Range.InsertParagraphAfter();

                    Word.Paragraph oPara13;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara13 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara13.LineSpacing = 10f;//设置文档的行间距
                    oPara13.SpaceBefore = 0;
                    oPara13.SpaceAfter = 0;
                    oPara13.Range.Font.Size = 12f;
                    oPara13.Range.Text = "(2)井身质量";
                    oPara13.Format.SpaceAfter = 2;
                    oPara13.Range.InsertParagraphAfter();

                    Word.Paragraph oPara14;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara14 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara14.LineSpacing = 10f;
                    oPara14.SpaceBefore = 0;
                    oPara14.SpaceAfter = 0;
                    oPara14.Range.Font.Size = 12f;

                    string[] strJszlData = strChartDatas[1].Split(',');
                    int iJszl_Hc = Int32.Parse(strJszlData[0]);
                    int iJszl_Lf = Int32.Parse(strJszlData[1]);
                    int iJszl_Xz = Int32.Parse(strJszlData[2]);
                    int jszl = iJszl_Hc + iJszl_Lf + iJszl_Xz;

                    oPara14.Range.Text = "本年共" + jszl + "口出现井身质量问题。";

                    wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                    oChart = oShape.OLEFormat.Object;
                    oChartApp = oChart.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oChart, null);

                    Parameters = new Object[1];
                    Parameters[0] = 1;
                    oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty, null, oChart, Parameters);

                    objChart = (Microsoft.Office.Interop.Graph.Chart)oShape.OLEFormat.Object;
                    objChart.ChartType = Microsoft.Office.Interop.Graph.XlChartType.xlColumnClustered;

                    dataSheet = objChart.Application.DataSheet;
                    dataSheet.Rows.Clear();
                    dataSheet.Columns.Clear();
                    dataSheet.Cells[1, 2] = "井身质量问题井数";
                    dataSheet.Cells[2, 1] = "韩城";
                    dataSheet.Cells[2, 2] = iJszl_Hc;
                    dataSheet.Cells[3, 1] = "临汾";
                    dataSheet.Cells[3, 2] = iJszl_Lf;
                    dataSheet.Cells[4, 1] = "忻州";
                    dataSheet.Cells[4, 2] = iJszl_Xz;
                    objChart.Application.Update();

                    oChartApp.GetType().InvokeMember("Update", BindingFlags.InvokeMethod, null, oChartApp, null);
                    oChartApp.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, oChartApp, null);

                    oShape.Width = oWord.InchesToPoints(5.75f);
                    oShape.Height = oWord.InchesToPoints(3.0f);

                    oParaTemp = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaTemp.Range.InsertParagraphAfter();

                    Word.Paragraph oPara15;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara15 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara15.LineSpacing = 10f;
                    oPara15.SpaceBefore = float.Parse("0");
                    oPara15.SpaceAfter = float.Parse("0");
                    oPara15.Range.Font.Size = 12f;
                    oPara15.Range.Text = "(3)下套管作业";
                    oPara15.Range.InsertParagraphAfter();

                    Word.Paragraph oPara16;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara16 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara16.LineSpacing = 10f;
                    oPara16.SpaceBefore = float.Parse("0");
                    oPara16.SpaceAfter = float.Parse("0");
                    oPara16.Range.Font.Size = 12f;

                    string[] strXtgData = strChartDatas[2].Split(',');
                    int iXtg_Hc = Int32.Parse(strXtgData[0]);
                    int iXtg_Lf = Int32.Parse(strXtgData[1]);
                    int iXtg_Xz = Int32.Parse(strXtgData[2]);
                    int iXtg_Hc_Shibai = Int32.Parse(strXtgData[3]);
                    int iXtg_Lf_Shibai = Int32.Parse(strXtgData[4]);
                    int iXtg_Xz_Shibai = Int32.Parse(strXtgData[5]);
                    int xtg = iXtg_Hc + iXtg_Lf + iXtg_Xz;
                    int xtg_yc = iXtg_Hc_Shibai + iXtg_Lf_Shibai + iXtg_Xz_Shibai;

                    oPara16.Range.Text = "本年下套管作业共" + xtg + "口井，施工异常" + xtg_yc + "口。";

                    wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                    oChart = oShape.OLEFormat.Object;
                    oChartApp = oChart.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oChart, null);

                    Parameters = new Object[1];
                    Parameters[0] = 1;
                    oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty, null, oChart, Parameters);

                    objChart = (Microsoft.Office.Interop.Graph.Chart)oShape.OLEFormat.Object;
                    objChart.ChartType = Microsoft.Office.Interop.Graph.XlChartType.xlColumnClustered;

                    dataSheet = objChart.Application.DataSheet;
                    dataSheet.Rows.Clear();
                    dataSheet.Columns.Clear();
                    dataSheet.Cells[1, 2] = "下套管作业";
                    dataSheet.Cells[1, 3] = "施工异常";
                    dataSheet.Cells[2, 1] = "韩城";
                    dataSheet.Cells[2, 2] = iXtg_Hc;
                    dataSheet.Cells[2, 3] = iXtg_Hc_Shibai;
                    dataSheet.Cells[3, 1] = "临汾";
                    dataSheet.Cells[3, 2] = iXtg_Lf;
                    dataSheet.Cells[3, 3] = iXtg_Lf_Shibai;
                    dataSheet.Cells[4, 1] = "忻州";
                    dataSheet.Cells[4, 2] = iXtg_Xz;
                    dataSheet.Cells[4, 3] = iXtg_Xz_Shibai;
                    objChart.Application.Update();

                    oChartApp.GetType().InvokeMember("Update", BindingFlags.InvokeMethod, null, oChartApp, null);
                    oChartApp.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, oChartApp, null);

                    oShape.Width = oWord.InchesToPoints(5.75f);
                    oShape.Height = oWord.InchesToPoints(3.0f);

                    oParaTemp = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaTemp.Range.InsertParagraphAfter();

                    Word.Paragraph oPara17;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara17 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara17.LineSpacing = 10f;
                    oPara17.SpaceBefore = float.Parse("0");
                    oPara17.SpaceAfter = float.Parse("0");
                    oPara17.Range.Font.Size = 12f;
                    oPara17.Range.Text = "(4)固井作业";
                    oPara17.Range.InsertParagraphAfter();


                    Word.Paragraph oPara18;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara18 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara18.LineSpacing = 10f;
                    oPara18.SpaceBefore = float.Parse("0");
                    oPara18.SpaceAfter = float.Parse("0");
                    oPara18.Range.Font.Size = 12f;

                    string[] strGjData = strChartDatas[3].Split(',');
                    int iGj_Hc = Int32.Parse(strGjData[0]);
                    int iGj_Lf = Int32.Parse(strGjData[1]);
                    int iGj_Xz = Int32.Parse(strGjData[2]);
                    int iGj_Hc_Shibai = Int32.Parse(strGjData[3]);
                    int iGj_Lf_Shibai = Int32.Parse(strGjData[4]);
                    int iGj_Xz_Shibai = Int32.Parse(strGjData[5]);
                    int gj = iGj_Hc + iGj_Lf + iGj_Xz;
                    int gj_yc = iGj_Hc_Shibai + iGj_Lf_Shibai + iGj_Xz_Shibai;

                    oPara18.Range.Text = "本年固井作业共" + gj + "口井，施工异常" + gj_yc + "口。";

                    wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                    oChart = oShape.OLEFormat.Object;
                    oChartApp = oChart.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oChart, null);

                    Parameters = new Object[1];
                    Parameters[0] = 1;
                    oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty, null, oChart, Parameters);

                    objChart = (Microsoft.Office.Interop.Graph.Chart)oShape.OLEFormat.Object;
                    objChart.ChartType = Microsoft.Office.Interop.Graph.XlChartType.xlColumnClustered;

                    dataSheet = objChart.Application.DataSheet;
                    dataSheet.Rows.Clear();
                    dataSheet.Columns.Clear();
                    dataSheet.Cells[1, 2] = "固井作业";
                    dataSheet.Cells[1, 3] = "施工异常";
                    dataSheet.Cells[2, 1] = "韩城";
                    dataSheet.Cells[2, 2] = iGj_Hc;
                    dataSheet.Cells[2, 3] = iGj_Hc_Shibai;
                    dataSheet.Cells[3, 1] = "临汾";
                    dataSheet.Cells[3, 2] = iGj_Lf;
                    dataSheet.Cells[3, 3] = iGj_Lf_Shibai;
                    dataSheet.Cells[4, 1] = "忻州";
                    dataSheet.Cells[4, 2] = iGj_Xz;
                    dataSheet.Cells[4, 3] = iGj_Xz_Shibai;
                    objChart.Application.Update();

                    oChartApp.GetType().InvokeMember("Update", BindingFlags.InvokeMethod, null, oChartApp, null);
                    oChartApp.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, oChartApp, null);

                    oShape.Width = oWord.InchesToPoints(5.75f);
                    oShape.Height = oWord.InchesToPoints(3.0f);

                    oParaTemp = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaTemp.Range.InsertParagraphAfter();

                    Word.Paragraph oPara20;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara20 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara20.LineSpacing = 10f;
                    oPara20.SpaceBefore = float.Parse("0");
                    oPara20.SpaceAfter = float.Parse("0");
                    oPara20.Range.Font.Size = 12f;
                    oPara20.Range.Text = "（5）处理井漏情况";
                    oPara20.Range.InsertParagraphAfter();

                    Word.Paragraph oPara20_1;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara20_1 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara20_1.LineSpacing = 10f;
                    oPara20_1.SpaceBefore = float.Parse("0");
                    oPara20_1.SpaceAfter = float.Parse("0");
                    oPara20_1.Range.Font.Size = 12f;

                    string[] strJlData = strChartDatas[4].Split(',');
                    int iJl_Hc = Int32.Parse(strJlData[0]);
                    int iJl_Lf = Int32.Parse(strJlData[1]);
                    int iJl_Xz = Int32.Parse(strJlData[2]);
                    int iJl_Hc_cg = Int32.Parse(strJlData[3]);
                    int iJl_Lf_cg = Int32.Parse(strJlData[4]);
                    int iJl_Xz_cg = Int32.Parse(strJlData[5]);
                    int jl = iJl_Hc + iJl_Lf + iJl_Xz;
                    int jl_cg = iJl_Hc_cg + iJl_Lf_cg + iJl_Xz_cg;

                    oPara20_1.Range.Text = "本年处理井漏共" + jl + "口，处理成功" + jl_cg + "口。";

                    wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                    oChart = oShape.OLEFormat.Object;
                    oChartApp = oChart.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oChart, null);

                    Parameters = new Object[1];
                    Parameters[0] = 1;
                    oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty, null, oChart, Parameters);

                    objChart = (Microsoft.Office.Interop.Graph.Chart)oShape.OLEFormat.Object;
                    objChart.ChartType = Microsoft.Office.Interop.Graph.XlChartType.xlColumnClustered;

                    dataSheet = objChart.Application.DataSheet;
                    dataSheet.Rows.Clear();
                    dataSheet.Columns.Clear();
                    dataSheet.Cells[1, 2] = "处理井漏";
                    dataSheet.Cells[1, 3] = "处理成功";
                    dataSheet.Cells[2, 1] = "韩城";
                    dataSheet.Cells[2, 2] = iJl_Hc;
                    dataSheet.Cells[2, 3] = iJl_Hc_cg;
                    dataSheet.Cells[3, 1] = "临汾";
                    dataSheet.Cells[3, 2] = iJl_Lf;
                    dataSheet.Cells[3, 3] = iJl_Lf_cg;
                    dataSheet.Cells[4, 1] = "忻州";
                    dataSheet.Cells[4, 2] = iJl_Xz;
                    dataSheet.Cells[4, 3] = iJl_Xz_cg;
                    objChart.Application.Update();

                    oChartApp.GetType().InvokeMember("Update", BindingFlags.InvokeMethod, null, oChartApp, null);
                    oChartApp.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, oChartApp, null);

                    oShape.Width = oWord.InchesToPoints(5.75f);
                    oShape.Height = oWord.InchesToPoints(3.0f);

                    oParaTemp = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaTemp.Range.InsertParagraphAfter();

                    Word.Paragraph oPara21;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara21 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara21.LineSpacing = 10f;
                    oPara21.SpaceBefore = float.Parse("0");
                    oPara21.SpaceAfter = float.Parse("0");
                    oPara21.Range.Font.Size = 12f;
                    oPara21.Range.Text = "（6）处理涌水情况";
                    oPara21.Range.InsertParagraphAfter();

                    Word.Paragraph oPara21_1;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara21_1 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara21_1.LineSpacing = 10f;
                    oPara21_1.SpaceBefore = float.Parse("0");
                    oPara21_1.SpaceAfter = float.Parse("0");
                    oPara21_1.Range.Font.Size = 12f;

                    string[] strYsData = strChartDatas[5].Split(',');
                    int iYs_Hc = Int32.Parse(strYsData[0]);
                    int iYs_Lf = Int32.Parse(strYsData[1]);
                    int iYs_Xz = Int32.Parse(strYsData[2]);
                    int iYs_Hc_cg = Int32.Parse(strYsData[3]);
                    int iYs_Lf_cg = Int32.Parse(strYsData[4]);
                    int iYs_Xz_cg = Int32.Parse(strYsData[5]);
                    int ys = iYs_Hc + iYs_Lf + iYs_Xz;
                    int ys_cg = iYs_Hc_cg + iYs_Lf_cg + iYs_Xz_cg;

                    oPara21_1.Range.Text = "本年处理涌水共" + ys + "口，处理成功" + ys_cg + "口。";

                    wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                    oChart = oShape.OLEFormat.Object;
                    oChartApp = oChart.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oChart, null);

                    Parameters = new Object[1];
                    Parameters[0] = 1;
                    oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty, null, oChart, Parameters);

                    objChart = (Microsoft.Office.Interop.Graph.Chart)oShape.OLEFormat.Object;
                    objChart.ChartType = Microsoft.Office.Interop.Graph.XlChartType.xlColumnClustered;

                    dataSheet = objChart.Application.DataSheet;
                    dataSheet.Rows.Clear();
                    dataSheet.Columns.Clear();
                    dataSheet.Cells[1, 2] = "处理涌水";
                    dataSheet.Cells[1, 3] = "处理成功";
                    dataSheet.Cells[2, 1] = "韩城";
                    dataSheet.Cells[2, 2] = iYs_Hc;
                    dataSheet.Cells[2, 3] = iYs_Hc_cg;
                    dataSheet.Cells[3, 1] = "临汾";
                    dataSheet.Cells[3, 2] = iYs_Lf;
                    dataSheet.Cells[3, 3] = iYs_Lf_cg;
                    dataSheet.Cells[4, 1] = "忻州";
                    dataSheet.Cells[4, 2] = iYs_Xz;
                    dataSheet.Cells[4, 3] = iYs_Xz_cg;
                    objChart.Application.Update();

                    oChartApp.GetType().InvokeMember("Update", BindingFlags.InvokeMethod, null, oChartApp, null);
                    oChartApp.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, oChartApp, null);

                    oShape.Width = oWord.InchesToPoints(5.75f);
                    oShape.Height = oWord.InchesToPoints(3.0f);

                    oParaTemp = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaTemp.Range.InsertParagraphAfter();

                    Word.Paragraph oPara22;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara22 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara22.LineSpacing = 5f;
                    oPara22.SpaceBefore = float.Parse("0");
                    oPara22.SpaceAfter = float.Parse("0");
                    oPara22.Range.Font.Size = 14f;
                    oPara22.Range.Font.Name = "宋体";
                    oPara22.Range.Font.Bold = 1;
                    oPara22.Range.Text = "2、压裂进度及质量影响因素分析";
                    oPara22.Range.InsertParagraphAfter();

                    Word.Paragraph oPara23;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara23 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara23.LineSpacing = 5f;
                    oPara23.SpaceBefore = float.Parse("0");
                    oPara23.SpaceAfter = float.Parse("0");
                    oPara23.Range.Font.Size = 12f;
                    oPara23.Range.Font.Name = "宋体";
                    oPara23.Range.Font.Bold = 0;
                    oPara23.Range.Text = "（1）阻工因素";
                    oPara23.Range.InsertParagraphAfter();

                    string[] strZgysData = strChartDatas[6].Split(',');
                    int iZgys_Hc_Tgzl = Int32.Parse(strZgysData[0]);
                    int iZgys_Hc_Xy = Int32.Parse(strZgysData[1]);
                    int iZgys_Hc_Gngx = Int32.Parse(strZgysData[2]);
                    int iZgys_Hc_Clwx = Int32.Parse(strZgysData[3]);
                    int iZgys_Hc_Jcbq = Int32.Parse(strZgysData[4]);
                    int iZgys_Hc_Jtbq = Int32.Parse(strZgysData[5]);
                    int iZgys_Hc_Bspy = Int32.Parse(strZgysData[6]);
                    int iZgys_Lf_Tgzl = Int32.Parse(strZgysData[7]);
                    int iZgys_Lf_Xy = Int32.Parse(strZgysData[8]);
                    int iZgys_Lf_Gngx = Int32.Parse(strZgysData[9]);
                    int iZgys_Lf_Clwx = Int32.Parse(strZgysData[10]);
                    int iZgys_Lf_Jcbq = Int32.Parse(strZgysData[11]);
                    int iZgys_Lf_Jtbq = Int32.Parse(strZgysData[12]);
                    int iZgys_Lf_Bspy = Int32.Parse(strZgysData[13]);
                    int iZgys_Xz_Tgzl = Int32.Parse(strZgysData[14]);
                    int iZgys_Xz_Xy = Int32.Parse(strZgysData[15]);
                    int iZgys_Xz_Gngx = Int32.Parse(strZgysData[16]);
                    int iZgys_Xz_Clwx = Int32.Parse(strZgysData[17]);
                    int iZgys_Xz_Jcbq = Int32.Parse(strZgysData[18]);
                    int iZgys_Xz_Jtbq = Int32.Parse(strZgysData[19]);
                    int iZgys_Xz_Bspy = Int32.Parse(strZgysData[20]);

                    wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                    oChart = oShape.OLEFormat.Object;
                    oChartApp = oChart.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oChart, null);

                    Parameters = new Object[1];
                    Parameters[0] = 1;
                    oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty, null, oChart, Parameters);

                    objChart = (Microsoft.Office.Interop.Graph.Chart)oShape.OLEFormat.Object;
                    objChart.ChartType = Microsoft.Office.Interop.Graph.XlChartType.xlColumnClustered;

                    dataSheet = objChart.Application.DataSheet;
                    dataSheet.Rows.Clear();
                    dataSheet.Columns.Clear();
                    dataSheet.Cells[1, 2] = "韩城";
                    dataSheet.Cells[1, 3] = "临汾";
                    dataSheet.Cells[1, 4] = "忻州";
                    dataSheet.Cells[2, 1] = "套管质量";
                    dataSheet.Cells[2, 2] = iZgys_Hc_Tgzl;
                    dataSheet.Cells[2, 3] = iZgys_Lf_Tgzl;
                    dataSheet.Cells[2, 4] = iZgys_Xz_Tgzl;
                    dataSheet.Cells[3, 1] = "下雨";
                    dataSheet.Cells[3, 2] = iZgys_Hc_Xy;
                    dataSheet.Cells[3, 3] = iZgys_Lf_Xy;
                    dataSheet.Cells[3, 4] = iZgys_Xz_Xy;
                    dataSheet.Cells[4, 1] = "工农关系";
                    dataSheet.Cells[4, 2] = iZgys_Hc_Gngx;
                    dataSheet.Cells[4, 3] = iZgys_Lf_Gngx;
                    dataSheet.Cells[4, 4] = iZgys_Xz_Gngx;
                    dataSheet.Cells[5, 1] = "车辆维修";
                    dataSheet.Cells[5, 2] = iZgys_Hc_Clwx;
                    dataSheet.Cells[5, 3] = iZgys_Lf_Clwx;
                    dataSheet.Cells[5, 4] = iZgys_Xz_Clwx;
                    dataSheet.Cells[6, 1] = "井场搬迁";
                    dataSheet.Cells[6, 2] = iZgys_Hc_Jcbq;
                    dataSheet.Cells[6, 3] = iZgys_Lf_Jcbq;
                    dataSheet.Cells[6, 4] = iZgys_Xz_Jcbq;
                    dataSheet.Cells[7, 1] = "井台搬迁";
                    dataSheet.Cells[7, 2] = iZgys_Hc_Jtbq;
                    dataSheet.Cells[7, 3] = iZgys_Lf_Jtbq;
                    dataSheet.Cells[7, 4] = iZgys_Xz_Jtbq;
                    dataSheet.Cells[8, 1] = "备水配液";
                    dataSheet.Cells[8, 2] = iZgys_Hc_Bspy;
                    dataSheet.Cells[8, 3] = iZgys_Lf_Bspy;
                    dataSheet.Cells[8, 4] = iZgys_Xz_Bspy;
                    objChart.Application.Update();

                    oChartApp.GetType().InvokeMember("Update", BindingFlags.InvokeMethod, null, oChartApp, null);
                    oChartApp.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, oChartApp, null);

                    oShape.Width = oWord.InchesToPoints(5.75f);
                    oShape.Height = oWord.InchesToPoints(3.0f);

                    oParaTemp = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaTemp.Range.InsertParagraphAfter();

                    Word.Paragraph oPara24;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara24 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara24.LineSpacing = 5f;
                    oPara24.Range.InsertParagraphBefore();
                    oPara24.SpaceBefore = float.Parse("0");
                    oPara24.SpaceAfter = float.Parse("0");
                    oPara24.Range.Font.Size = 12f;
                    oPara24.Range.Font.Name = "宋体";
                    oPara24.Range.Font.Bold = 0;
                    oPara24.Range.Text = "（2）入井材料";
                    oPara24.Range.InsertParagraphAfter();

                    Word.Paragraph oPara25;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara25 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara25.LineSpacing = 5f;
                    oPara25.SpaceBefore = float.Parse("0");
                    oPara25.SpaceAfter = float.Parse("0");
                    oPara25.Range.Font.Size = 12f;
                    oPara25.Range.Font.Name = "宋体";
                    oPara25.Range.Font.Bold = 0;
                    oPara25.Range.Text = "各区块加砂量如下：";
                    oPara25.Range.InsertParagraphAfter();

                    string[] strRjclData = strChartDatas[7].Split(',');
                    int iRjcl_Hc_100 = Int32.Parse(strRjclData[0]);
                    int iRjcl_Hc_90 = Int32.Parse(strRjclData[1]);
                    int iRjcl_Hc_80 = Int32.Parse(strRjclData[2]);
                    int iRjcl_Hc_70 = Int32.Parse(strRjclData[3]);
                    int iRjcl_Lf_100 = Int32.Parse(strRjclData[4]);
                    int iRjcl_Lf_90 = Int32.Parse(strRjclData[5]);
                    int iRjcl_Lf_80 = Int32.Parse(strRjclData[6]);
                    int iRjcl_Lf_70 = Int32.Parse(strRjclData[7]);
                    int iRjcl_Xz_100 = Int32.Parse(strRjclData[8]);
                    int iRjcl_Xz_90 = Int32.Parse(strRjclData[9]);
                    int iRjcl_Xz_80 = Int32.Parse(strRjclData[10]);
                    int iRjcl_Xz_70 = Int32.Parse(strRjclData[11]);

                    wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                    oChart = oShape.OLEFormat.Object;
                    oChartApp = oChart.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oChart, null);

                    Parameters = new Object[1];
                    Parameters[0] = 1;
                    oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty, null, oChart, Parameters);

                    objChart = (Microsoft.Office.Interop.Graph.Chart)oShape.OLEFormat.Object;
                    objChart.ChartType = Microsoft.Office.Interop.Graph.XlChartType.xlColumnClustered;

                    dataSheet = objChart.Application.DataSheet;
                    dataSheet.Rows.Clear();
                    dataSheet.Columns.Clear();
                    dataSheet.Cells[1, 2] = "韩城";
                    dataSheet.Cells[1, 3] = "临汾";
                    dataSheet.Cells[1, 4] = "忻州";
                    dataSheet.Cells[2, 1] = "大于100%";
                    dataSheet.Cells[2, 2] = iRjcl_Hc_100;
                    dataSheet.Cells[2, 3] = iRjcl_Lf_100;
                    dataSheet.Cells[2, 4] = iRjcl_Xz_100;
                    dataSheet.Cells[3, 1] = "90%-100%";
                    dataSheet.Cells[3, 2] = iRjcl_Hc_90;
                    dataSheet.Cells[3, 3] = iRjcl_Lf_90;
                    dataSheet.Cells[3, 4] = iRjcl_Xz_90;
                    dataSheet.Cells[4, 1] = "80%-90%";
                    dataSheet.Cells[4, 2] = iRjcl_Hc_80;
                    dataSheet.Cells[4, 3] = iRjcl_Lf_80;
                    dataSheet.Cells[4, 4] = iRjcl_Xz_80;
                    dataSheet.Cells[5, 1] = "小于80%";
                    dataSheet.Cells[5, 2] = iRjcl_Hc_70;
                    dataSheet.Cells[5, 3] = iRjcl_Lf_70;
                    dataSheet.Cells[5, 4] = iRjcl_Xz_70;

                    objChart.Application.Update();

                    oChartApp.GetType().InvokeMember("Update", BindingFlags.InvokeMethod, null, oChartApp, null);
                    oChartApp.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, oChartApp, null);

                    oShape.Width = oWord.InchesToPoints(5.75f);
                    oShape.Height = oWord.InchesToPoints(3.0f);

                    oParaTemp = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaTemp.Range.InsertParagraphAfter();

                    Word.Paragraph oPara26;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara26 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara26.LineSpacing = 5f;
                    oPara26.Range.InsertParagraphBefore();
                    oPara26.SpaceBefore = float.Parse("0");
                    oPara26.SpaceAfter = float.Parse("0");
                    oPara26.Range.Font.Size = 12f;
                    oPara26.Range.Font.Name = "宋体";
                    oPara26.Range.Font.Bold = 0;
                    oPara26.Range.Text = "（3）施工质量";
                    oPara26.Range.InsertParagraphAfter();

                    Word.Paragraph oPara27;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara27 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara27.LineSpacing = 5f;
                    oPara27.SpaceBefore = float.Parse("0");
                    oPara27.SpaceAfter = float.Parse("0");
                    oPara27.Range.Font.Size = 12f;
                    oPara27.Range.Font.Name = "宋体";
                    oPara27.Range.Font.Bold = 0;
                    oPara27.Range.InsertParagraphAfter();

                    string[] strSgzlData = strChartDatas[8].Split(',');
                    int iSgzl_Hc = Int32.Parse(strSgzlData[0]);
                    int iSgzl_Lf = Int32.Parse(strSgzlData[1]);
                    int iSgzl_Xz = Int32.Parse(strSgzlData[2]);
                    int iSgzl_Hc_sb = Int32.Parse(strSgzlData[3]);
                    int iSgzl_Lf_sb = Int32.Parse(strSgzlData[4]);
                    int iSgzl_Xz_sb = Int32.Parse(strSgzlData[5]);
                    int sgzl = iSgzl_Hc + iSgzl_Lf + iSgzl_Xz;
                    int sgzl_sb = iSgzl_Hc_sb + iSgzl_Lf_sb + iSgzl_Xz_sb;
                    string cgl = "100%";
                    if (sgzl_sb > 0)
                    {
                        cgl = String.Format("{0:N2}", (sgzl - sgzl_sb) * 100.0f / sgzl) + "%";
                    }

                    oPara27.Range.Text = "本年共压裂施工" + sgzl + "次，失败" + sgzl_sb + "口。一次成功率为" + cgl;

                    wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                    oChart = oShape.OLEFormat.Object;
                    oChartApp = oChart.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oChart, null);

                    Parameters = new Object[1];
                    Parameters[0] = 1;
                    oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty, null, oChart, Parameters);

                    objChart = (Microsoft.Office.Interop.Graph.Chart)oShape.OLEFormat.Object;
                    objChart.ChartType = Microsoft.Office.Interop.Graph.XlChartType.xlColumnClustered;

                    dataSheet = objChart.Application.DataSheet;
                    dataSheet.Rows.Clear();
                    dataSheet.Columns.Clear();
                    dataSheet.Cells[1, 2] = "压裂次数";
                    dataSheet.Cells[1, 3] = "失败次数";
                    dataSheet.Cells[2, 1] = "韩城";
                    dataSheet.Cells[2, 2] = iSgzl_Hc;
                    dataSheet.Cells[2, 3] = iSgzl_Hc_sb;
                    dataSheet.Cells[3, 1] = "临汾";
                    dataSheet.Cells[3, 2] = iSgzl_Lf;
                    dataSheet.Cells[3, 3] = iSgzl_Lf_sb;
                    dataSheet.Cells[4, 1] = "忻州";
                    dataSheet.Cells[4, 2] = iSgzl_Xz;
                    dataSheet.Cells[4, 3] = iSgzl_Xz_sb;
                    objChart.Application.Update();

                    oChartApp.GetType().InvokeMember("Update", BindingFlags.InvokeMethod, null, oChartApp, null);
                    oChartApp.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, oChartApp, null);

                    oShape.Width = oWord.InchesToPoints(5.75f);
                    oShape.Height = oWord.InchesToPoints(3.0f);

                    oParaTemp = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oParaTemp.Range.InsertParagraphAfter();

                    Word.Paragraph oPara28;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara28 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara28.LineSpacing = 15f;
                    oPara28.Range.InsertParagraphBefore();
                    oPara28.SpaceBefore = float.Parse("0");
                    oPara28.SpaceAfter = float.Parse("0");
                    oPara28.Range.Font.Size = 16f;
                    oPara28.Range.Font.Name = "宋体";
                    oPara28.Range.Font.Bold = 1;
                    oPara28.Range.Text = "三、技术研究及其他工作";
                    oPara28.Range.InsertParagraphAfter();

                    Word.Paragraph oPara29;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara29 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara29.LineSpacing = 15f;
                    oPara29.SpaceBefore = float.Parse("0");
                    oPara29.SpaceAfter = float.Parse("0");
                    oPara29.Range.Font.Size = 14f;
                    oPara29.Range.Font.Name = "宋体";
                    oPara29.Range.Font.Bold = 1;
                    oPara29.Range.Text = "（一）韩城";
                    oPara29.Range.InsertParagraphAfter();

                    Word.Paragraph oPara32;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara32 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara32.LineSpacing = 15f;
                    oPara32.SpaceBefore = float.Parse("0");
                    oPara32.SpaceAfter = float.Parse("0");
                    oPara32.Range.Font.Size = 12f;
                    oPara32.Range.Font.Name = "宋体";
                    oPara32.Range.Font.Bold = 0;
                    oPara32.Range.Text = txt_report_hc.Text;

                    oPara32.Range.InsertParagraphAfter();

                    Word.Paragraph oPara30;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara30 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara30.LineSpacing = 15f;
                    oPara30.SpaceBefore = float.Parse("0");
                    oPara30.SpaceAfter = float.Parse("0");
                    oPara30.Range.Font.Size = 14f;
                    oPara30.Range.Font.Name = "宋体";
                    oPara30.Range.Font.Bold = 1;
                    oPara30.Range.Text = "（二）临汾";
                    oPara30.Range.InsertParagraphAfter();

                    Word.Paragraph oPara33;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara33 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara33.LineSpacing = 12f;
                    oPara33.SpaceBefore = float.Parse("0");
                    oPara33.SpaceAfter = float.Parse("0");
                    oPara33.Range.Font.Size = 12f;
                    oPara33.Range.Font.Name = "宋体";
                    oPara33.Range.Font.Bold = 0;
                    oPara33.Range.Text = txt_report_lf.Text;

                    oPara33.Range.InsertParagraphAfter();

                    Word.Paragraph oPara31;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara31 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara31.LineSpacing = 15f;
                    oPara31.SpaceBefore = float.Parse("0");
                    oPara31.SpaceAfter = float.Parse("0");
                    oPara31.Range.Font.Size = 14f;
                    oPara31.Range.Font.Name = "宋体";
                    oPara31.Range.Font.Bold = 1;
                    oPara31.Range.Text = "（三）忻州";
                    oPara31.Range.InsertParagraphAfter();

                    Word.Paragraph oPara34;
                    oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara34 = oDoc.Content.Paragraphs.Add(ref oRng);
                    oPara34.LineSpacing = 15f;
                    oPara34.SpaceBefore = float.Parse("0");
                    oPara34.SpaceAfter = float.Parse("0");
                    oPara34.Range.Font.Size = 12f;
                    oPara34.Range.Font.Name = "宋体";
                    oPara34.Range.Font.Bold = 0;
                    oPara34.Range.Text = txt_report_xz.Text;

                    string dstDir = Server.MapPath("../temp/" + userName + "/");

                    if (Directory.Exists(dstDir) == false)
                    {
                        try
                        {
                            Directory.CreateDirectory(dstDir);
                        }
                        catch (System.Exception e1)
                        {
                            this.Response.Write("<script language='JavaScript'>window.alert('服务器端IO操作异常'); </script>");
                            return;
                        }
                    }
                    if (File.Exists(dstDir + "质量简报.doc"))
                    {
                        try
                        {
                            File.Delete(dstDir + "质量简报.doc");
                        }
                        catch (System.Exception e2)
                        {

                        }
                    }

                    oDoc.SaveAs(dstDir + "质量简报.doc");
                    oWord.Quit();
                    oDoc = null;
                    oWord = null;

                    Response.Redirect("../temp/" + userName + "/质量简报.doc");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language=javascript>alert('" + ex.Message + "');</script>");
                return;
            }
        }

        protected void Button_Zhou_Click(object sender, EventArgs e)
        {
            string strRiqi = tbRiqi.Text;
            string strWeek = MyTools.GetWeek(strRiqi);
            string begin = strWeek.Split(',')[0];
            string end = strWeek.Split(',')[1];
            lblRiqi.Text = begin + "," + end;

            lblType.Text = "zhou";

            lblZuanjingRiqi.Text = begin + " 至 " + end;
            lblYalieRiqi.Text = begin + " 至 " + end;

            dataZuanjing.Text = "";
            dataYalie.Text = "";

            dataKzys.Text = "";
            dataJszl.Text = "";
            dataXtgzy.Text = "";
            dataGjzy.Text = "";
            dataCljl.Text = "";
            dataClys.Text = "";

            dataZgys.Text = "";
            dataRjcl.Text = "";
            dataSgzl.Text = "";

            txt_report_hc.Text = "";
            txt_report_lf.Text = "";
            txt_report_xz.Text = "";

            getZtqkZj(begin, end);
            getZtqkYl(begin, end);

            getGJKzys(begin, end);
            getGJJszl(begin, end);
            getGJXtgzy(begin, end);
            getGJGjzy(begin, end);
            getGJCljl(begin, end);
            getGJClys(begin, end);

            getGJZgys(begin, end);
            getGJRjcl(begin, end);
            getGJSgzl(begin, end);

            getJishu(begin, end);
        }

        protected void Button_Yue_Click(object sender, EventArgs e)
        {
            string strRiqi = tbRiqi.Text;
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);
            string begin = y + "-" + m + "-01";
            DateTime dtEnd = new DateTime(y, m, MyTools.GetMonthDays(strRiqi));
            string end = null;
            if (dtEnd > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
            else end = y + "-" + m + "-" + MyTools.GetMonthDays(strRiqi);
            lblRiqi.Text = begin + "," + end;

            lblType.Text = "yue";

            lblZuanjingRiqi.Text = begin + " 至 " + end;
            lblYalieRiqi.Text = begin + " 至 " + end;

            dataZuanjing.Text = "";
            dataYalie.Text = "";

            dataKzys.Text = "";
            dataJszl.Text = "";
            dataXtgzy.Text = "";
            dataGjzy.Text = "";
            dataCljl.Text = "";
            dataClys.Text = "";

            dataZgys.Text = "";
            dataRjcl.Text = "";
            dataSgzl.Text = "";

            txt_report_hc.Text = "";
            txt_report_lf.Text = "";
            txt_report_xz.Text = "";

            getZtqkZj(begin, end);
            getZtqkYl(begin, end);

            getGJKzys(begin, end);
            getGJJszl(begin, end);
            getGJXtgzy(begin, end);
            getGJGjzy(begin, end);
            getGJCljl(begin, end);
            getGJClys(begin, end);

            getGJZgys(begin, end);
            getGJRjcl(begin, end);
            getGJSgzl(begin, end);

            getJishu(begin, end);
        }

        protected void Button_Ji_Click(object sender, EventArgs e)
        {
            string strRiqi = tbRiqi.Text;
            string begin = MyTools.GetQuarterBegin(strRiqi).ToString("yyyy-MM-dd");
            DateTime dtEnd = MyTools.GetQuarterEnd(strRiqi);
            string end = null;
            if (dtEnd > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
            else end = dtEnd.ToString("yyyy-MM-dd");
            lblRiqi.Text = begin + "," + end;

            lblType.Text = "ji";

            lblZuanjingRiqi.Text = begin + " 至 " + end;
            lblYalieRiqi.Text = begin + " 至 " + end;

            dataZuanjing.Text = "";
            dataYalie.Text = "";

            dataKzys.Text = "";
            dataJszl.Text = "";
            dataXtgzy.Text = "";
            dataGjzy.Text = "";
            dataCljl.Text = "";
            dataClys.Text = "";

            dataZgys.Text = "";
            dataRjcl.Text = "";
            dataSgzl.Text = "";

            txt_report_hc.Text = "";
            txt_report_lf.Text = "";
            txt_report_xz.Text = "";

            getZtqkZj(begin, end);
            getZtqkYl(begin, end);

            getGJKzys(begin, end);
            getGJJszl(begin, end);
            getGJXtgzy(begin, end);
            getGJGjzy(begin, end);
            getGJCljl(begin, end);
            getGJClys(begin, end);

            getGJZgys(begin, end);
            getGJRjcl(begin, end);
            getGJSgzl(begin, end);

            getJishu(begin, end);
        }

        protected void Button_Nian_Click(object sender, EventArgs e)
        {
            string strRiqi = tbRiqi.Text;
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);
            string begin = y + "-01-01";
            DateTime dtEnd = new DateTime(y, 12, 31);
            string end = null;
            if (dtEnd > DateTime.Now) end = DateTime.Now.ToString("yyyy-MM-dd");
            else end = y + "-12-31";
            lblRiqi.Text = begin + "," + end;

            lblType.Text = "nian";

            lblZuanjingRiqi.Text = begin + " 至 " + end;
            lblYalieRiqi.Text = begin + " 至 " + end;

            dataZuanjing.Text = "";
            dataYalie.Text = "";

            dataKzys.Text = "";
            dataJszl.Text = "";
            dataXtgzy.Text = "";
            dataGjzy.Text = "";
            dataCljl.Text = "";
            dataClys.Text = "";

            dataZgys.Text = "";
            dataRjcl.Text = "";
            dataSgzl.Text = "";

            txt_report_hc.Text = "";
            txt_report_lf.Text = "";
            txt_report_xz.Text = "";

            getZtqkZj(begin, end);
            getZtqkYl(begin, end);

            getGJKzys(begin, end);
            getGJJszl(begin, end);
            getGJXtgzy(begin, end);
            getGJGjzy(begin, end);
            getGJCljl(begin, end);
            getGJClys(begin, end);

            getGJZgys(begin, end);
            getGJRjcl(begin, end);
            getGJSgzl(begin, end);

            getJishu(begin, end);
        }

        //总体情况-钻井
        private void getZtqkZj(string begin, string end)
        {
            string strRiqi = tbRiqi.Text;
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);
            string nianBegin = y + "-01-01";
            DateTime dtEnd = new DateTime(y, 12, 31);
            string nianEnd = null;
            if (dtEnd > DateTime.Now) nianEnd = DateTime.Now.ToString("yyyy-MM-dd");
            else nianEnd = y + "-12-31";

            lblZtqkZj.Text = begin + " 至 " + end;

            List<string[]> lstRecords = new List<string[]>();
            List<string[]> lstOldTemp = new List<string[]>();
            List<string[]> lstNowTemp = new List<string[]>();

            List<string[]> lstJingbie = new List<string[]>();

            string sqlTemp = null;
            System.Data.DataTable dtTemp = null;

            sqlTemp = "select qukuai,jinghao,jingbie from jing_jichuxinxi";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[3];
                    recTemp[0] = dtTemp.Rows[i]["qukuai"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    recTemp[2] = dtTemp.Rows[i]["jingbie"].ToString();
                    lstJingbie.Add(recTemp);
                }
            }

            
            sqlTemp = "select place,duihao from Xls_Zj_Rbb_Zj where riqi >= '" + begin + "' and riqi <= '" + end + "' group by place,duihao";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["duihao"].ToString();

                    lstRecords.Add(recTemp);
                }
            }
            int i_zj_kt_hc = 0;
            int i_zj_kf_hc = 0;
            int i_zj_kt_lf = 0;
            int i_zj_kf_lf = 0;
            int i_zj_kt_xz = 0;
            int i_zj_kf_xz = 0;
            for (int i = 0; i < lstRecords.Count; i++)
            {
                if (lstRecords[i][0] == "韩城")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("韩城" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_zj_kt_hc++;
                            }
                            else
                            {
                                i_zj_kf_hc++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "临汾")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("临汾" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_zj_kt_lf++;
                            }
                            else
                            {
                                i_zj_kf_lf++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "忻州")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("忻州" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_zj_kt_xz++;
                            }
                            else
                            {
                                i_zj_kf_xz++;
                            }
                            break;
                        }
                    }
                }
            }
            lstRecords.Clear();
            

            sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Tjb where wanjingshijian between '" + begin + "' and '" + end + "'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstNowTemp.Count; j++)
                    {
                        if (recTemp[0] == lstNowTemp[j][0] && recTemp[1] == lstNowTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstNowTemp.Add(recTemp);
                }
            }
            sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Wj where riqi between '" + begin + "' and '" + end + "'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstOldTemp.Count; j++)
                    {
                        if (recTemp[0] == lstOldTemp[j][0] && recTemp[1] == lstOldTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstOldTemp.Add(recTemp);
                }
            }
            for (int i = 0; i < lstNowTemp.Count; i++)
            {
                bool b = true;
                for (int j = 0; j < lstOldTemp.Count; j++)
                {
                    if (lstOldTemp[j][0] == lstNowTemp[i][0] &&
                        lstOldTemp[j][1] == lstNowTemp[i][1])
                    {
                        b = false;
                        break;
                    }
                }
                if (b) lstRecords.Add(lstNowTemp[i]);
            }
            for (int j = 0; j < lstOldTemp.Count; j++)
            {
                lstRecords.Add(lstOldTemp[j]);
            }
            int i_z_wj_kt_hc = 0;
            int i_z_wj_kf_hc = 0;
            int i_z_wj_kt_lf = 0;
            int i_z_wj_kf_lf = 0;
            int i_z_wj_kt_xz = 0;
            int i_z_wj_kf_xz = 0;
            for (int i = 0; i < lstRecords.Count; i++)
            {
                if (lstRecords[i][0] == "韩城")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("韩城" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_wj_kt_hc++;
                            }
                            else
                            {
                                i_z_wj_kf_hc++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "临汾")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("临汾" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_wj_kt_lf++;
                            }
                            else
                            {
                                i_z_wj_kf_lf++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "忻州")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("忻州" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_wj_kt_xz++;
                            }
                            else
                            {
                                i_z_wj_kf_xz++;
                            }
                            break;
                        }
                    }
                }
            }
            lstNowTemp.Clear();
            lstOldTemp.Clear();
            lstRecords.Clear();

            sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Tjb where wanjingshijian between '" + nianBegin + "' and '" + nianEnd + "'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstNowTemp.Count; j++)
                    {
                        if (recTemp[0] == lstNowTemp[j][0] && recTemp[1] == lstNowTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstNowTemp.Add(recTemp);
                }
            }
            sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Wj where riqi between '" + nianBegin + "' and '" + nianEnd + "'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstOldTemp.Count; j++)
                    {
                        if (recTemp[0] == lstOldTemp[j][0] && recTemp[1] == lstOldTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstOldTemp.Add(recTemp);
                }
            }
            for (int i = 0; i < lstNowTemp.Count; i++)
            {
                bool b = true;
                for (int j = 0; j < lstOldTemp.Count; j++)
                {
                    if (lstOldTemp[j][0] == lstNowTemp[i][0] &&
                        lstOldTemp[j][1] == lstNowTemp[i][1])
                    {
                        b = false;
                        break;
                    }
                }
                if (b) lstRecords.Add(lstNowTemp[i]);
            }
            for (int j = 0; j < lstOldTemp.Count; j++)
            {
                lstRecords.Add(lstOldTemp[j]);
            }
            int i_n_wj_kt_hc = 0;
            int i_n_wj_kf_hc = 0;
            int i_n_wj_kt_lf = 0;
            int i_n_wj_kf_lf = 0;
            int i_n_wj_kt_xz = 0;
            int i_n_wj_kf_xz = 0;
            for (int i = 0; i < lstRecords.Count; i++)
            {
                if (lstRecords[i][0] == "韩城")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("韩城" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_wj_kt_hc++;
                            }
                            else
                            {
                                i_n_wj_kf_hc++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "临汾")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("临汾" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_wj_kt_lf++;
                            }
                            else
                            {
                                i_n_wj_kf_lf++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "忻州")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("忻州" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_wj_kt_xz++;
                            }
                            else
                            {
                                i_n_wj_kf_xz++;
                            }
                            break;
                        }
                    }
                }
            }
            lstNowTemp.Clear();
            lstOldTemp.Clear();
            lstRecords.Clear();

            sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Zj where riqi = '" + end + "' and gongkuang not like '%停工%'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstRecords.Count; j++)
                    {
                        if (recTemp[0] == lstRecords[j][0] && recTemp[1] == lstRecords[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstRecords.Add(recTemp);
                }
            }
            int i_z_zz_kt_hc = 0;
            int i_z_zz_kf_hc = 0;
            int i_z_zz_kt_lf = 0;
            int i_z_zz_kf_lf = 0;
            int i_z_zz_kt_xz = 0;
            int i_z_zz_kf_xz = 0;
            for (int i = 0; i < lstRecords.Count; i++)
            {
                if (lstRecords[i][0] == "韩城")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("韩城" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_zz_kt_hc++;
                            }
                            else
                            {
                                i_z_zz_kf_hc++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "临汾")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("临汾" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_zz_kt_lf++;
                            }
                            else
                            {
                                i_z_zz_kf_lf++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "忻州")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("忻州" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_zz_kt_xz++;
                            }
                            else
                            {
                                i_z_zz_kf_xz++;
                            }
                            break;
                        }
                    }
                }
            }
            lstNowTemp.Clear();
            lstOldTemp.Clear();
            lstRecords.Clear();

            sqlTemp = "select t2.jingbie, t1.place, SUM(t1.dangrijinchi) as 'jinchi' from Xls_Zj_Rbb_Zj t1, jing_jichuxinxi t2 where t1.riqi between '" + begin + "' and '" + end + "' and t1.jinghao=t2.jinghao group by t2.jingbie, t1.place";
            dtTemp = DataBaseHelper.query(sqlTemp);
            float f_z_jc_kt_hc = 0;
            float f_z_jc_kf_hc = 0;
            float f_z_jc_kt_lf = 0;
            float f_z_jc_kf_lf = 0;
            float f_z_jc_kt_xz = 0;
            float f_z_jc_kf_xz = 0;
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string jc = dtTemp.Rows[i]["jinchi"].ToString().Trim();
                    string qk = dtTemp.Rows[i]["place"].ToString();
                    if (dtTemp.Rows[i]["jingbie"].ToString() == "勘探")
                    {
                        if (qk == "韩城")
                        {
                            try
                            {
                                f_z_jc_kt_hc += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                        if (qk == "临汾")
                        {
                            try
                            {
                                f_z_jc_kt_lf += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                        if (qk == "忻州")
                        {
                            try
                            {
                                f_z_jc_kt_xz += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                    }
                    else
                    {
                        if (qk == "韩城")
                        {
                            try
                            {
                                f_z_jc_kf_hc += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                        if (qk == "临汾")
                        {
                            try
                            {
                                f_z_jc_kf_lf += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                        if (qk == "忻州")
                        {
                            try
                            {
                                f_z_jc_kf_xz += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                    }
                }
            }

            sqlTemp = "select t2.jingbie, t1.place, SUM(t1.dangrijinchi) as 'jinchi' from Xls_Zj_Rbb_Zj t1, jing_jichuxinxi t2 where t1.riqi between '" + nianBegin + "' and '" + nianEnd + "' and t1.jinghao=t2.jinghao group by t2.jingbie, t1.place";
            dtTemp = DataBaseHelper.query(sqlTemp);
            float f_n_jc_kt_hc = 0;
            float f_n_jc_kf_hc = 0;
            float f_n_jc_kt_lf = 0;
            float f_n_jc_kf_lf = 0;
            float f_n_jc_kt_xz = 0;
            float f_n_jc_kf_xz = 0;
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string jc = dtTemp.Rows[i]["jinchi"].ToString();
                    if (dtTemp.Rows[i]["jingbie"].ToString() == "勘探")
                    {
                        if (dtTemp.Rows[i]["place"].ToString() == "韩城")
                        {
                            try
                            {
                                f_n_jc_kt_hc += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                        if (dtTemp.Rows[i]["place"].ToString() == "临汾")
                        {
                            try
                            {
                                f_n_jc_kt_lf += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                        if (dtTemp.Rows[i]["place"].ToString() == "忻州")
                        {
                            try
                            {
                                f_n_jc_kt_xz += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                    }
                    else
                    {
                        if (dtTemp.Rows[i]["place"].ToString() == "韩城")
                        {
                            try
                            {
                                f_n_jc_kf_hc += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                        if (dtTemp.Rows[i]["place"].ToString() == "临汾")
                        {
                            try
                            {
                                f_n_jc_kf_lf += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                        if (dtTemp.Rows[i]["place"].ToString() == "忻州")
                        {
                            try
                            {
                                f_n_jc_kf_xz += float.Parse(jc);
                            }
                            catch (System.Exception fe)
                            {
                            }
                        }
                    }
                }
            }

            string strZuanjing = "";

            string strWanjing = "";
            string strJinchi = "";
            string type = lblType.Text;
            if (type == "zhou")
            {
                strWanjing = "周完井（口）";
                strJinchi = "周进尺（米）";
            }
            if (type == "yue")
            {
                strWanjing = "月完井（口）";
                strJinchi = "月进尺（米）";
            }
            if (type == "ji")
            {
                strWanjing = "季完井（口）";
                strJinchi = "季进尺（米）";
            }

            if (type != "nian")
            {
                System.Data.DataTable dtZtqkZj = new System.Data.DataTable();
                DataColumn col_class = new DataColumn("类别");
                dtZtqkZj.Columns.Add(col_class);
                DataColumn col_qukuai = new DataColumn("区块");
                dtZtqkZj.Columns.Add(col_qukuai);
                DataColumn col_zuanji = new DataColumn("钻机（台）");
                dtZtqkZj.Columns.Add(col_zuanji);
                DataColumn col_zhouwanjing = new DataColumn(strWanjing);
                dtZtqkZj.Columns.Add(col_zhouwanjing);
                DataColumn col_nianwanjing = new DataColumn("年完井（口）");
                dtZtqkZj.Columns.Add(col_nianwanjing);
                DataColumn col_zhoujinchi = new DataColumn(strJinchi);
                dtZtqkZj.Columns.Add(col_zhoujinchi);
                DataColumn col_nianjinchi = new DataColumn("年进尺（米）");
                dtZtqkZj.Columns.Add(col_nianjinchi);
                DataColumn col_zhengzuan = new DataColumn("正钻（口）");
                dtZtqkZj.Columns.Add(col_zhengzuan);
                DataColumn col_beizhu = new DataColumn("备注");
                dtZtqkZj.Columns.Add(col_beizhu);
                DataRow dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "勘探";
                dt_row["区块"] = "韩城";
                dt_row["钻机（台）"] = i_zj_kt_hc;
                dt_row[strWanjing] = i_z_wj_kt_hc;
                dt_row["年完井（口）"] = i_n_wj_kt_hc;
                dt_row[strJinchi] = f_z_jc_kt_hc;
                dt_row["年进尺（米）"] = f_n_jc_kt_hc;
                dt_row["正钻（口）"] = i_z_zz_kt_hc;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kt_hc + fg_field;
                strZuanjing += i_z_wj_kt_hc + fg_field;
                strZuanjing += i_n_wj_kt_hc + fg_field;
                strZuanjing += f_z_jc_kt_hc + fg_field;
                strZuanjing += f_n_jc_kt_hc + fg_field;
                strZuanjing += i_z_zz_kt_hc + fg_field;
                strZuanjing += fg_record;


                dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "勘探";
                dt_row["区块"] = "临汾";
                dt_row["钻机（台）"] = i_zj_kt_lf;
                dt_row[strWanjing] = i_z_wj_kt_lf;
                dt_row["年完井（口）"] = i_n_wj_kt_lf;
                dt_row[strJinchi] = f_z_jc_kt_lf;
                dt_row["年进尺（米）"] = f_n_jc_kt_lf;
                dt_row["正钻（口）"] = i_z_zz_kt_lf;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kt_lf + fg_field;
                strZuanjing += i_z_wj_kt_lf + fg_field;
                strZuanjing += i_n_wj_kt_lf + fg_field;
                strZuanjing += f_z_jc_kt_lf + fg_field;
                strZuanjing += f_n_jc_kt_lf + fg_field;
                strZuanjing += i_z_zz_kt_lf + fg_field;
                strZuanjing += fg_record;

                dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "勘探";
                dt_row["区块"] = "忻州";
                dt_row["钻机（台）"] = i_zj_kt_xz;
                dt_row[strWanjing] = i_z_wj_kt_xz;
                dt_row["年完井（口）"] = i_n_wj_kt_xz;
                dt_row[strJinchi] = f_z_jc_kt_xz;
                dt_row["年进尺（米）"] = f_n_jc_kt_xz;
                dt_row["正钻（口）"] = i_z_zz_kt_xz;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kt_xz + fg_field;
                strZuanjing += i_z_wj_kt_xz + fg_field;
                strZuanjing += i_n_wj_kt_xz + fg_field;
                strZuanjing += f_z_jc_kt_xz + fg_field;
                strZuanjing += f_n_jc_kt_xz + fg_field;
                strZuanjing += i_z_zz_kt_xz + fg_field;
                strZuanjing += fg_record;

                dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "开发";
                dt_row["区块"] = "韩城";
                dt_row["钻机（台）"] = i_zj_kf_hc;
                dt_row[strWanjing] = i_z_wj_kf_hc;
                dt_row["年完井（口）"] = i_n_wj_kf_hc;
                dt_row[strJinchi] = f_z_jc_kf_hc;
                dt_row["年进尺（米）"] = f_n_jc_kf_hc;
                dt_row["正钻（口）"] = i_z_zz_kf_hc;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kf_hc + fg_field;
                strZuanjing += i_z_wj_kf_hc + fg_field;
                strZuanjing += i_n_wj_kf_hc + fg_field;
                strZuanjing += f_z_jc_kf_hc + fg_field;
                strZuanjing += f_n_jc_kf_hc + fg_field;
                strZuanjing += i_z_zz_kf_hc + fg_field;
                strZuanjing += fg_record;

                dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "开发";
                dt_row["区块"] = "临汾";
                dt_row["钻机（台）"] = i_zj_kf_lf;
                dt_row[strWanjing] = i_z_wj_kf_lf;
                dt_row["年完井（口）"] = i_n_wj_kf_lf;
                dt_row[strJinchi] = f_z_jc_kf_lf;
                dt_row["年进尺（米）"] = f_n_jc_kf_lf;
                dt_row["正钻（口）"] = i_z_zz_kf_lf;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kf_lf + fg_field;
                strZuanjing += i_z_wj_kf_lf + fg_field;
                strZuanjing += i_n_wj_kf_lf + fg_field;
                strZuanjing += f_z_jc_kf_lf + fg_field;
                strZuanjing += f_n_jc_kf_lf + fg_field;
                strZuanjing += i_z_zz_kf_lf + fg_field;
                strZuanjing += fg_record;

                dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "开发";
                dt_row["区块"] = "忻州";
                dt_row["钻机（台）"] = i_zj_kf_xz;
                dt_row[strWanjing] = i_z_wj_kf_xz;
                dt_row["年完井（口）"] = i_n_wj_kf_xz;
                dt_row[strJinchi] = f_z_jc_kf_xz;
                dt_row["年进尺（米）"] = f_n_jc_kf_xz;
                dt_row["正钻（口）"] = i_z_zz_kf_xz;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kf_xz + fg_field;
                strZuanjing += i_z_wj_kf_xz + fg_field;
                strZuanjing += i_n_wj_kf_xz + fg_field;
                strZuanjing += f_z_jc_kf_xz + fg_field;
                strZuanjing += f_n_jc_kf_xz + fg_field;
                strZuanjing += i_z_zz_kf_xz + fg_field;
                strZuanjing += "";

                dataZuanjing.Text = strZuanjing;

                grd_zj.DataSource = dtZtqkZj;
                grd_zj.DataBind();
                GroupRows(grd_zj, 0);
            }
            else
            {
                System.Data.DataTable dtZtqkZj = new System.Data.DataTable();
                DataColumn col_class = new DataColumn("类别");
                dtZtqkZj.Columns.Add(col_class);
                DataColumn col_qukuai = new DataColumn("区块");
                dtZtqkZj.Columns.Add(col_qukuai);
                DataColumn col_zuanji = new DataColumn("钻机（台）");
                dtZtqkZj.Columns.Add(col_zuanji);
                DataColumn col_nianwanjing = new DataColumn("年完井（口）");
                dtZtqkZj.Columns.Add(col_nianwanjing);
                DataColumn col_nianjinchi = new DataColumn("年进尺（米）");
                dtZtqkZj.Columns.Add(col_nianjinchi);
                DataColumn col_zhengzuan = new DataColumn("正钻（口）");
                dtZtqkZj.Columns.Add(col_zhengzuan);
                DataColumn col_beizhu = new DataColumn("备注");
                dtZtqkZj.Columns.Add(col_beizhu);
                DataRow dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "勘探";
                dt_row["区块"] = "韩城";
                dt_row["钻机（台）"] = i_zj_kt_hc;
                dt_row["年完井（口）"] = i_n_wj_kt_hc;
                dt_row["年进尺（米）"] = f_n_jc_kt_hc;
                dt_row["正钻（口）"] = i_z_zz_kt_hc;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kt_hc + fg_field;
                strZuanjing += i_z_wj_kt_hc + fg_field;
                strZuanjing += i_n_wj_kt_hc + fg_field;
                strZuanjing += f_z_jc_kt_hc + fg_field;
                strZuanjing += f_n_jc_kt_hc + fg_field;
                strZuanjing += i_z_zz_kt_hc + fg_field;
                strZuanjing += fg_record;


                dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "勘探";
                dt_row["区块"] = "临汾";
                dt_row["钻机（台）"] = i_zj_kt_lf;
                dt_row["年完井（口）"] = i_n_wj_kt_lf;
                dt_row["年进尺（米）"] = f_n_jc_kt_lf;
                dt_row["正钻（口）"] = i_z_zz_kt_lf;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kt_lf + fg_field;
                strZuanjing += i_z_wj_kt_lf + fg_field;
                strZuanjing += i_n_wj_kt_lf + fg_field;
                strZuanjing += f_z_jc_kt_lf + fg_field;
                strZuanjing += f_n_jc_kt_lf + fg_field;
                strZuanjing += i_z_zz_kt_lf + fg_field;
                strZuanjing += fg_record;

                dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "勘探";
                dt_row["区块"] = "忻州";
                dt_row["钻机（台）"] = i_zj_kt_xz;
                dt_row["年完井（口）"] = i_n_wj_kt_xz;
                dt_row["年进尺（米）"] = f_n_jc_kt_xz;
                dt_row["正钻（口）"] = i_z_zz_kt_xz;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kt_xz + fg_field;
                strZuanjing += i_z_wj_kt_xz + fg_field;
                strZuanjing += i_n_wj_kt_xz + fg_field;
                strZuanjing += f_z_jc_kt_xz + fg_field;
                strZuanjing += f_n_jc_kt_xz + fg_field;
                strZuanjing += i_z_zz_kt_xz + fg_field;
                strZuanjing += fg_record;

                dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "开发";
                dt_row["区块"] = "韩城";
                dt_row["钻机（台）"] = i_zj_kf_hc;
                dt_row["年完井（口）"] = i_n_wj_kf_hc;
                dt_row["年进尺（米）"] = f_n_jc_kf_hc;
                dt_row["正钻（口）"] = i_z_zz_kf_hc;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kf_hc + fg_field;
                strZuanjing += i_z_wj_kf_hc + fg_field;
                strZuanjing += i_n_wj_kf_hc + fg_field;
                strZuanjing += f_z_jc_kf_hc + fg_field;
                strZuanjing += f_n_jc_kf_hc + fg_field;
                strZuanjing += i_z_zz_kf_hc + fg_field;
                strZuanjing += fg_record;

                dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "开发";
                dt_row["区块"] = "临汾";
                dt_row["钻机（台）"] = i_zj_kf_lf;
                dt_row["年完井（口）"] = i_n_wj_kf_lf;
                dt_row["年进尺（米）"] = f_n_jc_kf_lf;
                dt_row["正钻（口）"] = i_z_zz_kf_lf;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kf_lf + fg_field;
                strZuanjing += i_z_wj_kf_lf + fg_field;
                strZuanjing += i_n_wj_kf_lf + fg_field;
                strZuanjing += f_z_jc_kf_lf + fg_field;
                strZuanjing += f_n_jc_kf_lf + fg_field;
                strZuanjing += i_z_zz_kf_lf + fg_field;
                strZuanjing += fg_record;

                dt_row = dtZtqkZj.NewRow();
                dt_row["类别"] = "开发";
                dt_row["区块"] = "忻州";
                dt_row["钻机（台）"] = i_zj_kf_xz;
                dt_row["年完井（口）"] = i_n_wj_kf_xz;
                dt_row["年进尺（米）"] = f_n_jc_kf_xz;
                dt_row["正钻（口）"] = i_z_zz_kf_xz;
                dt_row["备注"] = "";
                dtZtqkZj.Rows.Add(dt_row);

                strZuanjing += i_zj_kf_xz + fg_field;
                strZuanjing += i_z_wj_kf_xz + fg_field;
                strZuanjing += i_n_wj_kf_xz + fg_field;
                strZuanjing += f_z_jc_kf_xz + fg_field;
                strZuanjing += f_n_jc_kf_xz + fg_field;
                strZuanjing += i_z_zz_kf_xz + fg_field;
                strZuanjing += "";

                dataZuanjing.Text = strZuanjing;

                grd_zj.DataSource = dtZtqkZj;
                grd_zj.DataBind();
                GroupRows(grd_zj, 0);
            }
        }

        //总体情况-压裂
        private void getZtqkYl(string begin, string end)
        {
            string strRiqi = tbRiqi.Text;
            string[] straRiqi = strRiqi.Split('-');
            int y = Int32.Parse(straRiqi[0]);
            int m = Int32.Parse(straRiqi[1]);
            int d = Int32.Parse(straRiqi[2]);
            string nianBegin = y + "-01-01";
            DateTime dtEnd = new DateTime(y, 12, 31);
            string nianEnd = null;
            if (dtEnd > DateTime.Now) nianEnd = DateTime.Now.ToString("yyyy-MM-dd");
            else nianEnd = y + "-12-31";

            lblZtqkYl.Text = begin + " 至 " + end;

            List<string[]> lstRecords = new List<string[]>();
            List<string[]> lstOldTemp = new List<string[]>();
            List<string[]> lstNowTemp = new List<string[]>();

            List<string[]> lstJingbie = new List<string[]>();

            string sqlTemp = null;
            System.Data.DataTable dtTemp = null;

            sqlTemp = "select qukuai,jinghao,jingbie from jing_jichuxinxi";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[3];
                    recTemp[0] = dtTemp.Rows[i]["qukuai"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    recTemp[2] = dtTemp.Rows[i]["jingbie"].ToString();
                    lstJingbie.Add(recTemp);
                }
            }

            sqlTemp = "select place,jinghao from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "'  and shifouyawan like '%是%' order by shigongriqi desc";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstRecords.Count; j++)
                    {
                        if (recTemp[0] == lstRecords[j][0] && recTemp[1] == lstRecords[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstRecords.Add(recTemp);
                }
            }
            int i_z_yw_kt_hc = 0;
            int i_z_yw_kf_hc = 0;
            int i_z_yw_kt_lf = 0;
            int i_z_yw_kf_lf = 0;
            int i_z_yw_kt_xz = 0;
            int i_z_yw_kf_xz = 0;
            for (int i = 0; i < lstRecords.Count; i++)
            {
                if (lstRecords[i][0] == "韩城")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("韩城" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_yw_kt_hc++;
                            }
                            else
                            {
                                i_z_yw_kf_hc++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "临汾")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("临汾" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_yw_kt_lf++;
                            }
                            else
                            {
                                i_z_yw_kf_lf++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "忻州")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("忻州" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_yw_kt_xz++;
                            }
                            else
                            {
                                i_z_yw_kf_xz++;
                            }
                            break;
                        }
                    }
                }
            }
            lstRecords.Clear();
            lstOldTemp.Clear();
            lstNowTemp.Clear();

            sqlTemp = "select place,jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[3];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    recTemp[2] = dtTemp.Rows[i]["cengwei"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstRecords.Count; j++)
                    {
                        if (recTemp[0] == lstRecords[j][0] && recTemp[1] == lstRecords[j][1] && recTemp[2] == lstRecords[j][2])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstRecords.Add(recTemp);
                }
            }
            int i_z_ci_kt_hc = 0;
            int i_z_ci_kf_hc = 0;
            int i_z_ci_kt_lf = 0;
            int i_z_ci_kf_lf = 0;
            int i_z_ci_kt_xz = 0;
            int i_z_ci_kf_xz = 0;
            for (int i = 0; i < lstRecords.Count; i++)
            {
                if (lstRecords[i][0] == "韩城")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("韩城" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_ci_kt_hc++;
                            }
                            else
                            {
                                i_z_ci_kf_hc++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "临汾")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("临汾" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_ci_kt_lf++;
                            }
                            else
                            {
                                i_z_ci_kf_lf++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "忻州")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("忻州" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_ci_kt_xz++;
                            }
                            else
                            {
                                i_z_ci_kf_xz++;
                            }
                            break;
                        }
                    }
                }
            }
            lstRecords.Clear();
            lstOldTemp.Clear();
            lstNowTemp.Clear();

            sqlTemp = "select place,jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shifouhege like '%是%' and shigongriqi between '" + begin + "' and '" + end + "'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[3];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    recTemp[2] = dtTemp.Rows[i]["cengwei"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstRecords.Count; j++)
                    {
                        if (recTemp[0] == lstRecords[j][0] && recTemp[1] == lstRecords[j][1] && recTemp[2] == lstRecords[j][2])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstRecords.Add(recTemp);
                }
            }
            int i_z_ce_kt_hc = 0;
            int i_z_ce_kf_hc = 0;
            int i_z_ce_kt_lf = 0;
            int i_z_ce_kf_lf = 0;
            int i_z_ce_kt_xz = 0;
            int i_z_ce_kf_xz = 0;
            for (int i = 0; i < lstRecords.Count; i++)
            {
                if (lstRecords[i][0] == "韩城")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("韩城" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_ce_kt_hc++;
                            }
                            else
                            {
                                i_z_ce_kf_hc++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "临汾")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("临汾" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_ce_kt_lf++;
                            }
                            else
                            {
                                i_z_ce_kf_lf++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "忻州")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("忻州" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_z_ce_kt_xz++;
                            }
                            else
                            {
                                i_z_ce_kf_xz++;
                            }
                            break;
                        }
                    }
                }
            }
            lstRecords.Clear();
            lstOldTemp.Clear();
            lstNowTemp.Clear();

            //年
            sqlTemp = "select place,jinghao from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + nianBegin + "' and '" + nianEnd + "'  and shifouyawan like '%是%' order by shigongriqi desc";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstRecords.Count; j++)
                    {
                        if (recTemp[0] == lstRecords[j][0] && recTemp[1] == lstRecords[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstRecords.Add(recTemp);
                }
            }
            int i_n_yw_kt_hc = 0;
            int i_n_yw_kf_hc = 0;
            int i_n_yw_kt_lf = 0;
            int i_n_yw_kf_lf = 0;
            int i_n_yw_kt_xz = 0;
            int i_n_yw_kf_xz = 0;
            for (int i = 0; i < lstRecords.Count; i++)
            {
                if (lstRecords[i][0] == "韩城")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("韩城" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_yw_kt_hc++;
                            }
                            else
                            {
                                i_n_yw_kf_hc++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "临汾")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("临汾" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_yw_kt_lf++;
                            }
                            else
                            {
                                i_n_yw_kf_lf++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "忻州")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("忻州" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_yw_kt_xz++;
                            }
                            else
                            {
                                i_n_yw_kf_xz++;
                            }
                            break;
                        }
                    }
                }
            }
            lstRecords.Clear();
            lstOldTemp.Clear();
            lstNowTemp.Clear();

            sqlTemp = "select place,jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + nianBegin + "' and '" + nianEnd + "'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[3];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    recTemp[2] = dtTemp.Rows[i]["cengwei"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstRecords.Count; j++)
                    {
                        if (recTemp[0] == lstRecords[j][0] && recTemp[1] == lstRecords[j][1] && recTemp[2] == lstRecords[j][2])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstRecords.Add(recTemp);
                }
            }
            int i_n_ci_kt_hc = 0;
            int i_n_ci_kf_hc = 0;
            int i_n_ci_kt_lf = 0;
            int i_n_ci_kf_lf = 0;
            int i_n_ci_kt_xz = 0;
            int i_n_ci_kf_xz = 0;
            for (int i = 0; i < lstRecords.Count; i++)
            {
                if (lstRecords[i][0] == "韩城")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("韩城" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_ci_kt_hc++;
                            }
                            else
                            {
                                i_n_ci_kf_hc++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "临汾")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("临汾" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_ci_kt_lf++;
                            }
                            else
                            {
                                i_n_ci_kf_lf++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "忻州")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("忻州" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_ci_kt_xz++;
                            }
                            else
                            {
                                i_n_ci_kf_xz++;
                            }
                            break;
                        }
                    }
                }
            }
            lstRecords.Clear();
            lstOldTemp.Clear();
            lstNowTemp.Clear();

            sqlTemp = "select place,jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shifouhege like '%是%' and shigongriqi between '" + nianBegin + "' and '" + nianEnd + "'";
            dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[3];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    recTemp[2] = dtTemp.Rows[i]["cengwei"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstRecords.Count; j++)
                    {
                        if (recTemp[0] == lstRecords[j][0] && recTemp[1] == lstRecords[j][1] && recTemp[2] == lstRecords[j][2])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstRecords.Add(recTemp);
                }
            }
            int i_n_ce_kt_hc = 0;
            int i_n_ce_kf_hc = 0;
            int i_n_ce_kt_lf = 0;
            int i_n_ce_kf_lf = 0;
            int i_n_ce_kt_xz = 0;
            int i_n_ce_kf_xz = 0;
            for (int i = 0; i < lstRecords.Count; i++)
            {
                if (lstRecords[i][0] == "韩城")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("韩城" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_ce_kt_hc++;
                            }
                            else
                            {
                                i_n_ce_kf_hc++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "临汾")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("临汾" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_ce_kt_lf++;
                            }
                            else
                            {
                                i_n_ce_kf_lf++;
                            }
                            break;
                        }
                    }
                }
                if (lstRecords[i][0] == "忻州")
                {
                    for (int j = 0; j < lstJingbie.Count; j++)
                    {
                        if ("忻州" == lstJingbie[j][0] && lstRecords[i][1].Trim() == lstJingbie[j][1])
                        {
                            if (lstJingbie[j][2].Trim() == "勘探")
                            {
                                i_n_ce_kt_xz++;
                            }
                            else
                            {
                                i_n_ce_kf_xz++;
                            }
                            break;
                        }
                    }
                }
            }
            lstRecords.Clear();
            lstOldTemp.Clear();
            lstNowTemp.Clear();

            string strData = "";
            string type = lblType.Text;
            if (type != "nian")
            {
                System.Data.DataTable dt_yl = new System.Data.DataTable();
                DataColumn yl_col_class = new DataColumn("类别");
                dt_yl.Columns.Add(yl_col_class);
                DataColumn yl_col_qukuai = new DataColumn("区块");
                dt_yl.Columns.Add(yl_col_qukuai);
                DataColumn zhouyalie_kou = new DataColumn("周压裂（口）");
                dt_yl.Columns.Add(zhouyalie_kou);
                DataColumn zhouyalie_ci = new DataColumn("周压裂（次）");
                dt_yl.Columns.Add(zhouyalie_ci);
                DataColumn zhouyalie_ceng = new DataColumn("周压裂（层）");
                dt_yl.Columns.Add(zhouyalie_ceng);
                DataColumn nianyalie_kou = new DataColumn("年压裂（口）");
                dt_yl.Columns.Add(nianyalie_kou);
                DataColumn nianyalie_ci = new DataColumn("年压裂（次）");
                dt_yl.Columns.Add(nianyalie_ci);
                DataColumn nianyalie_ceng = new DataColumn("年压裂（层）");
                dt_yl.Columns.Add(nianyalie_ceng);
                DataColumn yl_beizhu = new DataColumn("备注");
                dt_yl.Columns.Add(yl_beizhu);

                DataRow dt_yl_kthc_row = dt_yl.NewRow();
                dt_yl_kthc_row["类别"] = "勘探";
                dt_yl_kthc_row["区块"] = "韩城";
                dt_yl_kthc_row["周压裂（口）"] = i_z_yw_kt_hc;
                dt_yl_kthc_row["周压裂（次）"] = i_z_ci_kt_hc;
                dt_yl_kthc_row["周压裂（层）"] = i_z_ce_kt_hc;
                dt_yl_kthc_row["年压裂（口）"] = i_n_yw_kt_hc;
                dt_yl_kthc_row["年压裂（次）"] = i_n_ci_kt_hc;
                dt_yl_kthc_row["年压裂（层）"] = i_n_ce_kt_hc;
                dt_yl.Rows.Add(dt_yl_kthc_row);

                strData += i_z_yw_kt_hc + fg_field;
                strData += i_z_ci_kt_hc + fg_field;
                strData += i_z_ce_kt_hc + fg_field;
                strData += i_n_yw_kt_hc + fg_field;
                strData += i_n_ci_kt_hc + fg_field;
                strData += i_n_ce_kt_hc + fg_field;
                strData += fg_record;

                DataRow dt_yl_ktlf_row = dt_yl.NewRow();
                dt_yl_ktlf_row["类别"] = "勘探";
                dt_yl_ktlf_row["区块"] = "临汾";
                dt_yl_ktlf_row["周压裂（口）"] = i_z_yw_kt_lf;
                dt_yl_ktlf_row["周压裂（次）"] = i_z_ci_kt_lf;
                dt_yl_ktlf_row["周压裂（层）"] = i_z_ce_kt_lf;
                dt_yl_ktlf_row["年压裂（口）"] = i_n_yw_kt_lf;
                dt_yl_ktlf_row["年压裂（次）"] = i_n_ci_kt_lf;
                dt_yl_ktlf_row["年压裂（层）"] = i_n_ce_kt_lf;
                dt_yl.Rows.Add(dt_yl_ktlf_row);

                strData += i_z_yw_kt_lf + fg_field;
                strData += i_z_ci_kt_lf + fg_field;
                strData += i_z_ce_kt_lf + fg_field;
                strData += i_n_yw_kt_lf + fg_field;
                strData += i_n_ci_kt_lf + fg_field;
                strData += i_n_ce_kt_lf + fg_field;
                strData += fg_record;

                //压裂表勘探忻州行  填充数据
                DataRow dt_yl_ktxz_row = dt_yl.NewRow();
                dt_yl_ktxz_row["类别"] = "勘探";
                dt_yl_ktxz_row["区块"] = "忻州";
                dt_yl_ktxz_row["周压裂（口）"] = i_z_yw_kt_xz;
                dt_yl_ktxz_row["周压裂（次）"] = i_z_ci_kt_xz;
                dt_yl_ktxz_row["周压裂（层）"] = i_z_ce_kt_xz;
                dt_yl_ktxz_row["年压裂（口）"] = i_n_yw_kt_xz;
                dt_yl_ktxz_row["年压裂（次）"] = i_n_ci_kt_xz;
                dt_yl_ktxz_row["年压裂（层）"] = i_n_ce_kt_xz;
                dt_yl.Rows.Add(dt_yl_ktxz_row);

                strData += i_z_yw_kt_xz + fg_field;
                strData += i_z_ci_kt_xz + fg_field;
                strData += i_z_ce_kt_xz + fg_field;
                strData += i_n_yw_kt_xz + fg_field;
                strData += i_n_ci_kt_xz + fg_field;
                strData += i_n_ce_kt_xz + fg_field;
                strData += fg_record;

                //压裂表开发韩城行  填充数据
                DataRow dt_yl_kfhc_row = dt_yl.NewRow();
                dt_yl_kfhc_row["类别"] = "开发";
                dt_yl_kfhc_row["区块"] = "韩城";
                dt_yl_kfhc_row["周压裂（口）"] = i_z_yw_kf_hc;
                dt_yl_kfhc_row["周压裂（次）"] = i_z_ci_kf_hc;
                dt_yl_kfhc_row["周压裂（层）"] = i_z_ce_kf_hc;
                dt_yl_kfhc_row["年压裂（口）"] = i_n_yw_kf_hc;
                dt_yl_kfhc_row["年压裂（次）"] = i_n_ci_kf_hc;
                dt_yl_kfhc_row["年压裂（层）"] = i_n_ce_kf_hc;
                dt_yl.Rows.Add(dt_yl_kfhc_row);

                strData += i_z_yw_kf_hc + fg_field;
                strData += i_z_ci_kf_hc + fg_field;
                strData += i_z_ce_kf_hc + fg_field;
                strData += i_n_yw_kf_hc + fg_field;
                strData += i_n_ci_kf_hc + fg_field;
                strData += i_n_ce_kf_hc + fg_field;
                strData += fg_record;

                //压裂表开发临汾行  填充数据
                DataRow dt_yl_kflf_row = dt_yl.NewRow();
                dt_yl_kflf_row["类别"] = "开发";
                dt_yl_kflf_row["区块"] = "临汾";
                dt_yl_kflf_row["周压裂（口）"] = i_z_yw_kf_lf;
                dt_yl_kflf_row["周压裂（次）"] = i_z_ci_kf_lf;
                dt_yl_kflf_row["周压裂（层）"] = i_z_ce_kf_lf;
                dt_yl_kflf_row["年压裂（口）"] = i_n_yw_kf_lf;
                dt_yl_kflf_row["年压裂（次）"] = i_n_ci_kf_lf;
                dt_yl_kflf_row["年压裂（层）"] = i_n_ce_kf_lf;
                dt_yl.Rows.Add(dt_yl_kflf_row);

                strData += i_z_yw_kf_lf + fg_field;
                strData += i_z_ci_kf_lf + fg_field;
                strData += i_z_ce_kf_lf + fg_field;
                strData += i_n_yw_kf_lf + fg_field;
                strData += i_n_ci_kf_lf + fg_field;
                strData += i_n_ce_kf_lf + fg_field;
                strData += fg_record;

                //压裂表开发忻州行  填充数据
                DataRow dt_yl_kfxz_row = dt_yl.NewRow();
                dt_yl_kfxz_row["类别"] = "开发";
                dt_yl_kfxz_row["区块"] = "忻州";
                dt_yl_kfxz_row["周压裂（口）"] = i_z_yw_kf_xz;
                dt_yl_kfxz_row["周压裂（次）"] = i_z_ci_kf_xz;
                dt_yl_kfxz_row["周压裂（层）"] = i_z_ce_kf_xz;
                dt_yl_kfxz_row["年压裂（口）"] = i_n_yw_kf_xz;
                dt_yl_kfxz_row["年压裂（次）"] = i_n_ci_kf_xz;
                dt_yl_kfxz_row["年压裂（层）"] = i_n_ce_kf_xz;
                dt_yl.Rows.Add(dt_yl_kfxz_row);

                strData += i_z_yw_kf_xz + fg_field;
                strData += i_z_ci_kf_xz + fg_field;
                strData += i_z_ce_kf_xz + fg_field;
                strData += i_n_yw_kf_xz + fg_field;
                strData += i_n_ci_kf_xz + fg_field;
                strData += i_n_ce_kf_xz + fg_field;
                strData += "";

                dataYalie.Text = strData;

                grd_yl.DataSource = dt_yl;
                grd_yl.DataBind();
                GroupRows(grd_yl, 0);
            }
            else
            {
                System.Data.DataTable dt_yl = new System.Data.DataTable();
                DataColumn yl_col_class = new DataColumn("类别");
                dt_yl.Columns.Add(yl_col_class);
                DataColumn yl_col_qukuai = new DataColumn("区块");
                dt_yl.Columns.Add(yl_col_qukuai);
                DataColumn nianyalie_kou = new DataColumn("年压裂（口）");
                dt_yl.Columns.Add(nianyalie_kou);
                DataColumn nianyalie_ci = new DataColumn("年压裂（次）");
                dt_yl.Columns.Add(nianyalie_ci);
                DataColumn nianyalie_ceng = new DataColumn("年压裂（层）");
                dt_yl.Columns.Add(nianyalie_ceng);
                DataColumn yl_beizhu = new DataColumn("备注");
                dt_yl.Columns.Add(yl_beizhu);

                DataRow dt_yl_kthc_row = dt_yl.NewRow();
                dt_yl_kthc_row["类别"] = "勘探";
                dt_yl_kthc_row["区块"] = "韩城";
                dt_yl_kthc_row["年压裂（口）"] = i_n_yw_kt_hc;
                dt_yl_kthc_row["年压裂（次）"] = i_n_ci_kt_hc;
                dt_yl_kthc_row["年压裂（层）"] = i_n_ce_kt_hc;
                dt_yl.Rows.Add(dt_yl_kthc_row);

                strData += i_z_yw_kt_hc + fg_field;
                strData += i_z_ci_kt_hc + fg_field;
                strData += i_z_ce_kt_hc + fg_field;
                strData += i_n_yw_kt_hc + fg_field;
                strData += i_n_ci_kt_hc + fg_field;
                strData += i_n_ce_kt_hc + fg_field;
                strData += fg_record;

                DataRow dt_yl_ktlf_row = dt_yl.NewRow();
                dt_yl_ktlf_row["类别"] = "勘探";
                dt_yl_ktlf_row["区块"] = "临汾";
                dt_yl_ktlf_row["年压裂（口）"] = i_n_yw_kt_lf;
                dt_yl_ktlf_row["年压裂（次）"] = i_n_ci_kt_lf;
                dt_yl_ktlf_row["年压裂（层）"] = i_n_ce_kt_lf;
                dt_yl.Rows.Add(dt_yl_ktlf_row);

                strData += i_z_yw_kt_lf + fg_field;
                strData += i_z_ci_kt_lf + fg_field;
                strData += i_z_ce_kt_lf + fg_field;
                strData += i_n_yw_kt_lf + fg_field;
                strData += i_n_ci_kt_lf + fg_field;
                strData += i_n_ce_kt_lf + fg_field;
                strData += fg_record;

                //压裂表勘探忻州行  填充数据
                DataRow dt_yl_ktxz_row = dt_yl.NewRow();
                dt_yl_ktxz_row["类别"] = "勘探";
                dt_yl_ktxz_row["区块"] = "忻州";
                dt_yl_ktxz_row["年压裂（口）"] = i_n_yw_kt_xz;
                dt_yl_ktxz_row["年压裂（次）"] = i_n_ci_kt_xz;
                dt_yl_ktxz_row["年压裂（层）"] = i_n_ce_kt_xz;
                dt_yl.Rows.Add(dt_yl_ktxz_row);

                strData += i_z_yw_kt_xz + fg_field;
                strData += i_z_ci_kt_xz + fg_field;
                strData += i_z_ce_kt_xz + fg_field;
                strData += i_n_yw_kt_xz + fg_field;
                strData += i_n_ci_kt_xz + fg_field;
                strData += i_n_ce_kt_xz + fg_field;
                strData += fg_record;

                //压裂表开发韩城行  填充数据
                DataRow dt_yl_kfhc_row = dt_yl.NewRow();
                dt_yl_kfhc_row["类别"] = "开发";
                dt_yl_kfhc_row["区块"] = "韩城";
                dt_yl_kfhc_row["年压裂（口）"] = i_n_yw_kf_hc;
                dt_yl_kfhc_row["年压裂（次）"] = i_n_ci_kf_hc;
                dt_yl_kfhc_row["年压裂（层）"] = i_n_ce_kf_hc;
                dt_yl.Rows.Add(dt_yl_kfhc_row);

                strData += i_z_yw_kf_hc + fg_field;
                strData += i_z_ci_kf_hc + fg_field;
                strData += i_z_ce_kf_hc + fg_field;
                strData += i_n_yw_kf_hc + fg_field;
                strData += i_n_ci_kf_hc + fg_field;
                strData += i_n_ce_kf_hc + fg_field;
                strData += fg_record;

                //压裂表开发临汾行  填充数据
                DataRow dt_yl_kflf_row = dt_yl.NewRow();
                dt_yl_kflf_row["类别"] = "开发";
                dt_yl_kflf_row["区块"] = "临汾";
                dt_yl_kflf_row["年压裂（口）"] = i_n_yw_kf_lf;
                dt_yl_kflf_row["年压裂（次）"] = i_n_ci_kf_lf;
                dt_yl_kflf_row["年压裂（层）"] = i_n_ce_kf_lf;
                dt_yl.Rows.Add(dt_yl_kflf_row);

                strData += i_z_yw_kf_lf + fg_field;
                strData += i_z_ci_kf_lf + fg_field;
                strData += i_z_ce_kf_lf + fg_field;
                strData += i_n_yw_kf_lf + fg_field;
                strData += i_n_ci_kf_lf + fg_field;
                strData += i_n_ce_kf_lf + fg_field;
                strData += fg_record;

                //压裂表开发忻州行  填充数据
                DataRow dt_yl_kfxz_row = dt_yl.NewRow();
                dt_yl_kfxz_row["类别"] = "开发";
                dt_yl_kfxz_row["区块"] = "忻州";
                dt_yl_kfxz_row["年压裂（口）"] = i_n_yw_kf_xz;
                dt_yl_kfxz_row["年压裂（次）"] = i_n_ci_kf_xz;
                dt_yl_kfxz_row["年压裂（层）"] = i_n_ce_kf_xz;
                dt_yl.Rows.Add(dt_yl_kfxz_row);

                strData += i_z_yw_kf_xz + fg_field;
                strData += i_z_ci_kf_xz + fg_field;
                strData += i_z_ce_kf_xz + fg_field;
                strData += i_n_yw_kf_xz + fg_field;
                strData += i_n_ci_kf_xz + fg_field;
                strData += i_n_ce_kf_xz + fg_field;
                strData += "";

                dataYalie.Text = strData;

                grd_yl.DataSource = dt_yl;
                grd_yl.DataBind();
                GroupRows(grd_yl, 0);
            }
        }

        //开钻验收
        private void getGJKzys(string begin, string end)
        {
            System.Data.DataTable dt = null;
            string sql = "select lururiqi,qukuai,jinghao,shigongduiwu,jiandu,yikaishijian,erkaishijian,yinsu,cuoshi,beizhu from Report_yikaiyanshou where lururiqi between '" + begin + "' and '" + end + "'";
            dt = DataBaseHelper.query(sql);

            int iTemp = dt.Rows.Count;


            if (iTemp == 0)
            {
                dt.Rows.Add();
            }

            string strData = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int nCols = dt.Columns.Count;
                for (int j = 0; j < nCols; j++)
                {
                    if(j == nCols-1) strData += dt.Rows[i][j].ToString() + fg_record;
                    else strData += dt.Rows[i][j].ToString() + fg_field;
                }
            }

            grd_yierkaiyanshou.DataSource = dt;
            grd_yierkaiyanshou.DataBind();
            int iRows_yierkaiyanshou = grd_yierkaiyanshou.Rows.Count;
            for (int i = 0; i < iRows_yierkaiyanshou; i++)
            {
                string strDate = grd_yierkaiyanshou.Rows[i].Cells[0].Text.ToString();
                grd_yierkaiyanshou.Rows[i].Cells[0].Text = strDate.Split(' ')[0];
                string strDate_yikai = grd_yierkaiyanshou.Rows[i].Cells[5].Text.ToString();
                grd_yierkaiyanshou.Rows[i].Cells[5].Text = strDate_yikai.Split(' ')[0];
                string strDate_erkai = grd_yierkaiyanshou.Rows[i].Cells[6].Text.ToString();
                grd_yierkaiyanshou.Rows[i].Cells[6].Text = strDate_erkai.Split(' ')[0];
            }

            List<string> lstJing = new List<string>();
            sql = "select jinghao from Xls_Zj_Rbb_Tjb where ykys_riqi between '" + begin + "' and '" + end + "'";
            dt = DataBaseHelper.query(sql);
            int iYikaiYanshou = dt.Rows.Count;
            for (int i = 0; i < iYikaiYanshou; i++)
            {
                string jh = dt.Rows[i]["jinghao"].ToString().Trim();
                bool b = true;
                for (int j = 0; j < lstJing.Count; j++)
                {
                    if (jh == lstJing[j])
                    {
                        b = false;
                        break;
                    }
                }
                if (b) lstJing.Add(jh);
            }
            iYikaiYanshou = lstJing.Count;
            lstJing.Clear();

            sql = "select jinghao from Xls_Zj_Rbb_Tjb where ekys_riqi between '" + begin + "' and '" + end + "'";
            dt = DataBaseHelper.query(sql);
            int iErkaiYanshou = dt.Rows.Count;
            for (int i = 0; i < iErkaiYanshou; i++)
            {
                string jh = dt.Rows[i]["jinghao"].ToString().Trim();
                bool b = true;
                for (int j = 0; j < lstJing.Count; j++)
                {
                    if (jh == lstJing[j])
                    {
                        b = false;
                        break;
                    }
                }
                if (b) lstJing.Add(jh);
            }
            iErkaiYanshou = lstJing.Count;
            lstJing.Clear();

            sql = "select jinghao from Report_yikaiyanshou where erkaishijian is not null and lururiqi between '" + begin + "' and '" + end + "'";
            dt = DataBaseHelper.query(sql);
            int iErkaiWenti = dt.Rows.Count;
            int iYikaiWenti = iTemp - iErkaiWenti;

            string str = "";
            string type = lblType.Text;
            if (type == "zhou") str = "本周";
            if (type == "yue") str = "本月";
            if (type == "ji") str = "本季";
            if (type == "nian") str = "本年";
            this.lblKzysInfo.Text = str + "一开验收" + iYikaiYanshou + "口井，不合格" + iYikaiWenti + "口。二开验收" + iErkaiYanshou + "口井，不合格" + iErkaiWenti + "口。";

            strData += lblKzysInfo.Text;
            dataKzys.Text = strData;
        }

        protected void yierkaiyanshou_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_yierkaiyanshou.PageIndex = e.NewPageIndex;
            string[] ss = lblRiqi.Text.Split(',');
            getGJKzys(ss[0], ss[1]);
        }

        //井身质量
        private void getGJJszl(string begin, string end)
        {
            System.Data.DataTable dt = new System.Data.DataTable("jingshenzhiliang");
            string sql = "select lururiqi,qukuai,jinghao,shigongduiwu,jiandu,yinsu,cuoshi,beizhu from Report_jingshenzhiliang where lururiqi between '" + begin + "' and '" + end + "'";
            dt = DataBaseHelper.query(sql);

            int iTemp = dt.Rows.Count;

            if (iTemp == 0)
            {
                dt.Rows.Add();
            }

            string strData = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int nCols = dt.Columns.Count;
                for (int j = 0; j < nCols; j++)
                {
                    if (j == nCols - 1) strData += dt.Rows[i][j].ToString() + fg_record;
                    else strData += dt.Rows[i][j].ToString() + fg_field;
                }
            }

            grd_jingshenzhiliang.DataSource = dt;
            grd_jingshenzhiliang.DataBind();
            //去除时分秒的日期格式
            int iRows_jingshenzhiliang = grd_jingshenzhiliang.Rows.Count;
            for (int i = 0; i < iRows_jingshenzhiliang; i++)
            {
                string strDate = grd_jingshenzhiliang.Rows[i].Cells[0].Text.ToString();
                grd_jingshenzhiliang.Rows[i].Cells[0].Text = strDate.Split(' ')[0];
            }

            string sqlTemp = "select jinghao from Xls_Zj_Rbb_Zj where riqi = '" + end + "' and gongkuang not like '%停工%'";
            System.Data.DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            int count = dtTemp.Rows.Count;

            string str = "";
            string type = lblType.Text;
            if (type == "zhou") str = "本周";
            if (type == "yue") str = "本月";
            if (type == "ji") str = "本季";
            if (type == "nian") str = "本年";
            this.lblJszlInfo.Text = str + "正钻" + count + "口井，井身质量超标" + iTemp + "口。";

            strData += lblJszlInfo.Text;
            dataJszl.Text = strData;
        }

        protected void jingshenzhiliang_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_jingshenzhiliang.PageIndex = e.NewPageIndex;
            string[] ss = lblRiqi.Text.Split(',');
            getGJJszl(ss[0], ss[1]);
        }

        //下套管作业
        private void getGJXtgzy(string begin, string end)
        {
            System.Data.DataTable dt_xiataoguan = new System.Data.DataTable("xiataoguan");
            string sql_xiataoguan = "select lururiqi,qukuai,jinghao,shigongduiwu,jiandu,yichang,cuoshi,beizhu from Report_xtgzuoye where lururiqi between '" + begin + "' and '" + end + "'";
            dt_xiataoguan = DataBaseHelper.query(sql_xiataoguan);

            int iTemp = dt_xiataoguan.Rows.Count;

            if (iTemp == 0)
            {
                dt_xiataoguan.Rows.Add();
            }

            string strData = "";
            for (int i = 0; i < dt_xiataoguan.Rows.Count; i++)
            {
                int nCols = dt_xiataoguan.Columns.Count;
                for (int j = 0; j < nCols; j++)
                {
                    if (j == nCols - 1) strData += dt_xiataoguan.Rows[i][j].ToString() + fg_record;
                    else strData += dt_xiataoguan.Rows[i][j].ToString() + fg_field;
                }
            }

            grd_xiataoguan.DataSource = dt_xiataoguan;
            grd_xiataoguan.DataBind();
            //去除时分秒的日期格式
            int iRows_xiataoguan = grd_xiataoguan.Rows.Count;
            for (int i = 0; i < iRows_xiataoguan; i++)
            {
                string strDate = grd_xiataoguan.Rows[i].Cells[0].Text.ToString();
                grd_xiataoguan.Rows[i].Cells[0].Text = strDate.Split(' ')[0];
            }

            List<string[]> lstTemp = new List<string[]>();
            string sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Xtg where riqi between '" + begin + "' and '" + end + "'";
            System.Data.DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstTemp.Count; j++)
                    {
                        if (recTemp[0] == lstTemp[j][0] && recTemp[1] == lstTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstTemp.Add(recTemp);
                }
            }
            int count = lstTemp.Count;
            string str = "";
            string type = lblType.Text;
            if (type == "zhou") str = "本周";
            if (type == "yue") str = "本月";
            if (type == "ji") str = "本季";
            if (type == "nian") str = "本年";
            this.lblXtgInfo.Text = str + "下套管监督" + count + "口井，施工异常" + iTemp + "口。";

            strData += lblXtgInfo.Text;
            dataXtgzy.Text = strData;
        }

        protected void xiataoguan_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_xiataoguan.PageIndex = e.NewPageIndex;
            string[] ss = lblRiqi.Text.Split(',');
            getGJXtgzy(ss[0], ss[1]);
        }

        //固井作业
        private void getGJGjzy(string begin, string end)
        {
            System.Data.DataTable dt_gujingzuoye = new System.Data.DataTable("gujingzuoye");
            string sql_gujingzuoye = "select lururiqi,qukuai,jinghao,shigongduiwu,jiandu,yichang,cuoshi,beizhu from Report_gjzuoye where lururiqi between '" + begin + "' and '" + end + "'";
            dt_gujingzuoye = DataBaseHelper.query(sql_gujingzuoye);

            int iTemp = dt_gujingzuoye.Rows.Count;

            if (iTemp == 0)
            {
                dt_gujingzuoye.Rows.Add();
            }

            string strData = "";
            for (int i = 0; i < dt_gujingzuoye.Rows.Count; i++)
            {
                int nCols = dt_gujingzuoye.Columns.Count;
                for (int j = 0; j < nCols; j++)
                {
                    if (j == nCols - 1) strData += dt_gujingzuoye.Rows[i][j].ToString() + fg_record;
                    else strData += dt_gujingzuoye.Rows[i][j].ToString() + fg_field;
                }
            }

            grd_gujingzuoye.DataSource = dt_gujingzuoye;
            grd_gujingzuoye.DataBind();
            //去除时分秒的日期格式
            int iRows_gujingzuoye = grd_gujingzuoye.Rows.Count;
            for (int i = 0; i < iRows_gujingzuoye; i++)
            {
                string strDate = grd_gujingzuoye.Rows[i].Cells[0].Text.ToString();
                grd_gujingzuoye.Rows[i].Cells[0].Text = strDate.Split(' ')[0];
            }

            List<string[]> lstTemp = new List<string[]>();
            string sqlTemp = "select place,jinghao from Xls_Zj_Rbb_Gj where riqi between '" + begin + "' and '" + end + "'";
            System.Data.DataTable dtTemp = DataBaseHelper.query(sqlTemp);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string[] recTemp = new string[2];
                    recTemp[0] = dtTemp.Rows[i]["place"].ToString();
                    recTemp[1] = dtTemp.Rows[i]["jinghao"].ToString();
                    bool b = true;
                    for (int j = 0; j < lstTemp.Count; j++)
                    {
                        if (recTemp[0] == lstTemp[j][0] && recTemp[1] == lstTemp[j][1])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b) lstTemp.Add(recTemp);
                }
            }
            int count = lstTemp.Count;
            string str = "";
            string type = lblType.Text;
            if (type == "zhou") str = "本周";
            if (type == "yue") str = "本月";
            if (type == "ji") str = "本季";
            if (type == "nian") str = "本年";
            this.lblGjInfo.Text = str + "固井监督" + count + "口井，施工异常" + iTemp + "口。";

            strData += lblGjInfo.Text;
            dataGjzy.Text = strData;
        }

        protected void gujingzuoye_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_gujingzuoye.PageIndex = e.NewPageIndex;
            string[] ss = lblRiqi.Text.Split(',');
            getGJGjzy(ss[0], ss[1]);
        }

        //处理井漏
        private void getGJCljl(string begin, string end)
        {
            System.Data.DataTable dt_chulijinglou = new System.Data.DataTable("chulijinglou");
            string sql_chulijinglou = "select lururiqi,qukuai,jinghao,shigongduiwu,jiandu,loushicengwei,loushiliang,cuoshi,jieguo,beizhu from Report_jinglou where lururiqi between '" + begin + "' and '" + end + "'";
            dt_chulijinglou = DataBaseHelper.query(sql_chulijinglou);

            int iTemp = dt_chulijinglou.Rows.Count;

            if (iTemp == 0)
            {
                dt_chulijinglou.Rows.Add();
            }

            string strData = "";
            for (int i = 0; i < dt_chulijinglou.Rows.Count; i++)
            {
                int nCols = dt_chulijinglou.Columns.Count;
                for (int j = 0; j < nCols; j++)
                {
                    if (j == nCols - 1) strData += dt_chulijinglou.Rows[i][j].ToString() + fg_record;
                    else strData += dt_chulijinglou.Rows[i][j].ToString() + fg_field;
                }
            }

            grd_chulijinglou.DataSource = dt_chulijinglou;
            grd_chulijinglou.DataBind();

            //去除时分秒的日期格式
            int iRows_chulijinglou = grd_chulijinglou.Rows.Count;
            for (int i = 0; i < iRows_chulijinglou; i++)
            {
                string strDate = grd_chulijinglou.Rows[i].Cells[0].Text.ToString();
                grd_chulijinglou.Rows[i].Cells[0].Text = strDate.Split(' ')[0];
            }

            sql_chulijinglou = "select * from Report_jinglou where lururiqi between '" + begin + "' and '" + end + "' and jieguo like '%成功%'";
            dt_chulijinglou = DataBaseHelper.query(sql_chulijinglou);
            int c = dt_chulijinglou.Rows.Count;
            string str = "";
            string type = lblType.Text;
            if (type == "zhou") str = "本周";
            if (type == "yue") str = "本月";
            if (type == "ji") str = "本季";
            if (type == "nian") str = "本年";
            lblJinglouInfo.Text = str + "共处理井漏" + iTemp + "口井，处理成功" + c + "口。";

            strData += lblJinglouInfo.Text;
            dataCljl.Text = strData;
        }

        protected void chulijinglou_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_chulijinglou.PageIndex = e.NewPageIndex;
            string[] ss = lblRiqi.Text.Split(',');
            getGJCljl(ss[0], ss[1]);
        }

        //处理涌水
        private void getGJClys(string begin, string end)
        {
            System.Data.DataTable dt_chuliyongshui = new System.Data.DataTable("chuliyongshui");
            string sql_chuliyongshui = "select lururiqi,qukuai,jinghao,shigongduiwu,jiandu,yongshuicengwei,yongshuiliang,cuoshi,jieguo,beizhu from Report_yongshui where lururiqi between '" + begin + "' and '" + end + "'";
            dt_chuliyongshui = DataBaseHelper.query(sql_chuliyongshui);

            int iTemp = dt_chuliyongshui.Rows.Count;

            if (iTemp == 0)
            {
                dt_chuliyongshui.Rows.Add();
            }

            string strData = "";
            for (int i = 0; i < dt_chuliyongshui.Rows.Count; i++)
            {
                int nCols = dt_chuliyongshui.Columns.Count;
                for (int j = 0; j < nCols; j++)
                {
                    if (j == nCols - 1) strData += dt_chuliyongshui.Rows[i][j].ToString() + fg_record;
                    else strData += dt_chuliyongshui.Rows[i][j].ToString() + fg_field;
                }
            }

            grd_chuliyongshui.DataSource = dt_chuliyongshui;
            grd_chuliyongshui.DataBind();
            //去除时分秒的日期格式
            int iRows_chuliyongshui = grd_chuliyongshui.Rows.Count;
            for (int i = 0; i < iRows_chuliyongshui; i++)
            {
                string strDate = grd_chuliyongshui.Rows[i].Cells[0].Text.ToString();
                grd_chuliyongshui.Rows[i].Cells[0].Text = strDate.Split(' ')[0];
            }

            dt_chuliyongshui = new System.Data.DataTable("chuliyongshui");
            sql_chuliyongshui = "select * from Report_yongshui where lururiqi between '" + begin + "' and '" + end + "' and jieguo like '%成功%'";
            dt_chuliyongshui = DataBaseHelper.query(sql_chuliyongshui);
            int c = dt_chuliyongshui.Rows.Count;
            string str = "";
            string type = lblType.Text;
            if (type == "zhou") str = "本周";
            if (type == "yue") str = "本月";
            if (type == "ji") str = "本季";
            if (type == "nian") str = "本年";
            lblYongshuiInfo.Text = str + "共处理涌水" + iTemp + "口井，处理成功" + c + "口。";

            strData += lblYongshuiInfo.Text;
            dataClys.Text = strData;
        }

        protected void chuliyongshui_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_chuliyongshui.PageIndex = e.NewPageIndex;
            string[] ss = lblRiqi.Text.Split(',');
            getGJClys(ss[0], ss[1]);
        }

        //阻工因素
        private void getGJZgys(string begin, string end)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select lururiqi,qukuai,taoguanzhiliangwenti,xiayu,gongnongguanxi,cheliangweixiu,jingchangbanqian,dengdaijingtaibanqian,beishuipeiye,beizhu from Report_zugongyinsu where lururiqi between '" + begin + "' and '" + end + "'";
            dt = DataBaseHelper.query(sql);

            int iTemp = dt.Rows.Count;

            if (iTemp == 0)
            {
                dt.Rows.Add();
            }

            string strData = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int nCols = dt.Columns.Count;
                for (int j = 0; j < nCols; j++)
                {
                    if (j == nCols - 1) strData += dt.Rows[i][j].ToString() + fg_record;
                    else strData += dt.Rows[i][j].ToString() + fg_field;
                }
            }

            grd_zugongyinsu.DataSource = dt;
            grd_zugongyinsu.DataBind();

            int iRows = grd_zugongyinsu.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                string strDate = grd_zugongyinsu.Rows[i].Cells[0].Text.ToString();
                grd_zugongyinsu.Rows[i].Cells[0].Text = strDate.Split(' ')[0];
            }

            dataZgys.Text = strData;
        }

        protected void zugongyinsu_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_zugongyinsu.PageIndex = e.NewPageIndex;
            string[] ss = lblRiqi.Text.Split(',');
            getGJZgys(ss[0], ss[1]);
        }

        //入井材料
        private void getGJRjcl(string begin, string end)
        {
            List<string[]> lstRecords = new List<string[]>();

            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select place,shigongduiwu from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' group by place,shigongduiwu";
            dt = DataBaseHelper.query(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] recs = new string[6];
                recs[0] = dt.Rows[i]["place"].ToString();
                recs[1] = dt.Rows[i]["shigongduiwu"].ToString();
                lstRecords.Add(recs);
            }
            for (int i = 0; i < lstRecords.Count; i++)
            {
                sql = "select place,jinghao,cengwei,wanchengbaifenbi from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' and shigongduiwu='" + lstRecords[i][1] + "' group by place,jinghao,cengwei,wanchengbaifenbi";
                dt = DataBaseHelper.query(sql);
                int c1 = 0;
                int c2 = 0;
                int c3 = 0;
                int c4 = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string str = dt.Rows[j][3].ToString();
                        string s1 = str.Split('％')[0];
                        s1 = s1.Split('%')[0];
                        try
                        {
                            float f = float.Parse(s1);
                            if (f > 100) c1++;
                            else if (f <= 100 && f > 90) c2++;
                            else if (f <= 90 && f > 80) c3++;
                            else if (f <= 80) c4++;
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
                lstRecords[i][2] = c1.ToString();
                lstRecords[i][3] = c2.ToString();
                lstRecords[i][4] = c3.ToString();
                lstRecords[i][5] = c4.ToString();
            }

            dt = new System.Data.DataTable();
            dt.Columns.Add("f1");
            dt.Columns.Add("f2");
            dt.Columns.Add("f3");
            dt.Columns.Add("f4");
            dt.Columns.Add("f5");
            dt.Columns.Add("f6");
            for (int i = 0; i < lstRecords.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["f1"] = lstRecords[i][0];
                dr["f2"] = lstRecords[i][1];
                dr["f3"] = lstRecords[i][2];
                dr["f4"] = lstRecords[i][3];
                dr["f5"] = lstRecords[i][4];
                dr["f6"] = lstRecords[i][5];
                dt.Rows.Add(dr);
            }

            int iTemp = dt.Rows.Count;

            string strData = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int nCols = dt.Columns.Count;
                for (int j = 0; j < nCols; j++)
                {
                    if (j == nCols - 1) strData += dt.Rows[i][j].ToString() + fg_record;
                    else strData += dt.Rows[i][j].ToString() + fg_field;
                }
            }

            if (iTemp == 0)
            {
                dt.Rows.Add();
            }

            grd_rujingcailiao.DataSource = dt;
            grd_rujingcailiao.DataBind();

            dataRjcl.Text += strData;
        }

        protected void rujingcailiao_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_rujingcailiao.PageIndex = e.NewPageIndex;
            string[] ss = lblRiqi.Text.Split(',');
            getGJRjcl(ss[0], ss[1]);
        }

        //施工质量
        private void getGJSgzl(string begin, string end)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select place,jinghao,cenghao,shigongmiaoshu,fujian from Xls_Yl_Rbb_Sbyysm where yalieriqi between '" + begin + "' and '" + end + "' order by place";
            dt = DataBaseHelper.query(sql);

            int iTemp = dt.Rows.Count;

            if (iTemp == 0)
            {
                dt.Rows.Add();
            }

            string strData = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int nCols = dt.Columns.Count;
                for (int j = 0; j < nCols; j++)
                {
                    if (j == nCols - 1) strData += dt.Rows[i][j].ToString() + fg_record;
                    else strData += dt.Rows[i][j].ToString() + fg_field;
                }
            }

            grd_shigongzhiliang.DataSource = dt;
            grd_shigongzhiliang.DataBind();

            int iRows = grd_shigongzhiliang.Rows.Count;
            if (iTemp > 0)
            {
                for (int i = 0; i < iRows; i++)
                {
                    grd_shigongzhiliang.Rows[i].Cells[4].Text = "";
                }
            }

            dt = new System.Data.DataTable();
            sql = "select place,jinghao,cengwei from Xls_Yl_Rbb_Ylsg where shigongriqi between '" + begin + "' and '" + end + "' group by place,jinghao,cengwei order by place";
            dt = DataBaseHelper.query(sql);
            string str = "";
            string type = lblType.Text;
            if (type == "zhou") str = "本周";
            if (type == "yue") str = "本月";
            if (type == "ji") str = "本季";
            if (type == "nian") str = "本年";
            int c = dt.Rows.Count;
            if (iTemp > 0)
            {
                string cgl = String.Format("{0:N2}", (c - iTemp) * 100.0f / c) + "%";
                lblSgzlInfo.Text = str + "共" + iTemp + "口井压裂失败，一次成功率为" + cgl;
            }
            else
            {
                lblSgzlInfo.Text = str + "共" + iTemp + "口井压裂失败，一次成功率为100%";
            }

            strData += lblSgzlInfo.Text;
            dataSgzl.Text = strData;
        }

        protected void shigongzhiliang_IndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_shigongzhiliang.PageIndex = e.NewPageIndex;
            string[] ss = lblRiqi.Text.Split(',');
            getGJSgzl(ss[0], ss[1]);
        }

        //技术研究及其他工作
        private void getJishu(string begin, string end)
        {
            lblJishuyanjiuRiqi.Text = begin + " 至 " + end; 

            try
            {
                string str_query_hc = "select lururiqi,hancheng from Jianbaoluru where lururiqi between '" + begin + "' and '" + end + "'";
                System.Data.DataTable dt_hancheng = DataBaseHelper.query(str_query_hc);
                if (dt_hancheng.Rows.Count > 0)
                {
                    String ss = "";
                    for (int i = 0; i < dt_hancheng.Rows.Count; i++)
                    {
                        ss += ((DateTime)dt_hancheng.Rows[i]["lururiqi"]).ToString("yyyy-MM-dd") + " : \n";
                        ss += dt_hancheng.Rows[i]["hancheng"].ToString() + "\n";
                    }
                    txt_report_hc.Text = ss;
                }

                string str_query_lf = "select lururiqi,linfen from Jianbaoluru where lururiqi between '" + begin + "' and '" + end + "'";
                System.Data.DataTable dt_linfen = DataBaseHelper.query(str_query_lf);
                if (dt_linfen.Rows.Count > 0)
                {
                    String ss = "";
                    for (int i = 0; i < dt_linfen.Rows.Count; i++)
                    {
                        ss += ((DateTime)dt_linfen.Rows[i]["lururiqi"]).ToString("yyyy-MM-dd") + " : \n";
                        ss += dt_linfen.Rows[i]["linfen"].ToString() + "\n";
                    }
                    txt_report_lf.Text = ss;
                }

                string str_query_xz = "select lururiqi,xinzhou from Jianbaoluru where lururiqi between '" + begin + "' and '" + end + "'";
                System.Data.DataTable dt_xinzhou = DataBaseHelper.query(str_query_xz);
                if (dt_xinzhou.Rows.Count > 0)
                {
                    String ss = "";
                    for (int i = 0; i < dt_xinzhou.Rows.Count; i++)
                    {
                        ss += ((DateTime)dt_xinzhou.Rows[i]["lururiqi"]).ToString("yyyy-MM-dd") + " : \n";
                        ss += dt_xinzhou.Rows[i]["xinzhou"].ToString() + "\n";
                    }
                    txt_report_xz.Text = ss;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language=javascript>alert('" + ex.Message + "');</script>");
                return;
            }
        }


        private string getNianData(string begin, string end)
        {
            System.Data.DataTable dt = null;
            string sql = null;

            int iKzys_Hc_Yikai = 0;
            int iKzys_Lf_Yikai = 0;
            int iKzys_Xz_Yikai = 0;
            int iKzys_Hc_Yikai_Shibai = 0;
            int iKzys_Lf_Yikai_Shibai = 0;
            int iKzys_Xz_Yikai_Shibai = 0;
            int iKzys_Hc_Erkai = 0;
            int iKzys_Lf_Erkai = 0;
            int iKzys_Xz_Erkai = 0;
            int iKzys_Hc_Erkai_Shibai = 0;
            int iKzys_Lf_Erkai_Shibai = 0;
            int iKzys_Xz_Erkai_Shibai = 0;

            sql = "select COUNT(jinghao) as shuliang from Report_yikaiyanshou where qukuai='韩城' and yikaishijian between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Hc_Yikai_Shibai = dt.Rows.Count;

            sql = "select COUNT(jinghao) as shuliang from Report_yikaiyanshou where qukuai='临汾' and yikaishijian between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Lf_Yikai_Shibai = dt.Rows.Count;

            sql = "select COUNT(jinghao) as shuliang from Report_yikaiyanshou where qukuai='忻州' and yikaishijian between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Xz_Yikai_Shibai = dt.Rows.Count;

            sql = "select COUNT(jinghao) as shuliang from Report_yikaiyanshou where qukuai='韩城' and erkaishijian between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Hc_Erkai_Shibai = dt.Rows.Count;

            sql = "select COUNT(jinghao) as shuliang from Report_yikaiyanshou where qukuai='临汾' and erkaishijian between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Lf_Erkai_Shibai = dt.Rows.Count;

            sql = "select COUNT(jinghao) as shuliang from Report_yikaiyanshou where qukuai='忻州' and erkaishijian between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Xz_Erkai_Shibai = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Tjb where place='韩城' and ykys_riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Hc_Yikai = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Tjb where place='临汾' and ykys_riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Lf_Yikai = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Tjb where place='忻州' and ykys_riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Xz_Yikai = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Tjb where place='韩城' and ekys_riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Hc_Erkai = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Tjb where place='临汾' and ekys_riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Lf_Erkai = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Tjb where place='忻州' and ekys_riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iKzys_Xz_Erkai = dt.Rows.Count;


            int iJszl_Hc = 0;
            int iJszl_Lf = 0;
            int iJszl_Xz = 0;

            sql = "select COUNT(jinghao) from Report_jingshenzhiliang where qukuai='韩城' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iJszl_Hc = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_jingshenzhiliang where qukuai='临汾' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iJszl_Lf = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_jingshenzhiliang where qukuai='忻州' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iJszl_Xz = dt.Rows.Count;


            int iXtg_Hc = 0;
            int iXtg_Lf = 0;
            int iXtg_Xz = 0;
            int iXtg_Hc_Shibai = 0;
            int iXtg_Lf_Shibai = 0;
            int iXtg_Xz_Shibai = 0;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Xtg where place='韩城' and riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iXtg_Hc = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Xtg where place='临汾' and riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iXtg_Lf = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Xtg where place='忻州' and riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iXtg_Xz = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_xtgzuoye where qukuai='韩城' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iXtg_Hc_Shibai = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_xtgzuoye where qukuai='临汾' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iXtg_Lf_Shibai = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_xtgzuoye where qukuai='忻州' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iXtg_Xz_Shibai = dt.Rows.Count;


            int iGj_Hc = 0;
            int iGj_Lf = 0;
            int iGj_Xz = 0;
            int iGj_Hc_Shibai = 0;
            int iGj_Lf_Shibai = 0;
            int iGj_Xz_Shibai = 0;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Gj where place='韩城' and riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iGj_Hc = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Gj where place='临汾' and riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iGj_Lf = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Xls_Zj_Rbb_Gj where place='忻州' and riqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iGj_Xz = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_gjzuoye where qukuai='韩城' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iGj_Hc_Shibai = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_gjzuoye where qukuai='临汾' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iGj_Lf_Shibai = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_gjzuoye where qukuai='忻州' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iGj_Xz_Shibai = dt.Rows.Count;


            int iJl_Hc = 0;
            int iJl_Lf = 0;
            int iJl_Xz = 0;
            int iJl_Hc_cg = 0;
            int iJl_Lf_cg = 0;
            int iJl_Xz_cg = 0;

            sql = "select COUNT(jinghao) from Report_jinglou where qukuai='韩城' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iJl_Hc = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_jinglou where qukuai='临汾' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iJl_Lf = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_jinglou where qukuai='忻州' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iJl_Xz = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_jinglou where qukuai='韩城' and jieguo like '成功' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iJl_Hc_cg = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_jinglou where qukuai='临汾' and jieguo like '成功' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iJl_Lf_cg = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_jinglou where qukuai='忻州' and jieguo like '成功' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iJl_Xz_cg = dt.Rows.Count;


            int iYs_Hc = 0;
            int iYs_Lf = 0;
            int iYs_Xz = 0;
            int iYs_Hc_cg = 0;
            int iYs_Lf_cg = 0;
            int iYs_Xz_cg = 0;

            sql = "select COUNT(jinghao) from Report_yongshui where qukuai='韩城' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iYs_Hc = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_yongshui where qukuai='临汾' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iYs_Lf = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_yongshui where qukuai='忻州' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iYs_Xz = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_yongshui where qukuai='韩城' and jieguo like '成功' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iYs_Hc_cg = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_yongshui where qukuai='临汾' and jieguo like '成功' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iYs_Lf_cg = dt.Rows.Count;

            sql = "select COUNT(jinghao) from Report_yongshui where qukuai='忻州' and jieguo like '成功' and lururiqi between '" + begin + "' and '" + end + "' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iYs_Xz_cg = dt.Rows.Count;


            int iZgys_Hc_Tgzl = 0;
            int iZgys_Hc_Xy = 0;
            int iZgys_Hc_Gngx = 0;
            int iZgys_Hc_Clwx = 0;
            int iZgys_Hc_Jcbq = 0;
            int iZgys_Hc_Jtbq = 0;
            int iZgys_Hc_Bspy = 0;
            int iZgys_Lf_Tgzl = 0;
            int iZgys_Lf_Xy = 0;
            int iZgys_Lf_Gngx = 0;
            int iZgys_Lf_Clwx = 0;
            int iZgys_Lf_Jcbq = 0;
            int iZgys_Lf_Jtbq = 0;
            int iZgys_Lf_Bspy = 0;
            int iZgys_Xz_Tgzl = 0;
            int iZgys_Xz_Xy = 0;
            int iZgys_Xz_Gngx = 0;
            int iZgys_Xz_Clwx = 0;
            int iZgys_Xz_Jcbq = 0;
            int iZgys_Xz_Jtbq = 0;
            int iZgys_Xz_Bspy = 0;

            sql = "select taoguanzhiliangwenti,xiayu,gongnongguanxi,cheliangweixiu,jingchangbanqian,dengdaijingtaibanqian,beishuipeiye from Report_zugongyinsu where qukuai='韩城' and lururiqi between '" + begin + "' and '" + end + "'";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string v1 = dt.Rows[i][0].ToString();
                    string v2 = dt.Rows[i][1].ToString();
                    string v3 = dt.Rows[i][2].ToString();
                    string v4 = dt.Rows[i][3].ToString();
                    string v5 = dt.Rows[i][4].ToString();
                    string v6 = dt.Rows[i][5].ToString();
                    string v7 = dt.Rows[i][6].ToString();
                    int n1, n2, n3, n4, n5, n6, n7;
                    try
                    {
                        n1 = Int32.Parse(v1);
                    }
                    catch (System.FormatException fe)
                    {
                        n1 = 0;
                    }
                    try
                    {
                        n2 = Int32.Parse(v2);
                    }
                    catch (System.FormatException fe)
                    {
                        n2 = 0;
                    }
                    try
                    {
                        n3 = Int32.Parse(v3);
                    }
                    catch (System.FormatException fe)
                    {
                        n3 = 0;
                    }
                    try
                    {
                        n4 = Int32.Parse(v4);
                    }
                    catch (System.FormatException fe)
                    {
                        n4 = 0;
                    }
                    try
                    {
                        n5 = Int32.Parse(v5);
                    }
                    catch (System.FormatException fe)
                    {
                        n5 = 0;
                    }
                    try
                    {
                        n6 = Int32.Parse(v6);
                    }
                    catch (System.FormatException fe)
                    {
                        n6 = 0;
                    }
                    try
                    {
                        n7 = Int32.Parse(v7);
                    }
                    catch (System.FormatException fe)
                    {
                        n7 = 0;
                    }
                    iZgys_Hc_Tgzl += n1;
                    iZgys_Hc_Xy += n2;
                    iZgys_Hc_Gngx += n3;
                    iZgys_Hc_Clwx += n4;
                    iZgys_Hc_Jcbq += n5;
                    iZgys_Hc_Jtbq += n6;
                    iZgys_Hc_Bspy += n7;
                }
            }

            sql = "select taoguanzhiliangwenti,xiayu,gongnongguanxi,cheliangweixiu,jingchangbanqian,dengdaijingtaibanqian,beishuipeiye from Report_zugongyinsu where qukuai='临汾' and lururiqi between '" + begin + "' and '" + end + "'";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string v1 = dt.Rows[i][0].ToString();
                    string v2 = dt.Rows[i][1].ToString();
                    string v3 = dt.Rows[i][2].ToString();
                    string v4 = dt.Rows[i][3].ToString();
                    string v5 = dt.Rows[i][4].ToString();
                    string v6 = dt.Rows[i][5].ToString();
                    string v7 = dt.Rows[i][6].ToString();
                    int n1, n2, n3, n4, n5, n6, n7;
                    try
                    {
                        n1 = Int32.Parse(v1);
                    }
                    catch (System.FormatException fe)
                    {
                        n1 = 0;
                    }
                    try
                    {
                        n2 = Int32.Parse(v2);
                    }
                    catch (System.FormatException fe)
                    {
                        n2 = 0;
                    }
                    try
                    {
                        n3 = Int32.Parse(v3);
                    }
                    catch (System.FormatException fe)
                    {
                        n3 = 0;
                    }
                    try
                    {
                        n4 = Int32.Parse(v4);
                    }
                    catch (System.FormatException fe)
                    {
                        n4 = 0;
                    }
                    try
                    {
                        n5 = Int32.Parse(v5);
                    }
                    catch (System.FormatException fe)
                    {
                        n5 = 0;
                    }
                    try
                    {
                        n6 = Int32.Parse(v6);
                    }
                    catch (System.FormatException fe)
                    {
                        n6 = 0;
                    }
                    try
                    {
                        n7 = Int32.Parse(v7);
                    }
                    catch (System.FormatException fe)
                    {
                        n7 = 0;
                    }
                    iZgys_Lf_Tgzl += n1;
                    iZgys_Lf_Xy += n2;
                    iZgys_Lf_Gngx += n3;
                    iZgys_Lf_Clwx += n4;
                    iZgys_Lf_Jcbq += n5;
                    iZgys_Lf_Jtbq += n6;
                    iZgys_Lf_Bspy += n7;
                }
            }

            sql = "select taoguanzhiliangwenti,xiayu,gongnongguanxi,cheliangweixiu,jingchangbanqian,dengdaijingtaibanqian,beishuipeiye from Report_zugongyinsu where qukuai='韩城' and lururiqi between '" + begin + "' and '" + end + "'";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string v1 = dt.Rows[i][0].ToString();
                    string v2 = dt.Rows[i][1].ToString();
                    string v3 = dt.Rows[i][2].ToString();
                    string v4 = dt.Rows[i][3].ToString();
                    string v5 = dt.Rows[i][4].ToString();
                    string v6 = dt.Rows[i][5].ToString();
                    string v7 = dt.Rows[i][6].ToString();
                    int n1, n2, n3, n4, n5, n6, n7;
                    try
                    {
                        n1 = Int32.Parse(v1);
                    }
                    catch (System.FormatException fe)
                    {
                        n1 = 0;
                    }
                    try
                    {
                        n2 = Int32.Parse(v2);
                    }
                    catch (System.FormatException fe)
                    {
                        n2 = 0;
                    }
                    try
                    {
                        n3 = Int32.Parse(v3);
                    }
                    catch (System.FormatException fe)
                    {
                        n3 = 0;
                    }
                    try
                    {
                        n4 = Int32.Parse(v4);
                    }
                    catch (System.FormatException fe)
                    {
                        n4 = 0;
                    }
                    try
                    {
                        n5 = Int32.Parse(v5);
                    }
                    catch (System.FormatException fe)
                    {
                        n5 = 0;
                    }
                    try
                    {
                        n6 = Int32.Parse(v6);
                    }
                    catch (System.FormatException fe)
                    {
                        n6 = 0;
                    }
                    try
                    {
                        n7 = Int32.Parse(v7);
                    }
                    catch (System.FormatException fe)
                    {
                        n7 = 0;
                    }
                    iZgys_Xz_Tgzl += n1;
                    iZgys_Xz_Xy += n2;
                    iZgys_Xz_Gngx += n3;
                    iZgys_Xz_Clwx += n4;
                    iZgys_Xz_Jcbq += n5;
                    iZgys_Xz_Jtbq += n6;
                    iZgys_Xz_Bspy += n7;
                }
            }


            int iRjcl_Hc_100 = 0;
            int iRjcl_Hc_90 = 0;
            int iRjcl_Hc_80 = 0;
            int iRjcl_Hc_70 = 0;
            int iRjcl_Lf_100 = 0;
            int iRjcl_Lf_90 = 0;
            int iRjcl_Lf_80 = 0;
            int iRjcl_Lf_70 = 0;
            int iRjcl_Xz_100 = 0;
            int iRjcl_Xz_90 = 0;
            int iRjcl_Xz_80 = 0;
            int iRjcl_Xz_70 = 0;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='韩城' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi >= '100%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Hc_100 = dt.Rows.Count;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='韩城' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi < '100%' and wanchengbaifenbi >= '90%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Hc_90 = dt.Rows.Count;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='韩城' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi < '90%' and wanchengbaifenbi >= '80%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Hc_80 = dt.Rows.Count;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='韩城' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi < '80%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Hc_70 = dt.Rows.Count;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='临汾' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi >= '100%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Lf_100 = dt.Rows.Count;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='临汾' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi < '100%' and wanchengbaifenbi >= '90%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Lf_90 = dt.Rows.Count;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='临汾' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi < '90%' and wanchengbaifenbi >= '80%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Lf_80 = dt.Rows.Count;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='临汾' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi < '80%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Lf_70 = dt.Rows.Count;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='忻州' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi >= '100%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Xz_100 = dt.Rows.Count;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='忻州' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi < '100%' and wanchengbaifenbi >= '90%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Xz_90 = dt.Rows.Count;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='忻州' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi < '90%' and wanchengbaifenbi >= '80%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Xz_80 = dt.Rows.Count;

            sql = "select jinghao from Xls_Yl_Rbb_Ylsg where place='忻州' and shigongriqi between '" + begin + "' and '" + end + "' and wanchengbaifenbi < '80%' group by jinghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iRjcl_Xz_70 = dt.Rows.Count;


            int iSgzl_Hc_Cishu = 0;
            int iSgzl_Lf_Cishu = 0;
            int iSgzl_Xz_Cishu = 0;
            int iSgzl_Hc_Wenti = 0;
            int iSgzl_Lf_Wenti = 0;
            int iSgzl_Xz_Wenti = 0;

            sql = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where place='韩城' and shigongriqi between '" + begin + "' and '" + end + "' group by jinghao,cengwei";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iSgzl_Hc_Cishu = dt.Rows.Count;

            sql = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where place='临汾' and shigongriqi between '" + begin + "' and '" + end + "' group by jinghao,cengwei";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iSgzl_Lf_Cishu = dt.Rows.Count;

            sql = "select jinghao,cengwei from Xls_Yl_Rbb_Ylsg where place='忻州' and shigongriqi between '" + begin + "' and '" + end + "' group by jinghao,cengwei";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iSgzl_Xz_Cishu = dt.Rows.Count;

            sql = "select jinghao,cenghao from Xls_Yl_Rbb_Sbyysm where place='韩城' and yalieriqi between '" + begin + "' and '" + end + "' group by jinghao,cenghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iSgzl_Hc_Wenti = dt.Rows.Count;

            sql = "select jinghao,cenghao from Xls_Yl_Rbb_Sbyysm where place='临汾' and yalieriqi between '" + begin + "' and '" + end + "' group by jinghao,cenghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iSgzl_Lf_Wenti = dt.Rows.Count;

            sql = "select jinghao,cenghao from Xls_Yl_Rbb_Sbyysm where place='忻州' and yalieriqi between '" + begin + "' and '" + end + "' group by jinghao,cenghao";
            dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0) iSgzl_Xz_Wenti = dt.Rows.Count;


            string str = "";
            str += iKzys_Hc_Yikai + ",";
            str += iKzys_Lf_Yikai + ",";
            str += iKzys_Xz_Yikai + ",";
            str += iKzys_Hc_Yikai_Shibai + ",";
            str += iKzys_Lf_Yikai_Shibai + ",";
            str += iKzys_Xz_Yikai_Shibai + ",";
            str += iKzys_Hc_Erkai + ",";
            str += iKzys_Lf_Erkai + ",";
            str += iKzys_Xz_Erkai + ",";
            str += iKzys_Hc_Erkai_Shibai + ",";
            str += iKzys_Lf_Erkai_Shibai + ",";
            str += iKzys_Xz_Erkai_Shibai + ";";

            str += iJszl_Hc + ",";
            str += iJszl_Lf + ",";
            str += iJszl_Xz + ";";

            str += iXtg_Hc + ",";
            str += iXtg_Lf + ",";
            str += iXtg_Xz + ",";
            str += iXtg_Hc_Shibai + ",";
            str += iXtg_Lf_Shibai + ",";
            str += iXtg_Xz_Shibai + ";";

            str += iGj_Hc + ",";
            str += iGj_Lf + ",";
            str += iGj_Xz + ",";
            str += iGj_Hc_Shibai + ",";
            str += iGj_Lf_Shibai + ",";
            str += iGj_Xz_Shibai + ";";

            str += iJl_Hc + ",";
            str += iJl_Lf + ",";
            str += iJl_Xz + ",";
            str += iJl_Hc_cg + ",";
            str += iJl_Lf_cg + ",";
            str += iJl_Xz_cg + ";";

            str += iYs_Hc + ",";
            str += iYs_Lf + ",";
            str += iYs_Xz + ",";
            str += iYs_Hc_cg + ",";
            str += iYs_Lf_cg + ",";
            str += iYs_Xz_cg + ";";

            str += iZgys_Hc_Tgzl + ",";
            str += iZgys_Hc_Xy + ",";
            str += iZgys_Hc_Gngx + ",";
            str += iZgys_Hc_Clwx + ",";
            str += iZgys_Hc_Jcbq + ",";
            str += iZgys_Hc_Jtbq + ",";
            str += iZgys_Hc_Bspy + ",";
            str += iZgys_Lf_Tgzl + ",";
            str += iZgys_Lf_Xy + ",";
            str += iZgys_Lf_Gngx + ",";
            str += iZgys_Lf_Clwx + ",";
            str += iZgys_Lf_Jcbq + ",";
            str += iZgys_Lf_Jtbq + ",";
            str += iZgys_Lf_Bspy + ",";
            str += iZgys_Xz_Tgzl + ",";
            str += iZgys_Xz_Xy + ",";
            str += iZgys_Xz_Gngx + ",";
            str += iZgys_Xz_Clwx + ",";
            str += iZgys_Xz_Jcbq + ",";
            str += iZgys_Xz_Jtbq + ",";
            str += iZgys_Xz_Bspy + ";";

            str += iRjcl_Hc_100 + ",";
            str += iRjcl_Hc_90 + ",";
            str += iRjcl_Hc_80 + ",";
            str += iRjcl_Hc_70 + ",";
            str += iRjcl_Lf_100 + ",";
            str += iRjcl_Lf_90 + ",";
            str += iRjcl_Lf_80 + ",";
            str += iRjcl_Lf_70 + ",";
            str += iRjcl_Xz_100 + ",";
            str += iRjcl_Xz_90 + ",";
            str += iRjcl_Xz_80 + ",";
            str += iRjcl_Xz_70 + ";";

            str += iSgzl_Hc_Cishu + ",";
            str += iSgzl_Lf_Cishu + ",";
            str += iSgzl_Xz_Cishu + ",";
            str += iSgzl_Hc_Wenti + ",";
            str += iSgzl_Lf_Wenti + ",";
            str += iSgzl_Xz_Wenti + ";";

            return str;
        }
    }
}
