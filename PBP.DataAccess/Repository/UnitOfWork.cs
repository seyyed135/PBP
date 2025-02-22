using PBP.DataAccess.Context;

namespace PBP.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        ContactRepository = new ContactRepository(_context);
    }

    public IContactRepository ContactRepository { get; private set; }
}