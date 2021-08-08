using System;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationService.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;

namespace AuthenticationService
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
            services.AddControllers();

            string connectionString = Environment.GetEnvironmentVariable("SQLSERVER_Auth");

            if (connectionString == null)
            {
                connectionString = Configuration.GetConnectionString("AuthConnection"); ;
            }

            services.AddDbContext<IAuthenticationContext, AuthenticationContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<Repository.IAuthRepository, Repository.AuthRepository>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenValidator, TokenValidator>();

            services.AddSwaggerGen(swag =>
            {
                swag.SwaggerDoc("v1", 
                    new OpenApiInfo
                     { 
                        Title = "Authentication API", 
                        Version = "v1" ,
                        Description = "This API can be used for creating users and generating JWT token."
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(builder => 
                            builder.AllowAnyHeader()
                                   .AllowAnyMethod()
                                   .AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthenticationService.API v1"));
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AuthenticationContext>();
                context.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
