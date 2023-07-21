using AplicationDomain.Layer___Bank_Api.Entities;

namespace AplicationDomain.Layer___Bank_Api.Interfacez
{
    public interface ICrudRepository<T> where T : BaseIdEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Insert(T entity);
        Task Update(T entity);
        Task<bool> Delete(int id);
    }
}
