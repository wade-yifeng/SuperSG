using System.Web.Mvc;

namespace Sleemon.Portal.Controllers
{
    /// <summary>
    /// 主页加载后需要进入ng的route
    /// 所以主页不需要权限限制
    /// </summary>
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Navigator()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Sidebar()
        {
            return View();
        }
    }
}