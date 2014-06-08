namespace TodoTest.Web
{
    public static class Extensions
    {
        public static int GetPageSize(this int? pageSize)
        {
            return pageSize.HasValue ? pageSize.Value : 10;
        }

        public static int GetPage(this int? page)
        {
            return page.HasValue ? page.Value : 1;
        }
    }
}