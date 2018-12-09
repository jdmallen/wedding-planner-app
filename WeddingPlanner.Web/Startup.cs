using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JDMallen.Toolbox.Extensions;
using JDMallen.Toolbox.Factories;
using JDMallen.Toolbox.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using WeddingPlanner.DataAccess.Config;
using WeddingPlanner.DataAccess.Entities.Identity;
using WeddingPlanner.Web.Utilities;

namespace WeddingPlanner.Web
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
			var settings = Configuration.GetSection("Settings").Get<Settings>();
			var oAuthSettings =
				Configuration.GetSection("OAuth").Get<OAuthConfiguration>();
			services.AddSingleton(settings);

			var signingKey =
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSecretKey));

			services.Configure<JwtOptions>(
				options =>
				{
					options.Audience = settings.JwtAudience;
					options.Issuer = settings.JwtIssuer;
					options.ValidForSpan = TimeSpan.FromMinutes(settings.JwtExpireMinutes);
					options.SigningCredentials = new SigningCredentials(
						signingKey,
						SecurityAlgorithms.HmacSha256);
				});

			services.AddScoped<ITokenFactory, TokenFactory>();

			var mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
			{
				Server = settings.DbConnectionServer,
				Database = settings.DbConnectionDbName,
				UserID = settings.DbConnectionLogin,
				Password = settings.DbConnectionPassword,
				OldGuids = true
			};

			services.AddDbContextPool<WpDbContext>(
				contextOptions => contextOptions.UseMySql(
					mySqlConnectionStringBuilder.ToString(),
					mySqlOptions =>
					{
						mySqlOptions.UnicodeCharSet(CharSet.Utf8mb4);
						mySqlOptions.EnableRetryOnFailure(5);
					}));

			services.AddCustomIdentity<WpDbContext, AppUser, AppRole>(
				options =>
				{
					options.Password.RequireDigit = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireUppercase = false;
					options.Password.RequiredLength = 2;
					options.Password.RequiredUniqueChars = 1;
				});

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			services.AddAuthentication(
						options =>
						{
							options.DefaultAuthenticateScheme =
								JwtBearerDefaults.AuthenticationScheme;
							options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
							options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
						})
					.AddJwtBearer(
						config =>
						{
							config.RequireHttpsMetadata = true;
							config.SaveToken = true;
							config.TokenValidationParameters = new TokenValidationParameters
							{
								ValidIssuer = settings.JwtIssuer,
								ValidAudience = settings.JwtIssuer,
								ValidateIssuerSigningKey = true,
								IssuerSigningKey = signingKey,
								RequireExpirationTime = true,
								ClockSkew = TimeSpan.Zero
							};
						})
					.AddOAuthGitHub(oAuthSettings)
					.AddOAuthGoogle(oAuthSettings);

			services.AddAntiforgery(
				options =>
				{
					options.Cookie.Name = "_af";
					options.Cookie.HttpOnly = true;
					options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
					options.HeaderName = "X-XSRF-TOKEN";
				}
			);

			services.AddMvc(
				options =>
				{
					options.SslPort = 44321;
					options.Filters.Add(new RequireHttpsAttribute());
				});
		}

		public void Configure(
			IApplicationBuilder app,
			IHostingEnvironment env,
			ILoggerFactory loggerFactory,
			WpDbContext dbContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRewriter(new RewriteOptions().AddRedirectToHttps(302, 44321));

			app.UseAuthentication();

			app.UseStaticFiles("");

			app.UseStatusCodePagesWithRedirects("/");

			app.UseMvc(
				routes =>
				{
					routes.MapRoute(
						name: "default",
						template: "{controller=BaseView}/{action=Index}/{id?}");
					routes.MapSpaFallbackRoute(
						name: "spa-fallback",
						defaults: new
						{
							controller = "BaseView",
							action = "Index"
						});
				});
#if DEBUG
			dbContext.DropTablesAndEnsureCreated();
#endif
		}
	}
}
