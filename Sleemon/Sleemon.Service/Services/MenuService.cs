namespace Sleemon.Service
{
    using System;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using Sleemon.Core;
    using Sleemon.Data;
    using Sleemon.Common;
    using System.Text;

    public class MenuService : IMenuService
    {
        private readonly ISleemonEntities _invoicingEntities;
        private readonly IRoleService roleService;
        private readonly IUserService userService;
        public MenuService([Dependency]IRoleService roleService, [Dependency]IUserService userService)
        {
            this._invoicingEntities = new SleemonEntities();
            this.roleService = roleService;
            this.userService = userService;
        }

   
        public IList<Permission> GetAllPermission()
        {
            var list = from p in this._invoicingEntities.Permission
                       where  p.IsActive == true && p.IsMenu == true
                       orderby  p.Sort
                       select p;

            return list.ToList();
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 根据Id获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isMenu">是否是菜单</param>
        /// <returns></returns>
        public Permission GetPermissionById(int id, bool isMenu)
        {

            var list = from p in this._invoicingEntities.Permission
                       where p.Id == id && p.IsActive == true && p.IsMenu == isMenu
                       orderby p.Sort
                       select p;

            return list.ToList()[0];
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 获取指定用户权限列表
        /// </summary>
        /// <param name="userunqiueid"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public IList<Permission> GetUserPermission(string userunqiueid)
        {
            IList<Permission> permissionList = new List<Permission>();
            UserViewModel user = userService.GetUserInfoById(userunqiueid);
            if (user != null && user.IsSuperAdmin)
            {
                permissionList = GetAllPermission();
                return permissionList;
            }

            IList<UserRole> userRoleList = roleService.GetRoleListByUserUniqueId(userunqiueid);
            if (userRoleList == null || userRoleList.Count <= 0)
            {
                return permissionList;
            }
            for (int i = 0; i < userRoleList.Count; i++)
            {
                IList<RolePermission> rolePermissionList = GetRolePermissionByRoleid(userRoleList[i].RoleId);
                if (rolePermissionList == null || rolePermissionList.Count <= 0)
                {
                    return permissionList;
                }
                for (int j = 0; j < rolePermissionList.Count; j++)
                {
                    Permission permission = GetPermissionById(rolePermissionList[j].PermissionId, true);
                    permissionList.Add(permission);
                }
            }

            return permissionList;
        }
        /// <summary>
        /// 获取指定权限的子权限
        /// </summary>
        /// <param name="Parentid"></param>
        /// <returns></returns>
        public IList<Permission> GetPermissionByUserIdAndParentid(string useruniqueid, int parentid)
        {
            IList<Permission> permissionList = GetUserPermission(useruniqueid);
            if (permissionList == null)
            {
                return permissionList;
            }
            var finalList = from s in permissionList
                            where s.ParentId == parentid
                            select s;
            return finalList.ToList();
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public IList<RolePermission> GetRolePermissionByRoleid(int roleid)
        {

            var list = from p in this._invoicingEntities.RolePermission
                       where p.RoleId == roleid && p.IsActive == true 
                       select p;

            return list.ToList();
        }
     

        //递归拼接菜单

        public string BuildMenuHtml(IList<Permission> permissionList, string li_id, string useruniqueid)
        {
            
            StringBuilder sbMenuHtml = new StringBuilder();
            bool hasSubMenu = false;
            bool first=false;
            if (permissionList == null || permissionList.Count <= 0)
            {
                return sbMenuHtml.ToString();
            }
            for (int i = 0; i < permissionList.Count; i++)
            {
                if (permissionList[i].ParentId == 0)
                {
                    first = true;
                }
                IList<Permission> subPermissionList = GetPermissionByUserIdAndParentid(useruniqueid, permissionList[i].Id);
                if (subPermissionList == null || subPermissionList.Count <= 0)
                {
                    hasSubMenu = false;
                }
                else
                {
                    hasSubMenu = true;
                }
                if (hasSubMenu)//父级菜单
                {
                    string menuName = permissionList[i].Name;
                    string iconClass = permissionList[i].IconClass;
                    string subli_id = li_id + "-" + (i+1);
                    //to do 拼接父级菜单html
                    sbMenuHtml.Append(" <li id='" + subli_id + "'>");
                    sbMenuHtml.AppendFormat(@"
                         <a href='#' class='dropdown-toggle'>
                         <i class='{0}'></i><span class='menu-text'>{1}</span>
                         <b class='arrow icon-angle-down'></b>
                         </a>
                        ", iconClass, menuName);
                    sbMenuHtml.AppendFormat(" <ul class='submenu'>");
                    sbMenuHtml.Append(BuildMenuHtml(subPermissionList, subli_id, useruniqueid));

                    sbMenuHtml.Append(" </ul>");
                    sbMenuHtml.Append("</li>");

                }
                else//最低级菜单
                {
                    string menuName = permissionList[i].Name;
                    string iconClass = permissionList[i].IconClass;
                    string url = permissionList[i].Url;

                    string subli_id = li_id + "-" + (i + 1);
                    if (first)//第一次就是最低级菜单
                    {

                        sbMenuHtml.AppendFormat("<li id='{0}' action='{1}'> ", subli_id, url);
                        sbMenuHtml.AppendFormat(@"
                         <a href='#' class='dropdown-toggle'>
                         <i class='{0}'></i><span class='menu-text'>{1}</span>
                         </a>
                        ", iconClass, menuName);
                        sbMenuHtml.Append("</li>");
                    }
                    else
                    {
                    //to do 拼接最低级菜单html
                  
                    sbMenuHtml.AppendFormat(@"  <li id='{0}' action='{1}'><a href='#'>{2}</a></li> 
                    ", subli_id, url, menuName);
                   
                    }
                }

            }


            return sbMenuHtml.ToString();
        }
    }
}
