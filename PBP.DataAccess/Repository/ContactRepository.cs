using Microsoft.EntityFrameworkCore;
using PBP.DataAccess.Context;
using PBP.DataAccess.Models;

namespace PBP.DataAccess.Repository;

public class ContactRepository(ApplicationDbContext context) : Repository<Contact>(context), IContactRepository
{
    private readonly ApplicationDbContext _context = context;


    public async Task<Contact?> GetContactByIdWithImageAsync(int id) => await _context.Set<Contact>()
                                                                                    .Include(c => c.Image)
                                                                                    .FirstOrDefaultAsync(c => c.Id == id);

    public IQueryable<Contact> GetFilteredContactsWithImages(string? searchName, string? searchPhone, DateTime? startDate, DateTime? endDate)
    {
        var query = _context.Set<Contact>()
                                .Include(c => c.Image)
                                .AsQueryable();

        if (!string.IsNullOrEmpty(searchName))
            query = query.Where(c => c.Name.Contains(searchName.Trim()));

        if (!string.IsNullOrEmpty(searchPhone))
            query = query.Where(c => c.PhoneNumber.Contains(searchPhone.Trim()));

        if (startDate.HasValue)
            query = query.Where(c => c.BirthDate >= startDate.Value.Date);

        if (endDate.HasValue)
            query = query.Where(c => c.BirthDate <= endDate.Value.Date);

        return query;
    }

    public void DeleteImage(Image image) => _context.Remove(image);
}