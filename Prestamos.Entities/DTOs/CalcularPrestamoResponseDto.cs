using System;
using System.Collections.Generic;
using System.Text;

namespace Prestamos.Entities.DTOs
{
    public class CalcularPrestamoResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Edad { get; set; }
        public decimal? Tasa { get; set; }
        public decimal? Cuota { get; set; }
        public decimal Monto { get; set; }
        public int Meses { get; set; }
    }
}
