namespace PatientInfoAPI_Assignments.DTOs
{
    public class DiseaseDTO
    {
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
    }
    public class DiseaseCreationDTO
    {
        public string DiseaseName { get; set; }
    }
    public class DiseaseSearchDTO
    {
        public string DiseaseName { get; set; }
    }
}
