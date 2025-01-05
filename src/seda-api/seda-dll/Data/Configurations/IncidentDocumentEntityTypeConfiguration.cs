using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using seda_dll.Models;
using seda_dll.Models.Enums;

namespace seda_dll.Data.Configurations;

public class IncidentDocumentEntityTypeConfiguration: IEntityTypeConfiguration<IncidentDocument>
{
    public void Configure(EntityTypeBuilder<IncidentDocument> builder)
    {
        builder.ToTable("incident_documents");
        builder.HasKey(d => d.Id);
        
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(x => x.IncidentLevel).HasColumnName("incident_level").HasConversion(new EnumToStringConverter<IncidentLevel>()).IsRequired();
        builder.Property(x => x.EmployeeFirstName).HasColumnName("employee_first_name").HasMaxLength(255).IsRequired();
        builder.Property(x => x.EmployeeLastName).HasColumnName("employee_last_name").HasMaxLength(255).IsRequired();
        builder.Property(x => x.SecurityOrganizationName).HasColumnName("security_name").HasMaxLength(255).IsRequired();
        builder.Property(x => x.TargetedOrganizationName).HasColumnName("targeted_name").IsRequired();
        builder.Property(x => x.TargetedOrganizationAddress).HasColumnName("targeted_address");
        builder.Property(x => x.TargetedOrganizationLatitude).HasColumnName("targeted_longitude");
        builder.Property(x => x.TargetedOrganizationLongitude).HasColumnName("targeted_latitude");
        builder.Property(x => x.FileSystemId).HasColumnName("file_system_id").IsRequired();
    }
}