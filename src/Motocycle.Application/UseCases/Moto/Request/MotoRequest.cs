using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Motocycle.Application.Commons.Responses;
using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.UseCases.Moto.Request
{
    public class MotoRequest : CommandRequest<MotoResponse>
    {
        public Guid Id { get; set; }
        public string Identification { get; set; }
        public int Year { get; set; }
        public string MotocyModel { get; set; }
        public string LicensePlate { get; set; }
    }

}
