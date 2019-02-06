using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

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
