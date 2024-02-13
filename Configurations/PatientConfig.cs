using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientInfoAPI_Assignments.Entites;

namespace PatientInfoAPI_Assignments.Configurations
{
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.PatientId);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Age).IsRequired();
            builder.Property(p => p.Gender).IsRequired().HasMaxLength(10);
            builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(p => p.Address).IsRequired().HasMaxLength(255);
        }
    }
}
