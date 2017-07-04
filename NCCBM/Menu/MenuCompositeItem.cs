using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCCBM.Menu
{
    [Serializable()] 
    public class MenuCompositeItem
    {
         
        private List<MenuCompositeItem> _children = new List<MenuCompositeItem>();
        private string _id;
        private string _text;
        private string _link;        
        private string _target;
        private string _level;
        /// <summary> 
        /// 菜单项 
        /// </summary> 
        /// <param name="text">菜单名</param> 
        /// <param name="link">链接</param> 
        public MenuCompositeItem(string id, string text, string link, string level) 
        {
            this._id = id;
            this._text = text; 
            this._link = link;
            this._level = level;
        } 
        /// <summary> 
        /// 菜单项 
        /// </summary> 
        /// <param name="text">菜单名</param> 
        /// <param name="link">链接</param> 
        /// <param name="target">跳转目标</param> 
        public MenuCompositeItem(string id, string text, string link, string level, string target) 
        {
            this._id = id;
            this._text = text; 
            this._link = link;
            this._level = level;
            this._target = target; 
        }

        /// <summary> 
        /// 设置或获取菜单名 
        /// </summary> 
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        } 
        /// <summary> 
        /// 设置或获取菜单名 
        /// </summary> 
        public string Text 
        { 
        get { return _text; } 
        set { _text = value; } 
        } 
        /// <summary> 
        /// 设置或获取链接 
        /// </summary> 
        public string Link 
        { 
        get { return _link; } 
        set { _link = value; } 
        } 
        /// <summary> 
        /// 跳转目标 
        /// </summary> 
        public string Target 
        { 
        get { return _target; } 
        set { _target=value; } 
        }
        /// <summary>
        /// 菜单级别
        /// </summary>
        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }
        /// <summary> 
        /// 设置或获取子菜单 
        /// </summary> 
        public List<MenuCompositeItem> Children 
        { 
        get { return _children; } 
        set { _children = value; } 
        } 

    }
}