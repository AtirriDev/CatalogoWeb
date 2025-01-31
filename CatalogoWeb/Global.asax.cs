using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace CatalogoWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptResourceDefinition jqueryDefinition = new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-1.10.2.min.js", // Ajusta la ruta a tu archivo jQuery
                DebugPath = "~/Scripts/jquery-1.10.2.js", // Ajusta la ruta a tu archivo jQuery de depuración
                CdnPath = "https://code.jquery.com/jquery-1.10.2.min.js", // Opcional: CDN de jQuery
                CdnDebugPath = "https://code.jquery.com/jquery-1.10.2.js" // Opcional: CDN de jQuery de depuración
            };

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", jqueryDefinition);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Establece la cultura para toda la aplicación
            CultureInfo culture = new CultureInfo("es-AR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}