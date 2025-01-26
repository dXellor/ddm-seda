namespace seda_dll_es.Models;

public class ESIncidentDocument
{
    public int Id { get; init; } 
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public string SecurityOrganizationName { get; set; }
    public string TargetedOrganizationName { get; set; }
    public IncidentLevel IncidentLevel { get; set; }
    public string TargetedOrganizationAddress { get; set; }
    public double TargetedOrganizationLatitude { get; set; }
    public double TargetedOrganizationLongitude { get; set; }
}

public enum IncidentLevel
{
    Low,
    Medium,
    High,
    Critical
}