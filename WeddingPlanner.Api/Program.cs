using System.Net;
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
						.UseKestrel(options =>
						{
							options.Listen(IPAddress.Loopback, 5000);
							options.Listen(IPAddress.Loopback,
											44321,
											listenOptions =>
											{
												listenOptions.UseHttps("cert.pfx", "");
											});
						})
						.UseUrls("https://localhost:44321")
						.UseStartup<Startup>()
						.Build();
		}
	}
}
