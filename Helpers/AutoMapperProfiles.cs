using AutoMapper;
using PatientInfoAPI_Assignments.DTOs;
using PatientInfoAPI_Assignments.Entites;

namespace PatientInfoAPI_Assignments.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PatientCreateionDTO, Patient>();
            CreateMap<DiseaseCreationDTO, Disease>();
            CreateMap<DiseaseDTO, Disease>().ReverseMap();

            CreateMap<DoctorCreationDTO, Doctor>();
            CreateMap<DoctorDTO, Doctor>().ReverseMap();

            CreateMap<Visit, VisitDTO>().ForMember(dest=>dest.Disease,opt=>opt.MapFrom(x=>x.DiseaseNavigation))
                .ForMember(dest => dest.Patient, opt => opt.MapFrom(x => x.Patient))
                .ForMember(dest => dest.ConsultingDoctorId, opt => opt.MapFrom(x => x.ConsultingDoctor))
                ;
            CreateMap<VisitCreatationDTO, Visit>();

            CreateMap<PatientDTO, Patient>().ReverseMap();
            CreateMap<PatientCreateionDTO, Patient>();
                //.ForMember(dest => dest.Visits,
                //           opt => opt.MapFrom(src => src.Visits));
            CreateMap<Patient, PatientUpdateDTO>();

        }
    }
}
