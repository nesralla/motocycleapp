namespace Motocycle.Domain.Entities
{

    public class Motocycle
    {
        public Guid Id { get; } // Identificador
        public int Year { get; private set; } // Ano
        public string MotocycleModel { get; private set; } // Modelo
        public string LicensePlate { get; private set; } // Placa (única)

        public Motocycle(int year, string motocyclemodel, string licenseplate)
        {
            Year = year;
            MotocycleModel = motocyclemodel;
            LicensePlate = licenseplate;
        }

    }

}
