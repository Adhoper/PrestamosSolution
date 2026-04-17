using Prestamos.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prestamos.Data.Interfaces
{
    public interface IPrestamoRepository
    {
        Task<TasaEdad?> ObtenerTasaPorEdadAsync(int edad);
        Task<PlazoPrestamo?> ObtenerPlazoPorMesesAsync(int meses);
        Task InsertarLogConsultaAsync( DateTime fechaNacimiento, int edad, decimal monto, int meses,decimal? tasa,decimal? valorCuota, string? mensajeResultado, string? ipConsulta);
    }
}
