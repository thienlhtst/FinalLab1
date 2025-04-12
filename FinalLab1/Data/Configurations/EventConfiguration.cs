using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinalLab1.Entities;

namespace FinalLab1.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(e => e.Organizers)
                .WithOne(e => e.Event)
                .HasForeignKey(e => e.EventId);
            
            builder.HasOne(e => e.Organizer)
                .WithMany(o => o.OrganizedEvents)
                .HasForeignKey(e =>e.Id);
            
          
    
            builder.HasMany(e => e.EventSessions)
                .WithOne(es => es.Event)
                .HasForeignKey(es => es.EventId);
            
            builder.HasMany(e => e.BankAccounts)
                .WithOne(es => es.Event)
                .HasForeignKey(es => es.EventId);
            
            builder.HasMany(e => e.Categories)
                .WithOne(ec => ec.Event)
                .HasForeignKey(ec => ec.EventId);
        }
    }
}
