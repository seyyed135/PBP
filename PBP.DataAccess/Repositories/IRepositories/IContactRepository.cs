using PBP.DataAccess.Models;

namespace PBP.DataAccess.Repositories;

public interface IContactRepository : IRepository<Contact>
{
    Task<Contact?> GetContactByIdWithImageAsync(int id);

    Task<IEnumerable<Contact>> GetAllContactsAndImagesAsync();

    void DeleteImage(Image image);

    IQueryable<Contact> GetFilteredContactsWithImagesAndChangesHistory(string? searchName, string? searchPhone, DateTime? startDate, DateTime? endDate);

    IQueryable<ContactChangeHistory> GetFilteredChangesHistoryWithContactsAndImages(int? contactId, FieldName? fieldName, string? content, DateTime? startDate, DateTime? endDate, string? startTime, string? endTime);

    Task AddChangeHistoryAsync(Contact contact);
}