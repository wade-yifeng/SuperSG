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

    public class PermissionService : IPermissionService
    {
        private readonly ISleemonEntities _invoicingEntities;
        public PermissionService([Dependency] ISleemonEntities entity)
        {
            this._invoicingEntities = entity;
        }
        /// <summary>
        /// 根据parentid获取子节点
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public IList<Permission> GetPermissionByParentId(int parentid)
        {
            var list = from p in this._invoicingEntities.Permission
                       where p.IsActive == true && p.ParentId == parentid
                       orderby p.Sort
                       select p;
            return list.ToList();
        }
        // <summary>
        // 根据parentid获取子节点 包括子节点的子节点 
        // </summary>
        // <param name="parentid"></param>
        // <returns></returns>
        public List<Permission> GetAllPermissionByParentId(int parentid)
        {
            List<Permission> list = new List<Permission>();
            list = GetPermissionByParentId(parentid).ToList();
            if (list.Count==0) { return list; }
            for (int i = 0; i < list.Count; i++)
            {
                list.AddRange(GetAllPermissionByParentId(list[i].Id).ToList());

            }
            return list;
        }
    }
}
