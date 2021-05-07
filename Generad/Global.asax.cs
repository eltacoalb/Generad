using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Generad
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_AcquireRequestState(Object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            var languageSession = "es";
            if (context != null && context.Session != null)
            {
                languageSession = context.Session["lang"] != null ? context.Session["lang"].ToString() : "es";
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageSession);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(languageSession);
            }
        }
    }
}
