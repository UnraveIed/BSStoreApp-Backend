using AspNetCoreRateLimit;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using Presentation.ActionFilters;
using Presentation.Controllers;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;

namespace WebAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));


        public static void ConfigureRepositoryManager(this IServiceCollection services) => 
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerService, LoggerManager>();

        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddSingleton<LogFilterAttribute>();
            services.AddScoped<ValidateMediaTypeAttribute>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",builder=>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.WithExposedHeaders("X-Pagination");
                });
            });
        }

        public static void ConfigureDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<BookDto>, DataShaper<BookDto>>();
        }

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config
                .OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

                if(systemTextJsonOutputFormatter != null)
                {
                    systemTextJsonOutputFormatter
                    .SupportedMediaTypes
                    .Add("application/vnd.unravel.hateoas+json");

                    systemTextJsonOutputFormatter
                    .SupportedMediaTypes
                    .Add("application/vnd.unravel.apiroot+json");
                }

                var xmlOutputFormatter = config
                .OutputFormatters
                .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

                if(xmlOutputFormatter != null)
                {
                    xmlOutputFormatter
                    .SupportedMediaTypes
                    .Add("application/vnd.unravel.hateoas+xml");

                    xmlOutputFormatter
                    .SupportedMediaTypes
                    .Add("application/vnd.unravel.apiroot+xml");
                }
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options=>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                options.Conventions.Controller<BooksController>()
                    .HasApiVersion(new ApiVersion(1, 0));
                options.Conventions.Controller<BooksV2Controller>()
                    .HasDeprecatedApiVersion(new ApiVersion(2, 0));
            });
        }

        public static void ConfigureResponseCaching(this IServiceCollection services) => 
            services.AddResponseCaching();

        public static void ConfigureHttpCacheHeaders(this IServiceCollection services) => 
            services.AddHttpCacheHeaders(options=>
            {
                options.MaxAge = 90;
                options.CacheLocation = Marvin.Cache.Headers.CacheLocation.Public;
            },
             validationOpt =>
             {
                 validationOpt.MustRevalidate = false;
             });

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>()
            {
                new RateLimitRule()
                {
                    Endpoint = "*",
                    Limit = 3,
                    Period = "1m"
                }
            };

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(opt=>
            {
                // Kullanici sifre kurallari
                //opt.Password.RequireDigit = true;
                //opt.Password.RequireLowercase = true;
                //opt.Password.RequireUppercase = true;
                //opt.Password.RequireNonAlphanumeric = true;
                //opt.Password.RequiredLength = 5;

                //opt.User.RequireUniqueEmail = true;

                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 5;

                opt.User.RequireUniqueEmail = true;

            })
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }

    }
}
