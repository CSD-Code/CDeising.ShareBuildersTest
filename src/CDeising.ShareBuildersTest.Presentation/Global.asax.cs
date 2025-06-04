using CDeising.ShareBuildersTest.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using CDeising.ShareBuildersTest.Application;

namespace CDeising.ShareBuildersTest.Presentation
{
    public class Global : HttpApplication
    {
        static IServiceCollection serviceCollection;
        static IServiceProvider serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get { return serviceProvider; }
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterScriptBundles(BundleTable.Bundles);
            BundleConfig.RegisterStyleBundles(BundleTable.Bundles);

            serviceCollection = new ServiceCollection();

            // Add items to DI here.
            serviceCollection.AddApplication();
            serviceCollection.AddInfrastructure();

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // TODO: General global exception handling.
        }

    }
}