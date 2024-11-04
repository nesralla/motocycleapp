using AutoMapper;
using Motocycle.Application.UseCases.ApiErrorLog.Response;
using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Domain.Models;

namespace Motocycle.Application.AutoMapper
{
    public class DomainToResponseMappingProfile : Profile
    {
        public DomainToResponseMappingProfile()
        {


            CreateMap<ErrorLog, ApiErrorLogResponse>()
                .ForMember(dest => dest.RootCause, opt => opt.MapFrom(src => src.RootCause))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

            CreateMap<Motocy, MotoResponse>()
                .ForMember(dest => dest.Identificador, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.Placa, opt => opt.MapFrom(src => src.LicensePlate))
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.MotocyModel))
                .ForMember(dest => dest.Ano, opt => opt.MapFrom(src => src.Year));

        }
    }
}