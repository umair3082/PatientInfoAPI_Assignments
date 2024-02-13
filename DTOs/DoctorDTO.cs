namespace PatientInfoAPI_Assignments.DTOs
{
    public class DoctorDTO
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
    public class DoctorCreationDTO
    {
        public string DoctorName { get; set; }
    }
    public class DoctorSearchDTO 
    {
        public string DoctorName { get; set; }
    }
}
