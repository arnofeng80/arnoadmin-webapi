using ArnoAdminCore.Base.Models;
using ArnoAdminCore.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Reflection;

namespace ArnoAdminCore.Auth
{
    public class AuthFilterAttribute : ActionFilterAttribute
    {
        public String Permission { get; set; }
        public AuthFilterAttribute() { }
        public AuthFilterAttribute(string permission)
        {
            this.Permission = permission;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.ActionDescriptor as ControllerActionDescriptor;

            if (action.MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            Operator op = Current.Operator;
            if (op == null)
            {
                context.Result = new JsonResult(Result.Unauthorized());
            }
            else
            {
                if (!String.IsNullOrEmpty(Permission))
                {

                }
            }
        }
    }
}
