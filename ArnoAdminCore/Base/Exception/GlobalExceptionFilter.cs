using ArnoAdminCore.Base.Models;
using ArnoAdminCore.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.Base.Exception
{
    public class GlobalExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MessageException)
            {
                context.Result = new JsonResult(Result.Error(context.Exception.Message));
            }
            else
            {
                LogHelper.WriteWithTime(context.Exception);
                context.Result = new JsonResult(Result.Error("系統內部錯誤"));
            }
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}
