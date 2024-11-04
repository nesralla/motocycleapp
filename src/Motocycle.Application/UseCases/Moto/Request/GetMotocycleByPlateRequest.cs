using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.UseCases.Moto.Request
{
    public class GetMotocycleByPlateRequest : CommandRequest<MotoResponse>
    {
        public string Placa { get; set; }
    }
}
