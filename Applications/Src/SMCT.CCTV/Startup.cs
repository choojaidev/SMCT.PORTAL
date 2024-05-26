using Clients;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SMCTPortal.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

//using SMCTPortal.DataAccess.Settings;
//using SMCTPortal.DataAccess.Repositories;
using Microsoft.Extensions.Options;
//using SMCTPortal.DataAccess.Repositories.Interfaces;
using MongoDB.Driver;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Microsoft.AspNetCore.Hosting;
namespace SMCTPortal
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //routes.MapRoute(nameof: "",
            //  URL: "",
            //  default:new { controller = "CitizenController", action = "create" }
            //  );




            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
       

            services.AddControllersWithViews();

            services.AddHttpClient();
            services.AddHttpClient("WIN-4T2Q1LE89DO").ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            services.AddSingleton<IDiscoveryCache>(r =>
            {
                var factory = r.GetRequiredService<IHttpClientFactory>();
                return new DiscoveryCache(Constants.Authority, () => factory.CreateClient());
            });
            
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie(options =>
                {
                    options.Cookie.Name = "SMCTPORTAL";
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = Constants.Authority;
                    //live id => SMCTPORTAL-Live : live => 2CuPN9Zn8Ipr8jsp1w9MlQ== ()
                    options.ClientId = "SMCTPORTAL-CCTV-DEV"; //weatherforcast-DEV SMCTPORTAL-DEV 
                    options.ClientSecret = "secret";

                    // code flow + PKCE (PKCE is turned on by default)
                    options.ResponseType = "code";
                    options.UsePkce = true;

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("custom.profile");
                    options.Scope.Add("resource1.scope1");
                    options.Scope.Add("resource2.scope1");
                    options.Scope.Add("offline_access");

                    // not mapped by default
                    options.ClaimActions.MapAll();
                    options.ClaimActions.MapJsonKey("website", "website");
                    options.ClaimActions.MapCustomJson("address", (json) => json.GetRawText());

                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtClaimTypes.Name,
                        RoleClaimType = JwtClaimTypes.Role,
                    };

                    options.Events.OnRedirectToIdentityProvider = ctx =>
                    {
                        // ctx.ProtocolMessage.Prompt = "create";
                        return Task.CompletedTask;
                    };
                });

      
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
                Secure = CookieSecurePolicy.Always,
            });
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.Use(async (ctx, next) =>
            {
                ctx.Request.Scheme = "https";
                await next();
            });
            app.UseStaticFiles();

            app.UseRouting();



            //app.Use(async (ctx, next) =>
            //{
            //    if (ctx.Request.Path == "/signout-oidc" &&
            //        !ctx.Request.Query["skip"].Any())
            //    {
            //        var location = ctx.Request.Path +
            //           ctx.Request.QueryString + "&skip=1";
            //        ctx.Response.StatusCode = 200;
            //        var html = $@"
            //<html><head>
            //   <meta http-equiv='refresh' content='0;url={location}' />
            //</head></html>";
            //        await ctx.Response.WriteAsync(html);
            //        return;
            //    }

            //    await next();

            //    if (ctx.Request.Path == "/signin-oidc" &&
            //        ctx.Response.StatusCode == 302)
            //    {
            //        var location = ctx.Response.Headers["location"];
            //        ctx.Response.StatusCode = 200;
            //        var html = $@"
            //  <html><head>
            //     <meta http-equiv='refresh' content='0;url={location}' />
            //  </head></html>";
            //        await ctx.Response.WriteAsync(html);
            //    }
            //});
           
                app.UseExceptionHandler("/Home/Error");
           




            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            app.UseCors();
            app.UseCookiePolicy();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
        name: "root",
        pattern: "/",
        defaults: new { controller = "Portal", action = "Index" });
                endpoints.MapDefaultControllerRoute()
                    .RequireAuthorization();
            });
        }
    }
}