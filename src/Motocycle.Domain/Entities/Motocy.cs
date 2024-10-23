namespace Motocycle.Domain.Entities
{

    public class Motocy
    {
        public Guid Id { get; } // Identificador
        public int Year { get; private set; } // Ano
        public string MotocyModel { get; private set; } // Modelo
        public string LicensePlate { get; private set; } // Placa (única)

        public Motocy(int year, string motocymodel, string licenseplate)
        {
            Year = year;
            MotocyModel = motocymodel;
            LicensePlate = licenseplate;
        }

    }

}
