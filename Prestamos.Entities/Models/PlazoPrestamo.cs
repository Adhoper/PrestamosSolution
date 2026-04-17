using System;
using System.Collections.Generic;
using System.Text;

namespace Prestamos.Entities.Models
{
    public class PlazoPrestamo
    {
        public int IdPlazo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int ValorMeses { get; set; }
    }
}
