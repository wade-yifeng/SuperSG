namespace Sleemon.Core
{
    using System.Collections.Generic;

    using Sleemon.Data;

    public interface IPermissionService
    {
        IList<Permission> GetPermissionByParentId(int parentid);
        List<Permission> GetAllPermissionByParentId(int parentid);
    }
}