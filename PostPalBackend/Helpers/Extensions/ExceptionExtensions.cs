using Microsoft.AspNetCore.Diagnostics;
using PostPalBackend.Helpers.Exceptions;

namespace PostPalBackend.Helpers.Extensions
{
	public static class ExceptionExtensions
	{
		public static void ConfigureExceptionHandler(this IApplicationBuilder app)
		{
			app.UseExceptionHandler(exceptionHandlerApp =>
			{
				exceptionHandlerApp.Run(async context =>
				{
					context.Response.ContentType = "application/json";
					var exceptionHandlerPathFeature =
						context.Features.Get<IExceptionHandlerPathFeature>();

					if (exceptionHandlerPathFeature?.Error is ProjectException)
					{
						var projectException = (ProjectException)exceptionHandlerPathFeature.Error;
						context.Response.StatusCode = projectException.HttpStatusCode;
						await context.Response.WriteAsync(new ExceptionResponse(projectException.Code, projectException.Details).ToString());

						return;
					}

					context.Response.StatusCode = StatusCodes.Status500InternalServerError;
					await context.Response.WriteAsync(new ExceptionResponse(
						ProjectStatusCodes.Http500InternalServerError,
						exceptionHandlerPathFeature == null ? "An exception was thrown" : exceptionHandlerPathFeature.Error.Message).ToString());
				});
			});
		}
	}
}
