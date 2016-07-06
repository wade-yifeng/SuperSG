using System;
using System.Web;
using System.Web.Mvc;

namespace Sleemon.Portal.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class SiteAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            var userClaims = httpContext.User.Identity;
            if (!userClaims.IsAuthenticated)
            {
                return false;
            }

            return true;

            // TODO: 持久化权限数据，判断Action权限
            var routeData = httpContext.Request.RequestContext.RouteData;
            var controller = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");
        }
    }
}