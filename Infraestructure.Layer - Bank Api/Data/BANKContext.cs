using AplicationDomain.Layer___Bank_Api.Entities;
using Infraestructure.Layer___Bank_Api.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Layer___Bank_Api.Data
{
    public partial class BANKContext : DbContext
    {
        public BANKContext()
        {
        }

        public BANKContext(DbContextOptions<BANKContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientAccount> ClientAccounts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankAccountsConfigurations());
            modelBuilder.ApplyConfiguration(new ClientAccountsConfigurations());
            modelBuilder.ApplyConfiguration(new ClientsConfigurations());
        }
    }
}
