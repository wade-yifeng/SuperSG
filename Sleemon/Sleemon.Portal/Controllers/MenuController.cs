using Sleemon.Core;
using Sleemon.Portal.Common;

namespace Sleemon.Portal.Controllers
{
    using Microsoft.Practices.Unity;
    /**
    *add by wolfgump 20160521
*/
    
    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using Sleemon.Data;
    using Sleemon.Portal.Core;

    public class MenuController : BaseController
    {
        private readonly string li_id = "menu";

        // GET: build menu
        public ActionResult BuildMenu()
        {
            if (this.User == null)
            {
                return Content("");
            }
            var permissionList = 
                ServiceClient.Request<IMenuService, IList<Permission>>(
                    service => service.GetPermissionByUserIdAndParentid(UserUniqueId, 0));

            var menuHtml =
                ServiceClient.Request<IMenuService, string>(
                    service => service.BuildMenuHtml(permissionList, li_id, this.UserUniqueId));
            return Content(menuHtml);
        }
      
    }
}