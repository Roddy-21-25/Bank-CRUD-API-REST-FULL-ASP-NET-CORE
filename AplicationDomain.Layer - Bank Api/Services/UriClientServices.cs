using AplicationDomain.Layer___Bank_Api.Entities;
using AplicationDomain.Layer___Bank_Api.Interfacez;

namespace AplicationDomain.Layer___Bank_Api.Services
{
    public class UriClientServices : IUriClientServices
    {
        private readonly string _BaseUri;

        public UriClientServices(string BaseUri)
        {
            _BaseUri = BaseUri;
        }

        public Uri GetClientPaginationUri(PaginationValues values, string actionUri)
        {
            string baseUrl = $"{_BaseUri}{actionUri}";
            return new Uri(baseUrl);
        }
    }
}
