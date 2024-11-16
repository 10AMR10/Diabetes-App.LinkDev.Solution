using AutoMapper;
using DiabetesApp.API.Dtos;
using DiabetesApp.Core.Enitities;

namespace DiabetesApp.API.Helpers
{
	public class MappingProfile:Profile
	{
        public MappingProfile()
        {
            CreateMap<Hospitail, HospitalDto>().ReverseMap();
            CreateMap<Patient, PatientToReturnDto>()
               .ForMember(d => d.Hospital, o => o.MapFrom(s => s.Hospital.HospitalName ));
            CreateMap<PhysiologicalIndicators, PhysiologicalIndicatorToRetunrDto>();
            CreateMap<PatientDto, Patient>();
			CreateMap<PhysiologicalIndicatorsDto, PhysiologicalIndicators>();


		}
	}
}
