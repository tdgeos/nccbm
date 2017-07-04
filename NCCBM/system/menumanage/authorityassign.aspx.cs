using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


public partial class authorityassign : System.Web.UI.Page
{
    protected SqlConnection myConn;
    protected SqlDataAdapter myAdapter;
    protected DataSet data;

    protected void Page_Load(object sender, EventArgs e)
    {
         string szRoleID="";
        //判断用户是否登陆,如果没有登陆直接转向登陆页面
        try
        {
            szRoleID = HttpContext.Current.Session["RoleID"].ToString();
        }
        catch (NullReferenceException ne)
        {
            HttpContext.Current.Response.Redirect("~/NoLogin.aspx");
        }
        grdRole.Attributes.Add("BorderColor", "#0088cc");
        grdRole.Attributes.Add("BorderWidth", "1");
    }

    protected void CreateTree(string strRoleID)
    {
        myConn = DBCONN.GetDBConn();
        CreateDataSet("0");
        tvMenu.Dispose();
        tvMenu.Nodes.Clear();
        InitTree(tvMenu.Nodes, "0");

        tvMenu.ShowCheckBoxes = TreeNodeTypes.All;
        InitCheck(strRoleID, tvMenu);

        tvMenu.Attributes.Add("onclick", "javascrirpt:client_OnTreeNodeChecked(event);");
        divName.Visible = true;
        divMenu.Visible = true;
    }

    //判断权限 T_System_APPLICATION T_System_ROLE_APPLICATION
    protected void InitCheck(string strRoleID,TreeView tvMine)
    {
        SqlDataReader dr;
        SqlConnection Myconn = DBCONN.GetDBConn();
        SqlCommand Mycomm = new SqlCommand("SELECT distinct ApplicationID FROM T_System_ROLE_APPLICATION where RoleID='" + strRoleID + "' order by ApplicationID", Myconn);
        Mycomm.Connection.Open();
        dr = Mycomm.ExecuteReader();
        while (dr.Read())
        {
            foreach(TreeNode tn in tvMine.Nodes)
            {
                if (dr["ApplicationID"].ToString() == tn.Value.ToString())
                {
                    tn.Checked = true;
                }
                SetCheck(dr["ApplicationID"].ToString(), tn);
            }
        
        }
        dr.Close();
        Mycomm.Connection.Close();
    }

    protected void SetCheck(string strValue,TreeNode tn)
    {
        foreach (TreeNode tnMine in tn.ChildNodes)
        {
            if (strValue == tnMine.Value.ToString())
            {
                tnMine.Checked = true;
            }
            SetCheck(strValue, tnMine);
        }
    }

    public DataSet CreateDataSet(string szParentID)
    {
        //string szRoleID;
        string szQuery;
        //获取数据连接语句，并创建数据库连接对象
        //szRoleID = HttpContext.Current.Session["RoleID"].ToString();
        try
        {
            //增加权限控制
            szQuery = "SELECT * FROM T_System_APPLICATION  order by T_ID";
            myAdapter = new SqlDataAdapter(szQuery, myConn);
            data = new DataSet();
            myAdapter.Fill(data, "treeMenu");
        }
        catch (Exception e)
        {
            throw e;
        }
        return data;
    }

    //从DataSet中取数据建树
    //从根节点开始递归调用显示子树
    public void InitTree(TreeNodeCollection Nds, string parentId)
    {
        TreeNode NewNode;
        string strImage;
       // string strTarget;
        //data为存储建树数据信息的数据集
        //用父节点进行筛选数据集中信息
        try
        {
            DataRow[] rows = data.Tables[0].Select("parent_Id='" + parentId + "'");

            foreach (DataRow row in rows)
            {
                if (row["Authority_Level"].ToString() == "2" || row["Authority_Level"].ToString() == "1")
                {
                    strImage = "../../css/res/1.gif";
                    NewNode = new
                        TreeNode(row["title"].ToString(),row["T_Id"].ToString(), strImage);
                }
                else
                {
                    strImage = "../../css/res/2.gif";
                    NewNode = new
                        TreeNode(row["title"].ToString(),row["T_Id"].ToString(), strImage);
                }
                Nds.Add(NewNode);

                InitTree(NewNode.ChildNodes, row["T_Id"].ToString());
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }


    protected void RoleSelectChanged(object sender, EventArgs e)
    {
        string strRole_ID = grdRole.SelectedValue.ToString().Trim();
        hidRoleID.Value = strRole_ID;
        lblRoleName.Text = grdRole.Rows[grdRole.SelectedIndex].Cells[1].Text.ToString();
        CreateTree(strRole_ID);
    }


    protected void cmdQuery_Click(object sender, EventArgs e)
    {
        //【功能】：数据查询事件
        string strName;
        string strQuery;
        strName = txtName.Text.Trim();
        strQuery = "SELECT * FROM T_SYSTEM_Role Where Role_Name like'%" + strName + "%' ORDER BY Role_ID";
        sdsRole.SelectCommand = strQuery;
        
        grdRole.DataSourceID = "sdsRole";
        grdRole.DataBind();
    }

    protected void cmdRestore_Click(object sender, EventArgs e)
    {
        //【功能】：返回
        divName.Visible = false;
        divMenu.Visible = false;
    }
    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        //【功能】：更新用户组事件
        string strRoleID;
        string strSQL;
        strRoleID = hidRoleID.Value.ToString();
        //清空用户权限
        strSQL = "Delete T_System_ROLE_APPLICATION Where RoleID ='" + strRoleID + "'";
        SqlConnection conMy = DBCONN.GetDBConn();
        SqlCommand cmdMy = new SqlCommand(strSQL, conMy);
        cmdMy.Connection.Open();
        cmdMy.ExecuteNonQuery();
        cmdMy.Connection.Close();
        //设置用户权限
        foreach (TreeNode tn in tvMenu.Nodes)
        {
            if (tn.Checked)
            {
                strSQL = "Insert Into T_System_ROLE_APPLICATION(RoleID,ApplicationID) Values('" + strRoleID + "','";
                strSQL = strSQL + tn.Value + "')";
                cmdMy = new SqlCommand(strSQL, conMy);
                cmdMy.Connection.Open();
                cmdMy.ExecuteNonQuery();
                cmdMy.Connection.Close();
            }
            CheckUpdateDB(conMy, strRoleID, tn);
        }
        common.alertMessage("提交成功");
        //返回
        cmdRestore_Click(sender, e);
    }

    protected void CheckUpdateDB(SqlConnection conMy,string strRoleID,TreeNode tn)
    {
        string strSQL = "";
        
        foreach (TreeNode tnMine in tn.ChildNodes)
        {
            if (tnMine.Checked)
            {
                strSQL = "Insert Into T_System_ROLE_APPLICATION(RoleID,ApplicationID) Values('" + strRoleID + "','";
                strSQL = strSQL + tnMine.Value + "')";
                SqlCommand cmdMy = new SqlCommand(strSQL, conMy);
                cmdMy.Connection.Open();
                cmdMy.ExecuteNonQuery();
                cmdMy.Connection.Close();
            }
            CheckUpdateDB(conMy, strRoleID, tnMine);
        }
    }
}
