using AutoMapper;
using Motocycle.Application.UseCases.ApiErrorLog.Request;
using Motocycle.Domain.Models;

namespace Motocycle.Application.AutoMapper
{
    public class RequestToDomainMappingProfile : Profile
    {
        public RequestToDomainMappingProfile()
        {

            CreateMap<ApiErrorLogRequest, ApiErrorLog>()
                .ForMember(dest => dest.RootCause, opt => opt.MapFrom(src => src.RootCause))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
        }
    }
}