using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinalLab1.Entities;

namespace FinalLab1.Data.Configurations
{
    public class EventSessionConfiguration : IEntityTypeConfiguration<EventSession>
    {
        public void Configure(EntityTypeBuilder<EventSession> builder)
        {
            builder.ToTable("EventSessions");
            builder.HasKey(e => e.Id);
            builder.Property(es => es.FromTime).IsRequired();
            builder.Property(es => es.ToTime).IsRequired();
            builder.HasOne(es => es.Event)
                .WithMany(e => e.EventSessions)
                .HasForeignKey(es => es.EventId);
            builder.HasOne(e=>e.Location)
                .WithMany(l=>l.EventSessions)
                .HasForeignKey(e=>e.LocationId);
            builder.HasMany(e=>e.Seats)
                .WithOne(se => se.EventSession)
                .HasForeignKey(se => se.EventSessionId);
            // TODO: Add more property/relationship configs here
        }
    }
}
