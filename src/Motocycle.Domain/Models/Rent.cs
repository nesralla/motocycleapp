
using Motocycle.Domain.Core.Models;
using Motocycle.Domain.Enums;

namespace Motocycle.Domain.Models
{
    public class Rent : Entity
    {
        public string Identification { get; set; } // Identificação
        public Guid DeliverymanId { get; set; } // Identificador do entregador
        public Guid MotocyId { get; set; } // Identificador da moto
        public DateTime StartDate { get; set; } // Data de início
        public DateTime EndDate { get; set; } // Data de término
        public DateTime PreviousEndDate { get; set; } // Data prevista de término
        public StatusTypes Status { get; set; } // Status
        public decimal PreviousValue { get; set; } // Valor previsto
        public decimal FinishValue { get; set; } // Valor final

        public int RentDays { get; set; }
        public Plan RentPlan { get; set; }
        public RentPlans RentTypePlan { get; set; }
        public Motocy Motocy { get; set; }
        public Deliveryman Deliveryman { get; set; }


    }
}
