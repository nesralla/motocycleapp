using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Motocycle.Application.Commons.Responses;
using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.UseCases.Moto.Request
{
    public class GetMotosRequest : CommandRequest<List<MotoResponse>>
    {

    }
}
