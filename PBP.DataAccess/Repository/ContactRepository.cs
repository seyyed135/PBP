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

    public IQueryable<Contact> GetFilteredContactsWithImagesAndChangesHistory(string? searchName, string? searchPhone, DateTime? startDate, DateTime? endDate)
    {
        var query = _context.Set<Contact>()
                                .Include(c => c.Image)
                                .Include(c => c.ChangesHistory)
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

    public IQueryable<ContactChangeHistory> GetFilteredChangesHistoryWithContactsAndImages(int? contactId, FieldName? fieldName, DateTime? startDate, DateTime? endDate)
    {
        var query = _context.Set<ContactChangeHistory>()
                                .Include(c => c.Contact)
                                .AsQueryable();

        if (contactId != null)
            query = query.Where(ch => ch.ContactId == contactId);

        if (fieldName.HasValue)
            query = query.Where(ch => ch.FieldName == fieldName);

        if (startDate.HasValue)
            query = query.Where(ch => ch.ChangedDate >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(ch => ch.ChangedDate <= endDate.Value);

        return query.OrderByDescending(ch => ch.ChangedDate);
    }

    public async Task AddChangeHistoryAsync(Contact contact)
    {
        var existingContact = await _context.Set<Contact>()
                                            .Include(c => c.Image)
                                            .AsNoTracking()
                                            .SingleOrDefaultAsync(c => c.Id == contact.Id);

        if (existingContact == null) return;

        var changes = new List<ContactChangeHistory>();

        CheckAndAddChange(changes, contact.Id, FieldName.Name, existingContact.Name, contact.Name);
        CheckAndAddChange(changes, contact.Id, FieldName.PhoneNumber, existingContact.PhoneNumber, contact.PhoneNumber);
        CheckAndAddChange(changes, contact.Id, FieldName.BirthDate, existingContact.BirthDate?.ToString(), contact.BirthDate?.ToString());
        CheckAndAddChange(changes, contact.Id, FieldName.Image, null, null, existingContact.Image?.Data, contact.Image?.Data);

        if (changes.Any())
        {
            await _context.Set<ContactChangeHistory>().AddRangeAsync(changes);
            foreach (var item in changes)
            {
                contact.ChangesHistory?.Add(item);
            }
            await _context.SaveChangesAsync();
        }
    }

    private void CheckAndAddChange(List<ContactChangeHistory> changes, int contactId, FieldName fieldName, string? oldValue, string? newValue, byte[]? oldImage = null, byte[]? newImage = null)
    {
        if (fieldName == FieldName.Image)
        {
            if (oldImage != null && newImage != null && !oldImage.SequenceEqual(newImage))
            {
                changes.Add(new ContactChangeHistory
                {
                    ContactId = contactId,
                    FieldName = fieldName,
                    OldImage = oldImage,
                    NewImage = newImage,
                    ChangedDate = DateTime.UtcNow.Date,
                    ChangedTime = DateTime.UtcNow.ToString("HH:mm")
                });
            }
        }
        else if (oldValue != newValue)
        {
            changes.Add(new ContactChangeHistory
            {
                ContactId = contactId,
                FieldName = fieldName,
                OldValue = oldValue,
                NewValue = newValue,
                ChangedDate = DateTime.UtcNow.Date,
                ChangedTime = DateTime.UtcNow.ToString("HH:mm")
            });
        }
    }
}