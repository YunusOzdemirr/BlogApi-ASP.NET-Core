using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.API.Filters;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Services.AutoMapper.Profiles;
using CmnSoftwareBackend.Services.Extensions;
using CmnSoftwareBackend.Shared.Utilities.Security.Encryption;
using CmnSoftwareBackend.Shared.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CmnSoftwareBackend.API
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

            services.AddControllers(options=>
            {
                options.Filters.Add<ValidationFilter>();
                options.Filters.Add<JsonExceptionFilter>();
            }).AddNewtonsoftJson(options=>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReact", builder => builder.WithOrigins("http://localhost:3000","http://localhost3001").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });
            services.AddAutoMapper(typeof(CategoryProfile), typeof(UserProfile), typeof(OperationClaimProfile), typeof(UserNotificationProfile),typeof(UserTokenProfile),typeof(ArticleProfile),typeof(CommentWithUserProfile));
            services.LoadMyServices();
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cmn.Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description= @"JWT Authorization header using the Bearer scheme.
                     Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name="Authorization",
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.ApiKey,
                    Scheme="Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme="oauth2",
                            Name="Bearer",
                            In=ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cmn.API v1"));
            app.UseHsts();
            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowReact");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
