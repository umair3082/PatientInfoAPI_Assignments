using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientInfoAPI_Assignments.Entites;

namespace PatientInfoAPI_Assignments.Configurations
{
    public class DiseaseConfig: IEntityTypeConfiguration<Disease>
    {
        public void Configure(EntityTypeBuilder<Disease> builder)
        {
            builder.HasKey(d => d.DiseaseId);
            builder.Property(d => d.DiseaseName).IsRequired().HasMaxLength(255);
        }
    }
}
