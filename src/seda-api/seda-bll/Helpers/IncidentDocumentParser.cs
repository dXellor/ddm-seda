using System.Text;
using System.Text.RegularExpressions;
using seda_dll.Models;
using seda_dll.Models.Enums;

namespace seda_bll.Helpers;

public static class IncidentDocumentParser
{
    private static readonly string _employeeNamePattern = @"Securityincidentresponseteammember: [A-Z][A-Za-z]+ [A-Z][A-Za-z]+";
    private static readonly string _securityOrganizationNamePattern = @"Cybersecurity Company: [A-Z0-9][A-Za-z0-9]+ ([A-Za-z0-9]+)+";
    private static readonly string _targetedOrganizationNamePattern = @"Targeted Organization: [A-Z0-9][A-Za-z0-9]+ ([A-Za-z0-9]+)+";
    private static readonly string _targetedOrganizationAddressPattern = @"Targeted\sOrganization\sAddress:\d+\s[A-Za-z\s]+,\s[A-Za-z\s]+,\s[A-Za-z\s]+,\s[A-Za-z\s]+(?:\s[A-Za-z0-9]{1,2}\d\s[A-Za-z]{2})?(\n)?,\s[A-Za-z]+";
    private static readonly string _incidentLevelPattern = @"Incidentseveritylevel: [A-Z][a-z]+";
    
    public static IncidentDocument ParseContent(string content)
    {
        content = AddSpacesToSentence(content);
        var documentInfo = new IncidentDocument();
        
        // Employee name
        var matches = Regex.Match(content, _employeeNamePattern);
        var matchedValue = matches.Value ?? " ";
        matchedValue = matchedValue.Replace("Securityincidentresponseteammember: ", "");
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
        documentInfo.TargetedOrganizationAddress = matchedValue;
        
        // Incident severity level
        matches = Regex.Match(content, _incidentLevelPattern);
        matchedValue = matches.Value ?? "";
        matchedValue = matchedValue.Replace("Incidentseveritylevel: ", "");
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
    
    private static string AddSpacesToSentence(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }

}