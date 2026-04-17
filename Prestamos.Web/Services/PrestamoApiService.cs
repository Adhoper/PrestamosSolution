using Prestamos.Web.Interfaces;
using Prestamos.Web.Models;

namespace Prestamos.Web.Services
{
    public class PrestamoApiService : IPrestamoApiService
    {
        private readonly HttpClient _httpClient;

        public PrestamoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PrestamoViewModel> CalcularPrestamoAsync(PrestamoViewModel model)
        {
            var request = new
            {
                fechaNacimiento = model.FechaNacimiento,
                monto = model.Monto,
                meses = model.Meses
            };

            var response = await _httpClient.PostAsJsonAsync("api/Prestamos/calcular", request);

            if (!response.IsSuccessStatusCode)
            {
                model.Success = false;
                model.Message = $"Error al consumir la API: {response.StatusCode}";
                return model;
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();

            if (result == null)
            {
                model.Success = false;
                model.Message = "No se recibió una respuesta válida desde la API.";
                return model;
            }

            model.Success = result.Success;
            model.Message = result.Message;
            model.Edad = result.Edad;
            model.Tasa = result.Tasa;
            model.Cuota = result.Cuota;

            return model;
        }

        private class ApiResponse
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
}
