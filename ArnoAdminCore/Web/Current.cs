using ArnoAdminCore.Base;
using Microsoft.Extensions.DependencyInjection;
using ArnoAdminCore.SystemManage.Models.Poco;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using ArnoAdminCore.Utils.Extension;
using System.Net;
using System.Net.Sockets;
using ArnoAdminCore.Base.Constants;
using ArnoAdminCore.Cache;
using ArnoAdminCore.SystemManage.Services;
using AutoMapper;

namespace ArnoAdminCore.Web
{
    public class Current
    {
        public static Operator Operator
        {
            get
            {
                var token = Session.GetString(WebConst.TOKEN_NAME);
                if (String.IsNullOrEmpty(token))
                {
                    return null;
                }
                Operator op = CacheFactory.Cache.GetCache<Operator>(token);
                if (op == null)
                {
                    IMapper _mapper = GlobalContext.ServiceProvider.GetService<IMapper>();
                    IUserService _userService = GlobalContext.ServiceProvider.GetService<IUserService>();
                    var user = _userService.FindByToken(token);
                    op = _mapper.Map<Operator>(user);
                    op.Roles = _userService.FindRolesByUserId(user.Id);
                    op.Menus = _userService.FindMenusByUserId(user.Id);

                    if (op != null)
                    {
                        CacheFactory.Cache.SetCache(token, op);
                    }
                }
                return op;
            }
            set
            {
                if (value == null)
                {
                    var token = Session.GetString(WebConst.TOKEN_NAME);
                    if (token != null)
                    {
                        if (value == null)
                        {
                            CacheFactory.Cache.RemoveCache(token);
                        }
                        else
                        {
                            CacheFactory.Cache.SetCache(token, value);
                        }
                    }
                }
            }
        }

        public static HttpContext HttpContext
        {
            get
            {
                IHttpContextAccessor hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
                return hca?.HttpContext;
            }
        }

        public static ISession Session
        {
            get
            {
                return HttpContext?.Session;
            }
        }

        public static String IP
        {
            get
            {
                string result = string.Empty;
                try
                {
                    if (HttpContext != null)
                    {
                        result = GetWebClientIp();
                    }
                    if (string.IsNullOrEmpty(result))
                    {
                        result = GetLanIp();
                    }
                }
                catch { }
                return result;
            }
        }

        private static string GetWebClientIp()
        {
            try
            {
                string ip = GetWebRemoteIp();
                foreach (var hostAddress in Dns.GetHostAddresses(ip))
                {
                    if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return hostAddress.ToString();
                    }
                }
            }
            catch { }
            return string.Empty;
        }

        public static string GetLanIp()
        {
            try
            {
                foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return hostAddress.ToString();
                    }
                }
            }
            catch { }
            return string.Empty;
        }

        private static string GetWebRemoteIp()
        {
            try
            {
                string ip = HttpContext?.Connection?.RemoteIpAddress.ToString();
                if (HttpContext != null && HttpContext.Request != null)
                {
                    if (HttpContext.Request.Headers.ContainsKey("X-Real-IP"))
                    {
                        ip = HttpContext.Request.Headers["X-Real-IP"].ToString();
                    }

                    if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                    {
                        ip = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                    }
                }
                return ip;
            }
            catch { }
            return string.Empty;
        }
    }
}
