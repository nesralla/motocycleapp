using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.UseCases.Moto.Request
{
    public class GetMotocycleByIdRequest : CommandRequest<MotoResponse>
    {
        public Guid Id { get; set; }
    }
}
