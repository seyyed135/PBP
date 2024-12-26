using Microsoft.EntityFrameworkCore;
using PBP.Models;

namespace PBP.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contact { get; set; }
    public DbSet<Image> Image { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasIndex(c => c.PhoneNumber)
            .IsUnique();

        modelBuilder.Entity<Contact>()
            .Property(c => c.BirthDate)
            .HasColumnType("Date");
    }
}