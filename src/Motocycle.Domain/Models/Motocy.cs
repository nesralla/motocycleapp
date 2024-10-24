using Motocycle.Domain.Core.Models;

namespace Motocycle.Domain.Models
{

    public class Motocy : Entity
    {
        public int Year { get; set; } // Ano
        public string MotocyModel { get; set; } // Modelo
        public string LicensePlate { get; set; } // Placa (única)



    }

}
