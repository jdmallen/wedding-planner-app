using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace WeddingPlanner.Api.Middleware
{
	public class RequestContextMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly string _propertyName;
		public const string DefaultPropertyName = "HttpRequestId";

		public RequestContextMiddleware(
			RequestDelegate next,
			string propertyName = DefaultPropertyName)
		{
			_next = next;
			_propertyName = propertyName;
		}

		public Task Invoke(HttpContext httpContext)
		{
			using (LogContext.PushProperty(_propertyName, Guid.NewGuid()))
			{
				return _next(httpContext);
			}
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class RequestContextMiddlewareExtensions
	{
		public static IApplicationBuilder UseRequestContextMiddleware(
			this IApplicationBuilder builder,
			string propertyName = RequestContextMiddleware.DefaultPropertyName)

		{
			return builder
				.UseMiddleware<RequestContextMiddleware>(propertyName);
		}
	}
}
