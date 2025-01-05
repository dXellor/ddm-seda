using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using seda_dll.Contracts;
using seda_dll.Data;
using seda_dll.Models;

namespace seda_dll.Repositories;

public class IncidentDocumentRepository: IIncidentDocumentRepository
{
    private readonly ILogger<IncidentDocumentRepository> _logger;
    private readonly DataContext _dataContext;
    private readonly DbSet<IncidentDocument> _documents;
    
    public IncidentDocumentRepository(ILogger<IncidentDocumentRepository> logger, DataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
        _documents = dataContext.Documents;
    }   
    
    public async Task<IEnumerable<IncidentDocument>> GetAllAsync()
    {
        return await _documents.AsNoTracking().ToListAsync().ConfigureAwait( false );
    }

    public async Task<IncidentDocument?> GetByIdAsync(int id)
    {
        return await _documents.AsNoTracking().FirstOrDefaultAsync( x => x.Id == id ).ConfigureAwait( false );
    }

    public async Task<IncidentDocument> CreateAsync(IncidentDocument newObject)
    {
        await _documents.AddAsync( newObject ).ConfigureAwait( false );
        await _dataContext.SaveChangesAsync().ConfigureAwait( false );
    
        return newObject;
    }

    public Task<IncidentDocument> UpdateAsync(IncidentDocument updatedObject)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}