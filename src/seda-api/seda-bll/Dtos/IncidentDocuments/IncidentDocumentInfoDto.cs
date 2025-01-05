using seda_dll.Models.Enums;

namespace seda_bll.Dtos.IncidentDocuments;

public class IncidentDocumentInfoDto
{
    public int Id { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public string SecurityOrganizationName { get; set; }
    public string TargetedOrganizationName { get; set; }
    public IncidentLevel IncidentLevel { get; set; }
    public string TargetedOrganizationAddress { get; set; }
    public double TargetedOrganizationLatitude { get; set; }
    public double TargetedOrganizationLongitude { get; set; }
}