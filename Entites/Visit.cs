namespace PatientInfoAPI_Assignments.Entites
{
    public class Visit
    {
        public int VisitId { get; set; }
        public DateTime VisitDateTime { get; set; }
        public int DiseaseId { get; set; }
        public Disease? DiseaseNavigation { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int ConsultingDoctorId { get; set; }
        public Doctor? ConsultingDoctor { get; set; }

        public string? Notes { get; set; }
    }
}
