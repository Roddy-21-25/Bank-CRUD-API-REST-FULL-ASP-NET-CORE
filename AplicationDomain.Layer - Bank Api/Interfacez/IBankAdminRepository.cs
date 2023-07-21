using AplicationDomain.Layer___Bank_Api.Entities;

namespace AplicationDomain.Layer___Bank_Api.Interfacez
{
    public interface IBankAdminRepository : ICrudRepository<BankAccount>
    {
        Task<BankAccount> GetAdmin(BankAccountDTO account);

        Task RegisterAdmin(BankAccount account);
    }
}