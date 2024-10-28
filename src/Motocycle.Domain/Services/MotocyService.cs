
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

namespace Motocycle.Domain.Services
{

    public class MotocyService(
        IHandler<DomainNotification> notifications,
        IMotocyRepository repository,
        IRegisterValidation<Motocy> registerValidation) : BaseServiceValidation<Motocy>(repository, notifications, registerValidation), IMotocyService
    {
        public readonly IConfiguration _configuration;
        private readonly IMotocyRepository _repository = repository;

        public async Task<Motocy> GetByPlateAsync(string plate)
        {
            var entity = await GetAllQueryAsNoTracking.FirstOrDefaultAsync(x => x.LicensePlate == plate);

            if (entity is null)
                Notifications.Handle(DomainNotification.ModelValidation("MOTO_NOTFOUND", "Moto não encontrado"));

            return entity;
        }
        public async Task<Motocy> GetDefaultMotoAsync()
        {
            var entity = await _repository.GetDefaultAsync();

            if (entity is null)
                Notifications.Handle(DomainNotification.ModelValidation("MOTO_NOTFOUND", "Nao existe padrão cadastrado"));

            return entity;
        }
    }


}