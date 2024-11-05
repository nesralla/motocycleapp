using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Motocycle.Api.Controllers.Base;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Application.Commons.Responses;
using Motocycle.Application.UseCases.Moto.Request;
using Motocycle.Infra.CrossCutting.Commons.Extensions;
using Motocycle.Application.UseCases.Delivery.Response;
using Motocycle.Application.UseCases.Delivery.Request;

namespace Motocycle.Api.Controllers
{
    [AllowAnonymous]
    public class EntregadoresController : ApiController
    {
        private readonly IMediator _mediator;

        public EntregadoresController(
            IHandler<DomainNotification> notifications,
            IMediator mediator) : base(notifications, mediator)
        {

            _mediator = mediator;
        }

        /// <summary>
        /// Create Moto
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, null, typeof(MotoResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]

        public async Task<ActionResult<DeliverymanResponse>> CreateMotocycleAsync(DeliverymanRequest request)
        {
            Notifications.LogInfo($"[{nameof(MotosController)}] [{nameof(CreateMotocycleAsync)}] - request: {request.ToJson()}");

            var result = await _mediator.Send(request);
            return ResponsePost("CreateMotocycleAsync", result.Id, result);
        }


    }
}