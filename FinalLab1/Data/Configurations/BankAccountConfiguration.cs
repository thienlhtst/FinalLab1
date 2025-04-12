using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinalLab1.Entities;

namespace FinalLab1.Data.Configurations
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccounts");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.NameOwner)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.NumberBank)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(b => b.Event)
                .WithMany()
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
