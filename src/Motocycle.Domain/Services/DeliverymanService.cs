
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories;
using Motocycle.Domain.Interfaces.Services;
using Motocycle.Domain.Interfaces.Validation;
using Motocycle.Domain.Models;
using Motocycle.Domain.Services.Base;
using Motocycle.Infra.CrossCutting.Commons.Extensions;

namespace Motocycle.Domain.Services
{

    public class DeliverymanService : BaseServiceValidation<Deliveryman>, IDeliverymanService
    {
        public readonly IConfiguration _configuration;
        private readonly IDeliverymanRepository _repository;

        public DeliverymanService(
            IHandler<DomainNotification> notifications,
            IDeliverymanRepository repository,
            IConfiguration configuration,
            IRegisterValidation<Deliveryman> registerValidation) :
            base(repository, notifications, registerValidation)
        {
            _configuration = configuration;
            _repository = repository;
        }
        public async Task<Deliveryman> GetByNationalIDAsync(string nationalid)
        {
            var entity = await GetAllQueryAsNoTracking.FirstOrDefaultAsync(x => x.NationalID == nationalid);

            if (entity is null)
                Notifications.Handle(DomainNotification.ModelValidation("Deliveryman_NOTFOUND", "Deliveryman não encontrada"));

            return entity;
        }


        public async Task<Deliveryman> GetDefaultDeliverymanAsync()
        {
            var entity = await _repository.GetDefaultAsync();

            if (entity is null)
                Notifications.Handle(DomainNotification.ModelValidation("Deliveryman_NOTFOUND", "Nao existe padrão cadastrado"));

            return entity;
        }
        public async Task<Deliveryman> RemoveDeliverymanAsync(Guid id)
        {
            var deliveryman = await BaseRepository.GetByIdAsync(id);

            if (deliveryman is null)
            {
                Notifications.Handle(DomainNotification.ModelValidation("Deliveryman_NOTFOUND", "Deliveryman não encontrada"));
                return default;
            }

            BaseRepository.Remove(deliveryman);

            return deliveryman;


        }
    }


}