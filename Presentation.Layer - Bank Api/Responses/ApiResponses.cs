using AplicationDomain.Layer___Bank_Api.Entities;

namespace Presentation.Layer___Bank_Api.Responses
{
    public class ApiResponses<T>
    {
        public T? Response { get; set; }
        public Metadata metadata { get; set; }
        public ApiResponses(T response)
        {
            Response = response;
        }
    }
}
