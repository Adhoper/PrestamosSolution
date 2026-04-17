using System;
using System.Collections.Generic;
using System.Text;

namespace Prestamos.Entities.DTOs
{
    public class CalcularPrestamoRequestDto
    {
        public DateTime FechaNacimiento { get; set; }
        public decimal Monto { get; set; }
        public int Meses { get; set; }
    }
}
