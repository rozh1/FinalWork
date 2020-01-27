using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FinalWork_BD_Test.Data
{
    public class RegisterMiddleware
    {
        private readonly RequestDelegate _next;

        public RegisterMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext httpContext, ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            if (httpContext.Request.Path == PathString.FromUriComponent(
                    new Uri($"{httpContext.Request.Scheme}://{httpContext.Request.Host}" + "/Home/CompleteRegister")))
            {
                await _next(httpContext);
                return;
            }


            bool completed_register = false;

            if (httpContext.User.Identity.IsAuthenticated)
            {
                var currentUser = userManager.GetUserAsync(httpContext.User).Result;
                var studentProfile = dbContext.StudentProfiles.FirstOrDefault(t => t.User == currentUser);

                if (studentProfile != null)
                    completed_register = true;
            }

            if (!completed_register)
            {
                var location = new Uri($"{httpContext.Request.Scheme}://{httpContext.Request.Host}" + "/Home/CompleteRegister");
                httpContext.Response.Redirect(location.ToString());
            }
            await _next(httpContext);

        }
    }
}
