using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TaskLint.Application;
using TaskLint.Infrastructure;
using TaskLint.Infrastructure.Persistence;
using NSwag;
using TaskLint.Api.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using NSwag.Generation.Processors.Security;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace TaskLint.Api
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
            services.AddApplication();

            services.AddInfrastructure(Configuration);

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

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddHttpContextAccessor();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationDbContext>();

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
                configure.Title = "TaskLint.Api";
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
                    settings.DocumentTitle = "TaskLint.Api v1";
                });

                app.UseDeveloperExceptionPage();

                app.UseMigrationsEndPoint();

                app.UseCors(this.AllowedOriginsCorsPolicy);
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHealthChecks("/health");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
