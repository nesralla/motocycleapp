using System;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
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
            Notifications.LogInfo($"[{nameof(MotoRequest)}] - Event: {data.LicensePlate} | Code: {data.Identification}");

            var entity = await _motocyService.GetAllQuery.Include(c => c.LicensePlate).FirstOrDefaultAsync(x => x.LicensePlate.Equals(data.LicensePlate), cancellationToken);

            if (entity is null)
            {
                Notifications.Handle(DomainNotification.Error("Motocy", $"Motocycle not found  for: {data.ToJson()}"));

                Notifications.LogError($"[{nameof(SyncCallbackInfoAsync)}] - Motocycle not found for  {data.ToJson()}");
                return;
            }

            entity.Identification = data.Identification;
            entity.LicensePlate = data.LicensePlate;
            entity.MotocyModel = data.MotocyModel;
            entity.Year = data.Year;
            entity.CreateAt = DateTime.UtcNow;
            entity.IsClosed = false;
            entity.IsDeleted = false;


            await _mediator.Publish(new MotocyEvent
            {
                CreateInfo = new CreateMotocyEvent
                {

                    LicensePlate = entity.LicensePlate,
                    MotocyModel = entity.MotocyModel,
                    Year = entity.Year

                }
            });

        }
    }
}