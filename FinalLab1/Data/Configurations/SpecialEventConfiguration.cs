using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinalLab1.Entities;

namespace FinalLab1.Data.Configurations
{
    public class SpecialEventConfiguration : IEntityTypeConfiguration<SpecialEvent>
    {
        public void Configure(EntityTypeBuilder<SpecialEvent> builder)
        {
            builder.ToTable("SpecialEvents");
            builder.HasKey(e => e.Id);
       

            builder.HasOne(se => se.Event)
                .WithMany(e => e.SpecialEvent)
                .HasForeignKey(se => se.EventId);
            // TODO: Add more property/relationship configs here
        }
    }
}
