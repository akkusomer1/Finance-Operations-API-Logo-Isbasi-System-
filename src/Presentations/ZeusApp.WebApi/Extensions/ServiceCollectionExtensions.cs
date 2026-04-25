using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreHero.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ZeusApp.Application.DTOs.Settings;
using ZeusApp.Application.Interfaces;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Application.Interfaces.Shared;
using ZeusApp.Infrastructure.DbContexts;
using ZeusApp.Infrastructure.Identity.Models;
using ZeusApp.Infrastructure.Identity.Services;
using ZeusApp.Infrastructure.Repositories;
using ZeusApp.Infrastructure.Shared.Services;
using ZeusApp.WebApi.Services;

namespace ZeusApp.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
        services.AddTransient<IDateTimeService, SystemDateTimeService>();
        services.AddTransient<IMailService, SMTPMailService>();
        services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();

        services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IBankRepository, BankRepository>();
        services.AddTransient<ICaseRepository, CaseRepository>();
        services.AddTransient<IContactRepository, ContactRepository>();
        services.AddTransient<IOtherAddressRepository, OtherAddressRepository>();
        services.AddTransient<IRelatedPersonRepository, RelatedPersonRepository>();
        services.AddTransient<IProductServiceRepository, ProductServiceRepository>();
        services.AddTransient<IProductBrandRepository, ProductBrandRepository>();
        services.AddTransient<IProductServiceCategoryRepository, ProductServiceCategoryRepository>();
        services.AddTransient<ICustomerCategoryRepository, CustomerCategoryRepository>();
        services.AddTransient<IProductInvoiceRepository, ProductInvoiceRepository>();
        services.AddTransient<IProductStockRepository, ProductStockRepository>();
        services.AddTransient<IInvoiceCategoryRepository, InvoiceCategoryRepository>();
        services.AddTransient<IInvoiceRepository, InvoiceRepository>();
        services.AddTransient<IStockCategoryRepository, StockCategoryRepository>();
        services.AddTransient<ICustomerSupplierRepository, CustomerSupplierRepository>();
        services.AddTransient<IUnitRepository, UnitRepository>();
        services.AddTransient<IServiceGroupRepository,ServiceGroupRepository>();
        services.AddTransient<IStockRepository, StockRepository>();
        services.AddTransient<IExpenseCategoryRepository, ExpenseCategoryRepository>();
        services.AddTransient<IGeneralBankRepository, GeneralBankRepository>();
        services.AddTransient<IBankAccountRepository, BankAccountRepository>();
        services.AddTransient<ILoanRepository, LoanRepository>();
        services.AddTransient<ILoanCategoryRepository, LoanCategoryRepository>();
        services.AddTransient<IOrderCategoryRepository, OrderCategoryRepository>();
        services.AddTransient<ICarrierCompanyRepository, CarrierCompanyRepository>();
        services.AddTransient<IExpenseRepository, ExpenseRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IHoldRepository, HoldRepository>();
        services.AddTransient<IProductOrderRepository, ProductOrderRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
    }

    public static void AddEssentials(this IServiceCollection services)
    {
        services.RegisterSwagger();
        services.AddVersioning();
    }

    private static void RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\ZeusApp.WebApi.xml");

            c.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "ZeusApp.WebApi",
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    }, new List<string>()
                },
            });
        });
    }

    private static void AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(2, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });
    }

    public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
        }
        else
        {
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<IdentityContext>().AddDefaultUI().AddDefaultTokenProviders();

        services.AddTransient<IIdentityService, IdentityService>();

        var key = Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            RequireExpirationTime = false,
            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero,
            ValidIssuer = configuration["JWTSettings:Issuer"],
            ValidAudience = configuration["JWTSettings:Audience"],
        };

        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
        services.AddSingleton(tokenValidationParameters);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParameters;

                jwt.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(Result.Fail("Yetkisiz erisim", 404));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(Result.Fail("Yetkisiz erisim", 403));
                        return context.Response.WriteAsync(result);
                    }
                };
            });
    }
}
