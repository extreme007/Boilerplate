using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Web.Helpers;
using AspNetCoreHero.Boilerplate.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Controllers
{
    [AllowAnonymous]
    public class SitemapController : BaseController<SitemapController>
    {
        //[Route("sitemap")]
        [Route("sitemap.xml")]
        public async Task<ActionResult> SitemapAsync()
        {
            //var request = _httpContextAccessor.HttpContext.Request;
            string baseUrl = $"{Request.Scheme}://{Request.Host}/";

            var response = await _mediator.Send(new GetAllArticleCachedQuery());
            var listArtices = new List<ArticleViewModel>();
            if (response.Succeeded)
            {
                listArtices = _mapper.Map<List<ArticleViewModel>>(response.Data.Take(300));
            }

            var responseCategory = await _mediator.Send(new GetAllArticleCategoryCachedQuery());
            var listCategory = new List<NavigationViewModel>();
            if (responseCategory.Succeeded)
            {
                listCategory =  _mapper.Map<List<NavigationViewModel>>(responseCategory.Data.Where(x=>x.IsVisible ==true));
            }

            var siteMapBuilder = new SitemapBuilder();
            siteMapBuilder.AddUrl(baseUrl, modified: DateTime.Now, changeFrequency: ChangeFrequency.Daily, priority: 1.0);

            foreach(var category in listCategory)
            {
                siteMapBuilder.AddUrl(baseUrl + category.Slug, modified: DateTime.Now, changeFrequency: ChangeFrequency.Daily, priority: 0.9);
            }

            foreach (var article in listArtices)
            {
                siteMapBuilder.AddUrl(baseUrl + article.Slug, modified: article.PostedDatetime, changeFrequency: null, priority: 0.8);
            }

            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
        }


        [Route("robots.txt")]
        public ContentResult RobotsTxt()
        {
            var sb = new StringBuilder();
            sb.AppendLine("User-agent: *")
                .AppendLine("Allow: /")
                //.AppendLine("Disallow:")
                .Append("Sitemap: ")
                .Append(Request.Scheme)
                .Append("://")
                .Append(Request.Host)
                .AppendLine("/sitemap.xml");

            return Content(sb.ToString(), "text/plain", Encoding.UTF8);
        }
    }
}
