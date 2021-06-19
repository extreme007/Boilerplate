namespace AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys
{
    public static class ArticleCacheKeys
    {
        public static string ListKey => "ArtilceList";

        public static string SelectListKey => "ArticleSelectList";

        public static string GetKey(int articleId) => $"Article-{articleId}";

        public static string GetDetailsKey(int articleId) => $"ArticleDetails-{articleId}";
        public static string ListKeyByGroupCategoryId(int groupCategoryId, int categoryId) => $"ArtilceList-G{groupCategoryId}-C{categoryId}";
    }
}