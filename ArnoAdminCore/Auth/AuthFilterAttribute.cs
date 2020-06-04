using ArnoAdminCore.Base.Models;
using ArnoAdminCore.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            base.OnActionExecuting(context);
            //if (context.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            //{
            //    return;
            //}
            //Operator op = Current.Operator;
            //if (op == null)
            //{
            //    actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.Unauthorized, "Unauthorized");
            //}
            //else
            //{
            //    if (!String.IsNullOrEmpty(Permission))
            //    {

            //    }
            //}
        }
    }
}
