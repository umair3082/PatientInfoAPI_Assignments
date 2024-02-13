namespace PatientInfoAPI_Assignments.DTOs
{
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
    public class PatientUpdateDTO
    {
        public int PatientId { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
    public class PatientCreateionDTO
    {
        public string Name { get; set; }
        public int? Age { get; set; } = 0;
        public string? Gender { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
    }

    public class PatientSearchDTO
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
