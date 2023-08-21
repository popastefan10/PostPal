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

					ProjectException? projectException = null;
					if (exceptionHandlerPathFeature?.Error is ProjectException)
					{
						projectException = (ProjectException)exceptionHandlerPathFeature.Error;
					}
					else if (exceptionHandlerPathFeature?.Error.InnerException is ProjectException)
					{
						projectException = (ProjectException)exceptionHandlerPathFeature.Error.InnerException;
					}
					if (projectException != null)
					{
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
