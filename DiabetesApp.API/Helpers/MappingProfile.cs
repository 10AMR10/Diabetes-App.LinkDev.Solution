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
            CreateMap<PhysiologicalIndicatorsDto, PhysiologicalIndicators>()
            .ForMember(d => d.Date, o => o.MapFrom(s => DateOnly.Parse(s.Date)))
            .ForMember(d => d.Time, o => o.MapFrom(s => DateOnly.Parse(s.Time)));


		}
	}
}
