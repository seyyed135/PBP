using PBP.DataAccess.Models;

namespace PBP.DataAccess.Repositories;

public interface IContactRepository : IRepository<Contact>
{
    Task<Contact?> GetContactByIdWithImageAsync(int id);

    IQueryable<Contact> GetFilteredContactsWithImagesAndChangesHistory(string? searchName, string? searchPhone, DateTime? startDate, DateTime? endDate);

    void DeleteImage(Image image);

    IQueryable<ContactChangeHistory> GetFilteredChangesHistoryWithContactsAndImages(int? contactId, FieldName? fieldName, string? content, DateTime? startDate, DateTime? endDate, string? startTime, string? endTime);

    Task AddChangeHistoryAsync(Contact contact);
}