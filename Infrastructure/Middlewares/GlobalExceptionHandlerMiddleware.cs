﻿namespace CRM.Services.Identity.Infrastructure.Middlewares
{
	public class GlobalExceptionHandlerMiddleware : object
	{
		public GlobalExceptionHandlerMiddleware
			(RequestDelegate next) : base()
		{
			Next = next;
		}

		private RequestDelegate Next { get; }

		public async Task
			InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await Next(httpContext);
			}
			catch //(System.Exception ex)
			{
				// Log Error (ex)!

				httpContext.Response.Redirect
					(location: "/Errors/Error500", permanent: false);
			}
		}
	}
}
