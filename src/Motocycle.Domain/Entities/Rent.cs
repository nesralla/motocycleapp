
using Motocycle.Domain.Enums;

namespace Motocycle.Domain.Entities
{
    public class Rent
    {
        public Guid Id { get; } // Identificador
        public Guid DeliverymanId { get; private set; } // Identificador do entregador
        public Guid MotocyId { get; private set; } // Identificador da moto
        public DateTime StartDate { get; private set; } // Data de início
        public DateTime EndDate { get; private set; } // Data de término
        public DateTime PreviousEndDate { get; private set; } // Data prevista de término
        public Rent(Guid deliverymandid, Guid motocyid, DateTime startdate, DateTime enddate,
        DateTime previousenddate)
        {
            DeliverymanId = deliverymandid;
            MotocyId = motocyid;
            StartDate = startdate;
            EndDate = enddate;
            PreviousEndDate = previousenddate;
        }

    }
}
