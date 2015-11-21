namespace BlogSystem.Web.Utilities
{
    using System.Web;

    public static class WebExtensions
    {

        public static string GetUserIp(HttpRequest request)
        {
            string ipList = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0].Split(':')[0];
            }

            return request.ServerVariables["REMOTE_ADDR"].Split(':')[0];
        }
    }
}