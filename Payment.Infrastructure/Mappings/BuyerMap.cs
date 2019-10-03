using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Infrastructure.Mappings
{
    public class BuyerMap : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Name.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Document.Number).IsRequired().HasMaxLength(14);
            builder.Property(x => x.Email.Address).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DeliveryAddress.ZipCode).IsRequired().HasMaxLength(60);
        }
    }
}
