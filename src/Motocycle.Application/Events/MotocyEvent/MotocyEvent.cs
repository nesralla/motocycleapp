using System;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.Events.MotocyEvent
{
    public class MotocyEvent : EventRequest
    {
        public CreateMotocycleSqsRequest MotocycleInfo { get; set; }
    }

    public sealed class CreateMotocycleSqsRequest
    {
        public string LicensePlate { get; set; }
        public string MotocyModel { get; set; }
        public int Year { get; set; }
    }


}
