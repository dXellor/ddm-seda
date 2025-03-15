using AutoMapper;
using seda_bll.Dtos.IncidentDocuments;
using seda_dll_es.Models;
using seda_dll.Models;

namespace seda_bll.MapperProfiles;

public class IncidentDocumentProfile: Profile
{
    public IncidentDocumentProfile()
    {
        CreateMap<IncidentDocument, IncidentDocumentInfoDto>().ReverseMap();
        CreateMap<IncidentDocument, ESIncidentDocument>().ReverseMap();
        CreateMap<DocumentQueryParameters, ESIncidentDocument>().ReverseMap();
        CreateMap<ESIncidentDocument, IncidentDocumentInfoDto>().ReverseMap();
    }
}