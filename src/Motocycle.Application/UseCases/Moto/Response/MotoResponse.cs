using System;
using System.Collections.Generic;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.UseCases.Moto.Response
{
    public class MotoResponse : ResponseBase
    {
        public Guid Id { get; set; }
        public string Identification { get; set; } // Identificador
        public int Year { get; set; } // Ano
        public string MotocyModel { get; set; } // Modelo
        public string LicensePlate { get; set; } // Placa (única)

    }

}
