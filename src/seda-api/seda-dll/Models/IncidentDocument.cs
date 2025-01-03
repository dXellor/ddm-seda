using seda_dll.Models.Enums;

namespace seda_dll.Models;

public class IncidentDocument
{
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public string SecurityOrganizationName { get; set; }
    public string TargetedOrganizationName { get; set; }
    public IncidentLevel IncidentLevel { get; set; }
    public string TargetedOrganizationAddress { get; set; }
    public string TargetedOrganizationCity { get; set; }
}