using FinalLab1.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalLab1.Data;

public class LabDbContext : DbContext
{
    public LabDbContext(DbContextOptions<LabDbContext> options)
        : base(options) { }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<DetailSeat> DetailSeats { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventCategory> EventCategories { get; set; }
    public DbSet<EventSession> EventSessions { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Method> Methods { get; set; }
    public DbSet<NumberSeat> NumberSeats { get; set; }
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<SpecialEvent> SpecialEvents { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tự động apply các file cấu hình Fluent API từ assembly hiện tại
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LabDbContext).Assembly);
        Dataseed.Seed(modelBuilder);
    }
}