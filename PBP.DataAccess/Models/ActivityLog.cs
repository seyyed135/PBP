using Microsoft.AspNetCore.Identity;
using PBP.DataAccess.Context;
using System.ComponentModel.DataAnnotations;

namespace PBP.DataAccess.Models;

public class ActivityLog
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = null!;
    public IdentityUser<string> User { get; set; } = null!;

    [Required]
    public string Action { get; set; } = null!;

    [Required]
    public DateTime Timestamp { get; set; }
}



public class ActivityLogService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task LogActivityAsync(string userId, string action)
    {
        var log = new ActivityLog
        {
            UserId = userId,
            Action = action,
            Timestamp = DateTime.UtcNow,
        };

        await _context.ActivityLog.AddAsync(log);
        await _context.SaveChangesAsync();
    }
}