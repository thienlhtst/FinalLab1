using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinalLab1.Entities;

namespace FinalLab1.Configurations
{
    public class NumberSeatConfiguration : IEntityTypeConfiguration<NumberSeat>
    {
        public void Configure(EntityTypeBuilder<NumberSeat> builder)
        {
            builder.ToTable("NumberSeats");
            builder.HasKey(e => e.Id);
          

            builder.HasOne(ns => ns.DetailSeat)
                .WithMany(ds => ds.NumberSeats)
                .HasForeignKey(ns => ns.DetailSeatId);

            builder.HasOne(ns => ns.Ticket)
                .WithOne(t => t.NumberSeat)
                .HasForeignKey<Ticket>(t => t.NumberSeatId);
        }
    }
}
