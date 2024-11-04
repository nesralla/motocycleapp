using MediatR;
using Microsoft.EntityFrameworkCore;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Application.Interfaces;
using Motocycle.Domain.Interfaces.Services;
using Motocycle.Application.UseCases.Moto.Request;
using Motocycle.Infra.CrossCutting.Commons.Extensions;
using Motocycle.Application.Events.MotocyEvent;

namespace Motocycle.Application.Services
{
    public class CallbackAppService : ICallbackAppService
    {
        private readonly IMediator _mediator;
        private readonly IMotocyService _motocyService;

        protected IHandler<DomainNotification> Notifications { get; }

        public CallbackAppService(
            IHandler<DomainNotification> notifications,
            IMediator mediator,
            IMotocyService motocyService
        )
        {
            _mediator = mediator;
            Notifications = notifications;
            _motocyService = motocyService;
        }



        public async Task SyncCallbackInfoAsync(MotoRequest data, CancellationToken cancellationToken = default)
        {
            Notifications.LogInfo($"[{nameof(MotoRequest)}] - Event: {data.Placa} | Code: {data.Identificador}");

            var entity = await _motocyService.GetAllQuery.Include(c => c.LicensePlate).FirstOrDefaultAsync(x => x.LicensePlate.Equals(data.Placa), cancellationToken);

            if (entity is null)
            {
                Notifications.Handle(DomainNotification.Error("Motocy", $"Motocycle not found  for: {data.ToJson()}"));

                Notifications.LogError($"[{nameof(SyncCallbackInfoAsync)}] - Motocycle not found for  {data.ToJson()}");
                return;
            }

            entity.Identification = data.Identificador;
            entity.LicensePlate = data.Placa;
            entity.MotocyModel = data.Modelo;
            entity.Year = data.Ano;
            entity.CreateAt = DateTime.UtcNow;
            entity.IsClosed = false;
            entity.IsDeleted = false;


            await _mediator.Publish(new MotocyEvent
            {
                MotocycleInfo = new CreateMotocycleSqsRequest
                {

                    LicensePlate = entity.LicensePlate,
                    MotocyModel = entity.MotocyModel,
                    Year = entity.Year

                }
            });

        }
    }
}