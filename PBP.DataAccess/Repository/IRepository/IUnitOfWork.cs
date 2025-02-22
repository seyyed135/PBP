namespace PBP.DataAccess.Repository;

public interface IUnitOfWork
{
    IContactRepository ContactRepository { get; }
}