﻿using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using System.IO;

namespace ApiCatalogoFilmes.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch
            {
                await HandleExceptionAsync(context);
            }
        }


        private static async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new { Message = "Erro inesperado durante a solicitação. Tente mais tarde!" });
        }
    }
}
