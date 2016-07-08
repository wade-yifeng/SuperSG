using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Sleemon.Portal.Core;
using Sleemon.Portal.Common;

namespace Sleemon.Portal.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    [SiteAuthorize]
    public class BaseController : Controller
    {
        [Dependency]
        protected ImplementServiceClient ServiceClient { get; set; }

        protected readonly string UserUniqueId;

        public BaseController()
        {
            if (User != null && User.Identity != null)
            {
                this.UserUniqueId = User.Identity.AsClaimsIdentity().GetUserUniqueId();
            }
        }
    }
}