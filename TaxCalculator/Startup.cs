﻿using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Models;
using TaxCalculator.Services;

namespace TaxCalculator
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddControllersAsServices();

           var serviceProvider = services.AddAutofacProvider(builder =>
            {
                builder.RegisterType<TaxCalculatorService>().As<ITaxCalculatorService>();
                builder.RegisterType<TaxBracketService>().As<ITaxBracketService>();
            });
            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseStatusCodePages();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
           
        }
    }
}
