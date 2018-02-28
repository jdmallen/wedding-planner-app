using JDMallen.Toolbox.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
			return WebHost.CreateDefaultBuilder(args)
						.UseKestrel(options => options.ConfigureEndpoints())
						.UseUrls("https://localhost:44321")
						.UseStartup<Startup>()
						.Build();
		}
	}
}
