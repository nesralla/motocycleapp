using Motocycle.Domain.Core.Models;
using Motocycle.Domain.Enums;

namespace Motocycle.Domain.Models
{

    public class Deliveryman : Entity
    {
        public string Name { get; set; } // Nome
        public string NationalID { get; } // CNPJ (único)
        public DateTime DateBorn { get; set; } // Data de nascimentoa
        public string DriveLicense { get; set; } // Número da CNH (único)
        public LicenseTypes LicenseType { get; set; } // Tipo da CNH
        public string DriveLicenseFile { get; set; } // Caminho da imagem da CNH

    }

}
