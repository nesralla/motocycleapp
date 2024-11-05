using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Motocycle.Application.Commons.Responses;
using Motocycle.Application.UseCases.Delivery.Response;
using Motocycle.Domain.Core.Messages;
using Motocycle.Domain.Enums;

namespace Motocycle.Application.UseCases.Delivery.Request
{
    public class CreateDeliverymanRequest : CommandRequest<DeliverymanResponse>
    {
        public string Identificador { get; set; } // Nome
        public string Nome { get; set; } // Nome
        public string Cnpj { get; } // CNPJ (único)
        public DateTime Data_Nascimento { get; set; } // Data de nascimentoa
        public string Numero_Cnh { get; set; } // Número da CNH (único)
        public LicenseTypes Tipo_Cnh { get; set; } // Tipo da CNH

    }
}
