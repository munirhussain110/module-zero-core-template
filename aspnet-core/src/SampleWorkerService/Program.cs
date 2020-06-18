using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.AspNetCore.Dependency;
using Abp.Dependency;
using Castle.Windsor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SampleWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    var abpBootstrapper = AbpBootstrapper.Create<SampleWorkerServiceModule>();
                    services.AddSingleton(abpBootstrapper);
                    abpBootstrapper.Initialize();

                    services.AddHostedService<Worker>();
                })
                .UseCastleWindsor(IocManager.Instance.IocContainer);
    }

    //public static IHostBuilder CreateHostBuilder(string[] args) =>
    //    Host.CreateDefaultBuilder(args)
    //        .ConfigureServices((hostContext, services) =>
    //        {
    //            services.AddHostedService<Worker>();
    //        });

    //private static IHostBuilder CreateHostBuilder(string[] args) =>
    //    Host.CreateDefaultBuilder(args)
    //        .ConfigureServices(services =>
    //        {
    //            var abpBootstrapper = AbpBootstrapper.Create<SampleWorkerServiceModule>();
    //            services.AddSingleton(abpBootstrapper);
    //            abpBootstrapper.Initialize();
    //            var container = new WindsorContainer();
    //            //WindsorRegistration.Populate(container, services, app.ApplicationServices);
    //            var sp = container.Resolve<IServiceProvider>();
    //            services.AddHostedService<Worker>();
    //        });
}
