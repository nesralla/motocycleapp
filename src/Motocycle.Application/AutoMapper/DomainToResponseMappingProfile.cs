using AutoMapper;
using Motocycle.Application.UseCases.ApiErrorLog.Response;
using Motocycle.Application.UseCases.Delivery.Response;
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
            CreateMap<Deliveryman, DeliverymanResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.NationalID))
                .ForMember(dest => dest.Data_Nascimento, opt => opt.MapFrom(src => src.DateBorn))
                .ForMember(dest => dest.Numero_Cnh, opt => opt.MapFrom(src => src.DriveLicense))
                .ForMember(dest => dest.Tipo_Cnh, opt => opt.MapFrom(src => src.LicenseType))
                 .ForMember(dest => dest.Imagem_Cnh, opt => opt.MapFrom(src => src.DriveLicenseFile));

        }
    }
}