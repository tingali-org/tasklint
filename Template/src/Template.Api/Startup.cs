using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Template.Application;
using Template.Application.Common.Interfaces;
using Template.Infrastructure;
using Template.Infrastructure.Persistence;
using Template.Api.Services;
using NSwag;
using Template.Api.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using NSwag.Generation.Processors.Security;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Template.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string AllowedOriginsCorsPolicy = "_allowedOriginsCorsPolicy";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(c =>
            {
                c.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowedOriginsCorsPolicy,
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44316");
                    });
            });

            //services.AddApplication();

            //services.AddInfrastructure(Configuration);

            //services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddSingleton<ICurrentUserService, CurrentUserService>();

            //services.AddHttpContextAccessor();

            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddHealthChecks();
            //services.AddHealthChecks()
            //        .AddDbContextCheck<ApplicationDbContext>();

            services.AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddFluentValidation();

            services.AddApiVersioning(c =>
            {
                c.DefaultApiVersion = new ApiVersion(1, 0);
                c.ApiVersionReader = new HeaderApiVersionReader("api-version");
                c.AssumeDefaultVersionWhenUnspecified = true;
                c.ReportApiVersions = true;
            });

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "Template.Api";
                configure.Version = "v1";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseOpenApi();

                app.UseSwaggerUi3(settings =>
                {
                    settings.DocumentTitle = "Template.Api v1";
                });

                app.UseDeveloperExceptionPage();

                app.UseMigrationsEndPoint();

                app.UseCors(this.AllowedOriginsCorsPolicy);
            }

            app.UseHealthChecks("/health");

            app.UseHttpsRedirection();

            app.UseExceptionHandler("/Error");

            app.UseHsts();

            app.UseRouting();

            app.UseAuthentication();

            //app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
