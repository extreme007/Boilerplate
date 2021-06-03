namespace AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys
{
    public static class ArticleCategoryCacheKeys
    {
        public static string ListKey => "ArticleCategoryList";

        public static string SelectListKey => "ArticleCategorySelectList";

        public static string GetKey(int articleCategoryId) => $"ArticleCategory-{articleCategoryId}";

        public static string GetDetailsKey(int articleCategoryId) => $"ArticleCategoryDetails-{articleCategoryId}";
    }
}