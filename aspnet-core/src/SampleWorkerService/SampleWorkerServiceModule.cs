using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpCompanyName.AbpProjectName;
using AbpCompanyName.AbpProjectName.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleWorkerService
{
    [DependsOn(        
        typeof(AbpProjectNameEntityFrameworkModule),
        typeof(AbpProjectNameApplicationModule)        
       )]
    public class SampleWorkerServiceModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SampleWorkerServiceModule).GetAssembly());
        }
    }
}
