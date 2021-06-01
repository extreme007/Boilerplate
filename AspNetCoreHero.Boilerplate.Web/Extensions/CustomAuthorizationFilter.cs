using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Extensions
{
    public class CustomAuthorizationFilter : ActionFilterAttribute,IAuthorizationFilter
    {


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var user = context.HttpContext.User;
            ////if (true)
            ////{
            ////    context.Result = new RedirectToActionResult("Error", "Home", null);
            ////}

            //if (!user.Identity.IsAuthenticated)
            //{
            //    var controller = context.ActionDescriptor.RouteValues["Controller"];
            //    var action = context.ActionDescriptor.RouteValues["Action"];
            //    if(controller == null && action == null)
            //    {
            //        context.Result = new RedirectToActionResult("Error", "Home", null);
            //    }

            //}

        }
    }
}
