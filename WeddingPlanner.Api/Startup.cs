using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using JDMallen.Toolbox.Factories;
using JDMallen.Toolbox.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
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
using Newtonsoft.Json.Linq;
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
			settings.JwtSecretKey = Configuration[nameof(settings.JwtSecretKey)];
			settings.DbConnectionPassword = Configuration[nameof(settings.DbConnectionPassword)];
			settings.GitHubClientSecret = Configuration[nameof(settings.GitHubClientSecret)];
#endif
			services.AddSingleton(settings);

			services.Configure<JwtOptions>(options =>
			{
				options.Audience = settings.JwtAudience;
				options.Issuer = settings.JwtIssuer;
				options.ValidForSpan = TimeSpan.FromMinutes(settings.JwtExpireMinutes);
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSecretKey));
				options.SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			});

			services.AddScoped<IJwtTokenFactory, JwtTokenFactory>();

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
						config.RequireHttpsMetadata = true;
						config.SaveToken = true;
						config.TokenValidationParameters = new TokenValidationParameters
						{
							ValidIssuer = settings.JwtIssuer,
							ValidAudience = settings.JwtIssuer,
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSecretKey)),
							ClockSkew = TimeSpan.Zero
						};
					})
					.AddOAuth("GitHub", options =>
					{
						options.ClientId = settings.GitHubClientId;
						options.ClientSecret = settings.GitHubClientSecret;
						options.CallbackPath = new PathString("/signin-github");
						options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
						options.TokenEndpoint = "https://github.com/login/oauth/access_token";
						options.UserInformationEndpoint = "https://api.github.com/user";
						options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
						options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
						options.ClaimActions.MapJsonKey("urn:github:login", "login");
						options.ClaimActions.MapJsonKey("urn:github:url", "html_url");
						options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

						options.Events = new OAuthEvents
						{
							OnCreatingTicket = async context =>
							{
								var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
								request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
								request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

								var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
								response.EnsureSuccessStatusCode();

								var user = JObject.Parse(await response.Content.ReadAsStringAsync());
								Debug.WriteLine(user);
								context.RunClaimActions(user);
							}
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

			app.UseRewriter(new RewriteOptions().AddRedirectToHttps(302, 44321));

			app.UseAuthentication();

			app.UseStaticFiles("");

			app.UseMvc();

			identityContext.Database.EnsureCreated();
			dbContext.Database.EnsureCreated();
		}
	}
}
