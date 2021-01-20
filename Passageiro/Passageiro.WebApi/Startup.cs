using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Passageiro.Repository.Data;
using Passageiro.Repository.Interfaces;
using Passageiro.Repository.Repositorys;
using Passageiro.Service.Interfaces;
using Passageiro.Service.Services;

namespace Passageiro.WebApi {
    public class Startup {
        public Startup (IConfiguration _configuration) {
            this._configuration = _configuration;

        }
        public IConfiguration _configuration { get; }
        private readonly IHostEnvironment _environment;
        private IConfigurationRoot _config;
        public Startup (
            IConfiguration configuration,
            IHostEnvironment environment
        ) {
            this._configuration = configuration;
            this._environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddMvc (options => {
                    var policy = new AuthorizationPolicyBuilder ()
                        .RequireAuthenticatedUser ()
                        .Build ();
                    options.Filters.Add (new AuthorizeFilter (policy));
                    options.EnableEndpointRouting = false;
                })
                .SetCompatibilityVersion (CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson ();

            services.AddCors ();
            services.AddControllers ();

            services.AddDbContext<PassageiroContext> (options =>
                options.UseSqlServer (_configuration.GetConnectionString ("DefaultConnection")));

            this.InitializeAutoMapper ();
            this._ConfigureInjectionDependecy (services);
            this._ConfigureSwagger (services);

            services.AddAutoMapper ();

            services.AddAuthorization (options => {
                options.AddPolicy ("Sistema", policy => { policy.RequireClaim (ClaimTypes.Role, "Sistema"); });
                options.AddPolicy ("Usuario", policy => { policy.RequireClaim (ClaimTypes.Role, "Usuario"); });
            });

            services.AddAuthentication (option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = Convert.ToBoolean (_configuration["JWTSettings:ValidateIssuer"]),
                ValidateAudience = Convert.ToBoolean (_configuration["JWTSettings:ValidateAudience"]),
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JWTSettings:Issuer"],
                ValidAudience = _configuration["JWTSettings:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_configuration["JWTSettings:Secret"]))
                };
            });

        }

        private void _ConfigureInjectionDependecy (IServiceCollection services) {
            services.AddScoped<IControleAcessoService, ControleAcessoService> ();
            services.AddScoped<IPassageiroService, PassageiroService> ();

            services.AddScoped<IControleAcessoRepository, ControleAcessoRepository> ();
            services.AddScoped<IPassageiroRepository, PassageiroRepository> ();
        }

        private void _ConfigureSwagger (IServiceCollection services) {
            services.AddSwaggerGen (s => {
                s.SwaggerDoc ("v1", new OpenApiInfo {
                    Version = "v1",
                        Title = "AeroAPI API",
                        Description = "API .NET Core 3.1",
                        Contact = new OpenApiContact {
                            Name = "AeroAPI",
                                Email = string.Empty,
                                Url = new Uri ("https://github.com/luicesar")
                        }
                });

                s.AddSecurityDefinition ("Bearer", new OpenApiSecurityScheme {
                    Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme."
                });

                s.AddSecurityRequirement (new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] { }

                    }
                });
            });
        }
        public void InitializeAutoMapper () {
            Mapper.Initialize (x =>
                x.AddProfile<MappingProfile> ()
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ())
                app.UseDeveloperExceptionPage ();
            else
                app.UseHttpsRedirection ();

            app.Use (async (context, next) => {
                await next ();

                if (context.Response.StatusCode == 404) {
                    context.Request.Path = "/index.html";
                    await next ();
                }
            });

            // Ativando middlewares para uso do Swagger
            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "API .NET Core 3.1 - V1");
            });

            app.UseHttpsRedirection ();

            app.UseStaticFiles ();
            app.UseRouting ();
            app.UseCors (x => x.AllowAnyOrigin ().AllowAnyMethod ().AllowAnyHeader ());

            app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });

            // app.Run (async (context) => {
            //     await context.Response.WriteAsync ("Não foi possível encontrar nada");
            // });

            // var options = new RewriteOptions ().AddRedirectToHttps (301, 5001);
            // app.UseRewriter (options);

            app.UseMvc ();
        }

    }
}