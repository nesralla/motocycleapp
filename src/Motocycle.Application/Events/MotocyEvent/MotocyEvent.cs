using System;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.Events.MotocyEvent
{
    public class MotocyEvent : EventRequest
    {
        public CreateMotocyEvent CreateInfo { get; set; }
        public UpdateMotocyPlateEvent UpdateInfo { get; set; }
    }

    public class CreateMotocyEvent
    {
        public string LicensePlate { get; set; }
        public string MotocyModel { get; set; }
        public int Year { get; set; }
    }

    public class UpdateMotocyPlateEvent
    {
        public string LicensePlate { get; set; }

    }
}
