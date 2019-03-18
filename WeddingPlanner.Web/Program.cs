using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HostFiltering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WeddingPlanner.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args)
		{
			return CreateCustomBuilder(args)
				.UseStartup<Startup>()
				.Build();
		}

		/// <summary>
		/// Based on 4e44025 of WebHost.cs
		/// in github.com/aspnet/AspNetCore/src/DefaultBuilder/src
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IWebHostBuilder CreateCustomBuilder(string[] args)
		{
			var builder = new WebHostBuilder();

			if (string.IsNullOrEmpty(
				builder.GetSetting(WebHostDefaults.ContentRootKey)))
			{
				builder.UseContentRoot(Directory.GetCurrentDirectory());
			}

			if (args != null)
			{
				builder.UseConfiguration(
					new ConfigurationBuilder().AddCommandLine(args).Build());
			}

			builder.ConfigureAppConfiguration(
					(hostingContext, config) =>
					{
						var env = hostingContext.HostingEnvironment;

						config.AddJsonFile(
								"appsettings.json",
								optional: true,
								reloadOnChange: true)
							.AddJsonFile(
								$"appsettings.{env.EnvironmentName}.json",
								optional: true,
								reloadOnChange: true);

						if (env.IsDevelopment()
						    || env.EnvironmentName.IndexOf(
							    "development",
							    StringComparison.CurrentCultureIgnoreCase)
						    != -1)
						{
							var appAssembly = Assembly.Load(
								new AssemblyName(env.ApplicationName));
							if (appAssembly != null)
							{
								config.AddUserSecrets(
									appAssembly,
									optional: true);
							}
						}

						config.AddEnvironmentVariables("WP_");

						if (args != null)
						{
							config.AddCommandLine(args);
						}
					})
				.ConfigureLogging(
					(hostingContext, logging) =>
					{
						logging.AddConfiguration(
							hostingContext.Configuration.GetSection("Logging"));
						logging.AddConsole();
						logging.AddDebug();
						logging.AddEventSourceLogger();
					})
				.UseDefaultServiceProvider(
					(context, options) =>
					{
						options.ValidateScopes =
							context.HostingEnvironment.IsDevelopment();
					});

			builder.UseKestrel(
					(builderContext, options) =>
					{
						options.Configure(
							builderContext.Configuration.GetSection("Kestrel"));
					})
				.ConfigureServices(
					(hostingContext, services) =>
					{
						// Fallback
						services.PostConfigure<HostFilteringOptions>(
							options =>
							{
								if (options.AllowedHosts == null
								    || options.AllowedHosts.Count == 0)
								{
									// "AllowedHosts": "localhost;127.0.0.1;[::1]"
									var hosts = hostingContext
										.Configuration["AllowedHosts"]
										?.Split(
											new[] {';'},
											StringSplitOptions
												.RemoveEmptyEntries);
									// Fall back to "*" to disable.
									options.AllowedHosts = (hosts?.Length > 0
										? hosts
										: new[] {"*"});
								}
							});
						// Change notification
						services
							.AddSingleton<IOptionsChangeTokenSource<
								HostFilteringOptions>>(
								new ConfigurationChangeTokenSource<
									HostFilteringOptions>(
									hostingContext.Configuration));

						services
							.AddTransient<IStartupFilter,
								HostFilteringStartupFilter>();

						services.AddRouting();
					});

			return builder;
		}
	}

	internal class HostFilteringStartupFilter : IStartupFilter
	{
		public Action<IApplicationBuilder> Configure(
			Action<IApplicationBuilder> next)
		{
			return app =>
			{
				app.UseHostFiltering();
				next(app);
			};
		}
	}
}
