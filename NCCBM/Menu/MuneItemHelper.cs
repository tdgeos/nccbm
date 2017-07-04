using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace NCCBM.Menu
{
    public class MuneItemHelper
    {
        private DataTable menu = null;

        public MuneItemHelper(string user_id)
        {
            string sql = "select * from T_System_APPLICATION";
            menu = DataBaseHelper.query(sql);
        }

        //获取parent的子菜单
        public void getMenuItem(MenuCompositeItem root, bool isAddChild, string parent)
        {
            string ugid = null;
            if (HttpContext.Current.Session["RoleID"] != null)
            {
                ugid = HttpContext.Current.Session["RoleID"].ToString();
            }

            MenuCompositeItem temp = null;
            DataRow[] items = menu.Select(" Parent_id ='" + parent + "'");
            foreach (DataRow dr in items)
            {
                string lv = dr["Authority_level"].ToString();
                string url = dr["NavigateUrl"].ToString();
                string id = dr["T_ID"].ToString();
                string title = dr["Title"].ToString();
                temp = new MenuCompositeItem(id, title, url, lv);
                root.Children.Add(temp);
            }

            int child_level = 0;
            if (isAddChild && root.Children.Count != 0)
            {
                child_level = Int32.Parse(root.Children[0].Level) + 1;
                int len=root.Children.Count;
                for (int i = 0; i < len; i++)
                {
                    getMenuItem(root.Children[i],false, root.Children[i].Id);
                }
            }
        }

        //获取菜单id的级别
        public string getLevel(string id)
        {
            DataRow[] items = menu.Select(" T_ID ='" + id + "'");
            if (items.Length >= 1)
            {
                return items[0]["Authority_Level"].ToString();
            }
            return null;
        }
    }
}