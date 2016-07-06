namespace Sleemon.Core
{
    using System.Collections.Generic;

    using Sleemon.Data;

    public interface IMenuService
    {
        IList<Permission> GetUserPermission(string userunqiueid);
        IList<RolePermission> GetRolePermissionByRoleid(int roleid);
        IList<Permission> GetPermissionByUserIdAndParentid(string userunqiueid, int parentid);
        Permission GetPermissionById(int id,bool isMenu);
        string BuildMenuHtml(IList<Permission> permissionList, string li_id, string userunqiueid);
    }
}