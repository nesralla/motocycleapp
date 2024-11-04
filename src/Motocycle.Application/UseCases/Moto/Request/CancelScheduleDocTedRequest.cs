using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Domain.Core.Messages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motocycle.Application.UseCases.Moto.Request
{
    public class RemoveMotocycleRequest : CommandRequest<MotoResponse>
    {
        public Guid Id { get; set; }
        public RemoveMotocycleRequest() { }
        public RemoveMotocycleRequest(Guid id)
        {
            Id = id;
        }
    }
}
