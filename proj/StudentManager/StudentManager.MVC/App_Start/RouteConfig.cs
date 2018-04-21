#region Using

using System.Web.Mvc;
using System.Web.Routing;
using Restaurant.MVC.App_Helpers;

#endregion

namespace Restaurant.MVC
{
  public static class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
      routes.LowercaseUrls = true;
      routes.MapRoute("Default", "{controller}/{action}/{id}", new
      {
        controller = "Home",
        action = "Index",
        id = UrlParameter.Optional
      }).RouteHandler = new DashRouteHandler();
    }
  }
}