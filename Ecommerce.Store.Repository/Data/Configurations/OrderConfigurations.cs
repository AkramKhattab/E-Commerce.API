using Ecommerce.Store.Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Repository.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");

            builder.Property(O => O.Status)
                   .HasConversion(OStatus => OStatus
                   .ToString()
                   , OStatus =>(OrderStatus) 
                   Enum.Parse(typeof(OrderStatus), OStatus));


            builder.OwnsOne(O => O.ShippingAddress, SA => SA.WithOwner());


            builder.HasOne(O => O.DeliveryMethod)
                   .WithMany()
                   .HasForeignKey(O => O.DeliveryMethodId);

        }
    }
}
