using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using seda_dll.Contracts;
using seda_dll.Data;
using seda_dll.Models;

namespace seda_dll.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ILogger<UserRepository> _logger;
    private readonly DataContext _dataContext;
    private readonly DbSet<User> _users;
 
    public UserRepository(ILogger<UserRepository> logger, DataContext dataContext )
    {
        _logger = logger;
        _dataContext = dataContext;
        _users = dataContext.Users;
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _users.AsNoTracking().ToListAsync().ConfigureAwait( false );
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _users.AsNoTracking().FirstOrDefaultAsync( x => x.Id == id ).ConfigureAwait( false ); 
    }

    public async Task<User> CreateAsync(User newObject)
    {
        await _users.AddAsync( newObject ).ConfigureAwait( false );
        await _dataContext.SaveChangesAsync().ConfigureAwait( false );
    
        return newObject;
    }

    public async Task<User> UpdateAsync(User updatedObject)
    {
        var existingUser = await _users.FirstOrDefaultAsync(u => u.Email == updatedObject.Email);

        if (existingUser == null)
        {
            throw new KeyNotFoundException($"User with email {updatedObject.Email} not found.");
        }
        
        _dataContext.Users.Update(existingUser);

        await _dataContext.SaveChangesAsync();

        return updatedObject;
    }

    public async Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _users.AsNoTracking().FirstOrDefaultAsync( x => x.Email == email ).ConfigureAwait( false ); 
    }
}