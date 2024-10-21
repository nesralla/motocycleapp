using Motocycle.Domain.Enums;

namespace Motocycle.Domain.Entities
{

    public class Deliveryman
    {
        public Guid Id { get; } // Identificador
        public string Name { get; set; } // Nome
        public string NationalID { get; } // CNPJ (único)
        public DateTime DateBorn { get; private set; } // Data de nascimento
        public string DriveLicense { get; private set; } // Número da CNH (único)
        public LicenseTypes LicenseType { get; set; } // Tipo da CNH
        public string DriveLicenseFile { get; private set; } // Caminho da imagem da CNH

        public Deliveryman(string name, string nationalid, DateTime dateborn, string drivelicense, LicenseTypes licensetype,
        string drivelicensefile)
        {
            Name = name;
            NationalID = nationalid;
            DateBorn = dateborn;
            DriveLicense = drivelicense;
            LicenseType = licensetype;
            DriveLicenseFile = drivelicensefile;
        }

    }

}
