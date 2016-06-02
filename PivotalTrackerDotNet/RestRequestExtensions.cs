using System.Globalization;

using RestSharp;

namespace PivotalTrackerDotNet
{
    public static class RestRequestExtensions
    {
        public static RestRequest SetPagination(this RestRequest request, int offset, int limit)
        {
            request.AddQueryParameter("offset",   offset.ToString(CultureInfo.InvariantCulture));
            request.AddQueryParameter("limit",    limit.ToString(CultureInfo.InvariantCulture));
            request.AddQueryParameter("envelope", "true");
            return request;
        }
    }
}