using PBP.DataAccess.Models;

namespace PBP.DataAccess.Repository;

public interface IContactRepository : IRepository<Contact>
{
    Task<Contact?> GetContactByIdWithImageAsync(int id);

    IQueryable<Contact> GetFilteredContactsWithImagesAndChangesHistory(string? searchName, string? searchPhone, DateTime? startDate, DateTime? endDate);

    void DeleteImage(Image image);

    IQueryable<ContactChangeHistory> GetFilteredChangesHistoryWithContactsAndImages(int? contactId, FieldName? feildName, DateTime? startDate, DateTime? endDate);

    Task AddChangeHistoryAsync(Contact contact);
}