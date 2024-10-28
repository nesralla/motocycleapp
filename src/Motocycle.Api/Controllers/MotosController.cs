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

namespace Motocycle.Api.Controllers
{
    public class MotosController : MainController
    {
        private readonly IMediator _mediator;

        public MotosController(
            IHandler<DomainNotification> notifications,
            IMediator mediator) : base(notifications)
        {

            _mediator = mediator;
        }

        /// <summary>
        /// Create Moto
        /// </summary>
        /// <param name="Placa"></param>
        /// <param name="Modelo"></param>
        /// <param name="Ano"></param>
        /// <returns></returns>
        [HttpPost("motos")]
        [SwaggerResponse(StatusCodes.Status201Created, null, typeof(MotoResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]

        public async Task<ActionResult<MotoResponse>> CreateMotoAsync([FromBody] MotoRequest request)
        {
            Notifications.LogInfo($"[{nameof(MotosController)}] [{nameof(CreateMotoAsync)}] - request: {request.ToJson()}");

            var result = await _mediator.Send(request);
            return ResponsePost("CreateMotoAsync", result.Id, result);
        }

        /// <summary>
        /// Update licnese plate for a Motocycle
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Placa"></param>
        /// <returns></returns>
        [HttpPut("motos/{Id}/placa")]
        [SwaggerResponse(StatusCodes.Status201Created, null, typeof(MotoResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]
        public async Task<ActionResult<MotoResponse>> UpdateLicensePlateByIdAsync([FromRoute] string id, [FromBody] MotoRequest request)
        {
            request.Id = Guid.Parse(id);
            Notifications.LogInfo($"[{nameof(MotosController)}] [{nameof(UpdateLicensePlateByIdAsync)}] - request: {request.ToJson()}");

            var result = await _mediator.Send(request);

            return ResponsePost("UpdateLicensePlateByIdAsync", result.Id, result);
        }

        /// <summary>
        /// Get Motos
        /// </summary>
        /// <returns></returns>
        [HttpGet("motos")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(MotoResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]
        public async Task<ActionResult<List<MotoResponse>>> GetMotosAsync([FromBody] MotoRequest request)
        {

            Notifications.LogInfo($"[{nameof(MotosController)}] [{nameof(GetMotosAsync)}] - request: {request.ToJson()}");

            var result = await _mediator.Send(request);

            return ResponseGet(new List<MotoResponse> { result });
        }




    }
}