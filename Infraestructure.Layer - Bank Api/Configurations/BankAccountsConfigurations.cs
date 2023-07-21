using AplicationDomain.Layer___Bank_Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Layer___Bank_Api.Configurations
{
    public class BankAccountsConfigurations : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("PK__BankAcco__AA08CB13E0CA8074");

            builder.ToTable("BankAccount");

            builder.Property(e => e.Id)
                .HasColumnName("BankId");

            builder.Property(e => e.BankAmount)
                .HasColumnName("BankAmount");

            builder.Property(e => e.BankPasswordAdmin)
                .HasColumnName("BankPasswordAdmin")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.BankUserAdmin)
                .HasColumnName("BankUserAdmin")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.HasOne(d => d.Account)
                .WithMany(p => p.BankAccounts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_BankAccount_ClientAccount");

            builder.HasOne(d => d.Client)
                .WithMany(p => p.BankAccounts)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_BankAccount_Client");
        }
    }
}
