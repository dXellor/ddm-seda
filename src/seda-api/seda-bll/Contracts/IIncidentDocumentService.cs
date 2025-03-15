using seda_bll.Dtos.IncidentDocuments;

namespace seda_bll.Contracts;

public interface IIncidentDocumentService: ICrudService<IncidentDocumentInfoDto>
{
    Task<IncidentDocumentInfoDto> HandleDocumentUpload(string fileName, string contentType, Stream documentStream);
    Task<IncidentDocumentInfoDto> UpdateAndIndexDocumentAsync(IncidentDocumentInfoDto documentInfoDto);
    Task<IEnumerable<IncidentDocumentInfoDto>> QueryIndexedDocumentsAsync(DocumentQueryParameters parameters, CancellationToken cancellationToken);
}