using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
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
using WeddingPlanner.DataAccess.Config;
using WeddingPlanner.Models.Entities;

namespace WeddingPlanner.Api
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
#if DEBUG
			settings.JwtSecretKey = Configuration["JwtSecretKey"];
			settings.DbConnectionPassword = Configuration["DbConnectionPassword"];
			settings.CertificatePassword = Configuration["CertificatePassword"];
#endif
			services.AddSingleton(settings);

			var builder = new SqlConnectionStringBuilder
			{
				DataSource = settings.DbConnectionServer,
				InitialCatalog = "WeddingPlanner",
				UserID = settings.DbConnectionLogin,
				Password = settings.DbConnectionPassword
			};

			services.AddDbContext<WpDbContext>(contextOptions => contextOptions.UseSqlServer(
													builder.ToString(),
													sqlServerOptions =>
													{
														sqlServerOptions.UseRowNumberForPaging(false);
														sqlServerOptions.EnableRetryOnFailure(5);
													}));

			services.AddCustomIdentity<WpIdentityContext, AppUser, AppRole>(builder.ToString());

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			services.AddAuthentication(options =>
					{
						options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
						options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
						options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					})
					.AddJwtBearer(config =>
					{
#if DEBUG
						config.RequireHttpsMetadata = false;
#endif
						config.SaveToken = true;
						config.TokenValidationParameters = new TokenValidationParameters
						{
							ValidIssuer = settings.JwtIssuer,
							ValidAudience = settings.JwtIssuer,
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSecretKey)),
							ClockSkew = TimeSpan.Zero
						};
					});

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
			WpIdentityContext identityContext,
			WpDbContext dbContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRewriter(new RewriteOptions().AddRedirectToHttps());

			app.UseAuthentication();

			app.UseMvc();

			identityContext.Database.EnsureCreated();
			dbContext.Database.EnsureCreated();
		}
	}
}
