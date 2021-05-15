using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SB.Repository.Database;
using SB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SB.API
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
            services.AddMemoryCache();
            services.AddControllers();
            //services.AddCors();
            services.AddMvcCore().AddApiExplorer();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                                  builder =>
                                  {
                                      builder.WithOrigins("*")
                                                          .AllowAnyHeader()
                                                          .AllowAnyOrigin()
                                                          .AllowAnyMethod();
                                  });
            });

             

            //Database Connections
            services.AddDbContext<DbshopbridgeContext>(options =>
                                 options.UseSqlServer(Configuration.GetConnectionString("SBDatabase")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new OpenApiInfo {
                        Title = "SB.API",
                        Version = "v1",
                        Description = "RESTful API for SB",
                        Contact = new OpenApiContact
                        {
                            Name = "Jitendra Patidar"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Private Only",
                        }

                    });
            });
            services.AddTransient<IInventoryService, InventoryService>();
            services.AddTransient<IUserService, UserService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SB.API v1"));
            }
            // Setup global exception handler middleware
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var exception = context.Features.Get<IExceptionHandlerFeature>();

                    if (exception != null)
                    {
                        //logger.Error($"{exception.Error.Message}\nStackTrace:\n{exception.Error.StackTrace}");

                        var err = new
                        {
                            context.Response.StatusCode,
                            exception.Error.Message,
                            exception.Error.StackTrace,
                        };

                        await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(err)).ConfigureAwait(false);
                    }
                });
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(x => x
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials());
            app.UseResponseCaching();
            app.UseHttpsRedirection();
         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
