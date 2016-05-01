using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FlightGates.DataStore;
using FlightGates.Services;

namespace FlightGates
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = BuildContainer();

            // Create the depenedency resolver.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<GateService>().As<IGateService>().InstancePerRequest();
            builder.RegisterType<GatesRepository>().As<IGatesRepository>().InstancePerRequest();
            builder.RegisterType<FlightsRepository>().As<IFlightsRepository>().InstancePerRequest();
            builder.RegisterType<DataContext>().As<IDataContext>().SingleInstance();

            return builder.Build();
        }

    }
}
