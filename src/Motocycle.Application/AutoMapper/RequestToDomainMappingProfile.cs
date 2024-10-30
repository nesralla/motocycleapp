using AutoMapper;
using Motocycle.Application.UseCases.ApiErrorLog.Request;
using Motocycle.Application.UseCases.Moto.Request;
using Motocycle.Domain.Models;

namespace Motocycle.Application.AutoMapper
{
    public class RequestToDomainMappingProfile : Profile
    {
        public RequestToDomainMappingProfile()
        {

            CreateMap<ApiErrorLogRequest, ErrorLog>()
                .ForMember(dest => dest.RootCause, opt => opt.MapFrom(src => src.RootCause))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

            CreateMap<MotoRequest, Motocy>()
                .ForMember(dest => dest.Identification, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate))
                .ForMember(dest => dest.MotocyModel, opt => opt.MapFrom(src => src.MotocyModel))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year));

        }
    }
}