using System;
using System.Collections.Generic;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.UseCases.Moto.Response
{
    public class MotoResponse : ResponseBase
    {
        public Guid Id { get; set; }
        public string Identificador { get; set; } // Identificador
        public int Ano { get; set; } // Ano
        public string Modelo { get; set; } // Modelo
        public string Placa { get; set; } // Placa (única)

    }

}
