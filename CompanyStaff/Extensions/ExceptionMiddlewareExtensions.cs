﻿using Contracts;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CompanyStaff.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigExceptionHandler(this WebApplication webApplication, ILoggerManager logger) 
        {
            webApplication.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Interval Server Error",
                        }.ToString());
                    }
                });
            });
        }
    }
}