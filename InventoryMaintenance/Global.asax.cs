using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;


namespace InventoryMaintenance
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageLoggingHandler());
        }
    }
}
