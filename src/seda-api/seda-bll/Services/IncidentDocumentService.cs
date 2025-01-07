using AutoMapper;
using Microsoft.Extensions.Logging;
using seda_bll.Contracts;
using seda_bll.Dtos.IncidentDocuments;
using seda_dll.Contracts;
using seda_dll.Models;

namespace seda_bll.Services;

public class IncidentDocumentService: IIncidentDocumentService
{
    private readonly IIncidentDocumentRepository _documentRepository;
    private readonly IFileManagementService _fileManagementService;
    private readonly ILogger<IncidentDocumentService> _logger;
    private readonly IMapper _mapper;
    
    public IncidentDocumentService(IIncidentDocumentRepository documentRepository, ILogger<IncidentDocumentService> logger, IMapper mapper, IFileManagementService fileManagementService)
    {
        _documentRepository = documentRepository;
        _logger = logger;
        _mapper = mapper;
        _fileManagementService = fileManagementService;
    }
    
    public async Task<IEnumerable<IncidentDocumentInfoDto>> GetAllAsync()
    {
        var result = await _documentRepository.GetAllAsync();
        return result.Select(r => _mapper.Map<IncidentDocument, IncidentDocumentInfoDto>(r));
    }

    public async Task<IncidentDocumentInfoDto?> GetByIdAsync(int id)
    {
        var result = await _documentRepository.GetByIdAsync(id);
        return  _mapper.Map<IncidentDocument, IncidentDocumentInfoDto>(result);
    }

    public async Task<IncidentDocumentInfoDto> CreateAsync(IncidentDocumentInfoDto newObject)
    {
        // TODO: Save into MinIO file system
        
        var result = await _documentRepository.CreateAsync(_mapper.Map<IncidentDocumentInfoDto, IncidentDocument>(newObject));
        return _mapper.Map<IncidentDocument, IncidentDocumentInfoDto>(result);
    }

    public async Task<IncidentDocumentInfoDto> UpdateAsync(IncidentDocumentInfoDto updatedObject)
    {
        var existingDocument = await _documentRepository.GetByIdAsync(updatedObject.Id);

        if (existingDocument == null)
        {
            return null;
        }

        _mapper.Map(updatedObject, existingDocument);

        var updatedEntity = await _documentRepository.UpdateAsync(existingDocument);
        return _mapper.Map<IncidentDocument, IncidentDocumentInfoDto>(updatedEntity);
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IncidentDocumentInfoDto> HandleDocumentUpload(
        string fileName,
        string contentType,
        Stream documentStream)
    {
        // Parse contents of a PDF
        
        // Upload to the MinIO 
        try
        {
            await _fileManagementService.UploadFile( fileName, contentType, documentStream );
        }
        catch (Exception e)
        {
            _logger.LogError($"Unable to upload file to the MinIO storage: {e.StackTrace}");
            throw new ArgumentException("Document upload failed");
        }
        
        
        // Create record in the database
        
        // Return result

        return new IncidentDocumentInfoDto()
        {
            Id = 1,
            EmployeeFirstName = "LmaoPAo"
        };
    }
}