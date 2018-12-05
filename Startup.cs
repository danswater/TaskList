using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using TaskList.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace TaskList
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddDbContext<TaskListDbContext>(option =>
              option.UseMySql(Configuration.GetConnectionString("DefaultDb")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Register Swagger
            services.AddSwaggerGen(option => {
              option.SwaggerDoc("v1", new Info
              {
                Title = "TaskList API",
                Version = "v1",
                Description = "A .NET core(version 2.1.403) exploration, practe and familiarization",
                TermsOfService = "None",
                Contact = new Contact
                {
                  Name = "Francis Lamayo",
                  Email = string.Empty,
                  Url = "www.google.com"
                },
                License = new License
                {
                  Name = "Use under LICX",
                  Url = "https://example.com/license"
                } 
              });

              // Set the comments path for the Swagger JSON and UI
              var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
              var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
              option.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint
                app.UseSwagger();

                // Enable middleware to serve swagger-ui
                app.UseSwaggerUI(option =>
                {
                  option.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                  option.RoutePrefix = string.Empty;
                });

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
