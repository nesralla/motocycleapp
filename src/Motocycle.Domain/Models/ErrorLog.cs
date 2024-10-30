using System;
using Motocycle.Domain.Core.Models;

namespace Motocycle.Domain.Models
{
    public class ErrorLog : Entity
    {
        public string RootCause { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
