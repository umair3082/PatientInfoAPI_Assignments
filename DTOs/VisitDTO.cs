using PatientInfoAPI_Assignments.Entites;

namespace PatientInfoAPI_Assignments.DTOs
{
    public class VisitDTO
    {
        public DateTime VisitDateTime { get; set; }
        public Disease Disease { get; set; }
        public Patient Patient { get; set; }
        public Doctor ConsultingDoctorId { get; set; }
        public string? Notes { get; set; }
    }
    public class VisitCreatationDTO
    {
        public DateTime VisitDateTime { get; set; }
        public int DiseaseId { get; set; }
        public int PatientId { get; set; }
        public int ConsultingDoctorId { get; set; }
        public string? Notes { get; set; }
    }
    public class VisitSearchDTO
    {
        public string? PatientName { get; set; }
        public string? Age { get; set; }
        public string?  PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Disease { get; set; }
        public string? Doctor { get; set; }
    }
}
