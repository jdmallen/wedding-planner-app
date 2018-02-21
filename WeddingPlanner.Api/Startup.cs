using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;
using WeddingPlanner.DataAccess.Config;
using WeddingPlanner.Models.Domain;
using WeddingPlanner.Service.Impls;
using WeddingPlanner.Service.Interfaces;

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

			services.AddIdentity<AppUser, AppRole>()
					.AddEntityFrameworkStores<WpDbContext>()
					.AddDefaultTokenProviders();

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
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey)),
							ClockSkew = TimeSpan.Zero
						};
					});

			services.AddScoped<IPasswordCheckerService, PasswordCheckerService>();

            services.AddMvc();
        }
		
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, WpDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.UseAuthentication();

            app.UseMvc();

			dbContext.Database.EnsureCreated();
		}
    }
}
