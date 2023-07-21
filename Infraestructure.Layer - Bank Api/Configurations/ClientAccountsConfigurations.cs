using AplicationDomain.Layer___Bank_Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Layer___Bank_Api.Configurations
{
    public class ClientAccountsConfigurations : IEntityTypeConfiguration<ClientAccount>
    {
        public void Configure(EntityTypeBuilder<ClientAccount> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("PK__ClientAc__349DA5A686BFCC08");

            builder.ToTable("ClientAccount");

            builder.Property(e => e.Id)
                .HasColumnName("AccountId");

            builder.Property(e => e.AccountAmount)
                .HasColumnName("AccountAmount");

            builder.Property(e => e.AccountBadge)
                .HasColumnName("AccountBadge")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.AccountCardType)
                .HasColumnName("AccountCardType")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.AccountName)
                .HasColumnName("AccountName")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.AccountNotification)
                .HasColumnName("AccountNotification")
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.AccountPaymentHistory)
                .HasColumnName("AccountPaymentHistory")
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.AccountTransaccion)
                .HasColumnName("AccountTransaccion")
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.HasOne(d => d.ClientIdAccountNavigation)
                .WithMany(p => p.ClientAccounts)
                .HasForeignKey(d => d.ClientIdAccount)
                .HasConstraintName("FK_ClientAccount_Client");
        }
    }
}
