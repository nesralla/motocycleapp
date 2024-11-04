using AutoMapper;
using Motocycle.Application.UseCases.ApiErrorLog.Request;
using Motocycle.Application.UseCases.Delivery.Request;
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
                .ForMember(dest => dest.Identification, opt => opt.MapFrom(src => src.Identificador))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.Placa))
                .ForMember(dest => dest.MotocyModel, opt => opt.MapFrom(src => src.Modelo))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Ano));

            CreateMap<GetMotocycleByPlateRequest, Motocy>()
             .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.Placa));

            CreateMap<UpdateMotocycleLicensePlateRequest, Motocy>()
            .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.Placa))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<GetMotosRequest, Motocy>();
            CreateMap<DeliverymanRequest, Deliveryman>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.NationalID, opt => opt.MapFrom(src => src.Cnpj))
            .ForMember(dest => dest.DateBorn, opt => opt.MapFrom(src => src.Data_Nascimento))
            .ForMember(dest => dest.DriveLicense, opt => opt.MapFrom(src => src.Numero_Cnh))
            .ForMember(dest => dest.LicenseType, opt => opt.MapFrom(src => src.Tipo_Cnh));


        }
    }
}