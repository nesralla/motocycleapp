
using Motocycle.Domain.Core.Models;
using Motocycle.Domain.Enums;

namespace Motocycle.Domain.Models
{
    public class Rent : Entity
    {
        public Guid Id { get; } // Identificador
        public Guid DeliverymanId { get; set; } // Identificador do entregador
        public Guid MotocyId { get; set; } // Identificador da moto
        public DateTime StartDate { get; set; } // Data de início
        public DateTime EndDate { get; set; } // Data de término
        public DateTime PreviousEndDate { get; set; } // Data prevista de término


    }
}
