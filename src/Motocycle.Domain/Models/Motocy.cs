using Motocycle.Domain.Core.Models;

namespace Motocycle.Domain.Models
{

    public class Motocy : Entity
    {
        public string Identification { get; set; } // Identificação
        public int Year { get; set; } // Ano
        public string MotocyModel { get; set; } // Modelo
        public string LicensePlate { get; set; } // Placa (única)

        public Motocy(string identification, int year, string motocyModel, string licensePlate)
        {
            Identification = identification;
            Year = year;
            MotocyModel = motocyModel;
            LicensePlate = licensePlate;
        }

    }

}
