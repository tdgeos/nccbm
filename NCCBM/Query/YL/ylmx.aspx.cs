using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NCCBM.Query.yl
{
    public partial class ylmx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = Request.QueryString["JH"];
            string[] ss = str.Split(',');
            lblCD.Text = ss[2] + "," + ss[3] + "," + ss[4] + "," + ss[5] + "," + ss[6] + "," + ss[7];
            setData(str);
        }

        void setData(string str)
        {
            string jh = str.Split(',')[0];
            string cw = str.Split(',')[1];
            cw = cw.Replace('_', '+');
            cw = cw.Replace('^', '#');
            setSk(jh, cw);
            setYlsg(jh, cw);
            setXb(jh, cw);
            setSb(jh, cw);
        }

        void setSk(string jh, string cw)
        {
            string sql = "select * from Xls_Yl_Rbb_Sk where jinghao='" + jh + "' and cengwei='" + cw + "'";
            DataTable dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                TB_sk1.Text = dt.Rows[0][1].ToString();
                TB_sk2.Text = dt.Rows[0][2].ToString();
                TB_sk3.Text = ((DateTime)dt.Rows[0][3]).ToString("yyyy-MM-dd");
                TB_sk4.Text = dt.Rows[0][4].ToString();
                TB_sk5.Text = dt.Rows[0][5].ToString();
                TB_sk6.Text = dt.Rows[0][6].ToString();
                TB_sk7.Text = dt.Rows[0][7].ToString();
                TB_sk8.Text = dt.Rows[0][8].ToString();
                TB_sk9.Text = dt.Rows[0][9].ToString();
                TB_sk10.Text = dt.Rows[0][10].ToString();
                TB_sk11.Text = dt.Rows[0][11].ToString();
                TB_sk12.Text = dt.Rows[0][12].ToString();
            }
        }

        void setYlsg(string jh, string cw)
        {
            string sql = "select * from Xls_Yl_Rbb_Ylsg where jinghao='" + jh + "' and cengwei='" + cw + "'";
            DataTable dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                TB_1.Text = dt.Rows[0][1].ToString();
                TB_2.Text = dt.Rows[0][2].ToString();
                TB_3.Text = ((DateTime)dt.Rows[0][3]).ToString("yyyy-MM-dd");
                TB_4.Text = dt.Rows[0][4].ToString();
                TB_5.Text = dt.Rows[0][5].ToString();
                TB_6.Text = dt.Rows[0][6].ToString();
                TB_7.Text = dt.Rows[0][7].ToString();
                TB_8.Text = dt.Rows[0][8].ToString();
                TB_9.Text = dt.Rows[0][9].ToString();
                TB_10.Text = dt.Rows[0][10].ToString();
                TB_11.Text = dt.Rows[0][11].ToString();
                TB_12.Text = dt.Rows[0][12].ToString();
                TB_13.Text = dt.Rows[0][13].ToString();
                TB_14.Text = dt.Rows[0][14].ToString();
                TB_15.Text = dt.Rows[0][15].ToString();
                TB_16.Text = dt.Rows[0][16].ToString();
                TB_17.Text = dt.Rows[0][17].ToString();
                TB_18.Text = dt.Rows[0][18].ToString();
                TB_19.Text = dt.Rows[0][19].ToString();
                TB_20.Text = dt.Rows[0][20].ToString();
                TB_21.Text = dt.Rows[0][21].ToString();
                TB_22.Text = dt.Rows[0][22].ToString();
                TB_23.Text = dt.Rows[0][23].ToString();
                TB_24.Text = dt.Rows[0][24].ToString();
                TB_25.Text = dt.Rows[0][25].ToString();
                TB_26.Text = dt.Rows[0][26].ToString();
                TB_27.Text = dt.Rows[0][27].ToString();
                TB_28.Text = dt.Rows[0][28].ToString();
                TB_29.Text = dt.Rows[0][29].ToString();
                TB_30.Text = dt.Rows[0][30].ToString();
                TB_31.Text = dt.Rows[0][31].ToString();
                TB_32.Text = dt.Rows[0][32].ToString();
                TB_33.Text = dt.Rows[0][33].ToString();
                TB_34.Text = dt.Rows[0][34].ToString();
                TB_35.Text = dt.Rows[0][35].ToString();
                TB_36.Text = dt.Rows[0][36].ToString();
                TB_37.Text = dt.Rows[0][37].ToString();
                TB_38.Text = dt.Rows[0][38].ToString();
                TB_39.Text = dt.Rows[0][39].ToString();
                TB_40.Text = dt.Rows[0][40].ToString();
                TB_41.Text = dt.Rows[0][41].ToString();
                TB_42.Text = dt.Rows[0][42].ToString();
                TB_43.Text = dt.Rows[0][43].ToString();
            }
        }

        void setXb(string jh, string cw)
        {
            string sql = "select * from Xls_Yl_Rbb_Yqjc where jinghao='" + jh + "'";
            DataTable dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                TB_yljc1.Text = dt.Rows[0][1].ToString();
                TB_yljc2.Text = dt.Rows[0][2].ToString();
                TB_yljc3.Text = dt.Rows[0][3].ToString();
                TB_yljc4.Text = dt.Rows[0][4].ToString();
                TB_yljc5.Text = dt.Rows[0][5].ToString();
                TB_yljc6.Text = dt.Rows[0][6].ToString();
                TB_yljc7.Text = dt.Rows[0][7].ToString();
                TB_yljc8.Text = dt.Rows[0][8].ToString();
                TB_yljc9.Text = dt.Rows[0][9].ToString();
                TB_yljc10.Text = dt.Rows[0][10].ToString();
                TB_yljc11.Text = dt.Rows[0][11].ToString();
                TB_yljc12.Text = dt.Rows[0][12].ToString();
                TB_yljc13.Text = dt.Rows[0][13].ToString();
                TB_yljc14.Text = dt.Rows[0][14].ToString();
                TB_yljc15.Text = dt.Rows[0][15].ToString();
                TB_yljc16.Text = dt.Rows[0][16].ToString();
                TB_yljc17.Text = dt.Rows[0][17].ToString();
                //TB_yljc18.Text = ((DateTime)dt.Rows[0][18]).ToString("yyyy-MM-dd");
                TB_yljc18.Text = dt.Rows[0][18].ToString();
            }
        }

        void setSb(string jh, string cw)
        {
            string sql = "select * from Xls_Yl_Rbb_Sbyysm where jinghao='" + jh + "' and cenghao='" + cw + "'";
            DataTable dt = DataBaseHelper.query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                TB_sb1.Text = dt.Rows[0][1].ToString();
                TB_sb2.Text = dt.Rows[0][2].ToString();
                TB_sb3.Text = ((DateTime)dt.Rows[0][3]).ToString("yyyy-MM-dd");
                TB_sb4.Text = dt.Rows[0][4].ToString();
                TB_sb5.Text = dt.Rows[0][5].ToString();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string s = lblCD.Text;
            string cd = s.Split(',')[0];
            if(cd == "0") Response.Redirect("ylzdycx.aspx?CD=" + s);
            else Response.Redirect("ylcx.aspx?CD=" + s);
        }
    }
}