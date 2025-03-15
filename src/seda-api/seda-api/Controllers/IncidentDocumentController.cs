using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seda_bll.Contracts;
using seda_bll.Dtos.IncidentDocuments;

namespace seda_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncidentDocumentController : ControllerBase
{
    private readonly IIncidentDocumentService _documentService;
    private readonly ILogger<IncidentDocumentController> _logger;

    public IncidentDocumentController(IIncidentDocumentService documentService, ILogger<IncidentDocumentController> logger)
    {
        _documentService = documentService;
        _logger = logger;
    }
    
    [HttpPost("IndexDocument")]
    [ProducesResponseType(typeof(IncidentDocumentInfoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> IndexDocumentAsync( [FromBody] IncidentDocumentInfoDto documentInfoDto )
    {
        var result = await _documentService.UpdateAndIndexDocumentAsync(documentInfoDto);
        return Ok(result);
    }
    
    [HttpPost("UploadDocument")]
    [ProducesResponseType(typeof(IncidentDocumentInfoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadDocumentAsync()
    {
        if (!Request.Form.Files.Any())
            return BadRequest("No content uploaded");

        var fileToUpload = Request.Form.Files[0];
        var documentInfo = await _documentService.HandleDocumentUpload(fileToUpload.FileName, fileToUpload.ContentType, fileToUpload.OpenReadStream());
        return Ok(documentInfo);
    }
    
    [HttpPost("QueryIndex")]
    [ProducesResponseType(typeof(IncidentDocumentInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> QueryIndexAsync(
        [FromQuery] DocumentQueryParameters queryParameters,
        CancellationToken cancellationToken )
    {
        var results = await _documentService.QueryIndexedDocumentsAsync(queryParameters, cancellationToken);
        return Ok(results);
    }
}

