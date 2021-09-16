using DefineX.Services.Discount.Business.Interfaces;
using DefineX.Services.Discount.DataAccess.Concrete.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.Middlewares
{
    public class LoggingMiddleware
    {

        private RequestDelegate next;

        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IMongoService _mongoService)
        {
            var originalBody = context.Response.Body;
            using var newBody = new MemoryStream();
            context.Response.Body = newBody;

            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            await _mongoService.LogEkle(new MongoLogModel { Path = context.Request.Path, LogType = "Rqt" , Data = requestBodyText });

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            try
            {
                await this.next(context);
                context.Request.Body = originalRequestBody;
            }
            finally
            {
                newBody.Seek(0, SeekOrigin.Begin);
                var bodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                await _mongoService.LogEkle(new MongoLogModel { Path = context.Request.Path, LogType = "Rsp", Data = bodyText });
                newBody.Seek(0, SeekOrigin.Begin);
                await newBody.CopyToAsync(originalBody);
            }
        }
    }
}
