using PBP.DataAccess.Models;

namespace PBP.DataAccess.Repository;

public interface IContactRepository : IRepository<Contact>
{
    Task<Contact?> GetContactByIdWithImageAsync(int id);

    IQueryable<Contact> GetFilteredContactsWithImages(string? searchName, string? searchPhone, DateTime? startDate, DateTime? endDate);

    void DeleteImage(Image image);
}