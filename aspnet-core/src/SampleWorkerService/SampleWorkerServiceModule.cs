using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpCompanyName.AbpProjectName;
using AbpCompanyName.AbpProjectName.Configuration;
using AbpCompanyName.AbpProjectName.EntityFrameworkCore;
using Castle.MicroKernel.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace SampleWorkerService
{
    [DependsOn(        
        typeof(AbpProjectNameCoreModule),
        typeof(AbpProjectNameEntityFrameworkModule),
        typeof(AbpProjectNameApplicationModule)        
       )]
    public class SampleWorkerServiceModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;
        public SampleWorkerServiceModule()
        {
            _appConfiguration = AppConfigurations.Get(Assembly.GetExecutingAssembly().GetDirectoryPathOrNull());
        }

        public override void PreInitialize()
        {

            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
               "Default"
               );
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            var builder = new DbContextOptionsBuilder<AbpProjectNameDbContext>();

            AbpProjectNameDbContextConfigurer.Configure(builder, Configuration.DefaultNameOrConnectionString);

            IocManager.IocContainer.Register(
               Castle.MicroKernel.Registration.Component
                   .For<DbContextOptions<AbpProjectNameDbContext>>()
                   .Instance(builder.Options)
                   .LifestyleTransient()    
           );

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SampleWorkerServiceModule).GetAssembly());
        }
    }
}
