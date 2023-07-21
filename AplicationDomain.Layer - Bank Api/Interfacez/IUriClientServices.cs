using AplicationDomain.Layer___Bank_Api.Entities;

namespace AplicationDomain.Layer___Bank_Api.Interfacez
{
    public interface IUriClientServices
    {
        Uri GetClientPaginationUri(PaginationValues values, string actionUri);
    }
}