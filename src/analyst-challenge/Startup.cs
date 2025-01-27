﻿using Amazon;
using Amazon.SQS;
using analyst_challenge.DAO;
using analyst_challenge.DAO.Impl;
using analyst_challenge.Services;
using analyst_challenge.Services.Impl;
using analyst_challenge.Workers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace analyst_challenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IEventReceiverService, EventReceiverServiceImpl>();
            services.AddSingleton<IEventReceiverDAO, EventReceiverDAOImpl>();

            services.AddSingleton<IAmazonSQS>(new AmazonSQSClient(
                Configuration["AWS_ACCESS_KEY"],
                Configuration["AWS_SECRET_KEY"],
                RegionEndpoint.USEast1
            ));
            services.AddSingleton<IHostedService, EventReceiverPersistWorker>();

            services.AddElasticsearch(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Analyst challenge IOT",
                        Version = "v1"
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Analyst challenge IOT"); });
            app.UseMvc();
        }
    }
}
