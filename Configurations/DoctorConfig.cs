using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientInfoAPI_Assignments.Entites;

namespace PatientInfoAPI_Assignments.Configurations
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.DoctorId);
            builder.Property(d => d.DoctorName).IsRequired().HasMaxLength(100);
        }
    }
}
