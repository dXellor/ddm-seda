using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seda_bll.Contracts;

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
    
    
}

