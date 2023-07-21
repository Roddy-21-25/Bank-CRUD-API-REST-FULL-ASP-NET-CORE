using AplicationDomain.Layer___Bank_Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Layer___Bank_Api.Configurations
{
    public class ClientsConfigurations : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.Property(e => e.Id)
                .HasColumnName("ClientId");

            builder.Property(e => e.ClientAge)
                .HasColumnName("ClientAge");

            builder.Property(e => e.ClientEmail)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.ClientFullName)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.ClientNotification)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.ClientPassword)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
