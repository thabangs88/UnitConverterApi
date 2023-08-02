using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using UnitConverterAPI.Context;
using UnitConverterAPI.Extensions;
using UnitConverterAPI.Interface;
using UnitConverterAPI.Options;
using UnitConverterAPI.Service;

namespace UnitConverterAPI
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                             .SetBasePath(env.ContentRootPath)
                             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                          .AddEnvironmentVariables();
                                Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }

        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                var tokenOptions = Configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = tokenOptions.GetSymmetricSecurityKey()
                };
            });

            _ = services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v3.1", new OpenApiInfo()
                {
                    Description = "API Service",
                    Contact = new OpenApiContact
                    {
                        Name = "Thabang Sibanyoni",
                        Email = "thabang.sibanyoni@gmail.co.za",
                        Url = new Uri("https://www.easyunitconverter.com/")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Specify the authorization token.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "bearer",

                });

                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                  {
                    {securityScheme, new string[] { }},
                  };
                c.AddSecurityRequirement(securityRequirements);
                c.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));
                c.TagActionsBy(api => api.GroupName);
                c.EnableAnnotations();
            });

            services.AddDbContext<AuthenticationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MasterUserdb")), ServiceLifetime.Transient);
            services.AddDbContext<UnitConverterDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MasterUnitConverterdb")), ServiceLifetime.Transient);

            services.AddOptions();

            services.Configure<TokenOptions>(Configuration.GetSection(nameof(TokenOptions)));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAuthorizationsService, AuthorizationService>();
            services.AddTransient<ITemperatureUnitService, TemperatureUnitService>();
            services.AddTransient<IWeightUnitService, WeightUnitService>();
            services.AddTransient<ILengthUnitService, LengthUnitService>();
            services.AddTransient<IAuthenticationDbContextService, AuthenticationDbContext>();
            services.AddTransient<IUnitConverterDbContextService, UnitConverterDbContext>();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder => builder
                  .AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  );


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v3.1/swagger.json", "ABSA API v3.1");
                c.RoutePrefix = "docs";
            });

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsApi");

     //       app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
