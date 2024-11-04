using System;
using System.Collections.Generic;
using Motocycle.Domain.Core.Messages;
using Motocycle.Domain.Enums;

namespace Motocycle.Application.UseCases.Delivery.Response
{
    public class DeliverymanResponse : ResponseBase
    {
        public Guid Id { get; set; }
        public string Identification { get; set; } // Nome
        public string Name { get; set; } // Nome
        public string NationalID { get; } // CNPJ (único)
        public DateTime DateBorn { get; set; } // Data de nascimentoa
        public string DriveLicense { get; set; } // Número da CNH (único)
        public LicenseTypes LicenseType { get; set; } // Tipo da CNH
        public string DriveLicenseFile { get; set; } // Caminho da imagem da CNH

    }

}
