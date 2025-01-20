using System.Text;
using System.Text.RegularExpressions;
using seda_dll.Models;
using seda_dll.Models.Enums;

namespace seda_bll.Helpers;

public static class IncidentDocumentParser
{
    private static readonly string _employeeNamePattern = @"Security incident response team member: [A-Z][A-Za-z]+ [A-Z][A-Za-z]+";
    private static readonly string _securityOrganizationNamePattern = @"Cybersecurity Company: [A-Z0-9][A-Za-z0-9]+ ?([A-Za-z0-9]+)*";
    private static readonly string _targetedOrganizationNamePattern = @"Targeted Organization: [A-Z0-9][A-Za-z0-9]+ ?([A-Za-z0-9]+)*";
    private static readonly string _targetedOrganizationAddressPattern = @"Targeted\sOrganization\sAddress: \d{1,5}\s[\w\s]+,\s[\w\s]+,\s[\w\s]+,\s[\w\s]+(?:\s[A-Z]{1,2}\d{1,2}\s\d[A-Z]{1,2}\d{1,2})?,\s([\w ]+)";
    private static readonly string _incidentLevelPattern = @"Incident severity level: [A-Z][a-z]+";
    
    public static IncidentDocument ParseContent(string content)
    {
        var documentInfo = new IncidentDocument();
        
        // Employee name
        var matches = Regex.Match(content, _employeeNamePattern);
        var matchedValue = matches.Value ?? " ";
        matchedValue = matchedValue.Replace("Security incident response team member: ", "");
        if (matchedValue.Split(" ").Length > 1)
        {
            documentInfo.EmployeeFirstName = matchedValue.Split(" ")[0];
            documentInfo.EmployeeLastName = matchedValue.Split(" ")[1];
        }
        
        // Security Org Name
        matches = Regex.Match(content, _securityOrganizationNamePattern);
        matchedValue = matches.Value ?? "";
        matchedValue = matchedValue.Replace("Cybersecurity Company:", "");
        documentInfo.SecurityOrganizationName = matchedValue;

        // Targeted Org Name
        matches = Regex.Match(content, _targetedOrganizationNamePattern);
        matchedValue = matches.Value ?? "";
        matchedValue = matchedValue.Replace("Targeted Organization:", "");
        documentInfo.TargetedOrganizationName = matchedValue;
        
        // Targeted Org Address
        matches = Regex.Match(content, _targetedOrganizationAddressPattern);
        matchedValue = matches.Value ?? "";
        matchedValue = matchedValue.Replace("Targeted Organization Address:", "");
        documentInfo.TargetedOrganizationAddress = matchedValue.Replace("\n", " ");
        
        // Incident severity level
        matches = Regex.Match(content, _incidentLevelPattern);
        matchedValue = matches.Value ?? "";
        matchedValue = matchedValue.Replace("Incident severity level: ", "");
        documentInfo.IncidentLevel = ConvertToIncidentLevelEnum(matchedValue);
        
        return documentInfo;
    }

    private static IncidentLevel ConvertToIncidentLevelEnum(string incidentLevel)
    {
        return incidentLevel.ToLower() switch
        {
            "low" => IncidentLevel.Low,
            "medium" => IncidentLevel.Medium,
            "high" => IncidentLevel.High,
            _ => IncidentLevel.Critical
        };
    }
}