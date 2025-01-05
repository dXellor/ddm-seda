using seda_dll.Models;

namespace seda_dll.Contracts;

public interface IUserRepository: ICrudRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}