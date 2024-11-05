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
    public class LocacaoController : ApiController
    {
        private readonly IMediator _mediator;

        public LocacaoController(
            IHandler<DomainNotification> notifications,
            IMediator mediator) : base(notifications, mediator)
        {

            _mediator = mediator;
        }

        /// <summary>
        /// Create Rent
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, null, typeof(MotoResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]

        public async Task<ActionResult<DeliverymanResponse>> CreateRentAsync(DeliverymanRequest request)
        {
            Notifications.LogInfo($"[{nameof(LocacaoController)}] [{nameof(CreateRentAsync)}] - request: {request.ToJson()}");

            var result = await _mediator.Send(request);
            return ResponsePost("CreateMotocycleAsync", result.Id, result);
        }
        /// <summary>
        /// Get Rent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(MotoResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]
        public async Task<ActionResult<MotoResponse>> GetRentByIdAsync([FromRoute] string id)
        {

            Notifications.LogInfo($"[{nameof(LocacaoController)}] [{nameof(GetRentByIdAsync)}] - request: ");

            var result = await _mediator.Send(new GetMotocycleByIdRequest { Id = Guid.Parse(id) });

            return ResponseGet(result);
        }
        /// <summary>
        /// Return rent
        /// </summary>
        /// <param name="Id">Guid Id </param>

        /// <returns></returns>
        [HttpPut("{Id}/devolucao")]
        [SwaggerResponse(StatusCodes.Status201Created, null, typeof(MotoResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]
        public async Task<ActionResult<MotoResponse>> ReturnByIdAsync([FromRoute] string id, UpdateMotocycleLicensePlateRequest request)
        {
            request.Id = Guid.Parse(id);
            Notifications.LogInfo($"[{nameof(LocacaoController)}] [{nameof(ReturnByIdAsync)}] - request: {request.ToJson()}");

            var result = await _mediator.Send(request);

            return ResponsePost("UpdateLicensePlateByIdAsync", result.Id, result);
        }

    }
}