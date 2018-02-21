using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WeddingPlanner.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
		{
			var certificate = new X509Certificate2();

			var builder = new WebHostBuilder()
				.UseKestrel(
					options =>
					{
						options.AddServerHeader = false;
						options.Listen(IPAddress.Loopback, 44300,
										listenOptions =>
										{
//											listenOptions.UseHttps();
										});
					})
				.UseContentRoot(Directory.GetCurrentDirectory())
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					var env = hostingContext.HostingEnvironment;

					config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
						.AddJsonFile($"appsettings.{env.EnvironmentName}.json",
									optional: true,
									reloadOnChange: true);

					if (env.IsDevelopment())
					{
						var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
						if (appAssembly != null)
						{
							config.AddUserSecrets(appAssembly, optional: true);
						}
					}

					config.AddEnvironmentVariables();

					if (args != null)
					{
						config.AddCommandLine(args);
					}
				})
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					logging.AddConsole();
					logging.AddDebug();
				})
				.UseIISIntegration()
				.UseDefaultServiceProvider((context, options) =>
				{
					options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
				})
				.ConfigureServices(services =>
				{
					services.AddTransient<IConfigureOptions<KestrelServerOptions>, KestrelServerOptionsSetup>();
				})
				.UseStartup<Startup>();

			return builder.Build();
		}
	}
}
