
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

    public class MotocyService : BaseServiceValidation<Motocy>, IMotocyService
    {
        public readonly IConfiguration _configuration;
        private readonly IMotocyRepository _repository;

        public MotocyService(
            IHandler<DomainNotification> notifications,
            IMotocyRepository repository,
            IConfiguration configuration,
            IRegisterValidation<Motocy> registerValidation) :
            base(repository, notifications, registerValidation)
        {
            _configuration = configuration;
            _repository = repository;
        }
        public async Task<Motocy> GetByPlateAsync(string plate)
        {
            var entity = await GetAllQueryAsNoTracking.FirstOrDefaultAsync(x => x.LicensePlate == plate);

            if (entity is null)
                Notifications.Handle(DomainNotification.ModelValidation("MOTO_NOTFOUND", "Moto não encontrada"));

            return entity;
        }
        public async Task<Motocy> UpdateLicensePlateAsync(Guid id, Motocy motocycle)
        {
            var entity = await BaseRepository.GetByIdAsync(id);
            if (entity is null)
            {
                Notifications.Handle(DomainNotification.ModelValidation("UpdatePlate", "Moto não encontrada"));
                return default;
            }

            motocycle.LicensePlate = entity?.LicensePlate;


            _ = _repository.UpdateAsync(motocycle);
            return entity;
        }

        public async Task<Motocy> GetDefaultMotoAsync()
        {
            var entity = await _repository.GetDefaultAsync();

            if (entity is null)
                Notifications.Handle(DomainNotification.ModelValidation("MOTO_NOTFOUND", "Nao existe padrão cadastrado"));

            return entity;
        }
        public async Task<Motocy> RemoveMotocycleAsync(Guid id)
        {
            var motoccycle = await BaseRepository.GetByIdAsync(id);

            if (motoccycle is null)
            {
                Notifications.Handle(DomainNotification.ModelValidation("MOTO_NOTFOUND", "Moto não encontrada"));
                return default;
            }

            BaseRepository.Remove(motoccycle);

            return motoccycle;


        }
    }


}