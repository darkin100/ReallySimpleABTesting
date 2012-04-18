using System.Configuration;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Website.Models;

namespace Website.App.Ioc
{
    public class WebInstaller : IWindsorInstaller
    {
        

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.AddHandlerSelector(new AdvertHandlerSelector(() => HttpContext.Current,ConfigurationManager.AppSettings["testname"]));

            container.Register(
                Component.For<IPrincipal>().LifeStyle.PerWebRequest.UsingFactoryMethod(() => HttpContext.Current.User)
                );

            container.Register(
                Component.For<HttpRequest>().LifeStyle.PerWebRequest.UsingFactoryMethod(() => HttpContext.Current.Request)
                );

            container.Register(
                Component.For<HttpResponse>().LifeStyle.PerWebRequest.UsingFactoryMethod(() => HttpContext.Current.Response)
                );

            container.Register(
                Component
                    .For<IControllerFactory>()
                    .Instance(new WindsorControllerFactory(container.Kernel))
                    .LifeStyle.Singleton);

            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient.Named(c.Name)));

            container.Register(
                Component.For<IAdvert>().ImplementedBy<Advert>()
                .DependsOn(
                    Dependency.OnValue("message", "If you are not measuring, you are not doing marketing!")
                )
            );

            container.Register(
                    Component.For<IAdvert>().ImplementedBy<Advert>()
                    .DependsOn(
                        Dependency.OnValue("message", "Press the call to action!!")
                    ).Named(".test")
                );
        }
    }
}