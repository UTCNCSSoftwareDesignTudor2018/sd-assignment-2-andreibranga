using DevExpress.Web.Mvc;
using Restaurant.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Restaurant.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DevExtremeBundleConfig.RegisterBundles(BundleTable.Bundles);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();
        }
        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            DevExpressHelper.Theme = "Material";
            DevExpressHelper.GlobalThemeBaseColor = "#4796CE";
            //DevExpress.Web.Mvc.DevExpressHelper.GlobalThemeFont = "15px 'Calibri'";
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Console.Write("Session Started");
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Console.Write("Session Ended");
        }
    }
}
