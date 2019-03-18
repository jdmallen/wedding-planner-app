using Serilog;

namespace WeddingPlanner.Web.Extensions
{
	public static class CustomSerilogExtensions
	{
		public static ILogger With(
			this ILogger logger,
			string propertyName,
			object value)
			=> logger.ForContext(propertyName, value, true);
	}
}
