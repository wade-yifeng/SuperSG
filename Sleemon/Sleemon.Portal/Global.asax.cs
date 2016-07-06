using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Sleemon.Portal.Common;
using Sleemon.Portal.Core;
using Sleemon.Portal.Factories;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sleemon.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private UnityContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            this.ConfigureIocContainer(GlobalConfiguration.Configuration);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = new HttpContextWrapper(((MvcApplication)sender).Context);
            var ex = Server.GetLastError();

            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.ContentType = "text/html";
            httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            httpContext.Response.TrySkipIisCustomErrors = true;

            var errorInfo = new HandleErrorInfo(ex, HttpContextUtility.CurrentController, HttpContextUtility.CurrentAction);
            
            // TODO: Log Error
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Index";
            routeData.Values["errorInfo"] = errorInfo;
            
            // TODO: Redirect to error action
            // ((IController)new ErrorController()).Execute(new RequestContext(httpContext, routeData));
        }

        private void ConfigureIocContainer(HttpConfiguration config)
        {
            this.container = new UnityContainer();

            this.container.RegisterInstance(new ServiceFactory(this.container), new ContainerControlledLifetimeManager());
            
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            this.container.LoadConfiguration(section, "UnityContainer");
            this.container.RegisterInstance(this.container.Resolve<ImplementServiceClient>(), new ContainerControlledLifetimeManager());

            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(this.container));

            config.DependencyResolver = new UnityDependencyResolver(this.container);
        }
    }
}
