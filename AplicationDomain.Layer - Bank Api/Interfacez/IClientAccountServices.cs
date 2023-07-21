using AplicationDomain.Layer___Bank_Api.Entities;

namespace AplicationDomain.Layer___Bank_Api.Interfacez
{
    public interface IClientAccountServices
    {
        Task<bool> Delete(int id);
        Task<PaginationLogic<ClientAccount>> GetAll(PaginationValues value);
        Task<ClientAccount> GetById(int id);
        Task Insert(ClientAccount entity);
        Task Update(ClientAccount entity);
    }
}