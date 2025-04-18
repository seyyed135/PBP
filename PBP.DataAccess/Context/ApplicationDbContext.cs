using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PBP.DataAccess.Models;

namespace PBP.DataAccess.Context;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contact { get; set; }
    public DbSet<Image> Image { get; set; }
    public DbSet<ContactChangeHistory> ContactChangeHistory { get; set; }
    public DbSet<ActivityLog> ActivityLog { get; set; }
    public DbSet<DynamicListItem> DynamicListItem { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(ul => ul.UserId);
        });

        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.HasKey(ur => new { ur.UserId, ur.RoleId });
        });

        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
        });

        modelBuilder.Entity<Contact>()
            .HasIndex(c => c.PhoneNumber)
            .IsUnique();

        modelBuilder.Entity<Contact>()
            .Property(c => c.BirthDate)
            .HasColumnType("Date");

        modelBuilder.Entity<ContactChangeHistory>()
            .Property(c => c.ChangedDate)
            .HasColumnType("Date");

        modelBuilder.Entity<ContactChangeHistory>()
            .HasIndex(c => c.ContactId);

        modelBuilder.Entity<ContactChangeHistory>()
            .HasIndex(c => c.FieldName);

        modelBuilder.Entity<ContactChangeHistory>()
            .HasIndex(c => c.ChangedDate);

        modelBuilder.Entity<ContactChangeHistory>()
            .HasIndex(c => c.ChangedTime);

        modelBuilder.Entity<DynamicListItem>()
            .HasIndex(c => c.Category);
    }
}