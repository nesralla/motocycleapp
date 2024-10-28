using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Motocycle.Application.Commons.Responses;
using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.UseCases.Moto.Request
{
    public class CreateMotoRequest : CommandRequest<MotoResponse>
    {

        public string Identificador { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
    }


}
