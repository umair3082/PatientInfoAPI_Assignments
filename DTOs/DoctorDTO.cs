namespace PatientInfoAPI_Assignments.DTOs
{
    public class DoctorDTO
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Spaciality { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
    public class DoctorCreationDTO
    {
        public string DoctorName { get; set; }
        public string Spaciality { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
    public class DoctorSearchDTO 
    {
        public string DoctorName { get; set; }
    }
}
