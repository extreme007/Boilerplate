namespace AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys
{
    public static class PartnerCacheKeys
    {
        public static string ListKey => "PartnerList";

        public static string SelectListKey => "PartnerSelectList";

        public static string GetKey(int PartnerId) => $"Partner-{PartnerId}";

        public static string GetDetailsKey(int PartnerId) => $"PartnerDetails-{PartnerId}";
    }
}