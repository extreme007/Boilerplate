using Abot2.Core;
using Abot2.Poco;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllPaged;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Boilerplate.Web.Abstractions;
using AspNetCoreHero.Boilerplate.Web.Helpers;
using AspNetCoreHero.Boilerplate.Web.Models;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    public class HomeController : BaseController<HomeController>
    {
        public IActionResult Index()
        {
            _notify.Information("Hi There!");
            return View();
        }

        //[NonAction]
        //public IActionResult CrawlData()
        //{
        //    var listUri = new List<string> { "https://m.baomoi.com/tin-moi.epi", "https://m.baomoi.com" };
        //    using (var connection = JobStorage.Current.GetConnection())
        //    {
        //        var recuring = connection.GetRecurringJobs().FirstOrDefault(x => x.Id == "Program.BackgroundJobCrawl");
        //        if(recuring == null)
        //        {
        //            RecurringJob.AddOrUpdate(() => Crawler(listUri), Cron.Hourly);
        //        }
        //    }
        //    return Ok();
        //}
        
        
        //private async Task Crawler(List<string> listUri)
        //{
        //    try
        //    {
        //        var countCrawled = 0;
        //        var responseArticle = await _mediator.Send(new GetAllArticlesNoCacheQuery());
        //        var dataArticleCache = responseArticle.Succeeded ? _mapper.Map<List<Article>>(responseArticle.Data) : new List<Article>();

        //        var responseArticleCategory = await _mediator.Send(new GetAllArticleCategoryCachedQuery());
        //        var dataCategory = responseArticleCategory.Succeeded ? _mapper.Map<List<NavigationViewModel>>(responseArticleCategory.Data) : new List<NavigationViewModel>();

        //        foreach (var Uri in listUri)
        //        {
        //            var isHot = !Uri.Contains("tin-moi");
        //            var crawledPage = await PageRequester(Uri);
        //            _logger.LogInformation("{crawledPage}", new { url = crawledPage.Uri, status = Convert.ToInt32(crawledPage.HttpResponseMessage?.StatusCode) });
        //            var aricleList = crawledPage.AngleSharpHtmlDocument.QuerySelector(".timeline").Children;
        //            if (aricleList != null)
        //            {
        //                foreach (var article in aricleList.Where(x => x.ClassName.Contains("rank1-stories") || x.ClassName.Contains("story")))
        //                {
        //                    AngleSharp.Dom.IElement elementData = article;
        //                    var isRank1 = article.ClassName.Contains("rank1-stories");
        //                    if (isRank1)
        //                    {
        //                        elementData = article.QuerySelector(".story");
        //                    }

        //                    var aid = elementData.GetAttribute("data-aid");

        //                    var storyElement = elementData.QuerySelector(".story__link");
        //                    var existed = dataArticleCache.Any(x => x.Aid == aid);
        //                    if (storyElement != null && !existed)
        //                    {
        //                        var storyMeta = storyElement.QuerySelector(".story__meta");

        //                        var title = storyElement.QuerySelector(".story__heading").TextContent.Trim();
        //                        var link = storyElement.GetAttribute("href");
        //                        var storyThumbElement = storyElement.QuerySelector(".story__thumb>img") ?? storyElement.QuerySelector(".story__thumb>.is-first>img");
        //                        string thumbImage = storyThumbElement == null ? null : storyThumbElement.GetAttribute("data-src") ?? storyThumbElement.GetAttribute("src");
        //                        var sourceImage = storyMeta.QuerySelector(".source-image")?.GetAttribute("src");
        //                        var sourceName = storyMeta.QuerySelector(".source-image")?.GetAttribute("alt");
        //                        var datetime = storyMeta.QuerySelector(".friendly").GetAttribute("datetime");
        //                        var description = storyElement.QuerySelector(".story__summary").TextContent.Trim();

        //                        string fullLink = string.Format("{0}{1}", "https://baomoi.com", link);

        //                        //Get Detail by link
        //                        var crawledPageDetail = await PageRequester(fullLink);
        //                        _logger.LogInformation("{crawledPageDetail}", new { url = crawledPageDetail.Uri, status = Convert.ToInt32(crawledPageDetail.HttpResponseMessage?.StatusCode) });
        //                        //Category

        //                        var angleSharpHtmlDocumentDetail = crawledPageDetail.AngleSharpHtmlDocument;
        //                        var breadcrumb = angleSharpHtmlDocumentDetail.QuerySelector(".breadcrumb").QuerySelectorAll(".item>a");
        //                        List<string> listBreadcrumb = new List<string>();
        //                        foreach (var item in breadcrumb)
        //                        {
        //                            listBreadcrumb.Add(item.TextContent.Trim());
        //                        }
        //                        var category = dataCategory.FirstOrDefault(x => x.Title == listBreadcrumb.Last()).Id;
        //                        var groupCategory = dataCategory.FirstOrDefault(x => x.Title == listBreadcrumb.First()).Id;

        //                        var newAricleDetail = angleSharpHtmlDocumentDetail.QuerySelector(".article");
        //                        if (newAricleDetail != null)
        //                        {
        //                            var type = string.Empty;
        //                            var typePhoto = newAricleDetail.ClassName.Contains("article--photo");
        //                            var typeVideo = newAricleDetail.ClassName.Contains("article--video");
        //                            if (typePhoto)
        //                            {
        //                                type = "Photo";
        //                            }
        //                            else if (typeVideo)
        //                            {
        //                                type = "Video";
        //                            }
        //                            else
        //                            {
        //                                type = "Text";
        //                            }
        //                            var fullDescription = newAricleDetail.QuerySelector(".article__sapo").TextContent.Trim();
        //                            var content = ReplaceContent(newAricleDetail.QuerySelector(".article__body").InnerHtml);
        //                            var image = newAricleDetail.QuerySelectorAll(".body-image>img").FirstOrDefault()?.GetAttribute("src");

        //                            var tagsElements = newAricleDetail.QuerySelector(".article__tag").QuerySelectorAll(".keyword");
        //                            var sourceLink = newAricleDetail.QuerySelector(".bm-source>a>.source").TextContent.Trim();
        //                            var authorNameElement = newAricleDetail.QuerySelectorAll(".body-author");
        //                            var author = authorNameElement != null && authorNameElement.Length > 0 ? authorNameElement[0].QuerySelector("strong").TextContent.Trim() : null;
        //                            var listTags = new List<string>();
        //                            foreach (var tag in tagsElements)
        //                            {
        //                                listTags.Add(tag.TextContent.Trim());
        //                            }

        //                            var articleCrawled = new Article
        //                            {
        //                                Aid = aid,
        //                                Title = title,
        //                                Slug = FriendlyUrlHelper.GenerateSlug(title),
        //                                Description = description,
        //                                ThumbImage = thumbImage,
        //                                Image = image,
        //                                Link = link,
        //                                FullLink = fullLink,
        //                                SourceImage = sourceImage,
        //                                SourceName = sourceName,
        //                                PostedDatetime = Convert.ToDateTime(datetime),
        //                                FullDescription = fullDescription,
        //                                Content = content,
        //                                SourceLink = sourceLink,
        //                                Author = author,
        //                                Tags = listTags.Count > 0 ? string.Join(',', listTags.ToArray()) : null,
        //                                Type = type,
        //                                CategoryId = category,
        //                                GroupCategoryId = groupCategory,
        //                                IsHot = isHot,
        //                                IsRank1 = isRank1,
        //                                ViewCount = 0,
        //                                CommentCount = 0,
        //                                CreatedBy = "0bdd8200-ff66-46f3-bfb0-78a43e1124cb",
        //                                CreatedOn = DateTime.Now,
        //                                IsPublished = typeVideo ? false : true
        //                            };

        //                            var dataSend = _mapper.Map<CreateArticleCommand>(articleCrawled);
        //                            var result = await _mediator.Send(dataSend);
        //                            if(result.Succeeded)
        //                            {
        //                                dataArticleCache.Add(articleCrawled);
        //                                countCrawled += 1;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        _logger.LogInformation(string.Format("-----------Total: {0}", countCrawled));

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("{crawledPage}", ex.Message);
        //    }

        //}

        private static string ReplaceContent(string content)
        {
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsNotConsumingCharacterReferences = true,
            });

            var document = parser.ParseDocument(content);
            var lazyImgae = document.QuerySelectorAll(".lazy-img");
            foreach (var item in lazyImgae)
            {
                var newElement = document.CreateElement("img");
                newElement.SetAttribute("class", item.ClassName);
                newElement.SetAttribute("src", item.GetAttribute("data-src"));
                newElement.SetAttribute("width", item.GetAttribute("width"));
                newElement.SetAttribute("height", item.GetAttribute("height"));
                item.Insert(AdjacentPosition.BeforeBegin, newElement.OuterHtml);
                item.Remove();
            }

            return document.QuerySelector("body").InnerHtml;
        }

        private static async Task<CrawledPage> PageRequester(string uri)
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 1,
                MinCrawlDelayPerDomainMilliSeconds = 3000
            };
            var pageRequester = new PageRequester(config, new WebContentExtractor());

            return await pageRequester.MakeRequestAsync(new Uri(uri));
        }
    }
}