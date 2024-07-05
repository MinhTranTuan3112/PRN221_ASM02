using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SalesRazorPageApp.Services.Interfaces;
using SalesRazorPageApp.Shared.Enums;

namespace SalesRazorPageApp.Attributes
{
    public class SessionAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public string[] Roles { get; set; } = [];

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var memberId = context.HttpContext.Session.GetString("memberId");
            var role = context.HttpContext.Session.GetString("role");

            if (string.IsNullOrEmpty(memberId) && role != Role.Admin.ToString())
            {
                // If the user is not logged in, redirect to the login page
                context.Result = new RedirectToPageResult("/Login");
                return;
            }

            if (role == Role.Admin.ToString() && (Roles.Contains(Role.Admin.ToString()) || Roles is []))
            {
                return;
            }

            var memberService = context.HttpContext.RequestServices.GetService<IMemberService>();
            if (memberService is null)
            {
                return;
            }


            var member = await memberService.GetMemberById(Convert.ToInt32(memberId));

            if (member is null)
            {
                context.Result = new RedirectToPageResult("/Login");
                return;
            }

            if (Roles is [])
            {
                return;
            }

        }
    }
}