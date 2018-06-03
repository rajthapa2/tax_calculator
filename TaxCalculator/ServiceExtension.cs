using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Services;

namespace TaxCalculator
{
    public static class ServiceExtension
    {
        public static IServiceProvider AddAutofacProvider(this IServiceCollection services, Action<ContainerBuilder> builderCallback = null)
        {
            var builder = new ContainerBuilder();

            builderCallback?.Invoke(builder);

            builder.Populate(services);

        
            var container = builder.Build();
            var provider = new AutofacServiceProvider(container);

//            container.Resolve<IApplicationLifetime>()
//                .ApplicationStarted.Register(() => container.Dispose());

            return provider;
        }
    }
}
