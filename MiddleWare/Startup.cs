﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MiddleWare
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Information);
            var logger = loggerFactory.CreateLogger("Middleware Demo");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                var timer = Stopwatch.StartNew();
                logger.LogInformation($"==> beginning request in {env.EnvironmentName}");
                await next();
                logger.LogInformation($"==> completed request {timer.ElapsedMilliseconds} ms");
            });

            app.UseStaticFiles();

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
