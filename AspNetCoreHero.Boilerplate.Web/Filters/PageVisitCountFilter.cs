using AspNetCoreHero.Boilerplate.Application.Features.Articles.Commands.Update;
using AspNetCoreHero.Boilerplate.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Filters
{
    public class PageVisitCountFilter: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            //var pageUrl = context.HttpContext.Request.Path;
            //var result = context.Result;
            var UrlId = string.IsNullOrEmpty(context.RouteData.Values["id"].ToString()) ? 0 : Convert.ToInt32(context.RouteData.Values["id"].ToString());
            //// Do something with Result.
            //if (filterContext.Canceled == true)
            //{
            //    // Action execution was short-circuited by another filter.
            //}

            //if (filterContext.Exception != null)
            //{
            //    // Exception thrown by action or action filter.
            //    // Set to null to handle the exception.
            //    filterContext.Exception = null;
            //}

            //check if the user opening the site for the first time 
            if (context.HttpContext.Session.Get<List<int>>("URLHistory") != null)
            {
                //The session variable exists. So the user has already visited this site and sessions is still alive. Check if this page is already visited by the user
                List<int> HistoryURLs = context.HttpContext.Session.Get<List<int>>("URLHistory");
                if (HistoryURLs.Exists((element => element == UrlId)))
                {
                    //If the user has already visited this page in this session, then we can ignore this visit. No need to update the counter.
                    context.HttpContext.Session.Set<int>("VisitedURL", 0);
                }
                else
                {
                    //if the user is visting this page for the first time in this session, then count this visit and also add this page to the list of visited pages(URLHistory variable)
                    HistoryURLs.Add(UrlId);
                    context.HttpContext.Session.Set<List<int>>("URLHistory", HistoryURLs);

                    //Make a note of the page Id to update the database later 
                    context.HttpContext.Session.Set<int>("VisitedURL", UrlId);
                }
            }
            else
            {
                //if there is no session variable already created, then the user is visiting this page for the first time in this session. Then create a session variable and take the count of the page Id
                List<int> HistoryURLs = new List<int>();
                HistoryURLs.Add(UrlId);
                context.HttpContext.Session.Set<List<int>>("URLHistory", HistoryURLs);
                context.HttpContext.Session.Set<int>("VisitedURL", UrlId);
            }
        }
    }
}
