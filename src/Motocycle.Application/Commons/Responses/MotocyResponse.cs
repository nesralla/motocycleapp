using System;
using System.Collections.Generic;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.Commons.Responses
{
    public class MotocyResponse : ResponseBase
    {
        public Guid Id { get; set; }
        public int Year { get; set; } // Ano
        public string MotocyModel { get; set; } // Modelo
        public string LicensePlate { get; set; } // Placa (única)

    }

}
