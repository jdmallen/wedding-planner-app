using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using JDMallen.Toolbox.EFCore.Extensions;
using JDMallen.Toolbox.EFCore.Patterns.Repository.Interfaces;
using JDMallen.Toolbox.Extensions;
using JDMallen.Toolbox.Options;
using JDMallen.Toolbox.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.AspNetCore;
using WeddingPlanner.DataAccess.Config;
using WeddingPlanner.DataAccess.Entities.Identity;
using WeddingPlanner.Services.Implementations;
using WeddingPlanner.Services.Interfaces;
using WeddingPlanner.Web.Extensions;
using WeddingPlanner.Web.Middleware;
using WeddingPlanner.Web.Utilities;

namespace WeddingPlanner.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IHostingEnvironment env)
		{
			Configuration = configuration;
			Env = env;
		}

		public IConfiguration Configuration { get; }

		public IHostingEnvironment Env { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var loggerConfig = new LoggerConfiguration();
			loggerConfig.ReadFrom.Configuration(Configuration);//.WriteTo.Debug(new CompactJsonFormatter());
			var logger = loggerConfig.CreateLogger();
			Log.Logger = logger;
			services.AddSingleton<ILoggerFactory>(
				x => new SerilogLoggerFactory(null, true));

			Log.Debug("Reading settings from configuration sources.");

			LogConfiguration();

			// Log.Debug("Configuration -- {0}: {1}", Configuration. );
			var settings = Configuration.GetSection("Settings").Get<Settings>();
			Log.Logger.With("Settings", settings).Debug("Settings section bound to object.");

			Log.Debug("Hosting environment is {HostingEnvironment}", Env.EnvironmentName);
			var oAuthSettings =
				Configuration.GetSection("OAuth").Get<OAuthConfiguration>();
			services.AddSingleton(settings);
			var signingKey =
				new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(settings.JwtSecretKey));

			services.Configure<JwtOptions>(
				options =>
				{
					options.Audience = settings.JwtAudience;
					options.Issuer = settings.JwtIssuer;
					options.ValidForSpan =
						TimeSpan.FromMinutes(settings.JwtExpireMinutes);
					options.SigningCredentials = new SigningCredentials(
						signingKey,
						SecurityAlgorithms.HmacSha256);
				});

			services.AddScoped<ITokenFactory, TokenFactory>();

			var connectionStringBuilder = new SqlConnectionStringBuilder
			{
				DataSource = settings.DbConnectionServer,
				InitialCatalog = settings.DbConnectionDbName,
				UserID = settings.DbConnectionLogin,
				Password = settings.DbConnectionPassword
			};

			if (Env.IsDevelopment())
			{
				connectionStringBuilder.IntegratedSecurity = true;
			}
			else
			{
				connectionStringBuilder.UserID =
					settings.DbConnectionLogin;
				connectionStringBuilder.Password =
					settings.DbConnectionPassword;
			}

			services.AddDbContextPool<WpDbContext>(
				contextOptions => contextOptions.UseSqlServer(
						connectionStringBuilder.ToString(),
						options =>
						{
							options.EnableRetryOnFailure(5);
							options.MigrationsAssembly("WeddingPlanner.Web");
						})
#if DEBUG
					.EnableSensitiveDataLogging()
#endif
			);

			services.AddScoped(typeof(IRepository<,>), typeof(WpRepository<,>));

			services.AddScoped<IInvitationService, InvitationService>();
			services.AddScoped<IInviteeService, InviteeService>();

			services
				.AddCustomIdentity<WpDbContext, AppUser, AppRole, Guid,
					UserStore<
						AppUser,
						AppRole,
						WpDbContext,
						Guid,
						AppUserClaim,
						AppUserRole,
						AppUserLogin,
						AppUserToken,
						AppRoleClaim>, CustomPasswordValidator<AppUser>,
					CustomIdentityErrorDescriber,
					AppUserClaim,
					AppUserRole,
					AppUserLogin,
					AppUserToken,
					AppRoleClaim>(
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
						options.DefaultScheme =
							JwtBearerDefaults.AuthenticationScheme;
						options.DefaultChallengeScheme =
							JwtBearerDefaults.AuthenticationScheme;
					})
				.AddJwtBearer(
					config =>
					{
						config.RequireHttpsMetadata = true;
						config.SaveToken = true;
						config.TokenValidationParameters =
							new TokenValidationParameters
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

			services.AddMvc();
		}

		public void Configure(
			IApplicationBuilder app,
			IHostingEnvironment env,
			ILoggerFactory loggerFactory,
			WpDbContext dbContext)
		{
			app.UseRequestContextMiddleware();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

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
				}
			);
#if DEBUG
			var orderOfDroppage = new List<string>
			{
				"Invitee",
				"MealChoice",
				"Relationship",
				"Invitation",
				"Address",
				"AppUserClaim",
				"AppUserLogin",
				"AppUserToken",
				"AspNetUserRoles",
				"AspNetRoleClaims",
				"AspNetRoles",
				"AppUser"
			};

			dbContext.DropTablesAndEnsureCreated(true, orderOfDroppage);
#endif
		}

		private void LogConfiguration()
		{
			foreach (var configurationProvider in
				((ConfigurationRoot) Configuration)
				.Providers)
			{
				var baseType = configurationProvider.GetType().BaseType;
				var prop = baseType.GetProperty(
					"Data",
					BindingFlags.NonPublic | BindingFlags.Instance);
				var data = (IDictionary<string, string>)
					prop?.GetValue(configurationProvider);
				Log.Logger.With("DictionaryData", data).Debug(
					"Values from {ProviderTypeName}", configurationProvider.GetType().Name);
			}
		}
	}
}
