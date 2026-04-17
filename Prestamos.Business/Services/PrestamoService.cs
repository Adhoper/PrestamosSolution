using Prestamos.Business.Interfaces;
using Prestamos.Data.Interfaces;
using Prestamos.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prestamos.Business.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;

        public PrestamoService(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
        }

        public async Task<CalcularPrestamoResponseDto> CalcularPrestamoAsync(
            CalcularPrestamoRequestDto request,
            string? ipConsulta)
        {
            var edad = CalcularEdad(request.FechaNacimiento);

            // Validación: menor a 18
            if (edad < 18)
            {
                var mensaje = "Lo Sentimos aun no cuenta con la edad para solicitar esta producto.";

                await _prestamoRepository.InsertarLogConsultaAsync(
                    request.FechaNacimiento,
                    edad,
                    request.Monto,
                    request.Meses,
                    null,
                    null,
                    mensaje,
                    ipConsulta);

                return new CalcularPrestamoResponseDto
                {
                    Success = false,
                    Message = mensaje,
                    Edad = edad,
                    Monto = request.Monto,
                    Meses = request.Meses,
                    Tasa = null,
                    Cuota = null
                };
            }

            // Validación: mayor a 25
            if (edad > 25)
            {
                var mensaje = "Favor pasar por una de nuestras sucursales para evaluar su caso.";

                await _prestamoRepository.InsertarLogConsultaAsync(
                    request.FechaNacimiento,
                    edad,
                    request.Monto,
                    request.Meses,
                    null,
                    null,
                    mensaje,
                    ipConsulta);

                return new CalcularPrestamoResponseDto
                {
                    Success = false,
                    Message = mensaje,
                    Edad = edad,
                    Monto = request.Monto,
                    Meses = request.Meses,
                    Tasa = null,
                    Cuota = null
                };
            }

            // Validación de meses
            var plazo = await _prestamoRepository.ObtenerPlazoPorMesesAsync(request.Meses);
            if (plazo == null)
            {
                var mensaje = "La cantidad de meses no es válida.";

                await _prestamoRepository.InsertarLogConsultaAsync(
                    request.FechaNacimiento,
                    edad,
                    request.Monto,
                    request.Meses,
                    null,
                    null,
                    mensaje,
                    ipConsulta);

                return new CalcularPrestamoResponseDto
                {
                    Success = false,
                    Message = mensaje,
                    Edad = edad,
                    Monto = request.Monto,
                    Meses = request.Meses,
                    Tasa = null,
                    Cuota = null
                };
            }

            // Obtener tasa por edad
            var tasaEdad = await _prestamoRepository.ObtenerTasaPorEdadAsync(edad);
            if (tasaEdad == null)
            {
                var mensaje = "No se encontró una tasa configurada para la edad proporcionada.";

                await _prestamoRepository.InsertarLogConsultaAsync(
                    request.FechaNacimiento,
                    edad,
                    request.Monto,
                    request.Meses,
                    null,
                    null,
                    mensaje,
                    ipConsulta);

                return new CalcularPrestamoResponseDto
                {
                    Success = false,
                    Message = mensaje,
                    Edad = edad,
                    Monto = request.Monto,
                    Meses = request.Meses,
                    Tasa = null,
                    Cuota = null
                };
            }

            // Fórmula: Cuota = (Monto * Tasa) / Cantidad de meses
            var cuota = (request.Monto * tasaEdad.Tasa) / request.Meses;
            cuota = Math.Round(cuota, 2);

            var mensajeExito = "Cálculo realizado correctamente.";

            await _prestamoRepository.InsertarLogConsultaAsync(
                request.FechaNacimiento,
                edad,
                request.Monto,
                request.Meses,
                tasaEdad.Tasa,
                cuota,
                mensajeExito,
                ipConsulta);

            return new CalcularPrestamoResponseDto
            {
                Success = true,
                Message = mensajeExito,
                Edad = edad,
                Monto = request.Monto,
                Meses = request.Meses,
                Tasa = tasaEdad.Tasa,
                Cuota = cuota
            };
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > hoy.AddYears(-edad))
            {
                edad--;
            }

            return edad;
        }
    }
}
