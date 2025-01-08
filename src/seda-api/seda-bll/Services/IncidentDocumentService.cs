using AutoMapper;
using Microsoft.Extensions.Logging;
using seda_bll.Contracts;
using seda_bll.Dtos.IncidentDocuments;
using seda_bll.Helpers;
using seda_dll.Contracts;
using seda_dll.Models;
using seda_dll.Models.Enums;
using UglyToad.PdfPig;

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
        var parsedDocument = ParseIncidentDocumentInfoFromPdf(fileName, documentStream);
        
        // Upload to the MinIO 
        try
        {
            documentStream.Position = 0;
            await _fileManagementService.UploadFile(fileName, contentType, documentStream);
        }
        catch (Exception e)
        {
            _logger.LogError($"Unable to upload file to the MinIO storage: {e.StackTrace}");
            throw new ArgumentException("Document upload failed");
        }
        
        // Create record in the database
        
        // Return result

        return _mapper.Map<IncidentDocument, IncidentDocumentInfoDto>(parsedDocument);
    }

    private IncidentDocument ParseIncidentDocumentInfoFromPdf(string fileName, Stream documentStream)
    {
        var titlePageContent = ReadContentFromPdfTitlePage(documentStream);
        var parsedDocumentInfo = IncidentDocumentParser.ParseContent(titlePageContent);
        parsedDocumentInfo.FileSystemId = fileName;
        return parsedDocumentInfo;
    }

    private string ReadContentFromPdfTitlePage(Stream pdfStream)
    {
        // var pdfReader = new PdfReader(pdfStream);
        // var pdf = new PdfDocument(pdfReader);
        //
        // ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
        // var pageContent = PdfTextExtractor.GetTextFromPage(pdf.GetPage(1), strategy);
        //
        // pdf.Close();
        // pdfReader.Close();
        //
        // return pageContent;
        
        using (var document = PdfDocument.Open(pdfStream))
        {
            // String to accumulate extracted text
            var text = new System.Text.StringBuilder();

            // Loop through each page and extract text
            var page = document.GetPage(1);
            var pageText = page.Text;

            // Append the text of the page
            text.AppendLine(pageText);

            // Output the text with preserved spaces
            return text.ToString();
        }
    }
}