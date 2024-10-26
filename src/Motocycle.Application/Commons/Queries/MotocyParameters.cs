using System.Collections.Generic;
using Motocycle.Application.Commons.Queries.Base;

namespace Motocycle.Application.Commons.Queries
{
    public class MotocyParameters : QueryStringParameters
    {
        public string Search { get; set; }
        public string LicensePlate { get; set; }
        public string MotocyModel { get; set; }
        public int Year { get; set; }
        public string OrderBy { get; set; } = "CreateAt";

    }
}

