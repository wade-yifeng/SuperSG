using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using Sleemon.Core;
using Sleemon.Portal.Common;
using Sleemon.Portal.Core;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Sleemon.Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly ImplementServiceClient serviceClient;

        public AccountController([Dependency] ImplementServiceClient serviceClient)
            : base()
        {
            this.serviceClient = serviceClient;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new Models.User { RedirectUrl = returnUrl };
            // 使用Owin传入的ReturnUrl
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Login(Models.User user)
        {
            var userModel = this.serviceClient.Request<IUserService, Data.User>((service) => service.GetUserById(user.UserUniqueId));

            var userIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userModel.Name),
                new Claim(ClaimTypes.NameIdentifier, userModel.UserUniqueId),
                new Claim(ClaimsIdentityExtensions.AvatarClaim, userModel.Avatar)
            }, DefaultAuthenticationTypes.ApplicationCookie);

            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = user.IsAutoLogin }, userIdentity);

            Response.Redirect(user.RedirectUrl.ToString());
        }
    }
}