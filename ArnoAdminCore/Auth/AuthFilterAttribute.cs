using ArnoAdminCore.Base.Models;
using ArnoAdminCore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ArnoAdminCore.Auth
{
    public class AuthFilterAttribute : AuthorizationFilterAttribute
    {
        public String Permission { get; set; }
        public AuthFilterAttribute() { }
        public AuthFilterAttribute(string permission)
        {
            this.Permission = permission;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            Operator op = Current.Operator;
            if (op == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.Unauthorized, "Unauthorized");
            }
            else
            {
                if(!String.IsNullOrEmpty(Permission))
                {

                }
            }
        }
    }
}
