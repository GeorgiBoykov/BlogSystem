namespace BlogSystem.Web.Utilities
{
    using System.Linq;
    using System.Web;

    using HtmlAgilityPack;

    public static class WebExtensions
    {
        public static string GetUserIp(HttpRequest request)
        {
            var ipList = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0].Split(':')[0];
            }

            return request.ServerVariables["REMOTE_ADDR"].Split(':')[0];
        }

        public static string TruncateHtml(string input, int lenght)
        {
            var truncated = input.Substring(0, lenght);
            var document = new HtmlDocument();
            document.LoadHtml(truncated);

            var firstImage = document.DocumentNode.Descendants("img").FirstOrDefault();

            var formatedHtml = string.Format(
                "{0}</br>{1}...",
                firstImage != null ? firstImage.OuterHtml : string.Empty,
                document.DocumentNode.InnerText);

            return formatedHtml;
        }
    }
}