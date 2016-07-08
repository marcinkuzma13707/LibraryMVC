using Autofac;
using Autofac.Integration.Mvc;
using LibraryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebModelService;
using WebModelService.BookModel;
using WebModelService.Interfaces;

namespace LibraryMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
             var builder = new ContainerBuilder();
             builder.RegisterType<UserService>()
             .As<IUserService>()
            .InstancePerDependency();
            builder.RegisterType<Library>()
            .As<Library>();
            builder.RegisterType<BookService>()
             .As<IBookService>()
            .InstancePerDependency();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            //builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            //builder.RegisterFilterProvider();
            
            // Set the dependency resolver to be Autofac.


           

            var container = builder.Build();
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
    }
}
