﻿using System;
using System.Collections.Generic;
using Motocycle.Domain.Core.Messages;
using Motocycle.Domain.Enums;

namespace Motocycle.Application.UseCases.Delivery.Response
{
    public class DeliverymanResponse : ResponseBase
    {
        public Guid Id { get; set; }
        public string Identificador { get; set; } // Nome
        public string Nome { get; set; } // Nome
        public string Cnpj { get; } // CNPJ (único)
        public DateTime Data_Nascimento { get; set; } // Data de nascimentoa
        public string Numero_Cnh { get; set; } // Número da CNH (único)
        public LicenseTypes Tipo_Cnh { get; set; } // Tipo da CNH
        public string Imagem_Cnh { get; set; } // Caminho da imagem da CNH

    }

}
