using AplicationDomain.Layer___Bank_Api.Entities;

namespace AplicationDomain.Layer___Bank_Api.Interfacez
{
    public interface IClientServices
    {
        Task<bool> Delete(int id);
        Task<PaginationLogic<Client>> GetAll(PaginationValues value);
        Task<Client> GetById(int id);
        Task Insert(Client entity);
        Task Update(Client entity);
        string HashPassword(string password);
    }
}