using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Prestamos.Data.Interfaces;
using Prestamos.Entities.Models;
using System.Data;

namespace Prestamos.Data.Repositories
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly string _connectionString;

        public PrestamoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<TasaEdad?> ObtenerTasaPorEdadAsync(int edad)
        {
            using var connection = CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Edad", edad);

            var result = await connection.QueryFirstOrDefaultAsync<TasaEdad>(
                "sp_ObtenerTasaPorEdad",
                parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<PlazoPrestamo?> ObtenerPlazoPorMesesAsync(int meses)
        {
            using var connection = CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Meses", meses);

            var result = await connection.QueryFirstOrDefaultAsync<PlazoPrestamo>(
                "sp_ObtenerPlazoPorMeses",
                parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task InsertarLogConsultaAsync(
            DateTime fechaNacimiento,
            int edad,
            decimal monto,
            int meses,
            decimal? tasa,
            decimal? valorCuota,
            string? mensajeResultado,
            string? ipConsulta)
        {
            using var connection = CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@FechaNacimiento", fechaNacimiento);
            parameters.Add("@Edad", edad);
            parameters.Add("@Monto", monto);
            parameters.Add("@Meses", meses);
            parameters.Add("@Tasa", tasa);
            parameters.Add("@ValorCuota", valorCuota);
            parameters.Add("@MensajeResultado", mensajeResultado);
            parameters.Add("@IpConsulta", ipConsulta);

            await connection.ExecuteAsync(
                "sp_InsertarLogConsultaPrestamo",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}
