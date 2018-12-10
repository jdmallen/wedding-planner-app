using System.Reflection;
using System.Threading;
using JDMallen.Toolbox.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WeddingPlanner.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Thread.Sleep(10000);
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
						.UseKestrel(options => options.ConfigureEndpoints())
						.UseStartup<Startup>()
						.ConfigureAppConfiguration((hostingContext, config) =>
						{
							var env = hostingContext.HostingEnvironment;
							if (env.EnvironmentName.Equals("LinuxDevelopment")){
								var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                        if (appAssembly != null)
                        {
                            config.AddUserSecrets(appAssembly, optional: true);
                        }
							}
						})
						.Build();
		}
	}
}
