using Motocycle.Domain.Enums;

namespace Motocycle.Domain.Models
{
    public class Plan
    {
        public RentPlans Type { get; private set; }
        public int DurationDays { get; private set; }
        public decimal CostPerDay { get; private set; }

        public Plan(RentPlans type)
        {
            Type = type;
            (DurationDays, CostPerDay) = GetPlanDetail(type);
        }

        private (int duration, decimal cost) GetPlanDetail(RentPlans type)
        {
            return type switch
            {
                RentPlans.A => (7, 30.00m),
                RentPlans.B => (15, 28.00m),
                RentPlans.C => (30, 22.00m),
                RentPlans.D => (45, 20.00m),
                RentPlans.E => (50, 18.00m),
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Dados inválidos.")
            };
        }

        public decimal TotalCost()
        {
            return DurationDays * CostPerDay;
        }
    }
}
