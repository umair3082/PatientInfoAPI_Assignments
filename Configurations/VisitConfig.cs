using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientInfoAPI_Assignments.Entites;

namespace PatientInfoAPI_Assignments.Configurations
{
    public class VisitConfig : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.HasKey(v => v.VisitId);
            builder.Property(v => v.VisitDateTime).IsRequired();
            builder.Property(v => v.DiseaseId).IsRequired().HasMaxLength(255);

            builder.HasOne(v => v.Patient)
                .WithMany()
                .HasForeignKey(v => v.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.ConsultingDoctor)
                .WithMany()
                .HasForeignKey(v => v.ConsultingDoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.DiseaseNavigation)
                .WithMany()
                .HasForeignKey(v => v.DiseaseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
