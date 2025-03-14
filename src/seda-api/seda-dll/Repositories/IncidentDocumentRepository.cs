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

    public async Task<IncidentDocument> UpdateAsync(IncidentDocument updatedObject)
    {
        var selectedDocument = _documents.FirstOrDefault( x => x.Id == updatedObject.Id );
        if( selectedDocument is null )
            throw new KeyNotFoundException("Document not found");
        
        selectedDocument.EmployeeFirstName = updatedObject.EmployeeFirstName;
        selectedDocument.EmployeeLastName = updatedObject.EmployeeLastName;
        selectedDocument.IncidentLevel = updatedObject.IncidentLevel;
        selectedDocument.SecurityOrganizationName = updatedObject.SecurityOrganizationName;
        selectedDocument.TargetedOrganizationAddress = updatedObject.TargetedOrganizationAddress;
        selectedDocument.TargetedOrganizationName = updatedObject.TargetedOrganizationName;
        selectedDocument.TargetedOrganizationLatitude = updatedObject.TargetedOrganizationLatitude;
        selectedDocument.TargetedOrganizationLongitude = updatedObject.TargetedOrganizationLongitude;
        
        await _dataContext.SaveChangesAsync().ConfigureAwait( false );

        return selectedDocument;
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}