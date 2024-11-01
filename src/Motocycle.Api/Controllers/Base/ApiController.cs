using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;

namespace Motocycle.Api.Controllers.Base
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("/api/v1/[controller]")]
    public class ApiController : MainController
    {
        public ApiController(IHandler<DomainNotification> notifications, IMediator mediator) : base(notifications)
        {
        }
    }
}
