using AplicationDomain.Layer___Bank_Api.Entities;
using AplicationDomain.Layer___Bank_Api.Interfacez;
using Infraestructure.Layer___Bank_Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Layer___Bank_Api.Repository
{
    public class BankAdminRepository : BaseCrudRepository<BankAccount>, IBankAdminRepository
    {
        public BankAdminRepository(BANKContext context) : base(context)
        {

        }

        public async Task<BankAccount> GetAdmin(BankAccountDTO account)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.BankUserAdmin == account.BankUserAdmin);
        }

        public async Task RegisterAdmin(BankAccount account)
        {
            _dbSet.Add(account);
            await _bANKContext.SaveChangesAsync();
        }


    }
}
