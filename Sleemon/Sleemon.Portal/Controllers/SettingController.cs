using Microsoft.Practices.Unity;
using Sleemon.Core;
using Sleemon.Portal.Common;
using Sleemon.Portal.Core;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sleemon.Portal.Controllers
{
    public class SettingController : BaseController
    {
        [HttpGet]
        public ActionResult Connect()
        {
            return View();
        }

        [HttpGet]
        public JsonContentAction GetDepartment()
        {
            var departments = base.ServiceClient.Request<IDepartmentService, IList<Data.DepartmentModel>>((service) => service.GetAllActivedDepartment());

            return new JsonContentAction(
                   new { records = departments, rootIds = SiteConfiguration.RootDepartments });
        }

        [HttpGet]
        public JsonContentAction GetUserListByDepartment(int departmentId)
        {
            var userList = base.ServiceClient.Request<IUserService, IList<Data.User>>((service) => service.GetUsersForDepartment(departmentId));

            return new JsonContentAction(userList);
        }
    }
}