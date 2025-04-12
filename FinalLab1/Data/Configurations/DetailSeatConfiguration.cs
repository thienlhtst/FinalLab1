using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinalLab1.Entities;

namespace FinalLab1.Data.Configurations
{
    public class DetailSeatConfiguration : IEntityTypeConfiguration<DetailSeat>
    {
        public void Configure(EntityTypeBuilder<DetailSeat> builder)
        {
            builder.ToTable("DetailSeats");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Section)
                .HasMaxLength(50);
            

            builder.HasMany(d => d.NumberSeats)
                .WithOne(ns => ns.DetailSeat)
                .HasForeignKey(ns => ns.DetailSeatId);
        }
    }
}
