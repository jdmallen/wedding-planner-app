using System.Threading;
using JDMallen.Toolbox.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
						.Build();
		}
	}
}
