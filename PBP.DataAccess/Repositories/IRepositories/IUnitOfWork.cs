namespace PBP.DataAccess.Repositories;

public interface IUnitOfWork
{
    IContactRepository ContactRepository { get; }
}