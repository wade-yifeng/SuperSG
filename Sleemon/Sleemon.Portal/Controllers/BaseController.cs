using Microsoft.Practices.Unity;
using Sleemon.Portal.Core;
using System.Web.Mvc;

namespace Sleemon.Portal.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    [SiteAuthorize]
    public class BaseController : Controller
    {
        [Dependency]
        public ImplementServiceClient ServiceClient { get; set; }
    }
}