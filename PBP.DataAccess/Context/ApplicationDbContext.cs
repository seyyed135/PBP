using Microsoft.EntityFrameworkCore;
using PBP.DataAccess.Models;

namespace PBP.DataAccess.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contact { get; set; }
    public DbSet<Image> Image { get; set; }
    public DbSet<ContactChangeHistory> ContactChangeHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasIndex(c => c.PhoneNumber)
            .IsUnique();

        modelBuilder.Entity<Contact>()
            .Property(c => c.BirthDate)
            .HasColumnType("Date");

        modelBuilder.Entity<ContactChangeHistory>()
          .Property(c => c.ChangedDate)
          .HasColumnType("Date");
    }
}