using Practica.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Practica
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Method to configure start configurations
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Get culture info of the system
            CultureInfo culture = CultureInfo.CurrentCulture;
            RegionInfo regionInfo = new RegionInfo(culture.Name);
            string region = regionInfo.TwoLetterISORegionName;
            var currencyDenominations = System.Configuration.ConfigurationManager.AppSettings[region];
            var splitDenominations = currencyDenominations.Split(',');
            if (splitDenominations.Length > 0)
            {
                Currency.CurrencyCode = culture.Name;
                Currency.CurrencyDenomination = new List<double>();
                foreach (var item in splitDenominations)
                {
                    double number= 0;
                    Double.TryParse(item, out number);
                    if (number!=0)
                        Currency.CurrencyDenomination.Add(number);
                }
                Currency.CurrencyDenomination = Currency.CurrencyDenomination.OrderByDescending(x => x).ToList();
            }
            else
            {
                //Default Currency denomination 
                Currency.CurrencyDenomination = new List<double> { 0.10, 0.50, 1, 2, 5, 10, 20, 50, 100, 200, 500 };
            }
        }
        /// <summary>
        /// Method to control the exceptions returned
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var ctx = HttpContext.Current;
            var exception = ctx.Server.GetLastError();
            ctx.Server.ClearError();
            switch (exception.HResult)
            {
                case 1:
                case 10:
                    Context.Response.ContentType = "application/json";
                    Context.Response.Write(exception.Message);
                    break;
                default:
                    HttpException httpException = exception as HttpException;
                    _ = httpException != null ? httpException.GetHttpCode() : 0;
                    if (httpException == null)
                    {
                        Context.Response.ContentType = "application/json";
                        Context.Response.Write(exception.Message);
                    }
                    else
                    {
                        Response.Redirect(string.Format("~/Home/Error?error={0}", exception.Message));
                    }

                    break;
            }

        }
    }
}
